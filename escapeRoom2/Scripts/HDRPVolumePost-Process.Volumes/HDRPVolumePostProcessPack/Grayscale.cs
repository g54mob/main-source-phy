using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace HDRPVolumePostProcessPack
{
	[Serializable]
	[VolumeComponentMenu("HDRP Volume Post-Process Pack/Grayscale")]
	public sealed class Grayscale : CustomPostProcessVolumeComponent, IPostProcessComponent, ICustomVolume
	{
		[Tooltip("Controls the intensity of the effect.")]
		public ClampedFloatParameter intensity = new ClampedFloatParameter(0f, 0f, 1f);

		private Material m_Material;

		private const string kShaderName = "Hidden/HDRP Volume Post-Process Pack/Grayscale";

		public override CustomPostProcessInjectionPoint injectionPoint => CustomPostProcessInjectionPoint.AfterPostProcess;

		public string ShaderName => "Hidden/HDRP Volume Post-Process Pack/Grayscale";

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
			if (Shader.Find("Hidden/HDRP Volume Post-Process Pack/Grayscale") != null)
			{
				m_Material = new Material(Shader.Find("Hidden/HDRP Volume Post-Process Pack/Grayscale"));
			}
			else
			{
				Debug.LogError("Unable to find shader 'Hidden/HDRP Volume Post-Process Pack/Grayscale'. Post Process Volume Grayscale is unable to load.");
			}
		}

		public override void Render(CommandBuffer cmd, HDCamera camera, RTHandle source, RTHandle destination)
		{
			if (!(m_Material == null))
			{
				m_Material.SetFloat("_Intensity", intensity.value);
				cmd.Blit(source, destination, m_Material, 0);
			}
		}

		public override void Cleanup()
		{
			CoreUtils.Destroy(m_Material);
		}
	}
}
