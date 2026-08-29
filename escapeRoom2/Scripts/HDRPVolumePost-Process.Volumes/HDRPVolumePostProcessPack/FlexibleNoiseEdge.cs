using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace HDRPVolumePostProcessPack
{
	[Serializable]
	[VolumeComponentMenu("HDRP Volume Post-Process Pack/Flexible Noise Edge")]
	public sealed class FlexibleNoiseEdge : CustomPostProcessVolumeComponent, IPostProcessComponent, ICustomVolume
	{
		public const float EDGE_THREAHOLD_MIN = 0f;

		public const float EDGE_THREAHOLD_MAX = 0.99f;

		public const float BODY_THREAHOLD_MIN = 0.01f;

		public const float BODY_THREAHOLD_MAX = 1f;

		public FloatParameter masterVolume = new ClampedFloatParameter(0f, 0f, 1f, overrideState: true);

		public FlexibleNoiseParameter setting = new FlexibleNoiseParameter(null, overrideState: true);

		private const string kShaderName = "Hidden/HDRP Volume Post-Process Pack/Flexible Noise Edge";

		private Material m_Material;

		public bool isPlayableRunning;

		public float prevRenderTime;

		public float startRenderTime;

		private float timeOffset;

		private FlexibleNoiseSetting cachedNoiseSetting;

		private Vector2 surfaceTextureRenderOffset = Vector2.zero;

		private Vector2 noiseTextureRenderOffset = Vector2.zero;

		public string ShaderName => "Hidden/HDRP Volume Post-Process Pack/Flexible Noise Edge";

		public FlexibleNoiseSetting RuntimeSetting
		{
			get
			{
				if (isPlayableRunning)
				{
					if (cachedNoiseSetting == null)
					{
						cachedNoiseSetting = UnityEngine.Object.Instantiate(setting.value);
					}
					return cachedNoiseSetting;
				}
				return setting.value;
			}
		}

		public bool IsActive()
		{
			if (m_Material != null && setting.value != null && masterVolume.overrideState)
			{
				return masterVolume.value > 0f;
			}
			return false;
		}

		public void StartPlayable()
		{
			ClearCache();
			isPlayableRunning = true;
			timeOffset = 0f;
			timeOffset = GetTime();
			prevRenderTime = GetTime();
			startRenderTime = GetTime();
			surfaceTextureRenderOffset = setting.value.surface.offset;
			noiseTextureRenderOffset = setting.value.noise.offset;
		}

		private float GetTime()
		{
			return Time.time - timeOffset;
		}

		public void StopPlayable()
		{
			isPlayableRunning = false;
			ClearCache();
		}

		public void ClearCache()
		{
			UnityEngine.Object.Destroy(cachedNoiseSetting);
			cachedNoiseSetting = null;
		}

		public override void Setup()
		{
			if (Shader.Find("Hidden/HDRP Volume Post-Process Pack/Flexible Noise Edge") != null)
			{
				m_Material = new Material(Shader.Find("Hidden/HDRP Volume Post-Process Pack/Flexible Noise Edge"));
			}
			else
			{
				Debug.LogError("Unable to find shader 'Hidden/HDRP Volume Post-Process Pack/Flexible Noise Edge'. Post Process Volume FlameEdge is unable to load.");
			}
		}

		public override void Render(CommandBuffer cmd, HDCamera camera, RTHandle source, RTHandle destination)
		{
			if (IsActive())
			{
				float time = GetTime();
				float num = time - prevRenderTime;
				Vector2 vector = Vector2.zero;
				Vector2 vector2 = Vector2.zero;
				switch (RuntimeSetting.surface.scrollType)
				{
				case VectorType.Constant:
					vector = RuntimeSetting.surface.vector;
					break;
				case VectorType.AnimationCurve:
				{
					float time2 = (time - startRenderTime) * RuntimeSetting.surface.curveMultiplier;
					vector = new Vector2((RuntimeSetting.surface.vectorXAnimationCurve == null) ? 0f : RuntimeSetting.surface.vectorXAnimationCurve.Evaluate(time2), (RuntimeSetting.surface.vectorYAnimationCurve == null) ? 0f : RuntimeSetting.surface.vectorYAnimationCurve.Evaluate(time2));
					break;
				}
				}
				switch (RuntimeSetting.noise.scrollType)
				{
				case VectorType.Constant:
					vector2 = RuntimeSetting.noise.vector;
					break;
				case VectorType.AnimationCurve:
				{
					float time3 = (time - startRenderTime) * RuntimeSetting.noise.curveMultiplier;
					vector2 = new Vector2((RuntimeSetting.noise.vectorXAnimationCurve == null) ? 0f : RuntimeSetting.noise.vectorXAnimationCurve.Evaluate(time3), (RuntimeSetting.noise.vectorYAnimationCurve == null) ? 0f : RuntimeSetting.noise.vectorYAnimationCurve.Evaluate(time3));
					break;
				}
				}
				surfaceTextureRenderOffset += vector * num;
				noiseTextureRenderOffset += vector2 * num;
				prevRenderTime = time;
				VignettableBase.SetVignetteSetting(m_Material, masterVolume.value, RuntimeSetting.vignette);
				TextureVignettableBase.SetVignetteTextureSetting(m_Material, RuntimeSetting.surface);
				m_Material.SetColor("_EdgeColor", RuntimeSetting.edgeColor);
				m_Material.SetFloat("_SurfaceThreshold", RuntimeSetting.surfaceThreshold);
				m_Material.SetFloat("_EdgeThreshold", RuntimeSetting.edgeThreshold);
				m_Material.SetFloat("_VignetteTextureOffsetTime", RuntimeSetting.offsetTime);
				m_Material.SetTexture("_NoiseTexture", RuntimeSetting.noise.texture);
				m_Material.SetFloat("_NoiseScaleX", RuntimeSetting.noise.scale.x);
				m_Material.SetFloat("_NoiseScaleY", RuntimeSetting.noise.scale.y);
				m_Material.SetFloat("_SurfacePositionX", surfaceTextureRenderOffset.x);
				m_Material.SetFloat("_SurfacePositionY", surfaceTextureRenderOffset.y);
				m_Material.SetFloat("_NoisePositionX", noiseTextureRenderOffset.x);
				m_Material.SetFloat("_NoisePositionY", noiseTextureRenderOffset.y);
				cmd.Blit(source, destination, m_Material, 0);
			}
		}

		public override void Override(VolumeComponent state, float interpFactor)
		{
			base.Override(state, interpFactor);
			FlexibleNoiseEdge flexibleNoiseEdge = state as FlexibleNoiseEdge;
			flexibleNoiseEdge.masterVolume.SetValue(new FloatParameter(interpFactor * masterVolume.value, overrideState: true));
			flexibleNoiseEdge.isPlayableRunning = isPlayableRunning;
			flexibleNoiseEdge.timeOffset = timeOffset;
			if (flexibleNoiseEdge.cachedNoiseSetting != null)
			{
				flexibleNoiseEdge.cachedNoiseSetting.vignette = setting.value.vignette;
				flexibleNoiseEdge.cachedNoiseSetting.surface = setting.value.surface;
				flexibleNoiseEdge.cachedNoiseSetting.noise = setting.value.noise;
			}
		}
	}
}
