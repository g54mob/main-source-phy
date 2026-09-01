using System;
using InternalModding.Misc;
using Modding.Serialization;
using UnityEngine;

namespace Modding.Modules.Official
{
	public class SteeringModuleBehaviour : BlockModuleBehaviour<SteeringModule>
	{
		private MKey leftKey;

		private MKey rightKey;

		private MToggle automaticToggle;

		private MSlider speedSlider;

		private MLimits limits;

		private ConfigurableJoint myJoint;

		private UnityEngine.Vector3 axis;

		private bool hasStarted;

		private int startFrames;

		private float input;

		private float angleToBe;

		private UnityEngine.Vector3 jointEulerRotation = Modding.Serialization.Vector3.zero;

		private float leftValue;

		private float emuLeftValue;

		private float rightValue;

		private float emuRightValue;

		private float FlipInvert
		{
			get
			{
				return (!base.Flipped) ? 1f : (-1f);
			}
		}

		public override void OnReload()
		{
			if (base.HasRigidbody)
			{
				base.Rigidbody.maxAngularVelocity = base.Module.MaxAngularSpeed;
			}
			limits.MaxValue = base.Module.LimitsHighestAngle;
			limits.iconInfo = base.Module.LimitsDisplay.ToFauxTransform();
		}

		public override void SafeAwake()
		{
			if (base.IsSimulating && !base.SimPhysics)
			{
				return;
			}
			try
			{
				leftKey = GetKey(base.Module.LeftKey);
				rightKey = GetKey(base.Module.RightKey);
				automaticToggle = GetToggle(base.Module.AutomaticToggle);
				speedSlider = GetSlider(base.Module.SpeedSlider);
				if (base.Module.HasLimits)
				{
					limits = AddLimits("Limits", "steering-limits", base.Module.LimitsDefaultMin, base.Module.LimitsDefaultMax, base.Module.LimitsHighestAngle, base.Module.LimitsDisplay.ToFauxTransform());
				}
			}
			catch (Exception ex)
			{
				MLog.Error("Could not get all mapper types for Steering Module! Module will be disabled.");
				MLog.Error(ex.ToString());
				UnityEngine.Object.Destroy(this);
				return;
			}
			if (!base.IsStripped)
			{
				myJoint = GetComponent<ConfigurableJoint>();
				switch (base.Module.Axis)
				{
				case Direction.X:
					myJoint.angularXMotion = ConfigurableJointMotion.Free;
					myJoint.angularYMotion = ConfigurableJointMotion.Locked;
					myJoint.angularZMotion = ConfigurableJointMotion.Locked;
					axis = new Modding.Serialization.Vector3(1f, 0f, 0f);
					break;
				case Direction.Y:
					myJoint.angularXMotion = ConfigurableJointMotion.Locked;
					myJoint.angularYMotion = ConfigurableJointMotion.Free;
					myJoint.angularZMotion = ConfigurableJointMotion.Locked;
					axis = new Modding.Serialization.Vector3(0f, 1f, 0f);
					break;
				case Direction.Z:
					myJoint.angularXMotion = ConfigurableJointMotion.Locked;
					myJoint.angularYMotion = ConfigurableJointMotion.Locked;
					myJoint.angularZMotion = ConfigurableJointMotion.Free;
					axis = new Modding.Serialization.Vector3(0f, 0f, 1f);
					break;
				}
			}
		}

		public void Start()
		{
			if (base.IsSimulating && base.SimPhysics)
			{
				if (base.HasRigidbody)
				{
					base.Rigidbody.maxAngularVelocity = base.Module.MaxAngularSpeed;
				}
				JointDrive angularYZDrive = myJoint.angularYZDrive;
				JointDrive angularXDrive = myJoint.angularXDrive;
				float positionDamper = (angularXDrive.positionDamper = 50f);
				angularYZDrive.positionDamper = positionDamper;
				positionDamper = (angularXDrive.positionSpring = 100000f);
				angularYZDrive.positionSpring = positionDamper;
				myJoint.angularYZDrive = angularYZDrive;
				myJoint.angularXDrive = angularXDrive;
				myJoint.targetAngularVelocity = axis * 10f;
			}
		}

		public override void KeyEmulationUpdate()
		{
			emuLeftValue = leftKey.EmulationValue();
			emuRightValue = rightKey.EmulationValue();
			if (automaticToggle.IsActive)
			{
				input = 1f;
			}
			else
			{
				input = leftValue + emuLeftValue - rightValue - emuRightValue;
			}
		}

		public override void SimulateUpdateHost()
		{
			if (!hasStarted)
			{
				if (startFrames == 3)
				{
					if (base.HasRigidbody)
					{
						base.Rigidbody.WakeUp();
					}
					hasStarted = true;
				}
				else
				{
					startFrames++;
				}
			}
			leftValue = leftKey.Value;
			rightValue = rightKey.Value;
			if (automaticToggle.IsActive)
			{
				input = 1f;
			}
			else
			{
				input = leftValue + emuLeftValue - rightValue - emuRightValue;
			}
		}

		public override void SimulateFixedUpdateHost()
		{
			if (!myJoint)
			{
				return;
			}
			float value = speedSlider.Value;
			Rigidbody connectedBody = myJoint.connectedBody;
			bool flag = connectedBody != null;
			if ((!flag || !connectedBody.isKinematic || base.HasRigidbody || !base.Rigidbody.isKinematic) && input != 0f && value != 0f)
			{
				if (base.HasRigidbody && base.Rigidbody.IsSleeping())
				{
					base.Rigidbody.WakeUp();
				}
				if (flag && connectedBody.IsSleeping())
				{
					connectedBody.WakeUp();
				}
				float num = input * Time.deltaTime * 100f * base.Module.TargetAngleSpeed * value * FlipInvert;
				angleToBe += num;
				if (base.Module.HasLimits && limits.IsActive)
				{
					float num2 = 0f - limits.Min;
					float max = limits.Max;
					angleToBe = ((angleToBe < num2) ? num2 : ((!(angleToBe > max)) ? angleToBe : max));
				}
				else if (angleToBe > 180f)
				{
					angleToBe -= 360f;
				}
				else if (angleToBe < -180f)
				{
					angleToBe += 360f;
				}
				jointEulerRotation.x = axis.x * angleToBe;
				jointEulerRotation.y = axis.y * angleToBe;
				jointEulerRotation.z = axis.z * angleToBe;
				myJoint.targetRotation = Quaternion.Euler(jointEulerRotation);
			}
		}
	}
}
