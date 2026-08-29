using System;

namespace GLTFast.Schema
{
	[Serializable]
	public abstract class NamedObject
	{
		public string name;

		internal void GltfSerializeName(JsonWriter writer)
		{
			if (!string.IsNullOrEmpty(name))
			{
				writer.AddPropertySafe("name", name);
			}
		}
	}
}
