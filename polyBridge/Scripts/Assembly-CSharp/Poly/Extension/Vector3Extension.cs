using UnityEngine;

namespace Poly.Extension
{
	public static class Vector3Extension
	{
		public static void Rotate90XY(this Vector3 v)
		{
			float x = v.x;
			v.x = 0f - v.y;
			v.y = x;
		}

		public static Vector3 Rotated90XY(this Vector3 v)
		{
			return new Vector3(0f - v.y, v.x, v.z);
		}

		public static float MaxCoordValue(this Vector3 v)
		{
			return Mathf.Max(Mathf.Max(v.x, v.y), v.z);
		}
	}
}
