using UnityEngine;
using UnityEngine.UI;

namespace Synty.Interface.FantasyMenus.Samples
{
	public class SampleBrightnessDisplay : MonoBehaviour
	{
		[Header("References")]
		public Image image;

		public Slider brightnessValueSlider;

		[Header("Paramters")]
		public float multiplier = 1f;

		public float min;

		public float max = 1f;

		private Color defaultColor;

		private void Awake()
		{
			defaultColor = image.color;
			brightnessValueSlider.onValueChanged.AddListener(OnValueChanged);
			OnValueChanged(brightnessValueSlider.value);
		}

		private void OnDestroy()
		{
			brightnessValueSlider.onValueChanged.RemoveListener(OnValueChanged);
		}

		private void OnValueChanged(float value)
		{
			image.color = defaultColor * Mathf.Lerp(min, max, value * multiplier);
		}
	}
}
