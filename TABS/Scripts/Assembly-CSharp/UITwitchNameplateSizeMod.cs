using UnityEngine;
using UnityEngine.UI;

public class UITwitchNameplateSizeMod : MonoBehaviour
{
	private Slider SliderRef;

	private SettingsInstance m_healthBarSizeOption;

	public void SetNewSize(float size)
	{
		m_healthBarSizeOption.currentSliderValue = size;
		m_healthBarSizeOption.SaveSettings();
	}

	private void Awake()
	{
		m_healthBarSizeOption = ServiceLocator.GetService<GlobalSettingsHandler>().GetSettingsInstance("GAMEPLAY_HEALTHBAR_SIZE");
	}

	private void Start()
	{
		SliderRef = GetComponent<Slider>();
		SliderRef.minValue = m_healthBarSizeOption.min;
		SliderRef.maxValue = m_healthBarSizeOption.max;
		SliderRef.value = m_healthBarSizeOption.currentSliderValue;
	}
}
