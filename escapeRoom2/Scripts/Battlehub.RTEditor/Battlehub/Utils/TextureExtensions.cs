using UnityEngine;

namespace Battlehub.Utils
{
	public static class TextureExtensions
	{
		public static bool IsReadable(this Texture2D texture)
		{
			if (texture == null)
			{
				return false;
			}
			try
			{
				texture.GetPixel(0, 0);
				return true;
			}
			catch (UnityException)
			{
				return false;
			}
		}

		public static Texture2D DeCompress(this Texture2D source)
		{
			RenderTexture temporary = RenderTexture.GetTemporary(source.width, source.height, 0, RenderTextureFormat.Default, RenderTextureReadWrite.sRGB);
			Graphics.Blit(source, temporary);
			RenderTexture active = RenderTexture.active;
			RenderTexture.active = temporary;
			Texture2D texture2D = new Texture2D(source.width, source.height);
			texture2D.ReadPixels(new Rect(0f, 0f, temporary.width, temporary.height), 0, 0);
			texture2D.Apply();
			RenderTexture.active = active;
			RenderTexture.ReleaseTemporary(temporary);
			return texture2D;
		}

		public static RenderTexture ConvertToARGB32(this RenderTexture self)
		{
			if (self.format == RenderTextureFormat.ARGB32)
			{
				return self;
			}
			RenderTexture temporary = RenderTexture.GetTemporary(self.width, self.height, 0, RenderTextureFormat.ARGB32);
			Graphics.Blit(self, temporary);
			return temporary;
		}
	}
}
