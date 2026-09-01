using Poly.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Toolbox
{
	public class TimeScale : MonoBehaviour
	{
		public float timeScale = 1f;

		public float timeTillPause = float.PositiveInfinity;

		public float tmp_sliderValue;

		public Slider slider;

		public InspectorButton button0;

		private void OnEnable()
		{
			if ((bool)slider)
			{
				slider.onValueChanged.AddListener(UpdateFromGUI);
			}
		}

		private void OnDisable()
		{
			if ((bool)slider)
			{
				slider.onValueChanged.RemoveListener(UpdateFromGUI);
			}
		}

		public void FixedUpdate()
		{
			timeTillPause -= Time.fixedDeltaTime;
			if (timeTillPause <= 1E-06f)
			{
				timeScale = 0f;
				timeTillPause = float.PositiveInfinity;
			}
		}

		public void UpdateFromGUI(float sliderValue)
		{
			if (sliderValue > 1f)
			{
				sliderValue = (sliderValue - 0.5f) * (sliderValue - 0.5f) - 0.25f + 1f;
			}
			float num = sliderValue - timeScale;
			if (Mathf.Abs(sliderValue - 1f) < 0.1f && num * (sliderValue - 1f) < 0f)
			{
				sliderValue = 1f;
				slider.value = 1f;
			}
			timeScale = sliderValue;
			Time.timeScale = timeScale;
		}

		public void OnValidate()
		{
			timeScale = Mathf.Clamp(timeScale, 0f, 100f);
			Time.timeScale = timeScale;
			button0.text = "Simulate for 1 second";
			button0.action = ButtonAction;
		}

		private void ButtonAction()
		{
			if (Application.isPlaying)
			{
				if (timeScale == 0f)
				{
					timeScale = 1f;
				}
				timeTillPause = 1f;
			}
		}

		public void OnGUI()
		{
			float num = timeScale;
			if (num > 1f)
			{
				num = Mathf.Sqrt(num - 1f + 0.25f) + 0.5f;
			}
			if ((bool)slider)
			{
				slider.value = num;
			}
			tmp_sliderValue = num * 100f + 10f;
		}
	}
}
