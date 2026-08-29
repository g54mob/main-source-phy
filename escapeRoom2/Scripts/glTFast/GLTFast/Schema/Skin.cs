using System;

namespace GLTFast.Schema
{
	[Serializable]
	public class Skin : NamedObject
	{
		public int inverseBindMatrices = -1;

		public int skeleton = -1;

		public uint[] joints;

		internal void GltfSerialize(JsonWriter writer)
		{
			writer.AddObject();
			GltfSerializeName(writer);
			if (inverseBindMatrices != -1)
			{
				writer.AddProperty("inverseBindMatrices", inverseBindMatrices);
			}
			if (skeleton != -1)
			{
				writer.AddProperty("skeleton", skeleton);
			}
			if (joints != null)
			{
				writer.AddArrayProperty("joints", joints);
			}
			writer.Close();
		}
	}
}
