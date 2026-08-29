using GLTFast.Schema;

namespace GLTFast.Export
{
	internal static class TextureComparer
	{
		public static bool Equals(TextureBase x, TextureBase y)
		{
			if (x == y)
			{
				return true;
			}
			if (x == null)
			{
				return false;
			}
			if (y == null)
			{
				return false;
			}
			if (x.sampler == y.sampler)
			{
				return x.source == y.source;
			}
			return false;
		}
	}
}
