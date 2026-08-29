using System;

namespace GLTFast.Schema
{
	[Serializable]
	public class TextureInfoExtensions
	{
		public TextureTransform KHR_texture_transform;

		internal void GltfSerialize(JsonWriter writer)
		{
			if (KHR_texture_transform != null)
			{
				writer.AddObject();
				writer.AddProperty("KHR_texture_transform");
				KHR_texture_transform.GltfSerialize(writer);
				writer.Close();
			}
		}
	}
}
