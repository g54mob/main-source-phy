using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	[Serializable]
	[PostProcess(typeof(HueShift3DRenderer), PostProcessEvent.AfterStack, "SC Post Effects/Image/3D Hue Shift", true)]
	public sealed class HueShift3D : PostProcessEffectSettings
	{
		[Range(0f, 1f)]
		[DisplayName("Opacity")]
		public FloatParameter intensity = new FloatParameter
		{
			value = 0.33f
		};

		[Range(0f, 1f)]
		[Tooltip("Speed")]
		public FloatParameter speed = new FloatParameter
		{
			value = 0.3f
		};

		[Range(0f, 3f)]
		[Tooltip("Size")]
		public FloatParameter size = new FloatParameter
		{
			value = 1f
		};

		[DisplayName("Geometry normal influence")]
		[Range(0f, 10f)]
		[Tooltip("Bends the effect over the scene's geometry normals\n\nHigh values may induce banding artifacts")]
		public FloatParameter geoInfluence = new FloatParameter
		{
			value = 5f
		};

		public static bool isOrtho;

		public override bool IsEnabledAndSupported(PostProcessRenderContext context)
		{
			if (enabled.value)
			{
				if ((float)intensity == 0f)
				{
					return false;
				}
				return true;
			}
			return false;
		}
	}
}
