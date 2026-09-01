using System;
using UnityEngine;

public class WaterZoneEntity : GenericEntity
{
	public WaterZone waterZone;

	public float radiusMultiplier = 10f;

	[SerializeField]
	private GameObject hideOnPlay;

	private MSlider baseValueSlider;

	private MSlider exponentialIncreaseSlider;

	private CalmZoneController calmZoneController;

	private Transform zoneTransform;

	public override void Init()
	{
		if (isInitialized)
		{
			return;
		}
		base.Init();
		if (waterZone == null)
		{
			LevelEnvironment env = LevelEditor.Instance.environmentManager.GetEnv(LevelSettings.LevelEnvironment.Water);
			if (env == null || env.envSetup[0] == null)
			{
				return;
			}
			calmZoneController = env.envSetup[0].GetComponentInChildren<CalmZoneController>();
			GameObject gameObject = new GameObject("WaterZone", typeof(WaterZone));
			zoneTransform = gameObject.transform;
			zoneTransform.SetParent(calmZoneController.transform, false);
			waterZone = gameObject.GetComponent<WaterZone>();
			waterZone.Pct = 1f;
			waterZone.Value = 0.5f;
			OnTransformChanged();
		}
		baseValueSlider = AddSliderUnclamped(4583, GenericEntity.LOGIC_PREFIX + "base_intensity", waterZone.baseValue, -4f, 4f, string.Empty);
		baseValueSlider.logScaling = true;
		baseValueSlider.ValueChanged += OnBaseValueChanged;
		exponentialIncreaseSlider = AddSlider(4584, GenericEntity.LOGIC_PREFIX + "exp_incr", waterZone.exponentialIncrease, 0.01f, 10f, string.Empty);
		exponentialIncreaseSlider.logScaling = true;
		exponentialIncreaseSlider.ValueChanged += OnExpSizeChanged;
		ReferenceMaster.onLevelSimulation = (Action<bool>)Delegate.Combine(ReferenceMaster.onLevelSimulation, new Action<bool>(OnLevelSimulate));
	}

	private void OnLevelSimulate(bool toggle)
	{
		hideOnPlay.SetActive(!toggle);
		zoneTransform.position = entity.myTransform.position;
		zoneTransform.rotation = entity.myTransform.rotation;
		waterZone.lastPos = zoneTransform.position;
		if (waterZone.secondary)
		{
			waterZone.superZone.needsUpdate = true;
		}
		waterZone.needsUpdate = true;
	}

	public override void OnRemove()
	{
		base.OnRemove();
		if (waterZone != null)
		{
			UnityEngine.Object.Destroy(waterZone);
		}
		if (baseValueSlider != null)
		{
			ReferenceMaster.onLevelSimulation = (Action<bool>)Delegate.Remove(ReferenceMaster.onLevelSimulation, new Action<bool>(OnLevelSimulate));
		}
	}

	private void OnTransformChanged()
	{
		if (!(zoneTransform == null))
		{
			zoneTransform.position = entity.myTransform.position;
			zoneTransform.rotation = entity.myTransform.rotation;
			Vector3 scale = entity.Scale;
			waterZone.Range = Mathf.Max(scale.x, scale.y, scale.z) * radiusMultiplier;
			if (waterZone.secondary)
			{
				waterZone.superZone.needsUpdate = true;
			}
			waterZone.needsUpdate = true;
		}
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

	protected void OnBaseValueChanged(float newValue)
	{
		waterZone.Value = newValue;
	}

	private void OnExpSizeChanged(float newValue)
	{
		waterZone.Exponent = newValue;
	}

	public override bool TriggerEvaluate()
	{
		return false;
	}
}
