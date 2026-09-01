using System;
using Poly.Math;

namespace Poly.Collide
{
	[Serializable]
	public class Segment : Shape
	{
		public float halfLengthX = 0.5f;

		public Segment(float lengthX, float radius)
		{
			type = Type.Segment;
			halfLengthX = 0.5f * lengthX;
			base.radius = radius;
		}

		public override Aabb GetAabb(ref Transform2 t2, float padding)
		{
			Vec2 b = t2.rotation.basisX;
			Vec2.setAddMul(ref t2.position, ref b, 0f - halfLengthX, out var v);
			Vec2.setAddMul(ref t2.position, ref b, halfLengthX, out var v2);
			Aabb result = default(Aabb);
			result.max = (result.min = v);
			result._Include(v2);
			result._Expand(radius + padding);
			return result;
		}
	}
}
