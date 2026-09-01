using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	[Serializable]
	[PostProcess(typeof(RadialBlurRenderer), PostProcessEvent.AfterStack, "SC Post Effects/Blurring/Radial Blur", true)]
	public sealed class RadialBlur : PostProcessEffectSettings
	{
		[Range(0f, 1f)]
		public FloatParameter amount = new FloatParameter
		{
			value = 0.5f
		};

		[Range(3f, 12f)]
		public IntParameter iterations = new IntParameter
		{
			value = 6
		};

		public override bool IsEnabledAndSupported(PostProcessRenderContext context)
		{
			if (enabled.value)
			{
				if ((float)amount == 0f)
				{
					return false;
				}
				return true;
			}
			return false;
		}
	}
}
