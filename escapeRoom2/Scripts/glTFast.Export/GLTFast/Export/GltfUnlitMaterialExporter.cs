using GLTFast.Logging;
using GLTFast.Materials;
using GLTFast.Schema;
using UnityEngine;

namespace GLTFast.Export
{
	public class GltfUnlitMaterialExporter : IMaterialExport
	{
		public bool ConvertMaterial(UnityEngine.Material unityMaterial, out GLTFast.Schema.Material material, IGltfWritable gltf, ICodeLogger logger)
		{
			gltf.RegisterExtensionUsage(Extension.MaterialsUnlit);
			material = new GLTFast.Schema.Material
			{
				name = unityMaterial.name,
				extensions = new MaterialExtensions
				{
					KHR_materials_unlit = new MaterialUnlit()
				}
			};
			if (GltfMaterialExporter.TryGetValue(unityMaterial, MaterialProperty.Cull, out int value))
			{
				material.doubleSided = value.Equals(0);
			}
			material = HandlePbrMetallicRoughness(gltf, material, unityMaterial);
			return true;
		}

		private static GLTFast.Schema.Material HandlePbrMetallicRoughness(IGltfWritable gltf, GLTFast.Schema.Material material, UnityEngine.Material unityMaterial)
		{
			if (GltfMaterialExporter.TryGetValue(unityMaterial, MaterialProperty.BaseColorTexture, out Texture2D value) && MaterialExport.AddImageExport(gltf, new ImageExport(value), out var textureId))
			{
				TextureInfo baseColorTexture = new TextureInfo
				{
					index = textureId,
					texCoord = GltfMaterialExporter.GetValue(unityMaterial, MaterialProperty.BaseColorTextureTexCoord)
				};
				GLTFast.Schema.Material material2 = material;
				if (material2.pbrMetallicRoughness == null)
				{
					material2.pbrMetallicRoughness = new PbrMetallicRoughness();
				}
				material.pbrMetallicRoughness.baseColorTexture = baseColorTexture;
				if (GltfMaterialExporter.TryCreateTextureTransform(gltf, unityMaterial, MaterialProperty.BaseColorTextureScaleTransform, MaterialProperty.BaseColorTextureRotation, out var result))
				{
					material.pbrMetallicRoughness.baseColorTexture.extensions = new TextureInfoExtensions
					{
						KHR_texture_transform = result
					};
				}
			}
			if (GltfMaterialExporter.TryGetValue(unityMaterial, MaterialProperty.BaseColor, out Color value2) && value2 != Color.white)
			{
				GLTFast.Schema.Material material2 = material;
				if (material2.pbrMetallicRoughness == null)
				{
					material2.pbrMetallicRoughness = new PbrMetallicRoughness();
				}
				material.pbrMetallicRoughness.BaseColor = value2.linear;
			}
			return material;
		}
	}
}
