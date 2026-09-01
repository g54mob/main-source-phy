using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Poly.Collide
{
	[StructLayout(LayoutKind.Explicit, Size = 4)]
	[DebuggerDisplay("{type}: ({vert0}, {vert1}, {vert2})")]
	public struct Feature
	{
		public enum Type : byte
		{
			PointPoint = 0,
			PointEdge = 1,
			EdgePoint = 2
		}

		[FieldOffset(0)]
		public Type type;

		[FieldOffset(1)]
		public byte vert0;

		[FieldOffset(2)]
		public byte vert1;

		[FieldOffset(3)]
		public byte vert2;

		[FieldOffset(0)]
		public uint key;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static readonly Feature invalid;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AreFeaturesMatchingAndRelatedByShapeSidewaysMovement(in Feature a, in Feature b)
		{
			if (a.type == Type.PointEdge)
			{
				if (b.type == Type.EdgePoint)
				{
					if (a.vert0 == b.vert1 && a.vert1 == b.vert2)
					{
						return true;
					}
					if (a.vert0 == b.vert0 && a.vert2 == b.vert2)
					{
						return true;
					}
				}
				else
				{
					if (a.vert0 == b.vert0 && a.vert1 == b.vert2)
					{
						return true;
					}
					if (a.vert0 == b.vert0 && a.vert2 == b.vert1)
					{
						return true;
					}
				}
			}
			else if (b.type == Type.PointEdge)
			{
				if (a.vert0 == b.vert0 && a.vert2 == b.vert2)
				{
					return true;
				}
				if (a.vert1 == b.vert0 && a.vert2 == b.vert1)
				{
					return true;
				}
			}
			else
			{
				if (a.vert0 == b.vert1 && a.vert2 == b.vert2)
				{
					return true;
				}
				if (a.vert1 == b.vert0 && a.vert2 == b.vert2)
				{
					return true;
				}
			}
			return false;
		}

		public static bool operator ==(Feature a, Feature b)
		{
			return a.key == b.key;
		}

		public static bool operator !=(Feature a, Feature b)
		{
			return a.key != b.key;
		}

		public override bool Equals(object other)
		{
			if (other is Feature)
			{
				return this == (Feature)other;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return key.GetHashCode();
		}
	}
}
