using UnityEngine;

namespace Battlehub.Utils
{
	public class StandardMaterialUtils : IMaterialUtil
	{
		public enum WorkflowMode
		{
			Specular = 0,
			Metallic = 1,
			Dielectric = 2
		}

		public enum SmoothnessMapChannel
		{
			SpecularMetallicAlpha = 0,
			AlbedoAlpha = 1
		}

		public enum BlendMode
		{
			Opaque = 0,
			Cutout = 1,
			Fade = 2,
			Transparent = 3
		}

		public enum UVSec
		{
			UV0 = 0,
			UV1 = 1
		}

		public static readonly WorkflowMode m_workflow = WorkflowMode.Metallic;

		void IMaterialUtil.SetMaterialKeywords(Material material)
		{
			if (!(material == null) && !(material.shader == null) && !(material.shader.name != "Standard"))
			{
				SetupMaterialWithBlendMode(material, GetBlendMode(material));
				SetMaterialKeywords(material, m_workflow);
			}
		}

		public static BlendMode GetBlendMode(Material material)
		{
			return (BlendMode)material.GetFloat("_Mode");
		}

		public static void SetBlendMode(Material material, BlendMode blendMode)
		{
			material.SetFloat("_Mode", (float)blendMode);
		}

		public static SmoothnessMapChannel GetSmoothnessMapChannel(Material material)
		{
			if ((int)material.GetFloat("_SmoothnessTextureChannel") == 1)
			{
				return SmoothnessMapChannel.AlbedoAlpha;
			}
			return SmoothnessMapChannel.SpecularMetallicAlpha;
		}

		public static void SetSmoothnessMapChannel(Material material, SmoothnessMapChannel channel)
		{
			material.SetFloat("_SmoothnessTextureChannel", (float)channel);
		}

		public static bool ShouldEmissionBeEnabled(Material mat, Color color)
		{
			bool flag = (mat.globalIlluminationFlags & MaterialGlobalIlluminationFlags.RealtimeEmissive) > MaterialGlobalIlluminationFlags.None;
			return color.maxColorComponent > 0.00039215686f || flag;
		}

		public static void SetMaterialKeywords(Material material, WorkflowMode workflowMode)
		{
			SetKeyword(material, "_NORMALMAP", (bool)material.GetTexture("_BumpMap") || (bool)material.GetTexture("_DetailNormalMap"));
			switch (workflowMode)
			{
			case WorkflowMode.Specular:
				SetKeyword(material, "_SPECGLOSSMAP", material.GetTexture("_SpecGlossMap"));
				break;
			case WorkflowMode.Metallic:
				SetKeyword(material, "_METALLICGLOSSMAP", material.GetTexture("_MetallicGlossMap"));
				break;
			}
			SetKeyword(material, "_PARALLAXMAP", material.GetTexture("_ParallaxMap"));
			SetKeyword(material, "_DETAIL_MULX2", (bool)material.GetTexture("_DetailAlbedoMap") || (bool)material.GetTexture("_DetailNormalMap"));
			bool flag = ShouldEmissionBeEnabled(material, material.GetColor("_EmissionColor"));
			SetKeyword(material, "_EMISSION", flag);
			if (material.HasProperty("_SmoothnessTextureChannel"))
			{
				SetKeyword(material, "_SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A", GetSmoothnessMapChannel(material) == SmoothnessMapChannel.AlbedoAlpha);
			}
			MaterialGlobalIlluminationFlags globalIlluminationFlags = material.globalIlluminationFlags;
			if ((globalIlluminationFlags & MaterialGlobalIlluminationFlags.AnyEmissive) != MaterialGlobalIlluminationFlags.None)
			{
				globalIlluminationFlags &= ~MaterialGlobalIlluminationFlags.EmissiveIsBlack;
				if (!flag)
				{
					globalIlluminationFlags |= MaterialGlobalIlluminationFlags.EmissiveIsBlack;
				}
				material.globalIlluminationFlags = globalIlluminationFlags;
			}
		}

		public static void SetKeyword(Material m, string keyword, bool state)
		{
			if (state)
			{
				m.EnableKeyword(keyword);
			}
			else
			{
				m.DisableKeyword(keyword);
			}
		}

		public static void SetupMaterialWithBlendMode(Material material, BlendMode blendMode)
		{
			switch (blendMode)
			{
			case BlendMode.Opaque:
				material.SetOverrideTag("RenderType", "");
				material.SetInt("_SrcBlend", 1);
				material.SetInt("_DstBlend", 0);
				material.SetInt("_ZWrite", 1);
				material.DisableKeyword("_ALPHATEST_ON");
				material.DisableKeyword("_ALPHABLEND_ON");
				material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
				material.renderQueue = -1;
				break;
			case BlendMode.Cutout:
				material.SetOverrideTag("RenderType", "TransparentCutout");
				material.SetInt("_SrcBlend", 1);
				material.SetInt("_DstBlend", 0);
				material.SetInt("_ZWrite", 1);
				material.EnableKeyword("_ALPHATEST_ON");
				material.DisableKeyword("_ALPHABLEND_ON");
				material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
				material.renderQueue = 2450;
				break;
			case BlendMode.Fade:
				material.SetOverrideTag("RenderType", "Transparent");
				material.SetInt("_SrcBlend", 5);
				material.SetInt("_DstBlend", 10);
				material.SetInt("_ZWrite", 0);
				material.DisableKeyword("_ALPHATEST_ON");
				material.EnableKeyword("_ALPHABLEND_ON");
				material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
				material.renderQueue = 3000;
				break;
			case BlendMode.Transparent:
				material.SetOverrideTag("RenderType", "Transparent");
				material.SetInt("_SrcBlend", 1);
				material.SetInt("_DstBlend", 10);
				material.SetInt("_ZWrite", 0);
				material.DisableKeyword("_ALPHATEST_ON");
				material.DisableKeyword("_ALPHABLEND_ON");
				material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
				material.renderQueue = 3000;
				break;
			}
		}
	}
}
