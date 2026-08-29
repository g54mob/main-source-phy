using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace IE.RichFX
{
	[Serializable]
	[VolumeComponentMenu("Rich FX/Color Effects/EGA Filter")]
	public sealed class EGAFilter : CustomPostProcessVolumeComponent, IPostProcessComponent
	{
		public ClampedFloatParameter colorDetail = new ClampedFloatParameter(0f, 0f, 5f);

		private Material m_Material;

		public override CustomPostProcessInjectionPoint injectionPoint => CustomPostProcessInjectionPoint.AfterPostProcess;

		public bool IsActive()
		{
			if (m_Material != null)
			{
				return colorDetail.value > 0f;
			}
			return false;
		}

		public override void Setup()
		{
			if (Shader.Find("Hidden/InanEvin/RichFX/EGAFilter") != null)
			{
				m_Material = new Material(Shader.Find("Hidden/InanEvin/RichFX/EGAFilter"));
			}
		}

		public override void Render(CommandBuffer cmd, HDCamera camera, RTHandle source, RTHandle destination)
		{
			if (!(m_Material == null))
			{
				m_Material.SetVectorArray("_Colors", ColorPalettes.colorPalettes2);
				m_Material.SetFloat("_ColorDetail", colorDetail.value);
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
