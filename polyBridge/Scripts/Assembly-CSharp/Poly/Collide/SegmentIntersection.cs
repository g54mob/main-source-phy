using Poly.Math;

namespace Poly.Collide
{
	public static class SegmentIntersection
	{
		public static bool Overlap(Segment segmentA, ref Transform2 tA, Segment segmentB, ref Transform2 tB)
		{
			Vec2 vec = tA.InvMul(tB.position);
			Vec2 vec2 = tA.rotation.InvMul(tB.rotation.basisX * segmentB.halfLengthX);
			Vec2 vec3 = vec + vec2;
			vec -= vec2;
			if (vec.y * vec3.y < 0f)
			{
				float num = vec.y / (vec.y - vec3.y);
				float num2 = (1f - num) * vec.x + num * vec3.x;
				if (num2 > 0f - segmentA.halfLengthX && num2 < segmentA.halfLengthX)
				{
					return true;
				}
			}
			return false;
		}

		public static bool Overlap(Segment segmentA, Vec2 vertB0InA, Vec2 vertB1InA)
		{
			Vec2 vec = vertB0InA;
			Vec2 vec2 = vertB1InA;
			if (vec.y * vec2.y < 0f)
			{
				float num = vec.y / (vec.y - vec2.y);
				float num2 = (1f - num) * vec.x + num * vec2.x;
				if (num2 > 0f - segmentA.halfLengthX && num2 < segmentA.halfLengthX)
				{
					return true;
				}
			}
			return false;
		}
	}
}
