using System.Collections.Generic;

namespace Koenigz.PerfectCulling
{
	public class PerfectCullingBakeGroupComparer : IEqualityComparer<PerfectCullingBakeGroup>
	{
		public bool Equals(PerfectCullingBakeGroup x, PerfectCullingBakeGroup y)
		{
			return false;
		}

		public int GetHashCode(PerfectCullingBakeGroup obj)
		{
			return 0;
		}
	}
}
