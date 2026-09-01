using UnityEngine;

namespace Poly.Extension
{
	public static class FloatExtension
	{
		public static bool IsEqual(this float v0, float v1, float precision = 1E-06f)
		{
			return Mathf.Abs(v1 - v0) <= precision;
		}

		public static bool IsEqual(this in Vec2 v0, in Vec2 v1, float precision = 1E-06f)
		{
			if (v0.x.IsEqual(v1.x, precision))
			{
				return v0.y.IsEqual(v1.y, precision);
			}
			return false;
		}
	}
}
