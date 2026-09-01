using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	[Serializable]
	[PostProcess(typeof(SketchRenderer), PostProcessEvent.BeforeStack, "SC Post Effects/Stylized/Sketch", true)]
	public sealed class Sketch : PostProcessEffectSettings
	{
		public enum SketchProjectionMode
		{
			WorldSpace = 0,
			ScreenSpace = 1
		}

		[Serializable]
		public sealed class SketchProjectioParameter : ParameterOverride<SketchProjectionMode>
		{
		}

		public enum SketchMode
		{
			EffectOnly = 0,
			Multiply = 1,
			Add = 2
		}

		[Serializable]
		public sealed class SketchModeParameter : ParameterOverride<SketchMode>
		{
		}

		[Tooltip("The Red channel is used for darker shades, whereas the Green channel is for lighter.")]
		public TextureParameter strokeTex = new TextureParameter
		{
			value = null
		};

		[Space]
		[Tooltip("Choose the type of UV space being used")]
		public SketchProjectioParameter projectionMode = new SketchProjectioParameter
		{
			value = SketchProjectionMode.WorldSpace
		};

		[Tooltip("Choose one of the different modes")]
		public SketchModeParameter blendMode = new SketchModeParameter
		{
			value = SketchMode.EffectOnly
		};

		[Space]
		[Range(0f, 1f)]
		public FloatParameter intensity = new FloatParameter
		{
			value = 1f
		};

		public Vector2Parameter brightness = new Vector2Parameter
		{
			value = new Vector2(0f, 1f)
		};

		[Range(1f, 32f)]
		public FloatParameter tiling = new FloatParameter
		{
			value = 8f
		};

		public override bool IsEnabledAndSupported(PostProcessRenderContext context)
		{
			if (enabled.value)
			{
				if ((float)intensity == 0f || strokeTex.value == null)
				{
					return false;
				}
				return true;
			}
			return false;
		}
	}
}
