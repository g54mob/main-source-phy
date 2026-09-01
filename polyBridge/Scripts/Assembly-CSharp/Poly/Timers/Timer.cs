using System.Diagnostics;
using Poly.Base;

namespace Poly.Timers
{
	public class Timer : Singleton<Timer>
	{
		public static int maxDepth;

		internal int writeIdx;

		internal int writeIdx_2;

		internal TimerInfo[] infos = new TimerInfo[16];

		internal TimerInfo[] infos_2 = new TimerInfo[16];

		internal bool validate_endToEndFrameTimerStarted;

		public static int currentDepth;

		internal int writeIdx_3;

		internal int writeIdx_4;

		internal TimerInfo[] info_3 = new TimerInfo[16];

		internal TimerInfo[] infos_4 = new TimerInfo[16];

		internal bool validate_endToEndFrameTimerStarted_3;

		public static int currentDepth_3;

		internal const int infosSize = 16;

		public bool isEmpty => writeIdx == 0;

		[Conditional("ENABLE_PERFORMANCE_TIMERS")]
		public static void Begin(TimerId id)
		{
		}

		[Conditional("ENABLE_PERFORMANCE_TIMERS")]
		public static void Split(TimerId id)
		{
		}

		[Conditional("ENABLE_PERFORMANCE_TIMERS")]
		public static void Split(TimerId prevId_toRemove, TimerId id)
		{
		}

		[Conditional("ENABLE_PERFORMANCE_TIMERS")]
		public static void End(TimerId id = TimerId.MatchLast)
		{
		}

		public void SwapAndResetActiveStream(bool skipAssert = false)
		{
			Values.Swap(ref infos, ref infos_2);
			Values.Swap(ref writeIdx, ref writeIdx_2);
			currentDepth = 0;
			writeIdx = 0;
			validate_endToEndFrameTimerStarted = false;
		}

		public void SwitchStreamGroupsForFixedUpdate()
		{
			Values.Swap(ref infos, ref info_3);
			Values.Swap(ref infos_2, ref infos_4);
			Values.Swap(ref writeIdx, ref writeIdx_3);
			Values.Swap(ref writeIdx_2, ref writeIdx_4);
			Values.Swap(ref validate_endToEndFrameTimerStarted, ref validate_endToEndFrameTimerStarted_3);
			Values.Swap(ref currentDepth, ref currentDepth_3);
		}

		public static string TimerResolutionInfo()
		{
			bool isHighResolution = Stopwatch.IsHighResolution;
			string text = "  Timer is " + (isHighResolution ? "" : "not ") + "high-resolution.\r\n";
			long frequency = Stopwatch.Frequency;
			return string.Concat(text + "  Timer frequency is " + frequency + " ticks per second.\r\n", "  Timer is accurate within ", (1000000000 / frequency).ToString(), " nanoseconds.\r\n");
		}
	}
}
