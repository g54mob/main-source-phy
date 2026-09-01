using UnityEngine;

namespace TFBGames
{
	public class ScaleBySafeSpace : MonoBehaviour
	{
		private GlobalSettingsHandler globalSettingsHandler;

		private SettingsInstance settingsSlider;

		private RectTransform rect;

		private Vector3 defaultScale;

		private void Awake()
		{
			rect = GetComponent<RectTransform>();
			defaultScale = rect.localScale;
			globalSettingsHandler = ServiceLocator.GetService<GlobalSettingsHandler>();
			settingsSlider = globalSettingsHandler.GetSettingsInstance(SafeArea.SAFE_AREA_SETTINGS_KEY);
			UpdateScale(settingsSlider.currentSliderValue);
		}

		private void OnEnable()
		{
			if (settingsSlider != null)
			{
				settingsSlider.OnSliderValueChanged += UpdateScale;
			}
		}

		private void OnDisable()
		{
			if (settingsSlider != null)
			{
				settingsSlider.OnSliderValueChanged -= UpdateScale;
			}
		}

		private void UpdateScale(float safeSpace)
		{
			float num = safeSpace / settingsSlider.max;
			rect.localScale = defaultScale * num;
		}
	}
}
