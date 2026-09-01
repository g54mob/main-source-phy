using UnityEngine;

namespace Poly.Extension
{
	public static class Vector2Util
	{
		public static float Cross(Vector2 a, Vector2 b)
		{
			return a.x * b.y - a.y * b.x;
		}

		public static float CrossAbs(Vector2 a, Vector2 b)
		{
			return Mathf.Abs(a.x * b.y - a.y * b.x);
		}
	}
}
