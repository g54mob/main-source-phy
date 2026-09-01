using System;
using dycJggssKJBbYomRwEcQasvEaFIib;

namespace BitCode.Performance
{
	internal sealed class TotalGcMemoryPerformanceCounter : ioHvfLyWrTmuseRULrayklBvBDFh
	{
		public TotalGcMemoryPerformanceCounter(int historySize)
			: base(historySize)
		{
		}

		protected override bool GetSample(out long retrievedSample)
		{
			retrievedSample = GC.GetTotalMemory(forceFullCollection: false);
			return true;
		}
	}
}
