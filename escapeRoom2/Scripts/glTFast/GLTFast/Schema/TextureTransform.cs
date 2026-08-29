using System;
using Unity.Mathematics;

namespace GLTFast.Schema
{
	[Serializable]
	public class TextureTransform
	{
		public float[] offset = new float[2];

		public float rotation;

		public float[] scale = new float[2] { 1f, 1f };

		public int texCoord = -1;

		internal void GltfSerialize(JsonWriter writer)
		{
			writer.AddObject();
			if (offset != null)
			{
				writer.AddArrayProperty("offset", offset);
			}
			if (scale != null)
			{
				writer.AddArrayProperty("scale", scale);
			}
			if (math.abs(rotation) >= float.Epsilon)
			{
				writer.AddProperty("rotation", rotation);
			}
			if (texCoord >= 0)
			{
				writer.AddProperty("texCoord", texCoord);
			}
			writer.Close();
		}
	}
}
