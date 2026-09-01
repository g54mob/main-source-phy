using System;
using InternalModding.Misc;
using Modding.Serialization;
using UnityEngine;

namespace Modding.Modules.Official
{
	public class SpinningModuleBehaviour : BlockModuleBehaviour<SpinningModule>
	{
		public MKey ForwardKey;

		public MKey BackwardKey;

		public MSlider SpeedSlider;

		public MSlider AccelerationSlider;

		public MToggle AutomaticToggle;

		public MToggle ToggleMode;

		public float WheelEquivalenceMultiplier = 10.4f;

		private Direction convertedAxis;

		private ConfigurableJoint joint;

		private float velocity;

		private float input;

		private bool hasStarted;

		private bool instantAcceleration;

		private bool forwardPressed;

		private bool backwardPressed;

		private bool forwardHeld;

		private bool backwardHeld;

		private bool emuForwardPressed;

		private bool emuBackwardPressed;

		private bool emuForwardHeld;

		private bool emuBackwardHeld;

		private int FlipInvert
		{
			get
			{
				return (!base.Flipped) ? 1 : (-1);
			}
		}

		public override void SafeAwake()
		{
			try
			{
				ForwardKey = GetKey(base.Module.Forward);
				BackwardKey = GetKey(base.Module.Backward);
				SpeedSlider = GetSlider(base.Module.SpeedSlider);
				AccelerationSlider = GetSlider(base.Module.AccelerationSlider);
				AccelerationSlider.maxInfinity = true;
				AutomaticToggle = GetToggle(base.Module.AutomaticToggle);
				ToggleMode = GetToggle(base.Module.ToggleModeToggle);
			}
			catch (Exception ex)
			{
				MLog.Error("Could not get all mapper types for Spinning Module! Module will be disabled.");
				MLog.Error(ex.ToString());
				UnityEngine.Object.Destroy(this);
				return;
			}
			AutomaticToggle.Toggled += delegate(bool isActive)
			{
				ToggleMode.DisplayInMapper = !isActive;
			};
			ToggleMode.DisplayInMapper = true;
			joint = GetComponent<ConfigurableJoint>();
			SetConvertedAxis();
		}

		public override void OnSimulateStart()
		{
			if (base.SimPhysics)
			{
				joint.angularXMotion = GetJointMotion(Direction.X);
				joint.angularYMotion = GetJointMotion(Direction.Y);
				joint.angularZMotion = GetJointMotion(Direction.Z);
				JointDrive angularXDrive = joint.angularXDrive;
				angularXDrive.positionDamper = 100000f;
				angularXDrive.positionSpring = 50f;
				joint.angularXDrive = angularXDrive;
				JointDrive angularYZDrive = joint.angularYZDrive;
				angularYZDrive.positionDamper = 100000f;
				angularYZDrive.positionSpring = 50f;
				joint.angularYZDrive = angularYZDrive;
				instantAcceleration = float.IsPositiveInfinity(AccelerationSlider.Value);
			}
		}

		private ConfigurableJointMotion GetJointMotion(Direction dir)
		{
			return (dir == convertedAxis) ? ConfigurableJointMotion.Free : ConfigurableJointMotion.Locked;
		}

		public override void OnReload()
		{
			base.Rigidbody.maxAngularVelocity = base.Module.MaxAngularSpeed;
			SetConvertedAxis();
		}

		private void SetConvertedAxis()
		{
			if (base.Module.Axis == Direction.Y)
			{
				convertedAxis = Direction.Z;
			}
			else if (base.Module.Axis == Direction.Z)
			{
				convertedAxis = Direction.Y;
			}
			else
			{
				convertedAxis = base.Module.Axis;
			}
		}

		public override void KeyEmulationUpdate()
		{
			if (base.SimPhysics)
			{
				emuForwardPressed = ForwardKey.EmulationPressed();
				emuBackwardPressed = BackwardKey.EmulationPressed();
				emuForwardHeld = ForwardKey.EmulationHeld(true);
				emuBackwardHeld = BackwardKey.EmulationHeld(true);
				CheckKeys(emuForwardPressed, emuBackwardPressed, emuForwardHeld, emuBackwardHeld, 1f, -1f, forwardHeld, backwardHeld);
			}
		}

		public override void SimulateUpdateHost()
		{
			forwardPressed = ForwardKey.IsPressed;
			backwardPressed = BackwardKey.IsPressed;
			forwardHeld = ForwardKey.IsHeld;
			backwardHeld = BackwardKey.IsHeld;
			CheckKeys(forwardPressed, backwardPressed, forwardHeld, backwardHeld, ForwardKey.Value, 0f - BackwardKey.Value, emuForwardHeld, emuBackwardHeld);
		}

		protected virtual void CheckKeys(bool forwardPress, bool backwardPress, bool forwardHeld, bool backwardHeld, float forwardVal, float backwardVal, bool altForwardHeld, bool altBackwardHeld)
		{
			if (AutomaticToggle.IsActive)
			{
				input = 1f;
			}
			else if (ToggleMode.IsActive)
			{
				if (forwardPress)
				{
					if (input > 0.9f)
					{
						input = 0f;
					}
					else
					{
						input = 1f;
					}
				}
				if (backwardPress)
				{
					if (input < -0.9f)
					{
						input = 0f;
					}
					else
					{
						input = -1f;
					}
				}
			}
			else if (forwardHeld)
			{
				input = forwardVal;
			}
			else if (!altForwardHeld)
			{
				if (backwardHeld)
				{
					input = backwardVal;
				}
				else if (!altBackwardHeld)
				{
					input = 0f;
				}
			}
		}

		public override void SimulateFixedUpdateHost()
		{
			if (base.IsDestroyed || joint == null)
			{
				return;
			}
			if (!hasStarted)
			{
				base.Rigidbody.maxAngularVelocity = base.Module.MaxAngularSpeed;
				hasStarted = true;
			}
			base.Rigidbody.WakeUp();
			if (instantAcceleration)
			{
				velocity = input * SpeedSlider.Value * (float)FlipInvert * WheelEquivalenceMultiplier;
				joint.targetAngularVelocity = convertedAxis.ToAxisVector() * velocity;
				return;
			}
			float num = SpeedSlider.Value * WheelEquivalenceMultiplier;
			float axisComponent = convertedAxis.GetAxisComponent(joint.targetAngularVelocity);
			bool flag = input > 0.9f || input < -0.9f;
			float num2 = 0f;
			num2 = (flag ? (input * (float)FlipInvert) : ((axisComponent == 0f) ? 0f : ((!(axisComponent > 0f)) ? 1f : (-1f))));
			float num3 = num2 * AccelerationSlider.Value * Time.deltaTime * WheelEquivalenceMultiplier;
			if (!flag && Mathf.Abs(num3) > Mathf.Abs(axisComponent))
			{
				num3 = Mathf.Sign(num3) * Mathf.Abs(axisComponent);
			}
			float value = axisComponent + num3;
			float b = Mathf.Clamp(value, 0f - num, num);
			float num4 = Mathf.Lerp(axisComponent, b, Time.deltaTime * 26f);
			joint.targetAngularVelocity = num4 * convertedAxis.ToAxisVector();
		}
	}
}
