using System;

namespace GLTFast.Schema
{
	[Serializable]
	public class Image : NamedObject
	{
		public string uri;

		public string mimeType;

		public int bufferView = -1;

		internal void GltfSerialize(JsonWriter writer)
		{
			writer.AddObject();
			GltfSerializeName(writer);
			if (!string.IsNullOrEmpty(uri))
			{
				writer.AddPropertySafe("uri", uri);
			}
			if (!string.IsNullOrEmpty(mimeType))
			{
				writer.AddProperty("mimeType", mimeType);
			}
			if (bufferView >= 0)
			{
				writer.AddProperty("bufferView", bufferView);
			}
			writer.Close();
		}
	}
}
