using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Kamgam.UGUIComponentsForSettings
{
	[SelectionBase]
	public class SliderUGUI : MonoBehaviour
	{
		public delegate void ValueChangedDelegate(float value);

		[Tooltip("How big should one step be. Keep in mind that very small step sizes are cumbersome if used with a controller or keyboard.")]
		public float StepSize;

		[Tooltip("The number format:\n'{0:N0}' = whole number without commas ('1')\n'{0:N1}' = one digit after the comma ('1.2')\n\nYou can learn more here: https://learn.microsoft.com/en-us/dotnet/standard/base-types/standard-numeric-format-strings")]
		public string ValueFormat;

		[Tooltip("Should the default move left/right input commands be used to change the value of the slider\nDisable if you want to ship your own controller input. You will have to call Increase() and Decrease() manually.")]
		public bool UseMoveCommandToChangeValue;

		public SliderWithEventOverridesUGUI Slider;

		[SerializeField]
		private Slider.SliderEvent OnValueChangedEvent;

		public ValueChangedDelegate OnValueChanged;

		[NonSerialized]
		protected float _lastSetValue;

		public TextMeshProUGUI TextTf;

		public TextMeshProUGUI ValueTf;

		public float MinValue
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public float MaxValue
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public bool WholeNumbers
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

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

		public int IntValue => 0;

		public string Text
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public void UpdateText()
		{
		}

		public void Start()
		{
		}

		private void onValueChangedHandler(float value)
		{
		}

		public bool onMove(AxisEventData eventData)
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
	}
}
