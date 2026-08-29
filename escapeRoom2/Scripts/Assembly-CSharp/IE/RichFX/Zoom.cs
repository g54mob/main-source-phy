using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace IE.RichFX
{
	[Serializable]
	[VolumeComponentMenu("Rich FX/Others/Zoom")]
	public sealed class Zoom : CustomPostProcessVolumeComponent, IPostProcessComponent
	{
		public ClampedFloatParameter scale = new ClampedFloatParameter(0f, 0f, 1f);

		public ClampedFloatParameter centerX = new ClampedFloatParameter(0.5f, 0f, 1f);

		public ClampedFloatParameter centerY = new ClampedFloatParameter(0.5f, 0f, 1f);

		private Material m_Material;

		public override CustomPostProcessInjectionPoint injectionPoint => CustomPostProcessInjectionPoint.AfterPostProcess;

		public bool IsActive()
		{
			if (m_Material != null)
			{
				return scale.value > 0f;
			}
			return false;
		}

		public override void Setup()
		{
			if (Shader.Find("Hidden/InanEvin/RichFX/Zoom") != null)
			{
				m_Material = new Material(Shader.Find("Hidden/InanEvin/RichFX/Zoom"));
			}
		}

		public override void Render(CommandBuffer cmd, HDCamera camera, RTHandle source, RTHandle destination)
		{
			if (!(m_Material == null))
			{
				m_Material.SetFloat("_Scale", scale.value);
				m_Material.SetFloat("_CenterX", centerX.value);
				m_Material.SetFloat("_CenterY", centerY.value);
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
