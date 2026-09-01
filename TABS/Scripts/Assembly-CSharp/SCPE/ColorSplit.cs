using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	[Serializable]
	[PostProcess(typeof(ColorSplitRenderer), PostProcessEvent.AfterStack, "SC Post Effects/Retro/Color Split", true)]
	public sealed class ColorSplit : PostProcessEffectSettings
	{
		public enum SplitMode
		{
			Single = 0,
			SingleBoxFiltered = 1,
			Double = 2,
			DoubleBoxFiltered = 3
		}

		[Serializable]
		public sealed class SplitModeParam : ParameterOverride<SplitMode>
		{
		}

		[DisplayName("Method")]
		[Tooltip("Box filtered methods provide a subtle blur effect and are less efficient")]
		public SplitModeParam mode = new SplitModeParam
		{
			value = SplitMode.Single
		};

		[Range(0f, 1f)]
		[Tooltip("The amount by which the color channels offset")]
		public FloatParameter offset = new FloatParameter
		{
			value = 1f
		};

		public override bool IsEnabledAndSupported(PostProcessRenderContext context)
		{
			if (enabled.value)
			{
				if ((float)offset == 0f)
				{
					return false;
				}
				return true;
			}
			return false;
		}
	}
}
