using System.Collections.Generic;
using System.IO;
using System.Net;
using SRF;

namespace SRDebugger.Internal
{
	public static class SRDebugApiUtil
	{
		public static string ParseErrorException(WebException ex)
		{
			if (ex.Response == null)
			{
				return ex.Message;
			}
			try
			{
				string response = ReadResponseStream(ex.Response);
				return ParseErrorResponse(response);
			}
			catch
			{
				return ex.Message;
			}
		}

		public static string ParseErrorResponse(string response, string fallback = "Unexpected Response")
		{
			try
			{
				Dictionary<string, object> dictionary = (Dictionary<string, object>)Json.Deserialize(response);
				string empty = string.Empty;
				empty += dictionary["errorMessage"];
				object value;
				if (dictionary.TryGetValue("errors", out value) && value is IList<object>)
				{
					IList<object> list = value as IList<object>;
					foreach (object item in list)
					{
						empty += "\n";
						empty += item;
					}
				}
				return empty;
			}
			catch
			{
				if (response.Contains("<html>"))
				{
					return fallback;
				}
				return response;
			}
		}

		public static bool ReadResponse(HttpWebRequest request, out string result)
		{
			try
			{
				WebResponse response = request.GetResponse();
				result = ReadResponseStream(response);
				return true;
			}
			catch (WebException ex)
			{
				result = ParseErrorException(ex);
				return false;
			}
		}

		public static string ReadResponseStream(WebResponse stream)
		{
			using (Stream stream2 = stream.GetResponseStream())
			{
				using (StreamReader streamReader = new StreamReader(stream2))
				{
					return streamReader.ReadToEnd();
				}
			}
		}
	}
}
