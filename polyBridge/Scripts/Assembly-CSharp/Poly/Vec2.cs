using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Poly
{
	[Serializable]
	[DebuggerDisplay("({x}, {y})")]
	public struct Vec2 : IComparable<Vec2>
	{
		public float x;

		public float y;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static readonly Vec2 zero = new Vec2(0f, 0f);

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static readonly Vec2 one = new Vec2(1f, 1f);

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static readonly Vec2 right = new Vec2(1f, 0f);

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static readonly Vec2 up = new Vec2(0f, 1f);

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static readonly Vec2 left = new Vec2(-1f, 0f);

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static readonly Vec2 down = new Vec2(0f, -1f);

		public float this[int i]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return i switch
				{
					0 => x, 
					1 => y, 
					_ => 0f, 
				};
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				switch (i)
				{
				case 0:
					x = value;
					break;
				case 1:
					y = value;
					break;
				}
			}
		}

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public float sqrMagnitude
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return x * x + y * y;
			}
		}

		public float magnitude
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return (float)System.Math.Sqrt(x * x + y * y);
			}
		}

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public Vec2 normalized
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				Vec2 result = this;
				result.Normalize();
				return result;
			}
		}

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public Vec2 rotated90
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new Vec2(0f - y, x);
			}
		}

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public Vec2 inversed_unchecked
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new Vec2(1f / x, 1f / y);
			}
		}

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public Vec2 inversed_safe
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new Vec2(1f / (x + 5.877472E-39f), 1f / (y + 5.877472E-39f));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Vec2(float x, float y)
		{
			this.x = x;
			this.y = y;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator Vec2(Vector2 v)
		{
			return new Vec2(v.x, v.y);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator Vector2(Vec2 v)
		{
			return new Vector2(v.x, v.y);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator Vec2(Vector3 v)
		{
			return new Vec2(v.x, v.y);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator Vector3(Vec2 v)
		{
			return new Vector3(v.x, v.y);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator Vec2(Vec3 v)
		{
			return new Vec2(v.x, v.y);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator Vec3(Vec2 v)
		{
			return new Vec3(v.x, v.y, 0f);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vec2 operator +(Vec2 a, Vec2 b)
		{
			return new Vec2(a.x + b.x, a.y + b.y);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vec2 operator -(Vec2 a, Vec2 b)
		{
			return new Vec2(a.x - b.x, a.y - b.y);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vec2 operator -(Vec2 a)
		{
			return new Vec2(0f - a.x, 0f - a.y);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vec2 operator *(float a, Vec2 b)
		{
			return new Vec2(a * b.x, a * b.y);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vec2 operator *(Vec2 a, float b)
		{
			return new Vec2(a.x * b, a.y * b);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vec2 operator /(Vec2 a, float b)
		{
			return new Vec2(a.x / b, a.y / b);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(Vec2 a, Vec2 b)
		{
			if (a.x == b.x)
			{
				return a.y == b.y;
			}
			return false;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(Vec2 a, Vec2 b)
		{
			if (a.x == b.x)
			{
				return a.y != b.y;
			}
			return true;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override bool Equals(object obj)
		{
			if (obj is Vec2)
			{
				return this == (Vec2)obj;
			}
			return false;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return x.GetHashCode() ^ y.GetHashCode();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Distance(in Vec2 a, in Vec2 b)
		{
			float num = b.x - a.x;
			float num2 = b.y - a.y;
			return (float)System.Math.Sqrt(num * num + num2 * num2);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float DistanceSqr(in Vec2 a, in Vec2 b)
		{
			float num = b.x - a.x;
			float num2 = b.y - a.y;
			return num * num + num2 * num2;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Dot(in Vec2 a, in Vec2 b)
		{
			return a.x * b.x + a.y * b.y;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Cross(in Vec2 a, in Vec2 b)
		{
			return a.x * b.y - a.y * b.x;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vec2 Cross(float a, in Vec2 b)
		{
			return new Vec2((0f - a) * b.y, a * b.x);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vec2 Scale(ref Vec2 a, ref Vec2 b)
		{
			return new Vec2(a.x * b.x, a.y * b.y);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vec2 Scale(Vec2 a, Vec2 b)
		{
			return new Vec2(a.x * b.x, a.y * b.y);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void add(in Vec2 v)
		{
			x += v.x;
			y += v.y;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void addMul(in Vec2 v, float f)
		{
			x += v.x * f;
			y += v.y * f;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void setAdd(ref Vec2 a, ref Vec2 b)
		{
			x = a.x + b.x;
			y = a.y + b.y;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void setAdd(ref Vec2 a, ref Vec2 b, out Vec2 v)
		{
			v.x = a.x + b.x;
			v.y = a.y + b.y;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void setAddMul(ref Vec2 a, ref Vec2 b, float c)
		{
			x = a.x + b.x * c;
			y = a.y + b.y * c;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void setAddMul(ref Vec2 a, ref Vec2 b, float c, out Vec2 v)
		{
			v.x = a.x + b.x * c;
			v.y = a.y + b.y * c;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void sub(in Vec2 v)
		{
			x -= v.x;
			y -= v.y;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void setSub(ref Vec2 v0, ref Vec2 v1)
		{
			x = v0.x - v1.x;
			y = v0.y - v1.y;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void setSub(in Vec2 v0, in Vec2 v1, out Vec2 v)
		{
			v.x = v0.x - v1.x;
			v.y = v0.y - v1.y;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void mul(float f)
		{
			x *= f;
			y *= f;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Normalize()
		{
			float num = 1f / (magnitude + 5.877472E-39f);
			x *= num;
			y *= num;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Rotate90()
		{
			float num = x;
			x = 0f - y;
			y = num;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void setRotated90(in Vec2 v0, out Vec2 v)
		{
			v.x = 0f - v0.y;
			v.y = v0.x;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vec2 LerpUnclamped(in Vec2 a, in Vec2 b, float t)
		{
			float num = 1f - t;
			return new Vec2(num * a.x + t * b.x, num * a.y + t * b.y);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vec2 Clamp(Vec2 v, in Vec2 min, in Vec2 max)
		{
			v.x = ((!(min.x <= v.x)) ? min.x : ((v.x <= max.x) ? v.x : max.x));
			v.y = ((!(min.y <= v.y)) ? min.y : ((v.y <= max.y) ? v.y : max.y));
			return v;
		}

		public override string ToString()
		{
			return $"({x:0.0}, {y:0.0})";
		}

		public int CompareTo(Vec2 other)
		{
			int num = y.CompareTo(other.y);
			if (num == 0)
			{
				return x.CompareTo(other.x);
			}
			return num;
		}

		public static Vec2 CoordAbs(Vec2 v)
		{
			if (v.x < 0f)
			{
				v.x *= -1f;
			}
			if (v.y < 0f)
			{
				v.y *= -1f;
			}
			return v;
		}
	}
}
