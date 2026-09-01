using System;
using UnityEngine;

public class CannonEntity : GenericEntity
{
	public CanonNPCv2 cannon;

	public HingeJoint joint;

	private MSlider rateSlider;

	private MSlider powerSlider;

	private MSlider rangeSlider;

	private MSlider angleSlider;

	public override void Init()
	{
		if (!isInitialized)
		{
			base.Init();
			breakForceAmount.ValueChanged += OnBreakChanged;
			rateSlider = AddSliderUnclamped(2440, GenericEntity.LOGIC_PREFIX + "rate", cannon.shootDelayMin + 1f, 1f, 10f, string.Empty);
			rateSlider.ValueChanged += OnRateChanged;
			powerSlider = AddSlider(2427, GenericEntity.LOGIC_PREFIX + "power", cannon.power * 0.001f, 0f, 50f, string.Empty, "K");
			powerSlider.logScaling = true;
			powerSlider.ValueChanged += OnPowerChanged;
			rangeSlider = AddSlider(2348, GenericEntity.LOGIC_PREFIX + "range", cannon.activationDistance, 5f, 250f, string.Empty);
			rangeSlider.logScaling = true;
			rangeSlider.ValueChanged += OnRangeChanged;
			if ((bool)joint)
			{
				angleSlider = AddSlider(2435, GenericEntity.LOGIC_PREFIX + "limits", joint.limits.max, 10f, 360f, string.Empty);
				angleSlider.ValueChanged += OnAngleChanged;
			}
			ReferenceMaster.onLevelSimulation = (Action<bool>)Delegate.Combine(ReferenceMaster.onLevelSimulation, new Action<bool>(OnLevelSimulate));
		}
	}

	private void OnRateChanged(float newValue)
	{
		if (newValue < 1f)
		{
			newValue = 1f;
		}
		float num = Mathf.Clamp(0.25f + newValue * 0.2f, 0.5f, 1f);
		cannon.shootDelayMin = newValue - num;
		cannon.shootDelayMax = newValue + num;
	}

	private void OnPowerChanged(float newValue)
	{
		cannon.power = newValue * 1000f;
	}

	private void OnRangeChanged(float newValue)
	{
		cannon.activationDistance = newValue;
	}

	private void OnAngleChanged(float newValue)
	{
		newValue = Mathf.Abs(newValue);
		JointLimits limits = joint.limits;
		limits.min = 0f - newValue;
		limits.max = newValue;
		joint.limits = limits;
		joint.useLimits = newValue < 360f;
	}

	private void OnBreakChanged(float newValue)
	{
	}

	private void OnLevelSimulate(bool toggle)
	{
	}

	public override void OnRemove()
	{
		base.OnRemove();
		if (rateSlider != null)
		{
			ReferenceMaster.onLevelSimulation = (Action<bool>)Delegate.Remove(ReferenceMaster.onLevelSimulation, new Action<bool>(OnLevelSimulate));
		}
	}

	private void OnTransformChanged()
	{
	}

	public override void UpdateOnTransformEvent()
	{
		OnTransformChanged();
	}

	public override void OnPositionChanged(Vector3 pos)
	{
		OnTransformChanged();
	}

	public override void OnRotationChanged(Quaternion rot)
	{
		OnTransformChanged();
	}

	public override void OnScaleChanged(Vector3 scale)
	{
		OnTransformChanged();
	}

	public override bool TriggerEvaluate()
	{
		return false;
	}
}
