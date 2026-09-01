using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	[Serializable]
	[PostProcess(typeof(InvertRenderer), PostProcessEvent.AfterStack, "SC Post Effects/Misc/Invert", true)]
	public sealed class Invert : PostProcessEffectSettings
	{
		[Range(0f, 1f)]
		public FloatParameter amount = new FloatParameter
		{
			value = 1f
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
