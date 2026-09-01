using System;
using System.Globalization;

namespace FFmpeg
{
	public static class FFmpegProgressParser
	{
		private const string FORMAT = "HH:mm:ss.ff";

		private static readonly string[] durationSeparators = new string[2] { "Duration: ", ", start:" };

		private static readonly string[] timeSeparators = new string[2] { " time=", " bitrate=" };

		private static int lastDurationTokensLength;

		public static void Parse(string log, ref float durationMiniSec, ref float progress)
		{
			string[] array = log.Split(durationSeparators, StringSplitOptions.RemoveEmptyEntries);
			if (array.Length != lastDurationTokensLength)
			{
				UpdateDuration(array, ref durationMiniSec);
				lastDurationTokensLength = array.Length;
			}
			else if (durationMiniSec > 0f)
			{
				string timeToken = GetTimeToken(log, timeSeparators);
				if (timeToken != null)
				{
					progress = GetMiliSec(timeToken) / durationMiniSec;
				}
			}
		}

		private static void UpdateDuration(string[] tokens, ref float durationMiniSec)
		{
			durationMiniSec = 0f;
			for (int i = 0; i < tokens.Length; i++)
			{
				durationMiniSec += GetMiliSec(tokens[i]);
			}
		}

		private static string GetTimeToken(string log, string[] separators)
		{
			string[] array = log.Split(separators, StringSplitOptions.RemoveEmptyEntries);
			if (array.Length > 2)
			{
				return array[^2];
			}
			return null;
		}

		private static float GetMiliSec(string token)
		{
			if (DateTime.TryParseExact(token, "HH:mm:ss.ff", null, DateTimeStyles.None, out var result))
			{
				return (float)result.TimeOfDay.TotalMilliseconds;
			}
			return 0f;
		}
	}
}
