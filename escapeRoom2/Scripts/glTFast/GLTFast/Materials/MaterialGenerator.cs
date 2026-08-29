using System;
using GLTFast.Logging;
using GLTFast.Schema;
using Unity.Mathematics;
using UnityEngine;

namespace GLTFast.Materials
{
	public abstract class MaterialGenerator : IMaterialGenerator
	{
		protected enum MaterialType
		{
			MetallicRoughness = 0,
			SpecularGlossiness = 1,
			Unlit = 2
		}

		public const string DefaultMaterialName = "glTF-Default-Material";

		public const string RenderTypeTag = "RenderType";

		public const string TransparentCutoutRenderType = "TransparentCutout";

		public const string OpaqueRenderType = "Opaque";

		public const string FadeRenderType = "Fade";

		public const string TransparentRenderType = "Transparent";

		public const string AlphaTestOnKeyword = "_ALPHATEST_ON";

		public const string TextureTransformKeyword = "_TEXTURE_TRANSFORM";

		public const string UVChannelSelectKeyword = "_UV_CHANNEL_SELECT";

		[Obsolete("Use MaterialProperty.AlphaCutoff instead.")]
		public static readonly int AlphaCutoffProperty = MaterialProperty.AlphaCutoff;

		[Obsolete("Use MaterialProperty.BaseColor instead.")]
		public static readonly int BaseColorProperty = MaterialProperty.BaseColor;

		[Obsolete("Use MaterialProperty.BaseColorTexture instead.")]
		public static readonly int BaseColorTextureProperty = MaterialProperty.BaseColorTexture;

		[Obsolete("Use MaterialProperty.BaseColorTextureRotation instead.")]
		public static readonly int BaseColorTextureRotationProperty = MaterialProperty.BaseColorTextureRotation;

		[Obsolete("Use MaterialProperty.BaseColorTextureScaleTransform instead.")]
		public static readonly int BaseColorTextureScaleTransformProperty = MaterialProperty.BaseColorTextureScaleTransform;

		[Obsolete("Use MaterialProperty.BaseColorTextureTexCoord instead.")]
		public static readonly int BaseColorTextureTexCoordProperty = MaterialProperty.BaseColorTextureTexCoord;

		[Obsolete("Use MaterialProperty.CullMode instead.")]
		public static readonly int CullModeProperty = MaterialProperty.CullMode;

		[Obsolete("Use MaterialProperty.Cull instead.")]
		public static readonly int CullProperty = MaterialProperty.Cull;

		[Obsolete("Use MaterialProperty.DstBlend instead.")]
		public static readonly int DstBlendProperty = MaterialProperty.DstBlend;

		[Obsolete("Use MaterialProperty.DiffuseFactor instead.")]
		public static readonly int DiffuseFactorProperty = MaterialProperty.DiffuseFactor;

		[Obsolete("Use MaterialProperty.DiffuseTexture instead.")]
		public static readonly int DiffuseTextureProperty = MaterialProperty.DiffuseTexture;

		[Obsolete("Use MaterialProperty.DiffuseTextureScaleTransform instead.")]
		public static readonly int DiffuseTextureScaleTransformProperty = MaterialProperty.DiffuseTextureScaleTransform;

		[Obsolete("Use MaterialProperty.DiffuseTextureRotation instead.")]
		public static readonly int DiffuseTextureRotationProperty = MaterialProperty.DiffuseTextureRotation;

		[Obsolete("Use MaterialProperty.DiffuseTextureTexCoord instead.")]
		public static readonly int DiffuseTextureTexCoordProperty = MaterialProperty.DiffuseTextureTexCoord;

		[Obsolete("Use MaterialProperty.EmissiveFactor instead.")]
		public static readonly int EmissiveFactorProperty = MaterialProperty.EmissiveFactor;

		[Obsolete("Use MaterialProperty.EmissiveTexture instead.")]
		public static readonly int EmissiveTextureProperty = MaterialProperty.EmissiveTexture;

		[Obsolete("Use MaterialProperty.EmissiveTextureRotation instead.")]
		public static readonly int EmissiveTextureRotationProperty = MaterialProperty.EmissiveTextureRotation;

		[Obsolete("Use MaterialProperty.EmissiveTextureScaleTransform instead.")]
		public static readonly int EmissiveTextureScaleTransformProperty = MaterialProperty.EmissiveTextureScaleTransform;

		[Obsolete("Use MaterialProperty.EmissiveTextureTexCoord instead.")]
		public static readonly int EmissiveTextureTexCoordProperty = MaterialProperty.EmissiveTextureTexCoord;

		[Obsolete("Use MaterialProperty.GlossinessFactor instead.")]
		public static readonly int GlossinessFactorProperty = MaterialProperty.GlossinessFactor;

		[Obsolete("Use MaterialProperty.NormalTexture instead.")]
		public static readonly int NormalTextureProperty = MaterialProperty.NormalTexture;

		[Obsolete("Use MaterialProperty.NormalTextureRotation instead.")]
		public static readonly int NormalTextureRotationProperty = MaterialProperty.NormalTextureRotation;

		[Obsolete("Use MaterialProperty.NormalTextureScaleTransform instead.")]
		public static readonly int NormalTextureScaleTransformProperty = MaterialProperty.NormalTextureScaleTransform;

		[Obsolete("Use MaterialProperty.NormalTextureTexCoord instead.")]
		public static readonly int NormalTextureTexCoordProperty = MaterialProperty.NormalTextureTexCoord;

		[Obsolete("Use MaterialProperty.NormalTextureScale instead.")]
		public static readonly int NormalTextureScaleProperty = MaterialProperty.NormalTextureScale;

		[Obsolete("Use MaterialProperty.Metallic instead.")]
		public static readonly int MetallicProperty = MaterialProperty.Metallic;

		[Obsolete("Use MaterialProperty.MetallicRoughnessMap instead.")]
		public static readonly int MetallicRoughnessMapProperty = MaterialProperty.MetallicRoughnessMap;

		[Obsolete("Use MaterialProperty.MetallicRoughnessMapScaleTransform instead.")]
		public static readonly int MetallicRoughnessMapScaleTransformProperty = MaterialProperty.MetallicRoughnessMapScaleTransform;

		[Obsolete("Use MaterialProperty.MetallicRoughnessMapRotation instead.")]
		public static readonly int MetallicRoughnessMapRotationProperty = MaterialProperty.MetallicRoughnessMapRotation;

		[Obsolete("Use MaterialProperty.MetallicRoughnessMapTexCoord instead.")]
		public static readonly int MetallicRoughnessMapUVChannelProperty = MaterialProperty.MetallicRoughnessMapTexCoord;

		[Obsolete("Use MaterialProperty.OcclusionTexture instead.")]
		public static readonly int OcclusionTextureProperty = MaterialProperty.OcclusionTexture;

		[Obsolete("Use MaterialProperty.OcclusionTextureStrength instead.")]
		public static readonly int OcclusionTextureStrengthProperty = MaterialProperty.OcclusionTextureStrength;

		[Obsolete("Use MaterialProperty.OcclusionTextureRotation instead.")]
		public static readonly int OcclusionTextureRotationProperty = MaterialProperty.OcclusionTextureRotation;

		[Obsolete("Use MaterialProperty.OcclusionTextureScaleTransform instead.")]
		public static readonly int OcclusionTextureScaleTransformProperty = MaterialProperty.OcclusionTextureScaleTransform;

		[Obsolete("Use MaterialProperty.OcclusionTextureTexCoord instead.")]
		public static readonly int OcclusionTextureTexCoordProperty = MaterialProperty.OcclusionTextureTexCoord;

		[Obsolete("Use MaterialProperty.RoughnessFactor instead.")]
		public static readonly int RoughnessFactorProperty = MaterialProperty.RoughnessFactor;

		[Obsolete("Use MaterialProperty.SpecularFactor instead.")]
		public static readonly int SpecularFactorProperty = MaterialProperty.SpecularFactor;

		[Obsolete("Use MaterialProperty.SpecularGlossinessTexture instead.")]
		public static readonly int SpecularGlossinessTextureProperty = MaterialProperty.SpecularGlossinessTexture;

		[Obsolete("Use MaterialProperty.SpecularGlossinessTextureScaleTransform instead.")]
		public static readonly int SpecularGlossinessTextureScaleTransformProperty = MaterialProperty.SpecularGlossinessTextureScaleTransform;

		[Obsolete("Use MaterialProperty.SpecularGlossinessTextureRotation instead.")]
		public static readonly int SpecularGlossinessTextureRotationProperty = MaterialProperty.SpecularGlossinessTextureRotation;

		[Obsolete("Use MaterialProperty.SpecularGlossinessTextureTexCoord instead.")]
		public static readonly int SpecularGlossinessTextureTexCoordProperty = MaterialProperty.SpecularGlossinessTextureTexCoord;

		[Obsolete("Use MaterialProperty.SrcBlend instead.")]
		public static readonly int SrcBlendProperty = MaterialProperty.SrcBlend;

		[Obsolete("Use MaterialProperty.ZWrite instead.")]
		public static readonly int ZWriteProperty = MaterialProperty.ZWrite;

		private static IMaterialGenerator s_DefaultMaterialGenerator;

		private static bool s_DefaultMaterialGenerated;

		private static UnityEngine.Material s_DefaultMaterial;

		protected ICodeLogger Logger { get; private set; }

		public static IMaterialGenerator GetDefaultMaterialGenerator()
		{
			if (s_DefaultMaterialGenerator != null)
			{
				return s_DefaultMaterialGenerator;
			}
			RenderPipeline renderPipeline = RenderPipelineUtils.RenderPipeline;
			if (renderPipeline == RenderPipeline.HighDefinition)
			{
				s_DefaultMaterialGenerator = new HighDefinitionRPMaterialGenerator();
				return s_DefaultMaterialGenerator;
			}
			throw new InvalidOperationException($"Could not determine default MaterialGenerator (render pipeline {renderPipeline})");
		}

		public UnityEngine.Material GetDefaultMaterial(bool pointsSupport = false)
		{
			if (pointsSupport)
			{
				Logger?.Warning(LogCode.TopologyPointsMaterialUnsupported);
			}
			if (!s_DefaultMaterialGenerated)
			{
				s_DefaultMaterial = GenerateDefaultMaterial(pointsSupport);
				s_DefaultMaterialGenerated = true;
			}
			return s_DefaultMaterial;
		}

		protected abstract UnityEngine.Material GenerateDefaultMaterial(bool pointsSupport = false);

		protected static Shader FindShader(string shaderName, ICodeLogger logger)
		{
			Shader shader = Shader.Find(shaderName);
			if (shader == null)
			{
				logger?.Error(LogCode.ShaderMissing, shaderName);
			}
			return shader;
		}

		public abstract UnityEngine.Material GenerateMaterial(MaterialBase gltfMaterial, IGltfReadable gltf, bool pointsSupport = false);

		public void SetLogger(ICodeLogger logger)
		{
			Logger = logger;
		}

		protected bool TrySetTexture(TextureInfoBase textureInfo, UnityEngine.Material material, IGltfReadable gltf, int texturePropertyId, int scaleTransformPropertyId = -1, int rotationPropertyId = -1, int uvChannelPropertyId = -1)
		{
			if (textureInfo != null && textureInfo.index >= 0)
			{
				int index = textureInfo.index;
				if (gltf.GetSourceTexture(index) != null)
				{
					Texture2D texture = gltf.GetTexture(index);
					if (texture != null)
					{
						material.SetTexture(texturePropertyId, texture);
						if (scaleTransformPropertyId >= 0 && rotationPropertyId >= 0 && uvChannelPropertyId >= 0)
						{
							bool flipY = gltf.IsTextureYFlipped(index);
							TrySetTextureTransform(textureInfo, material, texturePropertyId, scaleTransformPropertyId, rotationPropertyId, uvChannelPropertyId, flipY);
						}
						return true;
					}
					Logger?.Error(LogCode.TextureLoadFailed, index.ToString());
				}
				else
				{
					Logger?.Error(LogCode.TextureNotFound, index.ToString());
				}
			}
			return false;
		}

		private void TrySetTextureTransform(TextureInfoBase textureInfo, UnityEngine.Material material, int texturePropertyId, int scaleTransformPropertyId = -1, int rotationPropertyId = -1, int uvChannelPropertyId = -1, bool flipY = false)
		{
			bool flag = false;
			float4 float5 = new float4(1f, 1f, 0f, 0f);
			int texCoord = textureInfo.texCoord;
			if (textureInfo.Extensions?.KHR_texture_transform != null)
			{
				flag = true;
				TextureTransform kHR_texture_transform = textureInfo.Extensions.KHR_texture_transform;
				if (kHR_texture_transform.texCoord >= 0)
				{
					texCoord = kHR_texture_transform.texCoord;
				}
				if (kHR_texture_transform.offset != null)
				{
					float5.z = kHR_texture_transform.offset[0];
					float5.w = 1f - kHR_texture_transform.offset[1];
				}
				if (kHR_texture_transform.scale != null)
				{
					float5.x = kHR_texture_transform.scale[0];
					float5.y = kHR_texture_transform.scale[1];
				}
				if (math.abs(kHR_texture_transform.rotation) >= float.Epsilon)
				{
					float num = math.cos(kHR_texture_transform.rotation);
					float num2 = math.sin(kHR_texture_transform.rotation);
					Vector2 vector = new Vector2(float5.x * num2, float5.y * (0f - num2));
					material.SetVector(rotationPropertyId, vector);
					float5.x *= num;
					float5.y *= num;
					float5.z -= vector.y;
				}
				else
				{
					material.SetVector(rotationPropertyId, Vector4.zero);
				}
				float5.w -= float5.y;
			}
			if (texCoord != 0)
			{
				if (uvChannelPropertyId >= 0 && (float)texCoord < 2f)
				{
					material.EnableKeyword("_UV_CHANNEL_SELECT");
					material.SetFloat(uvChannelPropertyId, texCoord);
				}
				else
				{
					Logger?.Error(LogCode.UVMulti, texCoord.ToString());
				}
			}
			if (flipY)
			{
				flag = true;
				float5.w = 1f - float5.w;
				float5.y = 0f - float5.y;
			}
			if (flag)
			{
				material.EnableKeyword("_TEXTURE_TRANSFORM");
			}
			material.SetTextureOffset(texturePropertyId, float5.zw);
			material.SetTextureScale(texturePropertyId, float5.xy);
			material.SetVector(scaleTransformPropertyId, float5);
		}

		protected static bool TransmissionWorkaroundShaderMode(Transmission transmission, ref Color baseColorLinear)
		{
			float num = Mathf.Min(Mathf.Min(baseColorLinear.r, baseColorLinear.g), baseColorLinear.b);
			if (baseColorLinear.maxColorComponent - num < 0.1f)
			{
				baseColorLinear.a *= 1f - transmission.transmissionFactor;
				return true;
			}
			baseColorLinear.a *= 1f - transmission.transmissionFactor * 0.5f;
			return false;
		}
	}
}
