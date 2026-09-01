using System.Runtime.CompilerServices;
using UnityEngine;

namespace Poly.Extension
{
	public static class Vector2Extension
	{
		public static void Rotate90(this Vector2 v)
		{
			float x = v.x;
			v.x = 0f - v.y;
			v.y = x;
		}

		public static Vector2 Rotated90(this Vector2 v)
		{
			return new Vector2(0f - v.y, v.x);
		}

		public static void SetSub(this Vector2 v, ref Vector2 v0, ref Vector2 v1)
		{
			v.x = v0.x - v1.x;
			v.y = v0.y - v1.y;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetSub(ref Vector2 v0, ref Vector2 v1, out Vector2 v)
		{
			v.x = v0.x - v1.x;
			v.y = v0.y - v1.y;
		}

		public static void Mul(this Vector2 v, float a)
		{
			v.x *= a;
			v.y *= a;
		}

		public static void Mul(ref Vector2 v, float a)
		{
			v.x *= a;
			v.y *= a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetRotated90(ref Vector2 v0, out Vector2 v)
		{
			v.x = 0f - v0.y;
			v.y = v0.x;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Dot(ref Vector2 a, ref Vector2 b)
		{
			return a.x * b.x + a.y * b.y;
		}

		public static int CompareTo(in Vector2 vec, in Vector2 other)
		{
			float y = vec.y;
			int num = y.CompareTo(other.y);
			if (num == 0)
			{
				y = vec.x;
				num = y.CompareTo(other.x);
			}
			return num;
		}
	}
}
