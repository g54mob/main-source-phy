using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	[Serializable]
	[PostProcess(typeof(TiltShiftRenderer), PostProcessEvent.AfterStack, "SC Post Effects/Blurring/Tilt Shift", true)]
	public class TiltShift : PostProcessEffectSettings
	{
		public enum TiltShiftMethod
		{
			Horizontal = 0,
			Radial = 1
		}

		[Serializable]
		public sealed class TiltShifMethodParameter : ParameterOverride<TiltShiftMethod>
		{
		}

		[DisplayName("Method")]
		public TiltShifMethodParameter mode = new TiltShifMethodParameter
		{
			value = TiltShiftMethod.Horizontal
		};

		[Space]
		[Range(0f, 1f)]
		public FloatParameter areaSize = new FloatParameter
		{
			value = 1f
		};

		[Range(0f, 1f)]
		[Tooltip("The amount of blurring that must be performed")]
		public FloatParameter amount = new FloatParameter
		{
			value = 0.5f
		};

		public override bool IsEnabledAndSupported(PostProcessRenderContext context)
		{
			if (enabled.value)
			{
				if ((float)amount == 0f || (float)areaSize == 0f)
				{
					return false;
				}
				return true;
			}
			return false;
		}
	}
}
