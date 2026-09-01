namespace Poly.Extension
{
	public static class SimpleTypesExtension
	{
		public static int ToInt(this bool b)
		{
			if (!b)
			{
				return 0;
			}
			return 1;
		}

		public static float ToFloat(this bool b)
		{
			if (!b)
			{
				return 0f;
			}
			return 1f;
		}
	}
}
