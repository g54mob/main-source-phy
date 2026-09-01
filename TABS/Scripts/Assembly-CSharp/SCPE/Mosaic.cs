using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	[Serializable]
	[PostProcess(typeof(MosaicRenderer), PostProcessEvent.AfterStack, "SC Post Effects/Stylized/Mosaic", true)]
	public sealed class Mosaic : PostProcessEffectSettings
	{
		public enum MosaicMode
		{
			Triangles = 0,
			Hexagons = 1,
			Circles = 2
		}

		[Serializable]
		public sealed class MosaicModeParam : ParameterOverride<MosaicMode>
		{
		}

		[DisplayName("Method")]
		[Tooltip("")]
		public MosaicModeParam mode = new MosaicModeParam
		{
			value = MosaicMode.Hexagons
		};

		[Range(0f, 1f)]
		[Tooltip("Size")]
		public FloatParameter size = new FloatParameter
		{
			value = 0.075f
		};

		public override bool IsEnabledAndSupported(PostProcessRenderContext context)
		{
			if (enabled.value)
			{
				if ((float)size == 0f)
				{
					return false;
				}
				return true;
			}
			return false;
		}
	}
}
