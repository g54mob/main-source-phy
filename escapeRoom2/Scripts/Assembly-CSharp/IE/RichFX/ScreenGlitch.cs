using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace IE.RichFX
{
	[Serializable]
	[VolumeComponentMenu("Rich FX/Screen Distortions/Screen Glitch")]
	public sealed class ScreenGlitch : CustomPostProcessVolumeComponent, IPostProcessComponent
	{
		public ClampedFloatParameter intensity = new ClampedFloatParameter(0f, 0f, 1f);

		public FloatParameter speed = new FloatParameter(2f);

		public ClampedFloatParameter xDisplacement = new ClampedFloatParameter(0f, -1f, 1f);

		public FloatParameter randomInferencePower = new FloatParameter(0.15f);

		public FloatParameter linePower = new FloatParameter(0.3f);

		public ClampedFloatParameter colorLerp = new ClampedFloatParameter(0.5f, -1f, 1f);

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
			if (Shader.Find("Hidden/InanEvin/RichFX/ScreenGlitch") != null)
			{
				m_Material = new Material(Shader.Find("Hidden/InanEvin/RichFX/ScreenGlitch"));
			}
		}

		public override void Render(CommandBuffer cmd, HDCamera camera, RTHandle source, RTHandle destination)
		{
			if (!(m_Material == null))
			{
				m_Material.SetFloat("_Speed", speed.value);
				m_Material.SetFloat("_NoiseWaves", intensity.value * 2f);
				m_Material.SetFloat("_XDisplacement", xDisplacement.value * 0.01f);
				m_Material.SetFloat("_RandomInferencePower", linePower.value);
				m_Material.SetFloat("_LinePower", randomInferencePower.value);
				m_Material.SetFloat("_ColorLerp", colorLerp.value);
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
