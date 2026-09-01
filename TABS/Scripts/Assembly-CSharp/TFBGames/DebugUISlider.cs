using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TFBGames
{
	public class DebugUISlider : MonoBehaviour
	{
		[SerializeField]
		private Slider slider;

		[SerializeField]
		private TMP_Text label;

		[SerializeField]
		private string labelString = "[DEBUG]";

		private void Start()
		{
			base.gameObject.SetActive(value: false);
		}

		private void OnSliderValueChanged(float value)
		{
			if (label != null)
			{
				label.text = $"{labelString}: {value}";
			}
		}
	}
}
