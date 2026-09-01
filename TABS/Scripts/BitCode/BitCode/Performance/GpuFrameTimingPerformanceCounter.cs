using JetBrains.Annotations;
using UnityEngine;
using dycJggssKJBbYomRwEcQasvEaFIib;

namespace BitCode.Performance
{
	internal sealed class GpuFrameTimingPerformanceCounter : FrameTimingPerformanceCounterBase
	{
		public GpuFrameTimingPerformanceCounter(int historySize, [NotNull] DFaEhMkrGEEOwIAPDVnDUgbLXWaeB timingWrapper)
			: base(historySize, timingWrapper, (FrameTiming timing) => timing.gpuFrameTime)
		{
		}
	}
}
