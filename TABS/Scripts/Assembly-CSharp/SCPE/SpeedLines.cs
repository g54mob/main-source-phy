using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	[Serializable]
	[PostProcess(typeof(SpeedLinesRenderer), PostProcessEvent.AfterStack, "SC Post Effects/Screen/Speed Lines", true)]
	public sealed class SpeedLines : PostProcessEffectSettings
	{
		[Range(0f, 1f)]
		public FloatParameter intensity = new FloatParameter
		{
			value = 1f
		};

		[Range(0f, 1f)]
		[Tooltip("Determines the radial tiling of the noise texture")]
		public FloatParameter size = new FloatParameter
		{
			value = 0.5f
		};

		[Range(0f, 1f)]
		public FloatParameter falloff = new FloatParameter
		{
			value = 0.25f
		};

		[Tooltip("Assign any grayscale texture with a vertically repeating pattern and a falloff from left to right")]
		public TextureParameter noiseTex = new TextureParameter
		{
			value = null
		};

		public override bool IsEnabledAndSupported(PostProcessRenderContext context)
		{
			if (enabled.value)
			{
				if ((float)intensity == 0f || noiseTex.value == null)
				{
					return false;
				}
				return true;
			}
			return false;
		}
	}
}
