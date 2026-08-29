using System;

namespace GLTFast.Schema
{
	[Serializable]
	public class MeshExtras
	{
		public string[] targetNames;

		internal void GltfSerialize(JsonWriter writer)
		{
			if (targetNames != null)
			{
				writer.AddArrayPropertySafe("targetNames", targetNames);
			}
		}
	}
}
