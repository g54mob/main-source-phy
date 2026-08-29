using System;

namespace GLTFast.Schema
{
	[Serializable]
	public class NodeLightsPunctual
	{
		public int light = -1;

		internal void GltfSerialize(JsonWriter writer)
		{
			writer.AddObject();
			if (light >= 0)
			{
				writer.AddProperty("light", light);
			}
			writer.Close();
		}
	}
}
