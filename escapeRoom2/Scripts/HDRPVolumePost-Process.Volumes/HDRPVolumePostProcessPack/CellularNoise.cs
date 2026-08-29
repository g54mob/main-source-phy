using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace HDRPVolumePostProcessPack
{
	[Serializable]
	[VolumeComponentMenu("HDRP Volume Post-Process Pack/Cellular Noise")]
	public sealed class CellularNoise : TextureVignettableBase, IPostProcessComponent, ICustomVolume
	{
		public const int DEFAULT_CELL_SIZE = 40;

		[Tooltip("Controls the intensity of the effect.")]
		public ClampedFloatParameter threshold = new ClampedFloatParameter(40f, 0f, 100f);

		public ClampedFloatParameter speed = new ClampedFloatParameter(1f, 0f, 30f);

		private const string kShaderName = "Hidden/HDRP Volume Post-Process Pack/Cellular Noise";

		public override CustomPostProcessInjectionPoint injectionPoint => CustomPostProcessInjectionPoint.AfterPostProcess;

		public string ShaderName => "Hidden/HDRP Volume Post-Process Pack/Cellular Noise";

		public override void Setup()
		{
			if (Shader.Find("Hidden/HDRP Volume Post-Process Pack/Cellular Noise") != null)
			{
				m_Material = new Material(Shader.Find("Hidden/HDRP Volume Post-Process Pack/Cellular Noise"));
			}
			else
			{
				Debug.LogError("Unable to find shader 'Hidden/HDRP Volume Post-Process Pack/Cellular Noise'. Post Process Volume Grayscale is unable to load.");
			}
			base.Setup();
		}

		public override void Render(CommandBuffer cmd, HDCamera camera, RTHandle source, RTHandle destination)
		{
			if (IsActive())
			{
				m_Material.SetFloat("_MasterVolume", masterVolume.value);
				m_Material.SetFloat("_Threshold", threshold.value);
				m_Material.SetFloat("_Speed", speed.value);
				base.Render(cmd, camera, source, destination);
			}
		}

		public override void Cleanup()
		{
			CoreUtils.Destroy(m_Material);
		}
	}
}
