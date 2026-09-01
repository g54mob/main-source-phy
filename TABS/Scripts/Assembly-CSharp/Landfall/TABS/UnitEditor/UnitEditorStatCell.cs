using System.Globalization;
using InControl;
using Landfall.TABS_Input;
using TFBGames;
using TMPro;
using UnityEngine;

namespace Landfall.TABS.UnitEditor
{
	public class UnitEditorStatCell : UnitEditorSelectableItem
	{
		private const int SystemKeyboardMaxLength = 6;

		public LocalizeText m_NameText;

		public LocalizeText m_DescriptionText;

		public TMP_InputField m_InputField;

		private float value;

		private UnitEditorManager.StatsWrapper stat;

		private ISystemKeyboard keyboard;

		public void Init(UnitEditorManager.StatsWrapper stat)
		{
			this.stat = stat;
			base.gameObject.SetActive(value: true);
			m_NameText.LocaleID = stat.name;
			m_DescriptionText.LocaleID = stat.description;
			value = stat.defaultValue;
			UpdateValue();
			keyboard = ServiceLocator.GetService<SystemKeyboardProvider>().Keyboard;
			if (keyboard != null)
			{
				base.Submitted += delegate
				{
					keyboard.Show(KeyboardType.Numeric, value.ToString(CultureInfo.InvariantCulture), m_NameText.Text.text, m_DescriptionText.Text.text, 6);
					SubscribeToKeyboardEvents(subscribe: true);
				};
			}
		}

		public void OnEdit(string newValue)
		{
			SubscribeToKeyboardEvents(subscribe: false);
			if (float.TryParse(newValue, out var result))
			{
				value = Mathf.Clamp(result, stat.minValue, stat.maxValue);
				UpdateValue();
			}
		}

		public void Increase()
		{
			if (!UnitEditorManager.isTestingUnit)
			{
				value += Mathf.Clamp(Mathf.Floor(value), 1f, 100f) * 0.1f;
				value = Mathf.Clamp(value, value, stat.maxValue);
				UpdateValue();
			}
		}

		public void Decrease()
		{
			if (!UnitEditorManager.isTestingUnit)
			{
				value -= Mathf.Clamp(Mathf.Floor(value), 1f, 100f) * 0.1f;
				value = Mathf.Clamp(value, stat.minValue, value);
				UpdateValue();
			}
		}

		public void UpdateValue(bool forceNewValue = false)
		{
			if (forceNewValue)
			{
				value = stat.CurrentValue;
			}
			if (stat.statsMode == UnitEditorManager.StatMode.Multiplier)
			{
				m_InputField.text = value.ToString("F2") + "x";
			}
			else
			{
				m_InputField.text = Mathf.Round(value).ToString();
			}
			stat.CurrentValue = value;
		}

		protected override void Start()
		{
			base.Start();
			PlayerActions.Instance.OnLastInputTypeChanged += OnInputTypeChanged;
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			PlayerActions.Instance.OnLastInputTypeChanged -= OnInputTypeChanged;
			SubscribeToKeyboardEvents(subscribe: false);
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			SubscribeToKeyboardEvents(subscribe: false);
		}

		private void OnInputTypeChanged(BindingSourceType obj)
		{
			if (PlayerActions.Instance.InputType == InputType.Controller && m_InputField.isFocused)
			{
				Select();
			}
		}

		private void SubscribeToKeyboardEvents(bool subscribe)
		{
			if (keyboard != null)
			{
				keyboard.InputCompleted -= OnEdit;
				if (subscribe)
				{
					keyboard.InputCompleted += OnEdit;
				}
			}
		}
	}
}
