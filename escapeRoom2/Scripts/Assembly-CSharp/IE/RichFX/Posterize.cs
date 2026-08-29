using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace IE.RichFX
{
	[Serializable]
	[VolumeComponentMenu("Rich FX/Artistic/Posterize")]
	public sealed class Posterize : CustomPostProcessVolumeComponent, IPostProcessComponent
	{
		public FloatParameter _Gamma = new FloatParameter(2f);

		public ClampedIntParameter _ColorCount = new ClampedIntParameter(0, 0, 1024);

		private Material m_Material;

		public override CustomPostProcessInjectionPoint injectionPoint => CustomPostProcessInjectionPoint.AfterPostProcess;

		public bool IsActive()
		{
			if (m_Material != null)
			{
				return (float)_ColorCount.value > 0f;
			}
			return false;
		}

		public override void Setup()
		{
			if (Shader.Find("Hidden/InanEvin/RichFX/Posterize") != null)
			{
				m_Material = new Material(Shader.Find("Hidden/InanEvin/RichFX/Posterize"));
			}
		}

		public override void Render(CommandBuffer cmd, HDCamera camera, RTHandle source, RTHandle destination)
		{
			if (!(m_Material == null))
			{
				m_Material.SetFloat("_Gamma", _Gamma.value);
				m_Material.SetFloat("_ColorCount", _ColorCount.value);
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
