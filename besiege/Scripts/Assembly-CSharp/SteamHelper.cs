using Steamworks;
using UnityEngine;

public class SteamHelper
{
	public static Texture2D FlipTexture(Texture2D original)
	{
		Texture2D texture2D = new Texture2D(original.width, original.height, original.format, false, true);
		int width = original.width;
		int height = original.height;
		for (int i = 0; i < width; i++)
		{
			for (int j = 0; j < height; j++)
			{
				texture2D.SetPixel(i, height - j - 1, original.GetPixel(i, j));
			}
		}
		return texture2D;
	}

	public static Texture2D GetSteamImageAsTexture2D(int iImage)
	{
		Texture2D texture2D = null;
		uint pnWidth;
		uint pnHeight;
		if (SteamUtils.GetImageSize(iImage, out pnWidth, out pnHeight))
		{
			byte[] array = new byte[pnWidth * pnHeight * 4];
			if (SteamUtils.GetImageRGBA(iImage, array, (int)(pnWidth * pnHeight * 4)))
			{
				texture2D = new Texture2D((int)pnWidth, (int)pnHeight, TextureFormat.RGBA32, false, true);
				texture2D.LoadRawTextureData(array);
				texture2D = FlipTexture(texture2D);
				texture2D.Apply();
			}
		}
		return texture2D;
	}
}
