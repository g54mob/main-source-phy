using UnityEngine;

public static class Vector3Util
{
	public static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 axis)
	{
		Vector3 vector = point - pivot;
		vector = Quaternion.Euler(axis) * vector;
		return vector + pivot;
	}
}
