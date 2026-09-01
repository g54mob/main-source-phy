using System.Collections.Generic;
using UnityEngine;

public static class DrawGuiTextUtil
{
	public static GUIStyle style;

	private static Dictionary<Color, Texture2D> colorToTexture = new Dictionary<Color, Texture2D>();

	public static void InitGuiStyle(Font font, Color backgroundColor, int fontSize = 24)
	{
		style = new GUIStyle();
		style.alignment = TextAnchor.MiddleCenter;
		style.fontSize = fontSize;
		style.fontStyle = FontStyle.Bold;
		style.font = font;
		style.normal.textColor = Color.white;
		style.normal.background = CreateOrGetBackgroundTexture(backgroundColor);
	}

	public static Texture2D CreateOrGetBackgroundTexture(Color backgroundColor)
	{
		if (!colorToTexture.TryGetValue(backgroundColor, out var value))
		{
			value = CreateBackgroundTexture(backgroundColor);
			colorToTexture.Add(backgroundColor, value);
		}
		return value;
	}

	public static Texture2D CreateBackgroundTexture(Color backgroundColor)
	{
		Texture2D texture2D = new Texture2D(1, 1);
		texture2D.SetPixel(0, 0, backgroundColor);
		texture2D.filterMode = FilterMode.Point;
		texture2D.Apply();
		return texture2D;
	}

	public static void DisplayGuiLabel_Slow(Vector3 posInWorld, string text, float desiredLabelWidthInMeters = 0f)
	{
		text = " " + text + " ";
		Camera camera = null;
		camera = Cameras.MainCamera();
		if ((object)camera == null)
		{
			camera = Camera.main;
		}
		Vector2 center = camera.WorldToScreenPoint(posInWorld);
		center.y = (float)Screen.height - center.y;
		GUIContent content = new GUIContent(text);
		Rect position = new Rect
		{
			size = style.CalcSize(content),
			center = center
		};
		if (0f < desiredLabelWidthInMeters)
		{
			float num = 0.5f * (float)Screen.width / (camera.orthographicSize * camera.aspect);
			int num2 = Mathf.FloorToInt(desiredLabelWidthInMeters * num);
			int fontSize = style.fontSize;
			style.fontSize = fontSize * num2 / Mathf.CeilToInt(position.size.x);
			position.size = style.CalcSize(content);
			position.center = center;
			GUI.Label(position, content, style);
			style.fontSize = fontSize;
		}
		else
		{
			GUI.Label(position, content, style);
		}
	}
}
