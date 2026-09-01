namespace Poly.Collide
{
	public struct ClosestPointProcess
	{
		public Vec2 pointInLocalA;

		public Vec2 pointInLocalB;

		public Vec2 normalInLocalA;

		public float distance;

		public Feature feature;

		public float tOnEdge;

		public float tEdgeInvLen;

		public float tDistMultiplier;
	}
}
