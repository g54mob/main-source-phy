namespace plog.Models
{
	public struct UniversalColor
	{
		public readonly byte Red;

		public readonly byte Green;

		public readonly byte Blue;

		public UniversalColor(byte red, byte green, byte blue)
		{
			Red = red;
			Green = green;
			Blue = blue;
		}

		public UniversalColor(int red, int green, int blue)
		{
			Red = (byte)red;
			Green = (byte)green;
			Blue = (byte)blue;
		}

		public UniversalColor(float red, float green, float blue)
		{
			Red = (byte)(red * 255f);
			Green = (byte)(green * 255f);
			Blue = (byte)(blue * 255f);
		}

		public UniversalColor CopyWith(byte? red = null, byte? green = null, byte? blue = null)
		{
			return new UniversalColor(red ?? Red, green ?? Green, blue ?? Blue);
		}
	}
}
