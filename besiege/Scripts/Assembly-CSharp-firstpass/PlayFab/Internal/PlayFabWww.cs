using System;
using System.Collections;
using System.IO;
using System.Text;
using Ionic.Zlib;
using PlayFab.SharedModels;
using UnityEngine;
using UnityEngine.Networking;

namespace PlayFab.Internal
{
	public class PlayFabWww : IPlayFabPlugin, ITransportPlugin
	{
		private bool _isInitialized;

		private int _pendingWwwMessages;

		public bool IsInitialized
		{
			get
			{
				return _isInitialized;
			}
		}

		public void Initialize()
		{
			_isInitialized = true;
		}

		public void Update()
		{
		}

		public void OnDestroy()
		{
		}

		public void SimpleGetCall(string fullUrl, Action<byte[]> successCallback, Action<string> errorCallback)
		{
			SingletonMonoBehaviour<PlayFabHttp>.instance.StartCoroutine(SimpleCallCoroutine("get", fullUrl, null, successCallback, errorCallback));
		}

		public void SimplePutCall(string fullUrl, byte[] payload, Action<byte[]> successCallback, Action<string> errorCallback)
		{
			SingletonMonoBehaviour<PlayFabHttp>.instance.StartCoroutine(SimpleCallCoroutine("put", fullUrl, payload, successCallback, errorCallback));
		}

		public void SimplePostCall(string fullUrl, byte[] payload, Action<byte[]> successCallback, Action<string> errorCallback)
		{
			SingletonMonoBehaviour<PlayFabHttp>.instance.StartCoroutine(SimpleCallCoroutine("post", fullUrl, payload, successCallback, errorCallback));
		}

		private static IEnumerator SimpleCallCoroutine(string method, string fullUrl, byte[] payload, Action<byte[]> successCallback, Action<string> errorCallback)
		{
			if (payload == null)
			{
				WWW www = new WWW(fullUrl);
				yield return www;
				if (!string.IsNullOrEmpty(www.error))
				{
					errorCallback(www.error);
				}
				else
				{
					successCallback(www.bytes);
				}
				yield break;
			}
			UnityWebRequest request;
			if (method == "put")
			{
				request = UnityWebRequest.Put(fullUrl, payload);
			}
			else
			{
				string strPayload = Encoding.UTF8.GetString(payload, 0, payload.Length);
				request = UnityWebRequest.Post(fullUrl, strPayload);
			}
			request.Send();
			while (request.uploadProgress < 1f || request.downloadProgress < 1f)
			{
				yield return 1;
			}
			if (!string.IsNullOrEmpty(request.error))
			{
				errorCallback(request.error);
			}
			else
			{
				successCallback(request.downloadHandler.data);
			}
		}

		public void MakeApiCall(object reqContainerObj)
		{
			CallRequestContainer reqContainer = (CallRequestContainer)reqContainerObj;
			reqContainer.RequestHeaders["Content-Type"] = "application/json";
			if (PlayFabSettings.CompressApiData)
			{
				reqContainer.RequestHeaders["Content-Encoding"] = "GZIP";
				reqContainer.RequestHeaders["Accept-Encoding"] = "GZIP";
				using (MemoryStream memoryStream = new MemoryStream())
				{
					using (GZipStream gZipStream = new GZipStream(memoryStream, CompressionMode.Compress, CompressionLevel.BestCompression))
					{
						gZipStream.Write(reqContainer.Payload, 0, reqContainer.Payload.Length);
					}
					reqContainer.Payload = memoryStream.ToArray();
				}
			}
			WWW www = new WWW(reqContainer.FullUrl, reqContainer.Payload, reqContainer.RequestHeaders);
			Action<string> wwwSuccessCallback = delegate(string response)
			{
				try
				{
					ISerializerPlugin plugin = PluginManager.GetPlugin<ISerializerPlugin>(PluginContract.PlayFab_Serializer, string.Empty);
					HttpResponseObject httpResponseObject = plugin.DeserializeObject<HttpResponseObject>(response);
					if (httpResponseObject.code == 200)
					{
						reqContainer.JsonResponse = plugin.SerializeObject(httpResponseObject.data);
						reqContainer.DeserializeResultJson();
						reqContainer.ApiResult.Request = reqContainer.ApiRequest;
						reqContainer.ApiResult.CustomData = reqContainer.CustomData;
						SingletonMonoBehaviour<PlayFabHttp>.instance.OnPlayFabApiResult(reqContainer);
						PlayFabDeviceUtil.OnPlayFabLogin(reqContainer.ApiResult, reqContainer.settings, reqContainer.instanceApi);
						try
						{
							PlayFabHttp.SendEvent(reqContainer.ApiEndpoint, reqContainer.ApiRequest, reqContainer.ApiResult, ApiProcessingEventType.Post);
						}
						catch (Exception exception)
						{
							Debug.LogException(exception);
						}
						try
						{
							reqContainer.InvokeSuccessCallback();
							return;
						}
						catch (Exception exception2)
						{
							Debug.LogException(exception2);
							return;
						}
					}
					if (reqContainer.ErrorCallback != null)
					{
						reqContainer.Error = PlayFabHttp.GeneratePlayFabError(reqContainer.ApiEndpoint, response, reqContainer.CustomData);
						PlayFabHttp.SendErrorEvent(reqContainer.ApiRequest, reqContainer.Error);
						reqContainer.ErrorCallback(reqContainer.Error);
					}
				}
				catch (Exception exception3)
				{
					Debug.LogException(exception3);
				}
			};
			Action<string> wwwErrorCallback = delegate(string errorCb)
			{
				reqContainer.JsonResponse = errorCb;
				if (reqContainer.ErrorCallback != null)
				{
					reqContainer.Error = PlayFabHttp.GeneratePlayFabError(reqContainer.ApiEndpoint, reqContainer.JsonResponse, reqContainer.CustomData);
					PlayFabHttp.SendErrorEvent(reqContainer.ApiRequest, reqContainer.Error);
					reqContainer.ErrorCallback(reqContainer.Error);
				}
			};
			SingletonMonoBehaviour<PlayFabHttp>.instance.StartCoroutine(PostPlayFabApiCall(www, wwwSuccessCallback, wwwErrorCallback));
		}

		private IEnumerator PostPlayFabApiCall(WWW www, Action<string> wwwSuccessCallback, Action<string> wwwErrorCallback)
		{
			yield return www;
			if (!string.IsNullOrEmpty(www.error))
			{
				wwwErrorCallback(www.error);
			}
			else
			{
				try
				{
					byte[] responseBytes = www.bytes;
					bool isGzipCompressed = responseBytes != null && responseBytes[0] == 31 && responseBytes[1] == 139;
					string responseText = "Unexpected error: cannot decompress GZIP stream.";
					if (!isGzipCompressed && responseBytes != null)
					{
						responseText = Encoding.UTF8.GetString(responseBytes, 0, responseBytes.Length);
					}
					if (isGzipCompressed)
					{
						MemoryStream stream = new MemoryStream(responseBytes);
						using (GZipStream gZipStream = new GZipStream(stream, CompressionMode.Decompress, false))
						{
							byte[] buffer = new byte[4096];
							using (MemoryStream output = new MemoryStream())
							{
								while (true)
								{
									int num;
									int read = (num = gZipStream.Read(buffer, 0, buffer.Length));
									if (num <= 0)
									{
										break;
									}
									output.Write(buffer, 0, read);
								}
								output.Seek(0L, SeekOrigin.Begin);
								StreamReader streamReader = new StreamReader(output);
								string jsonResponse = streamReader.ReadToEnd();
								wwwSuccessCallback(jsonResponse);
							}
						}
					}
					else
					{
						wwwSuccessCallback(responseText);
					}
				}
				catch (Exception ex)
				{
					Exception e = ex;
					wwwErrorCallback("Unhandled error in PlayFabWWW: " + e);
				}
			}
			www.Dispose();
		}

		public int GetPendingMessages()
		{
			return _pendingWwwMessages;
		}
	}
}
