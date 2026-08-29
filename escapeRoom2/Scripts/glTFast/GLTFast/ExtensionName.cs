namespace GLTFast
{
	public static class ExtensionName
	{
		public const string DracoMeshCompression = "KHR_draco_mesh_compression";

		public const string MaterialsPbrSpecularGlossiness = "KHR_materials_pbrSpecularGlossiness";

		public const string MaterialsTransmission = "KHR_materials_transmission";

		public const string MaterialsUnlit = "KHR_materials_unlit";

		public const string MeshGPUInstancing = "EXT_mesh_gpu_instancing";

		public const string MeshoptCompression = "EXT_meshopt_compression";

		public const string MeshQuantization = "KHR_mesh_quantization";

		public const string TextureBasisUniversal = "KHR_texture_basisu";

		public const string TextureTransform = "KHR_texture_transform";

		public const string LightsPunctual = "KHR_lights_punctual";

		public const string MaterialsClearcoat = "KHR_materials_clearcoat";

		public const string MaterialsIor = "KHR_materials_ior";

		public const string MaterialsSheen = "KHR_materials_sheen";

		public const string MaterialsSpecular = "KHR_materials_specular";

		public const string MaterialsVariants = "KHR_materials_variants";

		public static string GetName(this Extension extension)
		{
			return extension switch
			{
				Extension.DracoMeshCompression => "KHR_draco_mesh_compression", 
				Extension.LightsPunctual => "KHR_lights_punctual", 
				Extension.MaterialsPbrSpecularGlossiness => "KHR_materials_pbrSpecularGlossiness", 
				Extension.MaterialsTransmission => "KHR_materials_transmission", 
				Extension.MaterialsUnlit => "KHR_materials_unlit", 
				Extension.MeshGPUInstancing => "EXT_mesh_gpu_instancing", 
				Extension.MeshQuantization => "KHR_mesh_quantization", 
				Extension.TextureBasisUniversal => "KHR_texture_basisu", 
				Extension.TextureTransform => "KHR_texture_transform", 
				Extension.MaterialsClearcoat => "KHR_materials_clearcoat", 
				Extension.MaterialsVariants => "KHR_materials_variants", 
				Extension.MeshoptCompression => "EXT_meshopt_compression", 
				Extension.MaterialsIor => "KHR_materials_ior", 
				Extension.MaterialsSpecular => "KHR_materials_specular", 
				Extension.MaterialsSheen => "KHR_materials_sheen", 
				_ => null, 
			};
		}
	}
}
