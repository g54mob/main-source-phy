using System;
using UnityEngine;

namespace Poly.Draw
{
	public static class GizmosExtension
	{
		private static Vector3[] circlePoints;

		public static void DrawCircle(Vector3 position, float radius, float angle = 0f)
		{
			Vector3 vector = position + circlePoints[circlePoints.Length - 1] * radius;
			for (int i = 0; i < circlePoints.Length; i++)
			{
				Vector3 vector2 = position + circlePoints[i] * radius;
				Gizmos.DrawLine(vector, vector2);
				vector = vector2;
			}
			DrawCross(position, 2f * radius, angle);
		}

		public static void DrawCross(Vector3 position, float size, float angle = 0f)
		{
			Quaternion obj = ((angle == 0f) ? Quaternion.identity : Quaternion.AngleAxis(angle, Vector3.forward));
			Vector3 vector = obj * Vector3.right * size * 0.5f;
			Vector3 vector2 = obj * Vector3.up * size * 0.5f;
			Gizmos.DrawLine(position - vector, position + vector);
			Gizmos.DrawLine(position - vector2, position + vector2);
		}

		static GizmosExtension()
		{
			InitCirclePoints(24);
		}

		private static void InitCirclePoints(int numPoints)
		{
			circlePoints = new Vector3[numPoints];
			for (int i = 0; i < numPoints; i++)
			{
				float f = (float)i / (float)numPoints * 2f * MathF.PI;
				float y = Mathf.Sin(f);
				float x = Mathf.Cos(f);
				circlePoints[i] = new Vector3(x, y, 0f);
			}
		}
	}
}
