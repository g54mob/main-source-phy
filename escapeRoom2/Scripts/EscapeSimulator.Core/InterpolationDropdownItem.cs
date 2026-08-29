using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterpolationDropdownItem : MonoBehaviour
{
	private static readonly Dictionary<Interpolation, Texture2D> curveTextures = new Dictionary<Interpolation, Texture2D>();

	public GameObject popup;

	public RawImage curveTexture;

	public Transform topPoint;

	public Transform botPoint;

	public Transform point;

	private int innerCurveTexturePadding;

	private Interpolation interpolation;

	private float percent;

	public void show()
	{
		if (!(popup == null))
		{
			Dropdown componentInParent = GetComponentInParent<Dropdown>();
			string text = GetComponentInChildren<Text>().text;
			int num = componentInParent.options.FindIndex((Dropdown.OptionData x) => x.text == text);
			interpolation = (Interpolation)num;
			popup.SetActive(value: true);
			percent = 0f;
			innerCurveTexturePadding = (int)((popup.transform as RectTransform).sizeDelta.x / 4f);
			(botPoint as RectTransform).anchoredPosition = new Vector2(-innerCurveTexturePadding, -innerCurveTexturePadding);
			(topPoint as RectTransform).anchoredPosition = new Vector2(innerCurveTexturePadding, innerCurveTexturePadding);
			syncVisual();
		}
	}

	public void hide()
	{
		if (!(popup == null))
		{
			popup.SetActive(value: false);
		}
	}

	public void Update()
	{
		if (!(popup == null))
		{
			syncVisual();
			percent += Time.deltaTime;
			if (percent > 1f)
			{
				percent = 1f;
			}
		}
	}

	private void syncVisual()
	{
		if (!curveTextures.TryGetValue(interpolation, out var value) || value == null)
		{
			innerCurveTexturePadding = (int)((popup.transform as RectTransform).sizeDelta.x / 4f);
			value = generateCurveTexture(interpolation, innerCurveTexturePadding);
			curveTextures[interpolation] = value;
		}
		curveTexture.texture = value;
		float x = Mathf.LerpUnclamped(botPoint.transform.position.x, topPoint.transform.position.x, percent);
		float y = Mathf.LerpUnclamped(botPoint.transform.position.y, topPoint.transform.position.y, interpolation.evaluate(percent));
		point.position = new Vector3(x, y, point.position.z);
	}

	private static Texture2D generateCurveTexture(Interpolation interpolation, int padding)
	{
		int num = padding * 4;
		int num2 = padding * 4;
		Texture2D texture2D = new Texture2D(num, num2, TextureFormat.RGBA32, mipChain: true)
		{
			filterMode = FilterMode.Trilinear
		};
		Color32[] pixels = new Color32[num * num2];
		texture2D.SetPixels32(pixels);
		int y = texture2D.height - padding;
		Color white = Color.white;
		float? a = 0.1f;
		drawLine(texture2D, padding, padding, padding, y, white.With(null, null, null, a));
		int x = texture2D.width - padding;
		Color white2 = Color.white;
		a = 0.1f;
		drawLine(texture2D, padding, padding, x, padding, white2.With(null, null, null, a));
		num -= padding * 2;
		num2 -= padding * 2;
		for (int i = 0; i < num - 1; i++)
		{
			float t = (float)i / (float)(num - 1);
			int num3 = i;
			int num4 = Mathf.RoundToInt(interpolation.evaluate(t) * (float)(num2 - 1));
			float t2 = (float)(i + 1) / (float)(num - 1);
			int num5 = i + 1;
			int num6 = Mathf.RoundToInt(interpolation.evaluate(t2) * (float)(num2 - 1));
			Color white3 = Color.white;
			Color deepSkyBlue = Color.deepSkyBlue;
			drawLine(texture2D, num3 + padding, num4 + padding, num5 + padding, num6 + padding, Color.Lerp(white3, deepSkyBlue, t));
		}
		texture2D.Apply();
		return texture2D;
		static void drawLine(Texture2D tex, int num9, int num12, int num8, int num11, Color color)
		{
			int num7 = Mathf.Abs(num8 - num9);
			int num10 = Mathf.Abs(num11 - num12);
			int num13 = ((num9 < num8) ? 1 : (-1));
			int num14 = ((num12 < num11) ? 1 : (-1));
			int num15 = num7 - num10;
			while (true)
			{
				tex.SetPixel(num9, num12, color);
				if (num9 == num8 && num12 == num11)
				{
					break;
				}
				int num16 = 2 * num15;
				if (num16 > -num10)
				{
					num15 -= num10;
					num9 += num13;
				}
				if (num16 < num7)
				{
					num15 += num7;
					num12 += num14;
				}
			}
		}
	}
}
