using System.IO;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

namespace GLTFast.Export
{
	internal class OrmImageExport : ImageExport
	{
		private static Material s_MetalGlossBlitMaterial;

		private static Material s_OcclusionBlitMaterial;

		private static Material s_GlossBlitMaterial;

		private readonly Texture2D m_OccTexture;

		private readonly Texture2D m_SmoothnessTexture;

		public override string FileName
		{
			get
			{
				if (m_Texture != null)
				{
					return base.FileName;
				}
				if (m_OccTexture != null && !string.IsNullOrEmpty(m_OccTexture.name))
				{
					return m_OccTexture.name + "." + base.FileExtension;
				}
				if (m_SmoothnessTexture != null && !string.IsNullOrEmpty(m_SmoothnessTexture.name))
				{
					return m_SmoothnessTexture.name + "ORM." + base.FileExtension;
				}
				return "ORM." + base.FileExtension;
			}
		}

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

		public override FilterMode FilterMode
		{
			get
			{
				if (m_Texture != null)
				{
					return m_Texture.filterMode;
				}
				if (m_OccTexture != null)
				{
					return m_OccTexture.filterMode;
				}
				if (m_SmoothnessTexture != null)
				{
					return m_SmoothnessTexture.filterMode;
				}
				return FilterMode.Bilinear;
			}
		}

		public override TextureWrapMode WrapModeU
		{
			get
			{
				if (m_Texture != null)
				{
					return m_Texture.wrapModeU;
				}
				if (m_OccTexture != null)
				{
					return m_OccTexture.wrapModeU;
				}
				if (m_SmoothnessTexture != null)
				{
					return m_SmoothnessTexture.wrapModeU;
				}
				return TextureWrapMode.Repeat;
			}
		}

		public override TextureWrapMode WrapModeV
		{
			get
			{
				if (m_Texture != null)
				{
					return m_Texture.wrapModeV;
				}
				if (m_OccTexture != null)
				{
					return m_OccTexture.wrapModeV;
				}
				if (m_SmoothnessTexture != null)
				{
					return m_SmoothnessTexture.wrapModeV;
				}
				return TextureWrapMode.Repeat;
			}
		}

		public bool HasOcclusion => m_OccTexture != null;

		public OrmImageExport(Texture2D metalGlossTexture = null, Texture2D occlusionTexture = null, Texture2D smoothnessTexture = null, ImageFormat imageFormat = ImageFormat.Unknown)
			: base(metalGlossTexture, imageFormat)
		{
			m_OccTexture = occlusionTexture;
			m_SmoothnessTexture = smoothnessTexture;
		}

		private static Material GetMetalGlossBlitMaterial()
		{
			if (s_MetalGlossBlitMaterial == null)
			{
				s_MetalGlossBlitMaterial = ImageExport.LoadBlitMaterial("glTFExportMetalGloss");
			}
			return s_MetalGlossBlitMaterial;
		}

		private static Material GetOcclusionBlitMaterial()
		{
			if (s_OcclusionBlitMaterial == null)
			{
				s_OcclusionBlitMaterial = ImageExport.LoadBlitMaterial("glTFExportOcclusion");
			}
			return s_OcclusionBlitMaterial;
		}

		private static Material GetGlossBlitMaterial()
		{
			if (s_GlossBlitMaterial == null)
			{
				s_GlossBlitMaterial = ImageExport.LoadBlitMaterial("glTFExportSmoothness");
			}
			return s_GlossBlitMaterial;
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

		protected override bool GenerateTexture(out byte[] imageData)
		{
			if (m_Texture != null || m_OccTexture != null || m_SmoothnessTexture != null)
			{
				imageData = EncodeOrmTexture(m_Texture, m_OccTexture, m_SmoothnessTexture, ImageFormat, base.JpgQuality);
				return true;
			}
			imageData = null;
			return false;
		}

		private static byte[] EncodeOrmTexture(Texture2D metalGlossTexture, Texture2D occlusionTexture, Texture2D smoothnessTexture, ImageFormat format, int jpgQuality)
		{
			Material metalGlossBlitMaterial = GetMetalGlossBlitMaterial();
			int num = int.MinValue;
			int num2 = int.MinValue;
			if (metalGlossTexture != null)
			{
				num = math.max(num, metalGlossTexture.width);
				num2 = math.max(num2, metalGlossTexture.height);
			}
			if (occlusionTexture != null)
			{
				num = math.max(num, occlusionTexture.width);
				num2 = math.max(num2, occlusionTexture.height);
			}
			if (smoothnessTexture != null)
			{
				num = math.max(num, smoothnessTexture.width);
				num2 = math.max(num2, smoothnessTexture.height);
			}
			RenderTexture temporary = RenderTexture.GetTemporary(num, num2, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear, 1, RenderTextureMemoryless.Depth);
			if (metalGlossTexture == null)
			{
				RenderTexture active = RenderTexture.active;
				RenderTexture.active = temporary;
				GL.Clear(clearDepth: true, clearColor: true, Color.white);
				RenderTexture.active = active;
			}
			else
			{
				Graphics.Blit(metalGlossTexture, temporary, metalGlossBlitMaterial);
			}
			if (occlusionTexture != null)
			{
				metalGlossBlitMaterial = GetOcclusionBlitMaterial();
				Graphics.Blit(occlusionTexture, temporary, metalGlossBlitMaterial);
			}
			if (smoothnessTexture != null)
			{
				metalGlossBlitMaterial = GetGlossBlitMaterial();
				Graphics.Blit(smoothnessTexture, temporary, metalGlossBlitMaterial);
			}
			Texture2D texture2D = new Texture2D(num, num2, SystemInfo.IsFormatSupported(GraphicsFormat.R8G8B8_UNorm, GraphicsFormatUsage.Sample) ? GraphicsFormat.R8G8B8_UNorm : GraphicsFormat.R8G8B8A8_UNorm, TextureCreationFlags.DontInitializePixels | TextureCreationFlags.DontUploadUponCreate);
			texture2D.ReadPixels(new Rect(0f, 0f, temporary.width, temporary.height), 0, 0);
			RenderTexture.ReleaseTemporary(temporary);
			texture2D.Apply();
			byte[] result = ((format == ImageFormat.Png) ? texture2D.EncodeToPNG() : texture2D.EncodeToJPG(jpgQuality));
			Object.Destroy(texture2D);
			return result;
		}

		public override int GetHashCode()
		{
			int num = 14;
			if (m_Texture != null)
			{
				num = num * 7 + m_Texture.GetHashCode();
			}
			if (m_OccTexture != null)
			{
				num = num * 7 + m_OccTexture.GetHashCode();
			}
			if (m_SmoothnessTexture != null)
			{
				num = num * 7 + m_SmoothnessTexture.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			if (obj == null || GetType() != obj.GetType())
			{
				return false;
			}
			return Equals((OrmImageExport)obj);
		}

		private bool Equals(OrmImageExport other)
		{
			if (m_Texture == other.m_Texture && m_OccTexture == other.m_OccTexture)
			{
				return m_SmoothnessTexture == other.m_SmoothnessTexture;
			}
			return false;
		}
	}
}
