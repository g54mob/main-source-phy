using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace IE.RichFX
{
	[Serializable]
	[VolumeComponentMenu("Rich FX/Artistic/Sketch Motion")]
	public sealed class SketchMotion : CustomPostProcessVolumeComponent, IPostProcessComponent
	{
		public ClampedFloatParameter sketchiness = new ClampedFloatParameter(0f, 0f, 1f);

		public ClampedFloatParameter speed = new ClampedFloatParameter(100f, 0f, 500f);

		public ClampedFloatParameter motionAmount = new ClampedFloatParameter(1f, 0f, 5f);

		public ClampedFloatParameter baseModifier = new ClampedFloatParameter(0.902f, 0.9f, 1f);

		public BoolParameter invert = new BoolParameter(value: true);

		public BoolParameter colored = new BoolParameter(value: false);

		private Material m_Material;

		public override CustomPostProcessInjectionPoint injectionPoint => CustomPostProcessInjectionPoint.AfterPostProcess;

		public bool IsActive()
		{
			if (m_Material != null)
			{
				return sketchiness.value > 0f;
			}
			return false;
		}

		public override void Setup()
		{
			if (Shader.Find("Hidden/InanEvin/RichFX/SketchMotion") != null)
			{
				m_Material = new Material(Shader.Find("Hidden/InanEvin/RichFX/SketchMotion"));
			}
		}

		public override void Render(CommandBuffer cmd, HDCamera camera, RTHandle source, RTHandle destination)
		{
			if (!(m_Material == null))
			{
				if (invert.value)
				{
					m_Material.EnableKeyword("INVERT");
				}
				else
				{
					m_Material.DisableKeyword("INVERT");
				}
				m_Material.SetFloat("_Sketchiness", sketchiness.value / 100f);
				m_Material.SetFloat("_Speed", speed.value);
				m_Material.SetFloat("_MotionAmount", motionAmount.value);
				m_Material.SetFloat("_BaseModifier", baseModifier.value);
				m_Material.SetInt("_Colored", colored.value ? 1 : 0);
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
