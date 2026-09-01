namespace Poly.Physics
{
	public struct TriangleShape
	{
		public Vec2 v0;

		public Vec2 v1;

		public Vec2 v2;

		public TriangleShape(Vec2 a, Vec2 b, Vec2 c)
		{
			v0 = a;
			v1 = b;
			v2 = c;
		}
	}
}
