using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.SimpleColorPicker.Scripts
{
	public class ColorPicker : MonoBehaviour
	{
		public Color Color;

		public ColorMode ColorMode;

		public ColorJoystick ColorJoystick;

		public Image Gradient;

		public RectTransform RectTransform;

		public Slider Hue;

		public ColorSlider R;

		public ColorSlider G;

		public ColorSlider B;

		public ColorSlider H;

		public ColorSlider S;

		public ColorSlider V;

		public ColorSlider A;

		public TMP_InputField Hex;

		public Button m_HexInputFieldGamepadButton;

		public Image[] CompareLook;

		public Image TransparencyLook;

		public Text Mode;

		public GameObject RgbSliders;

		public GameObject HsvSliders;

		public bool Locked;

		[HideInInspector]
		private Texture2D _Texture;

		public Texture2D Texture
		{
			get
			{
				if (_Texture == null)
				{
					_Texture = new Texture2D(128, 128)
					{
						filterMode = FilterMode.Point
					};
				}
				return _Texture;
			}
		}

		public void Awake()
		{
			Gradient.sprite = Sprite.Create(Texture, new Rect(0f, 0f, Texture.width, Texture.height), new Vector2(0.5f, 0.5f), 100f);
			SetColor(Color);
			CompareLook[0].color = Color;
			Hex.onEndEdit.AddListener(delegate
			{
				OnEndHexEdit();
			});
			m_HexInputFieldGamepadButton.onClick.AddListener(OnHexInputFieldGamepadButton);
		}

		public void OnEnable()
		{
			Hex.ActivateInputField();
		}

		public void UpdateForCurrentDevice()
		{
			Hex.interactable = !GamepadVirtualKeyboard.IsSupported();
			m_HexInputFieldGamepadButton.gameObject.SetActive(GamepadVirtualKeyboard.IsSupported());
		}

		public bool InputFieldHasFocus()
		{
			if (Hex.gameObject.activeInHierarchy)
			{
				return Hex.isFocused;
			}
			return false;
		}

		private void OnEndHexEdit()
		{
			if (ColorUtility.TryParseHtmlString("#" + Hex.text, out var color))
			{
				SetColor(color, picker: true, sliders: true, hex: false);
				CustomShape selectedCustomShape = SandboxSelectionSet.GetSelectedCustomShape();
				if ((bool)selectedCustomShape && !selectedCustomShape.m_Color.Equals(color))
				{
					GameUI.m_Instance.m_SandboxEditCustomShape.SetShapeColor(color);
					SandboxUndo.SnapShot();
				}
			}
			else
			{
				CustomShape selectedCustomShape2 = SandboxSelectionSet.GetSelectedCustomShape();
				if ((bool)selectedCustomShape2)
				{
					Hex.text = ColorUtility.ToHtmlStringRGBA(selectedCustomShape2.m_Color).Substring(0, 6);
				}
			}
		}

		public void Update()
		{
		}

		public void Select()
		{
			CompareLook[0].color = Color;
			Debug.LogFormat("Color selected: {0}", Color);
		}

		public void Review()
		{
			Application.OpenURL("https://www.assetstore.unity3d.com/#!/content/120033");
		}

		public void SetColor(Color color, bool picker = true, bool sliders = true, bool hex = true, bool hue = true)
		{
			Color.RGBToHSV(color, out var H, out var S, out var V);
			SetColor((S > 0f) ? H : this.H.Value, S, V, color.a, picker, sliders, hex, hue);
		}

		public void SetColor(float h, float s, float v, float a, bool picker = true, bool sliders = true, bool hex = true, bool hue = true)
		{
			Color color = Color.HSVToRGB(h, s, v);
			color.a = a;
			Image transparencyLook = TransparencyLook;
			Color color2 = (CompareLook[1].color = color);
			Color color4 = (transparencyLook.color = color2);
			Color = color4;
			ColorJoystick.Center.color = new Color(Color.r, Color.g, Color.b);
			Locked = true;
			if (sliders || ColorMode == ColorMode.Hsv)
			{
				R.Set(Color.r);
				G.Set(Color.g);
				B.Set(Color.b);
			}
			if (sliders || ColorMode == ColorMode.Rgb)
			{
				H.Set(h);
				S.Set(s);
				V.Set(v);
			}
			A.Set(Color.a);
			if (hue)
			{
				Hue.value = h;
			}
			if (hex)
			{
				Hex.text = ColorUtility.ToHtmlStringRGBA(Color).Substring(0, 6);
			}
			if (picker)
			{
				ColorJoystick.transform.localPosition = new Vector2(s * (float)Texture.width / (float)Texture.width * RectTransform.rect.width, v * (float)Texture.height / (float)Texture.height * RectTransform.rect.height);
			}
			Locked = false;
			UpdateGradient();
		}

		public void OnHueShanged(float value)
		{
			if (!Locked)
			{
				Color.RGBToHSV(Color, out var H, out var S, out var V);
				H = value;
				SetColor(H, S, V, A.Value, picker: true, sliders: true, hex: true, hue: false);
			}
		}

		public void OnSliderChanged()
		{
			if (!Locked)
			{
				if (ColorMode == ColorMode.Rgb)
				{
					SetColor(new Color(R.Value, G.Value, B.Value, A.Value), picker: true, sliders: false);
				}
				else
				{
					SetColor(H.Value, S.Value, V.Value, A.Value, picker: true, sliders: false);
				}
			}
		}

		public void OnHexValueChanged(string value)
		{
			if (!Locked)
			{
				value = Regex.Replace(value.ToUpper(), "[^0-9A-F]", "");
				Hex.text = value;
				if (ColorUtility.TryParseHtmlString("#" + value, out var color))
				{
					SetColor(color, picker: true, sliders: true, hex: false);
				}
			}
		}

		public void SwitchMode()
		{
			ColorMode = ((ColorMode == ColorMode.Rgb) ? ColorMode.Hsv : ColorMode.Rgb);
			SetMode(ColorMode);
		}

		public void SetMode(ColorMode mode)
		{
			RgbSliders.SetActive(mode == ColorMode.Rgb);
			HsvSliders.SetActive(mode == ColorMode.Hsv);
			Mode.text = ((mode == ColorMode.Rgb) ? "HSV" : "RGB");
		}

		private void UpdateGradient()
		{
			List<Color> list = new List<Color>();
			for (int i = 0; i < Texture.height; i++)
			{
				for (int j = 0; j < Texture.width; j++)
				{
					list.Add(Color.HSVToRGB(Hue.value, (float)j / (float)Texture.width, (float)i / (float)Texture.height));
				}
			}
			Texture.SetPixels(list.ToArray());
			Texture.Apply();
		}

		private void OnHexInputFieldGamepadButton()
		{
			GamepadVirtualKeyboard.MaybeOpenVirtualKeyboard(Hex.text, Hex.characterLimit, string.Empty, multiline: false, OnHexEntered);
		}

		private void OnHexEntered(string text)
		{
			if (text != null)
			{
				Hex.text = text;
				OnEndHexEdit();
			}
		}
	}
}
