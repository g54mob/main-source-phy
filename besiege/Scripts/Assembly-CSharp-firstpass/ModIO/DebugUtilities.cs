using System;
using System.Collections.Generic;
using System.Text;
using ModIO.API;
using UnityEngine;
using UnityEngine.Networking;

namespace ModIO
{
	public static class DebugUtilities
	{
		public struct RequestDebugData
		{
			public string userIdString;

			public int timeSent;

			public string downloadLocation;
		}

		public static Dictionary<UnityWebRequest, RequestDebugData> webRequestDebugData = new Dictionary<UnityWebRequest, RequestDebugData>();

		public static void DebugWebRequest(UnityWebRequestAsyncOperation operation, LocalUser userData, int timeSent = -1)
		{
		}

		public static void DebugDownload(UnityWebRequestAsyncOperation operation, LocalUser userData, string downloadLocation, int timeSent = -1)
		{
		}

		public static string GetRequestInfo(UnityWebRequest webRequest, string userIdString)
		{
			if (webRequest == null)
			{
				return "NULL_WEB_REQUEST";
			}
			if (userIdString == null)
			{
				userIdString = "[NOT RECORDED]";
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("URL: ");
			stringBuilder.Append(webRequest.url);
			stringBuilder.Append(" (");
			stringBuilder.Append(webRequest.method.ToUpper());
			stringBuilder.AppendLine(")");
			stringBuilder.Append("User: ");
			stringBuilder.AppendLine(userIdString);
			stringBuilder.AppendLine("Headers:");
			string[] mODIO_REQUEST_HEADER_KEYS = APIClient.MODIO_REQUEST_HEADER_KEYS;
			foreach (string text in mODIO_REQUEST_HEADER_KEYS)
			{
				string requestHeader = webRequest.GetRequestHeader(text);
				if (requestHeader == null)
				{
					continue;
				}
				stringBuilder.Append("  ");
				stringBuilder.Append(text);
				stringBuilder.Append('=');
				if (text.ToUpper() == "AUTHORIZATION")
				{
					if (requestHeader != null && requestHeader.StartsWith("Bearer ") && requestHeader.Length > 8)
					{
						stringBuilder.Append("Bearer [OAUTH_TOKEN]");
					}
					else
					{
						stringBuilder.Append(requestHeader);
					}
				}
				else
				{
					stringBuilder.Append(requestHeader);
				}
				stringBuilder.AppendLine();
			}
			UploadHandler uploadHandler = webRequest.uploadHandler;
			if (uploadHandler != null)
			{
				List<StringValueParameter> stringFields = null;
				List<BinaryDataParameter> binaryFields = null;
				string requestHeader2 = webRequest.GetRequestHeader("content-type");
				if (requestHeader2.ToLower() == "application/x-www-form-urlencoded")
				{
					ParseURLEncodedFormData(uploadHandler.data, out stringFields);
				}
				else if (requestHeader2.Contains("multipart/form-data"))
				{
					ParseMultipartFormData(uploadHandler.data, out stringFields, out binaryFields);
				}
				else
				{
					Debug.Log("[mod.io] Unable to parse upload data for content-type '" + requestHeader2 + "'");
				}
				if (stringFields != null)
				{
					stringBuilder.AppendLine("String Fields:");
					int index = stringBuilder.Length - 1;
					int num = 0;
					foreach (StringValueParameter item in stringFields)
					{
						stringBuilder.Append("  ");
						stringBuilder.Append(item.key);
						stringBuilder.Append('=');
						stringBuilder.Append(item.value);
						stringBuilder.AppendLine();
						num++;
					}
					stringBuilder.Insert(index, " [" + num + "]");
				}
				if (binaryFields != null)
				{
					stringBuilder.AppendLine("Binary Fields:");
					int index2 = stringBuilder.Length - 1;
					int num2 = 0;
					foreach (BinaryDataParameter item2 in binaryFields)
					{
						stringBuilder.Append("  ");
						stringBuilder.Append(item2.key);
						stringBuilder.Append('=');
						stringBuilder.Append(item2.fileName);
						stringBuilder.Append(" (");
						stringBuilder.Append((item2.contents != null) ? ValueFormatting.ByteCount(item2.contents.Length, null) : "NULL_DATA");
						stringBuilder.Append(")");
						stringBuilder.AppendLine();
						num2++;
					}
					stringBuilder.Insert(index2, " [" + num2 + "]");
				}
			}
			return stringBuilder.ToString();
		}

		public static string GetResponseInfo(UnityWebRequest webRequest)
		{
			if (webRequest == null)
			{
				return "NULL_WEB_REQUEST";
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("URL: ");
			stringBuilder.Append(webRequest.url);
			stringBuilder.Append(" (");
			stringBuilder.Append(webRequest.method.ToUpper());
			stringBuilder.AppendLine(")");
			stringBuilder.Append("Response Code: ");
			stringBuilder.AppendLine(webRequest.responseCode.ToString());
			stringBuilder.Append("Response Error: ");
			if (string.IsNullOrEmpty(webRequest.error))
			{
				stringBuilder.AppendLine("NO_ERROR");
			}
			else
			{
				stringBuilder.AppendLine(webRequest.error);
			}
			stringBuilder.AppendLine("Headers:");
			Dictionary<string, string> responseHeaders = webRequest.GetResponseHeaders();
			if (responseHeaders == null || responseHeaders.Count == 0)
			{
				stringBuilder.AppendLine("  NONE");
			}
			else
			{
				foreach (KeyValuePair<string, string> item in responseHeaders)
				{
					stringBuilder.Append("  ");
					stringBuilder.Append(item.Key);
					stringBuilder.Append('=');
					stringBuilder.Append(item.Value);
					stringBuilder.AppendLine();
				}
			}
			if (webRequest.isNetworkError() || webRequest.isHttpError())
			{
				WebRequestError webRequestError = WebRequestError.GenerateFromWebRequest(webRequest);
				stringBuilder.AppendLine("mod.io Error Details:");
				stringBuilder.Append("  flags=");
				if (webRequestError.isAuthenticationInvalid)
				{
					stringBuilder.Append("[AuthenticationInvalid]");
				}
				if (webRequestError.isServerUnreachable)
				{
					stringBuilder.Append("[ServerUnreachable]");
				}
				if (webRequestError.isRequestUnresolvable)
				{
					stringBuilder.Append("[RequestUnresolvable]");
				}
				if (!webRequestError.isAuthenticationInvalid && !webRequestError.isServerUnreachable && !webRequestError.isRequestUnresolvable)
				{
					stringBuilder.Append("[NONE]");
				}
				stringBuilder.AppendLine();
				stringBuilder.Append("  limitedUntilTimeStamp=");
				stringBuilder.AppendLine(webRequestError.limitedUntilTimeStamp.ToString());
				stringBuilder.Append("  errorReference=");
				stringBuilder.AppendLine(webRequestError.errorReference.ToString());
				stringBuilder.Append("  errorMessage=");
				stringBuilder.AppendLine(webRequestError.errorMessage);
				if (webRequestError.fieldValidationMessages != null && webRequestError.fieldValidationMessages.Count > 0)
				{
					stringBuilder.AppendLine("  fieldValidation:");
					foreach (KeyValuePair<string, string> fieldValidationMessage in webRequestError.fieldValidationMessages)
					{
						stringBuilder.Append("    [");
						stringBuilder.Append(fieldValidationMessage.Key);
						stringBuilder.Append("]=");
						stringBuilder.Append(fieldValidationMessage.Value);
						stringBuilder.AppendLine();
					}
				}
				stringBuilder.Append("  displayMessage=");
				stringBuilder.AppendLine(webRequestError.displayMessage);
			}
			stringBuilder.AppendLine("Body:");
			string text = null;
			try
			{
				text = ((webRequest.downloadHandler != null) ? webRequest.downloadHandler.text : "  NULL_DOWNLOAD_HANDLER");
			}
			catch
			{
				text = "  TEXT_ACCESS_NOT_SUPPORTED";
			}
			stringBuilder.AppendLine(text);
			return stringBuilder.ToString();
		}

		public static void ParseURLEncodedFormData(byte[] data, out List<StringValueParameter> stringFields)
		{
			stringFields = null;
			if (data == null || data.Length == 0)
			{
				return;
			}
			stringFields = new List<StringValueParameter>();
			string text = Encoding.UTF8.GetString(data);
			string[] array = text.Split(new char[1] { '&' }, StringSplitOptions.RemoveEmptyEntries);
			string[] array2 = array;
			foreach (string text2 in array2)
			{
				string[] array3 = text2.Split('=');
				if (array3.Length != 0)
				{
					if (array3.Length != 2)
					{
						stringFields.Add(StringValueParameter.Create(text2, "BADLY_FORMATTED_FORMDATA"));
					}
					else
					{
						stringFields.Add(StringValueParameter.Create(array3[0], array3[1]));
					}
				}
			}
		}

		public static void ParseMultipartFormData(byte[] data, out List<StringValueParameter> stringFields, out List<BinaryDataParameter> binaryFields)
		{
			stringFields = null;
			binaryFields = null;
			if (data == null || data.Length == 0)
			{
				return;
			}
			string text = Encoding.UTF8.GetString(data);
			string text2 = "\r\n";
			int num = -1;
			num = text.IndexOf(text2, 1);
			if (num < 0)
			{
				return;
			}
			string text3 = text.Substring(0, num).Trim();
			string[] array = text.Split(new string[1] { text3 }, StringSplitOptions.RemoveEmptyEntries);
			stringFields = new List<StringValueParameter>();
			binaryFields = new List<BinaryDataParameter>();
			string[] array2 = array;
			foreach (string text4 in array2)
			{
				string text5 = null;
				int num2 = 0;
				int num3 = 0;
				int num4 = 0;
				text5 = "Content-Type: ";
				num2 = text4.IndexOf(text5);
				if (num2 < 0)
				{
					continue;
				}
				num3 = num2 + text5.Length;
				num4 = text4.IndexOf(text2, num3);
				string text6 = text4.Substring(num3, num4 - num3);
				if (text6.Contains("text/plain"))
				{
					StringValueParameter stringValueParameter = new StringValueParameter();
					text5 = "name=\"";
					num2 = text4.IndexOf(text5);
					if (num2 < 0)
					{
						stringValueParameter.key = "KEY_NOT_FOUND";
					}
					else
					{
						num3 = num2 + text5.Length;
						num4 = text4.IndexOf("\"", num3);
						stringValueParameter.key = text4.Substring(num3, num4 - num3);
					}
					text5 = text2 + text2;
					num2 = text4.IndexOf(text5);
					if (num2 < 0)
					{
						stringValueParameter.value = "VALUE_NOT_FOUND";
					}
					else
					{
						num3 = num2 + text5.Length;
						stringValueParameter.value = text4.Substring(num3).Trim();
					}
					stringFields.Add(stringValueParameter);
					continue;
				}
				BinaryDataParameter binaryDataParameter = new BinaryDataParameter();
				binaryDataParameter.mimeType = text6;
				BinaryDataParameter binaryDataParameter2 = binaryDataParameter;
				text5 = "name=\"";
				num2 = text4.IndexOf(text5);
				if (num2 < 0)
				{
					binaryDataParameter2.key = "KEY_NOT_FOUND";
				}
				else
				{
					num3 = num2 + text5.Length;
					num4 = text4.IndexOf("\"", num3);
					binaryDataParameter2.key = text4.Substring(num3, num4 - num3);
				}
				text5 = "filename=\"";
				num2 = text4.IndexOf(text5);
				if (num2 < 0)
				{
					binaryDataParameter2.fileName = "FILENAME_NOT_FOUND";
				}
				else
				{
					num3 = num2 + text5.Length;
					num4 = text4.IndexOf("\"", num3);
					binaryDataParameter2.fileName = text4.Substring(num3, num4 - num3);
				}
				text5 = text2 + text2;
				num2 = text4.IndexOf(text5);
				if (num2 < 0)
				{
					binaryDataParameter2.contents = null;
				}
				else
				{
					num3 = num2 + text5.Length;
					int length = text4.Length - num3 - text2.Length;
					binaryDataParameter2.contents = Encoding.UTF8.GetBytes(text4.Substring(num3, length));
				}
				binaryFields.Add(binaryDataParameter2);
			}
		}

		public static string GenerateUserIdString(UserProfile profile)
		{
			if (profile == null)
			{
				return "NULL_USER_PROFILE";
			}
			string text = profile.username;
			if (string.IsNullOrEmpty(text))
			{
				text = "NO_USERNAME";
			}
			return "[" + profile.id + "]:" + text;
		}
	}
}
