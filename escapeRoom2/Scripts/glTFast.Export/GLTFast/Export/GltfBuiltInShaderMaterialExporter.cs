using GLTFast.Materials;
using GLTFast.Schema;
using UnityEngine;

namespace GLTFast.Export
{
	public class GltfBuiltInShaderMaterialExporter : GltfMaterialExporter
	{
		protected override MaterialBase.AlphaMode GetAlphaMode(UnityEngine.Material material)
		{
			if (GltfMaterialExporter.TryGetValue(material, MaterialProperty.Mode, out int value))
			{
				switch ((StandardShaderMode)value)
				{
				case StandardShaderMode.Cutout:
					return MaterialBase.AlphaMode.Mask;
				case StandardShaderMode.Fade:
				case StandardShaderMode.Transparent:
					return MaterialBase.AlphaMode.Blend;
				}
			}
			return MaterialBase.AlphaMode.Opaque;
		}

		protected override float GetAlphaCutoff(UnityEngine.Material material)
		{
			return material.GetFloat(MaterialProperty.AlphaCutoff);
		}

		protected override bool IsDoubleSided(UnityEngine.Material material)
		{
			if (GltfMaterialExporter.TryGetValue(material, MaterialProperty.CullMode, out int value))
			{
				return value == 0;
			}
			return false;
		}
	}
}
