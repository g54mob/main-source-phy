#define TRACE
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace Open.Nat
{
	internal static class StreamExtensions
	{
		internal static string ReadAsMany(this StreamReader stream, int bytesToRead)
		{
			char[] array = new char[bytesToRead];
			stream.ReadBlock(array, 0, bytesToRead);
			return new string(array);
		}

		internal static string GetXmlElementText(this XmlNode node, string elementName)
		{
			XmlElement xmlElement = node[elementName];
			if (xmlElement == null)
			{
				return string.Empty;
			}
			return xmlElement.InnerText;
		}

		internal static bool ContainsIgnoreCase(this string s, string pattern)
		{
			return s.IndexOf(pattern, StringComparison.OrdinalIgnoreCase) >= 0;
		}

		internal static void LogInfo(this TraceSource source, string format, params object[] args)
		{
			source.TraceEvent(TraceEventType.Information, 0, format, args);
		}

		internal static void LogWarn(this TraceSource source, string format, params object[] args)
		{
			source.TraceEvent(TraceEventType.Warning, 0, format, args);
		}

		internal static void LogError(this TraceSource source, string format, params object[] args)
		{
			source.TraceEvent(TraceEventType.Error, 0, format, args);
		}

		internal static string ToPrintableXml(this XmlDocument document)
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.Unicode))
				{
					try
					{
						xmlTextWriter.Formatting = Formatting.Indented;
						document.WriteContentTo(xmlTextWriter);
						xmlTextWriter.Flush();
						memoryStream.Flush();
						memoryStream.Position = 0L;
						StreamReader streamReader = new StreamReader(memoryStream);
						return streamReader.ReadToEnd();
					}
					catch (Exception)
					{
						return document.ToString();
					}
				}
			}
		}

		public static Task<TResult> TimeoutAfter<TResult>(this Task<TResult> task, TimeSpan timeout)
		{
			CancellationTokenSource timeoutCancellationTokenSource = new CancellationTokenSource();
			return TaskExtension.WhenAny(task, TaskExtension.Delay(timeout, timeoutCancellationTokenSource.Token)).ContinueWith(delegate(Task<Task> t)
			{
				Task result = t.Result;
				if (result == task)
				{
					timeoutCancellationTokenSource.Cancel();
					return task;
				}
				throw new TimeoutException("The operation has timed out. The network is broken, router has gone or is too busy.");
			}).Unwrap();
		}
	}
}
