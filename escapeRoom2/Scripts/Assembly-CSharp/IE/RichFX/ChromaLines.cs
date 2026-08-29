using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace IE.RichFX
{
	[Serializable]
	[VolumeComponentMenu("Rich FX/Screen Distortions/Chroma Lines")]
	public sealed class ChromaLines : CustomPostProcessVolumeComponent, IPostProcessComponent
	{
		public ClampedFloatParameter intensity = new ClampedFloatParameter(0f, 0f, 1f);

		public ClampedFloatParameter scanlinesIntensity = new ClampedFloatParameter(0.04f, 0f, 1f);

		public FloatParameter speed = new FloatParameter(6f);

		public IntParameter scanlinesCount = new IntParameter(800);

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
			if (Shader.Find("Hidden/InanEvin/RichFX/ChromaLines") != null)
			{
				m_Material = new Material(Shader.Find("Hidden/InanEvin/RichFX/ChromaLines"));
			}
		}

		public override void Render(CommandBuffer cmd, HDCamera camera, RTHandle source, RTHandle destination)
		{
			if (!(m_Material == null))
			{
				m_Material.SetFloat("_Intensity", intensity.value);
				m_Material.SetFloat("_Speed", speed.value);
				m_Material.SetFloat("_ScanlinesCount", scanlinesCount.value);
				m_Material.SetFloat("_ScanlinesIntensity", scanlinesIntensity.value);
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
