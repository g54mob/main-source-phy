using System;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Poly.UI
{
	[Serializable]
	public class UiSliderWrapper
	{
		public Slider slider;

		public Text valueLabel;

		public string valueSuffix;

		public UnityAction<float> onValueChanged;

		public void InitValue(float value)
		{
			slider.value = value;
			DisplayValue(value);
		}

		public void OnEnable()
		{
			onValueChanged = (UnityAction<float>)Delegate.Combine(onValueChanged, new UnityAction<float>(DisplayValue));
			slider.onValueChanged.AddListener(onValueChanged);
		}

		public void OnDisable()
		{
			slider.onValueChanged.RemoveListener(onValueChanged);
			onValueChanged = (UnityAction<float>)Delegate.Remove(onValueChanged, new UnityAction<float>(DisplayValue));
		}

		private void DisplayValue(float value)
		{
			valueLabel.text = $"{value:0.0}{valueSuffix}";
		}
	}
}
