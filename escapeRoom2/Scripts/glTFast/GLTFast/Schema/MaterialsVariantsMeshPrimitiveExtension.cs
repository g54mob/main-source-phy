using System;
using System.Collections.Generic;

namespace GLTFast.Schema
{
	[Serializable]
	public class MaterialsVariantsMeshPrimitiveExtension
	{
		public List<MaterialVariantsMapping> mappings;

		public bool TryGetMaterialIndex(int variantIndex, out int materialIndex)
		{
			foreach (MaterialVariantsMapping mapping in mappings)
			{
				int[] variants = mapping.variants;
				foreach (int num in variants)
				{
					if (variantIndex == num)
					{
						materialIndex = mapping.material;
						return true;
					}
				}
			}
			materialIndex = -1;
			return false;
		}

		internal void GltfSerialize(JsonWriter writer)
		{
			writer.AddObject();
			writer.AddArray("mappings");
			foreach (MaterialVariantsMapping mapping in mappings)
			{
				mapping.GltfSerialize(writer);
			}
			writer.CloseArray();
			writer.Close();
		}
	}
}
