using System;
using Unity.Mathematics;
using UnityEngine;

namespace GLTFast.Schema
{
	[Serializable]
	public class MaterialSpecular
	{
		public float specularFactor = 1f;

		public TextureInfo specularTexture;

		public float[] specularColorFactor = new float[3] { 1f, 1f, 1f };

		public TextureInfo specularColorTexture;

		public Color SpecularColor
		{
			get
			{
				return new Color(specularColorFactor[0], specularColorFactor[1], specularColorFactor[2]);
			}
			set
			{
				specularColorFactor = new float[3] { value.r, value.g, value.b };
			}
		}

		internal void GltfSerialize(JsonWriter writer)
		{
			writer.AddObject();
			if (math.abs(specularFactor - 1f) > 0.001f)
			{
				writer.AddProperty("specularFactor", specularFactor);
			}
			if (specularTexture != null)
			{
				writer.AddProperty("specularTexture");
				specularTexture.GltfSerialize(writer);
			}
			if (specularColorFactor != null && specularColorFactor.Length > 2 && (math.abs(specularColorFactor[0] - 1f) > 0.001f || math.abs(specularColorFactor[1] - 1f) > 0.001f || math.abs(specularColorFactor[2] - 1f) > 0.001f))
			{
				writer.AddArrayProperty("specularColorFactor", specularColorFactor);
			}
			if (specularColorTexture != null)
			{
				writer.AddProperty("specularColorTexture");
				specularColorTexture.GltfSerialize(writer);
			}
			writer.Close();
		}
	}
}
