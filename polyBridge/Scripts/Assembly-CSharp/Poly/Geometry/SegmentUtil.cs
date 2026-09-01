using System.Runtime.CompilerServices;

namespace Poly.Geometry
{
	public static class SegmentUtil
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool _AreNonZeroSegmentsIntersecting_OrNearlyIntersecting(ref Segment segA_in, ref Segment segB_in)
		{
			Vec2 vec = segA_in.v1 - segA_in.v0;
			Vec2 vec2 = segB_in.v1 - segB_in.v0;
			float magnitude = vec.magnitude;
			float magnitude2 = vec2.magnitude;
			vec /= magnitude + 5.877472E-39f;
			vec2 /= magnitude2 + 5.877472E-39f;
			Segment segment = default(Segment);
			segment.v0 = segA_in.v0 - vec * 0.01f;
			segment.v1 = segA_in.v1 + vec * 0.01f;
			Segment segment2 = default(Segment);
			segment2.v0 = segB_in.v0 - vec2 * 0.01f;
			segment2.v1 = segB_in.v1 + vec2 * 0.01f;
			Vec2 a = segment.normal;
			Vec2 a2 = segment2.normal;
			Vec2 b = segment2.v0 - segment.v0;
			Vec2 b2 = segment2.v1 - segment.v0;
			Vec2 b3 = segment.v0 - segment2.v0;
			Vec2 b4 = segment.v1 - segment2.v0;
			float num = Vec2.Dot(in a, in b);
			float num2 = Vec2.Dot(in a, in b2);
			float num3 = Vec2.Dot(in a2, in b3);
			float num4 = Vec2.Dot(in a2, in b4);
			if (num * num2 < 0f)
			{
				return num3 * num4 < 0f;
			}
			return false;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool _IsVertexWithinDistanceOfANonZeroSegment(ref Segment segA, ref Vec2 v2, float tolerance)
		{
			Vec2 v3 = segA.v0;
			Vec2 a = segA.v1 - segA.v0;
			float magnitude = a.magnitude;
			a /= magnitude + 5.877472E-39f;
			Vec2 b = v2 - v3;
			float num = Vec2.Dot(in a, in b);
			float num2 = Vec2.Dot(a.rotated90, in b);
			if (0f - tolerance <= num2 && num2 < tolerance && 0f - tolerance < num)
			{
				return num < tolerance + magnitude;
			}
			return false;
		}
	}
}
