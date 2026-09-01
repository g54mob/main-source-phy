using JetBrains.Annotations;
using UnityEngine;
using dycJggssKJBbYomRwEcQasvEaFIib;

namespace BitCode.Performance
{
	internal sealed class CpuFrameTimingPerformanceCounter : FrameTimingPerformanceCounterBase
	{
		public CpuFrameTimingPerformanceCounter(int historySize, [NotNull] DFaEhMkrGEEOwIAPDVnDUgbLXWaeB timingWrapper)
			: base(historySize, timingWrapper, (FrameTiming timing) => timing.cpuFrameTime)
		{
		}
	}
}
