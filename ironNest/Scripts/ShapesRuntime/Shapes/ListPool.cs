using System.Collections.Generic;

namespace Shapes
{
	internal static class ListPool<T>
	{
		private static readonly Stack<List<T>> pool;

		public static List<T> Alloc()
		{
			return null;
		}

		public static void Free(List<T> list)
		{
		}
	}
}
