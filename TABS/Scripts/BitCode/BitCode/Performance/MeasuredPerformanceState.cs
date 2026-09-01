using System;

namespace BitCode.Performance
{
	[Flags]
	public enum MeasuredPerformanceState
	{
		Inconclusive = 0,
		Adequate = 1,
		GpuSurplus = 2,
		CpuSurplus = 4,
		GpuConstrained = 8,
		CpuConstrained = 0x10
	}
}
