using System;
using System.Collections.Generic;
using Unity.Collections;

namespace MagicaCloth2
{
	public class MultiDataBuilder<T> : IDisposable where T : unmanaged
	{
		private int indexCount;

		public NativeParallelMultiHashMap<int, T> Map;

		public MultiDataBuilder(int indexCount, int dataCapacity)
		{
			this.indexCount = indexCount;
			Map = new NativeParallelMultiHashMap<int, T>(dataCapacity, Allocator.Persistent);
		}

		public void Dispose()
		{
			if (Map.IsCreated)
			{
				Map.Dispose();
			}
		}

		public int Count()
		{
			return Map.Count();
		}

		public int GetDataCount(int index)
		{
			if (!Map.ContainsKey(index))
			{
				return 0;
			}
			return Map.CountValuesForKey(index);
		}

		public void Add(int key, T data)
		{
			Map.Add(key, data);
		}

		public int CountValuesForKey(int key)
		{
			return Map.CountValuesForKey(key);
		}

		public (T[], uint[]) ToArray()
		{
			if (!Map.IsCreated || indexCount == 0)
			{
				return (null, null);
			}
			uint[] array = new uint[indexCount];
			List<T> list = new List<T>(Map.Capacity);
			for (int i = 0; i < indexCount; i++)
			{
				int count = list.Count;
				int num = 0;
				if (Map.ContainsKey(i))
				{
					foreach (T item in Map.GetValuesForKey(i))
					{
						list.Add(item);
						num++;
					}
				}
				array[i] = DataUtility.Pack12_20(num, count);
			}
			return (list.ToArray(), array);
		}

		public uint[] ToIndexArray()
		{
			return ToArray().Item2;
		}

		public void ToNativeArray(out NativeArray<uint> indexArray, out NativeArray<T> dataArray)
		{
			indexArray = new NativeArray<uint>(indexCount, Allocator.Persistent);
			List<T> list = new List<T>(Map.Capacity);
			for (int i = 0; i < indexCount; i++)
			{
				int count = list.Count;
				int num = 0;
				if (Map.ContainsKey(i))
				{
					foreach (T item in Map.GetValuesForKey(i))
					{
						list.Add(item);
						num++;
					}
				}
				indexArray[i] = DataUtility.Pack12_20(num, count);
			}
			dataArray = new NativeArray<T>(list.ToArray(), Allocator.Persistent);
		}
	}
}
