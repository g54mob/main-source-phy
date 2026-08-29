using GLTFast.Logging;
using GLTFast.Schema;
using UnityEngine;

namespace GLTFast.Materials
{
	public interface IMaterialGenerator
	{
		UnityEngine.Material GetDefaultMaterial(bool pointsSupport = false);

		UnityEngine.Material GenerateMaterial(MaterialBase gltfMaterial, IGltfReadable gltf, bool pointsSupport = false);

		void SetLogger(ICodeLogger logger);
	}
}
