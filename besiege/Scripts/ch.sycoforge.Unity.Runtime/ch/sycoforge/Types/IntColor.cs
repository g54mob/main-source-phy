namespace ch.sycoforge.Types
{
	public struct IntColor
	{
		public int argb;

		public IntColor(int argb)
		{
			this.argb = argb;
		}

		public static implicit operator IntColor(ByteColor c)
		{
			IntColor result = default(IntColor);
			int num = c.a << 24;
			int num2 = c.r << 16;
			int num3 = c.g << 8;
			int b = c.b;
			result.argb = num | num2 | num3 | b;
			return result;
		}

		public static implicit operator IntColor(int c)
		{
			return new IntColor
			{
				argb = c
			};
		}

		public static implicit operator int(IntColor c)
		{
			return c.argb;
		}

		public static implicit operator ByteColor(IntColor c)
		{
			return new ByteColor
			{
				a = (byte)((c.argb >> 24) & 0xFF),
				r = (byte)((c.argb >> 16) & 0xFF),
				g = (byte)((c.argb >> 8) & 0xFF),
				b = (byte)(c.argb & 0xFF)
			};
		}
	}
}
