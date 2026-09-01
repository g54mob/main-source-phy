using UnityEngine;

public class DecalPolygon
{
	public Vector3[] vertices = new Vector3[9];

	public int Count;

	public static Vector3[] tempPolygon = new Vector3[9];

	public static bool[] positive = new bool[9];

	public static int positiveCount = 0;

	public static int tempCount = 0;

	public static Vector3 vectorZero = Vector3.zero;

	public DecalPolygon(params Vector3[] vts)
	{
		for (int i = 0; i < vts.Length; i++)
		{
			vertices[i] = vts[i];
		}
		Count = vts.Length;
	}

	public static bool ClipPolygon(DecalPolygon polygon, Plane plane)
	{
		positiveCount = 0;
		tempCount = 0;
		for (int i = 0; i < polygon.Count; i++)
		{
			positive[i] = !plane.GetSide(polygon.vertices[i]);
			if (positive[i])
			{
				positiveCount++;
			}
		}
		if (positiveCount == 0)
		{
			return false;
		}
		if (positiveCount == polygon.Count)
		{
			return true;
		}
		for (int j = 0; j < polygon.Count; j++)
		{
			int num = j + 1;
			num %= polygon.Count;
			if (positive[j])
			{
				tempPolygon[tempCount] = polygon.vertices[j];
				tempCount++;
			}
			if (positive[j] != positive[num])
			{
				Vector3 a = polygon.vertices[num];
				Vector3 b = polygon.vertices[j];
				Vector3 vector = LineCast(plane, a, b);
				tempPolygon[tempCount] = vector;
				tempCount++;
			}
		}
		polygon.Count = tempCount;
		for (int k = 0; k < polygon.Count; k++)
		{
			polygon.vertices[k] = tempPolygon[k];
		}
		return true;
	}

	private static Vector3 LineCast(Plane plane, Vector3 a, Vector3 b)
	{
		Ray ray = new Ray(a, b - a);
		float enter;
		plane.Raycast(ray, out enter);
		return ray.GetPoint(enter);
	}
}
