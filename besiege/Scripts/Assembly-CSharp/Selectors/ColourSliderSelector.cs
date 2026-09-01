using UnityEngine;

namespace Selectors
{
	public class ColourSliderSelector : Selector
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
		private ColourHolder valueHolder;

		[SerializeField]
		private Texture colourPicker;

		private bool updateCallback;

		private Camera hudCamera;

		private bool isDragging;

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
				Slider = (MColourSlider)value;
				if (Slider != null)
				{
					Slider.ValueChanged += OnValueChanged;
					sliderButton.Held += Slider.HeldDown;
					sliderButton.Released += Slider.ReleasedButton;
					updateCallback = true;
				}
			}
		}

		public MColourSlider Slider { get; set; }

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

		public Color Value
		{
			get
			{
				return Slider.Value;
			}
			set
			{
				Slider.Value = value;
				UpdateVisual();
			}
		}

		private void Awake()
		{
			hudCamera = GameObject.Find("HUD Cam").GetComponent<Camera>();
			sliderButton.Down += delegate
			{
				isDragging = true;
			};
			sliderButton.Released += OnSliderRelease;
			valueHolder.ColourChanged += OnManualInput;
		}

		protected void OnDisable()
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

		private void OnValueChanged(Color newColor)
		{
			UpdateVisual();
		}

		protected override void UpdateVisual()
		{
			UpdateSliderKnobFromValue();
			UpdateText();
		}

		private void OnSliderRelease()
		{
			if (isDragging)
			{
				isDragging = false;
				if (base.gameObject.activeInHierarchy)
				{
					OnEdit();
				}
			}
		}

		private void OnManualInput(Color newValue)
		{
			if (Slider.snapColors)
			{
				Value = SnapColor(newValue);
				valueHolder.SetText(Value);
			}
			else
			{
				Value = newValue;
			}
			OnEdit();
		}

		private void Update()
		{
			if (isDragging)
			{
				float x = hudCamera.ScreenToWorldPoint(InputManager.CursorPosition()).x;
				MoveSliderKnob(x);
			}
		}

		private void UpdateText()
		{
			if (Slider != null)
			{
				valueHolder.SetText(Slider.Value);
				valueHolder.SetConflict(InConflict());
			}
		}

		public override void Init()
		{
			if (Slider == null)
			{
				Debug.LogWarning("Colour slider has not been assigned to " + base.transform.name);
				return;
			}
			base.Init();
			UpdateTitle();
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

		private int ColorToPixelPos(Color color)
		{
			if (Slider.useHue)
			{
				return HueToPixelPos(color);
			}
			return ClosestColorPos(color);
		}

		private int ClosestColorPos(Color color)
		{
			float num = 180f;
			int result = 0;
			Texture2D texture2D = colourPicker as Texture2D;
			color = ((!(color == Color.black)) ? color : new Color(0.01f, 0f, 0f, 1f));
			for (int i = 0; i < (colourPicker as Texture2D).width; i++)
			{
				Color pixel = texture2D.GetPixel(i, 0);
				float num2 = Mathf.Abs(color.r - pixel.r) + Mathf.Abs(color.g - pixel.g) + Mathf.Abs(color.b - pixel.b);
				float num3 = Vector3.Angle(ColorToVector3(color), ColorToVector3(pixel));
				if (num3 * num2 < num)
				{
					num = num3 * num2;
					result = i;
				}
			}
			return result;
		}

		private int HueToPixelPos(Color color)
		{
			float S;
			float V;
			float H;
			Color.RGBToHSV(color, out H, out S, out V);
			H = 1f - H;
			return Mathf.RoundToInt(H * (float)(colourPicker as Texture2D).width);
		}

		private Color SnapColor(Color color)
		{
			return (colourPicker as Texture2D).GetPixel(ColorToPixelPos(color), 0);
		}

		private void UpdateSliderKnobFromValue()
		{
			UpdateSliderKnobFromXPos(ColorToPixelPos(Slider.Value));
		}

		private void UpdateSliderKnobFromXPos(int xPos)
		{
			float num = Mathf.Clamp01((float)xPos / ((float)(colourPicker as Texture2D).width * 1f));
			float x = ((!Slider.useHue) ? (num * (RightPosition - LeftPosition) + LeftPosition) : Mathf.Lerp(RightPosition, LeftPosition, num));
			sliderKnob.position = new Vector3(x, sliderKnob.position.y, sliderKnob.position.z);
		}

		private void MoveSliderKnob(float worldX)
		{
			float num = Mathf.Clamp01((worldX - LeftPosition) / (RightPosition - LeftPosition));
			if (Slider != null)
			{
				int x = Mathf.FloorToInt(num * (float)(colourPicker as Texture2D).width);
				Slider.Value = (colourPicker as Texture2D).GetPixel(x, 0);
			}
			sliderKnob.position = new Vector3(Mathf.Clamp(worldX, LeftPosition, RightPosition), sliderKnob.position.y, sliderKnob.position.z);
		}

		protected Vector3 ColorToVector3(Color color)
		{
			return new Vector3(color.r, color.g, color.b);
		}

		protected Color Vector3ToColor(Vector3 vector)
		{
			return new Color(vector.x, vector.y, vector.z);
		}
	}
}
