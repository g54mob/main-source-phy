using System;
using System.IO;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

namespace GLTFast.Export
{
	internal class ImageExport : ImageExportBase
	{
		private static Material s_ColorBlitMaterial;

		protected readonly Texture2D m_Texture;

		protected ImageFormat m_ImageFormat;

		protected virtual ImageFormat ImageFormat
		{
			get
			{
				if (m_ImageFormat != ImageFormat.Unknown)
				{
					return m_ImageFormat;
				}
				if (!HasAlpha(m_Texture))
				{
					return ImageFormat.Jpg;
				}
				return ImageFormat.Png;
			}
		}

		public override string FileName
		{
			get
			{
				string text = m_Texture.name;
				if (string.IsNullOrEmpty(text))
				{
					text = "texture";
				}
				return text + "." + FileExtension;
			}
		}

		public override FilterMode FilterMode
		{
			get
			{
				if (!(m_Texture != null))
				{
					return FilterMode.Bilinear;
				}
				return m_Texture.filterMode;
			}
		}

		public override TextureWrapMode WrapModeU
		{
			get
			{
				if (!(m_Texture != null))
				{
					return TextureWrapMode.Repeat;
				}
				return m_Texture.wrapModeU;
			}
		}

		public override TextureWrapMode WrapModeV
		{
			get
			{
				if (!(m_Texture != null))
				{
					return TextureWrapMode.Repeat;
				}
				return m_Texture.wrapModeV;
			}
		}

		public override string MimeType => ImageFormat switch
		{
			ImageFormat.Jpg => "image/jpeg", 
			ImageFormat.Png => "image/png", 
			_ => throw new ArgumentOutOfRangeException(), 
		};

		protected string FileExtension => ImageFormat switch
		{
			ImageFormat.Jpg => "jpg", 
			ImageFormat.Png => "png", 
			_ => throw new ArgumentOutOfRangeException(), 
		};

		public ImageExport(Texture2D texture, ImageFormat imageFormat = ImageFormat.Unknown)
		{
			m_Texture = texture;
			m_ImageFormat = imageFormat;
		}

		protected virtual bool GenerateTexture(out byte[] imageData)
		{
			if (m_Texture != null)
			{
				imageData = ImageExportBase.EncodeTexture(m_Texture, ImageFormat, base.JpgQuality, hasAlpha: true, GetColorBlitMaterial());
				return imageData != null;
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

		public override byte[] GetData()
		{
			GenerateTexture(out var imageData);
			return imageData;
		}

		public override int GetHashCode()
		{
			int num = 13;
			if (m_Texture != null)
			{
				num = num * 7 + m_Texture.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			if (obj == null || GetType() != obj.GetType())
			{
				return false;
			}
			return Equals((ImageExport)obj);
		}

		private bool Equals(ImageExport other)
		{
			return m_Texture == other.m_Texture;
		}

		protected static Material LoadBlitMaterial(string shaderName)
		{
			Shader shader = Shader.Find("Hidden/" + shaderName);
			if (shader == null)
			{
				Debug.LogError("Missing Shader " + shaderName);
				return null;
			}
			return new Material(shader);
		}

		private static bool HasAlpha(Texture2D texture)
		{
			return GraphicsFormatUtility.HasAlphaChannel(GraphicsFormatUtility.GetGraphicsFormat(texture.format, isSRGB: false));
		}

		private static Material GetColorBlitMaterial()
		{
			if (s_ColorBlitMaterial == null)
			{
				s_ColorBlitMaterial = LoadBlitMaterial("glTFExportColor");
			}
			return s_ColorBlitMaterial;
		}
	}
}
