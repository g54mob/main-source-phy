using System;

namespace GLTFast.Schema
{
	[Serializable]
	public class MaterialUnlit
	{
		internal void GltfSerialize(JsonWriter writer)
		{
			writer.AddObject();
			writer.Close();
		}
	}
}
