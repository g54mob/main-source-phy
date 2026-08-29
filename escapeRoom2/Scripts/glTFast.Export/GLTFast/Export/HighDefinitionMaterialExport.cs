using GLTFast.Logging;
using GLTFast.Materials;
using GLTFast.Schema;
using Unity.Mathematics;
using UnityEngine;

namespace GLTFast.Export
{
	public class HighDefinitionMaterialExport : MaterialExportBase
	{
		private const string k_KeywordNormalMapTangentSpace = "_NORMALMAP_TANGENT_SPACE";

		private const string k_KeywordMaskMap = "_MASKMAP";

		private static readonly int k_AORemapMax = Shader.PropertyToID("_AORemapMax");

		private static readonly int k_AORemapMin = Shader.PropertyToID("_AORemapMin");

		private static readonly int k_EmissiveColor = Shader.PropertyToID("_EmissiveColor");

		private static readonly int k_EmissionColorMap = Shader.PropertyToID("_EmissiveColorMap");

		private static readonly int k_NormalMap = Shader.PropertyToID("_NormalMap");

		private static readonly int k_NormalScale = Shader.PropertyToID("_NormalScale");

		private static readonly int k_BaseColorMap = Shader.PropertyToID("_BaseColorMap");

		private static readonly int k_MaskMap = Shader.PropertyToID("_MaskMap");

		private static readonly int k_SmoothnessRemapMax = Shader.PropertyToID("_SmoothnessRemapMax");

		private static readonly int k_SmoothnessRemapMin = Shader.PropertyToID("_SmoothnessRemapMin");

		private static readonly int k_UnlitColor = Shader.PropertyToID("_UnlitColor");

		private static readonly int k_CoatMask = Shader.PropertyToID("_CoatMask");

		private static readonly int k_CoatMaskMap = Shader.PropertyToID("_CoatMaskMap");

		public override bool ConvertMaterial(UnityEngine.Material uMaterial, out GLTFast.Schema.Material material, IGltfWritable gltf, ICodeLogger logger)
		{
			bool flag = true;
			material = new GLTFast.Schema.Material
			{
				name = uMaterial.name,
				pbrMetallicRoughness = new PbrMetallicRoughness
				{
					metallicFactor = 0f,
					roughnessFactor = 1f
				}
			};
			MaterialExportBase.SetAlphaModeAndCutoff(uMaterial, material);
			material.doubleSided = MaterialExportBase.IsDoubleSided(uMaterial, MaterialProperty.CullMode);
			if (uMaterial.HasProperty(k_EmissiveColor))
			{
				Color color = uMaterial.GetColor(k_EmissiveColor);
				float num = math.max(color.r, math.max(color.g, color.b));
				if (num > 1f)
				{
					color.r /= num;
					color.g /= num;
					color.b /= num;
				}
				material.Emissive = color;
			}
			if (uMaterial.HasProperty(k_EmissionColorMap))
			{
				UnityEngine.Texture texture = uMaterial.GetTexture(k_EmissionColorMap);
				if (texture != null)
				{
					if (texture is Texture2D)
					{
						material.emissiveTexture = MaterialExportBase.ExportTextureInfo(texture, gltf);
						MaterialExportBase.ExportTextureTransform(material.emissiveTexture, uMaterial, k_EmissionColorMap, gltf);
					}
					else
					{
						logger?.Error(LogCode.TextureInvalidType, "emission", material.name);
						flag = false;
					}
				}
			}
			if (uMaterial.HasProperty(k_NormalMap) && uMaterial.IsKeywordEnabled("_NORMALMAP_TANGENT_SPACE"))
			{
				UnityEngine.Texture texture2 = uMaterial.GetTexture(k_NormalMap);
				if (texture2 != null)
				{
					if (texture2 is Texture2D)
					{
						material.normalTexture = MaterialExportBase.ExportNormalTextureInfo(texture2, uMaterial, gltf, k_NormalScale);
						MaterialExportBase.ExportTextureTransform(material.normalTexture, uMaterial, k_NormalMap, gltf);
					}
					else
					{
						logger?.Error(LogCode.TextureInvalidType, "normal", uMaterial.name);
						flag = false;
					}
				}
			}
			if (uMaterial.HasProperty(k_CoatMask) && uMaterial.GetFloat(k_CoatMask) > 0f)
			{
				gltf.RegisterExtensionUsage(Extension.MaterialsClearcoat);
				material.extensions = material.extensions ?? new MaterialExtensions();
				material.extensions.KHR_materials_clearcoat = new ClearCoat();
				material.extensions.KHR_materials_clearcoat.clearcoatFactor = uMaterial.GetFloat(k_CoatMask);
				if (uMaterial.HasProperty(k_CoatMaskMap))
				{
					UnityEngine.Texture texture3 = uMaterial.GetTexture(k_CoatMaskMap);
					if (texture3 != null)
					{
						if (texture3 is Texture2D)
						{
							material.extensions.KHR_materials_clearcoat.clearcoatTexture = MaterialExportBase.ExportTextureInfo(texture3, gltf);
							MaterialExportBase.ExportTextureTransform(material.extensions.KHR_materials_clearcoat.clearcoatTexture, uMaterial, k_CoatMaskMap, gltf);
						}
						else
						{
							logger?.Error(LogCode.TextureInvalidType, "clearcoat", material.name);
							flag = false;
						}
					}
				}
			}
			int mainTexProperty = (uMaterial.HasProperty(k_BaseColorMap) ? k_BaseColorMap : MaterialExportBase.MainTexProperty);
			if (MaterialExportBase.IsUnlit(uMaterial))
			{
				ExportUnlit(material, uMaterial, mainTexProperty, gltf, logger);
			}
			else
			{
				flag &= ExportPbrMetallicRoughness(uMaterial, material, gltf, logger);
			}
			return flag;
		}

		private static bool ExportPbrMetallicRoughness(UnityEngine.Material uMaterial, GLTFast.Schema.Material material, IGltfWritable gltf, ICodeLogger logger)
		{
			bool result = true;
			PbrMetallicRoughness pbrMetallicRoughness = new PbrMetallicRoughness
			{
				metallicFactor = 0f,
				roughnessFactor = 1f
			};
			bool flag = false;
			if (uMaterial.HasProperty(MaterialExportBase.MetallicProperty))
			{
				pbrMetallicRoughness.metallicFactor = uMaterial.GetFloat(MaterialExportBase.MetallicProperty);
				flag = pbrMetallicRoughness.metallicFactor > 0f;
			}
			if (uMaterial.HasProperty(k_BaseColorMap))
			{
				UnityEngine.Texture texture = uMaterial.GetTexture(k_BaseColorMap);
				if ((bool)texture)
				{
					if (texture is Texture2D)
					{
						pbrMetallicRoughness.baseColorTexture = MaterialExportBase.ExportTextureInfo(texture, gltf, (material.GetAlphaMode() == MaterialBase.AlphaMode.Opaque) ? ImageFormat.Jpg : ImageFormat.Unknown);
						MaterialExportBase.ExportTextureTransform(pbrMetallicRoughness.baseColorTexture, uMaterial, k_BaseColorMap, gltf);
					}
					else
					{
						logger?.Error(LogCode.TextureInvalidType, "main", uMaterial.name);
						result = false;
					}
				}
			}
			MaskMapImageExport maskMapImageExport = null;
			if (uMaterial.IsKeywordEnabled("_MASKMAP") && uMaterial.HasProperty(k_MaskMap))
			{
				Texture2D texture2D = uMaterial.GetTexture(k_MaskMap) as Texture2D;
				if (texture2D != null)
				{
					bool flag2 = false;
					if (uMaterial.HasProperty(k_SmoothnessRemapMin))
					{
						float num = uMaterial.GetFloat(k_SmoothnessRemapMin);
						pbrMetallicRoughness.roughnessFactor = 1f - num;
						if (uMaterial.HasProperty(k_SmoothnessRemapMax))
						{
							float num2 = uMaterial.GetFloat(k_SmoothnessRemapMax);
							flag2 = math.abs(num - num2) > 1.1920929E-07f;
							if (num2 < 1f && flag2)
							{
								logger?.Warning(LogCode.RemapUnsupported, "Smoothness");
							}
						}
					}
					float num3 = 1f;
					if (uMaterial.HasProperty(k_AORemapMin))
					{
						float num4 = uMaterial.GetFloat(k_AORemapMin);
						num3 = math.clamp(1f - num4, 0f, 1f);
						if (uMaterial.HasProperty(k_AORemapMax) && uMaterial.GetFloat(k_AORemapMax) < 1f && num3 > 0f)
						{
							logger?.Warning(LogCode.RemapUnsupported, "AO");
						}
					}
					bool flag3 = num3 > 0f;
					if (flag || flag3 || flag2)
					{
						maskMapImageExport = new MaskMapImageExport(texture2D);
						if (MaterialExport.AddImageExport(gltf, maskMapImageExport, out var textureId))
						{
							if (flag || flag2)
							{
								pbrMetallicRoughness.metallicRoughnessTexture = new TextureInfo
								{
									index = textureId
								};
								MaterialExportBase.ExportTextureTransform(pbrMetallicRoughness.metallicRoughnessTexture, uMaterial, k_MaskMap, gltf);
							}
							if (num3 > 0f)
							{
								material.occlusionTexture = new OcclusionTextureInfo
								{
									index = textureId,
									strength = num3
								};
								MaterialExportBase.ExportTextureTransform(material.occlusionTexture, uMaterial, k_BaseColorMap, gltf);
							}
						}
					}
				}
			}
			if (uMaterial.HasProperty(MaterialExportBase.BaseColorProperty))
			{
				pbrMetallicRoughness.BaseColor = uMaterial.GetColor(MaterialExportBase.BaseColorProperty).linear;
			}
			else if (uMaterial.HasProperty(MaterialExportBase.ColorProperty))
			{
				pbrMetallicRoughness.BaseColor = uMaterial.GetColor(MaterialExportBase.ColorProperty).linear;
			}
			if (maskMapImageExport == null && uMaterial.HasProperty(MaterialExportBase.SmoothnessProperty))
			{
				pbrMetallicRoughness.roughnessFactor = 1f - uMaterial.GetFloat(MaterialExportBase.SmoothnessProperty);
			}
			material.pbrMetallicRoughness = pbrMetallicRoughness;
			return result;
		}

		protected override bool GetUnlitColor(UnityEngine.Material uMaterial, out Color baseColor)
		{
			if (uMaterial.HasProperty(k_UnlitColor))
			{
				baseColor = uMaterial.GetColor(k_UnlitColor);
				return true;
			}
			return base.GetUnlitColor(uMaterial, out baseColor);
		}
	}
}
