using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using SRDebugger.Services;
using SRF;
using UnityEngine;

namespace SRDebugger.Internal
{
	public class BugReportApi
	{
		private readonly string _apiKey;

		private readonly BugReport _bugReport;

		private bool _isBusy;

		private WWW _www;

		public bool IsComplete { get; private set; }

		public bool WasSuccessful { get; private set; }

		public string ErrorMessage { get; private set; }

		public float Progress
		{
			get
			{
				if (_www == null)
				{
					return 0f;
				}
				return Mathf.Clamp01(_www.progress + _www.uploadProgress);
			}
		}

		public BugReportApi(BugReport report, string apiKey)
		{
			_bugReport = report;
			_apiKey = apiKey;
		}

		public IEnumerator Submit()
		{
			if (_isBusy)
			{
				throw new InvalidOperationException("BugReportApi is already sending a bug report");
			}
			_isBusy = true;
			ErrorMessage = string.Empty;
			IsComplete = false;
			WasSuccessful = false;
			_www = null;
			try
			{
				string json = BuildJsonRequest(_bugReport);
				byte[] jsonBytes = Encoding.UTF8.GetBytes(json);
				Dictionary<string, string> headers = new Dictionary<string, string>();
				headers["Content-Type"] = "application/json";
				headers["Accept"] = "application/json";
				headers["Method"] = "POST";
				headers["X-ApiKey"] = _apiKey;
				_www = new WWW("http://srdebugger.stompyrobot.uk/report/submit", jsonBytes, headers);
			}
			catch (Exception ex)
			{
				ErrorMessage = ex.Message;
			}
			if (_www == null)
			{
				SetCompletionState(false);
				yield break;
			}
			yield return _www;
			if (!string.IsNullOrEmpty(_www.error))
			{
				ErrorMessage = _www.error;
				SetCompletionState(false);
				yield break;
			}
			if (!_www.responseHeaders.ContainsKey("X-STATUS"))
			{
				ErrorMessage = "Completion State Unknown";
				SetCompletionState(false);
				yield break;
			}
			string status = _www.responseHeaders["X-STATUS"];
			if (!status.Contains("200"))
			{
				ErrorMessage = SRDebugApiUtil.ParseErrorResponse(_www.text, status);
				SetCompletionState(false);
			}
			else
			{
				SetCompletionState(true);
			}
		}

		private void SetCompletionState(bool wasSuccessful)
		{
			_bugReport.ScreenshotData = null;
			WasSuccessful = wasSuccessful;
			IsComplete = true;
			_isBusy = false;
			if (!wasSuccessful)
			{
				Debug.LogError("Bug Reporter Error: " + ErrorMessage);
			}
		}

		private static string BuildJsonRequest(BugReport report)
		{
			Hashtable hashtable = new Hashtable();
			hashtable.Add("userEmail", report.Email);
			hashtable.Add("userDescription", report.UserDescription);
			hashtable.Add("console", CreateConsoleDump());
			hashtable.Add("systemInformation", report.SystemInformation);
			if (report.ScreenshotData != null)
			{
				hashtable.Add("screenshot", Convert.ToBase64String(report.ScreenshotData));
			}
			return Json.Serialize(hashtable);
		}

		private static IList<IList<string>> CreateConsoleDump()
		{
			List<IList<string>> list = new List<IList<string>>();
			IReadOnlyList<ConsoleEntry> entries = Service.Console.Entries;
			foreach (ConsoleEntry item in entries)
			{
				List<string> list2 = new List<string>();
				list2.Add(item.LogType.ToString());
				list2.Add(item.Message);
				list2.Add(item.StackTrace);
				if (item.Count > 1)
				{
					list2.Add(item.Count.ToString());
				}
				list.Add(list2);
			}
			return list;
		}
	}
}
