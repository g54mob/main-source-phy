using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	[Serializable]
	[PostProcess(typeof(KuwaharaRenderer), PostProcessEvent.AfterStack, "SC Post Effects/Stylized/Kuwahara", true)]
	public sealed class Kuwahara : PostProcessEffectSettings
	{
		public enum KuwaharaMode
		{
			Regular = 0,
			DepthFade = 1
		}

		[Serializable]
		public sealed class KuwaharaModeParam : ParameterOverride<KuwaharaMode>
		{
		}

		[DisplayName("Method")]
		[Tooltip("Choose to apply the effect to the entire screen, or fade in/out over a distance")]
		public KuwaharaModeParam mode = new KuwaharaModeParam
		{
			value = KuwaharaMode.Regular
		};

		[Range(0f, 8f)]
		[DisplayName("Radius")]
		public IntParameter radius = new IntParameter
		{
			value = 5
		};

		public BoolParameter invertFadeDistance = new BoolParameter
		{
			value = false
		};

		[DisplayName("Fade distance")]
		public FloatParameter fadeDistance = new FloatParameter
		{
			value = 1000f
		};

		public override bool IsEnabledAndSupported(PostProcessRenderContext context)
		{
			if (enabled.value)
			{
				if ((int)radius == 0)
				{
					return false;
				}
				return true;
			}
			return false;
		}
	}
}
