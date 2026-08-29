using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace HDRPVolumePostProcessPack
{
	[Serializable]
	[VolumeComponentMenu("HDRP Volume Post-Process Pack/Noisy Distortion")]
	public sealed class NoisyDistortion : VignettableBase, IPostProcessComponent, ICustomVolume
	{
		[Tooltip("Controls the intensity of the effect.")]
		public ClampedFloatParameter intensity = new ClampedFloatParameter(2f, 0f, 10f);

		public TextureParameter sourceHeightMap = new TextureParameter(null);

		public ClampedFloatParameter bakeNoiseScale = new ClampedFloatParameter(1f, 0f, 10f);

		public IntParameter generateNoiseTextureSize = new IntParameter(2048);

		public FloatParameter noiseRoughnessMagnification = new FloatParameter(1f);

		public TextureParameter noiseTexture = new TextureParameter(null, overrideState: true);

		public Vector2Parameter runtimeNoiseScale = new Vector2Parameter(Vector2.one);

		public Vector2Parameter runtimeNoiseVector = new Vector2Parameter(Vector2.zero);

		public ClampedFloatParameter trancateNoiseValue = new ClampedFloatParameter(0f, 0f, 1f);

		private const string kShaderName = "Hidden/HDRP Volume Post-Process Pack/Noisy Distortion";

		public override CustomPostProcessInjectionPoint injectionPoint => CustomPostProcessInjectionPoint.AfterPostProcess;

		public string ShaderName => "Hidden/HDRP Volume Post-Process Pack/Noisy Distortion";

		public override void Setup()
		{
			if (Shader.Find("Hidden/HDRP Volume Post-Process Pack/Noisy Distortion") != null)
			{
				m_Material = new Material(Shader.Find("Hidden/HDRP Volume Post-Process Pack/Noisy Distortion"));
			}
			else
			{
				Debug.LogError("Unable to find shader 'Hidden/HDRP Volume Post-Process Pack/Noisy Distortion'. Post Process Volume NoisyDistortion is unable to load.");
			}
		}

		public override void Render(CommandBuffer cmd, HDCamera camera, RTHandle source, RTHandle destination)
		{
			if (!(m_Material == null))
			{
				VignettableBase.SetVignetteSetting(m_Material, masterVolume.value, vignette.value);
				noiseTexture.overrideState = true;
				m_Material.SetFloat("_Intensity", intensity.value * masterVolume.value);
				m_Material.SetFloat("_RuntimeNoiseScaleX", runtimeNoiseScale.value.x);
				m_Material.SetFloat("_RuntimeNoiseScaleY", runtimeNoiseScale.value.y);
				m_Material.SetFloat("_RuntimeNoiseVectorX", runtimeNoiseVector.value.x);
				m_Material.SetFloat("_RuntimeNoiseVectorY", runtimeNoiseVector.value.y);
				m_Material.SetTexture("_NoiseMap", noiseTexture.value);
				m_Material.SetFloat("_TrancateNoiseValue", trancateNoiseValue.value);
				m_Material.SetFloat("_TrancateNoiseValue", trancateNoiseValue.value);
				m_Material.SetFloat("_NoiseOffsetX", -0.5f);
				m_Material.SetFloat("_NoiseOffsetY", -0.5f);
				cmd.Blit(source, destination, m_Material, 0);
			}
		}

		public override void Cleanup()
		{
			CoreUtils.Destroy(m_Material);
		}
	}
}
