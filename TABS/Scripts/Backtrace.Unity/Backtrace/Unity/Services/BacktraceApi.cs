using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Backtrace.Unity.Common;
using Backtrace.Unity.Interfaces;
using Backtrace.Unity.Model;
using Backtrace.Unity.Types;
using UnityEngine;
using UnityEngine.Networking;

namespace Backtrace.Unity.Services
{
	internal class BacktraceApi : IBacktraceApi
	{
		private bool _shouldDisplayFailureMessage = true;

		private readonly Uri _serverUrl;

		private readonly string _minidumpUrl;

		private readonly BacktraceCredentials _credentials;

		private readonly bool _ignoreSslValidation;

		[Obsolete("RequestHandler is obsolete. BacktraceApi won't be able to provide BacktraceData in every situation")]
		public Func<string, BacktraceData, BacktraceResult> RequestHandler { get; set; }

		public Action<Exception> OnServerError { get; set; }

		public Action<BacktraceResult> OnServerResponse { get; set; }

		public bool EnablePerformanceStatistics { get; set; }

		public string ServerUrl => _serverUrl.ToString();

		public BacktraceApi(BacktraceCredentials credentials, bool ignoreSslValidation = false)
		{
			_credentials = credentials;
			if (_credentials == null)
			{
				throw new ArgumentException(string.Format("{0} cannot be null", "BacktraceCredentials"));
			}
			_ignoreSslValidation = ignoreSslValidation;
			_serverUrl = credentials.GetSubmissionUrl();
			_minidumpUrl = credentials.GetMinidumpSubmissionUrl().ToString();
		}

		public IEnumerator SendMinidump(string minidumpPath, IEnumerable<string> attachments, Action<BacktraceResult> callback = null)
		{
			if (attachments == null)
			{
				attachments = new List<string>();
			}
			Stopwatch stopWatch = (EnablePerformanceStatistics ? Stopwatch.StartNew() : new Stopwatch());
			byte[] array = File.ReadAllBytes(minidumpPath);
			if (array == null || array.Length == 0)
			{
				yield break;
			}
			List<IMultipartFormSection> formData = new List<IMultipartFormSection>
			{
				new MultipartFormFileSection("upload_file", array)
			};
			foreach (string attachment in attachments)
			{
				if (File.Exists(attachment) && new FileInfo(attachment).Length < 10000000)
				{
					formData.Add(new MultipartFormFileSection($"attachment__{Path.GetFileName(attachment)}", File.ReadAllBytes(attachment)));
				}
			}
			yield return new WaitForEndOfFrame();
			byte[] array2 = UnityWebRequest.GenerateBoundary();
			using (UnityWebRequest request = UnityWebRequest.Post(_minidumpUrl, formData, array2))
			{
				if (_ignoreSslValidation)
				{
					request.certificateHandler = new BacktraceSelfSSLCertificateHandler();
				}
				request.SetRequestHeader("Content-Type", $"multipart/form-data; boundary={Encoding.UTF8.GetString(array2)}");
				request.timeout = 15000;
				yield return request.SendWebRequest();
				BacktraceResult backtraceResult = ((request.isNetworkError || request.isHttpError) ? new BacktraceResult
				{
					Message = request.error,
					Status = BacktraceResultStatus.ServerError
				} : BacktraceResult.FromJson(request.downloadHandler.text));
				callback?.Invoke(backtraceResult);
				if (EnablePerformanceStatistics)
				{
					stopWatch.Stop();
					UnityEngine.Debug.Log($"Backtrace - minidump send time: {stopWatch.GetMicroseconds()}μs");
				}
				yield return backtraceResult;
			}
		}

		public IEnumerator Send(BacktraceData data, Action<BacktraceResult> callback = null)
		{
			if (RequestHandler != null)
			{
				yield return RequestHandler(ServerUrl, data);
			}
			else if (data != null)
			{
				string json = data.ToJson();
				yield return Send(json, data.Attachments, data.Deduplication, callback);
			}
		}

		public IEnumerator Send(string json, List<string> attachments, int deduplication, Action<BacktraceResult> callback)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			if (deduplication > 0)
			{
				dictionary["_mod_duplicate"] = deduplication.ToString();
			}
			yield return Send(json, attachments, dictionary, callback);
		}

		public IEnumerator Send(string json, List<string> attachments, Dictionary<string, string> queryAttributes, Action<BacktraceResult> callback)
		{
			Stopwatch stopWatch = (EnablePerformanceStatistics ? Stopwatch.StartNew() : new Stopwatch());
			string uri = ((queryAttributes != null) ? GetParametrizedQuery(_serverUrl.ToString(), queryAttributes) : ServerUrl);
			List<IMultipartFormSection> list = new List<IMultipartFormSection>
			{
				new MultipartFormFileSection("upload_file", Encoding.UTF8.GetBytes(json), "upload_file.json", "application/json")
			};
			foreach (string attachment in attachments)
			{
				if (File.Exists(attachment) && new FileInfo(attachment).Length < 10000000)
				{
					list.Add(new MultipartFormFileSection($"attachment__{Path.GetFileName(attachment)}", File.ReadAllBytes(attachment)));
				}
			}
			byte[] array = UnityWebRequest.GenerateBoundary();
			using (UnityWebRequest request = UnityWebRequest.Post(uri, list, array))
			{
				if (_ignoreSslValidation)
				{
					request.certificateHandler = new BacktraceSelfSSLCertificateHandler();
				}
				request.SetRequestHeader("Content-Type", "multipart/form-data; boundary=" + Encoding.UTF8.GetString(array));
				request.timeout = 15000;
				yield return request.SendWebRequest();
				BacktraceResult backtraceResult;
				if (request.responseCode == 429)
				{
					backtraceResult = new BacktraceResult
					{
						Message = "Server report limit reached",
						Status = BacktraceResultStatus.LimitReached
					};
					if (OnServerResponse != null)
					{
						OnServerResponse(backtraceResult);
					}
				}
				else if (request.responseCode == 200 && (!request.isNetworkError || !request.isHttpError))
				{
					backtraceResult = BacktraceResult.FromJson(request.downloadHandler.text);
					_shouldDisplayFailureMessage = true;
					if (OnServerResponse != null)
					{
						OnServerResponse(backtraceResult);
					}
				}
				else
				{
					PrintLog(request);
					Exception ex = new Exception(request.error);
					backtraceResult = BacktraceResult.OnNetworkError(ex);
					if (OnServerError != null)
					{
						OnServerError(ex);
					}
				}
				callback?.Invoke(backtraceResult);
				if (EnablePerformanceStatistics)
				{
					stopWatch.Stop();
					UnityEngine.Debug.Log($"Backtrace - JSON send time: {stopWatch.GetMicroseconds()}μs");
				}
				yield return backtraceResult;
			}
		}

		private void PrintLog(UnityWebRequest request)
		{
			if (_shouldDisplayFailureMessage)
			{
				_shouldDisplayFailureMessage = false;
				UnityEngine.Debug.LogWarning(string.Format("{0}{1}", $"[Backtrace]::Reponse code: {request.responseCode}, Response text: {request.error}", "\n Please check provided url to Backtrace service or learn more from our integration guide: https://support.backtrace.io/hc/en-us/articles/360040515991-Unity-Integration-Guide"));
			}
		}

		private string GetParametrizedQuery(string serverUrl, Dictionary<string, string> queryAttributes)
		{
			UriBuilder uriBuilder = new UriBuilder(serverUrl);
			if (queryAttributes == null || !queryAttributes.Any())
			{
				return uriBuilder.Uri.ToString();
			}
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			if (string.IsNullOrEmpty(uriBuilder.Query))
			{
				flag = false;
				stringBuilder.Append("?");
			}
			for (int i = 0; i < queryAttributes.Count; i++)
			{
				if (i != 0 || flag)
				{
					stringBuilder.Append("&");
				}
				KeyValuePair<string, string> keyValuePair = queryAttributes.ElementAt(i);
				stringBuilder.AppendFormat("{0}={1}", keyValuePair.Key, keyValuePair.Value);
			}
			uriBuilder.Query += stringBuilder.ToString();
			return Uri.EscapeUriString(uriBuilder.Uri.ToString());
		}
	}
}
