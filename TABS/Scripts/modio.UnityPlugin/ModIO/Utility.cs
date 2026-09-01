using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace ModIO
{
	public static class Utility
	{
		public static bool Like(this string toSearch, string toFind)
		{
			return new Regex("\\A" + new Regex("\\.|\\$|\\^|\\{|\\[|\\(|\\||\\)|\\*|\\+|\\?|\\\\").Replace(toFind, (Match ch) => "\\" + ch).Replace('_', '.').Replace("%", ".*") + "\\z", RegexOptions.Singleline).IsMatch(toSearch);
		}

		public static bool IsURL(string toCheck)
		{
			string text = "[a-zA-Z0-9-_.]+";
			return new Regex("^(http(s)?(://))?(www.)?" + text, RegexOptions.IgnoreCase).IsMatch(toCheck);
		}

		public static bool IsEmail(string toCheck)
		{
			return new Regex("^([a-z0-9\\+_\\-]+)(\\.[a-z0-9\\+_\\-]+)*@([a-z0-9\\-]+\\.)+[a-z]{2,63}$", RegexOptions.IgnoreCase).IsMatch(toCheck);
		}

		public static bool IsSecurityCode(string toCheck)
		{
			return new Regex("^[a-z0-9]{5}$", RegexOptions.IgnoreCase).IsMatch(toCheck);
		}

		public static void SafeMapArraysOrZero<T1, T2>(T1[] sourceArray, Func<T1, T2> mapElementDelegate, out T2[] destinationArray)
		{
			if (sourceArray == null)
			{
				destinationArray = new T2[0];
				return;
			}
			destinationArray = new T2[sourceArray.Length];
			for (int i = 0; i < sourceArray.Length; i++)
			{
				destinationArray[i] = mapElementDelegate(sourceArray[i]);
			}
		}

		public static string GenerateExceptionDebugString(Exception e)
		{
			StringBuilder stringBuilder = new StringBuilder();
			Exception baseException = e.GetBaseException();
			stringBuilder.Append(baseException.GetType().Name + ": " + baseException.Message + "\n");
			StackTrace stackTrace = new StackTrace(baseException, fNeedFileInfo: true);
			int num = Math.Min(stackTrace.FrameCount, 6);
			for (int i = 0; i < num; i++)
			{
				StackFrame frame = stackTrace.GetFrame(i);
				if (frame == null)
				{
					stringBuilder.AppendLine("[NULL STACK FRAME]");
					continue;
				}
				MethodBase method = frame.GetMethod();
				if (method != null)
				{
					stringBuilder.Append(string.Concat(method.ReflectedType, ".", method.Name, "("));
					ParameterInfo[] parameters = method.GetParameters();
					ParameterInfo[] array = parameters;
					foreach (ParameterInfo parameterInfo in array)
					{
						stringBuilder.Append(parameterInfo.ParameterType.Name + " " + parameterInfo.Name + ", ");
					}
					if (parameters.Length != 0)
					{
						stringBuilder.Length -= 2;
					}
					stringBuilder.Append(")");
				}
				else
				{
					stringBuilder.Append("[NULL METHOD REFERENCE]");
				}
				stringBuilder.AppendLine(" @ " + frame.GetFileName() + ":" + frame.GetFileLineNumber());
			}
			return stringBuilder.ToString();
		}

		public static string ExtractYouTubeIdFromURL(string youTubeURL)
		{
			string result = null;
			string pattern = "(?:https?:\\/\\/|\\/\\/)?(?:www\\.|m\\.)?(?:youtu\\.be\\/|youtube\\.com\\/(?:embed\\/|v\\/|watch\\?v=|watch\\?.+&v=))([\\w-]{11})(?![\\w-])";
			Match match = Regex.Match(youTubeURL, pattern);
			if (match != null)
			{
				result = match.Groups[1].Value;
			}
			return result;
		}

		public static string GenerateYouTubeThumbnailURL(string youTubeId)
		{
			return "https://img.youtube.com/vi/" + youTubeId + "/hqdefault.jpg";
		}

		public static string EncodeEncryptedAppTicket(byte[] ticketData, uint ticketSize)
		{
			byte[] array = new byte[ticketSize];
			Array.Copy(ticketData, array, ticketSize);
			string result = null;
			try
			{
				result = Convert.ToBase64String(array);
			}
			catch
			{
			}
			return result;
		}

		public static string SafeTrimString(string s)
		{
			if (s == null)
			{
				return string.Empty;
			}
			return s.Trim();
		}

		public static int[] MapProfileIds(IList<ModProfile> profiles)
		{
			if (profiles == null)
			{
				return null;
			}
			int[] array = new int[profiles.Count];
			for (int i = 0; i < profiles.Count; i++)
			{
				array[i] = profiles[i]?.id ?? 0;
			}
			return array;
		}

		[Obsolete("Use EncodeEncryptedAppTicket() instead")]
		public static string ConvertSteamEncryptedAppTicket(byte[] pTicket, uint pcbTicket)
		{
			return EncodeEncryptedAppTicket(pTicket, pcbTicket);
		}
	}
}
