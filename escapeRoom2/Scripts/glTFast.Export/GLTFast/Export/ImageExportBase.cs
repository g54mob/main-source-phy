using UnityEngine;
using UnityEngine.Experimental.Rendering;

namespace GLTFast.Export
{
	public abstract class ImageExportBase
	{
		public abstract string FileName { get; }

		public abstract string MimeType { get; }

		public abstract FilterMode FilterMode { get; }

		public abstract TextureWrapMode WrapModeU { get; }

		public abstract TextureWrapMode WrapModeV { get; }

		public int JpgQuality { get; set; } = 60;

		public abstract bool Write(string filePath, bool overwrite);

		public abstract byte[] GetData();

		protected static byte[] EncodeTexture(Texture2D texture, ImageFormat format, int jpgQuality, bool hasAlpha = true, Material blitMaterial = null)
		{
			bool flag = false;
			Texture2D texture2D;
			if (texture.isReadable && blitMaterial == null)
			{
				texture2D = texture;
			}
			else
			{
				RenderTexture temporary = RenderTexture.GetTemporary(texture.width, texture.height, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear, 1, RenderTextureMemoryless.Depth);
				if (blitMaterial == null)
				{
					Graphics.Blit(texture, temporary);
				}
				else
				{
					Graphics.Blit(texture, temporary, blitMaterial);
				}
				texture2D = new Texture2D(texture.width, texture.height, (!hasAlpha && SystemInfo.IsFormatSupported(GraphicsFormat.R8G8B8_UNorm, GraphicsFormatUsage.Sample)) ? GraphicsFormat.R8G8B8_UNorm : GraphicsFormat.R8G8B8A8_UNorm, TextureCreationFlags.DontInitializePixels | TextureCreationFlags.DontUploadUponCreate);
				texture2D.ReadPixels(new Rect(0f, 0f, temporary.width, temporary.height), 0, 0);
				RenderTexture.ReleaseTemporary(temporary);
				texture2D.Apply();
				flag = true;
			}
			byte[] result = ((format == ImageFormat.Png) ? texture2D.EncodeToPNG() : texture2D.EncodeToJPG(jpgQuality));
			if (flag)
			{
				Object.Destroy(texture2D);
			}
			return result;
		}
	}
}
