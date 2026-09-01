using System;
using System.Runtime.CompilerServices;
using Poly.Math;
using Poly.Solver;
using UnityEngine;

namespace Poly.Physics
{
	public struct WheelJointSolveProcess
	{
		public short motionIdx0;

		public short motionIdx1;

		public Vec2 posError;

		public float errorOffset_Dir0;

		public float errorOffset_Dir1;

		public Vec2 dir0;

		public Vec2 dir1;

		public float deltaAngle0ToDeltaDistance_Dir0;

		public float deltaAngle0ToDeltaDistance_Dir1;

		public float deltaAngle1ToDeltaDistance_Dir0;

		public float deltaAngle1ToDeltaDistance_Dir1;

		public Vec2 invVirtualMass;

		public float springStiffness;

		public float springDamping;

		public Vec2 prism_posError;

		public Vec2 fromLimit;

		public float prism_errorOffset_Dir1;

		public float prism_deltaAngle0ToDeltaDistance_Dir1;

		public Vec2 prism_invVirtualMass;

		public bool posErrorLimitBreached;

		public void BuildProcess_PerFrame(Rigidbody body0, Rigidbody body1, in Vec2 pivot, in Vec2 connectedPivot, in Vec2 prismaticAxis, bool enablePrismaticMovement, in Vec2 prismaticLimits, bool useSharedPivotPoint)
		{
			motionIdx0 = body0.worldIdx;
			motionIdx1 = body1.worldIdx;
			ref Poly.Solver.Motion motion = ref body0.motion;
			ref Poly.Solver.Motion motion2 = ref body1.motion;
			ref Rotation2 rotation = ref body0._t2.rotation;
			ref Rotation2 rotation2 = ref body1._t2.rotation;
			Vec2 vec = rotation * pivot;
			Vec2 vec2 = rotation2 * connectedPivot;
			Vec2 vec3 = rotation * prismaticAxis;
			dir0 = Vec2.right;
			dir1 = Vec2.up;
			if (enablePrismaticMovement)
			{
				dir0 = vec3;
				dir1 = vec3.rotated90;
			}
			Vec2 vec4 = vec;
			if (useSharedPivotPoint)
			{
				vec = motion2.com - motion.com + vec2;
			}
			deltaAngle0ToDeltaDistance_Dir0 = Vec2.Dot(in dir0, vec.rotated90);
			deltaAngle1ToDeltaDistance_Dir0 = Vec2.Dot(in dir0, vec2.rotated90);
			deltaAngle0ToDeltaDistance_Dir1 = Vec2.Dot(in dir1, vec.rotated90);
			deltaAngle1ToDeltaDistance_Dir1 = Vec2.Dot(in dir1, vec2.rotated90);
			Vec2 b = vec2 - vec4;
			float num = deltaAngle1ToDeltaDistance_Dir0 * motion2.angle - deltaAngle0ToDeltaDistance_Dir0 * motion.angle;
			float num2 = deltaAngle1ToDeltaDistance_Dir1 * motion2.angle - deltaAngle0ToDeltaDistance_Dir1 * motion.angle;
			errorOffset_Dir0 = Vec2.Dot(in dir0, in b) - num;
			errorOffset_Dir1 = Vec2.Dot(in dir1, in b) - num2;
			invVirtualMass.x = JointUtil.ComputeInverseVirtualMass(in motion, in motion2, motion.com + vec, motion2.com + vec2, in dir0);
			invVirtualMass.y = JointUtil.ComputeInverseVirtualMass(in motion, in motion2, motion.com + vec, motion2.com + vec2, in dir1);
			if (enablePrismaticMovement)
			{
				Vec2 vec5;
				if (useSharedPivotPoint)
				{
					vec5 = vec;
				}
				else
				{
					Vec2 vec6 = motion.com + vec;
					float num3 = Mathf.Clamp(Vec2.Dot(motion2.com + vec2 - vec6, in dir0), prismaticLimits.x, prismaticLimits.y);
					vec5 = vec + num3 * dir0;
				}
				prism_deltaAngle0ToDeltaDistance_Dir1 = Vec2.Dot(in dir1, vec5.rotated90);
				prism_invVirtualMass.x = invVirtualMass.x;
				prism_invVirtualMass.y = JointUtil.ComputeInverseVirtualMass(in motion, in motion2, motion.com + vec5, motion2.com + vec2, in dir1);
				float num4 = deltaAngle1ToDeltaDistance_Dir1 * motion2.angle - prism_deltaAngle0ToDeltaDistance_Dir1 * motion.angle;
				Vec2 b2 = vec2 - vec4;
				prism_errorOffset_Dir1 = Vec2.Dot(in dir1, in b2) - num4;
			}
		}

		public void ComputeAndCacheSpringParams(float springConstant, float dampingConstant, float dampingConstantMultiplier, float regressionFixup_DampingMultiplier, SolverSettings settings)
		{
			JointSolverSettings joints = settings.joints;
			float deltaTimeForVelocity = settings.deltaTimeForVelocity;
			float x = invVirtualMass.x;
			float num = 1f / (x + 5.877472E-39f);
			float num2 = (settings.integrateInSolverIterations ? 1f : ((float)settings.numIterations));
			float num3 = springConstant * deltaTimeForVelocity * deltaTimeForVelocity;
			float num4 = dampingConstant * dampingConstantMultiplier * regressionFixup_DampingMultiplier * deltaTimeForVelocity;
			float num5 = Mathf.Max(0.01f, joints.jointTau);
			num3 *= x / num5;
			num4 *= x / joints.jointDamping;
			num4 /= num2;
			if (num5 < 0.5f)
			{
				num3 *= num5 / 0.5f;
				num3 = Mathf.Clamp01(num3);
				num4 = Mathf.Clamp01(num4);
				num3 /= num5 / 0.5f;
			}
			else
			{
				num3 = Mathf.Clamp01(num3);
				num4 = Mathf.Clamp01(num4);
			}
			num3 *= num5 * num / num2;
			num4 *= joints.jointDamping * num;
			springStiffness = num3;
			springDamping = num4;
		}

		public void RecalculatePositionErrors_PerIntegration(Poly.Solver.Motion[] motionsPtr, in Vec2 pivot, in Vec2 connectedPivot, in Vec2 prismaticAxis, bool enablePrismaticMovement, in Vec2 prismaticLimits)
		{
			ref Poly.Solver.Motion m = ref motionsPtr[motionIdx0];
			ref Poly.Solver.Motion m2 = ref motionsPtr[motionIdx1];
			GetPosErrors(in m, in m2, out posError);
			if (enablePrismaticMovement)
			{
				prism_GetPosErrors_OnlyYNeeded(in m, in m2, out prism_posError);
				fromLimit.x = prism_posError.x - prismaticLimits.x;
				fromLimit.y = prism_posError.x - prismaticLimits.y;
				if (fromLimit.x * fromLimit.y <= 0f)
				{
					prism_posError.x = 0f;
					return;
				}
				bool flag = fromLimit.x < 0f;
				prism_posError.x = (flag ? fromLimit.x : fromLimit.y);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void GetPosErrors(in Poly.Solver.Motion m0, in Poly.Solver.Motion m1, out Vec2 posError)
		{
			Vec2 b = m1.com - m0.com;
			posError.x = Vec2.Dot(in dir0, in b) + GetAngularError_Dir0(in m0, in m1);
			posError.y = Vec2.Dot(in dir1, in b) + GetAngularError_Dir1(in m0, in m1);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[Obsolete]
		public void GetVelErrors(in Poly.Solver.Motion m0, in Poly.Solver.Motion m1, out Vec2 velError)
		{
			Vec2 b = m1.linVel - m0.linVel;
			velError.x = Vec2.Dot(in dir0, in b) + GetAngularPointVelocity_Dir0(in m0, in m1);
			velError.y = Vec2.Dot(in dir1, in b) + GetAngularPointVelocity_Dir1(in m0, in m1);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float GetVelErrors_Dir0(in Poly.Solver.Motion m0, in Poly.Solver.Motion m1)
		{
			Vec2 b = m1.linVel - m0.linVel;
			return Vec2.Dot(in dir0, in b) + GetAngularPointVelocity_Dir0(in m0, in m1);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float GetVelErrors_Dir1(in Poly.Solver.Motion m0, in Poly.Solver.Motion m1)
		{
			Vec2 b = m1.linVel - m0.linVel;
			return Vec2.Dot(in dir1, in b) + GetAngularPointVelocity_Dir1(in m0, in m1);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float GetAngularError_Dir0(in Poly.Solver.Motion motion0, in Poly.Solver.Motion motion1)
		{
			return deltaAngle1ToDeltaDistance_Dir0 * motion1.angle - deltaAngle0ToDeltaDistance_Dir0 * motion0.angle + errorOffset_Dir0;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float GetAngularError_Dir1(in Poly.Solver.Motion motion0, in Poly.Solver.Motion motion1)
		{
			return deltaAngle1ToDeltaDistance_Dir1 * motion1.angle - deltaAngle0ToDeltaDistance_Dir1 * motion0.angle + errorOffset_Dir1;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float GetAngularPointVelocity_Dir0(in Poly.Solver.Motion motion0, in Poly.Solver.Motion motion1)
		{
			return deltaAngle1ToDeltaDistance_Dir0 * motion1.angVel - deltaAngle0ToDeltaDistance_Dir0 * motion0.angVel;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float GetAngularPointVelocity_Dir1(in Poly.Solver.Motion motion0, in Poly.Solver.Motion motion1)
		{
			return deltaAngle1ToDeltaDistance_Dir1 * motion1.angVel - deltaAngle0ToDeltaDistance_Dir1 * motion0.angVel;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void ApplyImpulse_Dir0(ref Poly.Solver.Motion motion0, ref Poly.Solver.Motion motion1, float impulse)
		{
			Vec2 vec = default(Vec2);
			vec.x = impulse * dir0.x;
			vec.y = impulse * dir0.y;
			motion0.linVel.x -= vec.x * motion0.invMass;
			motion0.linVel.y -= vec.y * motion0.invMass;
			motion0.angVel -= impulse * deltaAngle0ToDeltaDistance_Dir0 * motion0.invInertia;
			motion1.linVel.x += vec.x * motion1.invMass;
			motion1.linVel.y += vec.y * motion1.invMass;
			motion1.angVel += impulse * deltaAngle1ToDeltaDistance_Dir0 * motion1.invInertia;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void ApplyImpulse_Dir1(ref Poly.Solver.Motion motion0, ref Poly.Solver.Motion motion1, float impulse)
		{
			Vec2 vec = default(Vec2);
			vec.x = impulse * dir1.x;
			vec.y = impulse * dir1.y;
			motion0.linVel.x -= vec.x * motion0.invMass;
			motion0.linVel.y -= vec.y * motion0.invMass;
			motion0.angVel -= impulse * deltaAngle0ToDeltaDistance_Dir1 * motion0.invInertia;
			motion1.linVel.x += vec.x * motion1.invMass;
			motion1.linVel.y += vec.y * motion1.invMass;
			motion1.angVel += impulse * deltaAngle1ToDeltaDistance_Dir1 * motion1.invInertia;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void ApplyPositionCorrection_Dir0(ref Poly.Solver.Motion motion0, ref Poly.Solver.Motion motion1, float impulse)
		{
			Vec2 vec = default(Vec2);
			vec.x = impulse * dir0.x;
			vec.y = impulse * dir0.y;
			motion0.com.x -= vec.x * motion0.invMass;
			motion0.com.y -= vec.y * motion0.invMass;
			motion0.angle -= impulse * deltaAngle0ToDeltaDistance_Dir0 * motion0.invInertia;
			motion1.com.x += vec.x * motion1.invMass;
			motion1.com.y += vec.y * motion1.invMass;
			motion1.angle += impulse * deltaAngle1ToDeltaDistance_Dir0 * motion1.invInertia;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void ApplyPositionCorrection_Dir1(ref Poly.Solver.Motion motion0, ref Poly.Solver.Motion motion1, float impulse)
		{
			Vec2 vec = default(Vec2);
			vec.x = impulse * dir1.x;
			vec.y = impulse * dir1.y;
			motion0.com.x -= vec.x * motion0.invMass;
			motion0.com.y -= vec.y * motion0.invMass;
			motion0.angle -= impulse * deltaAngle0ToDeltaDistance_Dir1 * motion0.invInertia;
			motion1.com.x += vec.x * motion1.invMass;
			motion1.com.y += vec.y * motion1.invMass;
			motion1.angle += impulse * deltaAngle1ToDeltaDistance_Dir1 * motion1.invInertia;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void prism_GetPosErrors_OnlyYNeeded(in Poly.Solver.Motion m0, in Poly.Solver.Motion m1, out Vec2 posError)
		{
			posError = default(Vec2);
			Vec2 b = m1.com - m0.com;
			posError.x = Vec2.Dot(in dir0, in b) + GetAngularError_Dir0(in m0, in m1);
			posError.y = Vec2.Dot(in dir1, in b) + prism_GetAngularError_Dir1(in m0, in m1);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void prism_GetVelErrors(in Poly.Solver.Motion m0, in Poly.Solver.Motion m1, out Vec2 velError)
		{
			velError = default(Vec2);
			Vec2 b = m1.linVel - m0.linVel;
			velError.x = Vec2.Dot(in dir0, in b) + GetAngularPointVelocity_Dir0(in m0, in m1);
			velError.y = Vec2.Dot(in dir1, in b) + prism_GetAngularPointVelocity_Dir1(in m0, in m1);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float prism_GetVelErrors_Dir1(in Poly.Solver.Motion m0, in Poly.Solver.Motion m1)
		{
			Vec2 b = m1.linVel - m0.linVel;
			return Vec2.Dot(in dir1, in b) + prism_GetAngularPointVelocity_Dir1(in m0, in m1);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float prism_GetAngularError_Dir1(in Poly.Solver.Motion motion0, in Poly.Solver.Motion motion1)
		{
			return deltaAngle1ToDeltaDistance_Dir1 * motion1.angle - prism_deltaAngle0ToDeltaDistance_Dir1 * motion0.angle + prism_errorOffset_Dir1;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float prism_GetAngularPointVelocity_Dir1(in Poly.Solver.Motion motion0, in Poly.Solver.Motion motion1)
		{
			return deltaAngle1ToDeltaDistance_Dir1 * motion1.angVel - prism_deltaAngle0ToDeltaDistance_Dir1 * motion0.angVel;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void prism_ApplyImpulse_Dir1(ref Poly.Solver.Motion motion0, ref Poly.Solver.Motion motion1, float impulse)
		{
			Vec2 vec = default(Vec2);
			vec.x = impulse * dir1.x;
			vec.y = impulse * dir1.y;
			motion0.linVel.x -= vec.x * motion0.invMass;
			motion0.linVel.y -= vec.y * motion0.invMass;
			motion0.angVel -= impulse * prism_deltaAngle0ToDeltaDistance_Dir1 * motion0.invInertia;
			motion1.linVel.x += vec.x * motion1.invMass;
			motion1.linVel.y += vec.y * motion1.invMass;
			motion1.angVel += impulse * deltaAngle1ToDeltaDistance_Dir1 * motion1.invInertia;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void prism_ApplyPositionCorrection_Dir1(ref Poly.Solver.Motion motion0, ref Poly.Solver.Motion motion1, float impulse)
		{
			Vec2 vec = default(Vec2);
			vec.x = impulse * dir1.x;
			vec.y = impulse * dir1.y;
			motion0.com.x -= vec.x * motion0.invMass;
			motion0.com.y -= vec.y * motion0.invMass;
			motion0.angle -= impulse * prism_deltaAngle0ToDeltaDistance_Dir1 * motion0.invInertia;
			motion1.com.x += vec.x * motion1.invMass;
			motion1.com.y += vec.y * motion1.invMass;
			motion1.angle += impulse * deltaAngle1ToDeltaDistance_Dir1 * motion1.invInertia;
		}
	}
}
