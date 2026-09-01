using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	[Serializable]
	[PostProcess(typeof(OverlayRenderer), PostProcessEvent.AfterStack, "SC Post Effects/Screen/Overlay", true)]
	public sealed class Overlay : PostProcessEffectSettings
	{
		public enum BlendMode
		{
			Linear = 0,
			Additive = 1,
			Multiply = 2,
			Screen = 3
		}

		[Serializable]
		public sealed class BlendModeParameter : ParameterOverride<BlendMode>
		{
		}

		[Tooltip("The texture's alpha channel controls its opacity")]
		public TextureParameter overlayTex = new TextureParameter
		{
			value = null
		};

		[Tooltip("Maintains the image aspect ratio, regardless of the screen width")]
		public BoolParameter autoAspect = new BoolParameter
		{
			value = false
		};

		[Tooltip("Blends the gradient through various Photoshop-like blending modes")]
		public BlendModeParameter blendMode = new BlendModeParameter
		{
			value = BlendMode.Linear
		};

		[Range(0f, 1f)]
		public FloatParameter intensity = new FloatParameter
		{
			value = 1f
		};

		[Range(0f, 1f)]
		public FloatParameter tiling = new FloatParameter
		{
			value = 0f
		};

		public override bool IsEnabledAndSupported(PostProcessRenderContext context)
		{
			if (enabled.value)
			{
				if (overlayTex.value == null || (float)intensity == 0f)
				{
					return false;
				}
				return true;
			}
			return false;
		}
	}
}
