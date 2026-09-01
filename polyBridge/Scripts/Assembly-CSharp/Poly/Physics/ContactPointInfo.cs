using UnityEngine;

namespace Poly.Physics
{
	public struct ContactPointInfo
	{
		public Vec2 position;

		public Vec2 normal;

		public Vec2 relativePointVelocityBeforeCollision;

		public Vec2 impulseApplied;

		public float distance;

		public float estimatedImpactImpulseMultiplier;

		public Vec2 delayedImpactImpulse;

		public bool isNewImpact;

		public float tangentVelocity => Mathf.Abs(Vec2.Dot(normal.rotated90, in relativePointVelocityBeforeCollision));
	}
}
