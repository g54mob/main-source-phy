using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace IE.RichFX
{
	[Serializable]
	[VolumeComponentMenu("Rich FX/Edge Effects/Color Edges")]
	public sealed class ColorEdges : CustomPostProcessVolumeComponent, IPostProcessComponent
	{
		public ClampedFloatParameter intensity = new ClampedFloatParameter(0f, 0f, 1f);

		public ClampedFloatParameter edgeWidth = new ClampedFloatParameter(0.5f, 0.5f, 10f);

		public ColorParameter edgeColor = new ColorParameter(Color.black);

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
			if (Shader.Find("Hidden/InanEvin/RichFX/ColorEdges") != null)
			{
				m_Material = new Material(Shader.Find("Hidden/InanEvin/RichFX/ColorEdges"));
			}
		}

		public override void Render(CommandBuffer cmd, HDCamera camera, RTHandle source, RTHandle destination)
		{
			if (!(m_Material == null))
			{
				m_Material.SetFloat("_Intensity", intensity.value);
				m_Material.SetFloat("_EdgeWidth", edgeWidth.value);
				m_Material.SetColor("_EdgeColor", edgeColor.value);
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
