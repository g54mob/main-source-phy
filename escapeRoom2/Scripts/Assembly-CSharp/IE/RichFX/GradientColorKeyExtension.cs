using UnityEngine;

namespace IE.RichFX
{
	public static class GradientColorKeyExtension
	{
		public static Vector4 ToVector(this GradientColorKey key)
		{
			Color linear = key.color.linear;
			return new Vector4(linear.r, linear.g, linear.b, key.time);
		}
	}
}
