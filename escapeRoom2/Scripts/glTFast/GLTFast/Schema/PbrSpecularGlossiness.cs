using System;
using UnityEngine;

namespace GLTFast.Schema
{
	[Serializable]
	public class PbrSpecularGlossiness
	{
		public float[] diffuseFactor = new float[4] { 1f, 1f, 1f, 1f };

		public TextureInfo diffuseTexture;

		public float[] specularFactor = new float[3] { 1f, 1f, 1f };

		public float glossinessFactor = 1f;

		public TextureInfo specularGlossinessTexture;

		public Color DiffuseColor => new Color(diffuseFactor[0], diffuseFactor[1], diffuseFactor[2], diffuseFactor[3]);

		public Color SpecularColor => new Color(specularFactor[0], specularFactor[1], specularFactor[2]);

		internal void GltfSerialize(JsonWriter writer)
		{
			writer.AddObject();
			writer.Close();
			throw new NotImplementedException($"GltfSerialize missing on {GetType()}");
		}
	}
}
