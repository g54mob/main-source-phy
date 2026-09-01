using System.Collections.Generic;
using System.Linq;

namespace mattmc3.dotmore.Extensions
{
	public static class Int32Extensions
	{
		public static IEnumerable<int> RangeTo(this int minValue, int maxValue, bool inclusive = true)
		{
			int num = (inclusive ? 1 : 0);
			return Enumerable.Range(minValue, maxValue - minValue + num);
		}
	}
}
