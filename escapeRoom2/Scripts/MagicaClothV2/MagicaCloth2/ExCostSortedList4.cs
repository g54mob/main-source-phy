using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Mathematics;

namespace MagicaCloth2
{
	public struct ExCostSortedList4
	{
		internal float4 costs;

		internal int4 data;

		public int Count
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				for (int num = 3; num >= 0; num--)
				{
					if (costs[num] >= 0f)
					{
						return num + 1;
					}
				}
				return 0;
			}
		}

		public bool IsValid => costs[0] >= 0f;

		public float MinCost
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return costs[0];
			}
		}

		public float MaxCost
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				for (int num = 3; num >= 0; num--)
				{
					if (costs[num] >= 0f)
					{
						return costs[num];
					}
				}
				return 0f;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ExCostSortedList4(float invalidCost)
		{
			costs = invalidCost;
			data = 0;
		}

		public bool Add(float cost, int item)
		{
			if (costs[3] >= 0f && cost > costs[3])
			{
				return false;
			}
			for (int i = 0; i < 4; i++)
			{
				float num = costs[i];
				if (num < 0f)
				{
					costs[i] = cost;
					data[i] = item;
					return true;
				}
				if (cost < num)
				{
					for (int num2 = 2; num2 >= i; num2--)
					{
						costs[num2 + 1] = costs[num2];
						data[num2 + 1] = data[num2];
					}
					costs[i] = cost;
					data[i] = item;
					return true;
				}
			}
			return false;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Contains(int item)
		{
			for (int i = 0; i < 4; i++)
			{
				if (costs[i] >= 0f && data[i] == item)
				{
					return true;
				}
			}
			return false;
		}

		public int indexOf(int item)
		{
			for (int i = 0; i < 4; i++)
			{
				if (costs[i] >= 0f && data[i] == item)
				{
					return i;
				}
			}
			return -1;
		}

		public void RemoveItem(int item)
		{
			int num = -1;
			for (int i = 0; i < 4; i++)
			{
				if (costs[i] >= 0f && data[i] == item)
				{
					num = i;
					break;
				}
			}
			if (num >= 0)
			{
				for (int j = num; j < 3; j++)
				{
					costs[j] = costs[j + 1];
					data[j] = data[j + 1];
				}
				costs[3] = -1f;
			}
		}

		public override string ToString()
		{
			FixedString512Bytes fs = default(FixedString512Bytes);
			for (int i = 0; i < Count; i++)
			{
				FixedStringMethods.Append(ref fs, $"({costs[i]} : {data[i]}) ");
			}
			return fs.ToString();
		}
	}
}
