using UnityEngine.Profiling;
using dycJggssKJBbYomRwEcQasvEaFIib;

namespace BitCode.Performance
{
	internal sealed class TotalAllocatedMemoryPerformanceCounter : ioHvfLyWrTmuseRULrayklBvBDFh
	{
		public TotalAllocatedMemoryPerformanceCounter(int historySize)
			: base(historySize)
		{
		}

		protected override bool GetSample(out long retrievedSample)
		{
			retrievedSample = Profiler.GetTotalAllocatedMemoryLong();
			return true;
		}
	}
}
