using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace HDRPVolumePostProcessPack
{
	[Serializable]
	[VolumeComponentMenu("HDRP Volume Post-Process Pack/Concentration Line")]
	public sealed class ConcentrationLine : CustomPostProcessVolumeComponent, IPostProcessComponent, ICustomVolume
	{
		[Tooltip("Controls the intensity of the effect.")]
		public ClampedFloatParameter intensity = new ClampedFloatParameter(0f, 0f, 1f, overrideState: true);

		public ClampedIntParameter lineFrequency = new ClampedIntParameter(40, 1, 200);

		public Vector2Parameter centerViewportInScreen = new Vector2Parameter(Vector2.one * 0.5f);

		public ClampedFloatParameter blankRadius = new ClampedFloatParameter(0.5f, 0f, 1f);

		public ClampedFloatParameter colorThreshold = new ClampedFloatParameter(0.8f, 0f, 1f);

		public ClampedFloatParameter blankAspect = new ClampedFloatParameter(0.8f, 0.001f, 2f);

		public ClampedFloatParameter lineRotation = new ClampedFloatParameter(0f, 0f, 1f);

		public ClampedFloatParameter autoRotationSpeed = new ClampedFloatParameter(0f, -10f, 10f);

		public ClampedFloatParameter lineRandomizeOffset = new ClampedFloatParameter(0f, 0f, 1f);

		public ClampedFloatParameter autoRandomizeSpeed = new ClampedFloatParameter(1f, -10f, 10f);

		public BoolParameter generateNoiseTextureAtRuntime = new BoolParameter(value: true, overrideState: true);

		public Texture2DParameter noiseTexture = new Texture2DParameter(null, overrideState: true);

		public ClampedFloatParameter noiseScale = new ClampedFloatParameter(0.5f, 0f, 1f);

		public IntParameter noiseTextureSize = new IntParameter(512);

		public ColorParameter lineColor = new ColorParameter(Color.white);

		private Material m_Material;

		private Texture runtimeNoiseTexture;

		private const string kShaderName = "Hidden/HDRP Volume Post-Process Pack/ConcentrationLine";

		public override CustomPostProcessInjectionPoint injectionPoint => CustomPostProcessInjectionPoint.AfterPostProcess;

		public string ShaderName => "Hidden/HDRP Volume Post-Process Pack/ConcentrationLine";

		public bool IsActive()
		{
			if (m_Material != null)
			{
				return intensity.value > 0f;
			}
			return false;
		}

		public override void Setup()
		{
			if (Shader.Find("Hidden/HDRP Volume Post-Process Pack/ConcentrationLine") != null)
			{
				m_Material = new Material(Shader.Find("Hidden/HDRP Volume Post-Process Pack/ConcentrationLine"));
			}
			else
			{
				Debug.LogError("Unable to find shader 'Hidden/HDRP Volume Post-Process Pack/ConcentrationLine'. Post Process Volume ConcentrationLine is unable to load.");
			}
		}

		public override void Render(CommandBuffer cmd, HDCamera camera, RTHandle source, RTHandle destination)
		{
			if (generateNoiseTextureAtRuntime.value)
			{
				if (runtimeNoiseTexture == null)
				{
					runtimeNoiseTexture = CreateNoiseTexture();
					noiseTexture.value = runtimeNoiseTexture;
				}
			}
			else
			{
				runtimeNoiseTexture = noiseTexture.value;
			}
			if (IsActive())
			{
				m_Material.SetFloat("_Intensity", intensity.value);
				m_Material.SetFloat("_CenterViewportInScreenX", centerViewportInScreen.value.x);
				m_Material.SetFloat("_CenterViewportInScreenY", centerViewportInScreen.value.y);
				m_Material.SetFloat("_LineFrequency", lineFrequency.value);
				m_Material.SetFloat("_BlankRadius", blankRadius.value);
				m_Material.SetFloat("_ColorThreshold", colorThreshold.value);
				m_Material.SetFloat("_BlankAspect", blankAspect.value);
				m_Material.SetTexture("_NoiseTexture", runtimeNoiseTexture);
				m_Material.SetFloat("_LineRotation", lineRotation.value);
				m_Material.SetFloat("_AutoRotationSpeed", autoRotationSpeed.value);
				m_Material.SetFloat("_LineRandomizeCycle", lineRandomizeOffset.value);
				m_Material.SetFloat("_AutoRandomizeSpeed", autoRandomizeSpeed.value);
				m_Material.SetColor("_LineColor", lineColor.value);
				cmd.Blit(source, destination, m_Material, 0);
			}
		}

		public override void Cleanup()
		{
			CoreUtils.Destroy(m_Material);
		}

		public Texture2D CreateNoiseTexture()
		{
			return TextureTool.CreateSimpleNoiseTexture((int)noiseTextureSize);
		}

		public override void Override(VolumeComponent state, float interpFactor)
		{
			base.Override(state, interpFactor);
			(state as ConcentrationLine).intensity.SetValue(new FloatParameter(interpFactor * intensity.value, overrideState: true));
		}
	}
}
