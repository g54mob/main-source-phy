using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace HDRPVolumePostProcessPack
{
	[Serializable]
	[VolumeComponentMenu("HDRP Volume Post-Process Pack/Analog TV Lines")]
	public sealed class AnalogTVLines : CustomPostProcessVolumeComponent, IPostProcessComponent, ICustomVolume
	{
		[Tooltip("Controls the intensity of the effect.")]
		public ClampedFloatParameter masterVolume = new ClampedFloatParameter(0f, 0f, 1f, overrideState: true);

		public ClampedIntParameter lineCount = new ClampedIntParameter(1000, 0, 2000);

		public Vector2Parameter chromaticR = new Vector2Parameter(new Vector2(0.1f, 0.1f));

		public Vector2Parameter chromaticG = new Vector2Parameter(new Vector2(0.1f, -0.1f));

		public Vector2Parameter chromaticB = new Vector2Parameter(new Vector2(-0.1f, 0.1f));

		public FloatParameter speed = new FloatParameter(1f);

		private Material m_Material;

		private const string kShaderName = "Hidden/HDRP Volume Post-Process Pack/Analog TV Lines";

		public override CustomPostProcessInjectionPoint injectionPoint => CustomPostProcessInjectionPoint.AfterPostProcess;

		public string ShaderName => "Hidden/HDRP Volume Post-Process Pack/Analog TV Lines";

		public bool IsActive()
		{
			if (m_Material != null)
			{
				return masterVolume.value > 0f;
			}
			return false;
		}

		public override void Setup()
		{
			if (Shader.Find("Hidden/HDRP Volume Post-Process Pack/Analog TV Lines") != null)
			{
				m_Material = new Material(Shader.Find("Hidden/HDRP Volume Post-Process Pack/Analog TV Lines"));
			}
			else
			{
				Debug.LogError("Unable to find shader 'Hidden/HDRP Volume Post-Process Pack/Analog TV Lines'. Post Process Volume Analog TV Lines is unable to load.");
			}
		}

		public override void Render(CommandBuffer cmd, HDCamera camera, RTHandle source, RTHandle destination)
		{
			if (!(m_Material == null))
			{
				m_Material.SetFloat("_Intensity", (float)lineCount.value * masterVolume.value);
				m_Material.SetVector("_ChromaticR", chromaticR.value);
				m_Material.SetVector("_ChromaticG", chromaticG.value);
				m_Material.SetVector("_ChromaticB", chromaticB.value);
				m_Material.SetFloat("_SppedY", speed.value);
				cmd.Blit(source, destination, m_Material, 0);
			}
		}

		public override void Cleanup()
		{
			CoreUtils.Destroy(m_Material);
		}
	}
}
