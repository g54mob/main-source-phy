using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace INab.ToonDetailer.URP
{
	[VolumeComponentMenu("INab Studio/Toon Detailer")]
	[VolumeRequiresRendererFeatures(new Type[] { typeof(ToonDetailer) })]
	[SupportedOnRenderPipeline(typeof(UniversalRenderPipelineAsset))]
	public sealed class ToonDetailerVolumeComponent : VolumeComponent, IPostProcessComponent
	{
		[SerializeField]
		public ColorParameter _ColorHue;

		[SerializeField]
		public BoolParameter _FadeAffectsOnlyContours;

		[SerializeField]
		public FloatParameter _FadeStart;

		[SerializeField]
		public FloatParameter _FadeEnd;

		public ClampedFloatParameter _BlackOffset;

		public ClampedFloatParameter _ContoursIntensity;

		public ClampedFloatParameter _ContoursThickness;

		public ClampedFloatParameter _ContoursElevationStrength;

		public ClampedFloatParameter _ContoursElevationSmoothness;

		public ClampedFloatParameter _ContoursDepressionStrength;

		public ClampedFloatParameter _ContoursDepressionSmoothness;

		public ClampedFloatParameter _CavityIntensity;

		public ClampedFloatParameter _CavityRadius;

		public ClampedFloatParameter _CavityStrength;

		public ClampedIntParameter _CavitySamples;

		public bool IsActive()
		{
			return false;
		}
	}
}
