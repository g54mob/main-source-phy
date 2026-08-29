using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace IE.RichFX
{
	[Serializable]
	[VolumeComponentMenu("Rich FX/Artistic/Rainbow Flow")]
	public sealed class RainbowFlow : CustomPostProcessVolumeComponent, IPostProcessComponent
	{
		public IntParameter steps = new IntParameter(0);

		public ClampedFloatParameter speed = new ClampedFloatParameter(0.15f, 0f, 50f);

		public FloatParameter multiplier = new FloatParameter(0.5f);

		public BoolParameter blackAndWhite = new BoolParameter(value: false);

		private Material m_Material;

		public override CustomPostProcessInjectionPoint injectionPoint => CustomPostProcessInjectionPoint.AfterPostProcess;

		public bool IsActive()
		{
			if (m_Material != null)
			{
				return (float)steps.value > 0f;
			}
			return false;
		}

		public override void Setup()
		{
			if (Shader.Find("Hidden/InanEvin/RichFX/RainbowFlow") != null)
			{
				m_Material = new Material(Shader.Find("Hidden/InanEvin/RichFX/RainbowFlow"));
			}
		}

		public override void Render(CommandBuffer cmd, HDCamera camera, RTHandle source, RTHandle destination)
		{
			if (!(m_Material == null))
			{
				if (blackAndWhite.value)
				{
					m_Material.EnableKeyword("ISBW");
				}
				else
				{
					m_Material.DisableKeyword("ISBW");
				}
				m_Material.SetFloat("_Multiplier", multiplier.value);
				m_Material.SetFloat("_Speed", speed.value);
				m_Material.SetFloat("_Steps", steps.value);
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
