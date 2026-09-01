using TMPro;
using UnityEngine;

namespace TFBGames
{
	[RequireComponent(typeof(TextMeshProUGUI))]
	public class InputBindingText : MonoBehaviour
	{
		[SerializeField]
		[Tooltip("Overrides for changing the text.")]
		protected InputBindingTextOverride[] m_overrides;

		[SerializeField]
		[HideInInspector]
		private TextMeshProUGUI m_text;

		private string m_defaultText;

		private GlobalSettingsHandler m_settingsHandler;

		private void Awake()
		{
			m_defaultText = m_text.text;
			m_settingsHandler = ServiceLocator.GetService<GlobalSettingsHandler>();
		}

		private void OnEnable()
		{
			SubscribeToOverrideEvents(subscribe: true);
			UpdateText();
		}

		private void OnDisable()
		{
			SubscribeToOverrideEvents(subscribe: false);
		}

		private void UpdateText()
		{
			m_text.text = GetDynamicText();
		}

		private string GetDynamicText()
		{
			if (m_overrides == null || m_overrides.Length == 0 || m_settingsHandler == null)
			{
				return m_defaultText;
			}
			int i = 0;
			for (int num = m_overrides.Length; i < num; i++)
			{
				InputBindingTextOverride inputBindingTextOverride = m_overrides[i];
				if (inputBindingTextOverride.condition == InputBindingTextCondition.GlobalSetting)
				{
					SettingsInstance settingsInstance = m_settingsHandler.GetSettingsInstance(inputBindingTextOverride.settingKey);
					if (settingsInstance != null && ((settingsInstance.settingsType == SettingsInstance.SettingsType.Options && inputBindingTextOverride.value == settingsInstance.currentValue) || (settingsInstance.settingsType == SettingsInstance.SettingsType.Slider && Mathf.Approximately(inputBindingTextOverride.sliderValue, settingsInstance.currentSliderValue))))
					{
						return inputBindingTextOverride.text;
					}
				}
			}
			return m_defaultText;
		}

		private void SubscribeToOverrideEvents(bool subscribe)
		{
			if (m_overrides == null || m_overrides.Length == 0 || m_settingsHandler == null)
			{
				return;
			}
			int i = 0;
			for (int num = m_overrides.Length; i < num; i++)
			{
				InputBindingTextOverride inputBindingTextOverride = m_overrides[i];
				if (inputBindingTextOverride.condition != InputBindingTextCondition.GlobalSetting)
				{
					continue;
				}
				SettingsInstance settingsInstance = m_settingsHandler.GetSettingsInstance(inputBindingTextOverride.settingKey);
				if (settingsInstance == null)
				{
					continue;
				}
				if (subscribe)
				{
					if (settingsInstance.settingsType == SettingsInstance.SettingsType.Options)
					{
						settingsInstance.OnValueChanged += OnSettingsValueChanged;
					}
					else
					{
						settingsInstance.OnSliderValueChanged += OnSettingsSliderValueChanged;
					}
				}
				else if (settingsInstance.settingsType == SettingsInstance.SettingsType.Options)
				{
					settingsInstance.OnValueChanged -= OnSettingsValueChanged;
				}
				else
				{
					settingsInstance.OnSliderValueChanged -= OnSettingsSliderValueChanged;
				}
			}
		}

		private void OnSettingsValueChanged(int obj)
		{
			UpdateText();
		}

		private void OnSettingsSliderValueChanged(float obj)
		{
			UpdateText();
		}
	}
}
