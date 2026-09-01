using System.Diagnostics;

namespace Poly.Extension
{
	[DebuggerDisplay("({x}, {y})")]
	public struct Vec2UShort
	{
		private uint value;

		public ushort x
		{
			get
			{
				return (ushort)(value >> 16);
			}
			set
			{
				this.value = (uint)(value << 16) | (this.value & 0xFFFF);
			}
		}

		public ushort y
		{
			get
			{
				return (ushort)value;
			}
			set
			{
				this.value = value | (this.value & 0xFFFF0000u);
			}
		}

		public Vec2UShort(ushort x, ushort y)
		{
			value = (uint)((x << 16) | y);
		}
	}
}
