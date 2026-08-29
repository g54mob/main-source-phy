using System;

namespace GLTFast.Schema
{
	[Serializable]
	public class ClearCoat
	{
		public float clearcoatFactor;

		public TextureInfo clearcoatTexture;

		public float clearcoatRoughnessFactor;

		public TextureInfo clearcoatRoughnessTexture;

		public NormalTextureInfo clearcoatNormalTexture;

		internal void GltfSerialize(JsonWriter writer)
		{
			writer.AddObject();
			if (clearcoatFactor > 0f)
			{
				writer.AddProperty("clearcoatFactor", clearcoatFactor);
			}
			if (clearcoatTexture != null)
			{
				writer.AddProperty("clearcoatTexture");
				clearcoatTexture.GltfSerialize(writer);
			}
			if (clearcoatRoughnessFactor > 0f)
			{
				writer.AddProperty("clearcoatRoughnessFactor", clearcoatRoughnessFactor);
			}
			if (clearcoatRoughnessTexture != null)
			{
				writer.AddProperty("clearcoatRoughnessTexture");
				clearcoatRoughnessTexture.GltfSerialize(writer);
			}
			if (clearcoatNormalTexture != null)
			{
				writer.AddProperty("clearcoatNormalTexture");
				clearcoatNormalTexture.GltfSerialize(writer);
			}
			writer.Close();
		}
	}
}
