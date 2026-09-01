using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Poly.Math
{
	[DebuggerDisplay("{angle_slow}°")]
	public struct Rotation2
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal float m00;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal float m10;

		public static readonly Rotation2 identity = new Rotation2(Vec2.right);

		private static bool warnOnce = true;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public Vec2 basisX
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new Vec2(m00, m10);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				m00 = value.x;
				m10 = value.y;
			}
		}

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public Vec2 basisY
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new Vec2(m01, m11);
			}
		}

		public Vec2 right
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new Vec2(m00, m10);
			}
		}

		public Vec2 up
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new Vec2(m01, m11);
			}
		}

		public float angle_slow
		{
			get
			{
				return Mathf.Atan2(right.y, right.x) * 57.29578f;
			}
			set
			{
				SetRotation_Slow(value, out this);
			}
		}

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal float m01 => 0f - m10;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal float m11 => m00;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Rotation2(Vec2 right)
		{
			m00 = right.x;
			m10 = right.y;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetRotation_Slow(float angle, out Rotation2 result)
		{
			float num = angle * (MathF.PI / 180f);
			if (num < -17453f || 17453f < num)
			{
				if (warnOnce)
				{
					UnityEngine.Debug.LogWarning("Rotation2's angle value too high");
					warnOnce = false;
				}
				float f = num / (MathF.PI * 2f);
				num -= Mathf.Floor(f);
			}
			result.m00 = Mathf.Cos(num);
			result.m10 = Mathf.Sin(num);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetRotation_Slow_NoAngleCheck(float angle, out Rotation2 result)
		{
			float f = angle * (MathF.PI / 180f);
			result.m00 = Mathf.Cos(f);
			result.m10 = Mathf.Sin(f);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vec2 operator *(Rotation2 m, Vec2 v)
		{
			Vec2 result = default(Vec2);
			result.x = m.m00 * v.x - m.m10 * v.y;
			result.y = m.m10 * v.x + m.m00 * v.y;
			return result;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Vec2 InvMul(in Vec2 v)
		{
			Vec2 result = default(Vec2);
			result.x = m00 * v.x + m10 * v.y;
			result.y = (0f - m10) * v.x + m00 * v.y;
			return result;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Rotation2 operator *(Rotation2 r0, Rotation2 r1)
		{
			return new Rotation2
			{
				basisX = r0 * r1.basisX
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Rotation2 InvMul(Rotation2 r1)
		{
			return new Rotation2
			{
				basisX = InvMul(r1.basisX)
			};
		}

		public static bool operator ==(in Rotation2 r0, in Rotation2 r1)
		{
			if (r0.m00 == r1.m00)
			{
				return r0.m10 == r1.m10;
			}
			return false;
		}

		public static bool operator !=(in Rotation2 r0, in Rotation2 r1)
		{
			if (r0.m00 == r1.m00)
			{
				return r0.m10 != r1.m10;
			}
			return true;
		}

		public override bool Equals(object obj)
		{
			if (obj is Rotation2)
			{
				return this == (Rotation2)obj;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return m00.GetHashCode() ^ m10.GetHashCode();
		}

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void RuntimeInitialize()
		{
			warnOnce = true;
		}
	}
}
