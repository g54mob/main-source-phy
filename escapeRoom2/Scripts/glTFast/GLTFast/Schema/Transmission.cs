using System;

namespace GLTFast.Schema
{
	[Serializable]
	public class Transmission
	{
		public float transmissionFactor;

		public TextureInfo transmissionTexture;

		internal void GltfSerialize(JsonWriter writer)
		{
			writer.AddObject();
			writer.Close();
			throw new NotImplementedException($"GltfSerialize missing on {GetType()}");
		}
	}
}
