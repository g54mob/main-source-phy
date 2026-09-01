using System;
using System.Collections.Generic;
using Pb;
using Poly.Base;
using Poly.Collide;
using Poly.Extension;
using Poly.Math;
using Poly.Solver;
using UnityEngine;

namespace Poly.Physics
{
	[Serializable]
	public class Collide
	{
		public World world;

		public CollisionDispatcherImpl dispatcher;

		public CollisionFilter filter = new CollisionFilter();

		public FastList<ShapeHandle> shapeHandles = new FastList<ShapeHandle>(16);

		public int numSegments;

		public static float collisionTolerance = 1f;

		public static float maxContactPointDistance = 0.01f;

		private static uint nextShapeId = 0u;

		internal HashSet<short> invalidateShapeIndices = new HashSet<short>();

		internal HashSet<short> notifyShapeIndices_CorrectFrictionAnglesOnly = new HashSet<short>();

		internal Dictionary<int, float> bodyIdxToAngleCorrection = new Dictionary<int, float>();

		public bool wereShapesRemovedLastFrame;

		public bool debug_warnOnceAboutTransformsBeingNotIdentical = true;

		private static int debug_collide_counter;

		public void Clear()
		{
			for (int i = 0; i < shapeHandles.Count; i++)
			{
				shapeHandles.array[i].Dispose();
			}
			shapeHandles.Clear();
			nextShapeId = 0u;
			wereShapesRemovedLastFrame = false;
			debug_warnOnceAboutTransformsBeingNotIdentical = true;
		}

		public short AddShapeHandle(ref ShapeHandle h)
		{
			short result = (short)shapeHandles.Count;
			shapeHandles.Add(in h);
			World.shapeHandleArray = shapeHandles.array;
			return result;
		}

		public void RemoveShapeHandle(short idxToRemove)
		{
			if (world.onlyDisableShapesInsteadOfRemovingThem)
			{
				shapeHandles[idxToRemove].layer = Layer.CollideNothing;
				shapeHandles[idxToRemove].entityHandle = null;
				shapeHandles[idxToRemove].entity = null;
				return;
			}
			wereShapesRemovedLastFrame = true;
			short num = (short)(shapeHandles.Count - 1);
			shapeHandles.RemoveAtAndSwap(idxToRemove);
			invalidateShapeIndices.Add(idxToRemove);
			if (idxToRemove < shapeHandles.Count)
			{
				ref ShapeHandle reference = ref shapeHandles.array[idxToRemove];
				if ((bool)reference.entityHandle)
				{
					reference.entityHandle.UpdateShapeHandleIndex(num, idxToRemove);
					invalidateShapeIndices.Add(num);
				}
				if (reference.entity != null)
				{
					((Rigidbody)reference.entity).UpdateShapeHandleIndex(num, idxToRemove);
					invalidateShapeIndices.Add(num);
				}
			}
			shapeHandles.array[num].Dispose();
		}

		public void RunAsserts(WorldCollisionInput input)
		{
		}

		public void DetectCollisions(WorldCollisionInput input, WorldCollisionOutput output, SolverSettings settings)
		{
			for (int i = 0; i < output.collisionEvents.Count; i++)
			{
				output.collisionEvents.array[i].Dispose();
			}
			output.collisionEvents.Clear();
			RunAsserts(input);
			float num = UnityEngine.Mathf.Max(settings.gravityMagnitude, 9.81f);
			_ = settings.deltaTimeForVelocity;
			_ = settings.deltaTimeForVelocity;
			float num2 = num * settings.frameDeltaTime * settings.frameDeltaTime;
			float num3 = 2f * num2 * settings.referencePenetrationRecoveryRateFactor;
			float penetrationCorrectionPerFrame_Proportional = num3 * 2f;
			HandlerInput input2 = default(HandlerInput);
			input2.collisionTolerance = collisionTolerance;
			input2.maxDistForNewPoint = (settings.debug_createPointsAtNegDistance ? 0f : collisionTolerance);
			HandlerOutput output2 = default(HandlerOutput);
			output2.info0 = default(CollisionInfo);
			output2.secondaryInfo1 = default(CollisionInfo);
			output2.numInfos = 1;
			for (int j = 0; j < input.broadphasePairs.Count; j++)
			{
				Vec2Short bpPair = Vec2Short.FromKey(input.broadphasePairs[j]);
				ref CollisionCache reference = ref input.caches.array[j];
				short shapeHandleIdx = bpPair.x;
				short shapeHandleIdx2 = bpPair.y;
				ref ShapeHandle reference2 = ref input.shapeHandles[bpPair.x];
				ref ShapeHandle reference3 = ref input.shapeHandles[bpPair.y];
				debug_collide_counter++;
				if (settings.debug_forceClearCollisionInfosBeforeProcess)
				{
					output2.info0 = default(CollisionInfo);
					output2.secondaryInfo1 = default(CollisionInfo);
					output2.numInfos = 1;
				}
				else
				{
					output2.info0.onlyAIsWheel = false;
					output2.info0.onlyBIsWheel = false;
					output2.secondaryInfo1.onlyAIsWheel = false;
					output2.secondaryInfo1.onlyBIsWheel = false;
					output2.info0.oneLess_highSpeedFactor = 0f;
					output2.secondaryInfo1.oneLess_highSpeedFactor = 0f;
				}
				ref CollisionInfo reference4 = ref output2.info0;
				reference4.maxContactPointDistance_experiment = maxContactPointDistance;
				bool flag = (reference2.recollisionType | reference3.recollisionType) == (RecollisionType)6;
				if (settings.testFastCollisionsForAllMarked && RecollisionType.Full_Rigidbody == ((reference2.recollisionType | reference3.recollisionType) & RecollisionType.Full_Rigidbody))
				{
					flag = true;
				}
				input2.maxDistForNewPoint = ((settings.debug_createPointsAtNegDistance && !flag) ? 0f : collisionTolerance);
				reference4.cacheIndex = j;
				reference4.cacheValue = reference;
				if (settings.averageFrictionRefSurfaceDistance && reference4.cacheValue.numContactPoints == 2 && reference4.cacheValue.pointCache0.sumFullImpulses_PrevFrame != 0f && reference4.cacheValue.pointCache1.sumFullImpulses_PrevFrame != 0f)
				{
					float num4 = reference4.cacheValue.pointCache0.sumFullImpulses_PrevFrame * reference4.cacheValue.pointCache0.persistent_refSurfaceDistance + reference4.cacheValue.pointCache1.sumFullImpulses_PrevFrame * reference4.cacheValue.pointCache1.persistent_refSurfaceDistance;
					num4 /= reference4.cacheValue.pointCache0.sumFullImpulses_PrevFrame + reference4.cacheValue.pointCache1.sumFullImpulses_PrevFrame;
					reference4.cacheValue.pointCache0.persistent_refSurfaceDistance = num4;
					reference4.cacheValue.pointCache1.persistent_refSurfaceDistance = num4;
				}
				CollisionDispatcherImpl.HandlerInfo handlerInfo = dispatcher.handlers[(int)reference2.shape.type, (int)reference3.shape.type];
				reference.isReversed = handlerInfo.isReversed;
				if (handlerInfo.isReversed)
				{
					shapeHandleIdx = bpPair.y;
					shapeHandleIdx2 = bpPair.x;
					reference2 = ref input.shapeHandles[bpPair.y];
					reference3 = ref input.shapeHandles[bpPair.x];
				}
				reference4.isReversed = handlerInfo.isReversed;
				reference4.cacheValue.isReversed = handlerInfo.isReversed;
				bool flag2 = handlerInfo.entityTypes != EntityTypes.BodyBody && reference3.entityHandle.isDynamic;
				reference4.contactSolverSettings = (flag2 ? settings.bridgeContact : settings.bodyContact);
				reference4.isDynamicRoad = flag2;
				bool flag3 = handlerInfo.entityTypes == EntityTypes.BodyEdge && !reference3.entityHandle.isDynamic;
				bool flag4 = handlerInfo.entityTypes == EntityTypes.BodyNode && ((Rigidbody)reference2.entity).mass == 0f && reference3.entityHandle.isDynamic;
				input2.a = reference2.shape;
				input2.b = reference3.shape;
				input2.wTa = reference2.t2;
				input2.wTb = reference3.t2;
				ref float angleA = ref input2.rotationState.angleA;
				ref float angleB = ref input2.rotationState.angleB;
				(float, float) angleFromMotions = CollisionInfo.GetAngleFromMotions(in reference2, in reference3, input.solverMotionsPtr);
				angleA = angleFromMotions.Item1;
				angleB = angleFromMotions.Item2;
				input2.rotationState.radiusA = input2.a.radius;
				input2.rotationState.radiusB = input2.b.radius;
				input2.rotationState.angleNormal = float.MinValue;
				output2.numInfos = 1;
				output2.closestFeatureIdx = 0;
				output2.info0.shapeHandleIdx0 = shapeHandleIdx;
				output2.info0.shapeHandleIdx1 = shapeHandleIdx2;
				output2.info0.entityTypes = handlerInfo.entityTypes;
				output2.secondaryInfo1.shapeHandleIdx0 = shapeHandleIdx;
				output2.secondaryInfo1.shapeHandleIdx1 = shapeHandleIdx2;
				output2.secondaryInfo1.entityTypes = handlerInfo.entityTypes;
				bool flag5 = false;
				if (handlerInfo.handler != null)
				{
					if (flag && settings.enableFastCollisions)
					{
						int num5 = 0;
						if (0 < reference4.cacheValue.numContactPoints)
						{
							num5 = reference4.cacheValue.pointCache0.numFramesWithNonZeroImpulse;
							if (1 < reference4.cacheValue.numContactPoints)
							{
								num5 = ((num5 < reference4.cacheValue.pointCache1.numFramesWithNonZeroImpulse) ? reference4.cacheValue.pointCache1.numFramesWithNonZeroImpulse : num5);
							}
						}
						if (num5 < 3)
						{
							RunHighFrequencyCollision_Old(in reference2, in reference3, input.nodesPtr, input.solverMotionsPtr, in handlerInfo, ref input2, ref output2, settings, num3, penetrationCorrectionPerFrame_Proportional, ref debug_warnOnceAboutTransformsBeingNotIdentical);
							flag5 = true;
						}
						else
						{
							handlerInfo.handler(ref input2, ref output2);
						}
					}
					else
					{
						handlerInfo.handler(ref input2, ref output2);
					}
					bool flag6 = true;
					CollisionEvent collisionEvent = default(CollisionEvent);
					ref CollisionEvent reference5 = ref collisionEvent;
					int num6 = -1;
					Poly.Solver.Motion motion = default(Poly.Solver.Motion);
					Poly.Solver.Motion motion2 = default(Poly.Solver.Motion);
					if (output2.numInfos == 0)
					{
						reference.Clear_AndTriggerExitCallbacks(in bpPair);
					}
					reference4.collisionEventIdx = -1;
					reference4.collisionEventPointIdx = 0;
					output2.secondaryInfo1.collisionEventIdx = -1;
					output2.secondaryInfo1.collisionEventPointIdx = 1;
					output2.secondaryInfo1.isReversed = reference4.isReversed;
					reference4.doFriction = output2.numInfos == 1;
					reference4.doFriction = true;
					output2.info0.hasSecondPoint = output2.numInfos == 2;
					output2.secondaryInfo1.hasSecondPoint = false;
					output2.info0.featureIdxInCache = 0;
					output2.secondaryInfo1.featureIdxInCache = 1;
					for (int k = 0; k < 2 && k < output2.numInfos; k++)
					{
						reference4.MaybeFirstTimeInit(settings);
						reference4.MaybeOnlyUpdateReferenceDepth(num3, penetrationCorrectionPerFrame_Proportional, settings);
						if (k == 0 && output2.numInfos == 2)
						{
							output2.secondaryInfo1.cacheValue.oneLess_highSpeedFactor = output2.info0.cacheValue.oneLess_highSpeedFactor;
							output2.secondaryInfo1.cacheValue.highSpeedBlendTimeLeft = output2.info0.cacheValue.highSpeedBlendTimeLeft;
							output2.secondaryInfo1.cacheValue.pointCache0.referencePenetrationDistance = output2.info0.cacheValue.pointCache0.referencePenetrationDistance;
						}
						else if (k == 1)
						{
							output2.info0.cacheValue.pointCache1.referencePenetrationDistance = output2.secondaryInfo1.cacheValue.pointCache1.referencePenetrationDistance;
						}
						reference4.CalcFrictionBouncinessAndSurfaceVelocity(input2.a, input2.b, settings.deltaTimeForVelocity, settings.globalFrictionCoefficientCap);
						bool flag7 = false;
						if (reference4.oneLess_highSpeedFactor != 1f)
						{
							flag7 = true;
						}
						CollisionInfo.ResetImpulses(ref reference4, settings);
						if (!flag7)
						{
							reference4.invMass0_scale = 1f;
							reference4.invMass1_scale = 1f;
							reference4.invInertia0_scale = 1f;
							reference4.invInertia1_scale = 1f;
							if (!flag5)
							{
								CollisionInfo.CacheValues(ref reference4, in reference2, in reference3, input.nodesPtr, input.solverMotionsPtr, settings.unwrapContactCaching, settings.nodeToMotionVelocityMultiplier);
							}
						}
						else
						{
							reference4.invMass0_scale = 1f;
							reference4.invMass1_scale = 1f / (reference4.oneLess_highSpeedFactor + 1E-12f);
							reference4.invInertia0_scale = 1f;
							reference4.invInertia1_scale = reference4.oneLess_highSpeedFactor + 1E-12f;
							CollisionInfo.CacheValues_MassChanger(ref reference4, in reference2, in reference3, input.nodesPtr, input.solverMotionsPtr, settings.unwrapContactCaching, settings.nodeToMotionVelocityMultiplier);
						}
						if (flag6)
						{
							bool flag8 = reference2.entity != null && ((Rigidbody)reference2.entity).collisionListeners.Count > 0;
							flag8 |= reference3.entity != null && ((Rigidbody)reference3.entity).collisionListeners.Count > 0;
							if (flag8)
							{
								reference5.a = reference2;
								reference5.b = reference3;
								reference5.isReversed = reference4.isReversed;
								reference5.idxA = reference4.shapeHandleIdx0;
								reference5.idxB = reference4.shapeHandleIdx1;
								int collisionInfoIdx_debug = ((reference4.entityTypes != EntityTypes.BodyBody) ? output.bridgeContact.Count : (-output.bodyContact.Count));
								reference5.collisionInfoIdx_debug = collisionInfoIdx_debug;
								num6 = output.collisionEvents.Count;
								output.collisionEvents.Add(in reference5);
								reference5 = ref output.collisionEvents.array[num6];
							}
							reference.hasListeners = flag8;
							reference4.cacheValue.hasListeners = flag8;
							flag6 = false;
						}
						if (settings.debug_triggerInternalCollisionCallback && 0 <= num6)
						{
							World.TriggerCollisionCallbacks_Internal_Process(ref reference4, ref reference5);
						}
						if (0 <= num6)
						{
							reference4.collisionEventIdx = num6;
							reference4.collisionEventPointIdx = reference5.numPoints;
							reference5.numPoints++;
							ref ContactPointInfo reference6 = ref reference5.point0;
							if (reference5.numPoints == 2)
							{
								reference6 = ref reference5.point1;
							}
							reference6.position = 0.5f * (reference4.contactPoint0 + reference4.contactPoint1);
							reference6.normal = reference4.normal;
							reference6.distance = reference4.distance;
							if (reference5.numPoints == 1)
							{
								CollisionInfo.CacheValues_Step1_CopyOrCreateMotions(in reference2, in reference3, input.nodesPtr, input.solverMotionsPtr, out motion, out motion2, ref output2.info0, settings.nodeToMotionVelocityMultiplier);
								reference5.relativeLinearVelocityBeforeCollision = (motion2.linVel - motion.linVel) * (1f / settings.deltaTimeForVelocity);
								reference5.relativeAngularVelocityBeforeCollisionInDeg = (motion2.angVel - motion.angVel) * 57.29578f / settings.deltaTimeForVelocity;
							}
							reference6.relativePointVelocityBeforeCollision = reference4.GetPointVelocityAtLocalContactPoints_Combined(in motion, in motion2);
							reference6.relativePointVelocityBeforeCollision *= 1f / settings.deltaTimeForVelocity;
						}
						reference4.minDistanceInFrame = float.MaxValue;
						if (settings.prioritizeBridgeContact_AndEnableHighFreqBridgeContact && reference4.entityTypes != EntityTypes.BodyBody && !flag3)
						{
							if (settings.trackEdgesWithCollisions_unused)
							{
								if ((reference4.entityTypes == EntityTypes.BodyEdge || reference4.entityTypes == EntityTypes.EdgeEdge) && k == 0)
								{
									WorldObjectImpl entityHandle = input.shapeHandles[reference4.shapeHandleIdx1].entityHandle;
									EdgeHandle edgeHandle = (EdgeHandle)entityHandle;
									if (!edgeHandle.runtime_isMarkedAsColliding)
									{
										output.edgesWithCollisions.Add(in entityHandle.worldIdx);
										edgeHandle.runtime_isMarkedAsColliding = true;
									}
								}
								if ((reference4.entityTypes == EntityTypes.EdgeNode || reference4.entityTypes == EntityTypes.EdgeEdge) && k == 0)
								{
									WorldObjectImpl entityHandle2 = input.shapeHandles[reference4.shapeHandleIdx0].entityHandle;
									EdgeHandle edgeHandle2 = (EdgeHandle)entityHandle2;
									if (!edgeHandle2.runtime_isMarkedAsColliding)
									{
										output.edgesWithCollisions.Add(in entityHandle2.worldIdx);
										edgeHandle2.runtime_isMarkedAsColliding = true;
									}
								}
							}
							if (flag4)
							{
								output.fullFrequencyBridgeContactIndices.Add(output.bridgeContact.Count);
								float num7 = Pb.Mathf.Clamp01((Pb.Mathf.Abs(Vec2.Dot(in reference4.normal, in Vec2.right)) - 0.5f) / 0.366f);
								num7 = 1f - num7;
								reference4.friction *= num7;
							}
							output.bridgeContact.Add(in reference4);
						}
						else
						{
							output.bodyContact.Add(in reference4);
						}
						if (k + 1 == output2.numInfos)
						{
							break;
						}
						if (output2.numInfos == 2)
						{
							output2.secondaryInfo1.cacheValue = reference4.cacheValue;
						}
						reference4 = ref output2.secondaryInfo1;
						reference4.doFriction = true;
					}
				}
				else if (handlerInfo.isIgnored)
				{
				}
			}
		}

		private static void RunHighFrequencyCollision(in ShapeHandle shapeHandle0, in ShapeHandle shapeHandle1, SolverNode[] nodesPtr, Poly.Solver.Motion[] solverMotionsPtr, in CollisionDispatcherImpl.HandlerInfo handlerInfo, ref HandlerInput handlerInput, ref HandlerOutput handlerOutput, SolverSettings settings, float penetrationCorrectionPerFrame_Constant, float penetrationCorrectionPerFrame_Proportional, ref bool debug_warnOnceAboutTransformsBeingNotIdentical)
		{
			CollisionInfo.CacheValues_Step1_CopyOrCreateMotions(in shapeHandle0, in shapeHandle1, nodesPtr, solverMotionsPtr, out var motion, out var motion2, ref handlerOutput.info0, settings.nodeToMotionVelocityMultiplier);
			Poly.Solver.Motion motion3 = motion;
			Poly.Solver.Motion motion4 = motion2;
			Vec2 comTbody = shapeHandle0.GetComTBody_InCollide();
			Vec2 comTbody2 = shapeHandle1.GetComTBody_InCollide();
			Poly.Solver.Solver.ClipVelocityOfSingleMotion_4HFCD(ref motion, settings);
			Poly.Solver.Solver.ClipVelocityOfSingleMotion_4HFCD(ref motion2, settings);
			int num = ((!settings.integrateInSolverIterations) ? 1 : settings.numIterations);
			int num2 = num;
			int num3 = 1;
			if (settings.betterFastCollisionSlicing)
			{
				float num4 = (shapeHandle1.fastLinearVel - shapeHandle0.fastLinearVel).magnitude * settings.frameDeltaTime / settings.deltaTimeForVelocity;
				num = UnityEngine.Mathf.CeilToInt(num4 / (0.99f * maxContactPointDistance) + 5.877472E-39f);
				if (num2 < num)
				{
					handlerOutput.info0.maxContactPointDistance_experiment = maxContactPointDistance * (float)num / (float)num2;
					num = num2;
				}
				else if (num < num2)
				{
					num = UnityEngine.Mathf.CeilToInt(num4 / (0.5f * maxContactPointDistance) + 5.877472E-39f);
					if (num2 < num)
					{
						num = num2;
					}
					if (num2 == 4 && num <= 2 && settings.betterSlowCollisionSlicing)
					{
						switch (num)
						{
						case 0:
						case 1:
							num3 = 4;
							break;
						case 2:
							num3 = 2;
							break;
						}
						num = 4;
					}
					else if (num <= num2 / 2 && settings.betterSlowCollisionSlicing)
					{
						for (; num <= num2 / 2; num++)
						{
							if (num2 % num == 0)
							{
								num3 = num2 / num;
								break;
							}
						}
						num = num2;
					}
					else
					{
						num = num2;
					}
				}
			}
			handlerOutput.secondaryInfo1 = handlerOutput.info0;
			HandlerOutput handlerOutput2 = handlerOutput;
			Transform2 transform = Transform2.identity;
			Transform2 transform2 = Transform2.identity;
			Poly.Solver.Motion motion5 = motion3;
			Poly.Solver.Motion motion6 = motion4;
			Transform2 transform3 = Transform2.identity;
			Transform2 transform4 = Transform2.identity;
			HandlerOutput handlerOutput3 = handlerOutput;
			bool flag = true;
			Vec2 a = Vec2.zero;
			int num5;
			for (int i = (num5 = -1); i < num; i += ((i < 0) ? 1 : num3))
			{
				if (i >= 0)
				{
					for (int j = 0; j < num3; j++)
					{
						Poly.Solver.Solver.IntegrateSingleMotion_NoClipVelocities_4HFCD(ref motion, settings.deltaTimeForVelocity, settings);
						Poly.Solver.Solver.IntegrateSingleMotion_NoClipVelocities_4HFCD(ref motion2, settings.deltaTimeForVelocity, settings);
					}
				}
				Rigidbody.CacheTransform2_InCollide(in motion, in comTbody, out var t2_out);
				Rigidbody.CacheTransform2_InCollide(in motion2, in comTbody2, out var t2_out2);
				if (debug_warnOnceAboutTransformsBeingNotIdentical && i == -1)
				{
					if (!Pb.Mathf.Approximately(in t2_out, in handlerInput.wTa, 1E-05f))
					{
						UnityEngine.Debug.LogWarning($"Fast CD: Transforms expected (nearly?) identical. Angle: {motion.angle}");
						debug_warnOnceAboutTransformsBeingNotIdentical = false;
					}
					if (!Pb.Mathf.Approximately(in t2_out2, in handlerInput.wTb, 1E-05f))
					{
						UnityEngine.Debug.LogWarning($"Fast CD: Transforms expected (nearly?) identical. Angle: {motion2.angle}");
						debug_warnOnceAboutTransformsBeingNotIdentical = false;
					}
				}
				bool flag2 = i >= 0 && settings.enableWelding;
				Vec2 vec = Vec2.zero;
				Vec2 vec2 = Vec2.zero;
				Vec2 b = Vec2.zero;
				handlerInput.wTa = t2_out;
				handlerInput.wTb = t2_out2;
				handlerInput.rotationState.angleA = motion.angle;
				handlerInput.rotationState.angleB = motion2.angle;
				if (flag2)
				{
					float num6 = 1f;
					vec = motion.linVel * num6;
					vec2 = motion2.linVel * num6;
					b = vec2 - vec;
					handlerInput.wTa.position += vec;
					handlerInput.wTb.position += vec2;
				}
				handlerOutput.closestFeatureIdx = 0;
				handlerInfo.handler(ref handlerInput, ref handlerOutput);
				if (flag)
				{
					handlerOutput2 = handlerOutput;
					transform = t2_out;
					transform2 = t2_out2;
					handlerOutput3 = handlerOutput;
					transform3 = t2_out;
					transform4 = t2_out2;
				}
				ref CollisionInfo reference = ref handlerOutput.info0;
				if (handlerOutput.closestFeatureIdx == 1)
				{
					reference = ref handlerOutput.secondaryInfo1;
				}
				if (handlerOutput.numInfos > 0)
				{
					ref CollisionInfo reference2 = ref handlerOutput.info0;
					for (int k = 0; k < handlerOutput.numInfos; k++)
					{
						if (flag2)
						{
							float num7 = Vec2.Dot(in reference2.normal, in b);
							reference2.distance -= num7;
							reference2.contactPoint0 -= vec;
							reference2.contactPoint1 -= vec2;
						}
						reference2 = ref handlerOutput.secondaryInfo1;
					}
				}
				else
				{
					reference.distance = float.PositiveInfinity;
				}
				Vec2 b2 = motion2.linVel - motion.linVel;
				bool flag3 = -0.0001f <= reference.distance;
				bool flag4 = 0f - 0.05f <= reference.distance;
				bool flag5 = 0f < Vec2.Dot(in reference.normal, in b2);
				bool flag6 = ((!flag2) ? (flag3 && !flag5) : (flag4 && !flag5));
				if (handlerOutput.numInfos != 0)
				{
					if (!(flag || flag6))
					{
						if (!flag)
						{
							bool flag7 = Vec2.Dot(in a, in reference.normal) > -0.7071069f;
							if (flag2 && (!flag5 || flag7) && flag4)
							{
								num5 = i;
								transform = t2_out;
								transform2 = t2_out2;
							}
							else
							{
								handlerOutput = handlerOutput2;
								t2_out = transform;
								t2_out2 = transform2;
								motion = motion5;
								motion2 = motion6;
							}
						}
						flag = false;
						break;
					}
					if (!flag)
					{
						handlerOutput2 = handlerOutput;
						transform = t2_out;
						transform2 = t2_out2;
						motion5 = motion;
						motion6 = motion2;
					}
					num5 = i;
				}
				if (handlerOutput.numInfos > 0)
				{
					a = reference.normal;
				}
				flag = false;
			}
			Vec2 b3 = motion2.linVel - motion.linVel;
			ref CollisionInfo reference3 = ref handlerOutput.info0;
			for (int l = 0; l < handlerOutput.numInfos; l++)
			{
				if (0 <= num5)
				{
					reference3.contactPoint0 = transform3 * transform.InvMul(reference3.contactPoint0);
					reference3.contactPoint1 = transform4 * transform2.InvMul(reference3.contactPoint1);
					if (reference3.distance < -0f)
					{
						if (reference3.onlyAIsWheel)
						{
							reference3.contactPoint1 = reference3.contactPoint0;
						}
						else if (reference3.onlyBIsWheel)
						{
							reference3.contactPoint0 = reference3.contactPoint1;
						}
						else
						{
							reference3.contactPoint1 = (reference3.contactPoint0 = 0.5f * (reference3.contactPoint0 + reference3.contactPoint1));
						}
					}
				}
				CollisionInfo.CacheValues_Step2(ref reference3, in motion3, in motion4, settings.unwrapContactCaching);
				reference3.comOffsetX = motion2.com.x - motion.com.x;
				reference3.comOffsetY = motion2.com.y - motion.com.y;
				reference3.angleOffsetA = motion.angle;
				reference3.angleOffsetB = motion2.angle;
				float num8 = Vec2.Dot(in reference3.normal, in b3);
				if (1E-06f < num8)
				{
					int num9 = ((num5 >= 0 && settings.enableWelding) ? 1 : 0);
					float num10 = (float)(-(num5 + 1) - num9) * num8;
					if (reference3.distance < 0f)
					{
						num10 += reference3.distance;
					}
					num10 -= penetrationCorrectionPerFrame_Constant;
					num10 /= 1f - penetrationCorrectionPerFrame_Proportional;
					reference3.ModifyReferenceDepth(l, num10);
					if (l == 0 && handlerOutput.numInfos == 2)
					{
						handlerOutput.secondaryInfo1.cacheValue.pointCache0.referencePenetrationDistance = handlerOutput.info0.cacheValue.pointCache0.referencePenetrationDistance;
					}
					else if (l == 1)
					{
						handlerOutput.info0.cacheValue.pointCache1.referencePenetrationDistance = handlerOutput.secondaryInfo1.cacheValue.pointCache1.referencePenetrationDistance;
					}
				}
				reference3.RecalculateDistanceRB_InCollide(in motion3, in motion4);
				reference3 = ref handlerOutput.secondaryInfo1;
			}
			if (handlerOutput.numInfos == 2)
			{
				handlerOutput.secondaryInfo1.cacheValue.pointCache0.refSurfaceDistance = handlerOutput.info0.cacheValue.pointCache0.refSurfaceDistance;
				handlerOutput.info0.cacheValue.pointCache1.refSurfaceDistance = handlerOutput.secondaryInfo1.cacheValue.pointCache1.refSurfaceDistance;
			}
		}

		private static void RunHighFrequencyCollision_Old(in ShapeHandle shapeHandle0, in ShapeHandle shapeHandle1, SolverNode[] nodesPtr, Poly.Solver.Motion[] solverMotionsPtr, in CollisionDispatcherImpl.HandlerInfo handlerInfo, ref HandlerInput handlerInput, ref HandlerOutput handlerOutput, SolverSettings settings, float penetrationCorrectionPerFrame_Constant, float penetrationCorrectionPerFrame_Proportional, ref bool debug_warnOnceAboutTransformsBeingNotIdentical)
		{
			CollisionInfo.CacheValues_Step1_CopyOrCreateMotions(in shapeHandle0, in shapeHandle1, nodesPtr, solverMotionsPtr, out var motion, out var motion2, ref handlerOutput.info0, settings.nodeToMotionVelocityMultiplier);
			Poly.Solver.Motion motion3 = motion;
			Poly.Solver.Motion motion4 = motion2;
			Vec2 comTbody = shapeHandle0.GetComTBody_InCollide();
			Vec2 comTbody2 = shapeHandle1.GetComTBody_InCollide();
			Poly.Solver.Solver.ClipVelocityOfSingleMotion_4HFCD(ref motion, settings);
			Poly.Solver.Solver.ClipVelocityOfSingleMotion_4HFCD(ref motion2, settings);
			int num = ((!settings.integrateInSolverIterations) ? 1 : settings.numIterations);
			int num2 = num;
			int num3 = 1;
			if (settings.betterFastCollisionSlicing)
			{
				float num4 = (shapeHandle1.fastLinearVel - shapeHandle0.fastLinearVel).magnitude * settings.frameDeltaTime / settings.deltaTimeForVelocity;
				num = UnityEngine.Mathf.CeilToInt(num4 / (0.99f * maxContactPointDistance) + 5.877472E-39f);
				if (num2 < num)
				{
					handlerOutput.info0.maxContactPointDistance_experiment = maxContactPointDistance * (float)num / (float)num2;
					num = num2;
				}
				else if (num < num2)
				{
					num = UnityEngine.Mathf.CeilToInt(num4 / (0.5f * maxContactPointDistance) + 5.877472E-39f);
					if (num2 < num)
					{
						num = num2;
					}
					if (num2 == 4 && num <= 2 && settings.betterSlowCollisionSlicing)
					{
						switch (num)
						{
						case 0:
						case 1:
							num3 = 4;
							break;
						case 2:
							num3 = 2;
							break;
						}
						num = 4;
					}
					else if (num <= num2 / 2 && settings.betterSlowCollisionSlicing)
					{
						for (; num <= num2 / 2; num++)
						{
							if (num2 % num == 0)
							{
								num3 = num2 / num;
								break;
							}
						}
						num = num2;
					}
					else
					{
						num = num2;
					}
				}
			}
			handlerOutput.secondaryInfo1 = handlerOutput.info0;
			HandlerOutput handlerOutput2 = handlerOutput;
			bool flag = true;
			Vec2 a = Vec2.zero;
			int num5;
			for (int i = (num5 = -1); i < num; i += ((i < 0) ? 1 : num3))
			{
				if (i >= 0)
				{
					for (int j = 0; j < num3; j++)
					{
						Poly.Solver.Solver.IntegrateSingleMotion_NoClipVelocities_4HFCD(ref motion, settings.deltaTimeForVelocity, settings);
						Poly.Solver.Solver.IntegrateSingleMotion_NoClipVelocities_4HFCD(ref motion2, settings.deltaTimeForVelocity, settings);
					}
				}
				Rigidbody.CacheTransform2_InCollide(in motion, in comTbody, out var t2_out);
				Rigidbody.CacheTransform2_InCollide(in motion2, in comTbody2, out var t2_out2);
				if (debug_warnOnceAboutTransformsBeingNotIdentical && i == -1)
				{
					if (!Pb.Mathf.Approximately(in t2_out, in handlerInput.wTa, 1E-05f))
					{
						UnityEngine.Debug.LogWarning($"Fast CD: Transforms expected (nearly?) identical. Angle: {motion.angle}");
						debug_warnOnceAboutTransformsBeingNotIdentical = false;
					}
					if (!Pb.Mathf.Approximately(in t2_out2, in handlerInput.wTb, 1E-05f))
					{
						UnityEngine.Debug.LogWarning($"Fast CD: Transforms expected (nearly?) identical. Angle: {motion2.angle}");
						debug_warnOnceAboutTransformsBeingNotIdentical = false;
					}
				}
				bool flag2 = i >= 0 && settings.enableWelding;
				Vec2 vec = Vec2.zero;
				Vec2 vec2 = Vec2.zero;
				Vec2 b = Vec2.zero;
				if (flag2)
				{
					float num6 = 1f;
					vec = motion.linVel * num6;
					vec2 = motion2.linVel * num6;
					b = vec2 - vec;
					t2_out.position += vec;
					t2_out2.position += vec2;
				}
				handlerInput.wTa = t2_out;
				handlerInput.wTb = t2_out2;
				handlerInput.rotationState.angleA = motion.angle;
				handlerInput.rotationState.angleB = motion2.angle;
				handlerOutput.closestFeatureIdx = 0;
				handlerInfo.handler(ref handlerInput, ref handlerOutput);
				if (flag)
				{
					handlerOutput2 = handlerOutput;
				}
				ref CollisionInfo reference = ref handlerOutput.info0;
				if (handlerOutput.closestFeatureIdx == 1)
				{
					reference = ref handlerOutput.secondaryInfo1;
				}
				if (handlerOutput.numInfos > 0)
				{
					ref CollisionInfo reference2 = ref handlerOutput.info0;
					for (int k = 0; k < handlerOutput.numInfos; k++)
					{
						if (flag2)
						{
							float num7 = Vec2.Dot(in reference2.normal, in b);
							reference2.distance -= num7;
							reference2.contactPoint0 -= vec;
							reference2.contactPoint1 -= vec2;
						}
						CollisionInfo.CacheValues_Step2(ref reference2, in motion, in motion2, settings.unwrapContactCaching);
						reference2 = ref handlerOutput.secondaryInfo1;
					}
				}
				else
				{
					reference.distance = float.PositiveInfinity;
				}
				Vec2 b2 = motion2.linVel - motion.linVel;
				bool flag3 = -0.0001f <= reference.distance;
				bool flag4 = 0f - 0.05f <= reference.distance;
				bool flag5 = 0f < Vec2.Dot(in reference.normal, in b2);
				bool flag6 = ((!flag2) ? (flag3 && !flag5) : (flag4 && !flag5));
				if (handlerOutput.numInfos != 0)
				{
					if (!(i == -1 || flag6))
					{
						if (!flag)
						{
							bool flag7 = Vec2.Dot(in a, in reference.normal) > -0.7071069f;
							if (flag2 && (!flag5 || flag7) && flag4)
							{
								num5 = i;
							}
							else
							{
								handlerOutput = handlerOutput2;
							}
						}
						flag = false;
						break;
					}
					handlerOutput2 = handlerOutput;
					num5 = i;
				}
				if (handlerOutput.numInfos > 0)
				{
					a = reference.normal;
				}
				flag = false;
			}
			Vec2 b3 = motion2.linVel - motion.linVel;
			ref CollisionInfo reference3 = ref handlerOutput.info0;
			for (int l = 0; l < handlerOutput.numInfos; l++)
			{
				float num8 = Vec2.Dot(in reference3.normal, in b3);
				if (1E-06f < num8)
				{
					int num9 = ((num5 >= 0 && settings.enableWelding) ? 1 : 0);
					float num10 = (float)(-(num5 + 1) - num9) * num8;
					num10 -= penetrationCorrectionPerFrame_Constant;
					num10 /= 1f - penetrationCorrectionPerFrame_Proportional;
					reference3.ModifyReferenceDepth(l, num10);
					if (l == 0 && handlerOutput.numInfos == 2)
					{
						handlerOutput.secondaryInfo1.cacheValue.pointCache0.referencePenetrationDistance = handlerOutput.info0.cacheValue.pointCache0.referencePenetrationDistance;
					}
					else if (l == 1)
					{
						handlerOutput.info0.cacheValue.pointCache1.referencePenetrationDistance = handlerOutput.secondaryInfo1.cacheValue.pointCache1.referencePenetrationDistance;
					}
				}
				reference3.RecalculateDistanceRB_InCollide(in motion3, in motion4);
				reference3 = ref handlerOutput.secondaryInfo1;
			}
			if (handlerOutput.numInfos == 2)
			{
				handlerOutput.secondaryInfo1.cacheValue.pointCache0.refSurfaceDistance = handlerOutput.info0.cacheValue.pointCache0.refSurfaceDistance;
				handlerOutput.info0.cacheValue.pointCache1.refSurfaceDistance = handlerOutput.secondaryInfo1.cacheValue.pointCache1.refSurfaceDistance;
			}
		}
	}
}
