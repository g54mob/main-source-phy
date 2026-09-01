using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	[Serializable]
	[PostProcess(typeof(AmbientOcclusion2DRenderer), PostProcessEvent.AfterStack, "SC Post Effects/Rendering/Ambient Occlusion 2D", true)]
	public sealed class AmbientOcclusion2D : PostProcessEffectSettings
	{
		[DisplayName("Debug")]
		[Tooltip("Shows only the effect, to alow for finetuning")]
		public BoolParameter aoOnly = new BoolParameter
		{
			value = false
		};

		[Header("Luminance-Based Amient Occlusion")]
		[Range(0f, 1f)]
		[Tooltip("Intensity")]
		public FloatParameter intensity = new FloatParameter
		{
			value = 0.5f
		};

		[Range(0.01f, 1f)]
		[Tooltip("Luminance threshold, pixels above this threshold will contribute to the effect")]
		public FloatParameter luminanceThreshold = new FloatParameter
		{
			value = 0.05f
		};

		[Range(0f, 3f)]
		[Tooltip("Distance")]
		public FloatParameter distance = new FloatParameter
		{
			value = 1f
		};

		[Header("Blur")]
		[Range(0f, 3f)]
		[DisplayName("Blur")]
		[Tooltip("The amount of blurring that must be performed")]
		public FloatParameter blurAmount = new FloatParameter
		{
			value = 3f
		};

		[Range(1f, 8f)]
		[Tooltip("The number of times the effect is blurred. More iterations provide a smoother effect but induce more drawcalls.")]
		public IntParameter iterations = new IntParameter
		{
			value = 4
		};

		[Range(1f, 8f)]
		[Tooltip("Every step halfs the resolution of the blur effect. Lower resolution provides a smoother blur but may induce flickering.")]
		public IntParameter downscaling = new IntParameter
		{
			value = 2
		};

		public override bool IsEnabledAndSupported(PostProcessRenderContext context)
		{
			if (enabled.value)
			{
				if ((float)intensity == 0f)
				{
					return false;
				}
				return true;
			}
			return false;
		}
	}
}
