using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Pb;
using Poly.Physics;
using UnityEngine;

namespace Poly.Solver
{
	[Serializable]
	[DebuggerDisplay("com: {com} ang: {angle} vel: {linVel} avel: {angVel} im: {invMass} ii: {invInertia}")]
	public struct Motion
	{
		public const bool useContinuousEdgeAngle = true;

		public static bool warnOnceAboutContinuousEdgeAngleTooBig = true;

		public Vec2 com;

		public Vec2 linVel;

		public float angle;

		public float angVel;

		public float invMass;

		public float invInertia;

		public SegmentMotionRef segment;

		public void SetZeroVelocity()
		{
			linVel = Vec2.zero;
			angVel = 0f;
		}

		public static void ComputeEdge_ComT_Mass_Inertia(EdgeHandle edge, ref Motion outMotion)
		{
			NodeHandle node = edge.node0;
			NodeHandle node2 = edge.node1;
			if (outMotion.segment == null)
			{
				outMotion.segment = new SegmentMotionRef();
				outMotion.segment.worldIdx0 = node.worldIdx;
				outMotion.segment.worldIdx1 = node2.worldIdx;
				outMotion.segment.currentStretchedLength = edge.solverEdge.length;
			}
			float num = edge.solverEdge.length * edge.solverEdge.length;
			if (node.solverNode.invMass * node2.solverNode.invMass != 0f)
			{
				float num2 = node2.mass / (node.mass + node2.mass);
				outMotion.segment.comT = num2;
				outMotion.invMass = 1f / (node.mass + node2.mass);
				float num3 = num * (num2 * num2 * node.mass + (1f - num2) * (1f - num2) * node2.mass);
				outMotion.invInertia = 1f / num3;
			}
			else if (node.solverNode.invMass + node2.solverNode.invMass != 0f)
			{
				outMotion.segment.comT = ((node.solverNode.invMass == 0f) ? 0f : 1f);
				outMotion.invMass = 0f;
				float num4 = ((node.solverNode.invMass == 0f) ? (num * node2.mass) : (num * node.mass));
				outMotion.invInertia = 1f / num4;
			}
			else
			{
				outMotion.segment.comT = 0.5f;
				outMotion.invMass = 0f;
				outMotion.invInertia = 0f;
			}
		}

		public static void ComputeFromNode(in SolverNode node, out Motion result, float nodeToMotionVelocityMultiplier)
		{
			result.com = node.pos;
			result.linVel = node.vel * nodeToMotionVelocityMultiplier;
			result.angle = 0f;
			result.angVel = 0f;
			result.invMass = node.invMass;
			result.invInertia = 0f;
			result.segment = null;
		}

		public static Motion ConvertNodesToMotion_OutsideSolver(ref SolverNode segmentNodeA, ref SolverNode segmentNodeB, ref Motion result, float nodeToMotionVelocityMultiplier)
		{
			result.com = Vec2.LerpUnclamped(in segmentNodeA.pos, in segmentNodeB.pos, result.segment.comT);
			result.linVel = Vec2.LerpUnclamped(in segmentNodeA.vel, in segmentNodeB.vel, result.segment.comT) * nodeToMotionVelocityMultiplier;
			Vec2 vec = segmentNodeB.pos - segmentNodeA.pos;
			result.angle = (float)System.Math.Atan2(vec.y, vec.x);
			float num = 0f;
			if (result.invInertia != 0f)
			{
				Vec2 vec2 = segmentNodeB.pos - segmentNodeB.vel - (segmentNodeA.pos - segmentNodeA.vel);
				float num2 = (float)System.Math.Atan2(vec2.y, vec2.x);
				float num3 = result.angle - num2;
				if (num3 <= -MathF.PI)
				{
					num3 += MathF.PI * 2f;
				}
				else if (num3 > MathF.PI)
				{
					num3 -= MathF.PI * 2f;
				}
				num = num3;
			}
			result.angVel = num * nodeToMotionVelocityMultiplier;
			float num4 = result.angle - result.segment.lastConvertedAngleRef;
			num4 = Pb.Mathf.WrapAngleToOnePi_Slow(num4);
			result.angle = num4 + result.segment.lastConvertedAngleRef;
			result.segment.lastConvertedAngleRef = result.angle;
			return result;
		}

		[Obsolete]
		public static void ConvertNodesToMotion_InSolver(int motionIdx, SolverNode[] nodesPtr, Motion[] motionsPtr, float velocityMultiplier)
		{
			ref Motion reference = ref motionsPtr[motionIdx];
			ref SolverNode reference2 = ref nodesPtr[reference.segment.worldIdx0];
			ref SolverNode reference3 = ref nodesPtr[reference.segment.worldIdx1];
			reference.com = Vec2.LerpUnclamped(in reference2.pos, in reference3.pos, reference.segment.comT);
			reference.linVel = Vec2.LerpUnclamped(in reference2.vel, in reference3.vel, reference.segment.comT) * velocityMultiplier;
			Vec2.setSub(in reference3.pos, in reference2.pos, out var v);
			float num = reference.angle;
			reference.angle = (float)System.Math.Atan2(v.y, v.x);
			float num2 = reference.angle - num;
			num2 = Pb.Mathf.WrapAngleToOnePi_Slow(num2);
			reference.angle = num2 + num;
			reference.segment.lastConvertedAngleRef = reference.angle;
			float num3 = 0f;
			if (reference.invInertia != 0f)
			{
				Vec2 vec = reference3.pos - reference3.vel - (reference2.pos - reference2.vel);
				float num4 = (float)System.Math.Atan2(vec.y, vec.x);
				float num5 = reference.angle - num4;
				if (num5 <= -MathF.PI)
				{
					num5 += MathF.PI * 2f;
				}
				else if (num5 > MathF.PI)
				{
					num5 -= MathF.PI * 2f;
				}
				num3 = num5;
			}
			reference.angVel = num3 * velocityMultiplier;
		}

		public static void ConvertNodesToMotion_InSolver_ComOnly(int motionIdx, SolverNode[] nodesPtr, Motion[] motionsPtr, float velocityMultiplier)
		{
			ref Motion reference = ref motionsPtr[motionIdx];
			ref SolverNode reference2 = ref nodesPtr[reference.segment.worldIdx0];
			ref SolverNode reference3 = ref nodesPtr[reference.segment.worldIdx1];
			reference.com = Vec2.LerpUnclamped(in reference2.pos, in reference3.pos, reference.segment.comT);
			Vec2.setSub(in reference3.pos, in reference2.pos, out var v);
			float num = reference.angle;
			reference.angle = (float)System.Math.Atan2(v.y, v.x);
			float num2 = reference.angle - num;
			num2 = Pb.Mathf.WrapAngleToOnePi_Slow(num2);
			reference.angle = num2 + num;
			reference.segment.lastConvertedAngleRef = reference.angle;
		}

		public static void ConvertNodesToMotion_InSolver_VelOnly(int motionIdx, SolverNode[] nodesPtr, Motion[] motionsPtr, float velocityMultiplier)
		{
			ref Motion reference = ref motionsPtr[motionIdx];
			ref SolverNode reference2 = ref nodesPtr[reference.segment.worldIdx0];
			ref SolverNode reference3 = ref nodesPtr[reference.segment.worldIdx1];
			reference.linVel = Vec2.LerpUnclamped(in reference2.vel, in reference3.vel, reference.segment.comT) * velocityMultiplier;
			float num = 0f;
			if (reference.invInertia != 0f)
			{
				Vec2 vec = reference3.pos - reference2.pos;
				Vec2 b = reference3.vel - reference2.vel;
				num = Vec2.Dot(vec.rotated90, in b) / vec.sqrMagnitude;
				float num2 = num;
				float num3 = 0.3141593f;
				num = Pb.Mathf.Clamp(num, 0f - num3, num3);
			}
			reference.angVel = num * velocityMultiplier;
		}

		public Vec2 GetPointVelocity(Vec2 pointInWorld)
		{
			Vec2 result = default(Vec2);
			result.x = linVel.x - angVel * (pointInWorld.y - com.y);
			result.y = linVel.y + angVel * (pointInWorld.x - com.x);
			return result;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void ApplyImpulse(Vec2 point, Vec2 impulse)
		{
			linVel.x += impulse.x * invMass;
			linVel.y += impulse.y * invMass;
			angVel += ((point.x - com.x) * impulse.y - (point.y - com.y) * impulse.x) * invInertia;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void ApplyAngularImpulse(float angularImpulse)
		{
			angVel += angularImpulse * invInertia;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void UpdateNodeVelocities_SegmentFast(Vec2 dCom, float dAngle, SolverNode[] nodesInSolver, float motionToNodeVelocityMultiplier)
		{
			nodesInSolver[segment.worldIdx0].vel += (segment.angleToNode0 * dAngle + dCom) * motionToNodeVelocityMultiplier;
			nodesInSolver[segment.worldIdx1].vel += (segment.angleToNode1 * dAngle + dCom) * motionToNodeVelocityMultiplier;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void UpdateNodePositions_SegmentFast(Vec2 dCom, float dAngle, SolverNode[] nodesInSolver)
		{
			nodesInSolver[segment.worldIdx0].pos += segment.angleToNode0 * dAngle + dCom;
			nodesInSolver[segment.worldIdx1].pos += segment.angleToNode1 * dAngle + dCom;
		}

		public Vec2 TransformPoint_Slow(Vec2 point)
		{
			Quaternion quaternion = Quaternion.AngleAxis(angle * 57.29578f, Vector3.forward);
			return com + (Vec2)(quaternion * point);
		}

		public Vec2 InverseTransformPoint_Slow(Vec2 point)
		{
			return (Vec2)(Quaternion.AngleAxis((0f - angle) * 57.29578f, Vector3.forward) * (point - com));
		}

		public Vec2 TransformDirection_Slow(Vec2 direction)
		{
			return (Vec2)(Quaternion.AngleAxis(angle * 57.29578f, Vector3.forward) * direction);
		}

		public Vec2 InverseTransformDirection_Slow(Vec2 direction)
		{
			return (Vec2)(Quaternion.AngleAxis((0f - angle) * 57.29578f, Vector3.forward) * direction);
		}

		[Obsolete]
		public static Motion ComputeFromPointMasses2D(NodeHandle[] nodes, float[] nodeRadius, float rbAngleDeg, ref Motion result, bool pointMassesHaveInertia = false)
		{
			int num = -1;
			int num2 = 0;
			float num3 = 0f;
			Vec2 zero = Vec2.zero;
			Vec2 zero2 = Vec2.zero;
			for (int i = 0; i < nodes.Length; i++)
			{
				if (nodes[i].isKinematic)
				{
					num = i;
					num2++;
					continue;
				}
				float mass = nodes[i].mass;
				num3 += mass;
				zero += mass * nodes[i].pos;
				zero2 += mass * nodes[i].solverNode.vel;
			}
			if (num2 == 0)
			{
				result.invMass = 1f / num3;
				result.com = zero / num3;
				result.linVel = zero2 / num3;
			}
			else
			{
				result.invMass = 0f;
				result.com = nodes[num].pos;
				result.linVel = nodes[num].solverNode.vel;
			}
			result.angle = rbAngleDeg * (MathF.PI / 180f);
			if (num2 < 2)
			{
				float num4 = 0f;
				Vector3 zero3 = Vector3.zero;
				for (int j = 0; j < nodes.Length; j++)
				{
					if (!nodes[j].isKinematic)
					{
						float mass2 = nodes[j].mass;
						Vec2 vec = nodes[j].pos - result.com;
						Vec2 vec2 = nodes[j].solverNode.vel - result.linVel;
						num4 += mass2 * vec.sqrMagnitude;
						zero3 += mass2 * Vector3.Cross(vec, vec2);
						if (pointMassesHaveInertia)
						{
							float num5 = 0.5f * mass2 * nodeRadius[j] * nodeRadius[j];
							num4 += num5;
						}
					}
				}
				result.invInertia = ((num4 != 0f) ? (1f / num4) : 0f);
				result.angVel = zero3.z * result.invInertia;
			}
			else
			{
				result.invInertia = 0f;
				result.angVel = 0f;
			}
			return result;
		}
	}
}
