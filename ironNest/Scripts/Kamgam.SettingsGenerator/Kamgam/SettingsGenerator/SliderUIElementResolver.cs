using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Kamgam.SettingsGenerator
{
	public class SliderUIElementResolver : SettingResolverForVisualElement, ISettingResolver
	{
		[Tooltip("How big should one step be. Keep in mind that very small step sizes are cumbersome if used with a controller or keyboard.")]
		public float StepSize;

		[Tooltip("Should the value be rounded to integers?\nNOTICE: If connected to an Integer settings then this will always be true.")]
		public bool WholeNumbers;

		[NonSerialized]
		protected float _value;

		public string ValueFormat;

		[Tooltip("Should the default move left/right input commands be used to change the value of the slider\nDisable if you want to ship your own controller input. You will have to call Increase() and Decrease() manually.")]
		public bool UseMoveCommandToChangeValue;

		protected Slider _slider;

		protected TextField _valueTf;

		protected SettingData.DataType[] supportedDataTypes;

		protected bool stopPropagation;

		public float Value
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public Slider Slider => null;

		public TextField ValueTf => null;

		public override SettingData.DataType[] GetSupportedDataTypes()
		{
			return null;
		}

		public override void OnEnable()
		{
		}

		public override void OnDisable()
		{
		}

		public override void OnDestroy()
		{
		}

		protected void onValueChanged(ChangeEvent<float> evt)
		{
		}

		public void onMove(NavigationMoveEvent evt)
		{
		}

		protected bool isFocused(VisualElement ele)
		{
			return false;
		}

		public float ConvertToStepValue(float value)
		{
			return 0f;
		}

		public void Increase()
		{
		}

		public void Decrease()
		{
		}

		public void Step(int steps)
		{
		}

		public override void Refresh()
		{
		}
	}
}
