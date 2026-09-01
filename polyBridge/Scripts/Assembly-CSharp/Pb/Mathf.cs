using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Poly;
using Poly.Math;

namespace Pb
{
	public static class Mathf
	{
		[StructLayout(LayoutKind.Explicit)]
		private struct FloatIntUnion
		{
			[FieldOffset(0)]
			public float f;

			[FieldOffset(0)]
			public int tmp;
		}

		public const float Pi = MathF.PI;

		public const float TwoPi = MathF.PI * 2f;

		public const float ThreePi = MathF.PI * 3f;

		public const float Epsilon = 1E-06f;

		public const float Epsilon2 = 1E-12f;

		public const float KindaSmallNumber = 0.0001f;

		public const float KindaSmallNumber2 = 9.999999E-09f;

		public const float MinDenominator = 5.877472E-39f;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float WrapAngleOnceToOnePi(float angle)
		{
			if (angle < -MathF.PI)
			{
				angle += MathF.PI * 2f;
			}
			else if (MathF.PI < angle)
			{
				angle -= MathF.PI * 2f;
			}
			return angle;
		}

		public static float WrapAngleToOnePi_Slow(float angle)
		{
			int num = (int)Math.Ceiling(angle / MathF.PI) & -2;
			angle -= (float)num * MathF.PI;
			return angle;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsAngleWithInOnePi(float angle)
		{
			if (-MathF.PI <= angle)
			{
				return angle <= MathF.PI;
			}
			return false;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Abs(float a)
		{
			if (!(0f <= a))
			{
				return 0f - a;
			}
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Min(float a, float b)
		{
			if (!(a <= b))
			{
				return b;
			}
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Max(float a, float b)
		{
			if (!(a <= b))
			{
				return a;
			}
			return b;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Abs(int a)
		{
			if (0 > a)
			{
				return -a;
			}
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Min(int a, int b)
		{
			if (a > b)
			{
				return b;
			}
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Max(int a, int b)
		{
			if (a > b)
			{
				return a;
			}
			return b;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Clamp(float v, float a, float b)
		{
			if (!(a <= v))
			{
				return a;
			}
			if (!(v <= b))
			{
				return b;
			}
			return v;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Clamp01(float v)
		{
			if (!(0f <= v))
			{
				return 0f;
			}
			if (!(v <= 1f))
			{
				return 1f;
			}
			return v;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Approximately(float a, float b, float precision = 1E-06f)
		{
			return Abs(b - a) < Max(precision * Max(Abs(a), Abs(b)), precision);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Approximately2(float a, float b, float precision = 1E-06f)
		{
			return Abs(b - a) < Max(1E-05f * Max(Abs(a), Abs(b)), precision);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Approximately(in Vec2 a, in Vec2 b, float precision = 1E-06f)
		{
			if (Approximately(a.x, b.x, precision))
			{
				return Approximately(a.y, b.y, precision);
			}
			return false;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Approximately(in Rotation2 a, in Rotation2 b, float precision = 1E-06f)
		{
			if (Approximately(a.m00, b.m00, precision))
			{
				return Approximately(a.m10, b.m10, precision);
			}
			return false;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Approximately(in Transform2 a, in Transform2 b, float precision = 1E-06f)
		{
			if (Approximately(in a.position, in b.position, precision))
			{
				return Approximately(in a.rotation, in b.rotation, precision);
			}
			return false;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static float atan_scalar_approximation(float x)
		{
			float num = x * x;
			return x * (0.99997723f + num * (-0.33262348f + num * (0.19354346f + num * (-0.11643287f + num * (0.05265332f + num * -0.0117212f)))));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float atan2_auto_1(float y, float x)
		{
			float num = ((0f <= x) ? x : (0f - x));
			float num2 = ((0f <= y) ? y : (0f - y));
			bool num3 = num < num2;
			float num4 = (num3 ? (x / y) : (y / x));
			float num5 = atan_scalar_approximation(num4);
			num5 = (num3 ? (((num4 >= 0f) ? (MathF.PI / 2f) : (-MathF.PI / 2f)) - num5) : num5);
			if (!(x >= 0f) || !(y >= 0f))
			{
				if (x < 0f && y >= 0f)
				{
					num5 = MathF.PI + num5;
				}
				else if (x < 0f && y < 0f)
				{
					num5 = -MathF.PI + num5;
				}
				else if (x >= 0f)
				{
					_ = 0f;
				}
			}
			return num5;
		}

		public static float _VFastSqrtApprox(float z)
		{
			if (z == 0f)
			{
				return 0f;
			}
			FloatIntUnion floatIntUnion = default(FloatIntUnion);
			floatIntUnion.tmp = 0;
			floatIntUnion.f = z;
			floatIntUnion.tmp -= 8388608;
			floatIntUnion.tmp >>= 1;
			floatIntUnion.tmp += 536870912;
			return floatIntUnion.f;
		}

		public static float _FastSqrt(float z)
		{
			if (z * z < 1E-12f)
			{
				return 0f;
			}
			FloatIntUnion floatIntUnion = default(FloatIntUnion);
			floatIntUnion.tmp = 0;
			float num = 0.5f * z;
			floatIntUnion.f = z;
			floatIntUnion.tmp = 1597463174 - (floatIntUnion.tmp >> 1);
			floatIntUnion.f *= 1.5f - num * floatIntUnion.f * floatIntUnion.f;
			return floatIntUnion.f * z;
		}

		public static float _FastInvSqrt(float z)
		{
			z += 5.877472E-39f;
			FloatIntUnion floatIntUnion = default(FloatIntUnion);
			floatIntUnion.tmp = 0;
			float num = 0.5f * z;
			floatIntUnion.f = z;
			floatIntUnion.tmp = 1597463174 - (floatIntUnion.tmp >> 1);
			floatIntUnion.f *= 1.5f - num * floatIntUnion.f * floatIntUnion.f;
			return floatIntUnion.f;
		}
	}
}
