using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using PlayFab.SharedModels;
using UnityEngine;
using UnityEngine.Networking;

namespace PlayFab.Internal
{
	public class PlayFabWebRequest : IPlayFabPlugin, ITransportPlugin
	{
		private static readonly Queue<Action> ResultQueueTransferThread = new Queue<Action>();

		private static readonly Queue<Action> ResultQueueMainThread = new Queue<Action>();

		private static readonly List<CallRequestContainer> ActiveRequests = new List<CallRequestContainer>();

		private static bool certValidationSet = false;

		private static readonly object _ThreadLock = new object();

		private static bool _isApplicationPlaying;

		private static int _activeCallCount = 0;

		private static string _unityVersion;

		private bool _isInitialized;

		public static RemoteCertificateValidationCallback CustomCertValidationHook
		{
			set
			{
				ServicePointManager.ServerCertificateValidationCallback = value;
				certValidationSet = true;
			}
		}

		public bool IsInitialized
		{
			get
			{
				return _isInitialized;
			}
		}

		public static void SkipCertificateValidation()
		{
			RemoteCertificateValidationCallback serverCertificateValidationCallback = AcceptAllCertifications;
			ServicePointManager.ServerCertificateValidationCallback = serverCertificateValidationCallback;
			certValidationSet = true;
		}

		public void Initialize()
		{
			SetupCertificates();
			_isApplicationPlaying = true;
			_unityVersion = Application.unityVersion;
			_isInitialized = true;
		}

		public void OnDestroy()
		{
			_isApplicationPlaying = false;
			lock (ResultQueueTransferThread)
			{
				ResultQueueTransferThread.Clear();
			}
			lock (ActiveRequests)
			{
				ActiveRequests.Clear();
			}
			lock (_ThreadLock)
			{
			}
		}

		private void SetupCertificates()
		{
			ServicePointManager.DefaultConnectionLimit = 10;
			ServicePointManager.Expect100Continue = false;
			if (!certValidationSet)
			{
				Debug.LogWarning("PlayFab API calls will likely fail because you have not set up a HttpWebRequest certificate validation mechanism");
				Debug.LogWarning("Please set a validation callback into PlayFab.Internal.PlayFabWebRequest.CustomCertValidationHook, or set PlayFab.Internal.PlayFabWebRequest.SkipCertificateValidation()");
			}
		}

		private static bool AcceptAllCertifications(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			return true;
		}

		public void SimpleGetCall(string fullUrl, Action<byte[]> successCallback, Action<string> errorCallback)
		{
			Thread thread = new Thread((ThreadStart)delegate
			{
				SimpleHttpsWorker("GET", fullUrl, null, successCallback, errorCallback);
			});
			thread.Start();
		}

		public void SimplePutCall(string fullUrl, byte[] payload, Action<byte[]> successCallback, Action<string> errorCallback)
		{
			Thread thread = new Thread((ThreadStart)delegate
			{
				SimpleHttpsWorker("PUT", fullUrl, payload, successCallback, errorCallback);
			});
			thread.Start();
		}

		public void SimplePostCall(string fullUrl, byte[] payload, Action<byte[]> successCallback, Action<string> errorCallback)
		{
			Thread thread = new Thread((ThreadStart)delegate
			{
				SimpleHttpsWorker("POST", fullUrl, payload, successCallback, errorCallback);
			});
			thread.Start();
		}

		private void SimpleHttpsWorker(string httpMethod, string fullUrl, byte[] payload, Action<byte[]> successCallback, Action<string> errorCallback)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(fullUrl);
			httpWebRequest.UserAgent = "UnityEngine-Unity; Version: " + _unityVersion;
			httpWebRequest.Method = httpMethod;
			httpWebRequest.KeepAlive = PlayFabSettings.RequestKeepAlive;
			httpWebRequest.Timeout = PlayFabSettings.RequestTimeout;
			httpWebRequest.AllowWriteStreamBuffering = false;
			httpWebRequest.ReadWriteTimeout = PlayFabSettings.RequestTimeout;
			if (payload != null)
			{
				httpWebRequest.ContentLength = payload.LongLength;
				using (Stream stream = httpWebRequest.GetRequestStream())
				{
					stream.Write(payload, 0, payload.Length);
				}
			}
			try
			{
				WebResponse response = httpWebRequest.GetResponse();
				byte[] array = null;
				using (Stream stream2 = response.GetResponseStream())
				{
					if (stream2 != null)
					{
						array = new byte[response.ContentLength];
						stream2.Read(array, 0, array.Length);
					}
				}
				successCallback(array);
			}
			catch (WebException ex)
			{
				try
				{
					using (Stream stream3 = ex.Response.GetResponseStream())
					{
						if (stream3 != null)
						{
							using (StreamReader streamReader = new StreamReader(stream3))
							{
								errorCallback(streamReader.ReadToEnd());
								return;
							}
						}
					}
				}
				catch (Exception exception)
				{
					Debug.LogException(exception);
				}
			}
			catch (Exception exception2)
			{
				Debug.LogException(exception2);
			}
		}

		public void MakeApiCall(object reqContainerObj)
		{
			CallRequestContainer callRequestContainer = (CallRequestContainer)reqContainerObj;
			callRequestContainer.HttpState = HttpRequestState.Idle;
			lock (ActiveRequests)
			{
				ActiveRequests.Insert(0, callRequestContainer);
			}
			ActivateThreadWorker();
		}

		private static void ActivateThreadWorker()
		{
		}

		private static void UpdateWorkerThread()
		{
			int count = ActiveRequests.Count;
			for (int num = count - 1; num >= 0; num--)
			{
				switch (ActiveRequests[num].HttpState)
				{
				case HttpRequestState.Error:
					ActiveRequests.RemoveAt(num);
					break;
				case HttpRequestState.Idle:
					Post(ActiveRequests[num]);
					break;
				case HttpRequestState.Sent:
					if (ActiveRequests[num].HttpRequest.isDone)
					{
						ProcessHttpResponse(ActiveRequests[num]);
					}
					break;
				case HttpRequestState.Received:
					ProcessJsonResponse(ActiveRequests[num]);
					ActiveRequests.RemoveAt(num);
					break;
				}
			}
		}

		private static void Post(CallRequestContainer reqContainer)
		{
			try
			{
				reqContainer.HttpRequest = new UnityWebRequest(reqContainer.FullUrl);
				foreach (KeyValuePair<string, string> requestHeader in reqContainer.RequestHeaders)
				{
					reqContainer.HttpRequest.SetRequestHeader(requestHeader.Key, requestHeader.Value);
				}
				reqContainer.HttpRequest.SetRequestHeader("Content-Type", "application/json");
				reqContainer.HttpRequest.method = "POST";
				reqContainer.HttpRequest.downloadHandler = new DownloadHandlerBuffer();
				reqContainer.HttpRequest.uploadHandler = new UploadHandlerRaw(reqContainer.Payload);
				reqContainer.HttpRequest.Send();
				reqContainer.HttpState = HttpRequestState.Sent;
			}
			catch (WebException ex)
			{
				reqContainer.JsonResponse = ResponseToString(ex.Response) ?? string.Concat(ex.Status, ": WebException making http request to: ", reqContainer.FullUrl);
				WebException exception = new WebException(reqContainer.JsonResponse, ex);
				Debug.LogException(exception);
				QueueRequestError(reqContainer);
			}
			catch (Exception innerException)
			{
				reqContainer.JsonResponse = "Unhandled exception in Post : " + reqContainer.FullUrl;
				Exception exception2 = new Exception(reqContainer.JsonResponse, innerException);
				Debug.LogException(exception2);
				QueueRequestError(reqContainer);
			}
		}

		private static void ProcessHttpResponse(CallRequestContainer reqContainer)
		{
			try
			{
				if (reqContainer.HttpRequest.error == null)
				{
					reqContainer.JsonResponse = reqContainer.HttpRequest.downloadHandler.text;
				}
				else
				{
					reqContainer.JsonResponse = reqContainer.HttpRequest.error;
					QueueRequestError(reqContainer);
				}
				reqContainer.HttpState = HttpRequestState.Received;
			}
			catch (Exception innerException)
			{
				string text = "Unhandled exception in ProcessHttpResponse : " + reqContainer.FullUrl;
				reqContainer.JsonResponse = reqContainer.JsonResponse ?? text;
				Exception exception = new Exception(text, innerException);
				Debug.LogException(exception);
				QueueRequestError(reqContainer);
			}
		}

		private static void QueueRequestError(CallRequestContainer reqContainer)
		{
			reqContainer.Error = PlayFabHttp.GeneratePlayFabError(reqContainer.ApiEndpoint, reqContainer.JsonResponse, reqContainer.CustomData);
			reqContainer.HttpState = HttpRequestState.Error;
			Debug.Log("QueueRequestError response: " + reqContainer.JsonResponse);
			lock (ResultQueueTransferThread)
			{
				ResultQueueTransferThread.Enqueue(delegate
				{
					PlayFabHttp.SendErrorEvent(reqContainer.ApiRequest, reqContainer.Error);
					if (reqContainer.ErrorCallback != null)
					{
						reqContainer.ErrorCallback(reqContainer.Error);
					}
				});
			}
		}

		private static void ProcessJsonResponse(CallRequestContainer reqContainer)
		{
			try
			{
				ISerializerPlugin plugin = PluginManager.GetPlugin<ISerializerPlugin>(PluginContract.PlayFab_Serializer, string.Empty);
				HttpResponseObject httpResponseObject = plugin.DeserializeObject<HttpResponseObject>(reqContainer.JsonResponse);
				if (httpResponseObject == null || httpResponseObject.code != 200)
				{
					QueueRequestError(reqContainer);
					return;
				}
				reqContainer.JsonResponse = plugin.SerializeObject(httpResponseObject.data);
				reqContainer.DeserializeResultJson();
				reqContainer.ApiResult.Request = reqContainer.ApiRequest;
				reqContainer.ApiResult.CustomData = reqContainer.CustomData;
				if (_isApplicationPlaying)
				{
					SingletonMonoBehaviour<PlayFabHttp>.instance.OnPlayFabApiResult(reqContainer);
				}
				lock (ResultQueueTransferThread)
				{
					ResultQueueTransferThread.Enqueue(delegate
					{
						PlayFabDeviceUtil.OnPlayFabLogin(reqContainer.ApiResult, reqContainer.settings, reqContainer.instanceApi);
					});
				}
				lock (ResultQueueTransferThread)
				{
					ResultQueueTransferThread.Enqueue(delegate
					{
						try
						{
							PlayFabHttp.SendEvent(reqContainer.ApiEndpoint, reqContainer.ApiRequest, reqContainer.ApiResult, ApiProcessingEventType.Post);
							reqContainer.InvokeSuccessCallback();
						}
						catch (Exception exception2)
						{
							Debug.LogException(exception2);
						}
					});
				}
			}
			catch (Exception innerException)
			{
				string text = "Unhandled exception in ProcessJsonResponse : " + reqContainer.FullUrl;
				reqContainer.JsonResponse = reqContainer.JsonResponse ?? text;
				Exception exception = new Exception(text, innerException);
				Debug.LogException(exception);
				QueueRequestError(reqContainer);
			}
		}

		public void Update()
		{
			UpdateWorkerThread();
			while (ResultQueueTransferThread.Count > 0)
			{
				Action item = ResultQueueTransferThread.Dequeue();
				ResultQueueMainThread.Enqueue(item);
			}
			while (ResultQueueMainThread.Count > 0)
			{
				Action action = ResultQueueMainThread.Dequeue();
				action();
			}
		}

		private static string ResponseToString(WebResponse webResponse)
		{
			if (webResponse == null)
			{
				return null;
			}
			try
			{
				using (Stream stream = webResponse.GetResponseStream())
				{
					if (stream == null)
					{
						return null;
					}
					using (StreamReader streamReader = new StreamReader(stream))
					{
						return streamReader.ReadToEnd();
					}
				}
			}
			catch (WebException ex)
			{
				try
				{
					using (Stream stream2 = ex.Response.GetResponseStream())
					{
						if (stream2 == null)
						{
							return null;
						}
						using (StreamReader streamReader2 = new StreamReader(stream2))
						{
							return streamReader2.ReadToEnd();
						}
					}
				}
				catch (Exception exception)
				{
					Debug.LogException(exception);
					return null;
				}
			}
			catch (Exception exception2)
			{
				Debug.LogException(exception2);
				return null;
			}
		}

		public int GetPendingMessages()
		{
			int num = 0;
			lock (ActiveRequests)
			{
				num += ActiveRequests.Count + _activeCallCount;
			}
			lock (ResultQueueTransferThread)
			{
				return num + ResultQueueTransferThread.Count;
			}
		}
	}
}
