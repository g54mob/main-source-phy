using System;
using System.Collections.Generic;
using UnityEngine;

public static class Maths
{
	public static float Exp2(float x)
	{
		return Mathf.Exp(x * 0.6931472f);
	}

	public static bool isPolygonSelfIntersecting(List<Vector2> points)
	{
		int count = points.Count;
		HashSet<Vector2> hashSet = new HashSet<Vector2>();
		foreach (Vector2 point in points)
		{
			if (!hashSet.Add(point))
			{
				return true;
			}
		}
		for (int i = 0; i < count; i++)
		{
			Vector2 vector = points[i];
			Vector2 vector2 = points[(i + 1) % count];
			Vector2 vector3 = points[(i + count - 1) % count];
			Vector2 normalized = (vector2 - vector).normalized;
			Vector2 normalized2 = (vector3 - vector).normalized;
			if (Mathf.Approximately(Vector2.Dot(normalized, normalized2), 1f))
			{
				return true;
			}
		}
		for (int j = 0; j < count; j++)
		{
			Vector2 a = points[j];
			Vector2 b = points[(j + 1) % count];
			for (int k = j + 1; k < count; k++)
			{
				if (k != j && (k + 1) % count != j && (j + 1) % count != k)
				{
					Vector2 c = points[k];
					Vector2 d = points[(k + 1) % count];
					if (!(getIntersectionPoint(a, b, c, d) == Vector2.zero))
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	public static Vector2 getIntersectionPoint(Vector2 a, Vector2 b, Vector2 c, Vector2 d)
	{
		float num = (b.x - a.x) * (d.y - c.y) - (b.y - a.y) * (d.x - c.x);
		if (Mathf.Approximately(num, 0f))
		{
			return Vector2.zero;
		}
		float num2 = (a.y - c.y) * (d.x - c.x) - (a.x - c.x) * (d.y - c.y);
		float num3 = (a.y - c.y) * (b.x - a.x) - (a.x - c.x) * (b.y - a.y);
		float num4 = num2 / num;
		float num5 = num3 / num;
		if (num4 < 0f || num4 > 1f || num5 < 0f || num5 > 1f)
		{
			return Vector2.zero;
		}
		float x = a.x + num4 * (b.x - a.x);
		float y = a.y + num4 * (b.y - a.y);
		return new Vector2(x, y);
	}

	public static bool isPolygonClockwise(List<Vector2> points)
	{
		return getPolygonSignedArea(points) > 0f;
	}

	public static float getPolygonSignedArea(List<Vector2> points)
	{
		float num = 0f;
		int count = points.Count;
		for (int i = 0; i < count; i++)
		{
			Vector2 vector = points[i];
			Vector2 vector2 = points[(i + 1) % count];
			num += (vector2.x - vector.x) * (vector2.y + vector.y);
		}
		return num;
	}

	public static List<int> triangulatePolygon(List<Vector2> points)
	{
		List<int> list = new List<int>();
		if (points == null)
		{
			Debug.LogError("Cannot triangulate null points.");
			return list;
		}
		if (points.Count < 3)
		{
			Debug.LogError("Cannot triangulate polygon with less than 3 points.");
			return list;
		}
		List<int> list2 = new List<int>();
		int count = points.Count;
		if (isPolygonClockwise(points))
		{
			for (int i = 0; i < count; i++)
			{
				list2.Add(i);
			}
		}
		else
		{
			for (int j = 0; j < count; j++)
			{
				list2.Add(count - 1 - j);
			}
		}
		int num = 0;
		while (list2.Count > 3 && num++ < 1000)
		{
			for (int k = 0; k < list2.Count - 1; k++)
			{
				int num2 = list2[k];
				int num3;
				if (k != 0)
				{
					num3 = list2[k - 1];
				}
				else
				{
					num3 = list2[list2.Count - 1];
				}
				int num4 = num3;
				int num5 = ((k == list2.Count - 1) ? list2[0] : list2[k + 1]);
				Vector2 vector = points[num2];
				Vector2 vector2 = points[num4];
				Vector2 vector3 = points[num5];
				if (crossProduct(vector2 - vector, vector3 - vector) < 0f)
				{
					continue;
				}
				bool flag = true;
				for (int l = 0; l < count; l++)
				{
					if (l != num2 && l != num4 && l != num5 && isPointInsideTriangle(points[l], vector, vector2, vector3))
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					list.Add(num4);
					list.Add(num2);
					list.Add(num5);
					list2.RemoveAt(k);
					break;
				}
			}
		}
		if (num >= 1000)
		{
			Debug.LogError($"Triangulation failed, too many iterations (exceeded max iterations of {1000}).");
			return new List<int>();
		}
		list.Add(list2[0]);
		list.Add(list2[1]);
		list.Add(list2[2]);
		return list;
	}

	public static bool isPointInsideTriangle(Vector2 p, Vector2 a, Vector2 b, Vector2 c)
	{
		float num = (a.x - c.x) * (p.y - c.y) - (a.y - c.y) * (p.x - c.x);
		float num2 = (b.x - a.x) * (p.y - a.y) - (b.y - a.y) * (p.x - a.x);
		if (num < 0f != num2 < 0f && num != 0f && num2 != 0f)
		{
			return false;
		}
		float num3 = (c.x - b.x) * (p.y - b.y) - (c.y - b.y) * (p.x - b.x);
		if (num3 != 0f)
		{
			return num3 < 0f == num + num2 <= 0f;
		}
		return true;
	}

	public static float crossProduct(Vector2 a, Vector2 b)
	{
		return a.x * b.y - a.y * b.x;
	}

	public static Vector2 rotate(Vector2 vector2, float degrees)
	{
		float f = degrees * (MathF.PI / 180f);
		float num = Mathf.Cos(f);
		float num2 = Mathf.Sin(f);
		float x = vector2.x * num - vector2.y * num2;
		float y = vector2.x * num2 + vector2.y * num;
		return new Vector2(x, y);
	}

	public static Matrix4x4 lerpMatrix(Matrix4x4 from, Matrix4x4 to, float t)
	{
		Matrix4x4 result = default(Matrix4x4);
		for (int i = 0; i < 16; i++)
		{
			result[i] = Mathf.Lerp(from[i], to[i], t);
		}
		return result;
	}

	public static float distanceFromPointToSegment(Vector3 p, Vector3 a, Vector3 b)
	{
		Vector3 vector = b - a;
		Vector3 lhs = p - a;
		float sqrMagnitude = vector.sqrMagnitude;
		if (sqrMagnitude == 0f)
		{
			return (p - a).magnitude;
		}
		float value = Vector3.Dot(lhs, vector) / sqrMagnitude;
		value = Mathf.Clamp01(value);
		Vector3 b2 = a + value * vector;
		return Vector3.Distance(p, b2);
	}

	public static Vector3 closestPointOnLineSegment(Vector3 point, Vector3 a, Vector3 b, out float t)
	{
		t = 0f;
		if (a == b)
		{
			return a;
		}
		Vector3 vector = b - a;
		Vector3 normalized = vector.normalized;
		float num = Vector3.Dot(point - a, normalized);
		float magnitude = vector.magnitude;
		t = num / magnitude;
		return a + Mathf.Clamp01(t) * magnitude * normalized;
	}

	public static Vector3 closestPointOnLine(Vector3 point, Vector3 lineOrigin, Vector3 lineDirection)
	{
		if (lineDirection == Vector3.zero)
		{
			return lineOrigin;
		}
		Vector3 vector = Vector3.Project(point - lineOrigin, lineDirection);
		return lineOrigin + vector;
	}

	public static float distanceFromPointToLine(Vector3 point, Vector3 lineOrigin, Vector3 lineDirection)
	{
		Vector3 vector = Vector3.Project(point - lineOrigin, lineDirection);
		Vector3 b = lineOrigin + vector;
		return Vector3.Distance(point, b);
	}

	public static float vectorProjectionPercent(Vector3 projected, Vector3 onVector)
	{
		return Vector3.Dot(projected, onVector) / onVector.sqrMagnitude;
	}

	public static Vector3 quadraticBezier(Vector3 p0, Vector3 p1, Vector3 p2, float t)
	{
		return (1f - t) * (1f - t) * p0 + 2f * (1f - t) * t * p1 + t * t * p2;
	}

	public static Vector3 quadraticBezierDerivative(Vector3 p0, Vector3 p1, Vector3 p2, float t)
	{
		return 2f * (1f - t) * (p1 - p0) + 2f * t * (p2 - p1);
	}

	public static Vector3 cubicBezier(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
	{
		float num = 1f - t;
		Vector3 vector = num * num * num * p0;
		Vector3 vector2 = 3f * num * num * t * p1;
		Vector3 vector3 = 3f * num * t * t * p2;
		Vector3 vector4 = t * t * t * p3;
		return vector + vector2 + vector3 + vector4;
	}

	public static Vector3 cubicBezierDerivative(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
	{
		float num = 1f - t;
		Vector3 vector = 3f * num * num * (p1 - p0);
		Vector3 vector2 = 6f * num * t * (p2 - p1);
		Vector3 vector3 = 3f * t * t * (p3 - p2);
		return vector + vector2 + vector3;
	}

	public static float closestCubicBezierPercent(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, Ray ray)
	{
		return closestCubicBezierPercent(p0, p1, p2, p3, (Vector3 curvePoint) => distanceFromPointToLine(curvePoint, ray.origin, ray.direction));
	}

	public static float closestCubicBezierPercent(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, Vector3 point)
	{
		return closestCubicBezierPercent(p0, p1, p2, p3, (Vector3 curvePoint) => (curvePoint - point).sqrMagnitude);
	}

	private static float closestCubicBezierPercent(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, Func<Vector3, float> distanceCalculator)
	{
		float num = float.PositiveInfinity;
		float num2 = 0f;
		for (float num3 = 0f; num3 <= 1f; num3 += 0.01f)
		{
			Vector3 arg = cubicBezier(p0, p1, p2, p3, num3);
			float num4 = distanceCalculator(arg);
			if (!(num4 >= num))
			{
				num = num4;
				num2 = num3;
			}
		}
		float num5 = Mathf.Clamp01(num2 - 0.01f);
		float num6 = Mathf.Clamp01(num2 + 0.01f);
		float num7 = 0f;
		while (num6 - num5 > 1E-06f)
		{
			num7 = (num5 + num6) / 2f;
			float t = (num5 + num7) / 2f;
			Vector3 arg2 = cubicBezier(p0, p1, p2, p3, t);
			float num8 = distanceCalculator(arg2);
			float t2 = (num6 + num7) / 2f;
			Vector3 arg3 = cubicBezier(p0, p1, p2, p3, t2);
			float num9 = distanceCalculator(arg3);
			if (num8 < num9)
			{
				num6 = num7;
			}
			else
			{
				num5 = num7;
			}
		}
		return num7;
	}
}
