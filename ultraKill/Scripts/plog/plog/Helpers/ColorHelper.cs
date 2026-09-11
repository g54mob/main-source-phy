using System.Collections.Generic;
using plog.Models;

namespace plog.Helpers
{
	public static class ColorHelper
	{
		private static readonly Dictionary<UniversalColor, (UniversalColor, UniversalColor)> ColorCache = new Dictionary<UniversalColor, (UniversalColor, UniversalColor)>();

		public static UniversalColor GetColorForHash(int hash)
		{
			return new UniversalColor((hash & 0xFF0000) >> 16, (hash & 0xFF00) >> 8, hash & 0xFF);
		}

		public static UniversalColor Desaturate(this UniversalColor color, float amount = 0.15f)
		{
			color = color.Lerp(Colors.White, amount);
			return color;
		}

		public static UniversalColor AdjustVibrance(this UniversalColor color, float amount = 1f)
		{
			float luminance = 0.299f * (float)(int)color.Red + 0.587f * (float)(int)color.Green + 0.114f * (float)(int)color.Blue;
			color = color.CopyWith(IncreaseChannelVibrance(color.Red, luminance, amount), IncreaseChannelVibrance(color.Green, luminance, amount), IncreaseChannelVibrance(color.Blue, luminance, amount));
			return color;
		}

		private static byte IncreaseChannelVibrance(byte channel, float luminance, float amount)
		{
			float num = ((float)(int)channel - luminance) * (1f + amount);
			float num2 = luminance + num;
			if (!(num2 < 0f))
			{
				if (num2 > 255f)
				{
					return byte.MaxValue;
				}
				return (byte)num2;
			}
			return 0;
		}

		public static UniversalColor Lerp(this UniversalColor color, UniversalColor target, float amount)
		{
			return new UniversalColor((byte)((double)(int)color.Red + (double)(target.Red - color.Red) * (double)amount), (byte)((double)(int)color.Green + (double)(target.Green - color.Green) * (double)amount), (byte)((double)(int)color.Blue + (double)(target.Blue - color.Blue) * (double)amount));
		}

		public static byte Lerp(this byte value, byte target, float amount)
		{
			return (byte)((double)(int)value + (double)(target - value) * (double)amount);
		}

		public static (UniversalColor, UniversalColor) GetColorPair(UniversalColor color)
		{
			if (ColorCache.TryGetValue(color, out var value))
			{
				return value;
			}
			UniversalColor item = color.AdjustVibrance(5f).Lerp(Colors.Black, 0.3f).Desaturate(0.4f)
				.AdjustVibrance(0.45f);
			UniversalColor item2 = color.AdjustVibrance(3f).Desaturate(0.8f);
			value = (item, item2);
			ColorCache[color] = value;
			return value;
		}
	}
}
