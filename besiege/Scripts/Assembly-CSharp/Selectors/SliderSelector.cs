using UnityEngine;

namespace Selectors
{
	public class SliderSelector : Selector
	{
		[SerializeField]
		private DynamicText title;

		[SerializeField]
		private float maxTitleWidth = -1f;

		[SerializeField]
		private Transform sliderBackground;

		[SerializeField]
		private UIButton sliderButton;

		[SerializeField]
		private Transform sliderKnob;

		[SerializeField]
		private SliderHolder valueHolder;

		private bool updateCallback;

		private Camera hudCamera;

		private bool isDragging;

		private float _value;

		private bool hadSlider;

		public override MapperType MapperType
		{
			get
			{
				return Slider;
			}
			set
			{
				if (updateCallback)
				{
					if (Slider != null)
					{
						Slider.ValueChanged -= OnValueChanged;
						sliderButton.Held -= Slider.HeldDown;
						sliderButton.Released -= Slider.ReleasedButton;
					}
					updateCallback = false;
				}
				Slider = (MSlider)value;
				if (Slider != null)
				{
					Slider.ValueChanged += OnValueChanged;
					sliderButton.Held += Slider.HeldDown;
					sliderButton.Released += Slider.ReleasedButton;
					updateCallback = true;
					hadSlider = true;
				}
			}
		}

		public MSlider Slider { get; set; }

		public float Min
		{
			get
			{
				return (Slider == null) ? valueHolder.Min : Slider.Min;
			}
		}

		public float Max
		{
			get
			{
				return (Slider == null) ? valueHolder.Max : Slider.Max;
			}
		}

		public string Prefix
		{
			get
			{
				return (Slider == null) ? valueHolder.prefix : Slider.Prefix;
			}
		}

		public string Suffix
		{
			get
			{
				return (Slider == null) ? valueHolder.suffix : Slider.Suffix;
			}
		}

		private float LeftPosition
		{
			get
			{
				return sliderBackground.position.x - sliderBackground.lossyScale.x / 2f;
			}
		}

		private float RightPosition
		{
			get
			{
				return sliderBackground.position.x + sliderBackground.lossyScale.x / 2f;
			}
		}

		public float Value
		{
			get
			{
				return (Slider == null) ? _value : Slider.Value;
			}
			set
			{
				if (Slider != null)
				{
					Slider.Value = value;
				}
				else
				{
					_value = value;
					UpdateText();
				}
				UpdateSliderKnobFromValue();
				if (this.OnChanged != null)
				{
					this.OnChanged();
				}
			}
		}

		public event OnChanged OnChanged;

		private void Awake()
		{
			hudCamera = GameObject.Find("HUD Cam").GetComponent<Camera>();
			sliderButton.Down += delegate
			{
				isDragging = true;
			};
			sliderButton.Released += OnSliderRelease;
			valueHolder.ValueChanged += OnManualInput;
		}

		private void Start()
		{
			if (!hadSlider)
			{
				_value = (Min + Max) * 0.5f;
				UpdateText();
			}
		}

		private void OnSliderRelease()
		{
			if (isDragging)
			{
				isDragging = false;
				if (base.gameObject.activeInHierarchy && MapperType != null)
				{
					OnEdit();
				}
			}
		}

		private void OnValueChanged(float newValue)
		{
			UpdateVisual();
		}

		protected override void UpdateVisual()
		{
			UpdateSliderKnobFromValue();
			UpdateText();
		}

		private void OnManualInput(float value)
		{
			bool flag = Slider == null;
			if (!flag && Slider.Looped)
			{
				float num = Slider.Max - Slider.Min;
				while (value > Slider.Max)
				{
					value -= num;
				}
				while (value < Slider.Min)
				{
					value += num;
				}
				Value = value;
			}
			else if (StatMaster.KeyMapper.disableSliderLimits || (!flag && Slider.Unclamped))
			{
				if (!flag && Slider.UnsignedOnly && value < 0f)
				{
					value = 0f;
				}
				Value = value;
			}
			else if (flag)
			{
				Value = Mathf.Clamp(value, valueHolder.minValue, valueHolder.maxValue);
			}
			else
			{
				Value = Mathf.Clamp(value, Min, Max);
			}
			if (MapperType != null)
			{
				OnEdit();
			}
		}

		private void OnDisable()
		{
			if (updateCallback)
			{
				if (Slider != null)
				{
					Slider.ValueChanged -= OnValueChanged;
					sliderButton.Held -= Slider.HeldDown;
					sliderButton.Released -= Slider.ReleasedButton;
				}
				updateCallback = false;
			}
		}

		private void Update()
		{
			if (isDragging)
			{
				float x = hudCamera.ScreenToWorldPoint(Input.mousePosition).x;
				MoveSliderKnob(x);
			}
		}

		private void UpdateText()
		{
			valueHolder.SetText(Value);
			valueHolder.SetConflict(InConflict());
		}

		public override void Init()
		{
			if (Slider == null)
			{
				Debug.LogWarning("Slider has not been assigned to " + base.transform.name);
				return;
			}
			base.Init();
			valueHolder.SetPrefixSuffix(Slider.Prefix, Slider.Suffix);
			UpdateTitle();
			valueHolder.disableSliderLimits = Slider.Unclamped;
			UpdateVisual();
		}

		private void UpdateTitle()
		{
			string text = Slider.DisplayName.Replace(" ", "\n").ToUpper();
			if (maxTitleWidth != -1f)
			{
				title.transform.localScale = Vector3.one;
			}
			string[] array = text.Split('\n');
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].Length > 8)
				{
					title.letterSpacing = 0.05f;
					break;
				}
			}
			title.SetText(text);
			if (maxTitleWidth != -1f)
			{
				Renderer component = title.GetComponent<Renderer>();
				float x = component.bounds.size.x;
				if (x > maxTitleWidth)
				{
					float num = maxTitleWidth / x;
					title.transform.localScale = new Vector3(num, num, num);
				}
			}
		}

		private void UpdateSliderKnobFromValue()
		{
			float value = (Value - Min) / (Max - Min);
			value = Mathf.Clamp01(value);
			if (Slider != null && Slider.logScaling)
			{
				if (Min < 0f)
				{
					value = value * 2f - 1f;
					float num = ((!(value < 0f)) ? 1f : (-1f));
					value = (Mathf.Sqrt(Mathf.Abs(value)) * num + 1f) * 0.5f;
				}
				else
				{
					value = Mathf.Sqrt(value);
				}
			}
			if (float.IsNaN(value))
			{
				value = 1f;
			}
			float x = value * (RightPosition - LeftPosition) + LeftPosition;
			sliderKnob.position = new Vector3(x, sliderKnob.position.y, sliderKnob.position.z);
		}

		private void MoveSliderKnob(float worldX)
		{
			float num = (worldX - LeftPosition) / (RightPosition - LeftPosition);
			if (Slider != null && Slider.Looped)
			{
				for (; num < 0f; num += 1f)
				{
				}
				while (num > 1f)
				{
					num -= 1f;
				}
			}
			else
			{
				num = Mathf.Clamp(num, 0f, 1f);
			}
			if (Slider != null && Slider.logScaling)
			{
				if (Min < 0f)
				{
					num = num * 2f - 1f;
					float num2 = ((!(num < 0f)) ? 1f : (-1f));
					num = (Mathf.Pow(Mathf.Abs(num), 2f) * num2 + 1f) * 0.5f;
				}
				else
				{
					num *= num;
				}
			}
			Value = ((!hadSlider || !Slider.maxInfinity || num != 1f) ? (num * (Max - Min) + Min) : float.PositiveInfinity);
		}
	}
}
