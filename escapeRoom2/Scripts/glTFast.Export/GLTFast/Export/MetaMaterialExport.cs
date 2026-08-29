using System;
using GLTFast.Logging;
using GLTFast.Schema;
using UnityEngine;

namespace GLTFast.Export
{
	[Obsolete("Use MaterialExport.GetDefaultMaterialExport instead.")]
	public class MetaMaterialExport<TLitExport, TGltfShaderGraphExport> : IMaterialExport where TLitExport : IMaterialExport, new() where TGltfShaderGraphExport : IMaterialExport, new()
	{
		public bool ConvertMaterial(UnityEngine.Material uMaterial, out GLTFast.Schema.Material material, IGltfWritable gltf, ICodeLogger logger)
		{
			return MetaMaterialExportShaderGraphs<TLitExport, TGltfShaderGraphExport>.Instance.ConvertMaterial(uMaterial, out material, gltf, logger);
		}
	}
}
