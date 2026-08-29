using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Mathematics;

namespace MagicaCloth2
{
	public class GridMap<T> : IDisposable where T : unmanaged
	{
		public struct GridEnumerator : IEnumerator<int3>, IEnumerator, IDisposable
		{
			internal NativeParallelMultiHashMap<int3, T> gridMap;

			internal int3 startGrid;

			internal int3 endGrid;

			internal int3 currentGrid;

			internal bool isFirst;

			public int3 Current => currentGrid;

			object IEnumerator.Current => Current;

			public void Dispose()
			{
			}

			public bool MoveNext()
			{
				if (isFirst)
				{
					isFirst = false;
					return true;
				}
				currentGrid.x++;
				if (currentGrid.x > endGrid.x)
				{
					currentGrid.x = startGrid.x;
					currentGrid.y++;
					if (currentGrid.y > endGrid.y)
					{
						currentGrid.y = startGrid.y;
						currentGrid.z++;
						if (currentGrid.z > endGrid.z)
						{
							return false;
						}
					}
				}
				return true;
			}

			public void Reset()
			{
				currentGrid = startGrid;
				isFirst = true;
			}

			public GridEnumerator GetEnumerator()
			{
				return this;
			}
		}

		private NativeParallelMultiHashMap<int3, T> gridMap;

		public int DataCount => gridMap.Count();

		public GridMap(int capacity = 0)
		{
			gridMap = new NativeParallelMultiHashMap<int3, T>(capacity, Allocator.Persistent);
		}

		public void Dispose()
		{
			if (gridMap.IsCreated)
			{
				gridMap.Dispose();
			}
		}

		public NativeParallelMultiHashMap<int3, T> GetMultiHashMap()
		{
			return gridMap;
		}

		public static GridEnumerator GetArea(int3 startGrid, int3 endGrid, NativeParallelMultiHashMap<int3, T> gridMap)
		{
			return new GridEnumerator
			{
				gridMap = gridMap,
				startGrid = math.min(startGrid, endGrid),
				endGrid = math.max(startGrid, endGrid),
				currentGrid = math.min(startGrid, endGrid),
				isFirst = true
			};
		}

		public static GridEnumerator GetArea(float3 pos, float radius, NativeParallelMultiHashMap<int3, T> gridMap, float gridSize)
		{
			int3 grid = GetGrid(pos - radius, gridSize);
			int3 grid2 = GetGrid(pos + radius, gridSize);
			return GetArea(grid, grid2, gridMap);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 GetGrid(float3 pos, float gridSize)
		{
			return new int3(math.floor(pos / gridSize));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddGrid(int3 grid, T data, NativeParallelMultiHashMap<int3, T> gridMap)
		{
			gridMap.Add(grid, data);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 AddGrid(float3 pos, T data, NativeParallelMultiHashMap<int3, T> gridMap, float gridSize)
		{
			int3 grid = GetGrid(pos, gridSize);
			gridMap.Add(grid, data);
			return grid;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 AddGrid(float3 pos, T data, NativeParallelMultiHashMap<int3, T>.ParallelWriter gridMap, float gridSize)
		{
			int3 grid = GetGrid(pos, gridSize);
			gridMap.Add(grid, data);
			return grid;
		}

		public static bool RemoveGrid(int3 grid, T data, NativeParallelMultiHashMap<int3, T> gridMap)
		{
			if (gridMap.ContainsKey(grid) && gridMap.TryGetFirstValue(grid, out var item, out var it))
			{
				do
				{
					if (item.Equals(data))
					{
						gridMap.Remove(it);
						return true;
					}
				}
				while (gridMap.TryGetNextValue(out item, ref it));
			}
			return false;
		}

		public static bool MoveGrid(int3 fromGrid, int3 toGrid, T data, NativeParallelMultiHashMap<int3, T> gridMap)
		{
			if (fromGrid.Equals(toGrid))
			{
				return false;
			}
			RemoveGrid(fromGrid, data, gridMap);
			AddGrid(toGrid, data, gridMap);
			return true;
		}
	}
}
