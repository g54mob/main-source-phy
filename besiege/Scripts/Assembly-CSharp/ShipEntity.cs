using System;
using UnityEngine;

[AddComponentMenu("LevelEditor/Entities/ShipEntity")]
public class ShipEntity : GenericEntity
{
	public ShipMultibodyAI shipAI;

	public ShipDamageController shipDmg;

	private MSlider speedSlider;

	private MSlider turningSlider;

	private MSlider circlingSlider;

	private MSlider rangeSlider;

	private MSlider damageSlider;

	private MSlider hpSlider;

	public override void Init()
	{
		if (!isInitialized)
		{
			base.Init();
			breakForceAmount.ValueChanged += OnBreakChanged;
			damageSlider = AddSliderUnclamped(4870, GenericEntity.LOGIC_PREFIX + "damage", shipDmg.bottomHullParts[0].forceToDamage, 100f, 10000f, string.Empty);
			damageSlider.logScaling = true;
			damageSlider.ValueChanged += OnDamageChanged;
			hpSlider = AddSliderUnclamped(2480, GenericEntity.LOGIC_PREFIX + "hitpoints", shipDmg.bottomHullParts[0].hitPoints, 0f, 50f, string.Empty);
			hpSlider.ValueChanged += OnHealthChanged;
			speedSlider = AddSliderUnclamped(2350, GenericEntity.LOGIC_PREFIX + "speed", shipAI.globalSpeed, 0f, 3f, string.Empty);
			speedSlider.ValueChanged += OnSpeedChanged;
			turningSlider = AddSlider(4869, GenericEntity.LOGIC_PREFIX + "turning", shipAI.turningSpeed, 0f, 2f, string.Empty);
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
		shipAI.globalSpeed = newValue;
	}

	private void OnTurningChanged(float newValue)
	{
		shipAI.turningSpeed = newValue;
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
		shipAI.disposition.behaviours[2].Radius = newValue * (7f / 12f);
	}

	private void OnDamageChanged(float newValue)
	{
		foreach (ShipPartHitManager bottomHullPart in shipDmg.bottomHullParts)
		{
			bottomHullPart.forceToDamage = newValue;
		}
		foreach (ShipPartHitManager topHullPart in shipDmg.topHullParts)
		{
			topHullPart.forceToDamage = newValue;
		}
	}

	private void OnBreakChanged(float newValue)
	{
	}

	private void OnHealthChanged(float newValue)
	{
		foreach (ShipPartHitManager bottomHullPart in shipDmg.bottomHullParts)
		{
			bottomHullPart.hitPoints = newValue;
		}
		foreach (ShipPartHitManager topHullPart in shipDmg.topHullParts)
		{
			topHullPart.hitPoints = newValue;
		}
	}

	protected override void OnPhysicsToggled(bool toggle)
	{
		if (startingSim)
		{
			return;
		}
		bool flag = !toggle;
		if (entity.isStatic != flag)
		{
			shipAI.enabled = toggle;
			shipDmg.enabled = toggle;
			foreach (ShipPartHitManager bottomHullPart in shipDmg.bottomHullParts)
			{
				bottomHullPart.enabled = toggle;
			}
			foreach (ShipPartHitManager topHullPart in shipDmg.topHullParts)
			{
				topHullPart.enabled = toggle;
			}
		}
		base.OnPhysicsToggled(toggle);
	}

	private void OnLevelSimulate(bool toggle)
	{
	}

	public override void OnRemove()
	{
		base.OnRemove();
		if (damageSlider != null)
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
