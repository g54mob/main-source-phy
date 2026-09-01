using TFBGames;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuSettingsButton : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	private const string DisabledLocalisationKey = "LABEL_DISABLED";

	public LocalizeText m_settingsNameText;

	public LocalizeText valueNameText;

	public Slider slider;

	public ScaleJiggle jiggle;

	private SettingsInstance m_settings;

	private GlobalSettingsHandler m_settingsHandler;

	private UISettingsItem uiSettingsItem;

	public SettingsInstance settingsInstance => m_settings;

	public void Init(SettingsInstance settings)
	{
		m_settings = settings;
		m_settingsHandler = ServiceLocator.GetService<GlobalSettingsHandler>();
		m_settingsNameText.LocaleID = settings.m_settingsKey;
		uiSettingsItem = GetComponentInChildren<UISettingsItem>();
		if (m_settings.DisableChanges)
		{
			if (uiSettingsItem != null)
			{
				uiSettingsItem.ChangeControlVisibility(enable: false);
			}
			OverrideValueText("LABEL_DISABLED");
			return;
		}
		if (settings.settingsType == SettingsInstance.SettingsType.Options)
		{
			settings.OnValueChanged += UpdateUI;
		}
		else
		{
			slider.value = Mathf.InverseLerp(settings.min, settings.max, settings.currentSliderValue);
			settings.OnSliderValueChanged += UpdateSliderUI;
		}
		UpdateValue();
	}

	public void Increment(int delta)
	{
		int num = m_settings.currentValue + delta;
		if (num >= m_settings.options.Length)
		{
			num = 0;
		}
		if (num < 0)
		{
			num = m_settings.options.Length - 1;
		}
		UpdateValue(num);
	}

	public void UpdateValue(int newValue)
	{
		m_settings.currentValue = newValue;
		UpdateValue();
	}

	private void UpdateUI(int newValue)
	{
		string text = m_settings.options[m_settings.currentValue];
		valueNameText.Localized = m_settings.localizeOptions && !float.TryParse(text, out var _);
		valueNameText.LocaleID = text;
	}

	private void UpdateSliderUI(float newValue)
	{
		if (!(valueNameText == null))
		{
			valueNameText.Text.text = m_settings.currentSliderValue.ToString("F0");
			if (m_settings.DisplayPercentage)
			{
				valueNameText.Text.text += "%";
			}
			slider.value = Mathf.InverseLerp(m_settings.min, m_settings.max, m_settings.currentSliderValue);
		}
	}

	public void UpdateValue()
	{
		if ((bool)jiggle)
		{
			jiggle.AddForce(2f);
		}
		if (!m_settings.DisableChanges)
		{
			if (m_settings.settingsType == SettingsInstance.SettingsType.Options)
			{
				m_settings.currentValue = Mathf.Clamp(m_settings.currentValue, 0, m_settings.options.Length - 1);
				UpdateUI(m_settings.currentValue);
			}
			else
			{
				m_settings.currentSliderValue = Mathf.Lerp(m_settings.min, m_settings.max, slider.value);
				UpdateSliderUI(m_settings.currentSliderValue);
			}
			if (GlobalSettingsHandler.DoesPlatformAllowRapidFileSystemAccess() || m_settings.m_settingsKey == "VIDEO_LANGUAGE")
			{
				m_settings.SaveSettings();
			}
		}
	}

	public void OpenOptionsPopUp()
	{
		if (!m_settings.DisableChanges)
		{
			GetComponentInParent<SettingsHandler>().OpenSettingsPopUp(m_settings, this);
		}
	}

	private void OverrideValueText(string localisationKey)
	{
		valueNameText.Localized = true;
		valueNameText.LocaleID = localisationKey;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (!(uiSettingsItem == null))
		{
			uiSettingsItem.OnSelect(eventData);
			if (!(uiSettingsItem.Selectable == null))
			{
				uiSettingsItem.Selectable.OnSelect(eventData);
			}
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (!(uiSettingsItem == null))
		{
			uiSettingsItem.OnDeselect(eventData);
			if (!(uiSettingsItem.Selectable == null))
			{
				uiSettingsItem.Selectable.OnDeselect(eventData);
			}
		}
	}
}
