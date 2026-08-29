using System;
using GLTFast.Logging;
using GLTFast.Schema;
using UnityEngine;

namespace GLTFast.Export
{
	public abstract class MaterialExportBase : IMaterialExport
	{
		public static readonly int BaseColorProperty = Shader.PropertyToID("_BaseColor");

		public static readonly int MainTexProperty = Shader.PropertyToID("_MainTex");

		public static readonly int ColorProperty = Shader.PropertyToID("_Color");

		public static readonly int MetallicProperty = Shader.PropertyToID("_Metallic");

		public static readonly int SmoothnessProperty = Shader.PropertyToID("_Smoothness");

		public static readonly int CutoffProperty = Shader.PropertyToID("_Cutoff");

		public abstract bool ConvertMaterial(UnityEngine.Material uMaterial, out GLTFast.Schema.Material material, IGltfWritable gltf, ICodeLogger logger);

		protected static void SetAlphaModeAndCutoff(UnityEngine.Material uMaterial, GLTFast.Schema.Material material)
		{
			switch (uMaterial.GetTag("RenderType", searchFallbacks: false, ""))
			{
			case "TransparentCutout":
				if (uMaterial.HasProperty(CutoffProperty))
				{
					material.alphaCutoff = uMaterial.GetFloat(CutoffProperty);
				}
				material.SetAlphaMode(MaterialBase.AlphaMode.Mask);
				break;
			case "Transparent":
			case "Fade":
				material.SetAlphaMode(MaterialBase.AlphaMode.Blend);
				break;
			default:
				material.SetAlphaMode(MaterialBase.AlphaMode.Opaque);
				break;
			}
		}

		protected static bool IsDoubleSided(UnityEngine.Material uMaterial, int cullPropId)
		{
			if (uMaterial.HasProperty(cullPropId))
			{
				return uMaterial.GetInt(cullPropId) == 0;
			}
			return false;
		}

		protected static bool IsUnlit(UnityEngine.Material material)
		{
			return material.shader.name.ToLowerInvariant().Contains("unlit");
		}

		protected void ExportUnlit(GLTFast.Schema.Material material, UnityEngine.Material uMaterial, int mainTexProperty, IGltfWritable gltf, ICodeLogger logger)
		{
			gltf.RegisterExtensionUsage(Extension.MaterialsUnlit);
			material.extensions = material.extensions ?? new MaterialExtensions();
			material.extensions.KHR_materials_unlit = new MaterialUnlit();
			PbrMetallicRoughness pbrMetallicRoughness = material.pbrMetallicRoughness ?? new PbrMetallicRoughness();
			if (GetUnlitColor(uMaterial, out var baseColor))
			{
				pbrMetallicRoughness.BaseColor = baseColor.linear;
			}
			if (uMaterial.HasProperty(mainTexProperty))
			{
				UnityEngine.Texture texture = uMaterial.GetTexture(mainTexProperty);
				if (texture != null)
				{
					if (texture is Texture2D)
					{
						pbrMetallicRoughness.baseColorTexture = ExportTextureInfo(texture, gltf);
						if (pbrMetallicRoughness.baseColorTexture != null)
						{
							ExportTextureTransform(pbrMetallicRoughness.baseColorTexture, uMaterial, mainTexProperty, gltf);
						}
					}
					else
					{
						logger?.Error(LogCode.TextureInvalidType, "main", material.name);
					}
				}
			}
			material.pbrMetallicRoughness = pbrMetallicRoughness;
		}

		protected virtual bool GetUnlitColor(UnityEngine.Material uMaterial, out Color baseColor)
		{
			if (uMaterial.HasProperty(BaseColorProperty))
			{
				baseColor = uMaterial.GetColor(BaseColorProperty);
				return true;
			}
			if (uMaterial.HasProperty(ColorProperty))
			{
				baseColor = uMaterial.GetColor(ColorProperty);
				return true;
			}
			baseColor = Color.magenta;
			return false;
		}

		protected static TextureInfo ExportTextureInfo(UnityEngine.Texture texture, IGltfWritable gltf, ImageFormat format = ImageFormat.Unknown)
		{
			Texture2D texture2D = texture as Texture2D;
			if (texture2D == null)
			{
				return null;
			}
			ImageExport imageExport = new ImageExport(texture2D, format);
			if (MaterialExport.AddImageExport(gltf, imageExport, out var textureId))
			{
				return new TextureInfo
				{
					index = textureId
				};
			}
			return null;
		}

		protected static NormalTextureInfo ExportNormalTextureInfo(UnityEngine.Texture texture, UnityEngine.Material material, IGltfWritable gltf, int normalScalePropId)
		{
			Texture2D texture2D = texture as Texture2D;
			if (texture2D == null)
			{
				return null;
			}
			NormalImageExport imageExport = new NormalImageExport(texture2D);
			if (MaterialExport.AddImageExport(gltf, imageExport, out var textureId))
			{
				NormalTextureInfo normalTextureInfo = new NormalTextureInfo
				{
					index = textureId
				};
				if (material.HasProperty(normalScalePropId))
				{
					normalTextureInfo.scale = material.GetFloat(normalScalePropId);
				}
				return normalTextureInfo;
			}
			return null;
		}

		[Obsolete("Use MaterialExport.AddImageExport instead.")]
		protected static bool AddImageExport(IGltfWritable gltf, ImageExportBase imageExport, out int textureId)
		{
			return MaterialExport.AddImageExport(gltf, imageExport, out textureId);
		}

		protected static void ExportTextureTransform(TextureInfoBase def, UnityEngine.Material mat, int texPropertyId, IGltfWritable gltf)
		{
			Vector2 textureOffset = mat.GetTextureOffset(texPropertyId);
			Vector2 textureScale = mat.GetTextureScale(texPropertyId);
			if (textureOffset != Vector2.zero || textureScale != Vector2.one)
			{
				gltf.RegisterExtensionUsage(Extension.TextureTransform);
				def.SetTextureTransform(new TextureTransform
				{
					scale = new float[2] { textureScale.x, textureScale.y },
					offset = new float[2]
					{
						textureOffset.x,
						1f - textureOffset.y - textureScale.y
					}
				});
			}
		}
	}
}
