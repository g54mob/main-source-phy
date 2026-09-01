using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	[Serializable]
	[PostProcess(typeof(BlackBarsRenderer), PostProcessEvent.AfterStack, "SC Post Effects/Screen/Black Bars", true)]
	public sealed class BlackBars : PostProcessEffectSettings
	{
		public enum Direction
		{
			Horizontal = 0,
			Vertical = 1
		}

		[Serializable]
		public sealed class DirectionParam : ParameterOverride<Direction>
		{
		}

		[DisplayName("Direction")]
		[Tooltip("")]
		public DirectionParam mode = new DirectionParam
		{
			value = Direction.Horizontal
		};

		[Range(0f, 1f)]
		[Tooltip("Size")]
		public FloatParameter size = new FloatParameter
		{
			value = 1f
		};

		[Range(0f, 1f)]
		[Tooltip("Max Size")]
		public FloatParameter maxSize = new FloatParameter
		{
			value = 0.33f
		};

		public override bool IsEnabledAndSupported(PostProcessRenderContext context)
		{
			if (enabled.value)
			{
				if ((float)size == 0f || (float)maxSize == 0f)
				{
					return false;
				}
				return true;
			}
			return false;
		}
	}
}
