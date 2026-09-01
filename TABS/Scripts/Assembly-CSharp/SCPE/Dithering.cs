using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	[Serializable]
	[PostProcess(typeof(DitheringRenderer), PostProcessEvent.AfterStack, "SC Post Effects/Retro/Dithering", true)]
	public sealed class Dithering : PostProcessEffectSettings
	{
		[DisplayName("Pattern")]
		[Tooltip("Note that the texture's filter mode (Point or Bilinear) greatly affects the behavior of the pattern")]
		public TextureParameter lut = new TextureParameter
		{
			value = null
		};

		[Range(0f, 1f)]
		[Tooltip("The screen's luminance values control the density of the dithering matrix")]
		public FloatParameter luminanceThreshold = new FloatParameter
		{
			value = 0.5f
		};

		[Range(0f, 1f)]
		[Tooltip("Fades the effect in or out")]
		public FloatParameter intensity = new FloatParameter
		{
			value = 0.5f
		};

		[Range(0f, 2f)]
		[DisplayName("Tiling")]
		public FloatParameter tiling = new FloatParameter
		{
			value = 1f
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
