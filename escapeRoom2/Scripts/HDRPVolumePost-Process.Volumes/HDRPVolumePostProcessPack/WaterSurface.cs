using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace HDRPVolumePostProcessPack
{
	[Serializable]
	[VolumeComponentMenu("HDRP Volume Post-Process Pack/Water Surface")]
	public sealed class WaterSurface : CustomPostProcessVolumeComponent, IPostProcessComponent, ICustomVolume
	{
		[Tooltip("Controls the intensity of the effect.")]
		public ClampedFloatParameter intensity = new ClampedFloatParameter(0f, 0f, 1f, overrideState: true);

		public ClampedIntParameter layerCount = new ClampedIntParameter(2, 1, 10);

		public ClampedFloatParameter rippleBrightness = new ClampedFloatParameter(0.51f, 0f, 1f);

		public ClampedFloatParameter surfaceBrightness = new ClampedFloatParameter(0.116f, 0.001f, 1f);

		public ClampedFloatParameter seed1 = new ClampedFloatParameter(1.26f, 0f, 5f);

		public ClampedFloatParameter seed2 = new ClampedFloatParameter(2f, 0f, 5f);

		public ClampedFloatParameter meshDetail = new ClampedFloatParameter(1f, 0f, 5f);

		public ColorParameter color = new ColorParameter(new Color(0f, 0.4f, 0.55f, 1f));

		private Material m_Material;

		private const string kShaderName = "Hidden/HDRP Volume Post-Process Pack/WaterSurface";

		public override CustomPostProcessInjectionPoint injectionPoint => CustomPostProcessInjectionPoint.AfterPostProcess;

		public string ShaderName => "Hidden/HDRP Volume Post-Process Pack/WaterSurface";

		public bool IsActive()
		{
			if (m_Material != null)
			{
				return intensity.value > 0f;
			}
			return false;
		}

		public override void Setup()
		{
			if (Shader.Find("Hidden/HDRP Volume Post-Process Pack/WaterSurface") != null)
			{
				m_Material = new Material(Shader.Find("Hidden/HDRP Volume Post-Process Pack/WaterSurface"));
			}
			else
			{
				Debug.LogError("Unable to find shader 'Hidden/HDRP Volume Post-Process Pack/WaterSurface'. Post Process Volume WaterSurface is unable to load.");
			}
		}

		public override void Render(CommandBuffer cmd, HDCamera camera, RTHandle source, RTHandle destination)
		{
			if (IsActive())
			{
				m_Material.SetFloat("_Intensity", intensity.value);
				m_Material.SetInt("_LayerCount", layerCount.value);
				m_Material.SetFloat("_RippleBrightness", rippleBrightness.value);
				m_Material.SetFloat("_SurfaceBrightness", surfaceBrightness.value);
				m_Material.SetFloat("_Seed1", seed1.value);
				m_Material.SetFloat("_Seed2", seed2.value);
				m_Material.SetFloat("_MeshDetail", meshDetail.value);
				m_Material.SetVector("_Color", color.value);
				cmd.Blit(source, destination, m_Material, 0);
			}
		}

		public override void Cleanup()
		{
			CoreUtils.Destroy(m_Material);
		}
	}
}
