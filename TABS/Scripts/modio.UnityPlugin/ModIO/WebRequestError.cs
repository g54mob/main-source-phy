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
				if (webRequest == null)
				{
					return -1;
				}
				return (int)webRequest.responseCode;
			}
		}

		[Obsolete("Use webRequest.method instead")]
		public string method
		{
			get
			{
				if (webRequest == null)
				{
					return "LOCAL";
				}
				return webRequest.method;
			}
		}

		[Obsolete("Use webRequest.url instead")]
		public string url
		{
			get
			{
				if (webRequest == null)
				{
					return string.Empty;
				}
				return webRequest.url;
			}
		}

		[Obsolete("Use webRequest.GetResponseHeaders() instead")]
		public Dictionary<string, string> responseHeaders
		{
			get
			{
				if (webRequest == null)
				{
					return null;
				}
				return webRequest.GetResponseHeaders();
			}
		}

		[Obsolete("Use webRequest.downloadHandler.text instead")]
		public string responseBody
		{
			get
			{
				if (webRequest != null && webRequest.downloadHandler != null && !(webRequest.downloadHandler is DownloadHandlerFile))
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
			WebRequestError webRequestError = new WebRequestError();
			webRequestError.webRequest = webRequest;
			webRequestError.timeStamp = ParseDateHeaderAsTimeStamp(webRequest);
			webRequestError.ApplyAPIErrorValues();
			webRequestError.ApplyInterpretedValues();
			return FilterDisplayMessage(webRequestError);
		}

		public static WebRequestError GenerateLocal(string errorMessage)
		{
			return FilterDisplayMessage(new WebRequestError
			{
				webRequest = null,
				timeStamp = ServerTimeStamp.Now,
				errorReference = 0,
				errorMessage = errorMessage,
				displayMessage = errorMessage,
				isAuthenticationInvalid = false,
				isUserTermsAgreementRequired = false,
				isServerUnreachable = false,
				isRequestUnresolvable = false,
				limitedUntilTimeStamp = -1
			});
		}

		private static WebRequestError FilterDisplayMessage(WebRequestError e)
		{
			string[] array = new string[8] { "playstation", "sony", "xbox", "microsoft", "switch", "nintendo", "steam", "epic" };
			if (string.IsNullOrEmpty(e.displayMessage))
			{
				return e;
			}
			string text = e.displayMessage.ToLower();
			for (int i = 0; i < array.Length; i++)
			{
				if (text.Contains(array[i]))
				{
					e.displayMessage = e.errorReference.ToString();
					break;
				}
			}
			return e;
		}

		private static int ParseDateHeaderAsTimeStamp(UnityWebRequest webRequest)
		{
			string responseHeader = webRequest.GetResponseHeader("Date");
			string format = "ddd, dd MMM yyyy HH:mm:ss 'GMT'";
			if (!string.IsNullOrEmpty(responseHeader) && DateTime.TryParseExact(responseHeader, format, CultureInfo.InvariantCulture.DateTimeFormat, DateTimeStyles.AssumeUniversal, out var result))
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
			if (webRequest.downloadHandler != null && !(webRequest.downloadHandler is DownloadHandlerFile))
			{
				try
				{
					string text = webRequest.downloadHandler.text;
					if (string.IsNullOrEmpty(text))
					{
						return;
					}
					APIWrapper aPIWrapper = JsonConvert.DeserializeObject<APIWrapper>(text, new JsonSerializerSettings
					{
						Error = IOUtilities.ReThrowNewtonsoftJsonException
					});
					if (aPIWrapper == null || aPIWrapper.error == null)
					{
						return;
					}
					errorReference = aPIWrapper.error.errorReference;
					errorMessage = aPIWrapper.error.message;
					fieldValidationMessages = aPIWrapper.error.errors;
				}
				catch (Exception ex)
				{
					Debug.LogWarning("[mod.io] Error deserializing API Error:\n" + ex.Message);
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
			if (num <= 422)
			{
				long num2 = num - 400;
				if ((ulong)num2 <= 15uL)
				{
					switch ((int)num2)
					{
					case 0:
					case 5:
					case 6:
					case 15:
						goto IL_00dd;
					case 1:
						goto IL_011b;
					case 3:
						goto IL_0146;
					case 4:
					case 10:
						goto IL_018b;
					case 8:
						goto IL_01af;
					case 2:
					case 7:
					case 9:
					case 11:
					case 12:
					case 13:
					case 14:
						goto IL_0389;
					}
				}
				if (num != 422)
				{
					goto IL_0389;
				}
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
					int length = stringBuilder.Length - 1;
					stringBuilder.Length = length;
				}
				displayMessage = stringBuilder.ToString();
				isRequestUnresolvable = true;
			}
			else if (num != 429)
			{
				if (num != 500)
				{
					if (num != 503)
					{
						goto IL_0389;
					}
					if (string.IsNullOrEmpty(errorMessage))
					{
						displayMessage = "The mod.io servers are currently offline.";
					}
					isServerUnreachable = true;
				}
				else
				{
					if (string.IsNullOrEmpty(errorMessage))
					{
						displayMessage = "There was an error with the mod.io servers. Staff have been notified, and will attempt to fix the issue as soon as possible.";
					}
					isRequestUnresolvable = true;
				}
			}
			else
			{
				if (!webRequest.GetResponseHeaders().TryGetValue("X-Ratelimit-RetryAfter", out var value) || !int.TryParse(value, out var result))
				{
					result = 60;
					Debug.LogWarning("[mod.io] Too many APIRequests have been made, however no valid X-Ratelimit-RetryAfter header was detected.\nPlease report this to jackson@mod.io with the following information:\n[" + webRequest.url + ":" + webRequest.method + "-" + errorMessage + "]");
				}
				if (string.IsNullOrEmpty(errorMessage))
				{
					displayMessage = "Too many requests have been made to the mod.io servers.\nReconnecting in " + result + " seconds.";
				}
				limitedUntilTimeStamp = timeStamp + result;
			}
			goto IL_03e2;
			IL_0389:
			if (webRequest.responseCode <= 0)
			{
				displayMessage = "The mod.io servers cannot be reached.\nPlease check your internet connection.";
				isServerUnreachable = true;
			}
			else
			{
				displayMessage = "Error synchronizing with the mod.io servers. [Error Code: " + webRequest.responseCode + "]";
				isRequestUnresolvable = true;
				Debug.LogWarning("[mod.io] An unhandled error was returned during a web request.\nPlease report this to jackson@mod.io with the following information");
			}
			goto IL_03e2;
			IL_011b:
			if (string.IsNullOrEmpty(errorMessage))
			{
				displayMessage = "Your mod.io authentication details have changed.\nTry logging in again.";
			}
			isAuthenticationInvalid = true;
			isRequestUnresolvable = true;
			goto IL_03e2;
			IL_00dd:
			if (string.IsNullOrEmpty(errorMessage))
			{
				displayMessage = "Error synchronizing with the mod.io servers. [Error Code: " + webRequest.responseCode + "]";
			}
			isRequestUnresolvable = true;
			goto IL_03e2;
			IL_0146:
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
			goto IL_03e2;
			IL_01af:
			if (string.IsNullOrEmpty(errorMessage))
			{
				displayMessage = "The mod.io servers could not be reached.\nPlease check your internet connection.";
			}
			isServerUnreachable = true;
			goto IL_03e2;
			IL_018b:
			if (string.IsNullOrEmpty(errorMessage))
			{
				displayMessage = "A networking error occurred.";
			}
			isRequestUnresolvable = true;
			goto IL_03e2;
			IL_03e2:
			if (string.IsNullOrEmpty(displayMessage))
			{
				displayMessage = errorMessage;
			}
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
