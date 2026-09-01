using System.Runtime.CompilerServices;
using Poly.Math;

namespace Poly.Collide
{
	public struct Aabb
	{
		public Vec2 min;

		public Vec2 max;

		public Vec2 center => 0.5f * (min + max);

		public Vec2 size => max - min;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Aabb(Vec2 center, float padding)
		{
			min.x = center.x - padding;
			min.y = center.y - padding;
			max.x = center.x + padding;
			max.y = center.y + padding;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void _Include(Vec2 point)
		{
			if (point.x < min.x)
			{
				min.x = point.x;
			}
			if (point.y < min.y)
			{
				min.y = point.y;
			}
			if (max.x < point.x)
			{
				max.x = point.x;
			}
			if (max.y < point.y)
			{
				max.y = point.y;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void _Expand(float padding)
		{
			min.x -= padding;
			min.y -= padding;
			max.x += padding;
			max.y += padding;
		}

		public bool IsOverlappingY(Aabb other)
		{
			if (min.y <= other.max.y)
			{
				return other.min.y <= max.y;
			}
			return false;
		}

		public static implicit operator Aabb(Bounds2 b)
		{
			return new Aabb
			{
				min = b.min,
				max = b.max
			};
		}

		public static implicit operator Bounds2(Aabb a)
		{
			return new Bounds2
			{
				min = a.min,
				max = a.max
			};
		}
	}
}
