using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	[Serializable]
	[PostProcess(typeof(GradientRenderer), PostProcessEvent.AfterStack, "SC Post Effects/Screen/Gradient", true)]
	public sealed class Gradient : PostProcessEffectSettings
	{
		public enum Mode
		{
			ColorFields = 0,
			Texture = 1
		}

		[Serializable]
		public sealed class GradientModeParameter : ParameterOverride<Mode>
		{
		}

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

		[Range(0f, 1f)]
		[DisplayName("Opacity")]
		public FloatParameter intensity = new FloatParameter
		{
			value = 1f
		};

		[Space]
		[Tooltip("Set the color either through 2 color fields, or a gradient texture")]
		public GradientModeParameter input = new GradientModeParameter
		{
			value = Mode.ColorFields
		};

		[Tooltip("The color's alpha channel controls its opacity")]
		public ColorParameter color1 = new ColorParameter
		{
			value = new Color(0f, 0.8f, 0.56f, 0.5f)
		};

		[Tooltip("The color's alpha channel controls its opacity")]
		public ColorParameter color2 = new ColorParameter
		{
			value = new Color(0.81f, 0.37f, 1f, 0.5f)
		};

		[Range(0f, 1f)]
		[Space]
		[Tooltip("Controls the rotation of the gradient")]
		public FloatParameter rotation = new FloatParameter
		{
			value = 0f
		};

		[DisplayName("Gradient")]
		[Tooltip("")]
		public TextureParameter gradientTex = new TextureParameter
		{
			value = null
		};

		[Tooltip("Blends the gradient through various Photoshop-like blending modes")]
		public BlendModeParameter mode = new BlendModeParameter
		{
			value = BlendMode.Linear
		};

		private const int RESOLUTION = 64;

		public override bool IsEnabledAndSupported(PostProcessRenderContext context)
		{
			if (enabled.value)
			{
				if ((float)intensity == 0f || (input.value == Mode.Texture && gradientTex.value == null))
				{
					return false;
				}
				return true;
			}
			return false;
		}
	}
}
