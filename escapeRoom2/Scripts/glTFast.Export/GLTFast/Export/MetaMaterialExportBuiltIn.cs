using GLTFast.Logging;
using GLTFast.Schema;
using UnityEngine;

namespace GLTFast.Export
{
	internal class MetaMaterialExportBuiltIn : IMaterialExport
	{
		private static IMaterialExport s_LitMaterialExport;

		private static IMaterialExport s_GltfBuiltInMaterialExport;

		private static IMaterialExport s_GltfUnlitMaterialExport;

		public static IMaterialExport Instance { get; } = new MetaMaterialExportBuiltIn();

		private MetaMaterialExportBuiltIn()
		{
		}

		public bool ConvertMaterial(UnityEngine.Material uMaterial, out GLTFast.Schema.Material material, IGltfWritable gltf, ICodeLogger logger)
		{
			return FindMatchingMaterialExport(uMaterial.shader.name).ConvertMaterial(uMaterial, out material, gltf, logger);
		}

		private static IMaterialExport FindMatchingMaterialExport(string shaderName)
		{
			if (!TryFindMatchingGltfMaterialExport(shaderName, out var materialExport))
			{
				if (s_LitMaterialExport == null)
				{
					s_LitMaterialExport = new StandardMaterialExport();
				}
				return s_LitMaterialExport;
			}
			return materialExport;
		}

		internal static bool TryFindMatchingGltfMaterialExport(string shaderName, out IMaterialExport materialExport)
		{
			if (shaderName.StartsWith("glTF/"))
			{
				if (TryFindMatchingGltfUnlitMaterialExport(shaderName, out materialExport))
				{
					return true;
				}
				if (s_GltfBuiltInMaterialExport == null)
				{
					s_GltfBuiltInMaterialExport = new GltfBuiltInShaderMaterialExporter();
				}
				materialExport = s_GltfBuiltInMaterialExport;
				return true;
			}
			materialExport = null;
			return false;
		}

		internal static bool TryFindMatchingGltfUnlitMaterialExport(string shaderName, out IMaterialExport materialExport)
		{
			if (shaderName.LastIndexOf("nlit") >= 0)
			{
				if (s_GltfUnlitMaterialExport == null)
				{
					s_GltfUnlitMaterialExport = new GltfUnlitMaterialExporter();
				}
				materialExport = s_GltfUnlitMaterialExport;
				return true;
			}
			materialExport = null;
			return false;
		}
	}
}
