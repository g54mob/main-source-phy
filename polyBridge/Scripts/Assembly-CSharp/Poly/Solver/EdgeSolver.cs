using System;
using Pb;

namespace Poly.Solver
{
	public static class EdgeSolver
	{
		public static void CacheNormals(in EdgeSolverInput input, bool gatherSumImpulses)
		{
			float edgeTau = input.settings.edgeTau;
			float edgeDamping = input.settings.edgeDamping;
			bool applyBreakageInSolver = input.settings.applyBreakageInSolver && input.areEdgesBreakable;
			float num = (gatherSumImpulses ? 1f : 0f);
			for (int i = 0; i < input.numEdges; i++)
			{
				ref SolverEdge reference = ref input.edges[i];
				_Single_CacheNormals(ref reference, in input.nodes[reference.nodeIdxA], in input.nodes[reference.nodeIdxB], edgeTau, edgeDamping, applyBreakageInSolver);
				reference.sumFullImpulsesInFrame += reference.sumFullImpulses;
				reference.sumFullImpulsesInFrame *= num;
				if (reference.pin_isUsing2d)
				{
					reference.sumVelImpulses2d_X = 0f;
					reference.sumVelImpulses2d_Y = 0f;
				}
				reference.sumVelImpulses = 0f;
				reference.sumFullImpulses = 0f;
			}
		}

		public static void CacheNormalsAndWarmStart(in EdgeSolverInput input, bool gatherSumImpulses)
		{
			float edgeTau = input.settings.edgeTau;
			float edgeDamping = input.settings.edgeDamping;
			bool applyBreakageInSolver = input.settings.applyBreakageInSolver && input.areEdgesBreakable;
			float num = (gatherSumImpulses ? 1f : 0f);
			for (int i = 0; i < input.numEdges; i++)
			{
				ref SolverEdge reference = ref input.edges[i];
				ref SolverNode reference2 = ref input.nodes[reference.nodeIdxA];
				ref SolverNode reference3 = ref input.nodes[reference.nodeIdxB];
				_Single_CacheNormals(ref reference, in reference2, in reference3, edgeTau, edgeDamping, applyBreakageInSolver);
				reference.sumFullImpulsesInFrame += reference.sumFullImpulses;
				reference.sumFullImpulsesInFrame *= num;
				if (!reference.isBroken && !reference.isSpring)
				{
					if (!reference.pin_isUsing2d)
					{
						reference.sumVelImpulses *= input.settings.warmStartingRatio;
						reference.sumFullImpulses = reference.sumVelImpulses;
						float directionX = reference.directionX;
						float directionY = reference.directionY;
						float num2 = reference2.invMass * reference.sumVelImpulses;
						float num3 = reference3.invMass * reference.sumVelImpulses;
						reference2.vel.x += num2 * directionX;
						reference2.vel.y += num2 * directionY;
						reference3.vel.x -= num3 * directionX;
						reference3.vel.y -= num3 * directionY;
					}
					else
					{
						reference.sumVelImpulses2d_X *= input.settings.warmStartingRatio;
						reference.sumVelImpulses2d_Y *= input.settings.warmStartingRatio;
						reference.sumVelImpulses = 0f;
						reference.sumFullImpulses = 0f;
						reference2.vel.x += reference2.invMass * reference.sumVelImpulses2d_X;
						reference2.vel.y += reference2.invMass * reference.sumVelImpulses2d_Y;
						reference3.vel.x -= reference3.invMass * reference.sumVelImpulses2d_X;
						reference3.vel.y -= reference3.invMass * reference.sumVelImpulses2d_Y;
					}
				}
				else
				{
					reference.sumVelImpulses = 0f;
					reference.sumFullImpulses = 0f;
				}
			}
		}

		public static void SolveVelocityAndPosition(in EdgeSolverInput input)
		{
			bool limitImpulsesInSolverOverride = input.settings.limitImpulsesInSolver && input.areEdgesBreakable;
			float inSolverImpulseLimitMultiplier = input.settings.inSolverImpulseLimitMultiplier;
			bool useSharedLimitForEntireFrameDuration = input.settings.useSharedLimitForEntireFrameDuration;
			int numEdgeIntegrationsPerFrame = input.settings.numEdgeIntegrationsPerFrame;
			float ropeSlop = input.settings.ropeSlop;
			for (int i = 0; i < input.numEdges; i++)
			{
				ref SolverEdge reference = ref input.edges[i];
				_Single_SolveVelocityAndPosition(ref reference, ref input.nodes[reference.nodeIdxA], ref input.nodes[reference.nodeIdxB], limitImpulsesInSolverOverride, inSolverImpulseLimitMultiplier, useSharedLimitForEntireFrameDuration, numEdgeIntegrationsPerFrame, ropeSlop);
			}
		}

		public static void SolvePosition(in EdgeSolverInput input)
		{
			float posEdgeTau = input.settings.posEdgeTau;
			for (int i = 0; i < input.numEdges; i++)
			{
				ref SolverEdge reference = ref input.edges[i];
				_Single_SolvePosition(ref reference, ref input.nodes[reference.nodeIdxA], ref input.nodes[reference.nodeIdxB], posEdgeTau);
			}
		}

		private static void _Single_CacheNormals(ref SolverEdge edge, in SolverNode nodeA, in SolverNode nodeB, float solverTau, float solverDamping, bool applyBreakageInSolver)
		{
			float num = nodeB.pos.x - nodeA.pos.x;
			float num2 = nodeB.pos.y - nodeA.pos.y;
			float num3 = (float)System.Math.Sqrt(num * num + num2 * num2);
			float num4 = 1f / (num3 + 5.877472E-39f);
			edge.virtualMass_Stiffness_Tau = edge.virtualMass * edge.stiffness * solverTau;
			edge.virtualMass_Damping_Damping = edge.virtualMass * edge.damping * solverDamping;
			float a = (edge.cachedPosError = num3 - edge.length);
			if (edge.isForceClamped && !edge.pin_isUnbreakable)
			{
				edge.wasForceClampedDuringFrame = true;
				if (applyBreakageInSolver)
				{
					edge.isBroken = true;
					edge.stiffness = 0f;
					edge.damping = 0f;
				}
			}
			if (edge.pin_isUnbreakable)
			{
				bool pin_isUsing2d = edge.pin_isUsing2d;
				edge.pin_isUsing2d = edge.length == 0f && Mathf.Abs(a) < 0.05f && edge.maxImpulsePerIntegration == float.PositiveInfinity;
				if (pin_isUsing2d ^ edge.pin_isUsing2d)
				{
					if (edge.pin_isUsing2d)
					{
						edge.sumVelImpulses2d_X = num * num4 * edge.sumVelImpulses;
						edge.sumVelImpulses2d_Y = num2 * num4 * edge.sumVelImpulses;
					}
					else
					{
						edge.sumVelImpulses = (num * edge.sumVelImpulses2d_X + num2 * edge.sumVelImpulses2d_Y) * num4;
						edge.lengthVelocity = 0f;
					}
				}
				if (!edge.pin_isUsing2d)
				{
					edge.directionX = num * num4;
					edge.directionY = num2 * num4;
				}
				else
				{
					edge.cachedPosError_X = num;
					edge.cachedPosError_Y = num2;
				}
			}
			else
			{
				edge.directionX = num * num4;
				edge.directionY = num2 * num4;
			}
		}

		private static void _Single_SolveVelocityAndPosition(ref SolverEdge edge, ref SolverNode nodeA, ref SolverNode nodeB, bool limitImpulsesInSolverOverride, float limitMultiplier, bool useSharedLimitForEntireFrameDuration, int numEdgeIntegrationsPerFrame, float ropeSlop)
		{
			float directionX = edge.directionX;
			float directionY = edge.directionY;
			float num = directionX * (nodeB.vel.x - nodeA.vel.x) + directionY * (nodeB.vel.y - nodeA.vel.y);
			num -= edge.lengthVelocity;
			float num2 = edge.cachedPosError;
			if (edge.isRope)
			{
				if (num2 < 0f - ropeSlop)
				{
					num += num2 + ropeSlop;
					num2 = 0f - ropeSlop;
				}
			}
			float num3 = num2 * edge.virtualMass_Stiffness_Tau;
			float num4 = num * edge.virtualMass_Damping_Damping;
			if (edge.isRope)
			{
				float num5 = edge.sumFullImpulses + (useSharedLimitForEntireFrameDuration ? edge.sumFullImpulsesInFrame : 0f);
				float num6 = num4 + num3;
				float num7 = num6 + num5;
				if (num7 < 0f)
				{
					num7 = 0f;
				}
				num7 -= num5;
				if (1E-12f < num6 * num6)
				{
					float num8 = num7 / num6;
					num4 *= num8;
					num3 *= num8;
				}
				else
				{
					num4 = num7 - num3;
				}
			}
			float num9 = num3 + num4;
			if (limitImpulsesInSolverOverride && 1E-12f < num9 * num9)
			{
				float num10 = num9;
				float num11 = edge.maxImpulsePerIntegration * limitMultiplier * edge.impulseLimitFactor;
				float num12 = 0f - num11 - edge.sumFullImpulses;
				float num13 = num11 * edge.maxTensionImpulseFactor - edge.sumFullImpulses;
				if (useSharedLimitForEntireFrameDuration)
				{
					num11 *= (float)numEdgeIntegrationsPerFrame;
					num12 = 0f - num11 - edge.sumFullImpulses - edge.sumFullImpulsesInFrame;
					num13 = num11 * edge.maxTensionImpulseFactor - edge.sumFullImpulses - edge.sumFullImpulsesInFrame;
				}
				num9 = ((num9 < num12) ? num12 : ((num13 < num9) ? num13 : num9));
				float num14 = num9 / num10;
				num4 *= num14;
				edge.isForceClamped = num14 != 1f;
			}
			if (edge.pin_isUsing2d)
			{
				float num15 = nodeB.vel.x - nodeA.vel.x;
				float num16 = nodeB.vel.y - nodeA.vel.y;
				float num17 = num15 * edge.virtualMass_Damping_Damping;
				float num18 = num16 * edge.virtualMass_Damping_Damping;
				float num19 = nodeB.pos.x - nodeA.pos.x;
				float num20 = nodeB.pos.y - nodeA.pos.y;
				float num21 = num19 * edge.virtualMass_Stiffness_Tau;
				float num22 = num20 * edge.virtualMass_Stiffness_Tau;
				float num23 = num17 + num21;
				float num24 = num18 + num22;
				nodeA.vel.x += nodeA.invMass * num23;
				nodeA.vel.y += nodeA.invMass * num24;
				nodeB.vel.x -= nodeB.invMass * num23;
				nodeB.vel.y -= nodeB.invMass * num24;
				edge.sumVelImpulses2d_X += num17;
				edge.sumVelImpulses2d_Y += num18;
				num9 = 0f;
				num4 = 0f;
			}
			edge.sumVelImpulses += num4;
			edge.sumFullImpulses += num9;
			float num25 = nodeA.invMass * num9;
			float num26 = nodeB.invMass * num9;
			nodeA.vel.x += num25 * directionX;
			nodeA.vel.y += num25 * directionY;
			nodeB.vel.x -= num26 * directionX;
			nodeB.vel.y -= num26 * directionY;
		}

		private static void _Single_SolvePosition(ref SolverEdge edge, ref SolverNode nodeA, ref SolverNode nodeB, float solverPosTau)
		{
			if (!edge.isSpring)
			{
				float num = nodeB.pos.x - nodeA.pos.x;
				float num2 = nodeB.pos.y - nodeA.pos.y;
				float num3 = (float)System.Math.Sqrt(num * num + num2 * num2);
				float num4 = 1f / (num3 + 5.877472E-39f);
				float num5 = num * num4;
				float num6 = num2 * num4;
				float num7 = edge.virtualMass * edge.stiffness * solverPosTau;
				float num8 = (num3 - edge.length) * num7;
				if (edge.isRope && num8 < 0f)
				{
					num8 = 0f;
				}
				float num9 = num8;
				float num10 = nodeA.invMass * num9;
				float num11 = nodeB.invMass * num9;
				nodeA.pos.x += num10 * num5;
				nodeA.pos.y += num10 * num6;
				nodeB.pos.x -= num11 * num5;
				nodeB.pos.y -= num11 * num6;
			}
		}
	}
}
