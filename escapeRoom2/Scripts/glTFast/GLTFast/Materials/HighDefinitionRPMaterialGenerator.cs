using GLTFast.Schema;
using UnityEngine;

namespace GLTFast.Materials
{
	public class HighDefinitionRPMaterialGenerator : ShaderGraphMaterialGenerator
	{
		public const string DistortionVectorsPass = "DistortionVectors";

		public static readonly int CullModeForwardProperty = Shader.PropertyToID("_CullModeForward");

		private static readonly int k_ZTestDepthEqualForOpaque = Shader.PropertyToID("_ZTestDepthEqualForOpaque");

		private static readonly int k_RenderQueueType = Shader.PropertyToID("_RenderQueueType");

		private const string k_DoubleSidedOnKeyword = "_DOUBLESIDED_ON";

		private static readonly int k_DoubleSidedNormalModePropId = Shader.PropertyToID("_DoubleSidedNormalMode");

		private static readonly int k_DoubleSidedConstantsPropId = Shader.PropertyToID("_DoubleSidedConstants");

		public const string MetallicStackLitShader = "glTF-pbrMetallicRoughnessStackLit";

		private static bool s_MetallicStackLitShaderQueried;

		private static Shader s_MetallicStackLitShader;

		protected override void SetDoubleSided(MaterialBase gltfMaterial, UnityEngine.Material material)
		{
			base.SetDoubleSided(gltfMaterial, material);
			material.EnableKeyword("_DOUBLESIDED_ON");
			material.SetFloat(MaterialProperty.DoubleSidedEnable, 1f);
			material.SetFloat(k_DoubleSidedNormalModePropId, 0f);
			material.SetVector(k_DoubleSidedConstantsPropId, new Vector4(-1f, -1f, -1f, 0f));
			material.SetFloat(MaterialProperty.CullMode, 0f);
			material.SetFloat(CullModeForwardProperty, 0f);
		}

		protected override void SetAlphaModeMask(MaterialBase gltfMaterial, UnityEngine.Material material)
		{
			base.SetAlphaModeMask(gltfMaterial, material);
			material.SetFloat(MaterialProperty.AlphaCutoffEnable, 1f);
			material.SetOverrideTag("MotionVector", "User");
			material.SetShaderPassEnabled("MOTIONVECTORS", enabled: false);
			if (gltfMaterial.Extensions?.KHR_materials_unlit != null)
			{
				material.EnableKeyword("_SURFACE_TYPE_TRANSPARENT");
				material.EnableKeyword("_DISABLE_SSR_TRANSPARENT");
				material.EnableKeyword("_ENABLE_FOG_ON_TRANSPARENT");
				material.SetShaderPassEnabled("TransparentDepthPrepass", enabled: false);
				material.SetShaderPassEnabled("TransparentDepthPostpass", enabled: false);
				material.SetShaderPassEnabled("TransparentBackface", enabled: false);
				material.SetShaderPassEnabled("RayTracingPrepass", enabled: false);
				material.SetShaderPassEnabled("DepthOnly", enabled: false);
				material.SetFloat(ShaderGraphMaterialGenerator.AlphaDstBlendProperty, 10f);
				material.SetOverrideTag("RenderType", "Transparent");
				material.SetShaderPassEnabled("DistortionVectors", enabled: false);
				material.SetFloat(MaterialProperty.DstBlend, 10f);
				material.SetFloat(MaterialProperty.SrcBlend, 1f);
				material.SetFloat(k_ZTestDepthEqualForOpaque, 4f);
				material.SetFloat(MaterialProperty.ZWrite, 0f);
			}
		}

		protected override Shader GetMetallicShader(MetallicShaderFeatures features)
		{
			if ((features & MetallicShaderFeatures.ClearCoat) != MetallicShaderFeatures.Default)
			{
				if (!s_MetallicStackLitShaderQueried)
				{
					s_MetallicStackLitShader = LoadShaderByName("glTF-pbrMetallicRoughnessStackLit");
					if (s_MetallicStackLitShader == null)
					{
						s_MetallicStackLitShader = base.GetMetallicShader(features);
					}
					s_MetallicStackLitShaderQueried = true;
				}
				return s_MetallicStackLitShader;
			}
			return base.GetMetallicShader(features);
		}

		protected override void SetShaderModeBlend(MaterialBase gltfMaterial, UnityEngine.Material material)
		{
			material.DisableKeyword("_ALPHATEST_ON");
			material.EnableKeyword("_SURFACE_TYPE_TRANSPARENT");
			material.EnableKeyword("_DISABLE_SSR_TRANSPARENT");
			material.EnableKeyword("_ENABLE_FOG_ON_TRANSPARENT");
			material.SetOverrideTag("RenderType", "Transparent");
			material.SetShaderPassEnabled("TransparentDepthPrepass", enabled: false);
			material.SetShaderPassEnabled("TransparentDepthPostpass", enabled: false);
			material.SetShaderPassEnabled("TransparentBackface", enabled: false);
			material.SetShaderPassEnabled("RayTracingPrepass", enabled: false);
			material.SetShaderPassEnabled("DepthOnly", enabled: false);
			material.SetFloat(MaterialProperty.AlphaCutoffEnable, 0f);
			material.SetFloat(k_RenderQueueType, 4f);
			material.SetFloat(MaterialProperty.SurfaceType, 1f);
			material.SetFloat(MaterialProperty.ZWrite, 0f);
			material.SetFloat(ShaderGraphMaterialGenerator.ZTestGBufferProperty, 4f);
			material.SetFloat(k_ZTestDepthEqualForOpaque, 4f);
			material.SetFloat(ShaderGraphMaterialGenerator.AlphaDstBlendProperty, 10f);
			material.SetFloat(MaterialProperty.DstBlend, 10f);
			material.SetFloat(MaterialProperty.SrcBlend, 5f);
			material.SetFloat(MaterialProperty.EnableBlendModePreserveSpecularLighting, 0f);
		}
	}
}
