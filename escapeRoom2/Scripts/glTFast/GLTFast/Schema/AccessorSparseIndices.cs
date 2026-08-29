using System;

namespace GLTFast.Schema
{
	[Serializable]
	public class AccessorSparseIndices
	{
		public uint bufferView;

		public int byteOffset;

		public GltfComponentType componentType;

		internal void GltfSerialize(JsonWriter writer)
		{
			writer.AddObject();
			writer.AddProperty("bufferView", bufferView);
			writer.AddProperty("componentType", componentType);
			if (byteOffset >= 0)
			{
				writer.AddProperty("byteOffset", byteOffset);
			}
			writer.Close();
		}
	}
}
