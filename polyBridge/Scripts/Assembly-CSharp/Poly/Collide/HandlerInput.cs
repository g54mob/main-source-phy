using Poly.Math;

namespace Poly.Collide
{
	public struct HandlerInput
	{
		public Transform2 wTa;

		public Transform2 wTb;

		public Shape a;

		public Shape b;

		public float collisionTolerance;

		public float maxDistForNewPoint;

		public RotationStateProcess rotationState;
	}
}
