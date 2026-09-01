using System;
using Poly.Math;

namespace Poly.Collide
{
	[Serializable]
	public class AabbShape : Shape
	{
		public Aabb aabb;

		public AabbShape(Bounds2 bounds)
		{
			type = Type.AabbShape;
			radius = 0f;
			aabb = bounds;
		}

		public override Aabb GetAabb(ref Transform2 unused_t2, float unused_padding)
		{
			return aabb;
		}
	}
}
