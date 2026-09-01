using System;
using UnityEngine;

public static class Mathfx
{
	private static readonly float doublePi = (float)Math.PI * 2f;

	private static readonly float fourDivPi = 4f / (float)Math.PI;

	private static readonly float fourDivPiSqr = 0.4052847f;

	public static float Hermite(float start, float end, float value)
	{
		return Mathf.Lerp(start, end, GetHermiteValue(value));
	}

	public static float GetHermiteValue(float v)
	{
		return v * v * (3f - 2f * v);
	}

	public static Vector3 Hermite(Vector3 p0, Vector3 v0, Vector3 p1, Vector3 v1, float mu)
	{
		float num = mu * mu;
		float num2 = num * mu;
		float num3 = 2f * num2 - 3f * num + 1f;
		float num4 = -2f * num2 + 3f * num;
		float num5 = num2 - 2f * num + mu;
		float num6 = num2 - num;
		return p0 * num3 + p1 * num4 + v0 * num5 + v1 * num6;
	}

	public static Vector3 LerpVector(Vector3 oldPos, Vector3 newPos, float alpha)
	{
		if (alpha == 0f)
		{
			return oldPos;
		}
		if (alpha == 1f)
		{
			return newPos;
		}
		float x = oldPos.x;
		float y = oldPos.y;
		float z = oldPos.z;
		float x2 = newPos.x;
		float y2 = newPos.y;
		float z2 = newPos.z;
		return new Vector3(x + (x2 - x) * alpha, y + (y2 - y) * alpha, z + (z2 - z) * alpha);
	}

	public static Quaternion LerpQuaternion(Quaternion oldRot, Quaternion newRot, float alpha)
	{
		if (alpha == 0f)
		{
			return oldRot;
		}
		if (alpha == 1f)
		{
			return newRot;
		}
		float x = oldRot.x;
		float y = oldRot.y;
		float z = oldRot.z;
		float w = oldRot.w;
		float x2 = newRot.x;
		float y2 = newRot.y;
		float z2 = newRot.z;
		float w2 = newRot.w;
		float num = x + (x2 - x) * alpha;
		float num2 = y + (y2 - y) * alpha;
		float num3 = z + (z2 - z) * alpha;
		float num4 = w + (w2 - w) * alpha;
		return new Quaternion((num > 1f) ? 1f : ((!(num < -1f)) ? num : (-1f)), (num2 > 1f) ? 1f : ((!(num2 < -1f)) ? num2 : (-1f)), (num3 > 1f) ? 1f : ((!(num3 < -1f)) ? num3 : (-1f)), (num4 > 1f) ? 1f : ((!(num4 < -1f)) ? num4 : (-1f)));
	}

	public static Quaternion SlerpQuaternion(Quaternion oldRot, Quaternion newRot, float alpha)
	{
		if (alpha == 0f)
		{
			return oldRot;
		}
		if (alpha == 1f)
		{
			return newRot;
		}
		float num = Quaternion.Dot(oldRot, newRot);
		float num2 = 0f;
		bool flag = false;
		if (num < 0f)
		{
			num2 = 0f - num;
			flag = true;
		}
		if (((!flag) ? num : num2) >= 0.999999f)
		{
			return oldRot;
		}
		bool flag2 = false;
		if (flag)
		{
			flag2 = true;
			num = num2;
		}
		float num3 = Mathf.Acos(num);
		float num4 = Mathf.Sqrt(1f - num * num);
		Quaternion result = default(Quaternion);
		if (((!(num4 < 0f)) ? num4 : (0f - num4)) < 0.001f)
		{
			if (flag2)
			{
				result.w = oldRot.w * 0.5f + newRot.w * 0.5f;
				result.x = oldRot.x * 0.5f + newRot.x * 0.5f;
				result.y = oldRot.y * 0.5f + newRot.y * 0.5f;
				result.z = oldRot.z * 0.5f + newRot.z * 0.5f;
			}
			else
			{
				result.w = oldRot.w * 0.5f - newRot.w * 0.5f;
				result.x = oldRot.x * 0.5f - newRot.x * 0.5f;
				result.y = oldRot.y * 0.5f - newRot.y * 0.5f;
				result.z = oldRot.z * 0.5f - newRot.z * 0.5f;
			}
			return result;
		}
		float num5 = Mathf.Sin((1f - alpha) * num3) / num4;
		float num6 = Mathf.Sin(alpha * num3) / num4;
		if (!flag2)
		{
			result.w = oldRot.w * num5 + newRot.w * num6;
			result.x = oldRot.x * num5 + newRot.x * num6;
			result.y = oldRot.y * num5 + newRot.y * num6;
			result.z = oldRot.z * num5 + newRot.z * num6;
		}
		else
		{
			result.w = oldRot.w * num5 - newRot.w * num6;
			result.x = oldRot.x * num5 - newRot.x * num6;
			result.y = oldRot.y * num5 - newRot.y * num6;
			result.z = oldRot.z * num5 - newRot.z * num6;
		}
		return result;
	}

	public static Vector3 Hermite(Vector3 start, Vector3 end, float v)
	{
		return Vector3.Lerp(start, end, GetHermiteValue(v));
	}

	public static Quaternion Hermite(Quaternion start, Quaternion end, float v)
	{
		return Quaternion.Lerp(start, end, GetHermiteValue(v));
	}

	public static float Sinerp(float start, float end, float value)
	{
		return Mathf.Lerp(start, end, Mathf.Sin(value * (float)Math.PI * 0.5f));
	}

	public static float Coserp(float start, float end, float value)
	{
		return Mathf.Lerp(start, end, 1f - Mathf.Cos(value * (float)Math.PI * 0.5f));
	}

	public static float Berp(float start, float end, float value)
	{
		value = Mathf.Clamp01(value);
		value = (Mathf.Sin(value * (float)Math.PI * (0.2f + 2.5f * value * value * value)) * Mathf.Pow(1f - value, 2.2f) + value) * (1f + 1.2f * (1f - value));
		return start + (end - start) * value;
	}

	public static float SmoothStep(float x, float min, float max)
	{
		x = Mathf.Clamp(x, min, max);
		float num = (x - min) / (max - min);
		float num2 = (x - min) / (max - min);
		return -2f * num * num * num + 3f * num2 * num2;
	}

	public static float Lerp(float start, float end, float value)
	{
		return (1f - value) * start + value * end;
	}

	public static Vector3 NearestPoint(Vector3 lineStart, Vector3 lineEnd, Vector3 point)
	{
		Vector3 vector = Vector3.Normalize(lineEnd - lineStart);
		float num = Vector3.Dot(point - lineStart, vector) / Vector3.Dot(vector, vector);
		return lineStart + num * vector;
	}

	public static Vector3 NearestPointStrict(Vector3 lineStart, Vector3 lineEnd, Vector3 point)
	{
		Vector3 vector = lineEnd - lineStart;
		Vector3 vector2 = Vector3.Normalize(vector);
		float value = Vector3.Dot(point - lineStart, vector2) / Vector3.Dot(vector2, vector2);
		return lineStart + Mathf.Clamp(value, 0f, Vector3.Magnitude(vector)) * vector2;
	}

	public static float Bounce(float x)
	{
		return Mathf.Abs(Mathf.Sin(6.28f * (x + 1f) * (x + 1f)) * (1f - x));
	}

	public static float SmoothBounce(float x)
	{
		return Mathf.Sin(6.28f * (x + 1f) * (x + 1f)) * (1f - x);
	}

	public static bool Approx(float val, float about, float range)
	{
		return Mathf.Abs(val - about) < range;
	}

	public static bool Approx(Vector3 val, Vector3 about, float range)
	{
		return (val - about).sqrMagnitude < range * range;
	}

	public static float Clerp(float start, float end, float value)
	{
		float num = Mathf.Abs(180f);
		if (end - start < 0f - num)
		{
			float num2 = (360f - start + end) * value;
			return start + num2;
		}
		if (end - start > num)
		{
			float num2 = (0f - (360f - end + start)) * value;
			return start + num2;
		}
		return start + (end - start) * value;
	}

	public static float LowSin(float x)
	{
		if (x < -(float)Math.PI)
		{
			x += doublePi;
		}
		else if (x > (float)Math.PI)
		{
			x -= doublePi;
		}
		if (x < 0f)
		{
			return fourDivPi * x + fourDivPiSqr * x * x;
		}
		return fourDivPi * x - fourDivPiSqr * x * x;
	}

	public static float HighSin(float x)
	{
		if (x < -(float)Math.PI)
		{
			x += doublePi;
		}
		else if (x > (float)Math.PI)
		{
			x -= doublePi;
		}
		if (x < 0f)
		{
			float num = fourDivPi * x + fourDivPiSqr * x * x;
			if (num < 0f)
			{
				return 0.225f * (num * (0f - num) - num) + num;
			}
			return 0.225f * (num * num - num) + num;
		}
		float num2 = fourDivPi * x - fourDivPiSqr * x * x;
		if (num2 < 0f)
		{
			return 0.225f * (num2 * (0f - num2) - num2) + num2;
		}
		return 0.225f * (num2 * num2 - num2) + num2;
	}

	public static float LowCos(float x)
	{
		if (x < -(float)Math.PI)
		{
			x += doublePi;
		}
		else if (x > (float)Math.PI)
		{
			x -= doublePi;
		}
		x += (float)Math.PI / 2f;
		if (x > (float)Math.PI)
		{
			x -= doublePi;
		}
		if (x < 0f)
		{
			return fourDivPi * x + fourDivPiSqr * x * x;
		}
		return fourDivPi * x - fourDivPiSqr * x * x;
	}

	public static float HighCos(float x)
	{
		if (x < -(float)Math.PI)
		{
			x += doublePi;
		}
		else if (x > (float)Math.PI)
		{
			x -= doublePi;
		}
		x += (float)Math.PI / 2f;
		if (x > (float)Math.PI)
		{
			x -= doublePi;
		}
		if (x < 0f)
		{
			float num = fourDivPi * x + fourDivPiSqr * x * x;
			if (num < 0f)
			{
				return 0.225f * (num * (0f - num) - num) + num;
			}
			return 0.225f * (num * num - num) + num;
		}
		float num2 = fourDivPi * x - fourDivPiSqr * x * x;
		if (num2 < 0f)
		{
			return 0.225f * (num2 * (0f - num2) - num2) + num2;
		}
		return 0.225f * (num2 * num2 - num2) + num2;
	}

	public static float TaylorSin(float x)
	{
		return x - x * x * x / 6f + x * x * x * x * x / 120f - x * x * x * x * x * x * x / 5040f;
	}
}
