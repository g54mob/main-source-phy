using System;

namespace Landfall.TABS
{
	public class TABSUtils
	{
		public static DateTime UnixToDate(int unixTime)
		{
			return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(unixTime).ToLocalTime();
		}
	}
}
