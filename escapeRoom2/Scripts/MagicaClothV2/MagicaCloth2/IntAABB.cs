using System;
using System.Runtime.CompilerServices;
using Unity.Mathematics;

namespace MagicaCloth2
{
	[Serializable]
	public struct IntAABB : IEquatable<IntAABB>
	{
		public int3 Min;

		public int3 Max;

		public int3 Extents => Max - Min;

		public int3 Center => (Max + Min) / 2;

		public bool IsValid => math.all(Min <= Max);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public IntAABB(int3 min, int3 max)
		{
			Min = min;
			Max = max;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Contains(int3 point)
		{
			return math.all((point >= Min) & (point <= Max));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Contains(IntAABB aabb)
		{
			return math.all((Min <= aabb.Min) & (Max >= aabb.Max));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Overlaps(IntAABB aabb)
		{
			return math.all((Max >= aabb.Min) & (Min <= aabb.Max));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Expand(int signedDistance)
		{
			Min -= signedDistance;
			Max += signedDistance;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Encapsulate(IntAABB aabb)
		{
			Min = math.min(Min, aabb.Min);
			Max = math.max(Max, aabb.Max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Encapsulate(int3 point)
		{
			Min = math.min(Min, point);
			Max = math.max(Max, point);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(IntAABB other)
		{
			if (Min.Equals(other.Min))
			{
				return Max.Equals(other.Max);
			}
			return false;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return $"AABB({Min}, {Max})";
		}
	}
}
