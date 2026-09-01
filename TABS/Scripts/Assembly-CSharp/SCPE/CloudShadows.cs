using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	[Serializable]
	[PostProcess(typeof(CloudShadowsRenderer), PostProcessEvent.BeforeStack, "SC Post Effects/Environment/Cloud Shadows", true)]
	public sealed class CloudShadows : PostProcessEffectSettings
	{
		[DisplayName("Texture (R)")]
		[Tooltip("The red channel of this texture is used to sample the clouds")]
		public TextureParameter texture = new TextureParameter
		{
			value = null
		};

		[Space]
		[Range(0f, 1f)]
		[DisplayName("Size")]
		public FloatParameter size = new FloatParameter
		{
			value = 0.5f
		};

		[Range(0f, 1f)]
		[DisplayName("Density")]
		public FloatParameter density = new FloatParameter
		{
			value = 0.5f
		};

		[Range(0f, 1f)]
		[DisplayName("Speed")]
		public FloatParameter speed = new FloatParameter
		{
			value = 0.5f
		};

		[DisplayName("Direction")]
		[Tooltip("Set the X and Z world-space direction the clouds should move in")]
		public Vector2Parameter direction = new Vector2Parameter
		{
			value = new Vector2(0f, 1f)
		};

		public static bool isOrtho;

		public override bool IsEnabledAndSupported(PostProcessRenderContext context)
		{
			if (enabled.value)
			{
				if ((float)density == 0f || texture.value == null)
				{
					return false;
				}
				return true;
			}
			return false;
		}
	}
}
