using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace IE.RichFX
{
	[Serializable]
	[VolumeComponentMenu("Rich FX/Edge Effects/Simple Outline")]
	public sealed class SimpleOutline : CustomPostProcessVolumeComponent, IPostProcessComponent
	{
		public ClampedFloatParameter intensity = new ClampedFloatParameter(0f, 0f, 1f);

		public ClampedFloatParameter threshold = new ClampedFloatParameter(0.01f, 0.01f, 1f);

		private Material m_Material;

		public override CustomPostProcessInjectionPoint injectionPoint => CustomPostProcessInjectionPoint.AfterPostProcess;

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
			if (Shader.Find("Hidden/InanEvin/RichFX/SimpleOutline") != null)
			{
				m_Material = new Material(Shader.Find("Hidden/InanEvin/RichFX/SimpleOutline"));
			}
		}

		public override void Render(CommandBuffer cmd, HDCamera camera, RTHandle source, RTHandle destination)
		{
			if (!(m_Material == null))
			{
				m_Material.SetFloat("_Intensity", intensity.value * 15f);
				m_Material.SetFloat("_Threshold", threshold.value);
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
