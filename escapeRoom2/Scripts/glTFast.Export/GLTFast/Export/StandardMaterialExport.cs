using GLTFast.Logging;
using GLTFast.Materials;
using GLTFast.Schema;
using Unity.Mathematics;
using UnityEngine;

namespace GLTFast.Export
{
	public class StandardMaterialExport : MaterialExportBase
	{
		private const string k_KeywordBumpMap = "_BUMPMAP";

		private const string k_KeywordEmission = "_EMISSION";

		private const string k_KeywordMetallicGlossMap = "_METALLICGLOSSMAP";

		private const string k_KeywordMetallicSpecGlossMap = "_METALLICSPECGLOSSMAP";

		private const string k_KeywordSmoothnessTextureAlbedoChannelA = "_SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A";

		private static readonly int k_EmissionColor = Shader.PropertyToID("_EmissionColor");

		private static readonly int k_EmissionMap = Shader.PropertyToID("_EmissionMap");

		private static readonly int k_BumpMap = Shader.PropertyToID("_BumpMap");

		private static readonly int k_BumpScale = Shader.PropertyToID("_BumpScale");

		private static readonly int k_OcclusionMap = Shader.PropertyToID("_OcclusionMap");

		private static readonly int k_OcclusionStrength = Shader.PropertyToID("_OcclusionStrength");

		private static readonly int k_BaseMap = Shader.PropertyToID("_BaseMap");

		private static readonly int k_ColorTexture = Shader.PropertyToID("_ColorTexture");

		private static readonly int k_TintColor = Shader.PropertyToID("_TintColor");

		private static readonly int k_MetallicGlossMap = Shader.PropertyToID("_MetallicGlossMap");

		private static readonly int k_Glossiness = Shader.PropertyToID("_Glossiness");

		private static readonly int k_GlossMapScale = Shader.PropertyToID("_GlossMapScale");

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
			material.doubleSided = MaterialExportBase.IsDoubleSided(uMaterial, MaterialProperty.Cull);
			if (uMaterial.IsKeywordEnabled("_EMISSION"))
			{
				if (uMaterial.HasProperty(k_EmissionColor))
				{
					Color color = uMaterial.GetColor(k_EmissionColor);
					float num = math.max(color.r, math.max(color.g, color.b));
					if (num > 1f)
					{
						color.r /= num;
						color.g /= num;
						color.b /= num;
					}
					material.Emissive = color;
				}
				if (uMaterial.HasProperty(k_EmissionMap))
				{
					UnityEngine.Texture texture = uMaterial.GetTexture(k_EmissionMap);
					if (texture != null)
					{
						if (texture is Texture2D)
						{
							material.emissiveTexture = MaterialExportBase.ExportTextureInfo(texture, gltf);
							if (material.emissiveTexture != null)
							{
								MaterialExportBase.ExportTextureTransform(material.emissiveTexture, uMaterial, k_EmissionMap, gltf);
							}
						}
						else
						{
							logger?.Error(LogCode.TextureInvalidType, "emission", material.name);
							flag = false;
						}
					}
				}
			}
			if (uMaterial.HasProperty(k_BumpMap) && (uMaterial.IsKeywordEnabled("_NORMALMAP") || uMaterial.IsKeywordEnabled("_BUMPMAP")))
			{
				UnityEngine.Texture texture2 = uMaterial.GetTexture(k_BumpMap);
				if (texture2 != null)
				{
					if (texture2 is Texture2D)
					{
						material.normalTexture = MaterialExportBase.ExportNormalTextureInfo(texture2, uMaterial, gltf, k_BumpScale);
						if (material.normalTexture != null)
						{
							MaterialExportBase.ExportTextureTransform(material.normalTexture, uMaterial, k_BumpMap, gltf);
						}
					}
					else
					{
						logger?.Error(LogCode.TextureInvalidType, "normal", uMaterial.name);
						flag = false;
					}
				}
			}
			bool flag2 = IsPbrMetallicRoughness(uMaterial);
			bool flag3 = flag2 && (HasMetallicGlossMap(uMaterial) || uMaterial.IsKeywordEnabled("_SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A"));
			Texture2D occlusionTexture = null;
			Texture2D metalGlossTexture = null;
			Texture2D smoothnessTexture = null;
			int mainTexProperty = MaterialExportBase.MainTexProperty;
			if (uMaterial.HasProperty(k_BaseMap))
			{
				mainTexProperty = k_BaseMap;
			}
			else if (uMaterial.HasProperty(k_ColorTexture))
			{
				mainTexProperty = k_ColorTexture;
			}
			if (MaterialExportBase.IsUnlit(uMaterial))
			{
				ExportUnlit(material, uMaterial, mainTexProperty, gltf, logger);
			}
			else if (flag2)
			{
				flag &= ExportPbrMetallicRoughness(uMaterial, material, mainTexProperty, gltf, logger, out metalGlossTexture, out smoothnessTexture);
			}
			else if (uMaterial.HasProperty(mainTexProperty))
			{
				UnityEngine.Texture texture3 = uMaterial.GetTexture(mainTexProperty);
				material.pbrMetallicRoughness = new PbrMetallicRoughness
				{
					metallicFactor = 0f,
					roughnessFactor = 1f,
					BaseColor = (uMaterial.HasProperty(MaterialExportBase.BaseColorProperty) ? uMaterial.GetColor(MaterialExportBase.BaseColorProperty).linear : Color.white)
				};
				if (texture3 != null)
				{
					material.pbrMetallicRoughness.baseColorTexture = MaterialExportBase.ExportTextureInfo(texture3, gltf);
					if (material.pbrMetallicRoughness.baseColorTexture != null)
					{
						MaterialExportBase.ExportTextureTransform(material.pbrMetallicRoughness.baseColorTexture, uMaterial, mainTexProperty, gltf);
					}
				}
				if (uMaterial.HasProperty(k_TintColor))
				{
					material.pbrMetallicRoughness.BaseColor = uMaterial.GetColor(k_TintColor).linear;
				}
			}
			if (uMaterial.HasProperty(k_OcclusionMap))
			{
				UnityEngine.Texture texture4 = uMaterial.GetTexture(k_OcclusionMap);
				if (texture4 != null)
				{
					if (texture4 is Texture2D texture2D)
					{
						if (!flag3)
						{
							material.occlusionTexture = ExportOcclusionTextureInfo(texture2D, gltf);
						}
						else
						{
							material.occlusionTexture = new OcclusionTextureInfo();
							occlusionTexture = texture2D;
						}
						if (material.occlusionTexture != null)
						{
							MaterialExportBase.ExportTextureTransform(material.occlusionTexture, uMaterial, mainTexProperty, gltf);
						}
					}
					else
					{
						logger?.Error(LogCode.TextureInvalidType, "occlusion", material.name);
						flag = false;
					}
				}
			}
			if (flag3 && material.pbrMetallicRoughness != null)
			{
				OrmImageExport ormImageExport = new OrmImageExport(metalGlossTexture, occlusionTexture, smoothnessTexture);
				if (MaterialExport.AddImageExport(gltf, ormImageExport, out var textureId))
				{
					if (material.pbrMetallicRoughness.MetallicRoughnessTexture != null)
					{
						material.PbrMetallicRoughness.MetallicRoughnessTexture.index = textureId;
						MaterialExportBase.ExportTextureTransform(material.PbrMetallicRoughness.MetallicRoughnessTexture, uMaterial, k_MetallicGlossMap, gltf);
					}
					if (ormImageExport.HasOcclusion)
					{
						material.occlusionTexture.index = textureId;
					}
				}
				else
				{
					logger?.Error(LogCode.ExportImageFailed);
				}
			}
			if (material.occlusionTexture != null && uMaterial.HasProperty(k_OcclusionStrength))
			{
				material.occlusionTexture.strength = uMaterial.GetFloat(k_OcclusionStrength);
			}
			return flag;
		}

		private static bool IsPbrMetallicRoughness(UnityEngine.Material material)
		{
			if (material.HasProperty(MaterialExportBase.MetallicProperty))
			{
				if (!HasMetallicGlossMap(material) && !material.HasProperty(k_Glossiness))
				{
					return material.HasProperty(MaterialExportBase.SmoothnessProperty);
				}
				return true;
			}
			return false;
		}

		private static bool ExportPbrMetallicRoughness(UnityEngine.Material uMaterial, GLTFast.Schema.Material material, int mainTexProperty, IGltfWritable gltf, ICodeLogger logger, out Texture2D metalGlossTexture, out Texture2D smoothnessTexture)
		{
			metalGlossTexture = null;
			smoothnessTexture = null;
			bool result = true;
			PbrMetallicRoughness pbrMetallicRoughness = new PbrMetallicRoughness
			{
				metallicFactor = 0f,
				roughnessFactor = 1f
			};
			bool flag = uMaterial.IsKeywordEnabled("_SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A");
			if (uMaterial.HasProperty(MaterialExportBase.BaseColorProperty))
			{
				pbrMetallicRoughness.BaseColor = uMaterial.GetColor(MaterialExportBase.BaseColorProperty).linear;
			}
			else if (uMaterial.HasProperty(MaterialExportBase.ColorProperty))
			{
				pbrMetallicRoughness.BaseColor = uMaterial.GetColor(MaterialExportBase.ColorProperty).linear;
			}
			if (uMaterial.HasProperty(k_TintColor))
			{
				float num = 1f;
				if (uMaterial.HasProperty(MaterialExportBase.ColorProperty))
				{
					Color color = uMaterial.GetColor(MaterialExportBase.ColorProperty);
					num = (color.r + color.g + color.b) / 3f;
				}
				pbrMetallicRoughness.BaseColor = (uMaterial.GetColor(k_TintColor) * num).linear;
			}
			if (uMaterial.HasProperty(mainTexProperty))
			{
				UnityEngine.Texture texture = uMaterial.GetTexture(mainTexProperty);
				if ((bool)texture)
				{
					if (texture is Texture2D)
					{
						pbrMetallicRoughness.baseColorTexture = MaterialExportBase.ExportTextureInfo(texture, gltf, flag ? ImageFormat.Jpg : ImageFormat.Unknown);
						if (pbrMetallicRoughness.BaseColorTexture != null)
						{
							MaterialExportBase.ExportTextureTransform(pbrMetallicRoughness.BaseColorTexture, uMaterial, mainTexProperty, gltf);
						}
					}
					else
					{
						logger?.Error(LogCode.TextureInvalidType, "main", uMaterial.name);
						result = false;
					}
				}
			}
			if (uMaterial.HasProperty(MaterialExportBase.MetallicProperty) && !HasMetallicGlossMap(uMaterial))
			{
				pbrMetallicRoughness.metallicFactor = uMaterial.GetFloat(MaterialExportBase.MetallicProperty);
			}
			if (uMaterial.HasProperty(k_Glossiness) || uMaterial.HasProperty(MaterialExportBase.SmoothnessProperty))
			{
				int nameID = (uMaterial.HasProperty(MaterialExportBase.SmoothnessProperty) ? MaterialExportBase.SmoothnessProperty : k_Glossiness);
				UnityEngine.Texture texture2 = (uMaterial.HasProperty(k_MetallicGlossMap) ? uMaterial.GetTexture(k_MetallicGlossMap) : null);
				float num2 = uMaterial.GetFloat(nameID);
				pbrMetallicRoughness.roughnessFactor = (((texture2 != null || flag) && uMaterial.HasProperty(k_GlossMapScale)) ? uMaterial.GetFloat(k_GlossMapScale) : (1f - num2));
			}
			if (uMaterial.HasProperty(k_MetallicGlossMap))
			{
				UnityEngine.Texture texture3 = uMaterial.GetTexture(k_MetallicGlossMap);
				if (texture3 != null)
				{
					if (texture3 is Texture2D texture2D)
					{
						pbrMetallicRoughness.metallicRoughnessTexture = pbrMetallicRoughness.metallicRoughnessTexture ?? new TextureInfo();
						metalGlossTexture = texture2D;
						if (HasMetallicGlossMap(uMaterial))
						{
							pbrMetallicRoughness.metallicFactor = 1f;
						}
						MaterialExportBase.ExportTextureTransform(pbrMetallicRoughness.metallicRoughnessTexture, uMaterial, k_MetallicGlossMap, gltf);
					}
					else
					{
						logger?.Error(LogCode.TextureInvalidType, "metallic/gloss", uMaterial.name);
						result = false;
					}
				}
			}
			if (uMaterial.IsKeywordEnabled("_SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A"))
			{
				Texture2D texture2D2 = uMaterial.GetTexture(mainTexProperty) as Texture2D;
				if (texture2D2 != null)
				{
					pbrMetallicRoughness.metallicRoughnessTexture = pbrMetallicRoughness.metallicRoughnessTexture ?? new TextureInfo();
					smoothnessTexture = texture2D2;
					MaterialExportBase.ExportTextureTransform(pbrMetallicRoughness.metallicRoughnessTexture, uMaterial, mainTexProperty, gltf);
				}
			}
			material.pbrMetallicRoughness = pbrMetallicRoughness;
			return result;
		}

		private static bool HasMetallicGlossMap(UnityEngine.Material uMaterial)
		{
			if (!uMaterial.IsKeywordEnabled("_METALLICGLOSSMAP"))
			{
				return uMaterial.IsKeywordEnabled("_METALLICSPECGLOSSMAP");
			}
			return true;
		}

		private static OcclusionTextureInfo ExportOcclusionTextureInfo(UnityEngine.Texture texture, IGltfWritable gltf)
		{
			Texture2D texture2D = texture as Texture2D;
			if (texture2D == null)
			{
				return null;
			}
			ImageExport imageExport = new ImageExport(texture2D);
			if (MaterialExport.AddImageExport(gltf, imageExport, out var textureId))
			{
				return new OcclusionTextureInfo
				{
					index = textureId
				};
			}
			return null;
		}
	}
}
