using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	[Serializable]
	[PostProcess(typeof(ColorizeRenderer), PostProcessEvent.AfterStack, "SC Post Effects/Image/Colorize", true)]
	public sealed class Colorize : PostProcessEffectSettings
	{
		public enum BlendMode
		{
			Linear = 0,
			Additive = 1,
			Multiply = 2,
			Screen = 3
		}

		[Serializable]
		public sealed class BlendModeParameter : ParameterOverride<BlendMode>
		{
		}

		[Tooltip("Blends the gradient through various Photoshop-like blending modes")]
		public BlendModeParameter mode = new BlendModeParameter
		{
			value = BlendMode.Linear
		};

		[Range(0f, 1f)]
		[Tooltip("Fades the effect in or out")]
		public FloatParameter intensity = new FloatParameter
		{
			value = 1f
		};

		[Tooltip("Supply a gradient texture.\n\nLuminance values are colorized from left to right")]
		public TextureParameter colorRamp = new TextureParameter
		{
			value = null
		};

		public override bool IsEnabledAndSupported(PostProcessRenderContext context)
		{
			if (enabled.value)
			{
				if ((float)intensity == 0f || !colorRamp.value)
				{
					return false;
				}
				return true;
			}
			return false;
		}
	}
}
