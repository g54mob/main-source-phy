using System;

namespace GLTFast.Schema
{
	[Serializable]
	public class MaterialVariantsMapping
	{
		public int material;

		public int[] variants;

		internal void GltfSerialize(JsonWriter writer)
		{
			writer.AddObject();
			writer.AddProperty("material", material);
			writer.AddArrayProperty("variants", variants);
			writer.Close();
		}
	}
}
