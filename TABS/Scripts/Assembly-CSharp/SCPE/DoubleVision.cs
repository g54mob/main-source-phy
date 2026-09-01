using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	[Serializable]
	[PostProcess(typeof(DoubleVisionRenderer), PostProcessEvent.AfterStack, "SC Post Effects/Blurring/Double Vision", true)]
	public sealed class DoubleVision : PostProcessEffectSettings
	{
		public enum Mode
		{
			FullScreen = 0,
			Edges = 1
		}

		[Serializable]
		public sealed class DoubleVisionMode : ParameterOverride<Mode>
		{
		}

		[DisplayName("Method")]
		[Tooltip("Choose to apply the effect over the entire screen or just the edges")]
		public DoubleVisionMode mode = new DoubleVisionMode
		{
			value = Mode.FullScreen
		};

		[Range(0f, 1f)]
		[Tooltip("Intensity")]
		public FloatParameter intensity = new FloatParameter
		{
			value = 0.1f
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
