using GLTFast.Materials;
using GLTFast.Schema;
using UnityEngine;

namespace GLTFast.Export
{
	public class GltfHdrpMaterialExporter : GltfShaderGraphMaterialExporter
	{
		protected override bool IsDoubleSided(UnityEngine.Material material)
		{
			if (GltfMaterialExporter.TryGetValue(material, MaterialProperty.DoubleSidedEnable, out int value))
			{
				return value != 0;
			}
			return false;
		}

		protected override MaterialBase.AlphaMode GetAlphaMode(UnityEngine.Material material)
		{
			if (GltfMaterialExporter.TryGetValue(material, MaterialProperty.AlphaCutoffEnable, out int value) && value == 1)
			{
				return MaterialBase.AlphaMode.Mask;
			}
			if (GltfMaterialExporter.TryGetValue(material, MaterialProperty.SurfaceType, out int value2))
			{
				if (value2 != 0)
				{
					return MaterialBase.AlphaMode.Blend;
				}
				return MaterialBase.AlphaMode.Opaque;
			}
			return MaterialBase.AlphaMode.Opaque;
		}
	}
}
