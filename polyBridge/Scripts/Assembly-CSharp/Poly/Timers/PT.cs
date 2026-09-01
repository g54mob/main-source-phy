using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Poly.Timers
{
	public static class PT
	{
		public static long Frequency
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				QueryPerformanceFrequency(out var lpFrequency);
				return lpFrequency;
			}
		}

		public static long Now
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				QueryPerformanceCounter(out var lpPerformanceCount);
				return lpPerformanceCount;
			}
		}

		[DllImport("Kernel32.dll")]
		private static extern bool QueryPerformanceCounter(out long lpPerformanceCount);

		[DllImport("Kernel32.dll")]
		private static extern bool QueryPerformanceFrequency(out long lpFrequency);
	}
}
