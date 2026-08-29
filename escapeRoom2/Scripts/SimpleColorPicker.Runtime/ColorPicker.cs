using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

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

	public InputField Hex;

	public Image TransparencyLook;

	public Text Mode;

	public GameObject RgbSliders;

	public GameObject HsvSliders;

	public bool Locked;

	[HideInInspector]
	public Texture2D Texture;

	public void init()
	{
		if (Texture == null)
		{
			Texture = new Texture2D(128, 128)
			{
				filterMode = FilterMode.Point
			};
			Gradient.sprite = Sprite.Create(Texture, new Rect(0f, 0f, Texture.width, Texture.height), new Vector2(0.5f, 0.5f), 100f);
			SetColor(Color);
		}
	}

	public void Start()
	{
		init();
	}

	public void SetColor(Color color, bool picker = true, bool sliders = true, bool hex = true, bool hue = true)
	{
		init();
		Color.RGBToHSV(color, out var H, out var S, out var V);
		SetColor((S > 0f) ? H : this.H.Value, S, V, A.Value, picker, sliders, hex, hue);
	}

	public void SetColor(float h, float s, float v, float a, bool picker = true, bool sliders = true, bool hex = true, bool hue = true)
	{
		init();
		Color color = Color.HSVToRGB(h, s, v);
		color.a = a;
		Color color2 = (TransparencyLook.color = color);
		Color = color2;
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
			Hex.text = ColorUtility.ToHtmlStringRGBA(Color);
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
		init();
		ColorMode = ((ColorMode == ColorMode.Rgb) ? ColorMode.Hsv : ColorMode.Rgb);
		SetMode(ColorMode);
	}

	public void SetMode(ColorMode mode)
	{
		init();
		RgbSliders.SetActive(mode == ColorMode.Rgb);
		HsvSliders.SetActive(mode == ColorMode.Hsv);
		Mode.text = ((mode == ColorMode.Rgb) ? "HSV" : "RGB");
	}

	private void UpdateGradient()
	{
		init();
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
}
