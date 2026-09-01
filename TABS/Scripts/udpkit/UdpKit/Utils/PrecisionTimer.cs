using System.Diagnostics;

namespace UdpKit.Utils
{
	internal static class PrecisionTimer
	{
		private static readonly long start = Stopwatch.GetTimestamp();

		private static readonly double freq = 1f / (float)Stopwatch.Frequency;

		internal static uint GetCurrentTime()
		{
			return (uint)((double)(Stopwatch.GetTimestamp() - start) * freq * 1000.0);
		}
	}
}
