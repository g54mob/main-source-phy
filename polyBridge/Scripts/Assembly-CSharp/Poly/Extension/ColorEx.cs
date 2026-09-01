using UnityEngine;

namespace Poly.Extension
{
	public static class ColorEx
	{
		private struct Col
		{
			public uint color;

			public static implicit operator Col(uint c)
			{
				return new Col
				{
					color = c
				};
			}

			public static implicit operator Color(Col c)
			{
				return FromHex(c.color);
			}
		}

		public static readonly Color pink = new Color32(248, 41, 217, byte.MaxValue);

		public static readonly Color lightGray = new Color32(191, 191, 191, byte.MaxValue);

		public static readonly Color gray = new Color32(127, 127, 127, byte.MaxValue);

		public static readonly Color darkGray = new Color32(63, 63, 63, byte.MaxValue);

		public static readonly Color roadGrad = new Color32(85, 68, 68, byte.MaxValue);

		public static readonly Color woodBrown = new Color32(204, 119, 34, byte.MaxValue);

		public static readonly Color steelRed = new Color32(204, 51, 51, byte.MaxValue);

		public static readonly Color hydraulicViolet = new Color32(153, 136, 187, byte.MaxValue);

		public static readonly Color ropeBrown = edgeGreen;

		public static readonly Color cableGray = new Color32(136, 153, 153, byte.MaxValue);

		public static readonly Color edgeGreen = new Color32(20, 227, 30, byte.MaxValue);

		public static readonly Color segmentYellow = new Color32(byte.MaxValue, 230, 26, byte.MaxValue);

		public static readonly Color fixedNodeRed = new Color32(167, 43, 43, byte.MaxValue);

		public static readonly Color dynamicNodeYellow = new Color32(byte.MaxValue, 235, 4, byte.MaxValue);

		public static readonly Color splitNodeGreen = new Color32(0, byte.MaxValue, 0, byte.MaxValue);

		public static readonly Color backgroundGray = new Color32(145, 145, 145, byte.MaxValue);

		public static readonly Color aeroBlue = new Color32(124, 185, 232, byte.MaxValue);

		public static readonly Color amber = new Color32(byte.MaxValue, 126, 0, byte.MaxValue);

		public static readonly Color orange = FromHex(15560724u);

		public static readonly Color orangeTiger = FromHex(16542211u);

		public static readonly Color orangeTangerine = FromHex(16417064u);

		public static readonly Color orangeMeriglod = FromHex(16559646u);

		public static readonly Color orangeHoney = FromHex(15505158u);

		public static readonly Color blueCobalt = FromHex(1259710u);

		public static readonly Color red = Color.red;

		public static readonly Color yellow = Color.yellow;

		public static readonly Color white = Color.white;

		public static readonly Color black = Color.black;

		public static readonly Color green = Color.green;

		public static readonly Color alphaHalf = new Color(1f, 1f, 1f, 0.5f);

		public static readonly Color[] retroMetroSet = new Color[9]
		{
			(Col)15357253u,
			(Col)16018075u,
			(Col)15702816u,
			(Col)15580979u,
			(Col)15589723u,
			(Col)12439346u,
			(Col)8895557u,
			(Col)2600687u,
			(Col)11746758u
		};

		public static readonly Color[] dutchFieldSet = new Color[9]
		{
			(Col)15073353u,
			(Col)767231u,
			(Col)5302673u,
			(Col)15128576u,
			(Col)10164725u,
			(Col)16753408u,
			(Col)14420660u,
			(Col)11785471u,
			(Col)49056u
		};

		public static Color FromHex(uint hex)
		{
			return new Color32((byte)(hex >> 16), (byte)(hex >> 8), (byte)hex, byte.MaxValue);
		}

		public static Color Tint(this Color c, Color tint)
		{
			Color result = default(Color);
			result.r = c.r * tint.r;
			result.g = c.g * tint.g;
			result.b = c.b * tint.b;
			result.a = c.a * tint.a;
			return result;
		}
	}
}
