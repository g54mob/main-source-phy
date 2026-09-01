using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TFBGames
{
	public class LocalMultiplayerSettingsButton : MonoBehaviour
	{
		private enum SelectableType
		{
			Button = 0,
			Slider = 1
		}

		[SerializeField]
		private TextMeshProUGUI settingsNameText;

		[SerializeField]
		private TextMeshProUGUI valueNameText;

		[SerializeField]
		private LocalizeText valueNameLocalizeText;

		[SerializeField]
		private ScaleJiggle jiggle;

		[SerializeField]
		private UISettingsItem uiSettingsItem;

		[SerializeField]
		private SelectableType selectableType;

		[SerializeField]
		private Button button;

		[SerializeField]
		private Slider slider;

		private SliderData sliderData;

		private List<string> options;

		private int index;

		private int size;

		public int Index => index;

		public SliderData SliderData => sliderData;

		public UISettingsItem UISettingsItem => uiSettingsItem;

		public Selectable GetSelectable()
		{
			if (selectableType != SelectableType.Slider)
			{
				return button;
			}
			return slider;
		}

		public void Init(ISettingsItemHandler handler, List<string> options, int index = 0)
		{
			AssignHandler(handler);
			if (options != null)
			{
				this.options = options;
				size = options.Count;
				this.index = index;
				SetLocaleID(options[index]);
			}
		}

		public void Init<T>(ISettingsItemHandler handler, int index = 0) where T : Enum
		{
			AssignHandler(handler);
			this.index = index;
			options = new List<string>(Enum.GetNames(typeof(T)));
			size = options.Count;
			SetLocaleID(options[index]);
		}

		public void Init(ISettingsItemHandler handler, SliderData sliderData)
		{
			AssignHandler(handler);
			this.sliderData = sliderData;
			if (slider != null)
			{
				slider.value = Mathf.InverseLerp(sliderData.min, sliderData.max, sliderData.current);
			}
			SetValueText(sliderData.current.ToString("F0"));
		}

		public void Increment(int delta)
		{
			index = (index + delta + size) % size;
			SetLocaleID(options[index]);
		}

		public void IncrementValue(float delta)
		{
			float current = Mathf.Clamp(sliderData.current + delta, sliderData.min, sliderData.max);
			sliderData.current = current;
			SetValueText(current.ToString("F0"));
		}

		public void OnSliderValueChanged(float value)
		{
			float num = Mathf.Lerp(sliderData.min, sliderData.max, value);
			if (sliderData.useRounding)
			{
				num = sliderData.GetRoundedValue(num);
			}
			sliderData.current = num;
			SetValueText(num.ToString("F0"));
		}

		private void AssignHandler(ISettingsItemHandler handler)
		{
			if (uiSettingsItem != null)
			{
				uiSettingsItem.SetSettingsItemHandler(handler);
			}
		}

		private void SetLocaleID(string id)
		{
			if (valueNameLocalizeText != null)
			{
				valueNameLocalizeText.LocaleID = id;
			}
		}

		private void SetValueText(string text)
		{
			if (valueNameText != null)
			{
				valueNameText.text = text;
			}
		}
	}
}
