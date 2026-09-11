using UnityEngine;
using plog.Models;

namespace plog.unity.Extensions
{
	public static class UniversalColorExtensions
	{
		public static Color ToUnityColor(this UniversalColor color)
		{
			return new Color((float)(int)color.Red / 255f, (float)(int)color.Green / 255f, (float)(int)color.Blue / 255f);
		}
	}
}
