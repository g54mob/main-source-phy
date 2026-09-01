using System.Collections.Generic;
using Poly.Base;
using Poly.Physics;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using UnityEngine;

namespace Poly.Solver
{
	public class EdgeSolverJobs : MonoBehaviour
	{
		[BurstCompile(CompileSynchronously = true)]
		private struct SolveVelocityAndPositionJob : IJob
		{
			public NativeArray<SolverEdge> edges;

			public NativeArray<Vec2> vel;

			public bool limitImpulsesInSolverOverride;

			public float impulseLimitMultiplier;

			public unsafe void Execute()
			{
				SolverEdge* unsafePtr = (SolverEdge*)edges.GetUnsafePtr();
				Vec2* unsafePtr2 = (Vec2*)vel.GetUnsafePtr();
				int length = edges.Length;
				for (int i = 0; i < length; i++)
				{
					SolverEdge* ptr = unsafePtr + i;
					Vec2* ptr2 = unsafePtr2 + ptr->nodeIdxA;
					Vec2* ptr3 = unsafePtr2 + ptr->nodeIdxB;
					float directionX = ptr->directionX;
					float directionY = ptr->directionY;
					float num = directionX * (ptr3->x - ptr2->x) + directionY * (ptr3->y - ptr2->y);
					num -= ptr->lengthVelocity;
					float num2 = ptr->cachedPosError;
					float num3;
					float num4;
					if (!ptr->isRope)
					{
						num3 = num2 * ptr->virtualMass_Stiffness_Tau;
						num4 = num * ptr->virtualMass_Damping_Damping;
					}
					else
					{
						if (num2 < -1.001f)
						{
							num += num2 + 1.001f;
							num2 = -1.001f;
						}
						num3 = num2 * ptr->virtualMass_Stiffness_Tau;
						num4 = num * ptr->virtualMass_Damping_Damping;
						float sumFullImpulses = ptr->sumFullImpulses;
						float num5 = num4 + num3;
						float num6 = num5 + sumFullImpulses;
						if (num6 < 0f)
						{
							num6 = 0f;
						}
						num6 -= sumFullImpulses;
						if (1E-12f < num5 * num5)
						{
							float num7 = num6 / num5;
							num4 *= num7;
							num3 *= num7;
						}
						else
						{
							num4 = num6 - num3;
						}
					}
					float num8 = num3 + num4;
					if (limitImpulsesInSolverOverride && 1E-12f < num8 * num8)
					{
						float num9 = num8;
						float num10 = ptr->maxImpulsePerIntegration * impulseLimitMultiplier * ptr->impulseLimitFactor;
						float num11 = 0f - num10 - ptr->sumFullImpulses;
						float num12 = num10 * ptr->maxTensionImpulseFactor - ptr->sumFullImpulses;
						num8 = ((num8 < num11) ? num11 : ((num12 < num8) ? num12 : num8));
						float num13 = num8 / num9;
						num4 *= num13;
						ptr->isForceClamped = num13 != 1f;
					}
					if (ptr->pin_isUsing2d)
					{
						float num14 = ptr3->x - ptr2->x;
						float num15 = ptr3->y - ptr2->y;
						float num16 = num14 * ptr->virtualMass_Damping_Damping;
						float num17 = num15 * ptr->virtualMass_Damping_Damping;
						float num18 = ptr->cachedPosError_X * ptr->virtualMass_Stiffness_Tau;
						float num19 = ptr->cachedPosError_Y * ptr->virtualMass_Stiffness_Tau;
						float num20 = num16 + num18;
						float num21 = num17 + num19;
						ptr2->x += ptr->invMassA * num20;
						ptr2->y += ptr->invMassA * num21;
						ptr3->x -= ptr->invMassB * num20;
						ptr3->y -= ptr->invMassB * num21;
						ptr->sumVelImpulses2d_X += num16;
						ptr->sumVelImpulses2d_Y += num17;
						num8 = 0f;
						num4 = 0f;
					}
					ptr->sumVelImpulses += num4;
					ptr->sumFullImpulses += num8;
					float num22 = ptr->invMassA * num8;
					float num23 = ptr->invMassB * num8;
					ptr2->x += num22 * directionX;
					ptr2->y += num22 * directionY;
					ptr3->x -= num23 * directionX;
					ptr3->y -= num23 * directionY;
				}
			}
		}

		public unsafe static void SolveVelocityAndPosition_AllIterations(in SolverSettings settings, in EdgeSolverInput input, List<DynamicAnchorJoint> dynamicAnchorJoints, List<Poly.Physics.Joint> joints, List<Poly.Physics.Joint> customShapeJoints, SolverNode[] nodesPtr, Motion[] motionsPtr, int nodesCount, CollisionInfo[] bridgeCollisions, FastList<int> fullFrequencyBridgeContactIndices, int numBridgeCollisions, bool isFirstAfterIntegration, bool usingContactWarmstarting)
		{
			_ = input.settings.useSharedLimitForEntireFrameDuration;
			bool flag = settings.solveDynamicAnchorsInBridgeSolver && settings.dynamicAnchors.useJointWarmstarting;
			SolveVelocityAndPositionJob jobData = new SolveVelocityAndPositionJob
			{
				edges = new NativeArray<SolverEdge>(input.edges, Allocator.TempJob),
				vel = new NativeArray<Vec2>(nodesCount, Allocator.TempJob),
				limitImpulsesInSolverOverride = (input.settings.limitImpulsesInSolver && input.areEdgesBreakable),
				impulseLimitMultiplier = input.settings.inSolverImpulseLimitMultiplier
			};
			SolverEdge* unsafePtr = (SolverEdge*)jobData.edges.GetUnsafePtr();
			SolverEdge* ptr = unsafePtr + jobData.edges.Length;
			Vec2* unsafePtr2 = (Vec2*)jobData.vel.GetUnsafePtr();
			_ = nodesCount;
			fixed (SolverNode* ptr2 = &nodesPtr[0])
			{
				SolverNode* ptr3 = ptr2 + nodesCount;
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
					Vec2* ptr4 = unsafePtr2;
					SolverNode* ptr5 = ptr2;
					while (ptr5 < ptr3)
					{
						*ptr4 = ptr5->vel;
						ptr5++;
						ptr4++;
					}
					jobData.Run();
					Vec2* ptr6 = unsafePtr2;
					for (SolverNode* ptr7 = ptr2; ptr7 < ptr3; ptr7++)
					{
						ptr7->vel = *ptr6;
						ptr6++;
					}
				}
				if (input.edges.Length != 0)
				{
					fixed (SolverEdge* ptr8 = &input.edges[0])
					{
						SolverEdge* ptr9 = unsafePtr;
						SolverEdge* ptr10 = ptr8;
						while (ptr9 < ptr)
						{
							*ptr10 = *ptr9;
							ptr9++;
							ptr10++;
						}
					}
				}
			}
			jobData.edges.Dispose();
			jobData.vel.Dispose();
		}

		public static void SolveVelocityAndPosition(in EdgeSolverInput input)
		{
		}

		private static void _Single_SolveVelocityAndPosition_unused_unmaintained(ref SolverEdge edge, ref SolverNode nodeA, ref SolverNode nodeB, float impulseLimitMultiplier)
		{
			float directionX = edge.directionX;
			float directionY = edge.directionY;
			float num = directionX * (nodeB.vel.x - nodeA.vel.x) + directionY * (nodeB.vel.y - nodeA.vel.y);
			num -= edge.lengthVelocity;
			float num2 = edge.cachedPosError;
			float num3;
			float num4;
			if (!edge.isRope)
			{
				num3 = num2 * edge.virtualMass_Stiffness_Tau;
				num4 = num * edge.virtualMass_Damping_Damping;
			}
			else
			{
				if (num2 < -1.001f)
				{
					num += num2 + 1.001f;
					num2 = -1.001f;
				}
				num3 = num2 * edge.virtualMass_Stiffness_Tau;
				num4 = num * edge.virtualMass_Damping_Damping;
				float sumFullImpulses = edge.sumFullImpulses;
				float num5 = num4 + num3;
				float num6 = num5 + sumFullImpulses;
				if (num6 < 0f)
				{
					num6 = 0f;
				}
				num6 -= sumFullImpulses;
				if (1E-12f < num5 * num5)
				{
					float num7 = num6 / num5;
					num4 *= num7;
					num3 *= num7;
				}
				else
				{
					num4 = num6 - num3;
				}
			}
			float num8 = num3 + num4;
			if (1E-12f < num8 * num8)
			{
				float num9 = num8;
				float num10 = edge.maxImpulsePerIntegration * impulseLimitMultiplier;
				float num11 = 0f - num10 - edge.sumFullImpulses;
				float num12 = num10 * edge.maxTensionImpulseFactor - edge.sumFullImpulses;
				num8 = ((num8 < num11) ? num11 : ((num12 < num8) ? num12 : num8));
				float num13 = num8 / num9;
				num4 *= num13;
				edge.isForceClamped = num13 != 1f;
			}
			edge.sumVelImpulses += num4;
			edge.sumFullImpulses += num8;
			float num14 = nodeA.invMass * num8;
			float num15 = nodeB.invMass * num8;
			nodeA.vel.x += num14 * directionX;
			nodeA.vel.y += num14 * directionY;
			nodeB.vel.x -= num15 * directionX;
			nodeB.vel.y -= num15 * directionY;
		}
	}
}
