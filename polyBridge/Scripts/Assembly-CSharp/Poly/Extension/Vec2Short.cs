using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Poly.Extension
{
	[StructLayout(LayoutKind.Explicit, Size = 4)]
	[DebuggerDisplay("({x}, {y})")]
	public struct Vec2Short : IComparable<Vec2Short>
	{
		[FieldOffset(0)]
		public short x;

		[FieldOffset(2)]
		public short y;

		[FieldOffset(0)]
		public int key;

		public static readonly Vec2Short zero = new Vec2Short(0, 0);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Vec2Short(short x, short y)
		{
			key = 0;
			this.x = x;
			this.y = y;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator Vector2Int(Vec2Short v)
		{
			return new Vector2Int(v.x, v.y);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(in Vec2Short a, in Vec2Short b)
		{
			if (a.x == b.x)
			{
				return a.y == b.y;
			}
			return false;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(in Vec2Short a, in Vec2Short b)
		{
			if (a.x == b.x)
			{
				return a.y != b.y;
			}
			return true;
		}

		public override bool Equals(object other)
		{
			if (other is Vec2Short)
			{
				return this == (Vec2Short)other;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return x.GetHashCode() ^ y.GetHashCode();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int CompareTo(Vec2Short other)
		{
			return key.CompareTo(other.key);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vec2Short FromKey(int key)
		{
			return new Vec2Short
			{
				key = key
			};
		}
	}
}
