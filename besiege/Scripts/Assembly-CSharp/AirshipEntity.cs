using System;
using UnityEngine;

[AddComponentMenu("LevelEditor/Entities/AirshipEntity")]
public class AirshipEntity : GenericEntity
{
	public float radiusMultiplier = 10f;

	public AirshipMultiAI shipAI;

	private MSlider speedSlider;

	private MSlider turningSlider;

	private MSlider circlingSlider;

	private MSlider rangeSlider;

	public override void Init()
	{
		if (!isInitialized)
		{
			base.Init();
			breakForceAmount.ValueChanged += OnBreakChanged;
			speedSlider = AddSliderUnclamped(2350, GenericEntity.LOGIC_PREFIX + "speed", shipAI.speed, 0f, 5f, string.Empty);
			speedSlider.ValueChanged += OnSpeedChanged;
			turningSlider = AddSlider(4869, GenericEntity.LOGIC_PREFIX + "turning", shipAI.turning, 0f, 2f, string.Empty);
			turningSlider.ValueChanged += OnTurningChanged;
			circlingSlider = AddSlider(3774, GenericEntity.LOGIC_PREFIX + "circling", Mathf.Sqrt(shipAI.circleRadius), 10f, 100f, string.Empty);
			circlingSlider.logScaling = true;
			circlingSlider.ValueChanged += OnCirclingChanged;
			rangeSlider = AddSlider(2348, GenericEntity.LOGIC_PREFIX + "range", shipAI.disposition.behaviours[1].Radius, 0f, 250f, string.Empty);
			rangeSlider.logScaling = true;
			rangeSlider.ValueChanged += OnRangeChanged;
			ReferenceMaster.onLevelSimulation = (Action<bool>)Delegate.Combine(ReferenceMaster.onLevelSimulation, new Action<bool>(OnLevelSimulate));
		}
	}

	private void OnSpeedChanged(float newValue)
	{
		shipAI.speed = newValue;
	}

	private void OnTurningChanged(float newValue)
	{
		shipAI.turning = newValue;
	}

	private void OnCirclingChanged(float newValue)
	{
		shipAI.circleRadius = newValue * newValue;
	}

	private void OnRangeChanged(float newValue)
	{
		if (newValue < 0.1f)
		{
			newValue = 0.01f;
		}
		shipAI.disposition.behaviours[1].Radius = newValue;
		shipAI.disposition.behaviours[2].Radius = newValue * 0.2f;
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
		if (circlingSlider != null)
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
