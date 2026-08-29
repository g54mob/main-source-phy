using System;

namespace GLTFast.Schema
{
	[Serializable]
	public class RootExtensions
	{
		public LightsPunctual KHR_lights_punctual;

		public MaterialsVariantsRootExtension KHR_materials_variants;

		internal void GltfSerialize(JsonWriter writer)
		{
			writer.AddObject();
			if (KHR_lights_punctual != null)
			{
				writer.AddProperty("KHR_lights_punctual");
				KHR_lights_punctual.GltfSerialize(writer);
			}
			if (KHR_materials_variants != null)
			{
				writer.AddProperty("KHR_materials_variants");
				KHR_materials_variants.GltfSerialize(writer);
			}
			writer.Close();
		}

		public virtual bool JsonUtilityCleanup()
		{
			if (KHR_lights_punctual != null && !KHR_lights_punctual.JsonUtilityCleanup())
			{
				KHR_lights_punctual = null;
			}
			if (KHR_materials_variants != null && !KHR_materials_variants.JsonUtilityCleanup())
			{
				KHR_materials_variants = null;
			}
			if (KHR_lights_punctual == null)
			{
				return KHR_materials_variants != null;
			}
			return true;
		}
	}
}
