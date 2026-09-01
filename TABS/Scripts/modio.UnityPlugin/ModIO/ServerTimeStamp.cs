using System;

namespace ModIO
{
	public static class ServerTimeStamp
	{
		private static readonly DateTime UNIX_EPOCH = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

		public static int Now => FromUTCDateTime(DateTime.UtcNow);

		public static int FromLocalDateTime(DateTime localDateTime)
		{
			return FromUTCDateTime(localDateTime.ToUniversalTime());
		}

		public static int FromUTCDateTime(DateTime utcDateTime)
		{
			return (int)utcDateTime.Subtract(UNIX_EPOCH).TotalSeconds;
		}

		public static DateTime ToLocalDateTime(int serverTimeStamp)
		{
			return UNIX_EPOCH.AddSeconds(serverTimeStamp).ToLocalTime();
		}

		public static DateTime ToUTCDateTime(int serverTimeStamp)
		{
			return UNIX_EPOCH.AddSeconds(serverTimeStamp);
		}
	}
}
