using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	[Serializable]
	[PostProcess(typeof(RipplesRenderer), PostProcessEvent.AfterStack, "SC Post Effects/Screen/Ripples", true)]
	public sealed class Ripples : PostProcessEffectSettings
	{
		public enum RipplesMode
		{
			Radial = 0,
			OmniDirectional = 1
		}

		[Serializable]
		public sealed class RipplesModeParam : ParameterOverride<RipplesMode>
		{
		}

		[DisplayName("Method")]
		public RipplesModeParam mode = new RipplesModeParam
		{
			value = RipplesMode.Radial
		};

		[Range(0f, 10f)]
		[DisplayName("Intensity")]
		public FloatParameter strength = new FloatParameter
		{
			value = 2f
		};

		[Range(1f, 10f)]
		[Tooltip("The frequency of the waves")]
		public FloatParameter distance = new FloatParameter
		{
			value = 5f
		};

		[Range(0f, 10f)]
		[Tooltip("Speed")]
		public FloatParameter speed = new FloatParameter
		{
			value = 3f
		};

		[Range(0f, 5f)]
		[Tooltip("Width")]
		public FloatParameter width = new FloatParameter
		{
			value = 1.5f
		};

		[Range(0f, 5f)]
		[Tooltip("Height")]
		public FloatParameter height = new FloatParameter
		{
			value = 1f
		};

		public override bool IsEnabledAndSupported(PostProcessRenderContext context)
		{
			if (enabled.value)
			{
				if ((float)strength == 0f)
				{
					return false;
				}
				return true;
			}
			return false;
		}
	}
}
