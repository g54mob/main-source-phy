using UnityEngine;

public static class VectorExtensions
{
	public static Vector3 Absolute(this Vector3 vector)
	{
		vector.x = Mathf.Abs(vector.x);
		vector.y = Mathf.Abs(vector.y);
		vector.z = Mathf.Abs(vector.z);
		return vector;
	}

	public static Vector2 Clamp(this Vector2 v, float min, float max)
	{
		v.x = Mathf.Clamp(v.x, min, max);
		v.y = Mathf.Clamp(v.y, min, max);
		return v;
	}

	public static Vector2 Clamp01(this Vector2 v)
	{
		v.x = Mathf.Clamp01(v.x);
		v.y = Mathf.Clamp01(v.y);
		return v;
	}

	public static Vector3 WithX(this Vector3 v, float x)
	{
		v = new Vector3(v.x, v.y, v.z);
		v.x = x;
		return v;
	}

	public static Vector3 WithY(this Vector3 v, float y)
	{
		v = new Vector3(v.x, v.y, v.z);
		v.y = y;
		return v;
	}

	public static Vector3 WithZ(this Vector3 v, float z)
	{
		v = new Vector3(v.x, v.y, v.z);
		v.z = z;
		return v;
	}

	public static float MaxAxis(this Vector3 v)
	{
		if (v.x >= v.y && v.x >= v.z)
		{
			return v.x;
		}
		if (v.y >= v.x && v.y >= v.z)
		{
			return v.y;
		}
		return v.z;
	}

	public static Vector3 Scaled(this Vector3 v, Vector3 scale)
	{
		v.Scale(scale);
		return v;
	}

	public static Vector3 Unscaled(this Vector3 v, Vector3 scale)
	{
		v.Scale(new Vector3((scale.x != 0f) ? (1f / scale.x) : 0f, (scale.y != 0f) ? (1f / scale.y) : 0f, (scale.z != 0f) ? (1f / scale.z) : 0f));
		return v;
	}

	public static float MaxAxis(this Vector2 v)
	{
		return Mathf.Max(v.x, v.y);
	}

	public static Vector2 Scaled(this Vector2 v, Vector2 scale)
	{
		v.Scale(scale);
		return v;
	}

	public static Vector2 Unscaled(this Vector2 v, Vector2 scale)
	{
		v.Scale(new Vector2((scale.x != 0f) ? (1f / scale.x) : 0f, (scale.y != 0f) ? (1f / scale.y) : 0f));
		return v;
	}
}
