using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	[Serializable]
	[PostProcess(typeof(KaleidoscopeRenderer), PostProcessEvent.AfterStack, "SC Post Effects/Misc/Kaleidoscope", true)]
	public sealed class Kaleidoscope : PostProcessEffectSettings
	{
		[Range(0f, 10f)]
		[Tooltip("The number of times the screen is split up")]
		public IntParameter splits = new IntParameter
		{
			value = 5
		};

		public override bool IsEnabledAndSupported(PostProcessRenderContext context)
		{
			if (enabled.value)
			{
				if ((int)splits == 0)
				{
					return false;
				}
				return true;
			}
			return false;
		}
	}
}
