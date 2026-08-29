using GLTFast.Logging;
using GLTFast.Schema;
using UnityEngine;

namespace GLTFast.Export
{
	public interface IMaterialExport
	{
		bool ConvertMaterial(UnityEngine.Material uMaterial, out GLTFast.Schema.Material material, IGltfWritable gltf, ICodeLogger logger);
	}
}
