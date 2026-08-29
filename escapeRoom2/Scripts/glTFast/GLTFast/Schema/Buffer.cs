using System;

namespace GLTFast.Schema
{
	[Serializable]
	public class Buffer : NamedObject
	{
		public uint byteLength;

		public string uri;

		internal void GltfSerialize(JsonWriter writer)
		{
			writer.AddObject();
			if (!string.IsNullOrEmpty(uri))
			{
				writer.AddPropertySafe("uri", uri);
			}
			writer.AddProperty("byteLength", byteLength);
			writer.Close();
		}
	}
}
