using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	[Serializable]
	[PostProcess(typeof(RefractionRenderer), PostProcessEvent.AfterStack, "SC Post Effects/Screen/Refraction", true)]
	public sealed class Refraction : PostProcessEffectSettings
	{
		[Tooltip("Takes a DUDV map (normal map without a blue channel) to perturb the image")]
		public TextureParameter refractionTex = new TextureParameter
		{
			value = null
		};

		[DisplayName("Using normal map")]
		[Tooltip("In the absense of a DUDV map, the supplied normal map can be converted in the shader")]
		public BoolParameter convertNormalMap = new BoolParameter
		{
			value = false
		};

		[Range(0f, 1f)]
		[Tooltip("Amount")]
		public FloatParameter amount = new FloatParameter
		{
			value = 1f
		};

		public override bool IsEnabledAndSupported(PostProcessRenderContext context)
		{
			if (enabled.value)
			{
				if ((float)amount == 0f || refractionTex.value == null)
				{
					return false;
				}
				return true;
			}
			return false;
		}
	}
}
