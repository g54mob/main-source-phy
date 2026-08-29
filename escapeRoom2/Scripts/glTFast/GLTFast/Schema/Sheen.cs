using System;
using Unity.Mathematics;
using UnityEngine;

namespace GLTFast.Schema
{
	[Serializable]
	public class Sheen
	{
		public float[] sheenColorFactor = new float[3] { 1f, 1f, 1f };

		public TextureInfo sheenColorTexture;

		public float sheenRoughnessFactor;

		public TextureInfo sheenRoughnessTexture;

		public Color SheenColor
		{
			get
			{
				return new Color(sheenColorFactor[0], sheenColorFactor[1], sheenColorFactor[2]);
			}
			set
			{
				sheenColorFactor = new float[3] { value.r, value.g, value.b };
			}
		}

		internal void GltfSerialize(JsonWriter writer)
		{
			writer.AddObject();
			if (sheenColorFactor != null && sheenColorFactor.Length > 2 && (math.abs(sheenColorFactor[0] - 1f) > 0.001f || math.abs(sheenColorFactor[1] - 1f) > 0.001f || math.abs(sheenColorFactor[2] - 1f) > 0.001f))
			{
				writer.AddArrayProperty("sheenColorFactor", sheenColorFactor);
			}
			if (sheenColorTexture != null)
			{
				writer.AddProperty("sheenColorTexture");
				sheenColorTexture.GltfSerialize(writer);
			}
			if (sheenRoughnessFactor > 0f)
			{
				writer.AddProperty("sheenRoughnessFactor", sheenRoughnessFactor);
			}
			if (sheenRoughnessTexture != null)
			{
				writer.AddProperty("sheenRoughnessTexture");
				sheenRoughnessTexture.GltfSerialize(writer);
			}
			writer.Close();
		}
	}
}
