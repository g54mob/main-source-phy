using System;
using System.Collections.Generic;
using Pb;
using Poly.Draw;
using Poly.Solver;
using Poly.UI;
using UnityEngine;

namespace Poly.Physics
{
	[RequireComponent(typeof(Rigidbody))]
	public class WheelJoint : Joint
	{
		private enum State
		{
			PastLowerLimit = 0,
			WithinLimits = 1,
			PastUpperLimit = 2
		}

		[Range(0f, 1f)]
		public float stiffness = 1f;

		[Range(0f, 1f)]
		public float damping = 1f;

		[Header("Prismatic movement")]
		[Tooltip("Prismatic movement is oriented in the Y direction")]
		public bool enablePrismaticMovement;

		[ShowIf("enablePrismaticMovement", false, false, "")]
		public Vector2 prismaticAxis = Vector2.up;

		[ShowIf("enablePrismaticMovement", false, false, "")]
		public Vector2 prismaticLimits = new Vector2(-0.1f, 0.1f);

		[Header("Spring action")]
		public bool enableSpring;

		[ShowIf("enableSpring", false, false, "")]
		public float springConstant = 1000f;

		[ShowIf("enableSpring", false, false, "")]
		public float dampingConstant = 1000f;

		[ShowIf("enableSpring", false, false, "")]
		public float dampingConstantMultiplier = 0.005f;

		[ShowIf("enableSpring", false, false, "")]
		public float regressionFixup_DampingMultiplier = 2f;

		[Header("Motor")]
		public bool enableMotor;

		[NonSerialized]
		public bool useSimpleVelocityMotor;

		[ShowIf("enableMotor", false, false, "")]
		public bool idleOnDownhill;

		[ShowIf("enableMotor", false, false, "")]
		public float targetMotorVelocity = 360f;

		[ShowIf("enableMotor", false, false, "")]
		public float maxMotorTorque = 1f;

		[ShowIf("enableMotor", false, false, "")]
		public float brakingForceMultiplier = 1f;

		[ShowIf("enableMotor", false, false, "")]
		public float highSpeedBrakingForceMultiplier = 1f;

		[Header("Acceleration Control for Motorized Custom Shapes")]
		[ShowIf("enableMotor", false, false, "")]
		public float desiredAcceleration_ForSimpleMotor = float.PositiveInfinity;

		[Header("Acceleration Control for Vehicles")]
		[ShowIf("enableMotor", false, false, "")]
		public float desiredAcceleration = float.PositiveInfinity;

		[ShowIf("enableMotor", false, false, "")]
		public float topSpeed = float.PositiveInfinity;

		private float expectedAngVelocity;

		private float measuredExternalImpulse;

		private float debug_lastVel;

		private float motorVelocity;

		private float prevMotorError;

		private float prevTorque;

		private float avgTorque;

		private float avgVelocity;

		private Vec2 velImpulse_SinceIntegration;

		private Vec2 fullImpulse_SinceIntegration;

		private Vec2 sumVelImpulses_InFrame;

		private float angVelImpulse_SinceIntegration;

		private State state = State.WithinLimits;

		private State prevState = State.WithinLimits;

		private bool lastMomentBeforeRelease_SolveMotorAfterWarmStartingToStopWheelsFromRolling;

		private bool parkingBrakesOn;

		private float timeElapsedUnderThresholdSpeed;

		private float refAngleForParkingBrake;

		private bool parkingSoftBrakeEnabled;

		private float parkingSoftBrakeRefAngle;

		private float prevSoftBrakesImpulse;

		private byte numIntegrationsDisplaced0;

		private byte numIntegrationsDisplaced1;

		private byte numIntegrationsDisplaced2;

		private static bool warnOnceAboutSimpleMotorWithTargetAcceleration = true;

		public const bool solvingOrderOverride_SolveAllSpringsFirstBeforeAllMotors = true;

		private WheelJointSolveProcess processBuffer;

		public bool isBreakable { get; set; }

		private bool useUncontrolledSimpleVelocityEnginePath
		{
			get
			{
				if (0f != desiredAcceleration_ForSimpleMotor)
				{
					return float.MaxValue == desiredAcceleration_ForSimpleMotor;
				}
				return true;
			}
		}

		public float currentTorque => avgTorque;

		public float currentVelocity => avgVelocity;

		public bool isBroken { get; set; }

		public bool applyAngularFriction { get; set; }

		public bool isCustomShape { get; set; }

		private new void Awake()
		{
			base.Awake();
			prismaticAxis.Normalize();
		}

		public override void PrepForSolving(SolverSettings settings)
		{
			lastMomentBeforeRelease_SolveMotorAfterWarmStartingToStopWheelsFromRolling = targetMotorVelocity == 0f || useSimpleVelocityMotor;
			timeElapsedUnderThresholdSpeed += settings.frameDeltaTime;
			JointSolverSettings joints = settings.joints;
			avgTorque = 0f;
			avgVelocity = 0f;
			ref WheelJointSolveProcess reference = ref processBuffer;
			reference.BuildProcess_PerFrame(base.body0, base.body1, in pivot, in connectedPivot, (Vec2)prismaticAxis, enablePrismaticMovement, (Vec2)prismaticLimits, joints.useSharedPivotPoint);
			if (enablePrismaticMovement && enableSpring)
			{
				reference.ComputeAndCacheSpringParams(springConstant, dampingConstant, dampingConstantMultiplier, regressionFixup_DampingMultiplier, settings);
			}
			sumVelImpulses_InFrame = Vec2.zero;
			if (!joints.useJointWarmstarting)
			{
				velImpulse_SinceIntegration = Vec2.zero;
				fullImpulse_SinceIntegration = Vec2.zero;
				angVelImpulse_SinceIntegration = 0f;
			}
			else if (!joints.warmstartVehicleEngine && !useSimpleVelocityMotor)
			{
				angVelImpulse_SinceIntegration = 0f;
			}
			if (!useSimpleVelocityMotor && parkingBrakesOn)
			{
				angVelImpulse_SinceIntegration = 0f;
			}
		}

		public override void Solve(SolverSettings settings, Poly.Solver.Motion[] motionsPtr)
		{
		}

		public void _Solve(SolverSettings settings, Poly.Solver.Motion[] motionsPtr)
		{
			ref WheelJointSolveProcess reference = ref processBuffer;
			ref Poly.Solver.Motion reference2 = ref motionsPtr[reference.motionIdx0];
			ref Poly.Solver.Motion reference3 = ref motionsPtr[reference.motionIdx1];
			if (reference2.invMass + reference3.invMass > 0f)
			{
				if (applyAngularFriction)
				{
					SolverAngularFriction(settings, ref reference2, ref reference3);
				}
				if (enablePrismaticMovement)
				{
					SolvePrismatic(settings, ref reference2, ref reference3, in reference);
				}
				else
				{
					SolveHinge(settings, ref reference2, ref reference3, in reference);
				}
			}
		}

		public void _Solve_HingeOnly(SolverSettings settings, Poly.Solver.Motion[] motionsPtr)
		{
			ref WheelJointSolveProcess reference = ref processBuffer;
			ref Poly.Solver.Motion reference2 = ref motionsPtr[reference.motionIdx0];
			ref Poly.Solver.Motion reference3 = ref motionsPtr[reference.motionIdx1];
			if (reference2.invMass + reference3.invMass > 0f)
			{
				if (applyAngularFriction)
				{
					SolverAngularFriction(settings, ref reference2, ref reference3);
				}
				SolveHinge(settings, ref reference2, ref reference3, in reference);
			}
		}

		private void SolveSpring(SolverSettings settings, ref Poly.Solver.Motion motion0, ref Poly.Solver.Motion motion1, in WheelJointSolveProcess process)
		{
			float num = process.posError.x;
			if (settings.joints.clipSpringForceToWithinPrismaticLimits)
			{
				num = ((!(prismaticLimits.x <= num)) ? prismaticLimits.x : ((num <= prismaticLimits.y) ? num : prismaticLimits.y));
			}
			float velErrors_Dir = process.GetVelErrors_Dir0(in motion0, in motion1);
			float impulse = (0f - num) * process.springStiffness - velErrors_Dir * process.springDamping;
			process.ApplyImpulse_Dir0(ref motion0, ref motion1, impulse);
		}

		private void SolveSimplePositionMotor_OnlyTauWithoutDamping(SolverSettings settings, ref Poly.Solver.Motion motion0, ref Poly.Solver.Motion motion1, in WheelJointSolveProcess process)
		{
			float num = motion1.angle - motion0.angle;
			num -= refAngleForParkingBrake * (MathF.PI / 180f);
			num = Pb.Mathf.WrapAngleToOnePi_Slow(num);
			float num2 = MathF.PI / 80f;
			num2 = maxMotorTorque * settings.joints.force2ImpulseRB * (motion0.invInertia + motion1.invInertia);
			if (num2 * num2 < num * num)
			{
				if (num < 0f)
				{
					refAngleForParkingBrake += num + num2;
				}
				else
				{
					refAngleForParkingBrake += num - num2;
				}
			}
			float num3 = targetMotorVelocity * settings.deltaTimeForVelocity * (MathF.PI / 180f);
			float num4 = motion1.angVel - motion0.angVel - num3;
			float num5 = 1f / (motion0.invInertia + motion1.invInertia);
			float num6 = (0f - num4) * num5;
			float num7 = (0f - num) * num5;
			float num8 = maxMotorTorque * settings.joints.force2ImpulseRB;
			float num9 = 0f - num8;
			float num10 = angVelImpulse_SinceIntegration + num6 + num7;
			num10 = ((!(num9 <= num10)) ? num9 : ((num10 <= num8) ? num10 : num8));
			num6 = num10 - angVelImpulse_SinceIntegration - num7;
			motion0.angVel -= (num6 + num7) * motion0.invInertia;
			motion1.angVel += (num6 + num7) * motion1.invInertia;
			angVelImpulse_SinceIntegration += num6;
		}

		private void SolveSoftBreakes(SolverSettings settings, ref Poly.Solver.Motion motion0, ref Poly.Solver.Motion motion1, in WheelJointSolveProcess process)
		{
			float angVel = motion0.angVel;
			float num = 0f - (motion1.angVel - angVel);
			float num2 = targetMotorVelocity * settings.deltaTimeForVelocity * (MathF.PI / 180f) - num;
			float angle = motion1.angle - motion0.angle - parkingSoftBrakeRefAngle;
			angle = Pb.Mathf.WrapAngleToOnePi_Slow(angle);
			float num3 = settings.maxSoftParkingBrakeAngleDeg * (MathF.PI / 180f);
			float num4 = UnityEngine.Mathf.Clamp(angle, 0f - num3, num3);
			if (num4 != angle)
			{
				parkingSoftBrakeRefAngle -= num4 - angle;
			}
			float num5 = num2 + 1f * num4 * settings.softParkingBrakeTau;
			float num6 = motion0.invInertia + motion1.invInertia;
			float num7 = 1f / num6;
			float num8 = num5 * num7;
			float num9 = 1f;
			float num10 = maxMotorTorque * settings.joints.force2ImpulseRB * num9;
			float num11 = 0f - num10;
			float num12 = num10;
			num8 -= angVelImpulse_SinceIntegration;
			num8 = ((!(num11 <= num8)) ? num11 : ((num8 <= num12) ? num8 : num12));
			num8 += angVelImpulse_SinceIntegration;
			motion0.angVel += num8 * motion0.invInertia;
			motion1.angVel -= num8 * motion1.invInertia;
			prevSoftBrakesImpulse = num8;
		}

		private void SolveSimpleVelocityMotor(SolverSettings settings, ref Poly.Solver.Motion motion0, ref Poly.Solver.Motion motion1, in WheelJointSolveProcess process)
		{
			float num = targetMotorVelocity * settings.deltaTimeForVelocity * (MathF.PI / 180f);
			float num2 = motion1.angVel - motion0.angVel - num;
			float num3 = 1f / (motion0.invInertia + motion1.invInertia);
			float num4 = (0f - num2) * num3;
			float num5 = maxMotorTorque * settings.joints.force2ImpulseRB;
			float num6 = 0f - num5;
			float num7 = angVelImpulse_SinceIntegration + num4;
			num7 = ((!(num6 <= num7)) ? num6 : ((num7 <= num5) ? num7 : num5));
			num4 = num7 - angVelImpulse_SinceIntegration;
			motion0.angVel -= num4 * motion0.invInertia;
			motion1.angVel += num4 * motion1.invInertia;
			angVelImpulse_SinceIntegration += num4;
		}

		private void SolveSimpleVelocityMotor_WithTargetAcceleration(SolverSettings settings, ref Poly.Solver.Motion motion0, ref Poly.Solver.Motion motion1, in WheelJointSolveProcess process)
		{
			if (settings.joints.useJointWarmstarting && warnOnceAboutSimpleMotorWithTargetAcceleration)
			{
				UnityEngine.Debug.LogWarning("Warnings: this engine type totally malfunctions at times when warmstarting is on. See layout 920.");
				warnOnceAboutSimpleMotorWithTargetAcceleration = false;
			}
			motion0.angVel += angVelImpulse_SinceIntegration * motion0.invInertia;
			motion1.angVel -= angVelImpulse_SinceIntegration * motion1.invInertia;
			angVelImpulse_SinceIntegration = 0f;
			float num = targetMotorVelocity * settings.deltaTimeForVelocity * (MathF.PI / 180f);
			float num2 = motion1.angVel - motion0.angVel;
			float num3 = num2 - num;
			float num4 = desiredAcceleration_ForSimpleMotor * settings.deltaTimeForVelocity * settings.deltaTimeForVelocity * (MathF.PI / 180f);
			num3 = ((!(0f - num4 <= num3)) ? (0f - num4) : ((num3 <= num4) ? num3 : num4));
			float num5 = 1f / (motion0.invInertia + motion1.invInertia);
			float num6 = (0f - num3) * num5;
			float num7 = measuredExternalImpulse + (num2 - expectedAngVelocity) * num5;
			float num8 = 0.9f;
			measuredExternalImpulse = (1f - num8) * measuredExternalImpulse + num8 * num7;
			num6 -= measuredExternalImpulse;
			float num9 = maxMotorTorque * settings.joints.force2ImpulseRB;
			float num10 = 0f - num9;
			float num11 = angVelImpulse_SinceIntegration + num6;
			num11 = ((!(num10 <= num11)) ? num10 : ((num11 <= num9) ? num11 : num9));
			num6 = num11 - angVelImpulse_SinceIntegration;
			motion0.angVel -= num6 * motion0.invInertia;
			motion1.angVel += num6 * motion1.invInertia;
			angVelImpulse_SinceIntegration += num6;
			expectedAngVelocity = num2 + (num6 + measuredExternalImpulse) / num5;
			_ = (num2 - debug_lastVel) / settings.deltaTimeForVelocity / settings.deltaTimeForVelocity;
			debug_lastVel = num2;
		}

		private void SolveMotor(SolverSettings settings, ref Poly.Solver.Motion motion0, ref Poly.Solver.Motion motion1, in WheelJointSolveProcess process)
		{
			if (useSimpleVelocityMotor)
			{
				if (useUncontrolledSimpleVelocityEnginePath && settings.joints.useJointWarmstarting)
				{
					SolveSimpleVelocityMotor(settings, ref motion0, ref motion1, in process);
				}
				else
				{
					SolveSimpleVelocityMotor_WithTargetAcceleration(settings, ref motion0, ref motion1, in process);
				}
				return;
			}
			JointSolverSettings joints = settings.joints;
			if (parkingBrakesOn)
			{
				if (!joints.useParkingBrakes || targetMotorVelocity != 0f)
				{
					parkingBrakesOn = false;
				}
				else
				{
					SolveSimplePositionMotor_OnlyTauWithoutDamping(settings, ref motion0, ref motion1, in process);
				}
			}
			motion0.angVel += (0f - angVelImpulse_SinceIntegration) * motion0.invInertia;
			motion1.angVel -= (0f - angVelImpulse_SinceIntegration) * motion1.invInertia;
			bool flag = false;
			if (parkingSoftBrakeEnabled)
			{
				if (!settings.useSoftParkingBrake || targetMotorVelocity != 0f)
				{
					parkingSoftBrakeEnabled = false;
				}
				else
				{
					SolveSoftBreakes(settings, ref motion0, ref motion1, in process);
					flag = true;
				}
			}
			float angVel = motion0.angVel;
			float num = 0f - (motion1.angVel - angVel);
			float num2 = targetMotorVelocity * settings.deltaTimeForVelocity * (MathF.PI / 180f);
			if (targetMotorVelocity == 0f)
			{
				if (joints.useParkingBrakes && !parkingBrakesOn)
				{
					float num3 = num / settings.deltaTimeForVelocity * 57.29578f;
					if (num3 < 10f)
					{
						parkingBrakesOn = true;
						refAngleForParkingBrake = (motion1.angle - motion0.angle) * 57.29578f;
					}
					else if (num3 < 45f)
					{
						if (4f < timeElapsedUnderThresholdSpeed)
						{
							parkingBrakesOn = true;
							refAngleForParkingBrake = (motion1.angle - motion0.angle) * 57.29578f;
						}
					}
					else
					{
						timeElapsedUnderThresholdSpeed = 0f;
					}
				}
				else if (settings.useSoftParkingBrake && !parkingSoftBrakeEnabled)
				{
					float num4 = num / settings.deltaTimeForVelocity * 57.29578f;
					if (num4 * num4 < 100f)
					{
						parkingSoftBrakeEnabled = true;
						parkingSoftBrakeRefAngle = motion1.angle - motion0.angle;
					}
					else if (num4 * num4 < 2025f)
					{
						if (4f < timeElapsedUnderThresholdSpeed)
						{
							parkingSoftBrakeEnabled = true;
							parkingSoftBrakeRefAngle = motion1.angle - motion0.angle;
						}
					}
					else
					{
						timeElapsedUnderThresholdSpeed = 0f;
					}
				}
			}
			else
			{
				parkingSoftBrakeEnabled = false;
				timeElapsedUnderThresholdSpeed = 0f;
				prevSoftBrakesImpulse = 0f;
			}
			if (flag && parkingSoftBrakeEnabled)
			{
				return;
			}
			float num5 = num2 - num;
			float num6 = 1f;
			float num7 = 1f;
			float num8 = num / (settings.deltaTimeForVelocity * (MathF.PI / 180f));
			float num9 = motorVelocity;
			float num10 = (num8 - num9) / settings.deltaTimeForVelocity;
			float num11 = num8 / (topSpeed + 1E-06f);
			if (num11 < 0f)
			{
				num11 = 0f - num11;
			}
			float num12 = 1f - num11;
			num12 = ((!(0f <= num12)) ? 0f : ((num12 <= 1f) ? num12 : 1f));
			float num13 = num11 - 1f;
			num13 = ((!(0f <= num13)) ? 0f : ((num13 <= 1f) ? num13 : 1f));
			float num14 = ((targetMotorVelocity * num8 <= 0f) ? ((1f - num13) * brakingForceMultiplier + num13 * highSpeedBrakingForceMultiplier) : 1f);
			float num15 = (targetMotorVelocity - num8) / settings.deltaTimeForVelocity;
			float num16 = desiredAcceleration * num14;
			if (!settings.integrateInSolverIterations)
			{
				num16 /= (float)settings.numIterations;
			}
			float num17 = (((!(0f - num16 <= num15)) ? (0f - num16) : ((num15 <= num16) ? num15 : num16)) - num10) * 0.5f * settings.deltaTimeForVelocity * (settings.deltaTimeForVelocity * (MathF.PI / 180f));
			motorVelocity = num8;
			if (idleOnDownhill)
			{
				if (num * num2 > 0f)
				{
					num6 = 0f;
				}
				else if (num * num2 < 0f || num2 == 0f)
				{
					num7 = 0f;
				}
			}
			float num18 = prevMotorError + num17;
			if (num18 * num18 < num5 * num5)
			{
				num5 = num18;
			}
			float num19 = motion0.invInertia + motion1.invInertia;
			float num20 = 1f / num19;
			float num21 = num5 * num20;
			float num22 = maxMotorTorque * joints.force2ImpulseRB * num14;
			float num23 = 0f - num22;
			float num24 = num22;
			if (motorVelocity < 0f)
			{
				num23 *= num12;
				num23 *= num7;
				num24 *= num6;
			}
			else if (motorVelocity > 0f)
			{
				num23 *= num6;
				num24 *= num12;
				num24 *= num7;
			}
			num21 -= angVelImpulse_SinceIntegration;
			num21 = ((!(num23 <= num21)) ? num23 : ((num21 <= num24) ? num21 : num24));
			num21 += angVelImpulse_SinceIntegration;
			prevTorque = num21 / joints.force2ImpulseRB;
			prevMotorError = num21 / num20;
			motion0.angVel += num21 * motion0.invInertia;
			motion1.angVel -= num21 * motion1.invInertia;
			if (joints.warmstartVehicleEngine)
			{
				angVelImpulse_SinceIntegration = 0f - num21;
			}
			avgTorque += prevTorque * settings.invNumIterations;
			avgVelocity += motorVelocity * settings.invNumIterations;
		}

		private void SolvePrismatic(SolverSettings settings, ref Poly.Solver.Motion motion0, ref Poly.Solver.Motion motion1, in WheelJointSolveProcess process)
		{
			JointSolverSettings joints = settings.joints;
			ref readonly Vec2 prism_posError = ref process.prism_posError;
			ref readonly Vec2 fromLimit = ref process.fromLimit;
			ref readonly Vec2 prism_invVirtualMass = ref process.prism_invVirtualMass;
			float num = process.GetVelErrors_Dir0(in motion0, in motion1);
			bool num2 = fromLimit.x * fromLimit.y < 0f;
			if (num2)
			{
				if (num > 0f)
				{
					num += fromLimit.y;
					num = ((0f < num) ? num : 0f);
				}
				else
				{
					num += fromLimit.x;
					num = ((num < 0f) ? num : 0f);
				}
			}
			float num3 = (0f - num) * damping * joints.jointDamping / prism_invVirtualMass.x;
			float num4 = (0f - prism_posError.x) * stiffness * joints.jointTau / prism_invVirtualMass.x;
			if (!num2)
			{
				float num5 = num3 + num4 + fullImpulse_SinceIntegration.x;
				num5 = ((!(fromLimit.y >= 0f)) ? ((0f < num5) ? num5 : 0f) : ((num5 < 0f) ? num5 : 0f));
				num3 = num5 - fullImpulse_SinceIntegration.x - num4;
			}
			float num6 = num4 + num3;
			process.ApplyImpulse_Dir0(ref motion0, ref motion1, num6);
			velImpulse_SinceIntegration.x += num3;
			sumVelImpulses_InFrame.x += num3;
			fullImpulse_SinceIntegration.x += num6;
			num = process.prism_GetVelErrors_Dir1(in motion0, in motion1);
			num4 = (0f - prism_posError.y) * stiffness * joints.jointTau / prism_invVirtualMass.y;
			num3 = (0f - num) * damping * joints.jointDamping / prism_invVirtualMass.y;
			num6 = num4 + num3;
			process.prism_ApplyImpulse_Dir1(ref motion0, ref motion1, num6);
			velImpulse_SinceIntegration.y += num3;
			sumVelImpulses_InFrame.y += num3;
			fullImpulse_SinceIntegration.y += num6;
		}

		private void SolveHinge(SolverSettings settings, ref Poly.Solver.Motion motion0, ref Poly.Solver.Motion motion1, in WheelJointSolveProcess process)
		{
			JointSolverSettings joints = settings.joints;
			float velErrors_Dir = process.GetVelErrors_Dir0(in motion0, in motion1);
			float num = (0f - process.posError.x) * stiffness * joints.jointTau / process.invVirtualMass.x;
			float num2 = (0f - velErrors_Dir) * damping * joints.jointDamping / process.invVirtualMass.x;
			float num3 = num + num2;
			process.ApplyImpulse_Dir0(ref motion0, ref motion1, num3);
			velImpulse_SinceIntegration.x += num2;
			sumVelImpulses_InFrame.x += num2;
			fullImpulse_SinceIntegration.x += num3;
			velErrors_Dir = process.GetVelErrors_Dir1(in motion0, in motion1);
			float num4 = (0f - process.posError.y) * stiffness * joints.jointTau / process.invVirtualMass.y;
			num2 = (0f - velErrors_Dir) * damping * joints.jointDamping / process.invVirtualMass.y;
			num3 = num4 + num2;
			process.ApplyImpulse_Dir1(ref motion0, ref motion1, num3);
			velImpulse_SinceIntegration.y += num2;
			sumVelImpulses_InFrame.y += num2;
			fullImpulse_SinceIntegration.y += num3;
		}

		private void SolverAngularFriction(SolverSettings settings, ref Poly.Solver.Motion motion0, ref Poly.Solver.Motion motion1)
		{
			float num = motion0.invInertia + motion1.invInertia;
			if (!(1E-06f < num))
			{
				return;
			}
			float num2 = (0f - (motion1.angVel - motion0.angVel)) * (isCustomShape ? settings.customShapeJointAngularDamping_dampingFactor_PerCsIteration : settings.trailerJointAngularDamping_dampingFactor) / num;
			motion0.angVel -= num2 * motion0.invInertia;
			motion1.angVel += num2 * motion1.invInertia;
			if (isCustomShape)
			{
				if (motion0.invMass != 0f)
				{
					motion0.linVel *= settings.oneLess_customShapeJointLinearDamping_dampingFactor_PerCsIteration;
				}
				if (motion1.invMass != 0f)
				{
					motion1.linVel *= settings.oneLess_customShapeJointLinearDamping_dampingFactor_PerCsIteration;
				}
			}
		}

		private void UpdateState(ref WheelJointSolveProcess process)
		{
			prevState = state;
			if (process.prism_posError.x == 0f)
			{
				state = State.WithinLimits;
			}
			else if (process.prism_posError.x < 0f)
			{
				state = State.PastLowerLimit;
			}
			else
			{
				state = State.PastUpperLimit;
			}
		}

		public void Warmstart(SolverSettings settings, Poly.Solver.Motion[] motionsPtr)
		{
			JointSolverSettings joints = settings.joints;
			ref WheelJointSolveProcess reference = ref processBuffer;
			ref Poly.Solver.Motion reference2 = ref motionsPtr[reference.motionIdx0];
			ref Poly.Solver.Motion reference3 = ref motionsPtr[reference.motionIdx1];
			if (reference2.invMass + reference3.invMass > 0f)
			{
				velImpulse_SinceIntegration *= joints.jointWarmstartingRatio;
				sumVelImpulses_InFrame += velImpulse_SinceIntegration;
				fullImpulse_SinceIntegration = velImpulse_SinceIntegration;
				reference.ApplyImpulse_Dir0(ref reference2, ref reference3, velImpulse_SinceIntegration.x);
				if (enablePrismaticMovement)
				{
					reference.prism_ApplyImpulse_Dir1(ref reference2, ref reference3, velImpulse_SinceIntegration.y);
				}
				else
				{
					reference.ApplyImpulse_Dir1(ref reference2, ref reference3, velImpulse_SinceIntegration.y);
				}
			}
			if (0f < reference2.invInertia + reference3.invInertia)
			{
				angVelImpulse_SinceIntegration *= joints.jointWarmstartingRatio;
				reference2.angVel -= angVelImpulse_SinceIntegration * reference2.invInertia;
				reference3.angVel += angVelImpulse_SinceIntegration * reference3.invInertia;
			}
		}

		public void SolveMotorsFirst(SolverSettings settings, Poly.Solver.Motion[] motionsPtr)
		{
			_ = settings.joints;
			ref WheelJointSolveProcess reference = ref processBuffer;
			ref Poly.Solver.Motion reference2 = ref motionsPtr[reference.motionIdx0];
			ref Poly.Solver.Motion reference3 = ref motionsPtr[reference.motionIdx1];
			if (0f < reference2.invInertia + reference3.invInertia)
			{
				SolveMotor(settings, ref reference2, ref reference3, in reference);
			}
		}

		public void Solve_NotWarmstarted(SolverSettings settings, Poly.Solver.Motion[] motionsPtr, bool isFirstAfterIntegration)
		{
			JointSolverSettings joints = settings.joints;
			ref WheelJointSolveProcess reference = ref processBuffer;
			ref Poly.Solver.Motion reference2 = ref motionsPtr[reference.motionIdx0];
			ref Poly.Solver.Motion reference3 = ref motionsPtr[reference.motionIdx1];
			if (!(reference2.invMass + reference3.invMass > 0f))
			{
				return;
			}
			if (isFirstAfterIntegration)
			{
				reference.RecalculatePositionErrors_PerIntegration(motionsPtr, in pivot, in connectedPivot, (Vec2)prismaticAxis, enablePrismaticMovement, (Vec2)prismaticLimits);
				if (isBreakable)
				{
					float num = (enablePrismaticMovement ? reference.prism_posError.sqrMagnitude : reference.posError.sqrMagnitude);
					float num2 = joints.posErrorLimit * joints.posErrorLimit;
					if (num <= num2)
					{
						numIntegrationsDisplaced0 = 0;
						numIntegrationsDisplaced1 = 0;
						numIntegrationsDisplaced2 = 0;
					}
					else
					{
						numIntegrationsDisplaced0++;
						numIntegrationsDisplaced1++;
						numIntegrationsDisplaced2++;
						if (num <= num2 * 4f)
						{
							numIntegrationsDisplaced1 = 0;
						}
						if (num <= num2 * 16f)
						{
							numIntegrationsDisplaced2 = 0;
						}
						if (settings.deltaTimeForVelocity != 0.005f)
						{
							UnityEngine.Debug.LogWarning("WheelJoint breaking counts are integration-step specific, please adjust to non-standard fps/integrationSubSteps");
						}
						if (200 <= numIntegrationsDisplaced0 || 100 <= numIntegrationsDisplaced1 || 1 <= numIntegrationsDisplaced2)
						{
							isBroken = true;
							UnityEngine.Debug.LogWarning("Breaking off wheel or trailer on " + base.transform.parent.name);
							return;
						}
					}
				}
				UpdateState(ref reference);
				if (!joints.useJointWarmstarting)
				{
					velImpulse_SinceIntegration = Vec2.zero;
					fullImpulse_SinceIntegration = Vec2.zero;
					angVelImpulse_SinceIntegration = 0f;
				}
				else if (!joints.warmstartVehicleEngine && !useSimpleVelocityMotor)
				{
					angVelImpulse_SinceIntegration = 0f;
				}
				if (enablePrismaticMovement && (prevState != state || state == State.WithinLimits))
				{
					velImpulse_SinceIntegration.x = 0f;
				}
			}
			if (enablePrismaticMovement && enableSpring)
			{
				SolveSpring(settings, ref reference2, ref reference3, in reference);
			}
			if (joints.useJointWarmstarting)
			{
				_ = lastMomentBeforeRelease_SolveMotorAfterWarmStartingToStopWheelsFromRolling;
			}
		}

		public void Solve_Position(SolverSettings settings, Poly.Solver.Motion[] motionsPtr)
		{
			ref WheelJointSolveProcess reference = ref processBuffer;
			ref Poly.Solver.Motion reference2 = ref motionsPtr[reference.motionIdx0];
			ref Poly.Solver.Motion reference3 = ref motionsPtr[reference.motionIdx1];
			if (reference2.invMass + reference3.invMass > 0f)
			{
				if (enablePrismaticMovement)
				{
					SolvePrismatic_Position(settings, ref reference2, ref reference3, in reference, (Vec2)prismaticLimits);
				}
				else
				{
					SolveHinge_Position(settings, ref reference2, ref reference3, in reference);
				}
			}
		}

		private void SolvePrismatic_Position(SolverSettings settings, ref Poly.Solver.Motion motion0, ref Poly.Solver.Motion motion1, in WheelJointSolveProcess process, in Vec2 prismaticLimits)
		{
			JointSolverSettings joints = settings.joints;
			Vec2 b = motion1.com - motion0.com;
			float num = Vec2.Dot(in process.dir0, in b) + process.GetAngularError_Dir0(in motion0, in motion1);
			Vec2 vec = default(Vec2);
			vec.x = num - prismaticLimits.x;
			vec.y = num - prismaticLimits.y;
			float num2 = num;
			num2 = ((!(vec.x * vec.y <= 0f)) ? ((vec.x < 0f) ? vec.x : vec.y) : 0f);
			if (num2 < 0f - joints.maxJointPositionCorrection)
			{
				num2 = 0f - joints.maxJointPositionCorrection;
			}
			else if (joints.maxJointPositionCorrection < num2)
			{
				num2 = joints.maxJointPositionCorrection;
			}
			float impulse = (0f - num2) * stiffness * joints.jointPosTau / process.prism_invVirtualMass.x;
			process.ApplyPositionCorrection_Dir0(ref motion0, ref motion1, impulse);
			b = motion1.com - motion0.com;
			float num3 = Vec2.Dot(in process.dir1, in b) + process.prism_GetAngularError_Dir1(in motion0, in motion1);
			if (num3 < 0f - joints.maxJointPositionCorrection)
			{
				num3 = 0f - joints.maxJointPositionCorrection;
			}
			else if (joints.maxJointPositionCorrection < num3)
			{
				num3 = joints.maxJointPositionCorrection;
			}
			impulse = (0f - num3) * stiffness * joints.jointPosTau / process.prism_invVirtualMass.y;
			process.prism_ApplyPositionCorrection_Dir1(ref motion0, ref motion1, impulse);
		}

		private void SolveHinge_Position(SolverSettings settings, ref Poly.Solver.Motion motion0, ref Poly.Solver.Motion motion1, in WheelJointSolveProcess process)
		{
			JointSolverSettings joints = settings.joints;
			Vec2 b = motion1.com - motion0.com;
			float num = Vec2.Dot(in process.dir0, in b) + process.GetAngularError_Dir0(in motion0, in motion1);
			if (num < 0f - joints.maxJointPositionCorrection)
			{
				num = 0f - joints.maxJointPositionCorrection;
			}
			else if (joints.maxJointPositionCorrection < num)
			{
				num = joints.maxJointPositionCorrection;
			}
			float impulse = (0f - num) * stiffness * joints.jointPosTau / process.invVirtualMass.x;
			process.ApplyPositionCorrection_Dir0(ref motion0, ref motion1, impulse);
			b = motion1.com - motion0.com;
			float num2 = Vec2.Dot(in process.dir1, in b) + process.GetAngularError_Dir1(in motion0, in motion1);
			if (num2 < 0f - joints.maxJointPositionCorrection)
			{
				num2 = 0f - joints.maxJointPositionCorrection;
			}
			else if (joints.maxJointPositionCorrection < num2)
			{
				num2 = joints.maxJointPositionCorrection;
			}
			impulse = (0f - num2) * stiffness * joints.jointPosTau / process.invVirtualMass.y;
			process.ApplyPositionCorrection_Dir1(ref motion0, ref motion1, impulse);
		}

		private void OnDrawGizmosSelected()
		{
			if (Application.isPlaying && (bool)base.body0 && base.body0.isAddedToWorld)
			{
				GlDrawer.color = Color.gray;
				Vec2 vec = base.body0.motion.TransformPoint_Slow(pivot);
				GlDrawer.DrawLine(base.body0.transform.position, vec);
				if (enablePrismaticMovement)
				{
					GlDrawer.color = Color.green;
					Vec2 vec2 = base.body0.motion.TransformDirection_Slow(prismaticAxis);
					vec2.Normalize();
					Vec2 vec3 = vec + vec2 * prismaticLimits.x;
					Vec2 vec4 = vec + vec2 * prismaticLimits.y;
					GlDrawer.DrawLine(vec3, vec4);
					GlDrawer.DrawCross(vec3, 0.05f, 45f);
					GlDrawer.DrawCross(vec4, 0.05f, 45f);
				}
				GlDrawer.color = Color.yellow;
				if ((bool)base.body1)
				{
					GlDrawer.DrawLine(base.body1.transform.position, base.body1.motion.TransformPoint_Slow(connectedPivot));
				}
				else
				{
					GlDrawer.DrawLine(Vector3.zero, connectedPivot);
				}
				return;
			}
			Rigidbody component = GetComponent<Rigidbody>();
			Gizmos.color = Color.gray;
			Vec2 vec5 = ((!autoConfigureThisAnchor) ? ((Vec2)component.transform.TransformPoint(anchor)) : ((Vec2)base.body1.transform.TransformPoint(connectedAnchor)));
			Gizmos.DrawLine(component.transform.position, vec5);
			if (enablePrismaticMovement)
			{
				GlDrawer.color = Color.green;
				Vec2 vec6 = (Vec2)component.transform.TransformDirection(prismaticAxis);
				vec6.Normalize();
				Vec2 vec7 = vec5 + vec6 * prismaticLimits.x;
				Vec2 vec8 = vec5 + vec6 * prismaticLimits.y;
				Gizmos.DrawLine(vec7, vec8);
				GizmosExtension.DrawCross(vec7, 0.05f, 45f);
				GizmosExtension.DrawCross(vec8, 0.05f, 45f);
			}
			Gizmos.color = Color.yellow;
			if ((bool)base.body1)
			{
				Gizmos.DrawLine(base.body1.transform.position, vec5);
			}
			else
			{
				Gizmos.DrawLine(Vector3.zero, connectedPivot);
			}
		}

		public static void All_NotWarmstarted(List<Joint> joints, Poly.Solver.Motion[] motionsPtr, bool isFirstAfterIntegration, SolverSettings settings)
		{
			for (int i = 0; i < joints.Count; i++)
			{
				WheelJoint wheelJoint = (WheelJoint)joints[i];
				if (!wheelJoint.isBroken)
				{
					wheelJoint.Solve_NotWarmstarted(settings, motionsPtr, isFirstAfterIntegration);
				}
			}
		}

		public static void All_Warmstart(List<Joint> joints, Poly.Solver.Motion[] motionsPtr, SolverSettings settings)
		{
			for (int i = 0; i < joints.Count; i++)
			{
				WheelJoint wheelJoint = (WheelJoint)joints[i];
				if (!wheelJoint.isBroken)
				{
					wheelJoint.Warmstart(settings, motionsPtr);
				}
			}
		}

		public static void All_SolveMotorsFirst(List<Joint> joints, Poly.Solver.Motion[] motionsPtr, SolverSettings settings)
		{
			for (int i = 0; i < joints.Count; i++)
			{
				WheelJoint wheelJoint = (WheelJoint)joints[i];
				if (!wheelJoint.isBroken)
				{
					wheelJoint.SolveMotorsFirst(settings, motionsPtr);
				}
			}
		}

		public static void All_Solve(List<Joint> joints, Poly.Solver.Motion[] motionsPtr, SolverSettings settings)
		{
			for (int i = 0; i < joints.Count; i++)
			{
				WheelJoint wheelJoint = (WheelJoint)joints[i];
				if (!wheelJoint.isBroken)
				{
					wheelJoint._Solve(settings, motionsPtr);
				}
			}
		}

		public static void All_Solve_HingeOnly(List<Joint> joints, Poly.Solver.Motion[] motionsPtr, SolverSettings settings)
		{
			for (int i = 0; i < joints.Count; i++)
			{
				((WheelJoint)joints[i])._Solve_HingeOnly(settings, motionsPtr);
			}
		}

		public static void All_SolvePosition(List<Joint> joints, Poly.Solver.Motion[] motionsPtr, SolverSettings settings)
		{
			for (int i = 0; i < settings.joints.numJointPostProjectionIterations; i++)
			{
				for (int j = 0; j < joints.Count; j++)
				{
					WheelJoint wheelJoint = (WheelJoint)joints[j];
					if (!wheelJoint.isBroken)
					{
						wheelJoint.Solve_Position(settings, motionsPtr);
					}
				}
			}
		}
	}
}
