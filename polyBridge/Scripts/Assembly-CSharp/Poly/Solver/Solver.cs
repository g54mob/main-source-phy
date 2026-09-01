using System;
using System.Collections.Generic;
using System.Diagnostics;
using Poly.Base;
using Poly.Physics;
using UnityEngine;

namespace Poly.Solver
{
	public static class Solver
	{
		public struct DebugInfo
		{
			public int frameCount;

			public int iteration;

			public int subIteration;

			public int constraintIdx;

			[Conditional("DEBUG")]
			public void SetEdgeIteration(int i)
			{
				subIteration = i;
			}

			[Conditional("DEBUG")]
			public void SetConstraintIdx(int i)
			{
				constraintIdx = i;
			}
		}

		public static DebugInfo info;

		public static void Solve(FastList<SolverNode> nodesAL, SolverEdge[] edges, List<Poly.Physics.Rigidbody> bodies, int numSolverBodies, List<short> solverRun_segmentMotionIndices, FastList<Motion> motionsAL, List<DynamicAnchorJoint> dynamicAnchorJoints, List<Poly.Physics.Joint> joints, List<Poly.Physics.Joint> customShapeJoints, CollisionInfo[] bodyCollisions, int numBodyCollisions, CollisionInfo[] bridgeCollisions, int numBridgeCollisions, FastList<int> fullFrequencyBridgeContactIndices, SolverSettings settings, HydraulicController hydraulicController, bool areEdgesBreakable)
		{
			EdgeSolverInput edgeSolverInput = new EdgeSolverInput(edges, nodesAL, settings, areEdgesBreakable);
			_ = nodesAL.Count;
			SolverNode[] array = nodesAL.array;
			Motion[] array2 = motionsAL.array;
			float deltaTimeForVelocity = settings.deltaTimeForVelocity;
			float deltaTimeForVelocityEdge = settings.deltaTimeForVelocityEdge;
			for (int i = 0; i < settings.numIterations; i++)
			{
				info.iteration = i;
				bool isFirstIterationInFrame = i == 0;
				bool flag = i == settings.numIterations - 1 || settings.integrateInSolverIterations;
				bool flag2 = i == 0 || settings.integrateInSolverIterations;
				bool flag3 = settings.integrateInSolverIterations && settings.testOnly_integrateInEdgeSubIterations;
				bool flag4 = settings.bodyContact.useContactWarmstarting || settings.bridgeContact.useContactWarmstarting;
				bool flag5 = settings.bodyContact.runPostProjectionInCollisionOnEveryIntegration || settings.bridgeContact.runPostProjectionInCollisionOnEveryIntegration;
				if (!settings.debug_solveNodesLast)
				{
					SolveEdges(edgeSolverInput, hydraulicController, settings, deltaTimeForVelocityEdge, isFirstIterationInFrame, flag2, flag, flag3, dynamicAnchorJoints, joints, customShapeJoints, array, array2, nodesAL.Count, bridgeCollisions, fullFrequencyBridgeContactIndices, numBridgeCollisions, flag4);
				}
				if (!settings.dynamicAnchors.useJointWarmstarting)
				{
					DynamicAnchorJoint.All_Solve(dynamicAnchorJoints, array, array2, settings);
				}
				if (flag2)
				{
					for (int j = 0; j < solverRun_segmentMotionIndices.Count; j++)
					{
						short motionIdx = solverRun_segmentMotionIndices[j];
						Motion.ConvertNodesToMotion_InSolver_ComOnly(motionIdx, array, array2, settings.nodeToMotionVelocityMultiplier);
						Motion.ConvertNodesToMotion_InSolver_VelOnly(motionIdx, array, array2, settings.nodeToMotionVelocityMultiplier);
					}
				}
				WheelJoint.All_NotWarmstarted(joints, array2, flag2, settings);
				WheelJoint.All_NotWarmstarted(customShapeJoints, array2, flag2, settings);
				if (settings.joints.useJointWarmstarting)
				{
					if (flag2)
					{
						WheelJoint.All_Warmstart(joints, array2, settings);
						WheelJoint.All_Warmstart(customShapeJoints, array2, settings);
					}
					ContactSolver.MaybeWarmStartContacts(bodyCollisions, numBodyCollisions, array2, array, i, settings, flag2);
					ContactSolver.MaybeWarmStartContacts(bridgeCollisions, numBridgeCollisions, array2, array, i, settings, flag2);
					WheelJoint.All_SolveMotorsFirst(joints, array2, settings);
					WheelJoint.All_SolveMotorsFirst(customShapeJoints, array2, settings);
				}
				else
				{
					WheelJoint.All_SolveMotorsFirst(joints, array2, settings);
					WheelJoint.All_SolveMotorsFirst(customShapeJoints, array2, settings);
				}
				if (!settings.solveDynamicAnchorsInBridgeSolver)
				{
					if (settings.dynamicAnchors.useJointWarmstarting && flag2)
					{
						DynamicAnchorJoint.All_Warmstart(dynamicAnchorJoints, array, array2, settings);
					}
					if (settings.dynamicAnchors.useJointWarmstarting)
					{
						DynamicAnchorJoint.All_Solve(dynamicAnchorJoints, array, array2, settings);
					}
				}
				WheelJoint.All_Solve(joints, array2, settings);
				if (!settings.highFreqCustomShapeHinge)
				{
					WheelJoint.All_Solve(customShapeJoints, array2, settings);
				}
				if (!settings.joints.useJointWarmstarting)
				{
					ContactSolver.MaybeWarmStartContacts(bodyCollisions, numBodyCollisions, array2, array, i, settings, flag2);
					ContactSolver.MaybeWarmStartContacts(bridgeCollisions, numBridgeCollisions, array2, array, i, settings, flag2);
				}
				ContactSolver.SolveContacts(bodyCollisions, numBodyCollisions, array2, array, settings, flag2 && !flag4);
				if (settings.firstBridgeContactBeforeEdgeWarmstarting || !settings.highFreqBridgeContact)
				{
					ContactSolver.SolveContacts(bridgeCollisions, numBridgeCollisions, array2, array, settings, flag2 && !flag4);
				}
				if (settings.debug_solveNodesLast)
				{
					SolveEdges(edgeSolverInput, hydraulicController, settings, deltaTimeForVelocityEdge, isFirstIterationInFrame, flag2, flag, flag3, dynamicAnchorJoints, joints, customShapeJoints, array, array2, nodesAL.Count, bridgeCollisions, fullFrequencyBridgeContactIndices, numBridgeCollisions, flag4);
				}
				if (flag)
				{
					if (!flag3)
					{
						IntegrateNodes(nodesAL.array, nodesAL.Count, deltaTimeForVelocityEdge, settings);
					}
					IntegrateMotions(array2, bodies.Count, deltaTimeForVelocity, settings);
					if (!flag3 && !settings.debug_postProjectNodesLast)
					{
						PostProjectEdges(edgeSolverInput, settings, dynamicAnchorJoints, array, array2);
					}
					if (settings.joints.useJointWarmstarting)
					{
						WheelJoint.All_SolvePosition(joints, array2, settings);
						WheelJoint.All_SolvePosition(customShapeJoints, array2, settings);
					}
					if (flag4 && (flag5 || i == settings.numIterations - 1))
					{
						bool isLastFrame = i == settings.numIterations - 1;
						ContactSolver.SolveContacts_PostProjection(bodyCollisions, numBodyCollisions, array2, array, settings, isLastFrame);
						ContactSolver.SolveContacts_PostProjection(bridgeCollisions, numBridgeCollisions, array2, array, settings, isLastFrame);
					}
					if (!flag3 && settings.debug_postProjectNodesLast)
					{
						PostProjectEdges(edgeSolverInput, settings, dynamicAnchorJoints, array, array2);
					}
				}
			}
		}

		private static void SolveEdges(EdgeSolverInput edgeSolverInput, HydraulicController hydraulicController, SolverSettings settings, float edgeDeltaTime, bool isFirstIterationInFrame, bool isFirstAfterIntegration, bool doIntegrate, bool doEdgeSubIntegrate, List<DynamicAnchorJoint> dynamicAnchorJoints, List<Poly.Physics.Joint> joints, List<Poly.Physics.Joint> customShapeJoints, SolverNode[] nodesPtr, Motion[] motionsPtr, int burst_NodesCount, CollisionInfo[] bridgeCollisions, FastList<int> fullFrequencyBridgeContactIndices, int numBridgeCollisions, bool usingContactWarmstarting)
		{
			bool flag = settings.solveDynamicAnchorsInBridgeSolver && settings.dynamicAnchors.useJointWarmstarting;
			if (!doEdgeSubIntegrate)
			{
				if (isFirstAfterIntegration)
				{
					bool gatherSumImpulses = !isFirstIterationInFrame;
					if (settings.warmStarting)
					{
						EdgeSolver.CacheNormalsAndWarmStart(in edgeSolverInput, gatherSumImpulses);
						if (flag)
						{
							DynamicAnchorJoint.All_Warmstart_BridgeSolver(dynamicAnchorJoints, nodesPtr, motionsPtr, settings);
						}
					}
					else
					{
						EdgeSolver.CacheNormals(in edgeSolverInput, gatherSumImpulses);
					}
				}
				if (World.debug_useBurstJobs)
				{
					EdgeSolverJobs.SolveVelocityAndPosition_AllIterations(in settings, in edgeSolverInput, dynamicAnchorJoints, joints, customShapeJoints, nodesPtr, motionsPtr, burst_NodesCount, bridgeCollisions, fullFrequencyBridgeContactIndices, numBridgeCollisions, isFirstAfterIntegration, usingContactWarmstarting);
				}
				else
				{
					for (int i = 0; i < settings.numEdgeSubIterations; i++)
					{
						if (settings.highFreqCustomShapeHinge)
						{
							WheelJoint.All_Solve_HingeOnly(customShapeJoints, motionsPtr, settings);
						}
						if (settings.highFreqBridgeContact && (!settings.firstBridgeContactBeforeEdgeWarmstarting || 0 < i))
						{
							if (i % settings.highFreqBridgeContact_InvFactor == 0)
							{
								ContactSolver.SolveContacts(bridgeCollisions, numBridgeCollisions, motionsPtr, nodesPtr, settings, i == 0 && isFirstAfterIntegration && !usingContactWarmstarting);
							}
							else if (settings.fullFrequencyOverrideForBracingNodes)
							{
								ContactSolver.SolveContacts_SelectedCInfosOnly(bridgeCollisions, fullFrequencyBridgeContactIndices.array, fullFrequencyBridgeContactIndices.Count, numBridgeCollisions, motionsPtr, nodesPtr, settings, i == 0 && isFirstAfterIntegration && !usingContactWarmstarting);
							}
						}
						if (flag)
						{
							DynamicAnchorJoint.All_Solve_BridgeSolver(dynamicAnchorJoints, nodesPtr, motionsPtr, settings);
						}
						if (World.debug_useBurstJobs)
						{
							EdgeSolverJobs.SolveVelocityAndPosition(in edgeSolverInput);
						}
						else
						{
							EdgeSolver.SolveVelocityAndPosition(in edgeSolverInput);
						}
					}
				}
				if (doIntegrate)
				{
					hydraulicController.UpdateInSolverOnIntegration(edgeSolverInput.edges);
				}
				return;
			}
			UnityEngine.Debug.LogWarning("Numerical inconsistency: Body integration steps are not adjusted to higher-frequency integration. And they're not sub-integrated along with points.");
			for (int j = 0; j < settings.numEdgeSubIterations; j++)
			{
				bool gatherSumImpulses2 = !isFirstIterationInFrame;
				if (settings.warmStarting)
				{
					EdgeSolver.CacheNormalsAndWarmStart(in edgeSolverInput, gatherSumImpulses2);
					if (flag)
					{
						DynamicAnchorJoint.All_Warmstart_BridgeSolver(dynamicAnchorJoints, nodesPtr, motionsPtr, settings);
					}
				}
				else
				{
					EdgeSolver.CacheNormals(in edgeSolverInput, gatherSumImpulses2);
				}
				if (settings.highFreqCustomShapeHinge)
				{
					WheelJoint.All_Solve_HingeOnly(customShapeJoints, motionsPtr, settings);
				}
				if (settings.highFreqBridgeContact && j % settings.highFreqBridgeContact_InvFactor == 0 && (!settings.firstBridgeContactBeforeEdgeWarmstarting || 0 < j))
				{
					ContactSolver.SolveContacts(bridgeCollisions, numBridgeCollisions, motionsPtr, nodesPtr, settings, (j == 0 && isFirstAfterIntegration && !usingContactWarmstarting) || j != 0);
				}
				if (flag)
				{
					DynamicAnchorJoint.All_Solve_BridgeSolver(dynamicAnchorJoints, nodesPtr, motionsPtr, settings);
				}
				if (World.debug_useBurstJobs)
				{
					EdgeSolverJobs.SolveVelocityAndPosition(in edgeSolverInput);
				}
				else
				{
					EdgeSolver.SolveVelocityAndPosition(in edgeSolverInput);
				}
				IntegrateNodes(edgeSolverInput.nodes, edgeSolverInput.numNodes, edgeDeltaTime, settings);
				hydraulicController.UpdateInSolverOnIntegration(edgeSolverInput.edges);
			}
		}

		private static void PostProjectEdges(EdgeSolverInput edgeSolverInput, SolverSettings settings, List<DynamicAnchorJoint> dynamicAnchorJoints, SolverNode[] nodesPtr, Motion[] motionsPtr)
		{
			if (!settings.usePostProjection)
			{
				return;
			}
			bool flag = settings.solveDynamicAnchorsInBridgeSolver && settings.dynamicAnchors.useJointWarmstarting;
			for (int i = 0; i < settings.posSubIterations; i++)
			{
				EdgeSolver.SolvePosition(in edgeSolverInput);
				if (flag)
				{
					DynamicAnchorJoint.All_SolvePosition_BridgeSolver(dynamicAnchorJoints, nodesPtr, motionsPtr, settings);
				}
			}
		}

		public static float CheckImpulseAccumulatorsForBreakage(SolverEdge[] edges, SolverSettings settings)
		{
			_ = 1f / (settings.deltaTimeForVelocityEdge * settings.deltaTimeForVelocityEdge);
			float num = 0f;
			for (int i = 0; i < edges.Length; i++)
			{
				ref SolverEdge reference = ref edges[i];
				reference.sumFullImpulsesInFrame += reference.sumFullImpulses;
				if (reference.pin_isUnbreakable)
				{
					continue;
				}
				float num2 = reference.sumFullImpulsesInFrame / (float)settings.numEdgeIntegrationsPerFrame;
				if (reference.isBroken || num2 < (0f - reference.maxImpulsePerIntegration) * 0.999999f || reference.maxTensionImpulseFactor * reference.maxImpulsePerIntegration * 0.999999f < num2)
				{
					if (SingletonBehaviour<World>.instance.areEdgesBreakable)
					{
						reference.isBroken = true;
					}
					if (!reference.excludeFromMaxStressCalculation)
					{
						num = 1f;
					}
					continue;
				}
				float b = ((num2 < 0f) ? (num2 / (0f - (reference.maxImpulsePerIntegration + 5.877472E-39f))) : (num2 / (reference.maxTensionImpulseFactor * reference.maxImpulsePerIntegration + 5.877472E-39f)));
				if (!reference.excludeFromMaxStressCalculation)
				{
					num = Mathf.Max(num, b);
				}
				if (settings.modifyImpulseClampingOverTime)
				{
					if (reference.wasForceClampedDuringFrame || reference.isForceClamped)
					{
						reference.wasForceClampedDuringFrame = false;
						reference.isForceClamped = false;
						float num3 = settings.inSolverImpulseLimitMultiplier * reference.impulseLimitFactor;
						num3 = 1f + (num3 - 1f) * settings.impulseLimitExcessMultiplierAfterImpulseWasClipped;
						num3 = ((num3 <= 1.5f) ? num3 : 1.5f);
						reference.impulseLimitFactor = num3 / settings.inSolverImpulseLimitMultiplier;
					}
					else if (1f < reference.impulseLimitFactor)
					{
						float num4 = settings.inSolverImpulseLimitMultiplier * reference.impulseLimitFactor;
						num4 = 1f + (num4 - 1f) / settings.impulseLimitExcessCooldownMultiplier;
						reference.impulseLimitFactor = num4 / settings.inSolverImpulseLimitMultiplier;
						reference.impulseLimitFactor = ((1.001f < reference.impulseLimitFactor) ? reference.impulseLimitFactor : 1f);
					}
				}
				else
				{
					reference.wasForceClampedDuringFrame = false;
					reference.isForceClamped = false;
				}
			}
			return num;
		}

		public static void IntegrateNodes(SolverNode[] nodesPtr, int numNodes, float deltaTime, SolverSettings settings)
		{
			Vec2 scaledGravity = settings.scaledGravity;
			float num = Mathf.Pow(1f - settings.nodeVelocityDrag, deltaTime);
			float num2 = scaledGravity.x * deltaTime * deltaTime;
			float num3 = scaledGravity.y * deltaTime * deltaTime;
			if (settings.clipNodeVelocities)
			{
				for (int i = 0; i < numNodes; i++)
				{
					ref SolverNode reference = ref nodesPtr[i];
					float num4 = reference.vel.x;
					float num5 = reference.vel.y;
					float num6 = num4 * num4 + num5 * num5;
					if (num6 > settings.maxLinearVelocityDisplacement_perIntegrationIteration * settings.maxLinearVelocityDisplacement_perIntegrationIteration)
					{
						float num7 = settings.maxLinearVelocityDisplacement_perIntegrationIteration / Mathf.Sqrt(num6);
						num4 *= num7;
						num5 *= num7;
					}
					reference.pos.x += num4;
					reference.pos.y += num5;
					reference.vel.x = num4 * num + reference.gravityScale * num2;
					reference.vel.y = num5 * num + reference.gravityScale * num3;
				}
			}
			else
			{
				for (int j = 0; j < numNodes; j++)
				{
					ref SolverNode reference2 = ref nodesPtr[j];
					float x = reference2.vel.x;
					float y = reference2.vel.y;
					reference2.pos.x += x;
					reference2.pos.y += y;
					reference2.vel.x = x * num + reference2.gravityScale * num2;
					reference2.vel.y = y * num + reference2.gravityScale * num3;
				}
			}
		}

		public static void IntegrateMotions(Motion[] motionsPtr, int numBodyMotions, float deltaTime, SolverSettings settings, bool clipAngles = false)
		{
			Vec2 scaledGravity = settings.scaledGravity;
			float oneLess_rigidbodyLinearDrag_PerIntegration = settings.oneLess_rigidbodyLinearDrag_PerIntegration;
			float oneLess_rigidbodyAngularDrag_PerIntegration = settings.oneLess_rigidbodyAngularDrag_PerIntegration;
			for (int i = 0; i < numBodyMotions; i++)
			{
				Motion motion = motionsPtr[i];
				Vec2 linVel = motion.linVel;
				float num = motion.angVel;
				if (settings.clipBodyVelocities)
				{
					float sqrMagnitude = linVel.sqrMagnitude;
					if (sqrMagnitude > settings.maxLinearVelocityDisplacement_perIntegrationIteration * settings.maxLinearVelocityDisplacement_perIntegrationIteration)
					{
						if (sqrMagnitude > 4f * settings.maxLinearVelocityDisplacement_perIntegrationIteration * settings.maxLinearVelocityDisplacement_perIntegrationIteration)
						{
							UnityEngine.Debug.LogWarning("Linear velocity very high; clamping");
						}
						float num2 = settings.maxLinearVelocityDisplacement_perIntegrationIteration / Mathf.Sqrt(sqrMagnitude);
						linVel *= num2;
					}
					if (Mathf.Abs(num) > settings.maxAngularVelocity_radPerSec_perIntegrationIteration)
					{
						if (Mathf.Abs(num) > 2f * settings.maxAngularVelocity_radPerSec_perIntegrationIteration)
						{
							UnityEngine.Debug.LogWarning("Angular velocity very high; clamping");
						}
						num = Mathf.Clamp(num, 0f - settings.maxAngularVelocity_radPerSec_perIntegrationIteration, settings.maxAngularVelocity_radPerSec_perIntegrationIteration);
					}
				}
				motion.com += linVel;
				motion.linVel = linVel * oneLess_rigidbodyLinearDrag_PerIntegration;
				motion.angle += num;
				motion.angVel = num * oneLess_rigidbodyAngularDrag_PerIntegration;
				if (motion.invMass != 0f)
				{
					motion.linVel += scaledGravity * deltaTime * deltaTime;
				}
				if (clipAngles)
				{
					int num3 = (int)(Mathf.Abs(motion.angle) / (MathF.PI * 4f));
					if (num3 >= 1)
					{
						if (motion.angle < 0f)
						{
							motion.angle += (float)num3 * 4f * MathF.PI;
						}
						else
						{
							motion.angle -= (float)num3 * 4f * MathF.PI;
						}
					}
					float num4 = MathF.PI * 20f * deltaTime;
					motion.angVel = Mathf.Clamp(motion.angVel, 0f - num4, num4);
				}
				motionsPtr[i] = motion;
			}
		}

		public static void ClipVelocityOfSingleMotion_4HFCD(ref Motion motion, SolverSettings settings)
		{
			Vec2 linVel = motion.linVel;
			float num = motion.angVel;
			if (settings.clipBodyVelocities)
			{
				float sqrMagnitude = linVel.sqrMagnitude;
				if (sqrMagnitude > settings.maxLinearVelocityDisplacement_perIntegrationIteration * settings.maxLinearVelocityDisplacement_perIntegrationIteration)
				{
					if (sqrMagnitude > 4f * settings.maxLinearVelocityDisplacement_perIntegrationIteration * settings.maxLinearVelocityDisplacement_perIntegrationIteration)
					{
						UnityEngine.Debug.LogWarning("Linear velocity very high; clamping");
					}
					float num2 = settings.maxLinearVelocityDisplacement_perIntegrationIteration / Mathf.Sqrt(sqrMagnitude);
					linVel *= num2;
				}
				if (Mathf.Abs(num) > settings.maxAngularVelocity_radPerSec_perIntegrationIteration)
				{
					if (Mathf.Abs(num) > 2f * settings.maxAngularVelocity_radPerSec_perIntegrationIteration)
					{
						UnityEngine.Debug.LogWarning("Angular velocity very high; clamping");
					}
					num = Mathf.Clamp(num, 0f - settings.maxAngularVelocity_radPerSec_perIntegrationIteration, settings.maxAngularVelocity_radPerSec_perIntegrationIteration);
				}
			}
			motion.linVel = linVel;
			motion.angVel = num;
		}

		public static void IntegrateSingleMotion_NoClipVelocities_4HFCD(ref Motion motion, float deltaTime, SolverSettings settings)
		{
			Vec2 linVel = motion.linVel;
			float angVel = motion.angVel;
			motion.com += linVel;
			motion.angle += angVel;
			motion.linVel = linVel * settings.oneLess_rigidbodyLinearDrag_PerIntegration;
			motion.angVel = angVel * settings.oneLess_rigidbodyAngularDrag_PerIntegration;
			if (motion.invMass != 0f)
			{
				motion.linVel += settings.scaledGravity * deltaTime * deltaTime;
			}
		}
	}
}
