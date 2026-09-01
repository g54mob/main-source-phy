using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.Tools
{
	public static class MMColors
	{
		public enum ColoringMode
		{
			Tint = 0,
			Multiply = 1,
			Replace = 2,
			ReplaceKeepAlpha = 3,
			Add = 4
		}

		public static readonly Color ReunoYellow;

		public static readonly Color BestRed;

		public static readonly Color AliceBlue;

		public static readonly Color AntiqueWhite;

		public static readonly Color Aqua;

		public static readonly Color Aquamarine;

		public static readonly Color Azure;

		public static readonly Color Beige;

		public static readonly Color Bisque;

		public static readonly Color Black;

		public static readonly Color BlanchedAlmond;

		public static readonly Color Blue;

		public static readonly Color BlueViolet;

		public static readonly Color Brown;

		public static readonly Color Burlywood;

		public static readonly Color CadetBlue;

		public static readonly Color Chartreuse;

		public static readonly Color Chocolate;

		public static readonly Color Coral;

		public static readonly Color CornflowerBlue;

		public static readonly Color Cornsilk;

		public static readonly Color Crimson;

		public static readonly Color Cyan;

		public static readonly Color DarkBlue;

		public static readonly Color DarkCyan;

		public static readonly Color DarkGoldenrod;

		public static readonly Color DarkGray;

		public static readonly Color DarkGreen;

		public static readonly Color DarkKhaki;

		public static readonly Color DarkMagenta;

		public static readonly Color DarkOliveGreen;

		public static readonly Color DarkOrange;

		public static readonly Color DarkOrchid;

		public static readonly Color DarkRed;

		public static readonly Color DarkSalmon;

		public static readonly Color DarkSeaGreen;

		public static readonly Color DarkSlateBlue;

		public static readonly Color DarkSlateGray;

		public static readonly Color DarkTurquoise;

		public static readonly Color DarkViolet;

		public static readonly Color DeepPink;

		public static readonly Color DeepSkyBlue;

		public static readonly Color DimGray;

		public static readonly Color DodgerBlue;

		public static readonly Color FireBrick;

		public static readonly Color FloralWhite;

		public static readonly Color ForestGreen;

		public static readonly Color Fuchsia;

		public static readonly Color Gainsboro;

		public static readonly Color GhostWhite;

		public static readonly Color Gold;

		public static readonly Color Goldenrod;

		public static readonly Color Gray;

		public static readonly Color Green;

		public static readonly Color GreenYellow;

		public static readonly Color Honeydew;

		public static readonly Color HotPink;

		public static readonly Color IndianRed;

		public static readonly Color Indigo;

		public static readonly Color Ivory;

		public static readonly Color Khaki;

		public static readonly Color Lavender;

		public static readonly Color Lavenderblush;

		public static readonly Color LawnGreen;

		public static readonly Color LemonChiffon;

		public static readonly Color LightBlue;

		public static readonly Color LightCoral;

		public static readonly Color LightCyan;

		public static readonly Color LightGoldenodYellow;

		public static readonly Color LightGray;

		public static readonly Color LightGreen;

		public static readonly Color LightPink;

		public static readonly Color LightSalmon;

		public static readonly Color LightSeaGreen;

		public static readonly Color LightSkyBlue;

		public static readonly Color LightSlateGray;

		public static readonly Color LightSteelBlue;

		public static readonly Color LightYellow;

		public static readonly Color Lime;

		public static readonly Color LimeGreen;

		public static readonly Color Linen;

		public static readonly Color Magenta;

		public static readonly Color Maroon;

		public static readonly Color MediumAquamarine;

		public static readonly Color MediumBlue;

		public static readonly Color MediumOrchid;

		public static readonly Color MediumPurple;

		public static readonly Color MediumSeaGreen;

		public static readonly Color MediumSlateBlue;

		public static readonly Color MediumSpringGreen;

		public static readonly Color MediumTurquoise;

		public static readonly Color MediumVioletRed;

		public static readonly Color MidnightBlue;

		public static readonly Color Mintcream;

		public static readonly Color MistyRose;

		public static readonly Color Moccasin;

		public static readonly Color NavajoWhite;

		public static readonly Color Navy;

		public static readonly Color OldLace;

		public static readonly Color Olive;

		public static readonly Color Olivedrab;

		public static readonly Color Orange;

		public static readonly Color Orangered;

		public static readonly Color Orchid;

		public static readonly Color PaleGoldenrod;

		public static readonly Color PaleGreen;

		public static readonly Color PaleTurquoise;

		public static readonly Color PaleVioletred;

		public static readonly Color PapayaWhip;

		public static readonly Color PeachPuff;

		public static readonly Color Peru;

		public static readonly Color Pink;

		public static readonly Color Plum;

		public static readonly Color PowderBlue;

		public static readonly Color Purple;

		public static readonly Color Red;

		public static readonly Color RosyBrown;

		public static readonly Color RoyalBlue;

		public static readonly Color SaddleBrown;

		public static readonly Color Salmon;

		public static readonly Color SandyBrown;

		public static readonly Color SeaGreen;

		public static readonly Color Seashell;

		public static readonly Color Sienna;

		public static readonly Color Silver;

		public static readonly Color SkyBlue;

		public static readonly Color SlateBlue;

		public static readonly Color SlateGray;

		public static readonly Color Snow;

		public static readonly Color SpringGreen;

		public static readonly Color SteelBlue;

		public static readonly Color Tan;

		public static readonly Color Teal;

		public static readonly Color Thistle;

		public static readonly Color Tomato;

		public static readonly Color Turquoise;

		public static readonly Color Violet;

		public static readonly Color Wheat;

		public static readonly Color White;

		public static readonly Color WhiteSmoke;

		public static readonly Color Yellow;

		public static readonly Color YellowGreen;

		public static Dictionary<int, Color> ColorDictionary;

		public static Color RandomColor()
		{
			return default(Color);
		}

		public static Color GetColorAt(int index)
		{
			return default(Color);
		}

		public static void InitializeDictionary()
		{
		}

		public static Color MMRandomColor(this Color color, Color min, Color max)
		{
			return default(Color);
		}

		public static Gradient FlatGradient(Color32 color, float alpha = 1f)
		{
			return null;
		}

		public static Gradient SimpleGradient(Color32 startColor, Color32 endColor, float startAlpha = 1f, float endAlpha = 1f)
		{
			return null;
		}

		public static Color MMColorize(this Color originalColor, Color targetColor, ColoringMode coloringMode, float lerpAmount = 1f)
		{
			return default(Color);
		}
	}
}
