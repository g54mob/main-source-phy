using UnityEngine;

public class SpinningEntity : GenericEntity
{
	public HingeJoint joint;

	public RotateRigidbody alt;

	public float minSpeed;

	public float maxSpeed = 360f;

	private MSlider speedSlider;

	public override void Init()
	{
		if (!isInitialized)
		{
			base.Init();
			if ((bool)joint)
			{
				speedSlider = AddSliderUnclamped(4871, GenericEntity.LOGIC_PREFIX + "rate", 0f - joint.motor.targetVelocity, minSpeed, maxSpeed, string.Empty);
				speedSlider.ValueChanged += OnSpeedChanged;
			}
		}
	}

	private void OnSpeedChanged(float newValue)
	{
		JointMotor motor = joint.motor;
		motor.targetVelocity = 0f - newValue;
		joint.motor = motor;
		if ((bool)alt)
		{
			alt.speed = 0f - newValue;
		}
	}

	public override void OnRotationChanged(Quaternion rot)
	{
		if ((bool)alt)
		{
			alt.UpdateInitial();
		}
	}

	public override bool TriggerEvaluate()
	{
		return false;
	}
}
