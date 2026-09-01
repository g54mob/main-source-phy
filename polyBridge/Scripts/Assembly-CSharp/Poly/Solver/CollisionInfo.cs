using System;
using System.Runtime.CompilerServices;
using Poly.Base;
using Poly.Collide;
using Poly.Physics;
using UnityEngine;

namespace Poly.Solver
{
	public struct CollisionInfo
	{
		public EntityTypes entityTypes;

		public short motionIdx0;

		public short motionIdx1;

		public ContactSolverSettings contactSolverSettings;

		public float maxContactPointDistance_experiment;

		public Vec2 normal;

		public float deltaAngle0ToDeltaDistance;

		public float deltaAngle1ToDeltaDistance;

		public float deltaAngle0ToDeltaDistance_Tangent;

		public float deltaAngle1ToDeltaDistance_Tangent;

		public float distance;

		public float minDistanceInFrame;

		public float referencePenetrationDistance;

		public float distanceOffset;

		public float comOffsetX;

		public float comOffsetY;

		public float angleOffsetA;

		public float angleOffsetB;

		public float frictionSurfaceDistanceOffset;

		public byte featureIdxInCache;

		public bool doFriction;

		public bool hasSecondPoint;

		public bool onlyAIsWheel;

		public bool onlyBIsWheel;

		public float friction;

		public int cacheIndex;

		public CollisionCache cacheValue;

		public float tmpRelativeSurfaceVelocity;

		public float virtualMass;

		public float virtualMassTangent;

		public float velImpulse_SinceIntegration;

		public float fullImpulse_SinceIntegration;

		public float frictionImpulse_SinceIntegration;

		public float sumVelImpulses_InFrame;

		public float sumFrictionImpulses_InFrame;

		public float sumVelImpulses_PrevFrame;

		public float sumFullImpulses_PrevFrame;

		public float sumFrictionImpulses_PrevFrame;

		public float sumFullImpulses_InFrame;

		public float lastVelError_forImpulseEstimation;

		public float lastPosError_forImpulseEstimation;

		public float oneLess_highSpeedFactor;

		public float initialSqrVel_relative;

		public float initialSqrVel_secondBody;

		public float invMass0_scale;

		public float invMass1_scale;

		public float invInertia0_scale;

		public float invInertia1_scale;

		public bool isDynamicRoad;

		public bool isDynamicRoadVsDynamicRigidbody;

		public bool debug_highSpeedVelocitiesRecorded;

		public Vec2 contactPoint0;

		public Vec2 contactPoint1;

		public int collisionEventIdx;

		public int collisionEventPointIdx;

		public bool isReversed;

		public short shapeHandleIdx0;

		public short shapeHandleIdx1;

		public CollisionType debug_type;

		private static int _numNoEffects_temp;

		private static int _numAngularSegments_temp;

		private const bool graduallyDecreaseHighSpeedBlend = false;

		public Vec2 tangent_slow => normal.rotated90;

		public float bounciness
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public short nodeIdx0 => motionIdx0;

		public short nodeIdx1 => motionIdx1;

		public void RecalculateDistanceRB(Motion[] motionsPtr)
		{
			ref Motion reference = ref motionsPtr[motionIdx0];
			ref Motion reference2 = ref motionsPtr[motionIdx1];
			float num = reference2.com.x - reference.com.x - comOffsetX;
			float num2 = reference2.com.y - reference.com.y - comOffsetY;
			float num3 = reference.angle - angleOffsetA;
			float num4 = reference2.angle - angleOffsetB;
			float num5 = normal.x * num + normal.y * num2 + deltaAngle1ToDeltaDistance * num4 - deltaAngle0ToDeltaDistance * num3;
			distance = distanceOffset + num5;
			Vec2 vec = tangent_slow;
			float num6 = vec.x * num + vec.y * num2 + deltaAngle1ToDeltaDistance_Tangent * num4 - deltaAngle0ToDeltaDistance_Tangent * num3;
			float num7 = frictionSurfaceDistanceOffset + num6;
			if (featureIdxInCache == 0)
			{
				cacheValue.pointCache0.refSurfaceDistance = 0f - num7;
			}
			else
			{
				cacheValue.pointCache1.refSurfaceDistance = 0f - num7;
			}
		}

		public void RecalculateDistanceRB_InCollide(in Motion motion0, in Motion motion1)
		{
			float num = motion1.com.x - motion0.com.x - comOffsetX;
			float num2 = motion1.com.y - motion0.com.y - comOffsetY;
			float num3 = motion0.angle - angleOffsetA;
			float num4 = motion1.angle - angleOffsetB;
			float num5 = normal.x * num + normal.y * num2 + deltaAngle1ToDeltaDistance * num4 - deltaAngle0ToDeltaDistance * num3;
			distance = distanceOffset + num5;
			Vec2 vec = tangent_slow;
			float num6 = vec.x * num + vec.y * num2 + deltaAngle1ToDeltaDistance_Tangent * num4 - deltaAngle0ToDeltaDistance_Tangent * num3;
			float num7 = frictionSurfaceDistanceOffset + num6;
			if (featureIdxInCache == 0)
			{
				cacheValue.pointCache0.refSurfaceDistance = 0f - num7;
			}
			else
			{
				cacheValue.pointCache1.refSurfaceDistance = 0f - num7;
			}
		}

		public void RecalculateDistanceRB_Node1(Motion[] motionsPtr, SolverNode[] nodesPtr)
		{
			ref Motion reference = ref motionsPtr[motionIdx0];
			ref SolverNode reference2 = ref nodesPtr[nodeIdx1];
			float num = reference2.pos.x - reference.com.x - comOffsetX;
			float num2 = reference2.pos.y - reference.com.y - comOffsetY;
			float num3 = reference.angle - angleOffsetA;
			float num4 = normal.x * num + normal.y * num2 - deltaAngle0ToDeltaDistance * num3;
			distance = distanceOffset + num4;
			Vec2 vec = tangent_slow;
			float num5 = vec.x * num + vec.y * num2 - deltaAngle0ToDeltaDistance_Tangent * num3;
			float num6 = frictionSurfaceDistanceOffset + num5;
			if (featureIdxInCache == 0)
			{
				cacheValue.pointCache0.refSurfaceDistance = 0f - num6;
			}
			else
			{
				cacheValue.pointCache1.refSurfaceDistance = 0f - num6;
			}
		}

		public void RecalculateDistanceRB_Node0_Node1(SolverNode[] nodesPtr)
		{
			ref SolverNode reference = ref nodesPtr[nodeIdx0];
			ref SolverNode reference2 = ref nodesPtr[nodeIdx1];
			float num = reference2.pos.x - reference.pos.x - comOffsetX;
			float num2 = reference2.pos.y - reference.pos.y - comOffsetY;
			float num3 = normal.x * num + normal.y * num2;
			distance = distanceOffset + num3;
			Vec2 vec = tangent_slow;
			float num4 = vec.x * num + vec.y * num2;
			float num5 = frictionSurfaceDistanceOffset + num4;
			if (featureIdxInCache == 0)
			{
				cacheValue.pointCache0.refSurfaceDistance = 0f - num5;
			}
			else
			{
				cacheValue.pointCache1.refSurfaceDistance = 0f - num5;
			}
		}

		public void CalcFrictionBouncinessAndSurfaceVelocity(Shape shape0, Shape shape1, float deltaTimeForVelocity, float globalFrictionCap)
		{
			friction = Mathf.Sqrt(shape0.friction * shape1.friction);
			bounciness = Mathf.Sqrt(shape0.bounciness * shape1.bounciness);
			tmpRelativeSurfaceVelocity = (shape0.tmpSurfaceVelocity + shape1.tmpSurfaceVelocity) * deltaTimeForVelocity;
			friction = Mathf.Min(friction, globalFrictionCap);
		}

		public void ComputeCollisionInfo2D_VirtualMass(in Motion motion0, in Motion motion1, bool unwrapCode)
		{
			float num6;
			float num9;
			if (unwrapCode)
			{
				float num = 0f - normal.y;
				float x = normal.x;
				Vec2 vec = default(Vec2);
				vec.x = contactPoint0.x - motion0.com.x;
				vec.y = contactPoint0.y - motion0.com.y;
				float num2 = vec.x * normal.y - vec.y * normal.x;
				num2 *= motion0.invInertia;
				Vec2 vec2 = default(Vec2);
				vec2.x = (0f - num2) * vec.y;
				vec2.y = num2 * vec.x;
				float num3 = vec.x * x - vec.y * num;
				num3 *= motion0.invInertia;
				Vec2 vec3 = default(Vec2);
				vec3.x = (0f - num3) * vec.y;
				vec3.y = num3 * vec.x;
				vec.x = contactPoint1.x - motion1.com.x;
				vec.y = contactPoint1.y - motion1.com.y;
				float num4 = vec.x * normal.y - vec.y * normal.x;
				num4 *= motion1.invInertia;
				Vec2 vec4 = default(Vec2);
				vec4.x = (0f - num4) * vec.y;
				vec4.y = num4 * vec.x;
				float num5 = normal.x * (vec2.x + vec4.x) + normal.y * (vec2.y + vec4.y);
				num6 = motion0.invMass + motion1.invMass + num5;
				float num7 = vec.x * x - vec.y * num;
				num7 *= motion1.invInertia;
				Vec2 vec5 = default(Vec2);
				vec5.x = (0f - num7) * vec.y;
				vec5.y = num7 * vec.x;
				float num8 = num * (vec3.x + vec5.x) + x * (vec3.y + vec5.y);
				num9 = motion0.invMass + motion1.invMass + num8;
			}
			else
			{
				Vec2 vec6 = tangent_slow;
				Vec2 vec7 = contactPoint0 - motion0.com;
				num6 = motion0.invMass + motion0.invInertia * Vector3.Dot(normal, Vector3.Cross(Vector3.Cross(vec7, normal), vec7));
				num9 = motion0.invMass + motion0.invInertia * Vector3.Dot(vec6, Vector3.Cross(Vector3.Cross(vec7, vec6), vec7));
				vec7 = contactPoint1 - motion1.com;
				num6 += motion1.invMass + motion1.invInertia * Vector3.Dot(normal, Vector3.Cross(Vector3.Cross(vec7, normal), vec7));
				num9 += motion1.invMass + motion1.invInertia * Vector3.Dot(vec6, Vector3.Cross(Vector3.Cross(vec7, vec6), vec7));
			}
			float num10 = 1E-12f;
			virtualMass = ((num6 > num10) ? (1f / num6) : 0f);
			virtualMassTangent = ((num9 > num10) ? (1f / num9) : 0f);
			if (motion0.invMass == 0f && motion1.invMass == 0f && (motion0.invInertia != 0f || motion0.invInertia != 0f))
			{
				if (_numAngularSegments_temp < 10)
				{
					_numAngularSegments_temp++;
					Debug.Log("Segments at angle in collision.");
				}
				virtualMass = 0f;
				virtualMassTangent = 0f;
			}
		}

		public void MaybeOnlyInitReferenceDepth()
		{
			ContactSolverSettings contactSolverSettings = this.contactSolverSettings;
			switch (featureIdxInCache)
			{
			case 0:
				if (!cacheValue.pointCache0.depthInitialized)
				{
					float num2 = 1f;
					cacheValue.pointCache0.depthInitialized = true;
					cacheValue.pointCache0.referencePenetrationDistance = Mathf.Clamp(distance, (0f - contactSolverSettings.maxReferencePenetration) * num2, 0f);
				}
				referencePenetrationDistance = cacheValue.pointCache0.referencePenetrationDistance;
				break;
			case 1:
				if (!cacheValue.pointCache1.depthInitialized)
				{
					float num = 1f;
					cacheValue.pointCache1.depthInitialized = true;
					cacheValue.pointCache1.referencePenetrationDistance = Mathf.Clamp(distance, (0f - contactSolverSettings.maxReferencePenetration) * num, 0f);
				}
				referencePenetrationDistance = cacheValue.pointCache1.referencePenetrationDistance;
				break;
			}
		}

		public void MaybeFirstTimeInit(SolverSettings settings)
		{
			if (cacheValue.oneLess_highSpeedFactor < 1f && featureIdxInCache == 0)
			{
				if (isDynamicRoadVsDynamicRigidbody)
				{
					float num = Mathf.Sqrt((initialSqrVel_relative < initialSqrVel_secondBody) ? initialSqrVel_relative : initialSqrVel_secondBody);
					float num2 = contactSolverSettings.highSpeedBlendRange.x * settings.deltaTimeForVelocity;
					float num3 = contactSolverSettings.highSpeedBlendRange.y * settings.deltaTimeForVelocity;
					float num4 = (num - num2) / (num3 - num2);
					float highSpeedBlend_maxBlendValue = contactSolverSettings.highSpeedBlend_maxBlendValue;
					num4 = ((num4 < 0f) ? 0f : ((highSpeedBlend_maxBlendValue < num4) ? highSpeedBlend_maxBlendValue : num4));
					if (cacheValue.oneLess_highSpeedFactor == 0f)
					{
						if (num4 != 0f)
						{
							cacheValue.oneLess_highSpeedFactor = 1f - num4;
							cacheValue.highSpeedBlendTimeLeft = contactSolverSettings.maxHighSpeedBlendDuration;
						}
						else
						{
							cacheValue.oneLess_highSpeedFactor = 1f;
							cacheValue.highSpeedBlendTimeLeft = 0f;
						}
					}
					else
					{
						float num5 = settings.frameDeltaTime / contactSolverSettings.highSpeedBlendCooldownDuration;
						cacheValue.oneLess_highSpeedFactor = Mathf.Min(cacheValue.oneLess_highSpeedFactor + num5, 1f);
						float num6 = 1f - num4;
						if (num6 < cacheValue.oneLess_highSpeedFactor)
						{
							cacheValue.oneLess_highSpeedFactor = num6;
						}
					}
				}
				else
				{
					cacheValue.oneLess_highSpeedFactor = 1f;
					cacheValue.highSpeedBlendTimeLeft = 0f;
				}
			}
			oneLess_highSpeedFactor = cacheValue.oneLess_highSpeedFactor;
		}

		public void MaybeOnlyUpdateReferenceDepth(float penetrationCorrectionPerFrame_Constant, float penetrationCorrectionPerFrame_Proportional, SolverSettings settings)
		{
			if (featureIdxInCache == 0)
			{
				if (cacheValue.oneLess_highSpeedFactor < 1f)
				{
					cacheValue.highSpeedBlendTimeLeft -= settings.frameDeltaTime;
					cacheValue.highSpeedBlendTimeLeft = ((cacheValue.highSpeedBlendTimeLeft < 0f) ? 0f : cacheValue.highSpeedBlendTimeLeft);
					if (0f == cacheValue.highSpeedBlendTimeLeft)
					{
						cacheValue.oneLess_highSpeedFactor = 1f;
					}
				}
				if (cacheValue.pointCache0.depthInitialized)
				{
					cacheValue.pointCache0.referencePenetrationDistance = Mathf.Max(cacheValue.pointCache0.referencePenetrationDistance * (1f - penetrationCorrectionPerFrame_Proportional) + penetrationCorrectionPerFrame_Constant, distance);
					cacheValue.pointCache0.referencePenetrationDistance = Mathf.Min(cacheValue.pointCache0.referencePenetrationDistance, 0f);
				}
				referencePenetrationDistance = cacheValue.pointCache0.referencePenetrationDistance;
			}
			else
			{
				if (cacheValue.pointCache1.depthInitialized)
				{
					cacheValue.pointCache1.referencePenetrationDistance = Mathf.Max(cacheValue.pointCache1.referencePenetrationDistance * (1f - penetrationCorrectionPerFrame_Proportional) + penetrationCorrectionPerFrame_Constant, distance);
					cacheValue.pointCache1.referencePenetrationDistance = Mathf.Min(cacheValue.pointCache1.referencePenetrationDistance, 0f);
				}
				referencePenetrationDistance = cacheValue.pointCache1.referencePenetrationDistance;
			}
			oneLess_highSpeedFactor = cacheValue.oneLess_highSpeedFactor;
		}

		public void ModifyReferenceDepth(int featureIdxOverride, float delta)
		{
			if (featureIdxOverride == 0)
			{
				if (cacheValue.pointCache0.depthInitialized && delta < cacheValue.pointCache0.referencePenetrationDistance)
				{
					cacheValue.pointCache0.referencePenetrationDistance = delta;
				}
			}
			else if (cacheValue.pointCache1.depthInitialized && delta < cacheValue.pointCache1.referencePenetrationDistance)
			{
				cacheValue.pointCache1.referencePenetrationDistance = delta;
			}
		}

		public void ResetReferenceDepth()
		{
			if (featureIdxInCache == 0)
			{
				cacheValue.pointCache0.depthInitialized = false;
			}
			else
			{
				cacheValue.pointCache1.depthInitialized = false;
			}
		}

		public void ZeroWarmstartingImpulses()
		{
			velImpulse_SinceIntegration = 0f;
			fullImpulse_SinceIntegration = 0f;
			frictionImpulse_SinceIntegration = 0f;
		}

		public void Assert_WarmstartingImpulsesAreZero()
		{
		}

		public static void ResetImpulses(ref CollisionInfo info, SolverSettings settings)
		{
			ref ContactPointCache pointCache = ref CollisionCache.GetPointCache(ref info.cacheValue, info.featureIdxInCache);
			info.sumVelImpulses_PrevFrame = pointCache.sumVelImpulses_PrevFrame;
			info.sumFullImpulses_PrevFrame = pointCache.sumFullImpulses_PrevFrame;
			info.sumFrictionImpulses_PrevFrame = pointCache.sumFrictionImpulses_PrevFrame;
			if (info.contactSolverSettings.useContactWarmstarting)
			{
				info.velImpulse_SinceIntegration = pointCache.velImpulse_SinceIntegration;
				info.fullImpulse_SinceIntegration = pointCache.fullImpulse_SinceIntegration;
				info.frictionImpulse_SinceIntegration = pointCache.frictionImpulse_SinceIntegration;
			}
			else
			{
				info.velImpulse_SinceIntegration = 0f;
				info.fullImpulse_SinceIntegration = 0f;
				info.velImpulse_SinceIntegration = 0f;
			}
		}

		public static void CacheValues(ref CollisionInfo info, in ShapeHandle shapeHandle0, in ShapeHandle shapeHandle1, SolverNode[] nodesPtr, Motion[] solverMotionsPtr, bool unwrapCode, float nodeToMotionVelocityMultiplier)
		{
			CacheValues_Step1_CopyOrCreateMotions(in shapeHandle0, in shapeHandle1, nodesPtr, solverMotionsPtr, out var motion, out var motion2, ref info, nodeToMotionVelocityMultiplier);
			CacheValues_Step2(ref info, in motion, in motion2, unwrapCode);
		}

		public static void CacheValues_MassChanger(ref CollisionInfo info, in ShapeHandle shapeHandle0, in ShapeHandle shapeHandle1, SolverNode[] nodesPtr, Motion[] solverMotionsPtr, bool unwrapCode, float nodeToMotionVelocityMultiplier)
		{
			CacheValues_Step1_CopyOrCreateMotions(in shapeHandle0, in shapeHandle1, nodesPtr, solverMotionsPtr, out var motion, out var motion2, ref info, nodeToMotionVelocityMultiplier);
			motion.invMass *= info.invMass0_scale;
			motion.invInertia *= info.invInertia0_scale;
			motion2.invMass *= info.invMass1_scale;
			motion2.invInertia *= info.invInertia1_scale;
			CacheValues_Step2(ref info, in motion, in motion2, unwrapCode);
		}

		public static (float, float) GetAngleFromMotions(in ShapeHandle shapeHandle0, in ShapeHandle shapeHandle1, Motion[] solverMotionsPtr)
		{
			bool num = 0 <= shapeHandle0.motionIdx;
			bool flag = 0 <= shapeHandle1.motionIdx;
			float item = (num ? solverMotionsPtr[shapeHandle0.motionIdx].angle : 0f);
			float item2 = (flag ? solverMotionsPtr[shapeHandle1.motionIdx].angle : 0f);
			return (item, item2);
		}

		public static void CacheValues_Step1_CopyOrCreateMotions(in ShapeHandle shapeHandle0, in ShapeHandle shapeHandle1, SolverNode[] nodesPtr, Motion[] solverMotionsPtr, out Motion motion0, out Motion motion1, ref CollisionInfo info, float nodeToMotionVelocityMultiplier)
		{
			bool flag = 0 <= shapeHandle0.motionIdx;
			bool flag2 = 0 <= shapeHandle1.motionIdx;
			info.motionIdx0 = (flag ? shapeHandle0.motionIdx : shapeHandle0.nodeIdx);
			info.motionIdx1 = (flag2 ? shapeHandle1.motionIdx : shapeHandle1.nodeIdx);
			if (flag)
			{
				motion0 = solverMotionsPtr[info.motionIdx0];
			}
			else
			{
				Motion.ComputeFromNode(in nodesPtr[info.nodeIdx0], out motion0, nodeToMotionVelocityMultiplier);
			}
			if (flag2)
			{
				motion1 = solverMotionsPtr[info.motionIdx1];
			}
			else
			{
				Motion.ComputeFromNode(in nodesPtr[info.nodeIdx1], out motion1, nodeToMotionVelocityMultiplier);
			}
		}

		public static void CacheValues_Step2(ref CollisionInfo info, in Motion motion0, in Motion motion1, bool unwrapCode)
		{
			Vec2.setSub(in info.contactPoint0, in motion0.com, out var v);
			Vec2.setSub(in info.contactPoint1, in motion1.com, out var v2);
			Vec2.setRotated90(in v, out var v3);
			Vec2.setRotated90(in v2, out var v4);
			info.deltaAngle0ToDeltaDistance = Vec2.Dot(in info.normal, in v3);
			info.deltaAngle1ToDeltaDistance = Vec2.Dot(in info.normal, in v4);
			Vec2.setRotated90(in info.normal, out var v5);
			info.deltaAngle0ToDeltaDistance_Tangent = Vec2.Dot(in v5, in v3);
			info.deltaAngle1ToDeltaDistance_Tangent = Vec2.Dot(in v5, in v4);
			info.comOffsetX = motion1.com.x - motion0.com.x;
			info.comOffsetY = motion1.com.y - motion0.com.y;
			info.angleOffsetA = motion0.angle;
			info.angleOffsetB = motion1.angle;
			info.distanceOffset = info.distance;
			float num = ((info.featureIdxInCache == 0) ? info.cacheValue.pointCache0.refSurfaceDistance : info.cacheValue.pointCache1.refSurfaceDistance);
			info.frictionSurfaceDistanceOffset = 0f - num;
			info.ComputeCollisionInfo2D_VirtualMass(in motion0, in motion1, unwrapCode);
			CacheValues_Step3_MaybeRecordHighSpeedVelocities(ref info, in motion0, in motion1, unwrapCode);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void CacheValues_Step3_MaybeRecordHighSpeedVelocities(ref CollisionInfo info, in Motion motion0, in Motion motion1, bool unwrapCode)
		{
			if (info.cacheValue.oneLess_highSpeedFactor < 1f && info.featureIdxInCache == 0)
			{
				info.initialSqrVel_relative = Vec2.DistanceSqr(in motion0.linVel, in motion1.linVel);
				info.isDynamicRoadVsDynamicRigidbody = info.isDynamicRoad && 0f < (motion0.invInertia + motion0.invMass) * (motion1.invInertia + motion1.invMass);
				info.initialSqrVel_secondBody = motion1.linVel.sqrMagnitude;
				info.debug_highSpeedVelocitiesRecorded = true;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void GetPointVelocityAtLocalContactPoints_Normal(in Motion motion0, in Motion motion1, out float velNormal)
		{
			Vec2 vec = default(Vec2);
			vec.x = motion0.linVel.x;
			vec.y = motion0.linVel.y;
			Vec2 vec2 = default(Vec2);
			vec2.x = motion1.linVel.x;
			vec2.y = motion1.linVel.y;
			float angVel = motion0.angVel;
			float angVel2 = motion1.angVel;
			float num = normal.x * vec.x + normal.y * vec.y;
			float num2 = normal.x * vec2.x + normal.y * vec2.y;
			float num3 = num + deltaAngle0ToDeltaDistance * angVel;
			float num4 = num2 + deltaAngle1ToDeltaDistance * angVel2;
			velNormal = num4 - num3;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void GetPointVelocityAtLocalContactPoints_Tangent(in Motion motion0, in Motion motion1, out float velTangent)
		{
			float num = 0f - normal.y;
			float x = normal.x;
			Vec2 vec = default(Vec2);
			vec.x = motion0.linVel.x;
			vec.y = motion0.linVel.y;
			Vec2 vec2 = default(Vec2);
			vec2.x = motion1.linVel.x;
			vec2.y = motion1.linVel.y;
			float angVel = motion0.angVel;
			float angVel2 = motion1.angVel;
			float num2 = num * vec.x + x * vec.y;
			float num3 = num * vec2.x + x * vec2.y;
			float num4 = num2 + deltaAngle0ToDeltaDistance_Tangent * angVel;
			float num5 = num3 + deltaAngle1ToDeltaDistance_Tangent * angVel2;
			velTangent = num5 - num4;
		}

		public Vec2 GetPointVelocityAtLocalContactPoints_Combined(in Motion motion0, in Motion motion1)
		{
			GetPointVelocityAtLocalContactPoints_Normal(in motion0, in motion1, out var velNormal);
			GetPointVelocityAtLocalContactPoints_Tangent(in motion0, in motion1, out var velTangent);
			return velNormal * normal + velTangent * tangent_slow;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void ApplyImpulse_Normal(float impulse, ref Motion motion0, ref Motion motion1)
		{
			Vec2 vec = default(Vec2);
			vec.x = impulse * normal.x;
			vec.y = impulse * normal.y;
			float num = motion0.invMass * invMass0_scale;
			float num2 = motion1.invMass * invMass1_scale;
			motion0.linVel.x -= vec.x * num;
			motion0.linVel.y -= vec.y * num;
			motion0.angVel -= impulse * deltaAngle0ToDeltaDistance * motion0.invInertia * invInertia0_scale;
			motion1.linVel.x += vec.x * num2;
			motion1.linVel.y += vec.y * num2;
			motion1.angVel += impulse * deltaAngle1ToDeltaDistance * motion1.invInertia * invInertia1_scale;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void ApplyImpulse_Tangent(float impulse, ref Motion motion0, ref Motion motion1)
		{
			float num = 0f - normal.y;
			float x = normal.x;
			Vec2 vec = default(Vec2);
			vec.x = impulse * num;
			vec.y = impulse * x;
			float num2 = motion0.invMass * invMass0_scale;
			float num3 = motion1.invMass * invMass1_scale;
			motion0.linVel.x -= vec.x * num2;
			motion0.linVel.y -= vec.y * num2;
			motion0.angVel -= impulse * deltaAngle0ToDeltaDistance_Tangent * motion0.invInertia * invInertia0_scale;
			motion1.linVel.x += vec.x * num3;
			motion1.linVel.y += vec.y * num3;
			motion1.angVel += impulse * deltaAngle1ToDeltaDistance_Tangent * motion1.invInertia * invInertia1_scale;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void ApplyPositionCorrection_Normal(float posImpulse, ref Motion motion0, ref Motion motion1)
		{
			Vec2 vec = default(Vec2);
			vec.x = posImpulse * normal.x;
			vec.y = posImpulse * normal.y;
			float num = motion0.invMass * invMass0_scale;
			float num2 = motion1.invMass * invMass1_scale;
			motion0.com.x -= vec.x * num;
			motion0.com.y -= vec.y * num;
			motion0.angle -= posImpulse * deltaAngle0ToDeltaDistance * motion0.invInertia * invInertia0_scale;
			motion1.com.x += vec.x * num2;
			motion1.com.y += vec.y * num2;
			motion1.angle += posImpulse * deltaAngle1ToDeltaDistance * motion1.invInertia * invInertia1_scale;
		}

		[Obsolete]
		public void RecalculateFull_KeptForReference(SolverNode[] nodesPtr, Motion[] motionsPtr, SolverSettings settings)
		{
			HandlerInput handlerInput = default(HandlerInput);
			handlerInput.collisionTolerance = Poly.Physics.Collide.collisionTolerance;
			handlerInput.maxDistForNewPoint = (settings.debug_createPointsAtNegDistance ? 0f : Poly.Physics.Collide.collisionTolerance);
			_ = SingletonBehaviour<World>.instance;
			ref ShapeHandle reference = ref World.shapeHandleArray[shapeHandleIdx0];
			ref ShapeHandle reference2 = ref World.shapeHandleArray[shapeHandleIdx1];
			bool enableWelding = settings.enableWelding;
			reference.CacheTransform2_InRecalculateFull(nodesPtr, motionsPtr, enableWelding, out var displacement);
			Vec2 b = -displacement;
			Vec2 vec = displacement;
			reference2.CacheTransform2_InRecalculateFull(nodesPtr, motionsPtr, enableWelding, out displacement);
			b += displacement;
			Vec2 vec2 = displacement;
			handlerInput.a = reference.shape;
			handlerInput.b = reference2.shape;
			handlerInput.wTa = reference.t2;
			handlerInput.wTb = reference2.t2;
			if (enableWelding)
			{
				float num = Vec2.Dot(in normal, in b);
				distance -= num;
				contactPoint0 -= vec;
				contactPoint1 -= vec2;
			}
			CalcFrictionBouncinessAndSurfaceVelocity(handlerInput.a, handlerInput.b, settings.deltaTimeForVelocity, settings.globalFrictionCoefficientCap);
			if (motionIdx0 >= 0 && motionIdx1 >= 0)
			{
				CacheValues_NoCreate(ref this, motionsPtr, settings.unwrapContactCaching);
			}
			else
			{
				CacheValues(ref this, in reference, in reference2, nodesPtr, motionsPtr, settings.unwrapContactCaching, settings.nodeToMotionVelocityMultiplier);
			}
		}

		[Obsolete]
		public static void CacheValues_NoCreate(ref CollisionInfo info, Motion[] solverMotionsPtr, bool unwrapCode)
		{
			CacheValues_Step2(ref info, in solverMotionsPtr[info.motionIdx0], in solverMotionsPtr[info.motionIdx1], unwrapCode);
		}
	}
}
