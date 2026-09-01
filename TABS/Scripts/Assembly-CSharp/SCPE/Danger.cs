using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	[Serializable]
	[PostProcess(typeof(DangerRenderer), PostProcessEvent.AfterStack, "SC Post Effects/Screen/Danger", true)]
	public sealed class Danger : PostProcessEffectSettings
	{
		public TextureParameter overlayTex = new TextureParameter
		{
			value = null
		};

		public ColorParameter color = new ColorParameter
		{
			value = new Color(0.66f, 0f, 0f)
		};

		[Range(0f, 1f)]
		[DisplayName("Opacity")]
		public FloatParameter intensity = new FloatParameter
		{
			value = 1f
		};

		[Range(0f, 1f)]
		[Tooltip("Size")]
		public FloatParameter size = new FloatParameter
		{
			value = 1f
		};

		public override bool IsEnabledAndSupported(PostProcessRenderContext context)
		{
			if (enabled.value)
			{
				if ((float)size == 0f || (float)intensity == 0f)
				{
					return false;
				}
				return true;
			}
			return false;
		}
	}
}
