using System;
using Unity.Mathematics;
using UnityEngine;

namespace GLTFast.Schema
{
	[Serializable]
	public abstract class PbrMetallicRoughnessBase<TTextureInfo> : PbrMetallicRoughnessBase where TTextureInfo : TextureInfoBase
	{
		public TTextureInfo baseColorTexture;

		public TTextureInfo metallicRoughnessTexture;

		public override TextureInfoBase BaseColorTexture => baseColorTexture;

		public override TextureInfoBase MetallicRoughnessTexture => metallicRoughnessTexture;
	}
	[Serializable]
	public abstract class PbrMetallicRoughnessBase
	{
		public float[] baseColorFactor = new float[4] { 1f, 1f, 1f, 1f };

		public float metallicFactor = 1f;

		public float roughnessFactor = 1f;

		public Color BaseColor
		{
			get
			{
				return new Color(baseColorFactor[0], baseColorFactor[1], baseColorFactor[2], baseColorFactor[3]);
			}
			set
			{
				baseColorFactor = new float[4] { value.r, value.g, value.b, value.a };
			}
		}

		public abstract TextureInfoBase BaseColorTexture { get; }

		public abstract TextureInfoBase MetallicRoughnessTexture { get; }

		internal void GltfSerialize(JsonWriter writer)
		{
			writer.AddObject();
			if (baseColorFactor != null && (math.abs(baseColorFactor[0] - 1f) > 0.001f || math.abs(baseColorFactor[1] - 1f) > 0.001f || math.abs(baseColorFactor[2] - 1f) > 0.001f || math.abs(baseColorFactor[3] - 1f) > 0.001f))
			{
				writer.AddArrayProperty("baseColorFactor", baseColorFactor);
			}
			if (metallicFactor < 1f)
			{
				writer.AddProperty("metallicFactor", metallicFactor);
			}
			if (roughnessFactor < 1f)
			{
				writer.AddProperty("roughnessFactor", roughnessFactor);
			}
			if (BaseColorTexture != null)
			{
				writer.AddProperty("baseColorTexture");
				BaseColorTexture.GltfSerialize(writer);
			}
			if (MetallicRoughnessTexture != null)
			{
				writer.AddProperty("metallicRoughnessTexture");
				MetallicRoughnessTexture.GltfSerialize(writer);
			}
			writer.Close();
		}
	}
}
