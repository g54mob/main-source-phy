using UnityEngine;

namespace LevelCreator
{
	public class DMEditorColors
	{
		public enum ColorState
		{
			Normal = 0,
			Highlighted = 1,
			HighlightedTransparent = 2,
			Muted = 3,
			NormalTransparent = 4
		}

		public static Color NormalColor = new Color(1f, 1f, 1f, 1f);

		public static Color NormalTransparentColor = new Color(1f, 1f, 1f, 0f);

		public static Color DarkNormalColor = new Color(0.22f, 0.22f, 0.22f, 1f);

		public static Color HighlightColor = new Color(1f, 0.87f, 0f, 1f);

		public static Color HighlightTransparentColor = new Color(1f, 0.87f, 0f, 0f);

		public static Color MutedColor = new Color(0.87f, 0.87f, 0.87f, 1f);

		public static Color GetColor(ColorState color)
		{
			switch (color)
			{
			case ColorState.Normal:
				return NormalColor;
			case ColorState.NormalTransparent:
				return NormalTransparentColor;
			case ColorState.Highlighted:
				return HighlightColor;
			case ColorState.HighlightedTransparent:
				return HighlightTransparentColor;
			case ColorState.Muted:
				return MutedColor;
			default:
				return Color.white;
			}
		}
	}
}
