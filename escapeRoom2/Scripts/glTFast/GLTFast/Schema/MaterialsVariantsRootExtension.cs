using System;
using System.Collections.Generic;

namespace GLTFast.Schema
{
	[Serializable]
	public class MaterialsVariantsRootExtension
	{
		public List<MaterialsVariant> variants;

		public bool JsonUtilityCleanup()
		{
			return variants != null;
		}

		internal void GltfSerialize(JsonWriter writer)
		{
			writer.AddObject();
			writer.AddArray("variants");
			foreach (MaterialsVariant variant in variants)
			{
				variant.GltfSerialize(writer);
			}
			writer.CloseArray();
			writer.Close();
		}
	}
}
