using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace HDRPVolumePostProcessPack
{
	[Serializable]
	[VolumeComponentMenu("HDRP Volume Post-Process Pack/Advanced Vignette")]
	public sealed class AdvancedVignette : TextureVignettableBase, IPostProcessComponent, ICustomVolume
	{
		private const string kShaderName = "Hidden/HDRP Volume Post-Process Pack/Advanced Vignette";

		public override CustomPostProcessInjectionPoint injectionPoint => CustomPostProcessInjectionPoint.AfterPostProcess;

		public string ShaderName => "Hidden/HDRP Volume Post-Process Pack/Advanced Vignette";

		public override void Setup()
		{
			if (Shader.Find("Hidden/HDRP Volume Post-Process Pack/Advanced Vignette") != null)
			{
				m_Material = new Material(Shader.Find("Hidden/HDRP Volume Post-Process Pack/Advanced Vignette"));
			}
			else
			{
				Debug.LogError("Unable to find shader 'Hidden/HDRP Volume Post-Process Pack/Advanced Vignette'. Post Process Volume AdvancedVignette is unable to load.");
			}
			base.Setup();
		}

		public override void Render(CommandBuffer cmd, HDCamera camera, RTHandle source, RTHandle destination)
		{
			if (IsActive())
			{
				base.Render(cmd, camera, source, destination);
			}
		}
	}
}
