using System;

namespace mattmc3.dotmore.Extensions
{
	public static class DateTimeExtensions
	{
		public static DateTime ToLocalTime(this DateTime utcTime, TimeZoneInfo timeZone)
		{
			if (utcTime.Kind == DateTimeKind.Local)
			{
				throw new ArgumentException("The date time specified must have a DateTimeKind of UTC, not local");
			}
			if (timeZone == null)
			{
				throw new ArgumentNullException("timeZone");
			}
			return TimeZoneInfo.ConvertTimeFromUtc(utcTime, timeZone);
		}

		public static bool IsLastDayOfMonth(this DateTime dt)
		{
			return dt.AddDays(1.0).Month != dt.Month;
		}

		public static DateTime GetNext(this DateTime dt, DayOfWeek dayOfWeek)
		{
			int num = 0;
			num = ((dt.DayOfWeek >= dayOfWeek) ? ((int)(7 - dayOfWeek) + (int)dayOfWeek) : (dayOfWeek - dt.DayOfWeek));
			return dt.AddDays(num);
		}

		public static DateTime GetLast(this DateTime dt, DayOfWeek dayOfWeek)
		{
			int num = 0;
			num = ((dt.DayOfWeek <= dayOfWeek) ? ((int)(7 - dayOfWeek) + (int)dayOfWeek) : (dt.DayOfWeek - dayOfWeek));
			return dt.AddDays(num * -1);
		}

		public static double ToUnixTime(this DateTime dt)
		{
			DateTime dateTime = new DateTime(1970, 1, 1);
			return (dt - dateTime).TotalMilliseconds;
		}

		public static DateTime ConvertFromUnixTimestamp(this double timestamp)
		{
			return new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(timestamp);
		}

		public static bool IsWeekday(this DateTime dt)
		{
			return dt.DayOfWeek != DayOfWeek.Saturday && dt.DayOfWeek != DayOfWeek.Sunday;
		}

		public static bool IsWeekend(this DateTime dt)
		{
			return dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday;
		}

		public static string ToShortDateString(this DateTime? dateTime)
		{
			return dateTime.ToShortDateString(string.Empty);
		}

		public static string ToShortDateString(this DateTime? dateTime, string returnIfNull)
		{
			if (dateTime.HasValue)
			{
				return dateTime.Value.ToShortDateString();
			}
			return returnIfNull;
		}
	}
}
