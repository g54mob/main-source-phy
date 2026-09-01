using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Kamgam.UGUIComponentsForSettings
{
	public class StepperUGUI : MonoBehaviour
	{
		public delegate void OnValueChangedDelegate(float value);

		public UnityEvent<float> OnValueChangedEvent;

		public OnValueChangedDelegate OnValueChanged;

		public float MinValue;

		public float MaxValue;

		public float StepSize;

		public bool WholeNumbers;

		public GameObject StepTemplate;

		public GameObject StepsContainer;

		[NonSerialized]
		protected List<StepperStepConsoleUGUI> _steps;

		public string ValueFormat;

		[Tooltip("Should the buttons be disabled if the limits (min,max) are reached?")]
		public bool DisableButtons;

		public Button DecreaseButton;

		public Button IncreaseButton;

		protected AutoNavigationOverrides decreaseButtonNavigationOverrides;

		protected AutoNavigationOverrides increaseButtonNavigationOverrides;

		protected float _value;

		public TextMeshProUGUI TextTf;

		public TextMeshProUGUI ValueTf;

		[SerializeField]
		[Tooltip("If enabled and if this is selected (has focus) then the descrease/increase action will be triggered by keyboard/controller navigation too.\nNOTICE: This also means it will deny left/right selection navigation away from this is object. Useful for console type UIs.")]
		protected bool _enableButtonControls;

		protected AutoNavigationOverrides _autoNavigationOverrides;

		protected Selectable _selectable;

		public bool ShowSteps => false;

		public float StepCountFloat => 0f;

		public int StepCount => 0;

		public AutoNavigationOverrides DecreaseButtonNavigationOverrides => null;

		public AutoNavigationOverrides IncreaseButtonNavigationOverrides => null;

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

		public bool EnableButtonControls
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		public AutoNavigationOverrides AutoNavigationOverrides => null;

		public Selectable Selectable => null;

		protected void updateValue(float value)
		{
		}

		public void Refresh()
		{
		}

		protected bool hasValidSteps()
		{
			return false;
		}

		protected void updateText(string text)
		{
		}

		public void OnEnable()
		{
		}

		public virtual void Update()
		{
		}

		public float ConvertToStepValue(float value)
		{
			return 0f;
		}

		public int GetStepToDisplay(float value)
		{
			return 0;
		}

		public void Increase()
		{
		}

		public void IncreaseLooped()
		{
		}

		public void Decrease()
		{
		}

		public void Step(int steps)
		{
		}

		protected void updateButtons()
		{
		}

		public void SetSelected()
		{
		}
	}
}
