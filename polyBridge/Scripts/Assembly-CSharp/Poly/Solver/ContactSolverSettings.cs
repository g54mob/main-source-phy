using System;
using UnityEngine;

namespace Poly.Solver
{
	[Serializable]
	public class ContactSolverSettings
	{
		public bool usePositionBasedFriction;

		[Range(1f, 100f)]
		public float posErrorClampingMultiplier = 2f;

		public bool useVelocityFriction = true;

		public bool usePrevFramesImpulseForCappingFriction;

		public const bool debug_forceUsePrevFramesImpulse = false;

		public const bool debug_dontZeroPosFriction = false;

		[Header("Standard settings")]
		[Range(0f, 1f)]
		public float tau = 0.8f;

		[Range(0f, 1f)]
		public float damping = 0.8f;

		[Range(0f, 1f)]
		public float frictionDamping = 0.8f;

		[Range(0f, 1f)]
		public float frictionTau = 0.8f;

		public float maxPosError = 0.2f;

		public float maxReferencePenetration = 1f;

		[Header("Warmstarting and Projection")]
		public bool useTauOffset = true;

		public bool useContactWarmstarting;

		[Range(0f, 1f)]
		public float contactWarmstartingRatio = 1f;

		[Range(0f, 1f)]
		public float frictionWarmstartingRatio = 1f;

		[Range(0f, 1f)]
		public float posTau = 1f;

		public bool runPostProjectionInCollisionOnEveryIntegration;

		public float maxPositionCorrection = 0.2f;

		public bool hackTest = true;

		[Header("High-speed Blending")]
		public Vec2 highSpeedBlendRange = new Vec2(10f, 20f);

		public float maxHighSpeedBlendDuration = 1f;

		public float highSpeedBlendCooldownDuration = 0.1f;

		[NonSerialized]
		public bool highSpeedBlend_forPosition;

		[NonSerialized]
		public bool highSpeedBlend_forVelocity;

		[NonSerialized]
		[Range(0f, 1f)]
		public float highSpeedBlend_maxBlendValue = 0.995f;

		[NonSerialized]
		public float deltaTimeForVelocity;

		[NonSerialized]
		public int numIterations;

		[NonSerialized]
		public float nodeToMotionVelocityMultiplier;

		[NonSerialized]
		public float motionToNodeVelocityMultiplier;

		[NonSerialized]
		public bool integrateInSolverIterations;

		[NonSerialized]
		public bool enableFriction;

		[NonSerialized]
		public bool trackContactImpulseThroughFrame = true;

		[NonSerialized]
		public bool legacy_convertPosErrorToVelError = true;

		[NonSerialized]
		public bool useTwoPointSolver;

		[NonSerialized]
		public bool useCentralFriction;
	}
}
