using System;
using System.Runtime.InteropServices;

namespace ch.sycoforge.Types
{
	[Serializable]
	[ComVisible(true)]
	public struct ColorGradient
	{
		public GradientKey[] ColorKeys;

		public GradientKey[] AlphaKeys;

		public int colorKeyCount;

		public int alphaKeyCount;

		public ColorGradient(int colorKeyCount, int alphaKeyCount)
		{
			ColorKeys = new GradientKey[colorKeyCount];
			this.colorKeyCount = colorKeyCount;
			AlphaKeys = new GradientKey[alphaKeyCount];
			this.alphaKeyCount = alphaKeyCount;
		}

		public Float4 SampleColor(float position)
		{
			Float4 result = Float4.zero;
			if (ColorKeys.Length >= 2)
			{
				if (position < ColorKeys[0].Position)
				{
					result = ColorKeys[0].Color;
				}
				else if (position > ColorKeys[ColorKeys.Length - 1].Position)
				{
					result = ColorKeys[ColorKeys.Length - 1].Color;
				}
				else
				{
					result = Sample(position, ColorKeys);
					result.w = Sample(position, AlphaKeys).w;
				}
			}
			return result;
		}

		private Float4 Sample(float position, GradientKey[] keys)
		{
			Float4 result = Float4.zero;
			if (keys != null)
			{
				Array.Sort(keys, (GradientKey x, GradientKey y) => x.Position.CompareTo(y.Position));
				GradientKey gradientKey = default(GradientKey);
				GradientKey gradientKey2 = default(GradientKey);
				for (int num = 0; num < keys.Length - 1; num++)
				{
					gradientKey = keys[num];
					gradientKey2 = keys[num + 1];
					if (position > gradientKey.Position && position < gradientKey2.Position)
					{
						break;
					}
				}
				float position2 = gradientKey.Position;
				float num2 = gradientKey2.Position - position2;
				float num3 = 1f / num2;
				float num4 = (position - position2) * num3;
				result = gradientKey2.Color * num4 + gradientKey.Color * (1f - num4);
			}
			return result;
		}

		public void Sort()
		{
			Array.Sort(ColorKeys, (GradientKey x, GradientKey y) => x.Position.CompareTo(y.Position));
			Array.Sort(AlphaKeys, (GradientKey x, GradientKey y) => x.Position.CompareTo(y.Position));
			colorKeyCount = ColorKeys.Length;
			alphaKeyCount = AlphaKeys.Length;
		}

		public static ColorGradient GetDefault()
		{
			ColorGradient result = new ColorGradient
			{
				colorKeyCount = 2,
				alphaKeyCount = 2,
				ColorKeys = new GradientKey[2],
				AlphaKeys = new GradientKey[2]
			};
			result.ColorKeys[0] = new GradientKey(new Float4(1f, 0.5f, 0f), 0f);
			result.ColorKeys[1] = new GradientKey(new Float4(1f, 1f, 1f), 0.51f);
			result.AlphaKeys[0] = new GradientKey(new Float4(1f, 1f, 1f, 1f), 0f);
			result.AlphaKeys[1] = new GradientKey(new Float4(1f, 1f, 1f, 1f), 1f);
			return result;
		}
	}
}
