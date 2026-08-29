using System.IO;
using UnityEngine;

namespace GLTFast.Export
{
	internal class MaskMapImageExport : ImageExport
	{
		private static Material s_BlitMaterial;

		protected override ImageFormat ImageFormat
		{
			get
			{
				if (m_ImageFormat == ImageFormat.Unknown)
				{
					return ImageFormat.Jpg;
				}
				return m_ImageFormat;
			}
		}

		public MaskMapImageExport(Texture2D maskMap = null, ImageFormat imageFormat = ImageFormat.Unknown)
			: base(maskMap, imageFormat)
		{
		}

		private static Material GetMaskMapBlitMaterial()
		{
			if (s_BlitMaterial == null)
			{
				s_BlitMaterial = ImageExport.LoadBlitMaterial("glTFExportMaskMap");
			}
			return s_BlitMaterial;
		}

		protected override bool GenerateTexture(out byte[] imageData)
		{
			if (m_Texture != null)
			{
				imageData = ImageExportBase.EncodeTexture(m_Texture, ImageFormat, base.JpgQuality, hasAlpha: false, GetMaskMapBlitMaterial());
				return true;
			}
			imageData = null;
			return false;
		}

		public override bool Write(string filePath, bool overwrite)
		{
			if (GenerateTexture(out var imageData))
			{
				File.WriteAllBytes(filePath, imageData);
				return true;
			}
			return false;
		}
	}
}
