using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	[Serializable]
	[PostProcess(typeof(PixelizeRenderer), PostProcessEvent.BeforeStack, "SC Post Effects/Retro/Pixelize", true)]
	public sealed class Pixelize : PostProcessEffectSettings
	{
		[Range(0f, 1f)]
		[Tooltip("Amount")]
		public FloatParameter amount = new FloatParameter
		{
			value = 0.05f
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
