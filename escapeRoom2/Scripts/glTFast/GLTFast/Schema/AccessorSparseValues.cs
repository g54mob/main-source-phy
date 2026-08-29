using System;

namespace GLTFast.Schema
{
	[Serializable]
	public class AccessorSparseValues
	{
		public uint bufferView;

		public int byteOffset;

		internal void GltfSerialize(JsonWriter writer)
		{
			writer.AddObject();
			writer.AddProperty("bufferView", bufferView);
			if (byteOffset >= 0)
			{
				writer.AddProperty("byteOffset", byteOffset);
			}
			writer.Close();
		}
	}
}
