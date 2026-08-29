using UnityEngine;

namespace GLTFast.Export
{
	internal class NormalImageExport : ImageExport
	{
		private static Material s_NormalBlitMaterial;

		protected override ImageFormat ImageFormat
		{
			get
			{
				if (m_ImageFormat != ImageFormat.Unknown)
				{
					return m_ImageFormat;
				}
				return ImageFormat.Png;
			}
		}

		public NormalImageExport(Texture2D texture)
			: base(texture)
		{
		}

		private static Material GetNormalBlitMaterial()
		{
			if (s_NormalBlitMaterial == null)
			{
				s_NormalBlitMaterial = ImageExport.LoadBlitMaterial("glTFExportNormal");
			}
			return s_NormalBlitMaterial;
		}

		protected override bool GenerateTexture(out byte[] imageData)
		{
			if (m_Texture != null)
			{
				imageData = ImageExportBase.EncodeTexture(m_Texture, ImageFormat, base.JpgQuality, hasAlpha: false, GetNormalBlitMaterial());
				return true;
			}
			imageData = null;
			return false;
		}
	}
}
