using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Poly.Math
{
	[DebuggerDisplay("({position.x}, {position.y}), {rotation.angle_slow}°")]
	public struct Transform2
	{
		public Vec2 position;

		internal Rotation2 rotation;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static Transform2 identity = new Transform2(Vec2.zero, 0f);

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public Vec2 right => rotation.right;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public Vec2 up => rotation.up;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public float angle_slow
		{
			get
			{
				return rotation.angle_slow;
			}
			set
			{
				rotation.angle_slow = value;
			}
		}

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public Transform2 inverse_unoptimized
		{
			get
			{
				_InvMul(ref this, ref identity, out var r);
				return r;
			}
		}

		public static implicit operator Transform2(Transform t)
		{
			return new Transform2((Vec2)t.position, t.rotation.eulerAngles.z);
		}

		public Transform2(Vec2 pos, Vec2 right)
		{
			position = pos;
			rotation = new Rotation2(right);
		}

		public Transform2(Vec2 pos, float angle)
		{
			position = pos;
			Rotation2.SetRotation_Slow(angle, out rotation);
		}

		public static Vec2 operator *(Transform2 t, Vec2 v)
		{
			return t.rotation * v + t.position;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void _InlineMul(ref Transform2 t, ref Vec2 v, out Vec2 r)
		{
			r.x = t.rotation.m00 * v.x - t.rotation.m10 * v.y + t.position.x;
			r.y = t.rotation.m10 * v.x + t.rotation.m00 * v.y + t.position.y;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Vec2 InvMul(Vec2 v)
		{
			v.sub(in position);
			return rotation.InvMul(in v);
		}

		public static Transform2 operator *(Transform2 t0, Transform2 t1)
		{
			return new Transform2
			{
				position = t0.position + t0.rotation * t1.position,
				rotation = t0.rotation * t1.rotation
			};
		}

		public Transform2 InvMul(Transform2 t1)
		{
			return new Transform2
			{
				position = rotation.InvMul(t1.position - position),
				rotation = rotation.InvMul(t1.rotation)
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void _InvMul(ref Transform2 t0, ref Transform2 t1, out Transform2 r)
		{
			float num = t1.position.x - t0.position.x;
			float num2 = t1.position.y - t0.position.y;
			r.position.x = t0.rotation.m00 * num + t0.rotation.m10 * num2;
			r.position.y = (0f - t0.rotation.m10) * num + t0.rotation.m00 * num2;
			r.rotation.m00 = t0.rotation.m00 * t1.rotation.m00 + t0.rotation.m10 * t1.rotation.m10;
			r.rotation.m10 = (0f - t0.rotation.m10) * t1.rotation.m00 + t0.rotation.m00 * t1.rotation.m10;
		}

		private static float CircularClipPlusMinusRange_Slow(float value, float range)
		{
			float num = value / range;
			value -= (float)(int)num * range;
			return value;
		}

		public static bool operator ==(in Transform2 t0, in Transform2 t1)
		{
			if (t0.position == t1.position)
			{
				return t0.rotation == t1.rotation;
			}
			return false;
		}

		public static bool operator !=(in Transform2 t0, in Transform2 t1)
		{
			if (!(t0.position != t1.position))
			{
				return t0.rotation != t1.rotation;
			}
			return true;
		}

		public override bool Equals(object obj)
		{
			if (obj is Transform2)
			{
				return this == (Transform2)obj;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return position.GetHashCode() ^ rotation.GetHashCode();
		}
	}
}
