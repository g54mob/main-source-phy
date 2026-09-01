using UnityEngine;

namespace Selectors
{
	public class SliderHolder : ValueHolder
	{
		[SerializeField]
		private SliderSelector sliderSelector;

		[SerializeField]
		private float _min = float.MinValue;

		[SerializeField]
		private float _max = float.MaxValue;

		public bool disableSliderLimits;

		public float Min
		{
			get
			{
				return (!(sliderSelector == null) && sliderSelector.Slider != null) ? sliderSelector.Slider.Min : _min;
			}
			set
			{
				_min = value;
			}
		}

		public float Max
		{
			get
			{
				return (!(sliderSelector == null) && sliderSelector.Slider != null) ? sliderSelector.Slider.Max : _max;
			}
			set
			{
				_max = value;
			}
		}

		protected override bool ValidateValue(float newValue, out float validatedValue, bool isExternalSet = false)
		{
			if (!isExternalSet && !StatMaster.KeyMapper.disableSliderLimits && !disableSliderLimits)
			{
				newValue = Mathf.Clamp(newValue, Min, Max);
			}
			return base.ValidateValue(newValue, out validatedValue, isExternalSet);
		}
	}
}
