using System.Collections.Generic;
using UnityEngine;
using plog.Helpers;
using plog.Models;

namespace plog.unity.Helpers
{
	public static class UnityColorHelper
	{
		private static readonly Dictionary<UniversalColor, (string, string)> ColorCache = new Dictionary<UniversalColor, (string, string)>();

		public static (string, string) GetHtmlColorPair(UniversalColor color)
		{
			if (ColorCache.TryGetValue(color, out (string, string) value))
			{
				return value;
			}
			UniversalColor color2 = color.AdjustVibrance(5f).Lerp(Colors.Black, 0.3f).Desaturate(0.4f)
				.AdjustVibrance(0.45f);
			UniversalColor color3 = color.AdjustVibrance(3f).Desaturate(0.8f);
			value = (ResolveHtmlColor(color2), ResolveHtmlColor(color3));
			ColorCache[color] = value;
			return value;
		}

		public static string ResolveHtmlColor(UniversalColor color)
		{
			return ColorUtility.ToHtmlStringRGB(new Color((float)(int)color.Red / 255f, (float)(int)color.Green / 255f, (float)(int)color.Blue / 255f));
		}
	}
}
