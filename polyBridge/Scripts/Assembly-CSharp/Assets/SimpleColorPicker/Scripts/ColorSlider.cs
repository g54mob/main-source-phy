using UnityEngine;
using UnityEngine.UI;

namespace Assets.SimpleColorPicker.Scripts
{
	public class ColorSlider : MonoBehaviour
	{
		public int MaxValue;

		public Slider Slider;

		public InputField InputField;

		public ColorPicker ColorPicker;

		public float Value => Slider.value;

		public void Set(float value)
		{
			Slider.value = value;
			InputField.text = Mathf.RoundToInt(value * (float)MaxValue).ToString();
		}

		public void OnValueChanged(float value)
		{
			if (!ColorPicker.Locked)
			{
				InputField.text = Mathf.RoundToInt(value * (float)MaxValue).ToString();
				ColorPicker.OnSliderChanged();
			}
		}

		public void OnValueChanged(string value)
		{
			if (!ColorPicker.Locked)
			{
				value = value.Replace("-", null);
				if (value == "")
				{
					InputField.text = "";
					return;
				}
				int num = Mathf.Min(int.Parse(value), MaxValue);
				InputField.text = num.ToString();
				Slider.value = (float)num / (float)MaxValue;
				ColorPicker.OnSliderChanged();
			}
		}
	}
}
