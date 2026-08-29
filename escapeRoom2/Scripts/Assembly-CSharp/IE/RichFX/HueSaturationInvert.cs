using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace IE.RichFX
{
	[Serializable]
	[VolumeComponentMenu("Rich FX/Color Effects/Hue Saturation Invert")]
	public sealed class HueSaturationInvert : CustomPostProcessVolumeComponent, IPostProcessComponent
	{
		public ClampedFloatParameter hueShift = new ClampedFloatParameter(0f, -1f, 1f);

		public ClampedFloatParameter saturation = new ClampedFloatParameter(1f, 0f, 2f);

		public ClampedFloatParameter invert = new ClampedFloatParameter(0f, 0f, 1f);

		private Material m_Material;

		public override CustomPostProcessInjectionPoint injectionPoint => CustomPostProcessInjectionPoint.AfterPostProcess;

		public bool IsActive()
		{
			if (m_Material != null)
			{
				if (hueShift.value == 0f && saturation.value == 1f)
				{
					return invert.value > 0f;
				}
				return true;
			}
			return false;
		}

		public override void Setup()
		{
			if (Shader.Find("Hidden/InanEvin/RichFX/HueSaturationInvert") != null)
			{
				m_Material = new Material(Shader.Find("Hidden/InanEvin/RichFX/HueSaturationInvert"));
			}
		}

		public override void Render(CommandBuffer cmd, HDCamera camera, RTHandle source, RTHandle destination)
		{
			if (!(m_Material == null))
			{
				m_Material.SetFloat("_Hue", hueShift.value);
				m_Material.SetFloat("_Saturation", saturation.value);
				m_Material.SetFloat("_Invert", invert.value);
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
