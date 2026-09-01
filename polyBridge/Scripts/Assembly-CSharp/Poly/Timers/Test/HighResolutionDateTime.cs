using System;
using System.Runtime.InteropServices;

namespace Poly.Timers.Test
{
	public static class HighResolutionDateTime
	{
		public static bool IsAvailable { get; private set; }

		public static DateTime UtcNow
		{
			get
			{
				if (!IsAvailable)
				{
					throw new InvalidOperationException("High resolution clock isn't available.");
				}
				GetSystemTimePreciseAsFileTime(out var filetime);
				return DateTime.FromFileTimeUtc(filetime);
			}
		}

		[DllImport("Kernel32.dll")]
		private static extern void GetSystemTimePreciseAsFileTime(out long filetime);

		static HighResolutionDateTime()
		{
			try
			{
				GetSystemTimePreciseAsFileTime(out var _);
				IsAvailable = true;
			}
			catch (EntryPointNotFoundException)
			{
				IsAvailable = false;
			}
		}
	}
}
