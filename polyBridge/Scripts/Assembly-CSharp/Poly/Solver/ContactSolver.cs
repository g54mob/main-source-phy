using System;
using Pb;
using Poly.Collide;
using Poly.Draw;
using UnityEngine;

namespace Poly.Solver
{
	public static class ContactSolver
	{
		public struct Mtx22
		{
			public float m00;

			public float m01;

			public float m10;

			public float m11;

			public static Vec2 operator *(Mtx22 m, Vec2 v)
			{
				return new Vec2(m.m00 * v.x + m.m01 * v.y, m.m10 * v.x + m.m11 * v.y);
			}
		}

		public static bool debugOnce_01;

		public static int[] twoPointSolverCaseCounts = new int[4];

		public static void MaybeWarmStartContacts(CollisionInfo[] collisions, int numCollisions, Motion[] motionsPtr, SolverNode[] nodesPtr, int iterationNumber, SolverSettings solverSettings, bool recomputeDistance)
		{
			if ((!solverSettings.bodyContact.useContactWarmstarting && !solverSettings.bridgeContact.useContactWarmstarting) || (!solverSettings.integrateInSolverIterations && iterationNumber != 0))
			{
				return;
			}
			for (int i = 0; i < numCollisions; i++)
			{
				ref CollisionInfo reference = ref collisions[i];
				ContactSolverSettings contactSolverSettings = reference.contactSolverSettings;
				if (recomputeDistance)
				{
					RecomputeDistance(ref reference, motionsPtr, nodesPtr);
				}
				if (reference.minDistanceInFrame <= reference.maxContactPointDistance_experiment && contactSolverSettings.useContactWarmstarting)
				{
					switch (reference.entityTypes)
					{
					case EntityTypes.BodyBody:
						WarmstartSingle_Rigidbodies(ref reference, motionsPtr, contactSolverSettings);
						break;
					case EntityTypes.BodyEdge:
						WarmstartSingle_RigidbodyVsSegment(ref reference, nodesPtr, motionsPtr, contactSolverSettings, contactSolverSettings.motionToNodeVelocityMultiplier);
						break;
					case EntityTypes.BodyNode:
						WarmstartSingle_RigidbodyVsParticle(ref reference, nodesPtr, motionsPtr, contactSolverSettings, contactSolverSettings.motionToNodeVelocityMultiplier);
						break;
					case EntityTypes.EdgeNode:
						WarmstartSingle_SegmentVsParticle(ref reference, nodesPtr, motionsPtr, contactSolverSettings, contactSolverSettings.motionToNodeVelocityMultiplier);
						break;
					case EntityTypes.EdgeEdge:
						WarmstartSingle_TwoSegments(ref reference, nodesPtr, motionsPtr, contactSolverSettings, contactSolverSettings.motionToNodeVelocityMultiplier);
						break;
					}
				}
				else if (contactSolverSettings.hackTest || iterationNumber == 0)
				{
					reference.ZeroWarmstartingImpulses();
				}
				else
				{
					reference.Assert_WarmstartingImpulsesAreZero();
				}
			}
		}

		public static void RecomputeDistance(ref CollisionInfo info, Motion[] motionsPtr, SolverNode[] nodesPtr, bool updateMinDistance = true)
		{
			switch (info.entityTypes)
			{
			case EntityTypes.BodyBody:
			case EntityTypes.BodyEdge:
			case EntityTypes.EdgeEdge:
				info.RecalculateDistanceRB(motionsPtr);
				break;
			case EntityTypes.BodyNode:
			case EntityTypes.EdgeNode:
				info.RecalculateDistanceRB_Node1(motionsPtr, nodesPtr);
				break;
			case EntityTypes.NodeNode:
				info.RecalculateDistanceRB_Node0_Node1(nodesPtr);
				break;
			}
			if (updateMinDistance && info.distance < info.minDistanceInFrame)
			{
				info.minDistanceInFrame = info.distance;
			}
		}

		public static void SolveContacts(CollisionInfo[] collisions, int numCollisions, Motion[] motionsPtr, SolverNode[] nodesPtr, SolverSettings solverSettings, bool recomputeDistance)
		{
			int num = 1;
			bool flag = false;
			for (int i = 0; i < numCollisions; i += num)
			{
				ref CollisionInfo reference = ref collisions[i];
				ContactSolverSettings contactSolverSettings = reference.contactSolverSettings;
				if (recomputeDistance && !flag)
				{
					int num2 = i;
					do
					{
						RecomputeDistance(ref collisions[num2], motionsPtr, nodesPtr);
					}
					while (collisions[num2++].hasSecondPoint);
				}
				flag = false;
				bool flag2 = false;
				if (contactSolverSettings.useTwoPointSolver && reference.hasSecondPoint && reference.entityTypes == EntityTypes.BodyBody)
				{
					ref CollisionInfo reference2 = ref collisions[i + 1];
					if (reference.minDistanceInFrame <= reference.maxContactPointDistance_experiment && reference2.minDistanceInFrame <= reference2.maxContactPointDistance_experiment)
					{
						reference.MaybeOnlyInitReferenceDepth();
						reference2.MaybeOnlyInitReferenceDepth();
						switch (reference.entityTypes)
						{
						case EntityTypes.BodyEdge:
							Motion.ConvertNodesToMotion_InSolver_VelOnly(reference.motionIdx1, nodesPtr, motionsPtr, contactSolverSettings.nodeToMotionVelocityMultiplier);
							break;
						case EntityTypes.EdgeNode:
							Motion.ConvertNodesToMotion_InSolver_VelOnly(reference.motionIdx0, nodesPtr, motionsPtr, contactSolverSettings.nodeToMotionVelocityMultiplier);
							break;
						case EntityTypes.EdgeEdge:
							Motion.ConvertNodesToMotion_InSolver_VelOnly(reference.motionIdx0, nodesPtr, motionsPtr, contactSolverSettings.nodeToMotionVelocityMultiplier);
							Motion.ConvertNodesToMotion_InSolver_VelOnly(reference.motionIdx1, nodesPtr, motionsPtr, contactSolverSettings.nodeToMotionVelocityMultiplier);
							break;
						}
						bool num3 = SolveDouble_Rigidbodies(ref reference, ref reference2, motionsPtr, contactSolverSettings);
						num = 2;
						if (num3)
						{
							flag2 = true;
						}
					}
				}
				if (!flag2 && reference.minDistanceInFrame <= reference.maxContactPointDistance_experiment)
				{
					num = 1;
					if (reference.hasSecondPoint)
					{
						flag = true;
					}
					reference.MaybeOnlyInitReferenceDepth();
					switch (reference.entityTypes)
					{
					case EntityTypes.BodyEdge:
						Motion.ConvertNodesToMotion_InSolver_VelOnly(reference.motionIdx1, nodesPtr, motionsPtr, contactSolverSettings.nodeToMotionVelocityMultiplier);
						break;
					case EntityTypes.EdgeNode:
						Motion.ConvertNodesToMotion_InSolver_VelOnly(reference.motionIdx0, nodesPtr, motionsPtr, contactSolverSettings.nodeToMotionVelocityMultiplier);
						break;
					case EntityTypes.EdgeEdge:
						Motion.ConvertNodesToMotion_InSolver_VelOnly(reference.motionIdx0, nodesPtr, motionsPtr, contactSolverSettings.nodeToMotionVelocityMultiplier);
						Motion.ConvertNodesToMotion_InSolver_VelOnly(reference.motionIdx1, nodesPtr, motionsPtr, contactSolverSettings.nodeToMotionVelocityMultiplier);
						break;
					}
					ref CollisionInfo nextInfo = ref reference;
					if (reference.hasSecondPoint)
					{
						nextInfo = ref collisions[i + 1];
					}
					switch (reference.entityTypes)
					{
					case EntityTypes.BodyBody:
						SolveSingle_Rigidbodies(ref reference, motionsPtr, contactSolverSettings, ref nextInfo);
						break;
					case EntityTypes.BodyEdge:
						SolveSingle_RigidbodyVsSegment(ref reference, nodesPtr, motionsPtr, contactSolverSettings, contactSolverSettings.motionToNodeVelocityMultiplier, ref nextInfo);
						break;
					case EntityTypes.BodyNode:
						SolveSingle_RigidbodyVsParticle(ref reference, nodesPtr, motionsPtr, contactSolverSettings, contactSolverSettings.motionToNodeVelocityMultiplier, ref nextInfo);
						break;
					case EntityTypes.EdgeNode:
						SolveSingle_SegmentVsParticle(ref reference, nodesPtr, motionsPtr, contactSolverSettings, contactSolverSettings.motionToNodeVelocityMultiplier, ref nextInfo);
						break;
					case EntityTypes.EdgeEdge:
						SolveSingle_TwoSegments(ref reference, nodesPtr, motionsPtr, contactSolverSettings, contactSolverSettings.motionToNodeVelocityMultiplier, ref nextInfo);
						break;
					}
				}
				else if (!flag2)
				{
					num = 1;
					if (reference.hasSecondPoint)
					{
						flag = true;
					}
					reference.ResetReferenceDepth();
				}
			}
		}

		public static void SolveContacts_SelectedCInfosOnly(CollisionInfo[] collisions, int[] collisionIndices, int numCollisionIndices, int numCollisions_debug, Motion[] motionsPtr, SolverNode[] nodesPtr, SolverSettings solverSettings, bool recomputeDistance)
		{
			bool flag = false;
			for (int i = 0; i < numCollisionIndices; i++)
			{
				int num = collisionIndices[i];
				ref CollisionInfo reference = ref collisions[num];
				ContactSolverSettings contactSolverSettings = reference.contactSolverSettings;
				if (recomputeDistance && !flag)
				{
					int num2 = num;
					do
					{
						RecomputeDistance(ref collisions[num2], motionsPtr, nodesPtr);
					}
					while (collisions[num2++].hasSecondPoint);
				}
				flag = false;
				bool flag2 = false;
				if (contactSolverSettings.useTwoPointSolver && reference.hasSecondPoint && reference.entityTypes == EntityTypes.BodyBody)
				{
					ref CollisionInfo reference2 = ref collisions[num + 1];
					if (reference.minDistanceInFrame <= reference.maxContactPointDistance_experiment && reference2.minDistanceInFrame <= reference2.maxContactPointDistance_experiment)
					{
						reference.MaybeOnlyInitReferenceDepth();
						reference2.MaybeOnlyInitReferenceDepth();
						switch (reference.entityTypes)
						{
						case EntityTypes.BodyEdge:
							Motion.ConvertNodesToMotion_InSolver_VelOnly(reference.motionIdx1, nodesPtr, motionsPtr, contactSolverSettings.nodeToMotionVelocityMultiplier);
							break;
						case EntityTypes.EdgeNode:
							Motion.ConvertNodesToMotion_InSolver_VelOnly(reference.motionIdx0, nodesPtr, motionsPtr, contactSolverSettings.nodeToMotionVelocityMultiplier);
							break;
						case EntityTypes.EdgeEdge:
							Motion.ConvertNodesToMotion_InSolver_VelOnly(reference.motionIdx0, nodesPtr, motionsPtr, contactSolverSettings.nodeToMotionVelocityMultiplier);
							Motion.ConvertNodesToMotion_InSolver_VelOnly(reference.motionIdx1, nodesPtr, motionsPtr, contactSolverSettings.nodeToMotionVelocityMultiplier);
							break;
						}
						if (SolveDouble_Rigidbodies(ref reference, ref reference2, motionsPtr, contactSolverSettings))
						{
							flag2 = true;
						}
					}
				}
				if (!flag2 && reference.minDistanceInFrame <= reference.maxContactPointDistance_experiment)
				{
					if (reference.hasSecondPoint)
					{
						flag = true;
					}
					reference.MaybeOnlyInitReferenceDepth();
					switch (reference.entityTypes)
					{
					case EntityTypes.BodyEdge:
						Motion.ConvertNodesToMotion_InSolver_VelOnly(reference.motionIdx1, nodesPtr, motionsPtr, contactSolverSettings.nodeToMotionVelocityMultiplier);
						break;
					case EntityTypes.EdgeNode:
						Motion.ConvertNodesToMotion_InSolver_VelOnly(reference.motionIdx0, nodesPtr, motionsPtr, contactSolverSettings.nodeToMotionVelocityMultiplier);
						break;
					case EntityTypes.EdgeEdge:
						Motion.ConvertNodesToMotion_InSolver_VelOnly(reference.motionIdx0, nodesPtr, motionsPtr, contactSolverSettings.nodeToMotionVelocityMultiplier);
						Motion.ConvertNodesToMotion_InSolver_VelOnly(reference.motionIdx1, nodesPtr, motionsPtr, contactSolverSettings.nodeToMotionVelocityMultiplier);
						break;
					}
					ref CollisionInfo nextInfo = ref reference;
					if (reference.hasSecondPoint)
					{
						nextInfo = ref collisions[num + 1];
					}
					switch (reference.entityTypes)
					{
					case EntityTypes.BodyBody:
						SolveSingle_Rigidbodies(ref reference, motionsPtr, contactSolverSettings, ref nextInfo);
						break;
					case EntityTypes.BodyEdge:
						SolveSingle_RigidbodyVsSegment(ref reference, nodesPtr, motionsPtr, contactSolverSettings, contactSolverSettings.motionToNodeVelocityMultiplier, ref nextInfo);
						break;
					case EntityTypes.BodyNode:
						SolveSingle_RigidbodyVsParticle(ref reference, nodesPtr, motionsPtr, contactSolverSettings, contactSolverSettings.motionToNodeVelocityMultiplier, ref nextInfo);
						break;
					case EntityTypes.EdgeNode:
						SolveSingle_SegmentVsParticle(ref reference, nodesPtr, motionsPtr, contactSolverSettings, contactSolverSettings.motionToNodeVelocityMultiplier, ref nextInfo);
						break;
					case EntityTypes.EdgeEdge:
						SolveSingle_TwoSegments(ref reference, nodesPtr, motionsPtr, contactSolverSettings, contactSolverSettings.motionToNodeVelocityMultiplier, ref nextInfo);
						break;
					}
				}
				else if (!flag2)
				{
					if (reference.hasSecondPoint)
					{
						flag = true;
					}
					reference.ResetReferenceDepth();
				}
			}
		}

		public static void SolveSingle_RigidbodyVsParticle(ref CollisionInfo info, SolverNode[] nodesPtr, Motion[] motionsPtr, ContactSolverSettings settings, float motionToNodeVelocityMultiplier, ref CollisionInfo nextInfo)
		{
			ref Motion motion = ref motionsPtr[info.motionIdx0];
			Motion.ComputeFromNode(in nodesPtr[info.nodeIdx1], out var result, settings.nodeToMotionVelocityMultiplier);
			SolveOneCollision_AnyShape(ref info, ref motion, ref result, settings, ref nextInfo);
			nodesPtr[info.nodeIdx1].vel = result.linVel * settings.motionToNodeVelocityMultiplier;
		}

		public static void SolveSingle_SegmentVsParticle(ref CollisionInfo info, SolverNode[] nodesPtr, Motion[] motionsPtr, ContactSolverSettings settings, float motionToNodeVelocityMultiplier, ref CollisionInfo nextInfo)
		{
			ref Motion reference = ref motionsPtr[info.motionIdx0];
			Motion oldMotion = reference;
			Motion.ComputeFromNode(in nodesPtr[info.nodeIdx1], out var result, settings.nodeToMotionVelocityMultiplier);
			SolveOneCollision_AnyShape(ref info, ref reference, ref result, settings, ref nextInfo);
			SolveSingle_UpdateParticleVelocityForSegments(ref reference, in oldMotion, nodesPtr, settings, motionToNodeVelocityMultiplier);
			nodesPtr[info.nodeIdx1].vel = result.linVel * settings.motionToNodeVelocityMultiplier;
		}

		public static void SolveSingle_RigidbodyVsSegment(ref CollisionInfo info, SolverNode[] nodesPtr, Motion[] motionsPtr, ContactSolverSettings settings, float motionToNodeVelocityMultiplier, ref CollisionInfo nextInfo)
		{
			ref Motion motion = ref motionsPtr[info.motionIdx0];
			ref Motion reference = ref motionsPtr[info.motionIdx1];
			Motion oldMotion = reference;
			SolveOneCollision_AnyShape(ref info, ref motion, ref reference, settings, ref nextInfo);
			SolveSingle_UpdateParticleVelocityForSegments(ref reference, in oldMotion, nodesPtr, settings, motionToNodeVelocityMultiplier);
		}

		public static void SolveSingle_TwoSegments(ref CollisionInfo info, SolverNode[] nodesPtr, Motion[] motionsPtr, ContactSolverSettings settings, float motionToNodeVelocityMultiplier, ref CollisionInfo nextInfo)
		{
			ref Motion reference = ref motionsPtr[info.motionIdx0];
			ref Motion reference2 = ref motionsPtr[info.motionIdx1];
			Motion oldMotion = reference;
			Motion oldMotion2 = reference2;
			SolveOneCollision_AnyShape(ref info, ref reference, ref reference2, settings, ref nextInfo);
			SolveSingle_UpdateParticleVelocityForSegments(ref reference, in oldMotion, nodesPtr, settings, motionToNodeVelocityMultiplier);
			SolveSingle_UpdateParticleVelocityForSegments(ref reference2, in oldMotion2, nodesPtr, settings, motionToNodeVelocityMultiplier);
		}

		public static void SolveSingle_Rigidbodies(ref CollisionInfo info, Motion[] motionsPtr, ContactSolverSettings settings, ref CollisionInfo nextInfo)
		{
			SolveOneCollision_AnyShape(ref info, ref motionsPtr[info.motionIdx0], ref motionsPtr[info.motionIdx1], settings, ref nextInfo);
		}

		private static void SolveOneCollision_Friction(ref CollisionInfo info, ref Motion motion0, ref Motion motion1, ContactSolverSettings settings)
		{
			info.GetPointVelocityAtLocalContactPoints_Tangent(in motion0, in motion1, out var velTangent);
			velTangent -= info.tmpRelativeSurfaceVelocity;
			float num = (0f - velTangent) * info.virtualMassTangent * settings.frictionDamping;
			bool usePrevFramesImpulseForCappingFriction = settings.usePrevFramesImpulseForCappingFriction;
			ref ContactPointCache reference = ref info.cacheValue.pointCache0;
			if (info.featureIdxInCache == 1)
			{
				reference = ref info.cacheValue.pointCache1;
			}
			float num2 = ((usePrevFramesImpulseForCappingFriction && 3 <= reference.numFramesWithNonZeroImpulse) ? info.sumFullImpulses_PrevFrame : info.sumFullImpulses_InFrame);
			num2 *= info.friction;
			if (settings.usePositionBasedFriction)
			{
				if (reference.numFramesWithNonZeroImpulse <= 3)
				{
					reference.refSurfaceDistance = 0f;
					reference.persistent_refSurfaceDistance = 0f;
				}
				float num3 = 0f;
				float num4 = (0f - (0f - reference.persistent_refSurfaceDistance)) * info.virtualMassTangent * settings.frictionTau;
				float num5 = ((0f <= num4) ? num4 : (0f - num4));
				if (num4 != 0f && num2 < num5)
				{
					float num6 = num2 / num5;
					num6 = UnityEngine.Mathf.Min(1f, num6 * settings.posErrorClampingMultiplier);
					reference.persistent_refSurfaceDistance *= num6;
				}
				num3 = (0f - (0f - reference.refSurfaceDistance)) * info.virtualMassTangent * settings.frictionTau;
				float num7 = ((0f <= num3) ? num3 : (0f - num3));
				if (num3 != 0f && num2 < num7)
				{
					float num8 = num2 / num7;
					num8 = UnityEngine.Mathf.Min(1f, num8 * settings.posErrorClampingMultiplier);
					num3 *= num8;
					reference.refSurfaceDistance *= num8;
				}
				num += num3;
				if (!settings.useVelocityFriction)
				{
					num = num3;
				}
			}
			else if (!settings.useVelocityFriction)
			{
				num = 0f;
			}
			float num9 = 0f - num2 - info.sumFrictionImpulses_InFrame;
			float num10 = num2 - info.sumFrictionImpulses_InFrame;
			num = ((num < num9) ? num9 : ((num10 < num) ? num10 : num));
			info.ApplyImpulse_Tangent(num, ref motion0, ref motion1);
			info.sumFrictionImpulses_InFrame += num;
			info.frictionImpulse_SinceIntegration += num;
		}

		public static void SolveOneCollision_AnyShape(ref CollisionInfo info, ref Motion motion0, ref Motion motion1, ContactSolverSettings settings, ref CollisionInfo nextInfo)
		{
			if (info.doFriction && settings.enableFriction)
			{
				SolveOneCollision_Friction(ref info, ref motion0, ref motion1, settings);
				if (info.hasSecondPoint && nextInfo.minDistanceInFrame < info.maxContactPointDistance_experiment)
				{
					SolveOneCollision_Friction(ref nextInfo, ref motion0, ref motion1, settings);
					nextInfo.doFriction = false;
				}
			}
			info.GetPointVelocityAtLocalContactPoints_Normal(in motion0, in motion1, out var velNormal);
			info.lastVelError_forImpulseEstimation = float.MaxValue;
			info.lastPosError_forImpulseEstimation = 0f;
			float num = 0f;
			float num2 = (settings.useContactWarmstarting ? 0.01f : 0.01f);
			if (!settings.useTauOffset)
			{
				num2 = 0f;
			}
			if (0f + num2 < info.distance)
			{
				info.lastVelError_forImpulseEstimation = velNormal;
				info.lastPosError_forImpulseEstimation = info.distance - num2;
				velNormal += info.distance - num2;
			}
			else
			{
				num = ((info.distance < info.referencePenetrationDistance) ? (info.distance - info.referencePenetrationDistance) : 0f);
				num = ((!settings.highSpeedBlend_forPosition) ? UnityEngine.Mathf.Max(num, 0f - settings.maxPosError) : UnityEngine.Mathf.Max(num, (0f - settings.maxPosError) * info.oneLess_highSpeedFactor));
				if (!settings.legacy_convertPosErrorToVelError && !settings.integrateInSolverIterations)
				{
					if (!debugOnce_01)
					{
						debugOnce_01 = true;
					}
					num /= (float)settings.numIterations;
				}
			}
			if (settings.highSpeedBlend_forVelocity)
			{
				float num3 = UnityEngine.Mathf.LerpUnclamped(0.01f, 1f, info.oneLess_highSpeedFactor);
				velNormal *= num3;
			}
			float num4 = (0f - num) * info.virtualMass * settings.tau;
			float num5 = (0f - velNormal) * info.virtualMass * settings.damping;
			float num6 = (settings.trackContactImpulseThroughFrame ? (0f - info.sumVelImpulses_InFrame) : (0f - info.velImpulse_SinceIntegration));
			num6 = (settings.trackContactImpulseThroughFrame ? (0f - info.sumFullImpulses_InFrame) : (0f - info.fullImpulse_SinceIntegration));
			float num7 = num4 + num5;
			if (num7 < num6)
			{
				num7 = num6;
				num5 = num7 - num4;
			}
			info.ApplyImpulse_Normal(num7, ref motion0, ref motion1);
			info.sumVelImpulses_InFrame += num5;
			info.sumFullImpulses_InFrame += num7;
			info.velImpulse_SinceIntegration += num5;
			info.fullImpulse_SinceIntegration += num7;
		}

		public static void SolveSingle_UpdateParticleVelocityForSegments(ref Motion motion, in Motion oldMotion, SolverNode[] nodesPtr, ContactSolverSettings settings, float motionToNodeVelocityMultiplier)
		{
			if (motion.invMass != 0f || motion.invInertia != 0f)
			{
				if (0.024674013f < motion.angVel * motion.angVel)
				{
					float a = ((-MathF.PI / 20f < oldMotion.angVel) ? (-MathF.PI / 20f) : oldMotion.angVel);
					float b = ((oldMotion.angVel < MathF.PI / 20f) ? (MathF.PI / 20f) : oldMotion.angVel);
					motion.angVel = Pb.Mathf.Clamp(motion.angVel, a, b);
					motion.angVel = Pb.Mathf.Clamp(motion.angVel, MathF.PI * -3f / 40f, MathF.PI * 3f / 40f);
				}
				Vec2 dCom = motion.linVel - oldMotion.linVel;
				float dAngle = motion.angVel - oldMotion.angVel;
				motion.UpdateNodeVelocities_SegmentFast(dCom, dAngle, nodesPtr, motionToNodeVelocityMultiplier);
			}
		}

		public static void DrawNormalAndDistance(ref CollisionInfo info, bool isRejected = false, bool useContactPoint1 = false)
		{
			GlDrawer.color = (isRejected ? Color.red : Color.yellow);
			GlDrawer.DrawLine(info.contactPoint0, info.contactPoint1);
			Vec2 rotated = info.normal.rotated90;
			Vec2 vec = (useContactPoint1 ? info.contactPoint1 : info.contactPoint0);
			GlDrawer.DrawLine(vec - rotated * 0.5f, vec + rotated * 0.5f);
		}

		public static void SolveContacts_PostProjection(CollisionInfo[] collisions, int numCollisions, Motion[] motionsPtr, SolverNode[] nodesPtr, SolverSettings solverSettings, bool isLastFrame)
		{
			for (int i = 0; i < numCollisions; i++)
			{
				ref CollisionInfo reference = ref collisions[i];
				ContactSolverSettings contactSolverSettings = reference.contactSolverSettings;
				if (!contactSolverSettings.useContactWarmstarting || (!isLastFrame && !contactSolverSettings.runPostProjectionInCollisionOnEveryIntegration) || contactSolverSettings.posTau == 0f)
				{
					continue;
				}
				switch (reference.entityTypes)
				{
				case EntityTypes.BodyEdge:
					Motion.ConvertNodesToMotion_InSolver_ComOnly(reference.motionIdx1, nodesPtr, motionsPtr, contactSolverSettings.nodeToMotionVelocityMultiplier);
					break;
				case EntityTypes.EdgeNode:
					Motion.ConvertNodesToMotion_InSolver_ComOnly(reference.motionIdx0, nodesPtr, motionsPtr, contactSolverSettings.nodeToMotionVelocityMultiplier);
					break;
				case EntityTypes.EdgeEdge:
					Motion.ConvertNodesToMotion_InSolver_ComOnly(reference.motionIdx0, nodesPtr, motionsPtr, contactSolverSettings.nodeToMotionVelocityMultiplier);
					Motion.ConvertNodesToMotion_InSolver_ComOnly(reference.motionIdx1, nodesPtr, motionsPtr, contactSolverSettings.nodeToMotionVelocityMultiplier);
					break;
				}
				float distance = reference.distance;
				RecomputeDistance(ref reference, motionsPtr, nodesPtr, updateMinDistance: false);
				if (reference.distance < 0f)
				{
					switch (reference.entityTypes)
					{
					case EntityTypes.BodyBody:
						PostProjectSingle_Rigidbodies(ref reference, motionsPtr, contactSolverSettings);
						break;
					case EntityTypes.BodyEdge:
						PostProjectSingle_RigidbodyVsSegment(ref reference, nodesPtr, motionsPtr, contactSolverSettings, contactSolverSettings.motionToNodeVelocityMultiplier);
						break;
					case EntityTypes.BodyNode:
						PostProjectSingle_RigidbodyVsParticle(ref reference, nodesPtr, motionsPtr, contactSolverSettings, contactSolverSettings.motionToNodeVelocityMultiplier);
						break;
					case EntityTypes.EdgeNode:
						PostProjectSingle_SegmentVsParticle(ref reference, nodesPtr, motionsPtr, contactSolverSettings, contactSolverSettings.motionToNodeVelocityMultiplier);
						break;
					case EntityTypes.EdgeEdge:
						PostProjectSingle_TwoSegments(ref reference, nodesPtr, motionsPtr, contactSolverSettings, contactSolverSettings.motionToNodeVelocityMultiplier);
						break;
					}
				}
				reference.distance = distance;
			}
		}

		public static void PostProjectSingle_Rigidbodies(ref CollisionInfo info, Motion[] motionsPtr, ContactSolverSettings settings)
		{
			PostProjectOneCollision_AnyShape(ref info, ref motionsPtr[info.motionIdx0], ref motionsPtr[info.motionIdx1], settings);
		}

		public static void PostProjectSingle_RigidbodyVsParticle(ref CollisionInfo info, SolverNode[] nodesPtr, Motion[] motionsPtr, ContactSolverSettings settings, float motionToNodeVelocityMultiplier)
		{
			ref Motion motion = ref motionsPtr[info.motionIdx0];
			Motion.ComputeFromNode(in nodesPtr[info.nodeIdx1], out var result, settings.nodeToMotionVelocityMultiplier);
			PostProjectOneCollision_AnyShape(ref info, ref motion, ref result, settings);
			nodesPtr[info.nodeIdx1].pos = result.com;
		}

		public static void PostProjectSingle_SegmentVsParticle(ref CollisionInfo info, SolverNode[] nodesPtr, Motion[] motionsPtr, ContactSolverSettings settings, float motionToNodeVelocityMultiplier)
		{
			ref Motion reference = ref motionsPtr[info.motionIdx0];
			Motion oldMotion = reference;
			Motion.ComputeFromNode(in nodesPtr[info.nodeIdx1], out var result, settings.nodeToMotionVelocityMultiplier);
			PostProjectOneCollision_AnyShape(ref info, ref reference, ref result, settings);
			SolveSingle_UpdateParticlePositionForSegments(in reference, in oldMotion, nodesPtr, settings, motionToNodeVelocityMultiplier);
			nodesPtr[info.nodeIdx1].pos = result.com;
		}

		public static void PostProjectSingle_RigidbodyVsSegment(ref CollisionInfo info, SolverNode[] nodesPtr, Motion[] motionsPtr, ContactSolverSettings settings, float motionToNodeVelocityMultiplier)
		{
			ref Motion motion = ref motionsPtr[info.motionIdx0];
			ref Motion reference = ref motionsPtr[info.motionIdx1];
			Motion oldMotion = reference;
			PostProjectOneCollision_AnyShape(ref info, ref motion, ref reference, settings);
			SolveSingle_UpdateParticlePositionForSegments(in reference, in oldMotion, nodesPtr, settings, motionToNodeVelocityMultiplier);
		}

		public static void PostProjectSingle_TwoSegments(ref CollisionInfo info, SolverNode[] nodesPtr, Motion[] motionsPtr, ContactSolverSettings settings, float motionToNodeVelocityMultiplier)
		{
			ref Motion reference = ref motionsPtr[info.motionIdx0];
			ref Motion reference2 = ref motionsPtr[info.motionIdx1];
			Motion oldMotion = reference;
			Motion oldMotion2 = reference2;
			PostProjectOneCollision_AnyShape(ref info, ref reference, ref reference2, settings);
			SolveSingle_UpdateParticlePositionForSegments(in reference, in oldMotion, nodesPtr, settings, motionToNodeVelocityMultiplier);
			SolveSingle_UpdateParticlePositionForSegments(in reference2, in oldMotion2, nodesPtr, settings, motionToNodeVelocityMultiplier);
		}

		public static void PostProjectOneCollision_AnyShape(ref CollisionInfo info, ref Motion motion0, ref Motion motion1, ContactSolverSettings settings)
		{
			float num = info.distance;
			if (num < 0f - settings.maxPositionCorrection)
			{
				num = 0f - settings.maxPositionCorrection;
			}
			float posImpulse = (0f - num) * info.virtualMass * settings.posTau;
			info.ApplyPositionCorrection_Normal(posImpulse, ref motion0, ref motion1);
		}

		public static void SolveSingle_UpdateParticlePositionForSegments(in Motion motion, in Motion oldMotion, SolverNode[] nodesPtr, ContactSolverSettings settings, float motionToNodeVelocityMultiplier)
		{
			if (motion.invMass != 0f || motion.invInertia != 0f)
			{
				Vec2 dCom = motion.com - oldMotion.com;
				float dAngle = motion.angle - oldMotion.angle;
				motion.UpdateNodePositions_SegmentFast(dCom, dAngle, nodesPtr);
			}
		}

		public static bool SolveDouble_Rigidbodies(ref CollisionInfo info0, ref CollisionInfo info1, Motion[] motionsPtr, ContactSolverSettings settings)
		{
			return SolveTwoCollisions_AnyShape(ref info0, ref info1, ref motionsPtr[info0.motionIdx0], ref motionsPtr[info0.motionIdx1], settings);
		}

		public static bool SolveTwoCollisions_AnyShape(ref CollisionInfo info0, ref CollisionInfo info1, ref Motion motion0, ref Motion motion1, ContactSolverSettings settings)
		{
			if (settings.useCentralFriction)
			{
				CollisionInfo info2 = ((info0.cacheValue.closestFeatureIdx == 0) ? info0 : info1);
				CollisionInfo collisionInfo = ((info0.cacheValue.closestFeatureIdx == 0) ? info1 : info0);
				if (info2.doFriction && settings.enableFriction)
				{
					if (Vec2.Dot(in info2.normal, in collisionInfo.normal) < 0.866f)
					{
						SolveOneCollision_Friction(ref info2, ref motion0, ref motion1, settings);
					}
					else
					{
						float num = collisionInfo.sumVelImpulses_PrevFrame / (info2.sumVelImpulses_PrevFrame + collisionInfo.sumVelImpulses_PrevFrame + 5.877472E-39f);
						info2.deltaAngle0ToDeltaDistance_Tangent = UnityEngine.Mathf.Lerp(info2.deltaAngle0ToDeltaDistance_Tangent, collisionInfo.deltaAngle0ToDeltaDistance_Tangent, num);
						info2.deltaAngle1ToDeltaDistance_Tangent = UnityEngine.Mathf.Lerp(info2.deltaAngle1ToDeltaDistance_Tangent, collisionInfo.deltaAngle1ToDeltaDistance_Tangent, num);
						info2.virtualMassTangent = UnityEngine.Mathf.Lerp(info2.virtualMassTangent, collisionInfo.virtualMassTangent, num);
						info2.sumVelImpulses_InFrame += collisionInfo.sumVelImpulses_InFrame;
						info2.sumFrictionImpulses_InFrame += collisionInfo.sumFrictionImpulses_InFrame;
						info2.sumFullImpulses_InFrame += collisionInfo.sumFullImpulses_InFrame;
						info2.sumVelImpulses_PrevFrame += collisionInfo.sumVelImpulses_PrevFrame;
						SolveOneCollision_Friction(ref info2, ref motion0, ref motion1, settings);
						float num2 = ((info0.cacheValue.closestFeatureIdx == 0) ? (1f - num) : num);
						float num3 = 1f - num2;
						info0.sumFrictionImpulses_InFrame = num2 * info2.sumFrictionImpulses_InFrame;
						info1.sumFrictionImpulses_InFrame = num3 * info2.sumFrictionImpulses_InFrame;
						info0.frictionImpulse_SinceIntegration = num2 * info2.frictionImpulse_SinceIntegration;
						info1.frictionImpulse_SinceIntegration = num3 * info2.frictionImpulse_SinceIntegration;
					}
				}
			}
			else
			{
				if (info0.doFriction && settings.enableFriction)
				{
					SolveOneCollision_Friction(ref info0, ref motion0, ref motion1, settings);
				}
				if (info1.doFriction && settings.enableFriction)
				{
					SolveOneCollision_Friction(ref info1, ref motion0, ref motion1, settings);
				}
			}
			Vec2 vec = default(Vec2);
			vec.x = COPY_TEMP_GetError(ref info0, ref motion0, ref motion1, settings);
			vec.y = COPY_TEMP_GetError(ref info1, ref motion0, ref motion1, settings);
			Vec2 vec2 = -info0.normal * motion0.invMass;
			Vec2 vec3 = info0.normal * motion1.invMass;
			float num4 = (0f - info0.deltaAngle0ToDeltaDistance) * motion0.invInertia;
			float num5 = info0.deltaAngle1ToDeltaDistance * motion1.invInertia;
			Vec2 vec4 = -info1.normal * motion0.invMass;
			Vec2 vec5 = info1.normal * motion1.invMass;
			float num6 = (0f - info1.deltaAngle0ToDeltaDistance) * motion0.invInertia;
			float num7 = info1.deltaAngle1ToDeltaDistance * motion1.invInertia;
			Mtx22 mtx = default(Mtx22);
			mtx.m00 = Vec2.Dot(vec3 - vec2, in info0.normal) + info0.deltaAngle1ToDeltaDistance * num5 - info0.deltaAngle0ToDeltaDistance * num4;
			mtx.m01 = Vec2.Dot(vec5 - vec4, in info0.normal) + info0.deltaAngle1ToDeltaDistance * num7 - info0.deltaAngle0ToDeltaDistance * num6;
			mtx.m10 = Vec2.Dot(vec3 - vec2, in info1.normal) + info1.deltaAngle1ToDeltaDistance * num5 - info1.deltaAngle0ToDeltaDistance * num4;
			mtx.m11 = Vec2.Dot(vec5 - vec4, in info1.normal) + info1.deltaAngle1ToDeltaDistance * num7 - info1.deltaAngle0ToDeltaDistance * num6;
			Vec2 vec6 = (settings.trackContactImpulseThroughFrame ? new Vec2(0f - info0.sumVelImpulses_InFrame, 0f - info1.sumVelImpulses_InFrame) : Vec2.zero);
			vec += mtx * vec6;
			Vec2 zero = Vec2.zero;
			bool flag = false;
			if (0f <= vec.x && 0f <= vec.y)
			{
				zero = Vec2.zero;
				flag = true;
				twoPointSolverCaseCounts[0]++;
			}
			else
			{
				float num8 = mtx.m00 * mtx.m11 - mtx.m10 * mtx.m01;
				if (0.0001f <= UnityEngine.Mathf.Abs(num8))
				{
					float num9 = (0f - vec.x) * mtx.m11 - (0f - vec.y) * mtx.m01;
					float num10 = mtx.m00 * (0f - vec.y) - mtx.m10 * (0f - vec.x);
					float num11 = num9 / num8;
					float num12 = num10 / num8;
					float f = mtx.m00 * num11 + mtx.m01 * num12 + vec.x;
					float f2 = mtx.m10 * num11 + mtx.m11 * num12 + vec.y;
					if (UnityEngine.Mathf.Abs(f) < 0.01f)
					{
						UnityEngine.Mathf.Abs(f2);
						_ = 0.01f;
					}
					if (-0f <= num11 && -0f <= num12)
					{
						zero.x = num11;
						zero.y = num12;
						flag = true;
						twoPointSolverCaseCounts[1]++;
						goto IL_059a;
					}
				}
				float num13 = (0f - vec.x) / mtx.m00;
				float num14 = mtx.m10 * num13 + vec.y;
				if (-0f <= num14 && -0f <= num13)
				{
					zero.x = num13;
					zero.y = 0f;
					flag = true;
					twoPointSolverCaseCounts[2]++;
				}
				else
				{
					float num15 = (0f - vec.y) / mtx.m11;
					float num16 = mtx.m01 * num15 + vec.x;
					if (-0f <= num16 && -0f <= num15)
					{
						zero.x = 0f;
						zero.y = num15;
						flag = true;
						twoPointSolverCaseCounts[3]++;
					}
				}
			}
			goto IL_059a;
			IL_059a:
			if (flag)
			{
				zero += vec6;
				info0.ApplyImpulse_Normal(zero.x, ref motion0, ref motion1);
				info1.ApplyImpulse_Normal(zero.y, ref motion0, ref motion1);
				info0.sumVelImpulses_InFrame += zero.x;
				info0.sumFullImpulses_InFrame += zero.x;
				info1.sumVelImpulses_InFrame += zero.y;
				info1.sumFullImpulses_InFrame += zero.y;
				info0.velImpulse_SinceIntegration += zero.x;
				info1.velImpulse_SinceIntegration += zero.y;
				info0.fullImpulse_SinceIntegration += zero.x;
				info1.fullImpulse_SinceIntegration += zero.y;
			}
			return flag;
		}

		public static float COPY_TEMP_GetError(ref CollisionInfo info, ref Motion motion0, ref Motion motion1, ContactSolverSettings settings)
		{
			info.GetPointVelocityAtLocalContactPoints_Normal(in motion0, in motion1, out var velNormal);
			info.lastVelError_forImpulseEstimation = float.MaxValue;
			info.lastPosError_forImpulseEstimation = 0f;
			float num = 0f;
			float num2 = (settings.useContactWarmstarting ? 0.01f : 0.01f);
			if (!settings.useTauOffset)
			{
				num2 = 0f;
			}
			if (0f + num2 < info.distance)
			{
				info.lastVelError_forImpulseEstimation = velNormal;
				info.lastPosError_forImpulseEstimation = info.distance - num2;
				velNormal += info.distance - num2;
			}
			else
			{
				num = ((info.distance < info.referencePenetrationDistance) ? (info.distance - info.referencePenetrationDistance) : 0f);
				if (settings.legacy_convertPosErrorToVelError)
				{
					velNormal += num * settings.tau / settings.damping;
					num = 0f;
				}
				else if (!settings.integrateInSolverIterations)
				{
					num /= (float)settings.numIterations;
				}
			}
			return velNormal;
		}

		public static void WarmstartSingle_Rigidbodies(ref CollisionInfo info, Motion[] motionsPtr, ContactSolverSettings settings)
		{
			WarmstartOneCollision_AnyShape(ref info, ref motionsPtr[info.motionIdx0], ref motionsPtr[info.motionIdx1], settings);
		}

		public static void WarmstartSingle_RigidbodyVsParticle(ref CollisionInfo info, SolverNode[] nodesPtr, Motion[] motionsPtr, ContactSolverSettings settings, float motionToNodeVelocityMultiplier)
		{
			ref Motion motion = ref motionsPtr[info.motionIdx0];
			Motion.ComputeFromNode(in nodesPtr[info.nodeIdx1], out var result, settings.nodeToMotionVelocityMultiplier);
			WarmstartOneCollision_AnyShape(ref info, ref motion, ref result, settings);
			nodesPtr[info.nodeIdx1].vel = result.linVel * settings.motionToNodeVelocityMultiplier;
		}

		public static void WarmstartSingle_SegmentVsParticle(ref CollisionInfo info, SolverNode[] nodesPtr, Motion[] motionsPtr, ContactSolverSettings settings, float motionToNodeVelocityMultiplier)
		{
			ref Motion reference = ref motionsPtr[info.motionIdx0];
			Motion oldMotion = reference;
			Motion.ComputeFromNode(in nodesPtr[info.nodeIdx1], out var result, settings.nodeToMotionVelocityMultiplier);
			WarmstartOneCollision_AnyShape(ref info, ref reference, ref result, settings);
			SolveSingle_UpdateParticleVelocityForSegments(ref reference, in oldMotion, nodesPtr, settings, motionToNodeVelocityMultiplier);
			nodesPtr[info.nodeIdx1].vel = result.linVel * settings.motionToNodeVelocityMultiplier;
		}

		public static void WarmstartSingle_RigidbodyVsSegment(ref CollisionInfo info, SolverNode[] nodesPtr, Motion[] motionsPtr, ContactSolverSettings settings, float motionToNodeVelocityMultiplier)
		{
			ref Motion motion = ref motionsPtr[info.motionIdx0];
			ref Motion reference = ref motionsPtr[info.motionIdx1];
			Motion oldMotion = reference;
			WarmstartOneCollision_AnyShape(ref info, ref motion, ref reference, settings);
			SolveSingle_UpdateParticleVelocityForSegments(ref reference, in oldMotion, nodesPtr, settings, motionToNodeVelocityMultiplier);
		}

		public static void WarmstartSingle_TwoSegments(ref CollisionInfo info, SolverNode[] nodesPtr, Motion[] motionsPtr, ContactSolverSettings settings, float motionToNodeVelocityMultiplier)
		{
			ref Motion reference = ref motionsPtr[info.motionIdx0];
			ref Motion reference2 = ref motionsPtr[info.motionIdx1];
			Motion oldMotion = reference;
			Motion oldMotion2 = reference2;
			WarmstartOneCollision_AnyShape(ref info, ref reference, ref reference2, settings);
			SolveSingle_UpdateParticleVelocityForSegments(ref reference, in oldMotion, nodesPtr, settings, motionToNodeVelocityMultiplier);
			SolveSingle_UpdateParticleVelocityForSegments(ref reference2, in oldMotion2, nodesPtr, settings, motionToNodeVelocityMultiplier);
		}

		public static void WarmstartOneCollision_AnyShape(ref CollisionInfo info, ref Motion motion0, ref Motion motion1, ContactSolverSettings settings)
		{
			if (info.velImpulse_SinceIntegration < 0f)
			{
				info.velImpulse_SinceIntegration = 0f;
			}
			info.velImpulse_SinceIntegration *= settings.contactWarmstartingRatio;
			info.fullImpulse_SinceIntegration = info.velImpulse_SinceIntegration;
			info.frictionImpulse_SinceIntegration *= settings.frictionWarmstartingRatio;
			info.ApplyImpulse_Normal(info.velImpulse_SinceIntegration, ref motion0, ref motion1);
			info.ApplyImpulse_Tangent(info.frictionImpulse_SinceIntegration, ref motion0, ref motion1);
			info.sumVelImpulses_InFrame += info.velImpulse_SinceIntegration;
			info.sumFullImpulses_InFrame += info.velImpulse_SinceIntegration;
			info.sumFrictionImpulses_InFrame += info.frictionImpulse_SinceIntegration;
		}
	}
}
