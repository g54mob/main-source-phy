using System;

namespace XGamingRuntime.Interop
{
	public struct TimeT
	{
		private readonly long SecondsSinceUnixEpoch;

		public DateTime DateTime
		{
			get
			{
				try
				{
					if (SecondsSinceUnixEpoch == -1)
					{
						return DateTime.MaxValue;
					}
					return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(SecondsSinceUnixEpoch);
				}
				catch (ArgumentOutOfRangeException)
				{
					return DateTime.MaxValue;
				}
			}
		}

		public TimeT(DateTime time)
		{
			SecondsSinceUnixEpoch = (long)(time - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
		}

		public TimeT(long secondSinceUnixEpoch)
		{
			SecondsSinceUnixEpoch = secondSinceUnixEpoch;
		}
	}
}
