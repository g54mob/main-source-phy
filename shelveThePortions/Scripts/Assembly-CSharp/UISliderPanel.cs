using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UISliderPanel : MonoBehaviour
{
	[SerializeField]
	private LocalizationTextActivator sliderTitle;

	[SerializeField]
	private TextMeshProUGUI sliderPercent;

	[SerializeField]
	private Slider slider;

	public GameObject HandSelector;

	[Space]
	[SerializeField]
	private UnityEvent<float> OnSliderChangeAction;

	private void OnEnable()
	{
		if (HandSelector != null)
		{
			HandSelector.SetActive(value: false);
		}
	}

	public void SetSlider(string title, int min, int max, int value)
	{
		if (sliderTitle != null)
		{
			sliderTitle.SetIDString(title);
		}
		SetSliderMinValue(min);
		SetSliderMaxValue(max);
		SetSliderValue(value);
	}

	public void SetSliderMinValue(int value)
	{
		slider.minValue = value;
	}

	public void SetSliderMaxValue(int value)
	{
		slider.maxValue = value;
	}

	public void SetSliderValue(float value)
	{
		slider.value = value;
		if (sliderPercent != null)
		{
			sliderPercent.text = Mathf.RoundToInt(slider.value * 100f).ToString();
		}
	}

	public void SetSliderValue(float value, float textValue)
	{
		slider.value = value;
		if (sliderPercent != null)
		{
			sliderPercent.text = textValue.ToString();
		}
	}

	public void OnSliderChange()
	{
		if (sliderPercent != null)
		{
			sliderPercent.text = Mathf.RoundToInt(slider.value * 100f).ToString();
		}
		if (OnSliderChangeAction != null)
		{
			OnSliderChangeAction.Invoke(slider.value);
		}
	}
}
