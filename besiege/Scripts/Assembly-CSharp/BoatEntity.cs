using System;
using UnityEngine;

[AddComponentMenu("LevelEditor/Entities/BoatEntity")]
public class BoatEntity : GenericEntity
{
	public MannedBoatAI boatAI;

	private MSlider speedSlider;

	private MSlider turningSlider;

	private MSlider rangeSlider;

	public override void Init()
	{
		if (!isInitialized)
		{
			base.Init();
			breakForceAmount.ValueChanged += OnBreakChanged;
			speedSlider = AddSliderUnclamped(2350, GenericEntity.LOGIC_PREFIX + "speed", boatAI.speed, 0f, 3f, string.Empty);
			speedSlider.ValueChanged += OnSpeedChanged;
			turningSlider = AddSlider(0, GenericEntity.LOGIC_PREFIX + "turning", boatAI.maxSpeedForTurn, 0f, 2f, string.Empty);
			turningSlider.ValueChanged += OnTurningChanged;
			rangeSlider = AddSlider(2348, GenericEntity.LOGIC_PREFIX + "range", 175f, 0f, 250f, string.Empty);
			rangeSlider.logScaling = true;
			rangeSlider.ValueChanged += OnRangeChanged;
			ReferenceMaster.onLevelSimulation = (Action<bool>)Delegate.Combine(ReferenceMaster.onLevelSimulation, new Action<bool>(OnLevelSimulate));
		}
	}

	private void OnSpeedChanged(float newValue)
	{
		boatAI.speed = newValue;
	}

	private void OnTurningChanged(float newValue)
	{
		boatAI.maxSpeedForTurn = newValue;
	}

	private void OnRangeChanged(float newValue)
	{
	}

	private void OnBreakChanged(float newValue)
	{
	}

	protected override void OnPhysicsToggled(bool toggle)
	{
		if (!startingSim)
		{
			bool flag = !toggle;
			if (entity.isStatic != flag)
			{
				boatAI.enabled = toggle;
			}
			base.OnPhysicsToggled(toggle);
		}
	}

	private void OnLevelSimulate(bool toggle)
	{
	}

	public override void OnRemove()
	{
		base.OnRemove();
		ReferenceMaster.onLevelSimulation = (Action<bool>)Delegate.Remove(ReferenceMaster.onLevelSimulation, new Action<bool>(OnLevelSimulate));
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
