using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	[Serializable]
	[PostProcess(typeof(TubeDistortionRenderer), PostProcessEvent.AfterStack, "SC Post Effects/Screen/Tube Distortion", true)]
	public sealed class TubeDistortion : PostProcessEffectSettings
	{
		public enum DistortionMode
		{
			Buldged = 0,
			Pinched = 1,
			Beveled = 2
		}

		[Serializable]
		public sealed class DistortionModeParam : ParameterOverride<DistortionMode>
		{
		}

		public DistortionModeParam mode = new DistortionModeParam
		{
			value = DistortionMode.Buldged
		};

		[Range(0f, 1f)]
		public FloatParameter amount = new FloatParameter
		{
			value = 0.5f
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
