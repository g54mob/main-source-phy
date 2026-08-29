using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace HDRPVolumePostProcessPack
{
	[Serializable]
	[VolumeComponentMenu("HDRP Volume Post-Process Pack/Sketch")]
	public sealed class Sketch : CustomPostProcessVolumeComponent, IPostProcessComponent, ICustomVolume
	{
		[Tooltip("Controls the intensity of the effect.")]
		public ClampedFloatParameter intensity = new ClampedFloatParameter(0f, 0f, 1f, overrideState: true);

		public ClampedFloatParameter normalBaseOutlineIntensity = new ClampedFloatParameter(1.5f, 0f, 10f);

		public ClampedFloatParameter depthBaseOutlineIntensity = new ClampedFloatParameter(1.5f, 0f, 10f);

		public ColorParameter outlineColor = new ColorParameter(Color.black);

		public ColorParameter surfaceColor = new ColorParameter(Color.white);

		public ClampedFloatParameter surfaceIntensity = new ClampedFloatParameter(1f, 0f, 1f);

		private Material m_Material;

		private const string kShaderName = "Hidden/HDRP Volume Post-Process Pack/Sketch";

		public override CustomPostProcessInjectionPoint injectionPoint => CustomPostProcessInjectionPoint.AfterPostProcess;

		public string ShaderName => "Hidden/HDRP Volume Post-Process Pack/Sketch";

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
			if (Shader.Find("Hidden/HDRP Volume Post-Process Pack/Sketch") != null)
			{
				m_Material = new Material(Shader.Find("Hidden/HDRP Volume Post-Process Pack/Sketch"));
			}
			else
			{
				Debug.LogError("Unable to find shader 'Hidden/HDRP Volume Post-Process Pack/Sketch'. Post Process Volume Sketch is unable to load.");
			}
		}

		public override void Render(CommandBuffer cmd, HDCamera camera, RTHandle source, RTHandle destination)
		{
			if (!(m_Material == null))
			{
				m_Material.SetFloat("_NormalOutlineThreshold", (1f - normalBaseOutlineIntensity.value) * intensity.value);
				m_Material.SetFloat("_DepthOutlineThreshold", (1f - depthBaseOutlineIntensity.value) * intensity.value);
				m_Material.SetVector("_OutlineColor", outlineColor.value);
				m_Material.SetVector("_CanvasColor", surfaceColor.value);
				m_Material.SetFloat("_CanvasIntensity", surfaceIntensity.value * intensity.value);
				cmd.Blit(source, destination, m_Material, 0);
			}
		}

		public override void Cleanup()
		{
			CoreUtils.Destroy(m_Material);
		}
	}
}
