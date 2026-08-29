using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace MagicaCloth2
{
	public static class NativeMultiHashMapExtensions
	{
		[BurstCompile]
		private struct SetParallelMultiHashMapJob<TKey, TValue> : IJob where TKey : unmanaged, IEquatable<TKey> where TValue : unmanaged
		{
			public NativeParallelMultiHashMap<TKey, TValue> map;

			[ReadOnly]
			public NativeArray<TKey> keyArray;

			[ReadOnly]
			public NativeArray<TValue> valueArray;

			public void Execute()
			{
				int length = keyArray.Length;
				for (int i = 0; i < length; i++)
				{
					map.Add(keyArray[i], valueArray[i]);
				}
			}
		}

		public static bool MC2Contains<TKey, TValue>(this ref NativeParallelMultiHashMap<TKey, TValue> map, TKey key, TValue value) where TKey : unmanaged, IEquatable<TKey> where TValue : unmanaged, IEquatable<TValue>
		{
			foreach (TValue item in map.GetValuesForKey(key))
			{
				if (item.Equals(value))
				{
					return true;
				}
			}
			return false;
		}

		public static void MC2UniqueAdd<TKey, TValue>(this ref NativeParallelMultiHashMap<TKey, TValue> map, TKey key, TValue value) where TKey : unmanaged, IEquatable<TKey> where TValue : unmanaged, IEquatable<TValue>
		{
			if (!map.MC2Contains(key, value))
			{
				map.Add(key, value);
			}
		}

		public static bool MC2RemoveValue<TKey, TValue>(this ref NativeParallelMultiHashMap<TKey, TValue> map, TKey key, TValue value) where TKey : unmanaged, IEquatable<TKey> where TValue : unmanaged, IEquatable<TValue>
		{
			if (map.TryGetFirstValue(key, out var item, out var it))
			{
				do
				{
					if (item.Equals(value))
					{
						map.Remove(it);
						return true;
					}
				}
				while (map.TryGetNextValue(out item, ref it));
			}
			return false;
		}

		public static FixedList512Bytes<TValue> MC2ToFixedList512Bytes<TKey, TValue>(this ref NativeParallelMultiHashMap<TKey, TValue> map, TKey key) where TKey : unmanaged, IEquatable<TKey> where TValue : unmanaged, IEquatable<TValue>
		{
			FixedList512Bytes<TValue> result = default(FixedList512Bytes<TValue>);
			if (map.ContainsKey(key))
			{
				foreach (TValue item in map.GetValuesForKey(key))
				{
					result.Add(item);
				}
			}
			return result;
		}

		public static FixedList128Bytes<TValue> MC2ToFixedList128Bytes<TKey, TValue>(this ref NativeParallelMultiHashMap<TKey, TValue> map, TKey key) where TKey : unmanaged, IEquatable<TKey> where TValue : unmanaged, IEquatable<TValue>
		{
			FixedList128Bytes<TValue> result = default(FixedList128Bytes<TValue>);
			if (map.ContainsKey(key))
			{
				foreach (TValue item in map.GetValuesForKey(key))
				{
					result.Add(item);
				}
			}
			return result;
		}

		public static (TKey[], TValue[]) MC2Serialize<TKey, TValue>(this ref NativeParallelMultiHashMap<TKey, TValue> map) where TKey : unmanaged, IEquatable<TKey> where TValue : unmanaged
		{
			if (!map.IsCreated || map.Count() == 0 || map.IsEmpty)
			{
				return (null, null);
			}
			using NativeArray<TKey> nativeArray = map.GetKeyArray(Allocator.Persistent);
			using NativeArray<TValue> nativeArray2 = map.GetValueArray(Allocator.Persistent);
			return (nativeArray.ToArray(), nativeArray2.ToArray());
		}

		public static NativeParallelMultiHashMap<int2, ushort> MC2Deserialize(int2[] keyArray, ushort[] valueArray)
		{
			int num = ((keyArray != null) ? keyArray.Length : 0);
			int num2 = ((valueArray != null) ? valueArray.Length : 0);
			NativeParallelMultiHashMap<int2, ushort> nativeParallelMultiHashMap = new NativeParallelMultiHashMap<int2, ushort>(num, Allocator.Persistent);
			if (num > 0 && num2 > 0)
			{
				using NativeArray<int2> keyArray2 = new NativeArray<int2>(keyArray, Allocator.Persistent);
				using NativeArray<ushort> valueArray2 = new NativeArray<ushort>(valueArray, Allocator.Persistent);
				new SetParallelMultiHashMapJob<int2, ushort>
				{
					map = nativeParallelMultiHashMap,
					keyArray = keyArray2,
					valueArray = valueArray2
				}.Run();
			}
			return nativeParallelMultiHashMap;
		}

		public static void MC2DisposeSafe<TKey, TValue>(this ref NativeParallelMultiHashMap<TKey, TValue> map) where TKey : unmanaged, IEquatable<TKey> where TValue : unmanaged, IEquatable<TValue>
		{
			if (map.IsCreated)
			{
				map.Dispose();
			}
		}
	}
}
