using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace IE.RichFX
{
	[Serializable]
	[VolumeComponentMenu("Rich FX/Screen Distortions/Wobble")]
	public sealed class Wobble : CustomPostProcessVolumeComponent, IPostProcessComponent
	{
		public ClampedFloatParameter amplitude = new ClampedFloatParameter(0.003f, 0f, 0.1f);

		public FloatParameter speed = new FloatParameter(10f);

		public FloatParameter frequence = new FloatParameter(0f);

		private Material m_Material;

		public override CustomPostProcessInjectionPoint injectionPoint => CustomPostProcessInjectionPoint.AfterPostProcess;

		public bool IsActive()
		{
			if (m_Material != null)
			{
				return frequence.value > 0f;
			}
			return false;
		}

		public override void Setup()
		{
			if (Shader.Find("Hidden/InanEvin/RichFX/Wobble") != null)
			{
				m_Material = new Material(Shader.Find("Hidden/InanEvin/RichFX/Wobble"));
			}
		}

		public override void Render(CommandBuffer cmd, HDCamera camera, RTHandle source, RTHandle destination)
		{
			if (!(m_Material == null))
			{
				m_Material.SetFloat("_Amplitude", amplitude.value);
				m_Material.SetFloat("_Speed", speed.value);
				m_Material.SetFloat("_Frequence", frequence.value);
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
