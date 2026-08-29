using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace HDRPVolumePostProcessPack
{
	[Serializable]
	[VolumeComponentMenu("HDRP Volume Post-Process Pack/Simple Ripple")]
	public sealed class SimpleRipple : CustomPostProcessVolumeComponent, IPostProcessComponent, ICustomVolume
	{
		[Tooltip("Controls the intensity of the effect.")]
		public ClampedFloatParameter masterVolume = new ClampedFloatParameter(1f, 0f, 1f, overrideState: true);

		public ClampedFloatParameter propagation = new ClampedFloatParameter(0f, 0f, 30f);

		public ClampedFloatParameter intensity = new ClampedFloatParameter(1f, 0f, 10f);

		public ClampedFloatParameter frequency = new ClampedFloatParameter(5f, 0f, 100f);

		public Vector2Parameter rippleCenterUV = new Vector2Parameter(Vector2.one / 2f);

		public BoolParameter autoPlay = new BoolParameter(value: true, overrideState: true);

		public ClampedFloatParameter rippleSpeed = new ClampedFloatParameter(1.5f, 0f, 100f);

		public ClampedFloatParameter rippleSpreadTime = new ClampedFloatParameter(5f, 0f, 100f);

		private Material m_Material;

		private const string kShaderName = "Hidden/HDRP Volume Post-Process Pack/Simple Ripple";

		private bool playing;

		private float runtimePropagation;

		private float ripplePlayTime;

		private float playOffsetTime;

		public override CustomPostProcessInjectionPoint injectionPoint => CustomPostProcessInjectionPoint.AfterPostProcess;

		public string ShaderName => "Hidden/HDRP Volume Post-Process Pack/Simple Ripple";

		public bool IsActive()
		{
			if (m_Material != null && masterVolume.overrideState)
			{
				return masterVolume.value > 0f;
			}
			return false;
		}

		public override void Setup()
		{
			if (Shader.Find("Hidden/HDRP Volume Post-Process Pack/Simple Ripple") != null)
			{
				m_Material = new Material(Shader.Find("Hidden/HDRP Volume Post-Process Pack/Simple Ripple"));
			}
			else
			{
				Debug.LogError("Unable to find shader 'Hidden/HDRP Volume Post-Process Pack/Simple Ripple'. Post Process Volume Ripples is unable to load.");
			}
			ResetPlayState();
		}

		public void ResetPlayState()
		{
			if (autoPlay.value)
			{
				playOffsetTime = Time.realtimeSinceStartup;
				runtimePropagation = 0f;
				playing = false;
			}
		}

		public override void Render(CommandBuffer cmd, HDCamera camera, RTHandle source, RTHandle destination)
		{
			if (!IsActive())
			{
				ResetPlayState();
				return;
			}
			float num = 0f;
			if (autoPlay.value)
			{
				if (!playing)
				{
					playOffsetTime = Time.realtimeSinceStartup;
					runtimePropagation = 0f;
					playing = true;
				}
				else
				{
					ripplePlayTime = Time.realtimeSinceStartup - playOffsetTime;
					runtimePropagation = rippleSpeed.value * ripplePlayTime;
					num = Mathf.InverseLerp(rippleSpreadTime.value, 0f, ripplePlayTime);
				}
			}
			else
			{
				runtimePropagation = propagation.value;
				num = intensity.value;
			}
			m_Material.SetFloat("_Intensity", num * masterVolume.value);
			m_Material.SetVector("_RippleCenterUV", rippleCenterUV.value);
			m_Material.SetFloat("_Frequency", frequency.value);
			m_Material.SetFloat("_Propagation", runtimePropagation);
			cmd.Blit(source, destination, m_Material, 0);
		}

		public override void Cleanup()
		{
			CoreUtils.Destroy(m_Material);
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			ResetPlayState();
		}
	}
}
