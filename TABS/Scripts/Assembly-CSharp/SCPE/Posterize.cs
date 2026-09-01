using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	[Serializable]
	[PostProcess(typeof(PosterizeRenderer), PostProcessEvent.BeforeStack, "SC Post Effects/Retro/Posterize", true)]
	public sealed class Posterize : PostProcessEffectSettings
	{
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
