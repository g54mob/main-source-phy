using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace HDRPVolumePostProcessPack
{
	public abstract class GaussianBlurBase : CustomPostProcessVolumeComponent, IPostProcessComponent, ICustomVolume
	{
		public ClampedFloatParameter intensity = new ClampedFloatParameter(0f, 0f, 1f, overrideState: true);

		public ClampedIntParameter sampleCount = new ClampedIntParameter(16, 0, 24);

		public ClampedFloatParameter sampleInterval = new ClampedFloatParameter(1f, 0.01f, 8f);

		public ClampedFloatParameter standardDeviation = new ClampedFloatParameter(8f, 0f, 300f);

		public ClampedFloatParameter brightness = new ClampedFloatParameter(0.5f, 0f, 2f);

		public Material m_Material;

		private const string kShaderName = "Hidden/HDRP Volume Post-Process Pack/Gaussian Blur";

		public const int MAX_SAMPLE_COUNT = 24;

		public float[] gaussianWeights = new float[24];

		public override CustomPostProcessInjectionPoint injectionPoint => CustomPostProcessInjectionPoint.AfterPostProcess;

		public string ShaderName => "Hidden/HDRP Volume Post-Process Pack/Gaussian Blur";

		public bool IsActive()
		{
			if (active && m_Material != null)
			{
				return intensity.value > 0f;
			}
			return false;
		}

		public override void Setup()
		{
			if (Shader.Find("Hidden/HDRP Volume Post-Process Pack/Gaussian Blur") != null)
			{
				m_Material = new Material(Shader.Find("Hidden/HDRP Volume Post-Process Pack/Gaussian Blur"));
			}
			else
			{
				Debug.LogError("Unable to find shader 'Hidden/HDRP Volume Post-Process Pack/Gaussian Blur'. Post Process Volume Gaussian Blur is unable to load.");
			}
			RefreshBlurWeight();
		}

		private void OnValidate()
		{
			RefreshBlurWeight();
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			RefreshBlurWeight();
		}

		private float CalcGaussianWeight(float dist)
		{
			return Mathf.Exp((0f - dist) * dist / (2f * standardDeviation.value * standardDeviation.value)) / standardDeviation.value;
		}

		private void RefreshBlurWeight()
		{
			if (gaussianWeights.Length < 24)
			{
				gaussianWeights = new float[24];
			}
			for (int i = 0; i < sampleCount.value; i++)
			{
				gaussianWeights[i] = CalcGaussianWeight(i);
			}
		}

		protected void SetMaterialProperties(Material mat)
		{
			if (gaussianWeights == null || float.IsNaN(gaussianWeights[0]))
			{
				RefreshBlurWeight();
			}
			mat.SetFloat("_Intensity", intensity.value);
			mat.SetFloatArray("_GaussianWeights", gaussianWeights);
			mat.SetInt("_SampleCount", sampleCount.value);
			mat.SetFloat("_SampleInterval", sampleInterval.value);
			mat.SetFloat("_Brightness", brightness.value);
		}

		public override void Cleanup()
		{
			CoreUtils.Destroy(m_Material);
		}
	}
}
