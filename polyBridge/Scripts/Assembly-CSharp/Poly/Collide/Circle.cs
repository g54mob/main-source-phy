using System;
using Poly.Math;

namespace Poly.Collide
{
	[Serializable]
	public class Circle : Shape
	{
		public Circle(float radius)
		{
			type = Type.Circle;
			base.radius = radius;
		}

		public override Aabb GetAabb(ref Transform2 t2, float padding)
		{
			return new Aabb(t2.position, radius + padding);
		}
	}
}
