using System;

namespace GLTFast.Schema
{
	[Serializable]
	public class MeshPrimitiveExtensions
	{
		public MaterialsVariantsMeshPrimitiveExtension KHR_materials_variants;

		internal void GltfSerialize(JsonWriter writer)
		{
			writer.AddObject();
			if (KHR_materials_variants != null)
			{
				writer.AddProperty("KHR_materials_variants");
				KHR_materials_variants.GltfSerialize(writer);
			}
			writer.Close();
		}
	}
}
