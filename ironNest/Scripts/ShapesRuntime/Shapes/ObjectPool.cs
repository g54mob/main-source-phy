using System.Collections.Generic;

namespace Shapes
{
	internal static class ObjectPool<T> where T : new()
	{
		private static readonly Stack<T> pool;

		public static T Alloc()
		{
			return default(T);
		}

		public static void Free(T obj)
		{
		}
	}
}
