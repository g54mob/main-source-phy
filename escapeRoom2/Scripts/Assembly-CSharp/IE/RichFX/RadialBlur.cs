using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace IE.RichFX
{
	[Serializable]
	[VolumeComponentMenu("Rich FX/Blurs/Radial Blur")]
	public sealed class RadialBlur : CustomPostProcessVolumeComponent, IPostProcessComponent
	{
		public ClampedFloatParameter intensity = new ClampedFloatParameter(0f, 0f, 1f);

		public ClampedFloatParameter centerX = new ClampedFloatParameter(0.5f, 0f, 1f);

		public ClampedFloatParameter centerY = new ClampedFloatParameter(0.5f, 0f, 1f);

		public ClampedFloatParameter radius = new ClampedFloatParameter(0f, 0f, 1f);

		public ClampedIntParameter samples = new ClampedIntParameter(4, 4, 20);

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
			if (Shader.Find("Hidden/InanEvin/RichFX/RadialBlur") != null)
			{
				m_Material = new Material(Shader.Find("Hidden/InanEvin/RichFX/RadialBlur"));
			}
		}

		public override void Render(CommandBuffer cmd, HDCamera camera, RTHandle source, RTHandle destination)
		{
			if (!(m_Material == null))
			{
				m_Material.SetFloat("_EffectAmount", intensity.value);
				m_Material.SetFloat("_CenterX", centerX.value);
				m_Material.SetFloat("_CenterY", centerY.value);
				m_Material.SetFloat("_Radius", radius.value);
				m_Material.SetFloat("_Samples", samples.value);
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
