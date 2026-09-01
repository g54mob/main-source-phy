using System;
using System.Collections.Generic;
using Poly.Base;
using Poly.Extension;
using Poly.Physics;
using Poly.Solver;
using UnityEngine;

namespace Poly.Collide
{
	public class PersistentCollisionCache
	{
		public FastList<CollisionCache> caches = new FastList<CollisionCache>(16);

		private FastList<CollisionCache> cachesSecondBuffer = new FastList<CollisionCache>(16);

		private FastList<int> broadphasePairsCopy = new FastList<int>(16);

		public static void SortPairs(FastList<int> pairIndices)
		{
			Array.Sort(pairIndices.array, 0, pairIndices.Count);
		}

		public void Clear()
		{
			for (int i = 0; i < caches.Count; i++)
			{
				caches.array[i].Clear_AndTriggerExitCallbacks(Vec2Short.FromKey(broadphasePairsCopy[i]));
			}
			caches.Clear();
			cachesSecondBuffer.Clear();
			broadphasePairsCopy.Clear();
		}

		public void InvalidateCaches(HashSet<short> invalidIndices, HashSet<short> notifyIndices_CorrectFrictionAnglesOnly, Dictionary<int, float> bodyIdxToAngleCorrection)
		{
			if (0 >= invalidIndices.Count && 0 >= notifyIndices_CorrectFrictionAnglesOnly.Count)
			{
				return;
			}
			notifyIndices_CorrectFrictionAnglesOnly.UnionWith(invalidIndices);
			for (int i = 0; i < broadphasePairsCopy.Count; i++)
			{
				Vec2Short bpPair = Vec2Short.FromKey(broadphasePairsCopy[i]);
				if (notifyIndices_CorrectFrictionAnglesOnly.Contains(bpPair.x) || notifyIndices_CorrectFrictionAnglesOnly.Contains(bpPair.y))
				{
					if (invalidIndices.Contains(bpPair.x) || invalidIndices.Contains(bpPair.y))
					{
						caches.array[i].Clear_AndTriggerExitCallbacks(in bpPair);
						caches.array[i] = default(CollisionCache);
					}
					else
					{
						caches.array[i].Notice_CorrectFrictionAnglesOnly(in bpPair, bodyIdxToAngleCorrection, resetX: false, resetY: false);
					}
				}
			}
			invalidIndices.Clear();
			notifyIndices_CorrectFrictionAnglesOnly.Clear();
			bodyIdxToAngleCorrection.Clear();
		}

		public void UdpateCachesForPairs(FastList<int> newPairs)
		{
			FastList<int> fastList = broadphasePairsCopy;
			FastList<CollisionCache> fastList2 = caches;
			FastList<CollisionCache> fastList3 = cachesSecondBuffer;
			fastList3.Clear();
			fastList3.Reserve(newPairs.Count);
			int i = 0;
			int num = 0;
			while (num < newPairs.Count)
			{
				if (i == fastList.Count || newPairs[num] < fastList[i])
				{
					fastList3.Add_Unchecked(default(CollisionCache));
					num++;
				}
				else if (fastList[i] == newPairs[num])
				{
					fastList3.Add_Unchecked(in fastList2[i]);
					num++;
					i++;
				}
				else
				{
					fastList2[i].Clear_AndTriggerExitCallbacks(Vec2Short.FromKey(fastList[i]));
					i++;
				}
			}
			for (; i < fastList.Count; i++)
			{
				fastList2[i].Clear_AndTriggerExitCallbacks(Vec2Short.FromKey(fastList[i]));
			}
			broadphasePairsCopy.Clear();
			broadphasePairsCopy.SetSize(newPairs.Count);
			Array.Copy(newPairs.array, broadphasePairsCopy.array, newPairs.Count);
			Values.Swap(ref caches, ref cachesSecondBuffer);
		}

		public void UpdateCachesFromCollisionInfos_Rigidbodies(FastList<CollisionInfo> collisionInfos, FastList<CollisionEvent> collisionEvents)
		{
			SolverSettings settings = SingletonBehaviour<World>.instance.settings;
			float num = -0.1f * settings.deltaTimeForVelocity;
			CollisionInfo[] array = collisionInfos.array;
			for (int i = 0; i < collisionInfos.Count; i++)
			{
				ref CollisionInfo reference = ref array[i];
				ref CollisionCache reference2 = ref caches.array[array[i].cacheIndex];
				int featureIdxInCache = reference.featureIdxInCache;
				ref ContactPointCache reference3 = ref reference.cacheValue.pointCache0;
				if (featureIdxInCache == 1)
				{
					reference3 = ref reference.cacheValue.pointCache1;
				}
				ref ContactPointCache reference4 = ref reference2.pointCache0;
				if (featureIdxInCache == 1)
				{
					reference4 = ref reference2.pointCache1;
				}
				bool flag = reference.sumFullImpulses_InFrame != 0f && !reference3.impulseEventWithheld && reference3.sumFullImpulses_PrevFrame == 0f && reference.lastVelError_forImpulseEstimation < num && 0.01f < reference.distance && 0.1f < (0f - reference.distance) / reference.lastVelError_forImpulseEstimation;
				if (0 <= reference.collisionEventIdx)
				{
					ref CollisionEvent reference5 = ref collisionEvents.array[reference.collisionEventIdx];
					ref ContactPointInfo reference6 = ref reference5.point0;
					if (featureIdxInCache == 1)
					{
						reference6 = ref reference5.point1;
					}
					reference6.impulseApplied = reference.sumFullImpulses_InFrame / settings.deltaTimeForVelocity * reference.normal + reference.sumFrictionImpulses_InFrame / settings.deltaTimeForVelocity * reference.tangent_slow;
					if (reference3.impulseEventWithheld)
					{
						float num2 = reference.sumFullImpulses_InFrame + reference3.sumFullImpulses_PrevFrame;
						reference6.delayedImpactImpulse = num2 / settings.deltaTimeForVelocity * reference.normal + reference.sumFrictionImpulses_InFrame / settings.deltaTimeForVelocity * reference.tangent_slow;
						reference6.estimatedImpactImpulseMultiplier = 0f;
					}
					else if (!flag)
					{
						reference6.delayedImpactImpulse = reference6.impulseApplied;
						reference6.estimatedImpactImpulseMultiplier = 1f;
					}
					else
					{
						float num3 = reference.lastVelError_forImpulseEstimation + reference.lastPosError_forImpulseEstimation;
						if (1E-12f < num3 * num3 && 0f < num3 * reference.lastVelError_forImpulseEstimation)
						{
							reference6.estimatedImpactImpulseMultiplier = reference.lastVelError_forImpulseEstimation / num3;
							reference6.delayedImpactImpulse = Vector2.zero;
						}
						else
						{
							reference6.estimatedImpactImpulseMultiplier = 1f;
						}
					}
					reference6.isNewImpact = 1E-05f < reference.sumFullImpulses_InFrame && reference3.sumFullImpulses_PrevFrame < reference.sumFullImpulses_InFrame && !reference3.impulseEventWithheld;
				}
				if (featureIdxInCache == 0)
				{
					reference2.feature0 = reference.cacheValue.feature0;
					reference2.oneLess_highSpeedFactor = reference.cacheValue.oneLess_highSpeedFactor;
					reference2.highSpeedBlendTimeLeft = reference.cacheValue.highSpeedBlendTimeLeft;
				}
				else
				{
					reference2.feature1 = reference.cacheValue.feature1;
					if (reference.cacheValue.oneLess_highSpeedFactor < reference2.oneLess_highSpeedFactor)
					{
						reference2.oneLess_highSpeedFactor = reference.cacheValue.oneLess_highSpeedFactor;
					}
					if (reference.cacheValue.highSpeedBlendTimeLeft < reference2.highSpeedBlendTimeLeft)
					{
						reference2.highSpeedBlendTimeLeft = reference.cacheValue.highSpeedBlendTimeLeft;
					}
				}
				reference4 = reference3;
				reference4.sumVelImpulses_PrevFrame = reference.sumVelImpulses_InFrame;
				if (reference4.sumFullImpulses_PrevFrame * reference4.sumFullImpulses_PrevFrame * 2f * 2f < reference.sumFullImpulses_InFrame * reference.sumFullImpulses_InFrame)
				{
					reference4.numFramesWithNonZeroImpulse = 0;
				}
				reference4.sumFullImpulses_PrevFrame = reference.sumFullImpulses_InFrame;
				reference4.sumFrictionImpulses_PrevFrame = reference.sumFrictionImpulses_InFrame;
				reference4.velImpulse_SinceIntegration = reference.velImpulse_SinceIntegration;
				reference4.fullImpulse_SinceIntegration = reference.fullImpulse_SinceIntegration;
				reference4.frictionImpulse_SinceIntegration = reference.frictionImpulse_SinceIntegration;
				reference4.impulseEventWithheld = flag;
				if (0f != reference.sumFullImpulses_InFrame)
				{
					reference4.numFramesWithNonZeroImpulse++;
				}
				else
				{
					reference4.numFramesWithNonZeroImpulse = 0;
				}
				if (0 <= reference.collisionEventIdx)
				{
					ref CollisionEvent collisionEvent = ref collisionEvents.array[reference.collisionEventIdx];
					if (reference.hasSecondPoint)
					{
						continue;
					}
					if (reference2.numContactPoints > 0 && reference.cacheValue.numContactPoints > 0)
					{
						reference2.Stay(ref reference, ref collisionEvent);
					}
					while (reference2.numContactPoints < reference.cacheValue.numContactPoints)
					{
						reference2.AddPoint(ref reference, ref collisionEvent);
						reference2.numContactPoints++;
					}
					while (reference.cacheValue.numContactPoints < reference2.numContactPoints)
					{
						if (reference2.numContactPoints == 1)
						{
							reference2.Clear_AndTriggerExitCallbacks(reference.isReversed ? new Vec2Short(reference.shapeHandleIdx1, reference.shapeHandleIdx0) : new Vec2Short(reference.shapeHandleIdx0, reference.shapeHandleIdx1));
						}
						else
						{
							reference2.numContactPoints--;
						}
					}
				}
				else
				{
					reference2.numContactPoints = reference.cacheValue.numContactPoints;
				}
			}
		}

		[Obsolete]
		public void UpdateCachesFromCollisionInfos_Particles(FastList<CollisionInfo> collisionInfos)
		{
			CollisionInfo[] array = collisionInfos.array;
			for (int i = 0; i < collisionInfos.Count; i++)
			{
				caches.array[array[i].cacheIndex] = array[i].cacheValue;
			}
		}
	}
}
