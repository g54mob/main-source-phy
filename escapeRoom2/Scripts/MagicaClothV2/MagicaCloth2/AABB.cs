using System;
using System.Runtime.CompilerServices;
using Unity.Mathematics;

namespace MagicaCloth2
{
	[Serializable]
	public struct AABB : IEquatable<AABB>
	{
		public float3 Min;

		public float3 Max;

		public float3 Extents => Max - Min;

		public float3 HalfExtents => (Max - Min) * 0.5f;

		public float3 Center => (Max + Min) * 0.5f;

		public float MaxSideLength
		{
			get
			{
				float3 extents = Extents;
				return math.max(math.max(extents.x, extents.y), extents.z);
			}
		}

		public bool IsValid => math.all(Min <= Max);

		public float SurfaceArea
		{
			get
			{
				float3 x = Max - Min;
				return 2f * math.dot(x, x.yzx);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public AABB(in float3 min, in float3 max)
		{
			Min = min;
			Max = max;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static AABB CreateFromCenterAndExtents(float3 center, float3 extents)
		{
			return CreateFromCenterAndHalfExtents(center, extents * 0.5f);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static AABB CreateFromCenterAndHalfExtents(float3 center, float3 halfExtents)
		{
			return new AABB(center - halfExtents, center + halfExtents);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Contains(in float3 point)
		{
			return math.all((point >= Min) & (point <= Max));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Contains(in AABB aabb)
		{
			return math.all((Min <= aabb.Min) & (Max >= aabb.Max));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Overlaps(in AABB aabb)
		{
			return math.all((Max >= aabb.Min) & (Min <= aabb.Max));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Expand(float signedDistance)
		{
			Min -= signedDistance;
			Max += signedDistance;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Encapsulate(in AABB aabb)
		{
			Min = math.min(Min, aabb.Min);
			Max = math.max(Max, aabb.Max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Encapsulate(in float3 point)
		{
			Min = math.min(Min, point);
			Max = math.max(Max, point);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(AABB other)
		{
			if (Min.Equals(other.Min))
			{
				return Max.Equals(other.Max);
			}
			return false;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Transform(in float4x4 toM)
		{
			float3 x = math.transform(toM, Min);
			float3 y = math.transform(toM, Max);
			Min = math.min(x, y);
			Max = math.max(x, y);
		}

		public override string ToString()
		{
			return $"AABB Center:{Center}, HalfExtents:{HalfExtents}, Min:{Min}, Max:{Max}";
		}
	}
}
