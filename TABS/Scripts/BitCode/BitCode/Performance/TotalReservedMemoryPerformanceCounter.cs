using UnityEngine.Profiling;
using dycJggssKJBbYomRwEcQasvEaFIib;

namespace BitCode.Performance
{
	internal sealed class TotalReservedMemoryPerformanceCounter : ioHvfLyWrTmuseRULrayklBvBDFh
	{
		public TotalReservedMemoryPerformanceCounter(int historySize)
			: base(historySize)
		{
		}

		protected override bool GetSample(out long retrievedSample)
		{
			retrievedSample = Profiler.GetTotalReservedMemoryLong();
			return true;
		}
	}
}
