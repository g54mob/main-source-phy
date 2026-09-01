using System.Runtime.CompilerServices;
using Poly.Solver;

namespace Poly.Physics
{
	public struct DynamicAnchorSolveProcess
	{
		public short motionIdx0;

		public short nodeIdx1;

		public Vec2 posError;

		public float errorOffset_DirX;

		public float errorOffset_DirY;

		public float deltaAngle0ToDeltaDistance_DirX;

		public float deltaAngle0ToDeltaDistance_DirY;

		public Vec2 invVirtualMass;

		public void BuildProcess_PerFrame(Rigidbody body0_in, NodeHandle node1_in, Vec2 pivotInLocal)
		{
			motionIdx0 = body0_in.worldIdx;
			nodeIdx1 = node1_in.worldIdx;
			ref Motion motion = ref body0_in.motion;
			ref SolverNode solverNode = ref node1_in.solverNode;
			Vec2 vec = body0_in._t2.rotation * pivotInLocal;
			Vec2 vec2 = motion.com + vec;
			posError = solverNode.pos - vec2;
			float num = vec.y * vec.y;
			invVirtualMass.x = solverNode.invMass + motion.invMass + motion.invInertia * num;
			float num2 = vec.x * vec.x;
			invVirtualMass.y = solverNode.invMass + motion.invMass + motion.invInertia * num2;
			deltaAngle0ToDeltaDistance_DirX = vec.rotated90.x;
			deltaAngle0ToDeltaDistance_DirY = vec.rotated90.y;
			Vec2 vec3 = Vec2.zero - vec;
			float num3 = 0f - deltaAngle0ToDeltaDistance_DirX * motion.angle;
			float num4 = 0f - deltaAngle0ToDeltaDistance_DirY * motion.angle;
			errorOffset_DirX = vec3.x - num3;
			errorOffset_DirY = vec3.y - num4;
		}

		public void RecalculatePositionErrors_PerIntegration(SolverNode[] nodesPtr, Motion[] motionsPtr)
		{
			ref Motion reference = ref motionsPtr[motionIdx0];
			ref SolverNode reference2 = ref nodesPtr[nodeIdx1];
			posError.x = reference2.pos.x - reference.com.x + 0f - deltaAngle0ToDeltaDistance_DirX * reference.angle + errorOffset_DirX;
			posError.y = reference2.pos.y - reference.com.y + 0f - deltaAngle0ToDeltaDistance_DirY * reference.angle + errorOffset_DirY;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void GetPosErrors(in Motion m0, in SolverNode n1, out Vec2 posError)
		{
			posError.x = n1.pos.x - m0.com.x + 0f - deltaAngle0ToDeltaDistance_DirX * m0.angle + errorOffset_DirX;
			posError.y = n1.pos.y - m0.com.y + 0f - deltaAngle0ToDeltaDistance_DirY * m0.angle + errorOffset_DirY;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float GetPosErrors_DirX(in Motion m0, in SolverNode n1)
		{
			return n1.pos.x - m0.com.x + 0f - deltaAngle0ToDeltaDistance_DirX * m0.angle + errorOffset_DirX;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float GetPosErrors_DirY(in Motion m0, in SolverNode n1)
		{
			return n1.pos.y - m0.com.y + 0f - deltaAngle0ToDeltaDistance_DirY * m0.angle + errorOffset_DirY;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float GetVelErrors_DirX(in Motion m0, in SolverNode n1)
		{
			return n1.vel.x - m0.linVel.x + 0f - deltaAngle0ToDeltaDistance_DirX * m0.angVel;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float GetVelErrors_DirY(in Motion m0, in SolverNode n1)
		{
			return n1.vel.y - m0.linVel.y + 0f - deltaAngle0ToDeltaDistance_DirY * m0.angVel;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void ApplyImpulse_DirX(ref Motion m0, ref SolverNode n1, float impulse)
		{
			m0.linVel.x -= impulse * m0.invMass;
			m0.angVel -= impulse * deltaAngle0ToDeltaDistance_DirX * m0.invInertia;
			n1.vel.x += impulse * n1.invMass;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void ApplyImpulse_DirY(ref Motion m0, ref SolverNode n1, float impulse)
		{
			m0.linVel.y -= impulse * m0.invMass;
			m0.angVel -= impulse * deltaAngle0ToDeltaDistance_DirY * m0.invInertia;
			n1.vel.y += impulse * n1.invMass;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void ApplyPositionCorrection_DirX(ref Motion m0, ref SolverNode n1, float impulse)
		{
			m0.com.x -= impulse * m0.invMass;
			m0.angle -= impulse * deltaAngle0ToDeltaDistance_DirX * m0.invInertia;
			n1.pos.x += impulse * n1.invMass;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void ApplyPositionCorrection_DirY(ref Motion m0, ref SolverNode n1, float impulse)
		{
			m0.com.y -= impulse * m0.invMass;
			m0.angle -= impulse * deltaAngle0ToDeltaDistance_DirY * m0.invInertia;
			n1.pos.y += impulse * n1.invMass;
		}
	}
}
