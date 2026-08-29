using GLTFast.Logging;
using GLTFast.Schema;
using UnityEngine;

namespace GLTFast.Export
{
	internal class MetaMaterialExportShaderGraphs<TLitExport, TGltfShaderGraphExport> : IMaterialExport where TLitExport : IMaterialExport, new() where TGltfShaderGraphExport : IMaterialExport, new()
	{
		private static TLitExport s_LitMaterialExport;

		private static TGltfShaderGraphExport s_GltfShaderGraphMaterialExport;

		public static MetaMaterialExportShaderGraphs<TLitExport, TGltfShaderGraphExport> Instance { get; } = new MetaMaterialExportShaderGraphs<TLitExport, TGltfShaderGraphExport>();

		private MetaMaterialExportShaderGraphs()
		{
		}

		public bool ConvertMaterial(UnityEngine.Material uMaterial, out GLTFast.Schema.Material material, IGltfWritable gltf, ICodeLogger logger)
		{
			string name = uMaterial.shader.name;
			IMaterialExport materialExport;
			if (name.StartsWith("Shader Graphs/glTF-"))
			{
				if (!MetaMaterialExportBuiltIn.TryFindMatchingGltfUnlitMaterialExport(name, out materialExport))
				{
					TGltfShaderGraphExport val = s_GltfShaderGraphMaterialExport;
					if (val == null)
					{
						s_GltfShaderGraphMaterialExport = new TGltfShaderGraphExport();
					}
					materialExport = s_GltfShaderGraphMaterialExport;
				}
			}
			else if (!MetaMaterialExportBuiltIn.TryFindMatchingGltfMaterialExport(name, out materialExport))
			{
				TLitExport val2 = s_LitMaterialExport;
				if (val2 == null)
				{
					s_LitMaterialExport = new TLitExport();
				}
				materialExport = s_LitMaterialExport;
			}
			return materialExport.ConvertMaterial(uMaterial, out material, gltf, logger);
		}
	}
}
