using System;

namespace GLTFast.Schema
{
	[Serializable]
	public class TextureExtensions
	{
		public TextureBasisUniversal KHR_texture_basisu;

		internal void GltfSerialize(JsonWriter writer)
		{
			throw new NotImplementedException($"GltfSerialize missing on {GetType()}");
		}
	}
}
