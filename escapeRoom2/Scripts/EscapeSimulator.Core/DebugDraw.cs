using System;
using UnityEngine;

public static class DebugDraw
{
	public static void ray(Ray ray, Color color, float duration = 0f)
	{
		line(ray.origin, ray.origin + ray.direction * 100f, color, duration);
	}

	public static void cross(Vector3 center, float size, Color color, float duration = 0f)
	{
		float num = size * 0.5f;
		line(center + Vector3.down * num, center + Vector3.up * num, color, duration);
		line(center + Vector3.left * num, center + Vector3.right * num, color, duration);
		line(center + Vector3.back * num, center + Vector3.forward * num, color, duration);
	}

	public static void circle(Vector3 center, Vector3 normal, float radius, Color color, float duration = 0f, int subdivisions = 36)
	{
		Quaternion quaternion = Quaternion.FromToRotation(Vector3.forward, normal);
		for (int i = 0; i < subdivisions; i++)
		{
			float f = MathF.PI * 2f * (float)i / (float)subdivisions;
			float f2 = MathF.PI * 2f * (float)(i + 1) / (float)subdivisions;
			Vector3 vector = quaternion * new Vector3(Mathf.Cos(f), Mathf.Sin(f));
			Vector3 vector2 = quaternion * new Vector3(Mathf.Cos(f2), Mathf.Sin(f2));
			line(center + radius * vector, center + radius * vector2, color, duration);
		}
	}

	public static void bounds(Bounds bounds, Color color, float duration = 0f)
	{
		Vector3 min = bounds.min;
		Vector3 max = bounds.max;
		edge(x1: false, y1: false, z1: false, x2: true, y2: false, z2: false);
		edge(x1: false, y1: false, z1: false, x2: false, y2: true, z2: false);
		edge(x1: false, y1: false, z1: false, x2: false, y2: false, z2: true);
		edge(x1: false, y1: true, z1: false, x2: true, y2: true, z2: false);
		edge(x1: false, y1: true, z1: false, x2: false, y2: true, z2: true);
		edge(x1: true, y1: true, z1: true, x2: false, y2: true, z2: true);
		edge(x1: true, y1: true, z1: true, x2: true, y2: false, z2: true);
		edge(x1: true, y1: true, z1: true, x2: true, y2: true, z2: false);
		edge(x1: false, y1: false, z1: true, x2: true, y2: false, z2: true);
		edge(x1: false, y1: false, z1: true, x2: false, y2: true, z2: true);
		edge(x1: true, y1: false, z1: false, x2: true, y2: true, z2: false);
		edge(x1: true, y1: false, z1: false, x2: true, y2: false, z2: true);
		void edge(bool x1, bool y1, bool z1, bool x2, bool y2, bool z2)
		{
			line(new Vector3(x1 ? max.x : min.x, y1 ? max.y : min.y, z1 ? max.z : min.z), new Vector3(x2 ? max.x : min.x, y2 ? max.y : min.y, z2 ? max.z : min.z), color, duration);
		}
	}

	private static void line(Vector3 start, Vector3 end, Color color, float duration = 0f)
	{
		if (duration < float.Epsilon)
		{
			Debug.DrawLine(start, end, color);
		}
		else
		{
			Debug.DrawLine(start, end, color, duration);
		}
	}
}
