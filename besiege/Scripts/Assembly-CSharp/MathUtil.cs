using System;
using UnityEngine;

public static class MathUtil
{
	public static float DifferenceBetweenAngles(float firstAngle, float secondAngle)
	{
		float num;
		for (num = secondAngle - firstAngle; num < -180f; num += 360f)
		{
		}
		while (num > 180f)
		{
			num -= 360f;
		}
		return num;
	}

	public static bool CompareVectors(Vector3 a, Vector3 b, float angleError)
	{
		if (!Mathf.Approximately(a.magnitude, b.magnitude))
		{
			return false;
		}
		float num = Mathf.Cos(angleError * ((float)Math.PI / 180f));
		float num2 = Vector3.Dot(a.normalized, b.normalized);
		return num2 >= num;
	}

	public static Vector3 NormalizeVector(float length, Vector3 v)
	{
		return new Vector3(v.x / length, v.y / length, v.z / length);
	}
}
