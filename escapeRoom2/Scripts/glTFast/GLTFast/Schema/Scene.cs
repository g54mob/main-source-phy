using System;

namespace GLTFast.Schema
{
	[Serializable]
	public class Scene : NamedObject
	{
		public uint[] nodes;

		internal void GltfSerialize(JsonWriter writer)
		{
			writer.AddObject();
			GltfSerializeName(writer);
			writer.AddArrayProperty("nodes", nodes);
			writer.Close();
		}
	}
}
