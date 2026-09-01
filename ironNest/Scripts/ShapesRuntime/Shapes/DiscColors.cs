using UnityEngine;

namespace Shapes
{
	public struct DiscColors
	{
		public Color innerStart;

		public Color outerStart;

		public Color innerEnd;

		public Color outerEnd;

		internal DiscColors(Color innerStart, Color outerStart, Color innerEnd, Color outerEnd)
		{
			this.innerStart = default(Color);
			this.outerStart = default(Color);
			this.innerEnd = default(Color);
			this.outerEnd = default(Color);
		}

		public static DiscColors Flat(Color color)
		{
			return default(DiscColors);
		}

		public static DiscColors Radial(Color inner, Color outer)
		{
			return default(DiscColors);
		}

		public static DiscColors Angular(Color start, Color end)
		{
			return default(DiscColors);
		}

		public static DiscColors Bilinear(Color innerStart, Color outerStart, Color innerEnd, Color outerEnd)
		{
			return default(DiscColors);
		}

		public static implicit operator DiscColors(Color flatColor)
		{
			return default(DiscColors);
		}
	}
}
