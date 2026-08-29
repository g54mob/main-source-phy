using GLTFast.Schema;

namespace GLTFast
{
	internal static class RootExtension
	{
		internal static bool IsASkeletonMissing(this RootBase root)
		{
			if (root.Skins != null)
			{
				foreach (Skin skin in root.Skins)
				{
					if (skin.skeleton < 0)
					{
						return true;
					}
				}
			}
			return false;
		}
	}
}
