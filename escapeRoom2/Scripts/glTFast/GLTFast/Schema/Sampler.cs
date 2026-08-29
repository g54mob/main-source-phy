using System;
using UnityEngine;

namespace GLTFast.Schema
{
	[Serializable]
	public class Sampler : NamedObject
	{
		public enum MagFilterMode
		{
			None = 0,
			Nearest = 9728,
			Linear = 9729
		}

		public enum MinFilterMode
		{
			None = 0,
			Nearest = 9728,
			Linear = 9729,
			NearestMipmapNearest = 9984,
			LinearMipmapNearest = 9985,
			NearestMipmapLinear = 9986,
			LinearMipmapLinear = 9987
		}

		public enum WrapMode
		{
			None = 0,
			ClampToEdge = 33071,
			MirroredRepeat = 33648,
			Repeat = 10497
		}

		public MagFilterMode magFilter;

		public MinFilterMode minFilter;

		public WrapMode wrapS = WrapMode.Repeat;

		public WrapMode wrapT = WrapMode.Repeat;

		public FilterMode FilterMode => ConvertFilterMode(minFilter, magFilter);

		public TextureWrapMode WrapU => ConvertWrapMode(wrapS);

		public TextureWrapMode WrapV => ConvertWrapMode(wrapT);

		private static FilterMode ConvertFilterMode(MinFilterMode minFilterToConvert, MagFilterMode magFilterToConvert)
		{
			switch (minFilterToConvert)
			{
			case MinFilterMode.LinearMipmapLinear:
				return FilterMode.Trilinear;
			case MinFilterMode.Nearest:
			case MinFilterMode.NearestMipmapNearest:
			case MinFilterMode.NearestMipmapLinear:
				return FilterMode.Point;
			default:
				if (magFilterToConvert == MagFilterMode.Nearest)
				{
					return FilterMode.Point;
				}
				return FilterMode.Bilinear;
			}
		}

		private static TextureWrapMode ConvertWrapMode(WrapMode wrapMode)
		{
			return wrapMode switch
			{
				WrapMode.ClampToEdge => TextureWrapMode.Clamp, 
				WrapMode.MirroredRepeat => TextureWrapMode.Mirror, 
				_ => TextureWrapMode.Repeat, 
			};
		}

		private static WrapMode ConvertWrapMode(TextureWrapMode wrapMode)
		{
			switch (wrapMode)
			{
			case TextureWrapMode.Clamp:
				return WrapMode.ClampToEdge;
			case TextureWrapMode.Mirror:
			case TextureWrapMode.MirrorOnce:
				return WrapMode.MirroredRepeat;
			default:
				return WrapMode.Repeat;
			}
		}

		public Sampler()
		{
		}

		public Sampler(FilterMode filterMode, TextureWrapMode wrapModeU, TextureWrapMode wrapModeV)
		{
			switch (filterMode)
			{
			case FilterMode.Point:
				magFilter = MagFilterMode.Nearest;
				minFilter = MinFilterMode.Nearest;
				break;
			case FilterMode.Bilinear:
				magFilter = MagFilterMode.Linear;
				minFilter = MinFilterMode.Linear;
				break;
			case FilterMode.Trilinear:
				magFilter = MagFilterMode.Linear;
				minFilter = MinFilterMode.LinearMipmapLinear;
				break;
			}
			wrapS = ConvertWrapMode(wrapModeU);
			wrapT = ConvertWrapMode(wrapModeV);
		}

		public void Apply(Texture2D image, MinFilterMode defaultMinFilter = MinFilterMode.Linear, MagFilterMode defaultMagFilter = MagFilterMode.Linear)
		{
			if (!(image == null))
			{
				image.wrapModeU = WrapU;
				image.wrapModeV = WrapV;
				image.filterMode = ConvertFilterMode((minFilter == MinFilterMode.None) ? defaultMinFilter : minFilter, (magFilter == MagFilterMode.None) ? defaultMagFilter : magFilter);
			}
		}

		internal void GltfSerialize(JsonWriter writer)
		{
			writer.AddObject();
			GltfSerializeName(writer);
			if (magFilter == MagFilterMode.Nearest)
			{
				writer.AddProperty("magFilter", (int)magFilter);
			}
			if (minFilter != MinFilterMode.None && minFilter != MinFilterMode.Linear)
			{
				writer.AddProperty("minFilter", (int)minFilter);
			}
			if (wrapS != WrapMode.None && wrapS != WrapMode.Repeat)
			{
				writer.AddProperty("wrapS", (int)wrapS);
			}
			if (wrapT != WrapMode.None && wrapT != WrapMode.Repeat)
			{
				writer.AddProperty("wrapT", (int)wrapT);
			}
			writer.Close();
		}
	}
}
