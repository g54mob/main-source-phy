using System;

namespace GLTFast.Schema
{
	[Serializable]
	public class Material : MaterialBase<MaterialExtensions, NormalTextureInfo, OcclusionTextureInfo, PbrMetallicRoughness, TextureInfo, TextureInfoExtensions>
	{
	}
}
