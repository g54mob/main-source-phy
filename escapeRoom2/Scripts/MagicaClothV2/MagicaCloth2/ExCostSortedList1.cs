using System;
using System.Runtime.CompilerServices;

namespace MagicaCloth2
{
	public struct ExCostSortedList1 : IComparable<ExCostSortedList1>
	{
		internal float cost;

		internal int data;

		public bool IsValid => cost >= 0f;

		public int Count
		{
			get
			{
				if (!IsValid)
				{
					return 0;
				}
				return 1;
			}
		}

		public float Cost => cost;

		public int Data => data;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ExCostSortedList1(float invalidCost)
		{
			cost = invalidCost;
			data = 0;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ExCostSortedList1(float invalidCost, int initData)
		{
			cost = invalidCost;
			data = initData;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Add(float cost, int item)
		{
			if (!IsValid || cost < this.cost)
			{
				this.cost = cost;
				data = item;
			}
		}

		public int CompareTo(ExCostSortedList1 other)
		{
			if (cost != other.cost)
			{
				if (!(cost < other.cost))
				{
					return 1;
				}
				return -1;
			}
			return 0;
		}

		public override string ToString()
		{
			return $"({cost} : {data})";
		}
	}
}
