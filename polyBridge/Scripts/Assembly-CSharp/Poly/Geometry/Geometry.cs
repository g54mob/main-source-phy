using Poly.Extension;
using UnityEngine;

namespace Poly.Geometry
{
	public static class Geometry
	{
		public static float GetSignedAngle(Vector2 a, Vector2 b)
		{
			float num = Mathf.Atan2(a.y, a.x) * 57.29578f;
			float num2 = Mathf.Atan2(b.y, b.x) * 57.29578f - num;
			if (num2 <= -180f)
			{
				num2 += 360f;
			}
			else if (num2 > 180f)
			{
				num2 -= 360f;
			}
			return num2;
		}

		public static WindingDirection GetWindingDirection(Vector2[] verts)
		{
			if (verts.Length < 3)
			{
				return WindingDirection.CounterClockWise;
			}
			float num = 0f;
			Vector2 vector = verts[^1];
			Vector2 a = vector - verts[^2];
			foreach (Vector2 vector2 in verts)
			{
				Vector2 vector3 = vector2 - vector;
				num += GetSignedAngle(a, vector3);
				vector = vector2;
				a = vector3;
			}
			if (num.IsEqual(360f, 1f))
			{
				return WindingDirection.CounterClockWise;
			}
			if (num.IsEqual(-360f, 1f))
			{
				return WindingDirection.ClockWise;
			}
			Debug.Log("Winding broken");
			return WindingDirection.Invalid;
		}
	}
}
