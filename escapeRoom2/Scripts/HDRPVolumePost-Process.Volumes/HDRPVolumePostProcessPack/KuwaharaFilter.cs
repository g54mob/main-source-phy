using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace HDRPVolumePostProcessPack
{
	[Serializable]
	[VolumeComponentMenu("HDRP Volume Post-Process Pack/Kuwahara Filter")]
	public sealed class KuwaharaFilter : CustomPostProcessVolumeComponent, IPostProcessComponent, ICustomVolume
	{
		[Tooltip("Controls the intensity of the effect.")]
		public ClampedIntParameter intensity = new ClampedIntParameter(3, 2, 8);

		private Material m_Material;

		private const string kShaderName = "Hidden/HDRP Volume Post-Process Pack/KuwaharaFilter";

		public override CustomPostProcessInjectionPoint injectionPoint => CustomPostProcessInjectionPoint.AfterPostProcess;

		public string ShaderName => "Hidden/HDRP Volume Post-Process Pack/KuwaharaFilter";

		public bool IsActive()
		{
			if (m_Material != null && intensity.overrideState)
			{
				return intensity.value > 0;
			}
			return false;
		}

		public override void Setup()
		{
			if (Shader.Find("Hidden/HDRP Volume Post-Process Pack/KuwaharaFilter") != null)
			{
				m_Material = new Material(Shader.Find("Hidden/HDRP Volume Post-Process Pack/KuwaharaFilter"));
			}
			else
			{
				Debug.LogError("Unable to find shader 'Hidden/HDRP Volume Post-Process Pack/KuwaharaFilter'. Post Process Volume KuwaharaFilter is unable to load.");
			}
		}

		public override void Render(CommandBuffer cmd, HDCamera camera, RTHandle source, RTHandle destination)
		{
			if (IsActive())
			{
				m_Material.SetInt("_BlockSize", intensity.value);
				cmd.Blit(source, destination, m_Material, 0);
			}
		}

		public override void Cleanup()
		{
			CoreUtils.Destroy(m_Material);
		}
	}
}
