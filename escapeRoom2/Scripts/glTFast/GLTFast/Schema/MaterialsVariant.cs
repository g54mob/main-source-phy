using System;

namespace GLTFast.Schema
{
	[Serializable]
	public class MaterialsVariant : NamedObject
	{
		internal void GltfSerialize(JsonWriter writer)
		{
			GltfSerializeName(writer);
		}
	}
}
