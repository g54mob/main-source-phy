using System;
using Poly.Base;
using Poly.Physics;
using Poly.UI;
using UnityEngine;

namespace Poly.Solver
{
	[CreateAssetMenu(fileName = "SolverSettings", menuName = "BridgePhysics/SolverSettings", order = 1)]
	public class SolverSettings : ScriptableObject
	{
		[Header("World")]
		public Vec2 gravity = 9.81f * Vec2.down;

		[Range(0f, 1f)]
		public float gravityFadeinDuration;

		[Header("Solver Settings")]
		[Range(1f, 16f)]
		public int numIterations = 4;

		public bool integrateInSolverIterations = true;

		[Header("Edges")]
		[Tooltip("Add extra iterations at lower tau, for more uniform & realistic force distribution.")]
		[Range(1f, 128f)]
		public int numEdgeSubIterations = 18;

		[Tooltip("Tau around 0.5 seems to give good force distribution with about 1% error for some simple tests.")]
		[Range(0f, 1f)]
		public float edgeTau = 0.05f;

		[Range(0f, 1f)]
		public float edgeDamping = 0.7f;

		public bool warmStarting = true;

		[ShowIf("warmStarting", false, false, "")]
		[Range(0f, 1f)]
		public float warmStartingRatio = 0.95f;

		public bool usePostProjection;

		[Range(1f, 16f)]
		public int posSubIterations = 3;

		[Range(0f, 1f)]
		public float posEdgeTau = 0.7f;

		[Header("Stability")]
		[Range(0f, 0.2f)]
		public float ropeSlop = 0.001f;

		[Range(1f, 20f)]
		public float referencePenetrationRecoveryRateFactor = 3f;

		[Tooltip("Global cap on friction, to prevent crazy-high wheel-to-wheel friction")]
		[Range(0f, 10f)]
		public float globalFrictionCoefficientCap = 3.1622777f;

		public const bool debug_stabilizeContact = true;

		public bool prioritizeBridgeContact_AndEnableHighFreqBridgeContact;

		[ShowIf("prioritizeBridgeContact_AndEnableHighFreqBridgeContact", false, false, "")]
		public bool highFreqBridgeContact;

		[ShowIf("prioritizeBridgeContact_AndEnableHighFreqBridgeContact", false, false, "")]
		public int highFreqBridgeContact_InvFactor = 3;

		[ShowIf("prioritizeBridgeContact_AndEnableHighFreqBridgeContact", false, false, "")]
		public bool fullFrequencyOverrideForBracingNodes = true;

		public const bool fullFrequencyReverseOverrideForRoadNodes = false;

		[ShowIf("prioritizeBridgeContact_AndEnableHighFreqBridgeContact", false, false, "")]
		public bool firstBridgeContactBeforeEdgeWarmstarting;

		public const bool forceAllJointsToHighestFrequency = false;

		[Space(10f)]
		public bool debug_triggerInternalCollisionCallback;

		public bool debug_forceClearCollisionInfosBeforeProcess;

		public bool averageFrictionRefSurfaceDistance = true;

		public ContactSolverSettings bodyContact;

		public ContactSolverSettings bridgeContact;

		public JointSolverSettings joints;

		public bool solveDynamicAnchorsInBridgeSolver;

		public JointSolverSettings dynamicAnchors;

		[Header("Collisions")]
		[Range(0f, 1f)]
		public float collisionTolerance = 0.3f;

		public float maxContactPointDistance = 0.05f;

		public bool predictiveContactPoints = true;

		[Header("High-frequency Collisions (semi-continuous)")]
		public bool enableFastCollisions = true;

		public bool enableWelding = true;

		public bool betterFastCollisionSlicing = true;

		[NonSerialized]
		public bool betterSlowCollisionSlicing = true;

		[Header("High-frequency Debug")]
		[Tooltip("Forces re-collision between rigid bodies also.")]
		public bool testFastCollisionsForAllMarked;

		[Header("Dev-only")]
		public bool limitImpulsesInSolver = true;

		[Range(1.000001f, 2f)]
		[ShowIf("limitImpulsesInSolver", false, false, "")]
		public float inSolverImpulseLimitMultiplier = 1.001f;

		[ShowIf("limitImpulsesInSolver", false, false, "")]
		public bool useSharedLimitForEntireFrameDuration;

		[ShowIf("limitImpulsesInSolver", false, false, "")]
		public bool applyBreakageInSolver;

		[Header("Breaking impulse modification when impulse clamped")]
		public bool modifyImpulseClampingOverTime;

		[Range(1.000001f, 2f)]
		[ShowIf("modifyImpulseClampingOverTime", false, false, "")]
		public float impulseLimitExcessMultiplierAfterImpulseWasClipped = 2f;

		[ShowIf("modifyImpulseClampingOverTime", false, false, "")]
		public float impulseLimitExcessCooldownRateFactor = 0.01f;

		[NonSerialized]
		public float impulseLimitExcessCooldownMultiplier = 1f;

		[Header("Velocity Control")]
		[Range(0f, 1f)]
		public float nodeVelocityDrag;

		public bool clipNodeVelocities = true;

		[Range(0f, 1f)]
		public float rigidbodyLinearDrag;

		[Range(0f, 1f)]
		public float rigidbodyAngularDrag;

		[NonSerialized]
		public float oneLess_rigidbodyLinearDrag_PerIntegration;

		[NonSerialized]
		public float oneLess_rigidbodyAngularDrag_PerIntegration;

		public bool clipBodyVelocities = true;

		public float maxLinearVelocity = 100f;

		public float maxLinearVelocity_Unbreakable = 250f;

		public float maxAngularVelocity_degPerSec = 3600f;

		[HideInInspector]
		public float frameDeltaTime;

		[Header("Vehicle control")]
		public bool useSoftParkingBrake;

		[ShowIf("useSoftParkingBrake", false, false, "")]
		public float maxSoftParkingBrakeAngleDeg = 1f;

		[ShowIf("useSoftParkingBrake", false, false, "")]
		public float softParkingBrakeTau = 0.7f;

		[Header("Debug")]
		public bool enableFriction = true;

		[NonSerialized]
		public float strengthMultiplier = 1f;

		[NonSerialized]
		[Header("Optimization validation")]
		public readonly bool unwrapContactCaching = true;

		[Header("Don't touch, this has been tested: Solver order")]
		public bool debug_solveNodesLast = true;

		public bool debug_postProjectNodesLast = true;

		[Tooltip("Requires Cached Contact Info !! Otherwise stress numbers are wrong.")]
		public bool testOnly_integrateInEdgeSubIterations;

		public const bool moveBodyContactToAfterEdgeWarmstartingAlso = false;

		[NonSerialized]
		public bool debug_createPointsAtNegDistance;

		[NonSerialized]
		public bool debug_useCollisionCaches = true;

		[Header("Joint ang-vel damping")]
		[Range(0f, 1f)]
		public float trailerJointAngularDamping = 0.5f;

		[NonSerialized]
		public float trailerJointAngularDamping_dampingFactor = 1f;

		[Range(0f, 1f)]
		public float customShapeJointAngularDamping = 0.2f;

		[NonSerialized]
		public float customShapeJointAngularDamping_dampingFactor_PerCsIteration;

		[Range(0f, 1f)]
		public float customShapeJointLinearDamping = 0.004f;

		[NonSerialized]
		public float oneLess_customShapeJointLinearDamping_dampingFactor_PerCsIteration = 1f;

		[Header("High-frequency custom shape hinge joints")]
		public bool highFreqCustomShapeHinge;

		[NonSerialized]
		public float gravityMagnitude;

		[NonSerialized]
		public Vec2 scaledGravity;

		[NonSerialized]
		public float invNumIterations;

		[NonSerialized]
		public float maxLinearVelocityDisplacement_perIntegrationIteration;

		[NonSerialized]
		public float maxAngularVelocity_radPerSec_perIntegrationIteration;

		[NonSerialized]
		public float deltaTimeForVelocity;

		[NonSerialized]
		public float deltaTimeForVelocityEdge;

		[NonSerialized]
		public int numEdgeIterationsPerFrame;

		[NonSerialized]
		public int numEdgeIntegrationsPerFrame;

		[NonSerialized]
		public float nodeToMotionVelocityMultiplier;

		[NonSerialized]
		public float motionToNodeVelocityMultiplier;

		public bool trackEdgesWithCollisions_unused => false;

		private float _deltaTimeForVelocity
		{
			get
			{
				if (!integrateInSolverIterations)
				{
					return frameDeltaTime;
				}
				return frameDeltaTime / (float)numIterations;
			}
		}

		private float _deltaTimeForVelocityEdge
		{
			get
			{
				float num = frameDeltaTime;
				if (integrateInSolverIterations)
				{
					num /= (float)numIterations;
					if (testOnly_integrateInEdgeSubIterations)
					{
						num /= (float)numEdgeSubIterations;
					}
				}
				return num;
			}
		}

		private float _force2Impulse
		{
			get
			{
				float num = deltaTimeForVelocity * deltaTimeForVelocity;
				if (!integrateInSolverIterations)
				{
					num /= (float)numIterations;
				}
				return num;
			}
		}

		public void ForceUpdateScaledGravity(float timeElapsed)
		{
			scaledGravity = Mathf.Clamp01(timeElapsed / (gravityFadeinDuration + 5.877472E-39f)) * gravity;
		}

		public void CacheValuesForFrame(float timeElapsed, bool areEdgesBreakable)
		{
			if (timeElapsed < 2f * gravityFadeinDuration || SingletonBehaviour<World>.instance.frameCount < 2)
			{
				scaledGravity = Mathf.Clamp01(timeElapsed / (gravityFadeinDuration + 5.877472E-39f)) * gravity;
			}
			deltaTimeForVelocityEdge = _deltaTimeForVelocityEdge;
			deltaTimeForVelocity = _deltaTimeForVelocity;
			joints.force2ImpulseRB = _force2Impulse;
			numEdgeIterationsPerFrame = numIterations * numEdgeSubIterations;
			numEdgeIntegrationsPerFrame = 1;
			if (integrateInSolverIterations)
			{
				numEdgeIntegrationsPerFrame *= numIterations;
				if (testOnly_integrateInEdgeSubIterations)
				{
					numEdgeIntegrationsPerFrame *= numEdgeSubIterations;
				}
			}
			nodeToMotionVelocityMultiplier = ((integrateInSolverIterations && testOnly_integrateInEdgeSubIterations) ? ((float)numEdgeSubIterations) : 1f);
			motionToNodeVelocityMultiplier = 1f / nodeToMotionVelocityMultiplier;
			invNumIterations = 1f / (float)numIterations;
			gravityMagnitude = gravity.magnitude;
			oneLess_rigidbodyLinearDrag_PerIntegration = Mathf.Pow(1f - rigidbodyLinearDrag, deltaTimeForVelocity);
			oneLess_rigidbodyAngularDrag_PerIntegration = Mathf.Pow(1f - rigidbodyAngularDrag, deltaTimeForVelocity);
			maxLinearVelocityDisplacement_perIntegrationIteration = (areEdgesBreakable ? maxLinearVelocity : maxLinearVelocity_Unbreakable) * deltaTimeForVelocity;
			maxAngularVelocity_radPerSec_perIntegrationIteration = maxAngularVelocity_degPerSec * deltaTimeForVelocity * (MathF.PI / 180f);
			bodyContact.nodeToMotionVelocityMultiplier = nodeToMotionVelocityMultiplier;
			bodyContact.motionToNodeVelocityMultiplier = motionToNodeVelocityMultiplier;
			bodyContact.integrateInSolverIterations = integrateInSolverIterations;
			bodyContact.enableFriction = enableFriction;
			bodyContact.numIterations = numIterations;
			bodyContact.deltaTimeForVelocity = deltaTimeForVelocity;
			bridgeContact.nodeToMotionVelocityMultiplier = nodeToMotionVelocityMultiplier;
			bridgeContact.motionToNodeVelocityMultiplier = motionToNodeVelocityMultiplier;
			bridgeContact.integrateInSolverIterations = integrateInSolverIterations;
			bridgeContact.enableFriction = enableFriction;
			bridgeContact.numIterations = numIterations;
			bridgeContact.deltaTimeForVelocity = deltaTimeForVelocity;
			bodyContact.highSpeedBlend_forVelocity = false;
			bridgeContact.highSpeedBlend_forVelocity = false;
			impulseLimitExcessCooldownMultiplier = Mathf.Pow(impulseLimitExcessMultiplierAfterImpulseWasClipped, impulseLimitExcessCooldownRateFactor);
			trailerJointAngularDamping_dampingFactor = 1f - Mathf.Pow(1f - trailerJointAngularDamping, deltaTimeForVelocity);
			customShapeJointAngularDamping_dampingFactor_PerCsIteration = 1f - Mathf.Pow(1f - customShapeJointAngularDamping, deltaTimeForVelocity);
			oneLess_customShapeJointLinearDamping_dampingFactor_PerCsIteration = Mathf.Pow(1f - customShapeJointLinearDamping, deltaTimeForVelocity);
			if (highFreqCustomShapeHinge)
			{
				customShapeJointAngularDamping_dampingFactor_PerCsIteration = 1f - Mathf.Pow(1f - customShapeJointAngularDamping_dampingFactor_PerCsIteration, 1f / (float)numEdgeSubIterations);
				oneLess_customShapeJointLinearDamping_dampingFactor_PerCsIteration = Mathf.Pow(oneLess_customShapeJointLinearDamping_dampingFactor_PerCsIteration, 1f / (float)numEdgeSubIterations);
			}
		}
	}
}
