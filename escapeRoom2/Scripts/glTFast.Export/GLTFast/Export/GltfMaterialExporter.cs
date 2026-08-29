using GLTFast.Logging;
using GLTFast.Materials;
using GLTFast.Schema;
using Unity.Mathematics;
using UnityEngine;

namespace GLTFast.Export
{
	public abstract class GltfMaterialExporter : MaterialExportBase
	{
		public override bool ConvertMaterial(UnityEngine.Material unityMaterial, out GLTFast.Schema.Material material, IGltfWritable gltf, ICodeLogger logger)
		{
			material = new GLTFast.Schema.Material
			{
				name = unityMaterial.name,
				pbrMetallicRoughness = new PbrMetallicRoughness(),
				doubleSided = IsDoubleSided(unityMaterial)
			};
			MaterialBase.AlphaMode alphaMode = GetAlphaMode(unityMaterial);
			material.SetAlphaMode(alphaMode);
			if (alphaMode == MaterialBase.AlphaMode.Mask)
			{
				material.alphaCutoff = GetAlphaCutoff(unityMaterial);
			}
			material = HandlePbrMetallicRoughness(gltf, material, unityMaterial);
			material = HandleNormal(gltf, material, unityMaterial);
			material = HandleOcclusion(gltf, material, unityMaterial);
			material = HandleEmission(gltf, material, unityMaterial);
			return true;
		}

		protected abstract MaterialBase.AlphaMode GetAlphaMode(UnityEngine.Material material);

		protected abstract float GetAlphaCutoff(UnityEngine.Material material);

		protected abstract bool IsDoubleSided(UnityEngine.Material material);

		private static GLTFast.Schema.Material HandlePbrMetallicRoughness(IGltfWritable gltf, GLTFast.Schema.Material material, UnityEngine.Material unityMaterial)
		{
			if (TryGetValue(unityMaterial, MaterialProperty.BaseColorTexture, out Texture2D value) && MaterialExport.AddImageExport(gltf, new ImageExport(value), out var textureId))
			{
				TextureInfo baseColorTexture = new TextureInfo
				{
					index = textureId,
					texCoord = GetValue(unityMaterial, MaterialProperty.BaseColorTextureTexCoord)
				};
				material.pbrMetallicRoughness.baseColorTexture = baseColorTexture;
				if (TryCreateTextureTransform(gltf, unityMaterial, MaterialProperty.BaseColorTextureScaleTransform, MaterialProperty.BaseColorTextureRotation, out var result))
				{
					material.pbrMetallicRoughness.baseColorTexture.extensions = new TextureInfoExtensions
					{
						KHR_texture_transform = result
					};
				}
			}
			if (TryGetValue(unityMaterial, MaterialProperty.BaseColor, out Color value2))
			{
				material.pbrMetallicRoughness.BaseColor = value2.linear;
			}
			material = HandleMetallicRoughness(gltf, material, unityMaterial);
			return material;
		}

		private static GLTFast.Schema.Material HandleMetallicRoughness(IGltfWritable gltf, GLTFast.Schema.Material material, UnityEngine.Material unityMaterial)
		{
			if (TryGetValue(unityMaterial, MaterialProperty.MetallicRoughnessMap, out Texture2D value) && MaterialExport.AddImageExport(gltf, new ImageExport(value), out var textureId))
			{
				TextureInfo textureInfo = new TextureInfo
				{
					index = textureId,
					texCoord = GetValue(unityMaterial, MaterialProperty.MetallicRoughnessMapTexCoord)
				};
				if (TryCreateTextureTransform(gltf, unityMaterial, MaterialProperty.MetallicRoughnessMapScaleTransform, MaterialProperty.MetallicRoughnessMapRotation, out var result))
				{
					textureInfo.extensions = new TextureInfoExtensions
					{
						KHR_texture_transform = result
					};
				}
				material.pbrMetallicRoughness.metallicRoughnessTexture = textureInfo;
			}
			if (TryGetValue(unityMaterial, MaterialProperty.Metallic, out float value2))
			{
				material.pbrMetallicRoughness.metallicFactor = value2;
			}
			if (TryGetValue(unityMaterial, MaterialProperty.RoughnessFactor, out float value3))
			{
				material.pbrMetallicRoughness.roughnessFactor = value3;
			}
			return material;
		}

		private static GLTFast.Schema.Material HandleNormal(IGltfWritable gltf, GLTFast.Schema.Material material, UnityEngine.Material unityMaterial)
		{
			if (!TryGetValue(unityMaterial, MaterialProperty.NormalTexture, out Texture2D value))
			{
				return material;
			}
			if (!MaterialExport.AddImageExport(gltf, new NormalImageExport(value), out var textureId))
			{
				return material;
			}
			TryGetValue(unityMaterial, MaterialProperty.NormalTextureScale, out float value2);
			NormalTextureInfo normalTexture = new NormalTextureInfo
			{
				index = textureId,
				texCoord = GetValue(unityMaterial, MaterialProperty.NormalTextureTexCoord),
				scale = value2
			};
			material.normalTexture = normalTexture;
			if (TryCreateTextureTransform(gltf, unityMaterial, MaterialProperty.NormalTextureScaleTransform, MaterialProperty.NormalTextureRotation, out var result))
			{
				material.normalTexture.extensions = new TextureInfoExtensions
				{
					KHR_texture_transform = result
				};
			}
			return material;
		}

		private static GLTFast.Schema.Material HandleOcclusion(IGltfWritable gltf, GLTFast.Schema.Material material, UnityEngine.Material unityMaterial)
		{
			if (!TryGetValue(unityMaterial, MaterialProperty.OcclusionTexture, out Texture2D value))
			{
				return material;
			}
			if (!MaterialExport.AddImageExport(gltf, new ImageExport(value), out var textureId))
			{
				return material;
			}
			TryGetValue(unityMaterial, MaterialProperty.OcclusionTextureStrength, out float value2);
			OcclusionTextureInfo occlusionTexture = new OcclusionTextureInfo
			{
				index = textureId,
				texCoord = GetValue(unityMaterial, MaterialProperty.OcclusionTextureTexCoord),
				strength = value2
			};
			material.occlusionTexture = occlusionTexture;
			if (TryCreateTextureTransform(gltf, unityMaterial, MaterialProperty.OcclusionTextureScaleTransform, MaterialProperty.OcclusionTextureRotation, out var result))
			{
				material.occlusionTexture.extensions = new TextureInfoExtensions
				{
					KHR_texture_transform = result
				};
			}
			return material;
		}

		private static GLTFast.Schema.Material HandleEmission(IGltfWritable gltf, GLTFast.Schema.Material material, UnityEngine.Material unityMaterial)
		{
			if (TryGetValue(unityMaterial, MaterialProperty.EmissiveTexture, out Texture2D value) && MaterialExport.AddImageExport(gltf, new ImageExport(value), out var textureId))
			{
				TextureInfo emissiveTexture = new TextureInfo
				{
					index = textureId,
					texCoord = GetValue(unityMaterial, MaterialProperty.EmissiveTextureTexCoord)
				};
				material.emissiveTexture = emissiveTexture;
				if (TryCreateTextureTransform(gltf, unityMaterial, MaterialProperty.EmissiveTextureScaleTransform, MaterialProperty.EmissiveTextureRotation, out var result))
				{
					material.emissiveTexture.extensions = new TextureInfoExtensions
					{
						KHR_texture_transform = result
					};
				}
			}
			if (TryGetValue(unityMaterial, MaterialProperty.EmissiveFactor, out Color value2))
			{
				material.Emissive = value2;
			}
			return material;
		}

		internal static bool TryCreateTextureTransform(IGltfWritable gltf, UnityEngine.Material uMaterial, int scaleTransformPropertyId, int rotationPropertyId, out TextureTransform result)
		{
			result = null;
			if (!uMaterial.IsKeywordEnabled("_TEXTURE_TRANSFORM"))
			{
				return false;
			}
			Vector4 vector = uMaterial.GetVector(scaleTransformPropertyId);
			Vector4 vector2 = uMaterial.GetVector(rotationPropertyId);
			if (math.abs(vector.z) >= float.Epsilon || math.abs(vector.w) >= float.Epsilon)
			{
				if (result == null)
				{
					result = new TextureTransform();
				}
				result.offset = new float[2] { vector.z, vector.w };
			}
			UvTransform uvTransform = UvTransform.FromMatrix(new float2x2(vector.x, vector.y, vector2.x, vector2.y));
			if (math.abs(uvTransform.rotation) > float.Epsilon)
			{
				if (result == null)
				{
					result = new TextureTransform();
				}
				result.rotation = uvTransform.rotation;
			}
			if (math.abs(uvTransform.scale.x - 1f) > 1.1920929E-07f || math.abs(uvTransform.scale.y - 1f) > 1.1920929E-07f)
			{
				if (result == null)
				{
					result = new TextureTransform();
				}
				result.scale = new float[2]
				{
					uvTransform.scale[0],
					uvTransform.scale[1]
				};
			}
			if (result != null)
			{
				gltf.RegisterExtensionUsage(Extension.TextureTransform);
				return true;
			}
			return false;
		}

		internal static bool TryGetValue(UnityEngine.Material material, int propertyId, out float value)
		{
			if (!material.HasProperty(propertyId))
			{
				value = 0f;
				return false;
			}
			value = material.GetFloat(propertyId);
			return true;
		}

		internal static bool TryGetValue(UnityEngine.Material material, int propertyId, out int value)
		{
			if (TryGetValue(material, propertyId, out float value2))
			{
				value = (int)value2;
				return true;
			}
			value = 0;
			return false;
		}

		internal static int GetValue(UnityEngine.Material material, int propertyId)
		{
			if (TryGetValue(material, propertyId, out int value))
			{
				return value;
			}
			return 0;
		}

		internal static bool TryGetValue(UnityEngine.Material material, int propertyId, out Color value)
		{
			if (!material.HasProperty(propertyId))
			{
				value = default(Color);
				return false;
			}
			value = material.GetColor(propertyId);
			return true;
		}

		internal static bool TryGetValue(UnityEngine.Material material, int propertyId, out Texture2D value)
		{
			if (!material.HasProperty(propertyId))
			{
				value = null;
				return false;
			}
			value = (Texture2D)material.GetTexture(propertyId);
			return (object)value != null;
		}
	}
}
