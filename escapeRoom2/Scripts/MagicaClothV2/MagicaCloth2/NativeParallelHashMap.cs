using System;
using Unity.Collections;

namespace MagicaCloth2
{
	public static class NativeParallelHashMap
	{
		public static void MC2DisposeSafe<TKey, TValue>(this ref NativeParallelHashMap<TKey, TValue> map) where TKey : unmanaged, IEquatable<TKey> where TValue : unmanaged, IEquatable<TValue>
		{
			if (map.IsCreated)
			{
				map.Dispose();
			}
		}
	}
}
