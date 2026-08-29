using System;
using GLTFast.Logging;
using GLTFast.Schema;
using UnityEngine;
using UnityEngine.Rendering;

namespace GLTFast.Materials
{
	public class ShaderGraphMaterialGenerator : MaterialGenerator
	{
		[Flags]
		protected enum ShaderMode
		{
			Opaque = 0,
			Blend = 1,
			Premultiply = 2
		}

		[Flags]
		protected enum MetallicShaderFeatures
		{
			Default = 0,
			ModeMask = 3,
			ModeOpaque = 0,
			ModeFade = 1,
			ModeTransparent = 2,
			DoubleSided = 4,
			ClearCoat = 8,
			Sheen = 0x10
		}

		[Flags]
		protected enum SpecularShaderFeatures
		{
			Default = 0,
			AlphaBlend = 2,
			DoubleSided = 4
		}

		public const string MetallicShader = "glTF-pbrMetallicRoughness";

		public const string UnlitShader = "glTF-unlit";

		public const string SpecularShader = "glTF-pbrSpecularGlossiness";

		public const string MotionVectorTag = "MotionVector";

		public const string MotionVectorUser = "User";

		public const string MotionVectorsPass = "MOTIONVECTORS";

		private const string k_ShaderGraphsPrefix = "Shader Graphs/";

		private const string k_OcclusionKeyword = "_OCCLUSION";

		private const string k_EmissiveKeyword = "_EMISSIVE";

		private static readonly int k_BaseMapPropId = Shader.PropertyToID("baseColorTexture");

		private static readonly int k_BaseMapScaleTransformPropId = Shader.PropertyToID("baseColorTexture_ST");

		private static readonly int k_BaseMapRotationPropId = Shader.PropertyToID("baseColorTexture_Rotation");

		private static readonly int k_BaseMapUVChannelPropId = Shader.PropertyToID("baseColorTexture_texCoord");

		public static readonly int TransmissionFactorProperty = Shader.PropertyToID("transmissionFactor");

		public static readonly int TransmissionTextureProperty = Shader.PropertyToID("transmissionTexture");

		public static readonly int ClearcoatProperty = Shader.PropertyToID("clearcoatFactor");

		public static readonly int ClearcoatTextureProperty = Shader.PropertyToID("clearcoatTexture");

		public static readonly int ClearcoatTextureScaleTransformProperty = Shader.PropertyToID("clearcoatTexture_ST");

		public static readonly int ClearcoatTextureRotationProperty = Shader.PropertyToID("clearcoatTexture_Rotation");

		public static readonly int ClearcoatTextureTexCoordProperty = Shader.PropertyToID("clearcoatTexture_texCoord");

		public static readonly int ClearcoatRoughnessProperty = Shader.PropertyToID("clearcoatRoughnessFactor");

		public static readonly int ClearcoatRoughnessTextureProperty = Shader.PropertyToID("clearcoatRoughnessTexture");

		public static readonly int ClearcoatRoughnessTextureScaleTransformProperty = Shader.PropertyToID("clearcoatRoughnessTexture_ST");

		public static readonly int ClearcoatRoughnessTextureRotationProperty = Shader.PropertyToID("clearcoatRoughnessTexture_Rotation");

		public static readonly int ClearcoatRoughnessTextureTexCoordProperty = Shader.PropertyToID("clearcoatRoughnessTexture_texCoord");

		public static readonly int ClearcoatNormalTextureProperty = Shader.PropertyToID("clearcoatNormalTexture");

		public static readonly int ClearcoatNormalTextureScaleProperty = Shader.PropertyToID("clearcoatNormalTexture_Scale");

		public static readonly int ClearcoatNormalTextureScaleTransformProperty = Shader.PropertyToID("clearcoatNormalTexture_ST");

		public static readonly int ClearcoatNormalTextureRotationProperty = Shader.PropertyToID("clearcoatNormalTextureRotation");

		public static readonly int ClearcoatNormalTextureTexCoordProperty = Shader.PropertyToID("clearcoatNormalTexture_texCoord");

		private const string k_ClearcoatKeyword = "_CLEARCOAT";

		public const string DisableSsrTransparentKeyword = "_DISABLE_SSR_TRANSPARENT";

		public const string EnableFogOnTransparentKeyword = "_ENABLE_FOG_ON_TRANSPARENT";

		public const string SurfaceTypeTransparentKeyword = "_SURFACE_TYPE_TRANSPARENT";

		public const string ShaderPassTransparentDepthPrepass = "TransparentDepthPrepass";

		public const string ShaderPassTransparentDepthPostpass = "TransparentDepthPostpass";

		public const string ShaderPassTransparentBackface = "TransparentBackface";

		public const string ShaderPassRayTracingPrepass = "RayTracingPrepass";

		public const string ShaderPassDepthOnlyPass = "DepthOnly";

		public static readonly int AlphaDstBlendProperty = Shader.PropertyToID("_AlphaDstBlend");

		public static readonly int ZTestGBufferProperty = Shader.PropertyToID("_ZTestGBuffer");

		private static Shader s_MetallicShader;

		private static Shader s_SpecularShader;

		private static Shader s_UnlitShader;

		private static bool s_MetallicShaderQueried;

		private static bool s_SpecularShaderQueried;

		private static bool s_UnlitShaderQueried;

		protected override UnityEngine.Material GenerateDefaultMaterial(bool pointsSupport = false)
		{
			if (pointsSupport)
			{
				base.Logger?.Warning(LogCode.TopologyPointsMaterialUnsupported);
			}
			UnityEngine.Material metallicMaterial = GetMetallicMaterial(MetallicShaderFeatures.Default);
			if (metallicMaterial != null)
			{
				metallicMaterial.name = "glTF-Default-Material";
			}
			return metallicMaterial;
		}

		public override UnityEngine.Material GenerateMaterial(MaterialBase gltfMaterial, IGltfReadable gltf, bool pointsSupport = false)
		{
			if (pointsSupport)
			{
				base.Logger?.Warning(LogCode.TopologyPointsMaterialUnsupported);
			}
			ShaderMode shaderMode = ShaderMode.Opaque;
			bool num = gltfMaterial.Extensions?.KHR_materials_unlit != null;
			bool flag = gltfMaterial.Extensions?.KHR_materials_pbrSpecularGlossiness != null;
			UnityEngine.Material material;
			MaterialType? materialType;
			if (num)
			{
				material = GetUnlitMaterial(gltfMaterial);
				materialType = MaterialType.Unlit;
				shaderMode = ((gltfMaterial.GetAlphaMode() == MaterialBase.AlphaMode.Blend) ? ShaderMode.Blend : ShaderMode.Opaque);
			}
			else if (flag)
			{
				materialType = MaterialType.SpecularGlossiness;
				SpecularShaderFeatures specularShaderFeatures = GetSpecularShaderFeatures(gltfMaterial);
				material = GetSpecularMaterial(specularShaderFeatures);
				if ((specularShaderFeatures & SpecularShaderFeatures.AlphaBlend) != SpecularShaderFeatures.Default)
				{
					shaderMode = ShaderMode.Blend;
				}
			}
			else
			{
				materialType = MaterialType.MetallicRoughness;
				MetallicShaderFeatures metallicShaderFeatures = GetMetallicShaderFeatures(gltfMaterial);
				material = GetMetallicMaterial(metallicShaderFeatures);
				shaderMode = (ShaderMode)(metallicShaderFeatures & MetallicShaderFeatures.ModeMask);
			}
			if (material == null)
			{
				return null;
			}
			material.name = gltfMaterial.name;
			Color baseColorLinear = Color.white;
			RenderQueue? renderQueue = null;
			if (gltfMaterial.Extensions != null)
			{
				PbrSpecularGlossiness kHR_materials_pbrSpecularGlossiness = gltfMaterial.Extensions.KHR_materials_pbrSpecularGlossiness;
				if (kHR_materials_pbrSpecularGlossiness != null)
				{
					baseColorLinear = kHR_materials_pbrSpecularGlossiness.DiffuseColor;
					material.SetVector(MaterialProperty.DiffuseFactor, kHR_materials_pbrSpecularGlossiness.DiffuseColor.gamma);
					material.SetVector(MaterialProperty.SpecularFactor, kHR_materials_pbrSpecularGlossiness.SpecularColor);
					material.SetFloat(MaterialProperty.GlossinessFactor, kHR_materials_pbrSpecularGlossiness.glossinessFactor);
					TrySetTexture(kHR_materials_pbrSpecularGlossiness.diffuseTexture, material, gltf, MaterialProperty.DiffuseTexture, MaterialProperty.DiffuseTextureScaleTransform, MaterialProperty.DiffuseTextureRotation, MaterialProperty.DiffuseTextureTexCoord);
					TrySetTexture(kHR_materials_pbrSpecularGlossiness.specularGlossinessTexture, material, gltf, MaterialProperty.SpecularGlossinessTexture, MaterialProperty.SpecularGlossinessTextureScaleTransform, MaterialProperty.SpecularGlossinessTextureRotation, MaterialProperty.SpecularGlossinessTextureTexCoord);
				}
			}
			if (gltfMaterial.PbrMetallicRoughness != null && gltfMaterial.Extensions?.KHR_materials_pbrSpecularGlossiness == null)
			{
				baseColorLinear = gltfMaterial.PbrMetallicRoughness.BaseColor;
				if (materialType != MaterialType.SpecularGlossiness)
				{
					TrySetTexture(gltfMaterial.PbrMetallicRoughness.BaseColorTexture, material, gltf, k_BaseMapPropId, k_BaseMapScaleTransformPropId, k_BaseMapRotationPropId, k_BaseMapUVChannelPropId);
				}
				if (materialType == MaterialType.MetallicRoughness)
				{
					material.SetFloat(MaterialProperty.Metallic, gltfMaterial.PbrMetallicRoughness.metallicFactor);
					material.SetFloat(MaterialProperty.RoughnessFactor, gltfMaterial.PbrMetallicRoughness.roughnessFactor);
					TrySetTexture(gltfMaterial.PbrMetallicRoughness.MetallicRoughnessTexture, material, gltf, MaterialProperty.MetallicRoughnessMap, MaterialProperty.MetallicRoughnessMapScaleTransform, MaterialProperty.MetallicRoughnessMapRotation, MaterialProperty.MetallicRoughnessMapTexCoord);
				}
			}
			if (TrySetTexture(gltfMaterial.NormalTexture, material, gltf, MaterialProperty.NormalTexture, MaterialProperty.NormalTextureScaleTransform, MaterialProperty.NormalTextureRotation, MaterialProperty.NormalTextureTexCoord))
			{
				material.SetFloat(MaterialProperty.NormalTextureScale, gltfMaterial.NormalTexture.scale);
			}
			if (TrySetTexture(gltfMaterial.OcclusionTexture, material, gltf, MaterialProperty.OcclusionTexture, MaterialProperty.OcclusionTextureScaleTransform, MaterialProperty.OcclusionTextureRotation, MaterialProperty.OcclusionTextureTexCoord))
			{
				material.EnableKeyword("_OCCLUSION");
				material.SetFloat(MaterialProperty.OcclusionTextureStrength, gltfMaterial.OcclusionTexture.strength);
			}
			if (TrySetTexture(gltfMaterial.EmissiveTexture, material, gltf, MaterialProperty.EmissiveTexture, MaterialProperty.EmissiveTextureScaleTransform, MaterialProperty.EmissiveTextureRotation, MaterialProperty.EmissiveTextureTexCoord))
			{
				material.EnableKeyword("_EMISSIVE");
			}
			if (gltfMaterial.Extensions != null)
			{
				Transmission kHR_materials_transmission = gltfMaterial.Extensions.KHR_materials_transmission;
				if (kHR_materials_transmission != null)
				{
					renderQueue = ApplyTransmission(ref baseColorLinear, gltf, kHR_materials_transmission, material, null);
				}
			}
			if (gltfMaterial.GetAlphaMode() == MaterialBase.AlphaMode.Mask)
			{
				SetAlphaModeMask(gltfMaterial, material);
				renderQueue = ((gltfMaterial.Extensions?.KHR_materials_unlit == null) ? new RenderQueue?(RenderQueue.AlphaTest) : new RenderQueue?(RenderQueue.Transparent));
			}
			else
			{
				material.SetFloat(MaterialProperty.AlphaCutoff, 0f);
				material.SetOverrideTag("MotionVector", "User");
				material.SetShaderPassEnabled("MOTIONVECTORS", enabled: false);
			}
			if (!renderQueue.HasValue)
			{
				renderQueue = ((shaderMode != ShaderMode.Opaque) ? new RenderQueue?(RenderQueue.Transparent) : new RenderQueue?((gltfMaterial.GetAlphaMode() == MaterialBase.AlphaMode.Mask) ? RenderQueue.AlphaTest : RenderQueue.Geometry));
			}
			material.renderQueue = (int)renderQueue.Value;
			if (gltfMaterial.doubleSided)
			{
				SetDoubleSided(gltfMaterial, material);
			}
			switch (shaderMode)
			{
			case ShaderMode.Opaque:
				SetShaderModeOpaque(gltfMaterial, material);
				break;
			case ShaderMode.Blend:
				SetShaderModeBlend(gltfMaterial, material);
				break;
			case ShaderMode.Premultiply:
				SetShaderModePremultiply(gltfMaterial, material);
				break;
			}
			material.SetVector(MaterialProperty.BaseColor, baseColorLinear.gamma);
			if (gltfMaterial.Emissive != Color.black)
			{
				material.SetColor(MaterialProperty.EmissiveFactor, gltfMaterial.Emissive);
				material.EnableKeyword("_EMISSIVE");
			}
			MaterialExtensions extensions = gltfMaterial.Extensions;
			if (extensions != null && extensions.KHR_materials_clearcoat?.clearcoatFactor > 0f)
			{
				ClearCoat kHR_materials_clearcoat = gltfMaterial.Extensions.KHR_materials_clearcoat;
				material.SetFloat(ClearcoatProperty, kHR_materials_clearcoat.clearcoatFactor);
				TrySetTexture(kHR_materials_clearcoat.clearcoatTexture, material, gltf, ClearcoatTextureProperty, ClearcoatTextureScaleTransformProperty, ClearcoatTextureRotationProperty, ClearcoatTextureTexCoordProperty);
				material.SetFloat(ClearcoatRoughnessProperty, kHR_materials_clearcoat.clearcoatRoughnessFactor);
				material.EnableKeyword("_CLEARCOAT");
				TrySetTexture(kHR_materials_clearcoat.clearcoatRoughnessTexture, material, gltf, ClearcoatRoughnessTextureProperty, ClearcoatRoughnessTextureScaleTransformProperty, ClearcoatRoughnessTextureRotationProperty, ClearcoatRoughnessTextureTexCoordProperty);
				TrySetTexture(kHR_materials_clearcoat.clearcoatNormalTexture, material, gltf, ClearcoatNormalTextureProperty, ClearcoatNormalTextureScaleTransformProperty, ClearcoatNormalTextureRotationProperty, ClearcoatNormalTextureTexCoordProperty);
				material.SetFloat(ClearcoatNormalTextureScaleProperty, kHR_materials_clearcoat.clearcoatNormalTexture.scale);
			}
			return material;
		}

		private UnityEngine.Material GetMetallicMaterial(MetallicShaderFeatures metallicShaderFeatures)
		{
			Shader metallicShader = GetMetallicShader(metallicShaderFeatures);
			if (metallicShader == null)
			{
				return null;
			}
			return new UnityEngine.Material(metallicShader);
		}

		private UnityEngine.Material GetUnlitMaterial(MaterialBase gltfMaterial)
		{
			Shader unlitShader = GetUnlitShader(gltfMaterial);
			if (unlitShader == null)
			{
				return null;
			}
			return new UnityEngine.Material(unlitShader);
		}

		private UnityEngine.Material GetSpecularMaterial(SpecularShaderFeatures features)
		{
			Shader specularShader = GetSpecularShader(features);
			if (specularShader == null)
			{
				return null;
			}
			return new UnityEngine.Material(specularShader);
		}

		protected virtual Shader GetMetallicShader(MetallicShaderFeatures features)
		{
			if (!s_MetallicShaderQueried)
			{
				s_MetallicShader = LoadShaderByName("glTF-pbrMetallicRoughness");
				s_MetallicShaderQueried = true;
			}
			return s_MetallicShader;
		}

		private Shader GetUnlitShader(MaterialBase gltfMaterial)
		{
			if (!s_UnlitShaderQueried)
			{
				s_UnlitShader = LoadShaderByName("glTF-unlit");
				s_UnlitShaderQueried = true;
			}
			return s_UnlitShader;
		}

		private Shader GetSpecularShader(SpecularShaderFeatures features)
		{
			if (!s_SpecularShaderQueried)
			{
				s_SpecularShader = LoadShaderByName("glTF-pbrSpecularGlossiness");
				s_SpecularShaderQueried = true;
			}
			return s_SpecularShader;
		}

		protected Shader LoadShaderByName(string shaderName)
		{
			return MaterialGenerator.FindShader("Shader Graphs/" + shaderName, base.Logger);
		}

		protected virtual void SetDoubleSided(MaterialBase gltfMaterial, UnityEngine.Material material)
		{
			material.doubleSidedGI = true;
		}

		protected virtual void SetAlphaModeMask(MaterialBase gltfMaterial, UnityEngine.Material material)
		{
			material.SetFloat(MaterialProperty.AlphaCutoff, gltfMaterial.alphaCutoff);
			material.EnableKeyword("_ALPHATEST_ON");
			material.SetOverrideTag("RenderType", "TransparentCutout");
			material.SetFloat(ZTestGBufferProperty, 3f);
		}

		protected virtual void SetShaderModeOpaque(MaterialBase gltfMaterial, UnityEngine.Material material)
		{
		}

		protected virtual void SetShaderModeBlend(MaterialBase gltfMaterial, UnityEngine.Material material)
		{
		}

		protected virtual void SetShaderModePremultiply(MaterialBase gltfMaterial, UnityEngine.Material material)
		{
		}

		protected virtual RenderQueue? ApplyTransmission(ref Color baseColorLinear, IGltfReadable gltf, Transmission transmission, UnityEngine.Material material, RenderQueue? renderQueue)
		{
			if (transmission.transmissionFactor > 0f && transmission.transmissionTexture.index < 0)
			{
				MaterialGenerator.TransmissionWorkaroundShaderMode(transmission, ref baseColorLinear);
			}
			return renderQueue;
		}

		protected MetallicShaderFeatures GetMetallicShaderFeatures(MaterialBase gltfMaterial)
		{
			MetallicShaderFeatures metallicShaderFeatures = MetallicShaderFeatures.Default;
			ShaderMode? shaderMode = null;
			if (gltfMaterial.Extensions != null)
			{
				if (gltfMaterial.Extensions.KHR_materials_clearcoat != null && gltfMaterial.Extensions.KHR_materials_clearcoat.clearcoatFactor > 0f)
				{
					metallicShaderFeatures |= MetallicShaderFeatures.ClearCoat;
				}
				if (gltfMaterial.Extensions.KHR_materials_sheen != null && gltfMaterial.Extensions.KHR_materials_sheen.SheenColor.maxColorComponent > 0f)
				{
					metallicShaderFeatures |= MetallicShaderFeatures.Sheen;
				}
				if (gltfMaterial.Extensions.KHR_materials_transmission != null && gltfMaterial.Extensions.KHR_materials_transmission.transmissionFactor > 0f)
				{
					shaderMode = ApplyTransmissionShaderFeatures(gltfMaterial);
				}
			}
			if (gltfMaterial.doubleSided)
			{
				metallicShaderFeatures |= MetallicShaderFeatures.DoubleSided;
			}
			if (!shaderMode.HasValue)
			{
				shaderMode = ((gltfMaterial.GetAlphaMode() == MaterialBase.AlphaMode.Blend) ? ShaderMode.Blend : ShaderMode.Opaque);
			}
			return (MetallicShaderFeatures)((int)metallicShaderFeatures | (int)shaderMode.Value);
		}

		protected virtual ShaderMode? ApplyTransmissionShaderFeatures(MaterialBase gltfMaterial)
		{
			Color baseColorLinear = Color.white;
			return (!MaterialGenerator.TransmissionWorkaroundShaderMode(gltfMaterial.Extensions.KHR_materials_transmission, ref baseColorLinear)) ? ShaderMode.Blend : ShaderMode.Premultiply;
		}

		private static SpecularShaderFeatures GetSpecularShaderFeatures(MaterialBase gltfMaterial)
		{
			SpecularShaderFeatures specularShaderFeatures = SpecularShaderFeatures.Default;
			if (gltfMaterial.doubleSided)
			{
				specularShaderFeatures |= SpecularShaderFeatures.DoubleSided;
			}
			if (gltfMaterial.GetAlphaMode() != MaterialBase.AlphaMode.Opaque)
			{
				specularShaderFeatures |= SpecularShaderFeatures.AlphaBlend;
			}
			return specularShaderFeatures;
		}
	}
}
