using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Poly.Math
{
	[Serializable]
	public struct Bounds2 : IComparable<Bounds2>
	{
		public Vec2 min;

		public Vec2 max;

		public Vec2 extents => 0.5f * (max - min);

		public Vec2 size => max - min;

		public Vec2 center => 0.5f * (min + max);

		public Bounds2(Vec2 center, Vec2 size)
		{
			Vec2.setAddMul(ref center, ref size, -0.5f, out min);
			Vec2.setAddMul(ref center, ref size, 0.5f, out max);
		}

		public void Expand(float amount)
		{
			min.addMul(in Vec2.one, 0f - amount);
			max.addMul(in Vec2.one, amount);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Contains(in Vec2 point)
		{
			if (min.x <= point.x && point.x <= max.x && min.y <= point.y)
			{
				return point.y <= max.y;
			}
			return false;
		}

		public int CompareTo(Bounds2 other)
		{
			int num = min.CompareTo(other.min);
			if (num == 0)
			{
				return max.CompareTo(other.max);
			}
			return num;
		}

		public static implicit operator Bounds2(Bounds b)
		{
			return new Bounds2((Vec2)b.center, (Vec2)b.size);
		}
	}
}
