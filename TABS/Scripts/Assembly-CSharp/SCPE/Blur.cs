using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	[Serializable]
	[PostProcess(typeof(BlurRenderer), PostProcessEvent.AfterStack, "SC Post Effects/Blurring/Blur", true)]
	public sealed class Blur : PostProcessEffectSettings
	{
		public enum BlurMethod
		{
			Gaussian = 0,
			Box = 1
		}

		[Serializable]
		public sealed class BlurMethodParameter : ParameterOverride<BlurMethod>
		{
		}

		[DisplayName("Method")]
		[Tooltip("Box blurring uses fewer texture samples but has a limited blur range")]
		public BlurMethodParameter mode = new BlurMethodParameter
		{
			value = BlurMethod.Gaussian
		};

		[Tooltip("When enabled, the amount of blur passes is doubled")]
		public BoolParameter highQuality = new BoolParameter
		{
			value = false
		};

		[Space]
		[Range(0f, 5f)]
		[Tooltip("The amount of blurring that must be performed")]
		public FloatParameter amount = new FloatParameter
		{
			value = 3f
		};

		[Range(1f, 12f)]
		[Tooltip("The number of times the effect is blurred. More iterations provide a smoother effect but induce more drawcalls.")]
		public IntParameter iterations = new IntParameter
		{
			value = 6
		};

		[Range(1f, 8f)]
		[Tooltip("Every step halfs the resolution of the blur effect. Lower resolution provides a smoother blur but may induce flickering.")]
		public IntParameter downscaling = new IntParameter
		{
			value = 2
		};

		public override bool IsEnabledAndSupported(PostProcessRenderContext context)
		{
			if (enabled.value && (float)amount > 0f)
			{
				return true;
			}
			return false;
		}
	}
}
