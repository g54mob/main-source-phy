using System;
using Landfall.TABS;
using UnityEngine;

[RequireComponent(typeof(SimpleStateAnimation))]
public class SafeAreaAnimationPointAdjust : MonoBehaviour
{
	[Serializable]
	private class PlatformOverrideInfo
	{
		[SerializeField]
		[Tooltip("Override the settings for these platforms.")]
		private SettingsInstance.Platform platform;

		[SerializeField]
		[Tooltip("How much further up the position will move when title safe is scaled.")]
		private float movementRangeDifference;

		public SettingsInstance.Platform Platform => platform;

		public float MovementRangeDifference => movementRangeDifference;
	}

	[SerializeField]
	private float movementRangeDifference;

	[SerializeField]
	[Tooltip("Override the settings for specific platforms.")]
	private PlatformOverrideInfo[] overrideSettings;

	private SettingsInstance settingsSlider;

	private float defaultHeight;

	private float maxHeight;

	private float heightDifference;

	private float range;

	private SimpleStateAnimation animator;

	private void Awake()
	{
		animator = GetComponent<SimpleStateAnimation>();
		defaultHeight = animator.m_State01LocalPosistion.y;
		maxHeight = defaultHeight + GetMovementRangeDifference();
		heightDifference = defaultHeight - maxHeight;
	}

	private void OnEnable()
	{
		GlobalSettingsHandler service = ServiceLocator.GetService<GlobalSettingsHandler>();
		if (service != null)
		{
			settingsSlider = service.GetSettingsInstance(SafeArea.SAFE_AREA_SETTINGS_KEY);
			range = settingsSlider.max - settingsSlider.min;
			if (settingsSlider != null)
			{
				settingsSlider.OnSliderValueChanged += UpdateAnimationBounds;
				float currentSliderValue = settingsSlider.currentSliderValue;
				UpdateAnimationBounds(currentSliderValue);
			}
			else
			{
				Debug.LogError("Unable to find settings instance with key: " + SafeArea.SAFE_AREA_SETTINGS_KEY);
			}
		}
	}

	private void OnDisable()
	{
		if (settingsSlider != null)
		{
			settingsSlider.OnSliderValueChanged -= UpdateAnimationBounds;
		}
	}

	private float GetMovementRangeDifference()
	{
		if (overrideSettings == null || overrideSettings.Length == 0)
		{
			return movementRangeDifference;
		}
		SettingsInstance.Platform currentPlatform = GlobalSettingsHandler.CurrentPlatform;
		int i = 0;
		for (int num = overrideSettings.Length; i < num; i++)
		{
			PlatformOverrideInfo platformOverrideInfo = overrideSettings[i];
			if (platformOverrideInfo.Platform.HasFlag(currentPlatform))
			{
				return platformOverrideInfo.MovementRangeDifference;
			}
		}
		return movementRangeDifference;
	}

	private void UpdateAnimationBounds(float value)
	{
		if (settingsSlider != null)
		{
			if (value < 0.1f)
			{
				value = 100f;
			}
			float num = heightDifference * ((settingsSlider.min - value) / range);
			float y = maxHeight - num;
			Vector3 state01LocalPosistion = animator.m_State01LocalPosistion;
			state01LocalPosistion.y = y;
			animator.m_State01LocalPosistion = state01LocalPosistion;
		}
	}
}
