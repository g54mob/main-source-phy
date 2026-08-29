using Unity.Mathematics;

namespace MagicaCloth2
{
	public struct ClothParameters
	{
		public float gravity;

		public float3 worldGravityDirection;

		public float gravityFalloff;

		public float stablizationTimeAfterReset;

		public float blendWeight;

		public float4x4 dampingCurveData;

		public float4x4 radiusCurveData;

		public ClothNormalAxis normalAxis;

		public float rotationalInterpolation;

		public float rootRotation;

		public CullingSettings.CullingParams culling;

		public InertiaConstraint.InertiaConstraintParams inertiaConstraint;

		public TetherConstraint.TetherConstraintParams tetherConstraint;

		public DistanceConstraint.DistanceConstraintParams distanceConstraint;

		public TriangleBendingConstraint.TriangleBendingConstraintParams triangleBendingConstraint;

		public AngleConstraint.AngleConstraintParams angleConstraint;

		public MotionConstraint.MotionConstraintParams motionConstraint;

		public ColliderCollisionConstraint.ColliderCollisionConstraintParams colliderCollisionConstraint;

		public SelfCollisionConstraint.SelfCollisionConstraintParams selfCollisionConstraint;

		public WindParams wind;

		public SpringConstraint.SpringConstraintParams springConstraint;
	}
}
