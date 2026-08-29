using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace IE.RichFX
{
	[Serializable]
	[VolumeComponentMenu("Rich FX/Screen Distortions/Scanlines")]
	public sealed class Scanlines : CustomPostProcessVolumeComponent, IPostProcessComponent
	{
		public ClampedFloatParameter intensity = new ClampedFloatParameter(0f, 0f, 10f);

		public ClampedFloatParameter noise = new ClampedFloatParameter(0f, 0f, 1f);

		public ClampedIntParameter count = new ClampedIntParameter(0, 0, 4096);

		private Material m_Material;

		public override CustomPostProcessInjectionPoint injectionPoint => CustomPostProcessInjectionPoint.AfterPostProcess;

		public bool IsActive()
		{
			if (m_Material != null && intensity.value > 0f)
			{
				return (float)count.value > 0f;
			}
			return false;
		}

		public override void Setup()
		{
			if (Shader.Find("Hidden/InanEvin/RichFX/Scanlines") != null)
			{
				m_Material = new Material(Shader.Find("Hidden/InanEvin/RichFX/Scanlines"));
			}
		}

		public override void Render(CommandBuffer cmd, HDCamera camera, RTHandle source, RTHandle destination)
		{
			if (!(m_Material == null))
			{
				m_Material.SetFloat("_Intensity", intensity.value);
				m_Material.SetFloat("_Count", count.value);
				m_Material.SetFloat("_Noise", noise.value);
				m_Material.SetTexture("_InputTexture", source);
				HDUtils.DrawFullScreen(cmd, m_Material, destination);
			}
		}

		public override void Cleanup()
		{
			CoreUtils.Destroy(m_Material);
		}
	}
}
