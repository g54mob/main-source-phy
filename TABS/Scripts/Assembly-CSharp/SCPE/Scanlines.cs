using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	[Serializable]
	[PostProcess(typeof(ScanlinesRenderer), PostProcessEvent.AfterStack, "SC Post Effects/Retro/Scanlines", true)]
	public sealed class Scanlines : PostProcessEffectSettings
	{
		[Range(0f, 1f)]
		[Tooltip("Intensity")]
		public FloatParameter intensity = new FloatParameter
		{
			value = 0.5f
		};

		[Range(0f, 2048f)]
		[DisplayName("Lines")]
		public FloatParameter amount = new FloatParameter
		{
			value = 700f
		};

		[Range(0f, 1f)]
		[Tooltip("Animation speed")]
		public FloatParameter speed = new FloatParameter
		{
			value = 0f
		};

		public override bool IsEnabledAndSupported(PostProcessRenderContext context)
		{
			if (enabled.value)
			{
				if (intensity.value == 0f)
				{
					return false;
				}
				return true;
			}
			return false;
		}
	}
}
