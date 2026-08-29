using System;
using System.Runtime.CompilerServices;

namespace MagicaCloth2
{
	[Serializable]
	public struct VertexAttribute : IEquatable<VertexAttribute>
	{
		public const byte Flag_Fixed = 1;

		public const byte Flag_Move = 2;

		public const byte Flag_InvalidMotion = 8;

		public const byte Flag_DisableCollision = 16;

		public const byte Flag_Triangle = 128;

		public static readonly VertexAttribute Invalid = default(VertexAttribute);

		public static readonly VertexAttribute Fixed = new VertexAttribute(1);

		public static readonly VertexAttribute Move = new VertexAttribute(2);

		public static readonly VertexAttribute DisableCollision = new VertexAttribute(16);

		public byte Value;

		public VertexAttribute(byte initialValue = 0)
		{
			Value = initialValue;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Clear()
		{
			Value = 0;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetFlag(byte flag, bool sw)
		{
			if (sw)
			{
				Value |= flag;
			}
			else
			{
				Value = (byte)(Value & ~flag);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetFlag(VertexAttribute attr, bool sw)
		{
			if (sw)
			{
				Value |= attr.Value;
			}
			else
			{
				Value = (byte)(Value & ~attr.Value);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool IsSet(byte flag)
		{
			return (Value & flag) != 0;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool IsInvalid()
		{
			return !IsSet(3);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool IsFixed()
		{
			return IsSet(1);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool IsMove()
		{
			return IsSet(2);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool IsDontMove()
		{
			return !IsSet(2);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool IsMotion()
		{
			return !IsSet(8);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool IsDisableCollision()
		{
			return IsSet(16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static VertexAttribute JoinAttribute(VertexAttribute attr1, VertexAttribute attr2)
		{
			if (attr1.Value >= attr2.Value)
			{
				return attr2;
			}
			return attr1;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(VertexAttribute other)
		{
			return Value == other.Value;
		}

		public override bool Equals(object obj)
		{
			if (obj is VertexAttribute other)
			{
				return Equals(other);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return Value;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(VertexAttribute lhs, VertexAttribute rhs)
		{
			return lhs.Value == rhs.Value;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(VertexAttribute lhs, VertexAttribute rhs)
		{
			return lhs.Value != rhs.Value;
		}
	}
}
