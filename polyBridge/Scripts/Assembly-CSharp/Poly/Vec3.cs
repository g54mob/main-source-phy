using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Poly
{
	[Serializable]
	[DebuggerDisplay("({x}, {y}, {z})")]
	public struct Vec3 : IComparable<Vec3>
	{
		public float x;

		public float y;

		public float z;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static readonly Vec3 zero = new Vec3(0f, 0f, 0f);

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static readonly Vec3 one = new Vec3(1f, 1f, 1f);

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static readonly Vec3 right = new Vec3(1f, 0f, 0f);

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static readonly Vec3 up = new Vec3(0f, 1f, 0f);

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static readonly Vec3 forward = new Vec3(0f, 0f, 1f);

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static readonly Vec3 left = new Vec3(-1f, 0f, 0f);

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static readonly Vec3 down = new Vec3(0f, -1f, 0f);

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static readonly Vec3 back = new Vec3(0f, 0f, -1f);

		public Vec3(float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public static implicit operator Vec3(Vector3 v)
		{
			return new Vec3(v.x, v.y, v.z);
		}

		public static implicit operator Vector3(Vec3 v)
		{
			return new Vector3(v.x, v.y, v.z);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vec3 operator +(Vec3 a, Vec3 b)
		{
			return new Vec3(a.x + b.x, a.y + b.y, a.z + b.z);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vec3 operator -(Vec3 a, Vec3 b)
		{
			return new Vec3(a.x - b.x, a.y - b.y, a.z - b.z);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vec3 operator -(Vec3 a)
		{
			return new Vec3(0f - a.x, 0f - a.y, 0f - a.z);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vec3 operator *(float a, Vec3 b)
		{
			return new Vec3(a * b.x, a * b.y, a * b.z);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vec3 operator *(Vec3 a, float b)
		{
			return new Vec3(a.x * b, a.y * b, a.z * b);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vec3 operator /(Vec3 a, float b)
		{
			return new Vec3(a.x / b, a.y / b, a.z / b);
		}

		public int CompareTo(Vec3 other)
		{
			int num = z.CompareTo(other.z);
			if (num == 0)
			{
				num = y.CompareTo(other.y);
			}
			if (num == 0)
			{
				num = x.CompareTo(other.x);
			}
			return num;
		}
	}
}
