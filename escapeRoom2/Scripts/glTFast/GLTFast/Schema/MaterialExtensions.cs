using System;

namespace GLTFast.Schema
{
	[Serializable]
	public class MaterialExtensions
	{
		public PbrSpecularGlossiness KHR_materials_pbrSpecularGlossiness;

		public MaterialUnlit KHR_materials_unlit;

		public Transmission KHR_materials_transmission;

		public ClearCoat KHR_materials_clearcoat;

		public Sheen KHR_materials_sheen;

		public MaterialSpecular KHR_materials_specular;

		public MaterialIor KHR_materials_ior;

		internal void GltfSerialize(JsonWriter writer)
		{
			writer.AddObject();
			if (KHR_materials_pbrSpecularGlossiness != null)
			{
				writer.AddProperty("KHR_materials_pbrSpecularGlossiness");
				KHR_materials_pbrSpecularGlossiness.GltfSerialize(writer);
			}
			if (KHR_materials_unlit != null)
			{
				writer.AddProperty("KHR_materials_unlit");
				KHR_materials_unlit.GltfSerialize(writer);
			}
			if (KHR_materials_transmission != null)
			{
				writer.AddProperty("KHR_materials_transmission");
				KHR_materials_transmission.GltfSerialize(writer);
			}
			if (KHR_materials_clearcoat != null)
			{
				writer.AddProperty("KHR_materials_clearcoat");
				KHR_materials_clearcoat.GltfSerialize(writer);
			}
			if (KHR_materials_sheen != null)
			{
				writer.AddProperty("KHR_materials_sheen");
				KHR_materials_sheen.GltfSerialize(writer);
			}
			if (KHR_materials_specular != null)
			{
				writer.AddProperty("KHR_materials_specular");
				KHR_materials_specular.GltfSerialize(writer);
			}
			if (KHR_materials_ior != null)
			{
				writer.AddProperty("KHR_materials_ior");
				KHR_materials_ior.GltfSerialize(writer);
			}
			writer.Close();
		}
	}
}
