using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace MTAssets.UltimateLODSystem.MeshSimplifier
{
	public struct Vector3d : IEquatable<Vector3d>
	{
		public static readonly Vector3d zero;

		public const double Epsilon = 5E-324;

		public double x;

		public double y;

		public double z;

		public double Magnitude
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return 0.0;
			}
		}

		public double MagnitudeSqr
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return 0.0;
			}
		}

		public Vector3d Normalized
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return default(Vector3d);
			}
		}

		public double this[int index]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return 0.0;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Vector3d(double value)
		{
			x = 0.0;
			y = 0.0;
			z = 0.0;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Vector3d(double x, double y, double z)
		{
			this.x = 0.0;
			this.y = 0.0;
			this.z = 0.0;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Vector3d(Vector3 vector)
		{
			x = 0.0;
			y = 0.0;
			z = 0.0;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3d operator +(Vector3d a, Vector3d b)
		{
			return default(Vector3d);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3d operator -(Vector3d a, Vector3d b)
		{
			return default(Vector3d);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3d operator *(Vector3d a, double d)
		{
			return default(Vector3d);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3d operator *(double d, Vector3d a)
		{
			return default(Vector3d);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3d operator /(Vector3d a, double d)
		{
			return default(Vector3d);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3d operator -(Vector3d a)
		{
			return default(Vector3d);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(Vector3d lhs, Vector3d rhs)
		{
			return false;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(Vector3d lhs, Vector3d rhs)
		{
			return false;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator Vector3d(Vector3 v)
		{
			return default(Vector3d);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator Vector3(Vector3d v)
		{
			return default(Vector3);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Set(double x, double y, double z)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Scale(ref Vector3d scale)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Normalize()
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Clamp(double min, double max)
		{
		}

		public override int GetHashCode()
		{
			return 0;
		}

		public override bool Equals(object obj)
		{
			return false;
		}

		public bool Equals(Vector3d other)
		{
			return false;
		}

		public override string ToString()
		{
			return null;
		}

		public string ToString(string format)
		{
			return null;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double Dot(ref Vector3d lhs, ref Vector3d rhs)
		{
			return 0.0;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Cross(ref Vector3d lhs, ref Vector3d rhs, out Vector3d result)
		{
			result = default(Vector3d);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double Angle(ref Vector3d from, ref Vector3d to)
		{
			return 0.0;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Lerp(ref Vector3d a, ref Vector3d b, double t, out Vector3d result)
		{
			result = default(Vector3d);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Scale(ref Vector3d a, ref Vector3d b, out Vector3d result)
		{
			result = default(Vector3d);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Normalize(ref Vector3d value, out Vector3d result)
		{
			result = default(Vector3d);
		}
	}
}
