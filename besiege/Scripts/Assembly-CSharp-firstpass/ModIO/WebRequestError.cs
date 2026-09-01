using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace ModIO
{
	public class WebRequestError
	{
		[Serializable]
		private class APIWrapper
		{
			[Serializable]
			public class APIError
			{
				[JsonProperty("error_ref")]
				public int errorReference = -1;

				[JsonProperty("message")]
				public string message;

				[JsonProperty("errors")]
				public Dictionary<string, string> errors;
			}

			public APIError error;
		}

		public const int MODIOERROR_USERNOTAGREED = 11051;

		public UnityWebRequest webRequest;

		public int timeStamp;

		public int errorReference;

		public string errorMessage;

		public IDictionary<string, string> fieldValidationMessages;

		public bool isAuthenticationInvalid;

		public bool isUserTermsAgreementRequired;

		public bool isServerUnreachable;

		public bool isRequestUnresolvable;

		public int limitedUntilTimeStamp;

		public string displayMessage;

		[Obsolete("Use webRequest.responseCode instead")]
		public int responseCode
		{
			get
			{
				return (int)((webRequest == null) ? (-1) : webRequest.responseCode);
			}
		}

		[Obsolete("Use webRequest.method instead")]
		public string method
		{
			get
			{
				return (webRequest == null) ? "LOCAL" : webRequest.method;
			}
		}

		[Obsolete("Use webRequest.url instead")]
		public string url
		{
			get
			{
				return (webRequest == null) ? string.Empty : webRequest.url;
			}
		}

		[Obsolete("Use webRequest.GetResponseHeaders() instead")]
		public Dictionary<string, string> responseHeaders
		{
			get
			{
				return (webRequest == null) ? null : webRequest.GetResponseHeaders();
			}
		}

		[Obsolete("Use webRequest.downloadHandler.text instead")]
		public string responseBody
		{
			get
			{
				if (webRequest != null && webRequest.downloadHandler != null && !(webRequest.downloadHandler is FileDownloadHandler))
				{
					return webRequest.downloadHandler.text;
				}
				return string.Empty;
			}
		}

		[Obsolete("Use WebRequestError.errorMessage instead")]
		public string message
		{
			get
			{
				return errorMessage;
			}
			set
			{
				errorMessage = value;
			}
		}

		public static WebRequestError GenerateFromWebRequest(UnityWebRequest webRequest)
		{
			if (webRequest == null)
			{
				Debug.LogWarning("[mod.io] WebRequestError.GenerateFromWebRequest(webRequest) parameter was null.");
				return GenerateLocal("An unknown error occurred.");
			}
			WebRequestError webRequestError = new WebRequestError();
			webRequestError.webRequest = webRequest;
			webRequestError.timeStamp = ParseDateHeaderAsTimeStamp(webRequest);
			webRequestError.ApplyAPIErrorValues();
			webRequestError.ApplyInterpretedValues();
			return webRequestError;
		}

		public static WebRequestError GenerateLocal(string errorMessage)
		{
			WebRequestError webRequestError = new WebRequestError();
			webRequestError.webRequest = null;
			webRequestError.timeStamp = ServerTimeStamp.Now;
			webRequestError.errorReference = 0;
			webRequestError.errorMessage = errorMessage;
			webRequestError.displayMessage = errorMessage;
			webRequestError.isAuthenticationInvalid = false;
			webRequestError.isUserTermsAgreementRequired = false;
			webRequestError.isServerUnreachable = false;
			webRequestError.isRequestUnresolvable = false;
			webRequestError.limitedUntilTimeStamp = -1;
			return webRequestError;
		}

		private static int ParseDateHeaderAsTimeStamp(UnityWebRequest webRequest)
		{
			string responseHeader = webRequest.GetResponseHeader("Date");
			string format = "ddd, dd MMM yyyy HH:mm:ss 'GMT'";
			DateTime result;
			if (!string.IsNullOrEmpty(responseHeader) && DateTime.TryParseExact(responseHeader, format, CultureInfo.InvariantCulture.DateTimeFormat, DateTimeStyles.AssumeUniversal, out result))
			{
				result = DateTime.SpecifyKind(result, DateTimeKind.Utc);
				return ServerTimeStamp.FromUTCDateTime(result);
			}
			return ServerTimeStamp.Now;
		}

		private void ApplyAPIErrorValues()
		{
			errorMessage = null;
			fieldValidationMessages = null;
			if (webRequest == null)
			{
				errorMessage = "An unknown error occurred. Please try again later.";
				return;
			}
			if (webRequest.downloadHandler != null && !(webRequest.downloadHandler is FileDownloadHandler))
			{
				string text = null;
				try
				{
					text = webRequest.downloadHandler.text;
				}
				catch (Exception ex)
				{
					Debug.LogWarning("[mod.io] Error reading webRequest.downloadHandler text body:\n" + ex.Message);
				}
				if (!string.IsNullOrEmpty(text))
				{
					APIWrapper aPIWrapper = null;
					if (text.StartsWith("<!DOCTYPE html>"))
					{
						int num = text.IndexOf("what-happened-section");
						int num2 = -1;
						if (num > 0)
						{
							num = text.IndexOf("<p>", num);
							if (num > 0)
							{
								num += 3;
								num2 = text.IndexOf("</p>", num);
							}
						}
						if (num2 > 0)
						{
							errorMessage = "A Cloudflare error has occurred: " + text.Substring(num, num2 - num);
						}
					}
					else
					{
						try
						{
							aPIWrapper = JsonConvert.DeserializeObject<APIWrapper>(text);
						}
						catch (Exception ex2)
						{
							Debug.LogWarning("[mod.io] Error parsing error object from response:\n" + ex2.Message);
						}
						if (aPIWrapper != null && aPIWrapper.error != null)
						{
							errorReference = aPIWrapper.error.errorReference;
							errorMessage = aPIWrapper.error.message;
							fieldValidationMessages = aPIWrapper.error.errors;
						}
					}
				}
			}
			if (errorMessage == null)
			{
				errorMessage = webRequest.error;
			}
		}

		private void ApplyInterpretedValues()
		{
			isAuthenticationInvalid = false;
			isUserTermsAgreementRequired = false;
			isServerUnreachable = false;
			isRequestUnresolvable = false;
			limitedUntilTimeStamp = -1;
			displayMessage = string.Empty;
			if (webRequest == null)
			{
				return;
			}
			long num = webRequest.responseCode;
			if (num >= 400 && num <= 415)
			{
				switch ((int)(num - 400))
				{
				case 0:
				case 5:
				case 6:
				case 15:
					goto IL_0108;
				case 1:
					goto IL_0149;
				case 3:
					goto IL_0177;
				case 4:
				case 10:
					goto IL_01c5;
				case 8:
					goto IL_01ec;
				}
			}
			if (num >= 500 && num <= 503)
			{
				switch ((int)(num - 500))
				{
				case 0:
					goto IL_03a5;
				case 3:
					goto IL_03cc;
				}
			}
			switch (num)
			{
			case 422L:
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendLine("The submitted data contained error(s).");
				if (fieldValidationMessages != null && fieldValidationMessages.Count > 0)
				{
					foreach (KeyValuePair<string, string> fieldValidationMessage in fieldValidationMessages)
					{
						stringBuilder.AppendLine("- [" + fieldValidationMessage.Key + "] " + fieldValidationMessage.Value);
					}
				}
				if (stringBuilder.Length > 0 && stringBuilder[stringBuilder.Length - 1] == '\n')
				{
					stringBuilder.Length--;
				}
				displayMessage = stringBuilder.ToString();
				isRequestUnresolvable = true;
				break;
			}
			case 429L:
			{
				Dictionary<string, string> dictionary = webRequest.GetResponseHeaders();
				string value;
				int result;
				if (!dictionary.TryGetValue("X-Ratelimit-RetryAfter", out value) || !int.TryParse(value, out result))
				{
					result = 60;
					Debug.LogWarning("[mod.io] Too many APIRequests have been made, however no valid X-Ratelimit-RetryAfter header was detected.\nPlease report this to jackson@mod.io with the following information:\n[" + webRequest.url + ":" + webRequest.method + "-" + errorMessage + "]");
				}
				if (string.IsNullOrEmpty(errorMessage))
				{
					displayMessage = "Too many requests have been made to the mod.io servers.\nReconnecting in " + result + " seconds.";
				}
				limitedUntilTimeStamp = timeStamp + result;
				break;
			}
			default:
				if (webRequest.responseCode <= 0)
				{
					displayMessage = "The mod.io servers cannot be reached.\nPlease check your internet connection.";
					isServerUnreachable = true;
				}
				else
				{
					displayMessage = "Error synchronizing with the mod.io servers. [Error Code: " + webRequest.responseCode + "]";
					isRequestUnresolvable = true;
				}
				break;
			}
			goto IL_044d;
			IL_03cc:
			if (string.IsNullOrEmpty(errorMessage))
			{
				displayMessage = "The mod.io servers are currently offline.";
			}
			isServerUnreachable = true;
			goto IL_044d;
			IL_01c5:
			if (string.IsNullOrEmpty(errorMessage))
			{
				displayMessage = "A networking error occurred.";
			}
			isRequestUnresolvable = true;
			goto IL_044d;
			IL_01ec:
			if (string.IsNullOrEmpty(errorMessage))
			{
				displayMessage = "The mod.io servers could not be reached.\nPlease check your internet connection.";
			}
			isServerUnreachable = true;
			goto IL_044d;
			IL_0149:
			if (string.IsNullOrEmpty(errorMessage))
			{
				displayMessage = "Your mod.io authentication details have changed.\nTry logging in again.";
			}
			isAuthenticationInvalid = true;
			isRequestUnresolvable = false;
			goto IL_044d;
			IL_044d:
			if (string.IsNullOrEmpty(displayMessage))
			{
				displayMessage = errorMessage;
			}
			return;
			IL_0108:
			if (string.IsNullOrEmpty(errorMessage))
			{
				displayMessage = "Error synchronizing with the mod.io servers. [Error Code: " + webRequest.responseCode + "]";
			}
			isRequestUnresolvable = true;
			goto IL_044d;
			IL_0177:
			if (errorReference == 11051)
			{
				isUserTermsAgreementRequired = true;
				displayMessage = "You have not yet agreed to the mod.io terms of service.";
			}
			else if (string.IsNullOrEmpty(errorMessage))
			{
				displayMessage = "Your account does not have the required permissions.";
			}
			isRequestUnresolvable = true;
			goto IL_044d;
			IL_03a5:
			if (string.IsNullOrEmpty(errorMessage))
			{
				displayMessage = "There was an error with the mod.io servers. Staff have been notified, and will attempt to fix the issue as soon as possible.";
			}
			isRequestUnresolvable = true;
			goto IL_044d;
		}

		[Obsolete("Set PluginSettings.requestLogging.errorsAsWarnings instead.")]
		public static void LogAsWarning(WebRequestError error)
		{
			Debug.LogWarning("[mod.io] Web Request Failed\n" + error.ToUnityDebugString());
		}

		[Obsolete("Use DebugUtilities.GetResponseInfo() instead.")]
		public string ToUnityDebugString()
		{
			if (webRequest == null)
			{
				return "Request failed prior to being sent.\n" + errorMessage;
			}
			return DebugUtilities.GetResponseInfo(webRequest);
		}
	}
}
