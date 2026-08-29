using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace HDRPVolumePostProcessPack
{
	[Serializable]
	public abstract class VignettableBase : CustomPostProcessVolumeComponent, IPostProcessComponent
	{
		protected Material m_Material;

		private const float MAX_VIGNETTE_INTENSITY = 1f;

		public ClampedFloatParameter masterVolume = new ClampedFloatParameter(0f, 0f, 1f, overrideState: true);

		public VignetteParameter vignette = new VignetteParameter(new VignetteSetting(), overrideState: true);

		private const string MIXED_MASK_BASE_VIGNETTE = "MIXED_MASK_BASE_VIGNETTE";

		private const string SIMPLE_MASK_BASE_VIGNETTE = "SIMPLE_MASK_BASE_VIGNETTE";

		public bool IsActive()
		{
			if (m_Material != null && masterVolume.overrideState)
			{
				return masterVolume.value > 0f;
			}
			return false;
		}

		public override void Setup()
		{
		}

		public override void Render(CommandBuffer cmd, HDCamera camera, RTHandle source, RTHandle destination)
		{
			SetVignetteSetting(m_Material, masterVolume.value, vignette.value);
			cmd.Blit(source, destination, m_Material, 0);
		}

		public static void SetVignetteSetting(Material mat, float masterVolume, VignetteSetting vignetteSetting)
		{
			mat.SetFloat("_VignetteIntensity", vignetteSetting.intensity * masterVolume);
			mat.SetFloat("_VignetteCenterU", vignetteSetting.centerUV.x);
			mat.SetFloat("_VignetteCenterV", vignetteSetting.centerUV.y);
			mat.SetFloat("_VignetteAspectRatio", vignetteSetting.aspectRatio);
			mat.SetFloat("_VignetteGradationPower", vignetteSetting.gradationPower);
			mat.SetColor("_VignetteColor", vignetteSetting.color);
			mat.SetTexture("_VignetteMaskTexture", vignetteSetting.maskTexture);
			mat.SetFloat("_VignetteMaskRotation", vignetteSetting.maskRotation);
			mat.SetFloat("_VignetteMaskScaleX", vignetteSetting.maskScale.x);
			mat.SetFloat("_VignetteMaskScaleY", vignetteSetting.maskScale.y);
			mat.SetFloat("_VignetteMaskAlphaSign", vignetteSetting.inverseAlpha);
			mat.SetFloat("_VignetteMaskReductionRatio", vignetteSetting.maskReductionRatio);
			switch (vignetteSetting.vignetteMode)
			{
			case AdvancedVignetteType.SimpleMask:
				mat.DisableKeyword("MIXED_MASK_BASE_VIGNETTE");
				mat.EnableKeyword("SIMPLE_MASK_BASE_VIGNETTE");
				break;
			case AdvancedVignetteType.MixedMask:
				mat.DisableKeyword("SIMPLE_MASK_BASE_VIGNETTE");
				mat.EnableKeyword("MIXED_MASK_BASE_VIGNETTE");
				break;
			default:
				mat.DisableKeyword("MIXED_MASK_BASE_VIGNETTE");
				mat.DisableKeyword("SIMPLE_MASK_BASE_VIGNETTE");
				break;
			}
		}

		public override void Cleanup()
		{
			CoreUtils.Destroy(m_Material);
		}

		public override void Override(VolumeComponent state, float interpFactor)
		{
			base.Override(state, interpFactor);
			(state as VignettableBase).masterVolume.SetValue(new FloatParameter(interpFactor * masterVolume.value, overrideState: true));
		}
	}
}
