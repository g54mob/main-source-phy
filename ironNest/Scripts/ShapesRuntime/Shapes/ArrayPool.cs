using System.Collections.Generic;

namespace Shapes
{
	internal static class ArrayPool<T>
	{
		private static readonly Stack<T[]> pool;

		public static T[] Alloc(int maxCount)
		{
			return null;
		}

		public static void Free(T[] obj)
		{
		}
	}
}
