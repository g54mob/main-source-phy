using Beautify.Universal;
using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.Rendering;

namespace MoreMountains.FeedbacksForThirdParty
{
	[RequireComponent(typeof(Volume))]
	[AddComponentMenu("More Mountains/Feedbacks/Shakers/PostProcessing/MM Beautify Shaker")]
	public class MMBeautifyShaker : MMShaker
	{
		public bool RelativeValues;

		[MMInspectorGroup("Bloom Intensity", true, 60, false)]
		[Tooltip("the curve used to animate the bloom intensity value on")]
		public AnimationCurve ShakeBloomIntensity;

		[Tooltip("the value to remap the curve's 0 to")]
		public float RemapBloomIntensityZero;

		[Tooltip("the value to remap the curve's 1 to")]
		public float RemapBloomIntensityOne;

		[MMInspectorGroup("Bloom Threshold", true, 61, false)]
		[Tooltip("the curve used to animate the bloom threshold value on")]
		public AnimationCurve ShakeBloomThreshold;

		[Tooltip("the value to remap the curve's 0 to")]
		public float RemapBloomThresholdZero;

		[Tooltip("the value to remap the curve's 1 to")]
		public float RemapBloomThresholdOne;

		[MMInspectorGroup("Chromatic Aberration Intensity", true, 62, false)]
		[Tooltip("the curve used to animate the chromatic aberration intensity value on")]
		public AnimationCurve ShakeChromaticAberration;

		[Tooltip("the value to remap the curve's 0 to. Beautify clamps chromaticAberrationIntensity to [0, 0.1]")]
		[Range(0f, 0.1f)]
		public float RemapChromaticAberrationZero;

		[Tooltip("the value to remap the curve's 1 to. Beautify clamps chromaticAberrationIntensity to [0, 0.1]. 0.05 is strongly visible")]
		[Range(0f, 0.1f)]
		public float RemapChromaticAberrationOne;

		[MMInspectorGroup("Creative Blur Intensity", true, 63, false)]
		[Tooltip("the curve used to animate the creative blur intensity value on")]
		public AnimationCurve ShakeCreativeBlur;

		[Tooltip("the value to remap the curve's 0 to. Beautify's blurIntensity ranges from 0 to 64")]
		[Range(0f, 64f)]
		public float RemapCreativeBlurZero;

		[Tooltip("the value to remap the curve's 1 to. Beautify's blurIntensity ranges from 0 to 64. 8-16 is strongly visible")]
		[Range(0f, 64f)]
		public float RemapCreativeBlurOne;

		[MMInspectorGroup("Anamorphic Flares Intensity", true, 64, false)]
		[Tooltip("the curve used to animate the anamorphic flares intensity value on")]
		public AnimationCurve ShakeAnamorphicFlaresIntensity;

		[Tooltip("the value to remap the curve's 0 to")]
		public float RemapAnamorphicFlaresIntensityZero;

		[Tooltip("the value to remap the curve's 1 to. No hard cap; 1-3 is a practical burst range")]
		public float RemapAnamorphicFlaresIntensityOne;

		protected Volume _volume;

		protected Beautify.Universal.Beautify _beautify;

		protected float _initialBloomIntensity;

		protected float _initialBloomThreshold;

		protected float _initialChromaticAberration;

		protected float _initialCreativeBlur;

		protected float _initialAnamorphicFlaresIntensity;

		protected float _originalShakeDuration;

		protected bool _originalRelativeValues;

		protected AnimationCurve _originalShakeBloomIntensity;

		protected float _originalRemapBloomIntensityZero;

		protected float _originalRemapBloomIntensityOne;

		protected AnimationCurve _originalShakeBloomThreshold;

		protected float _originalRemapBloomThresholdZero;

		protected float _originalRemapBloomThresholdOne;

		protected AnimationCurve _originalShakeChromaticAberration;

		protected float _originalRemapChromaticAberrationZero;

		protected float _originalRemapChromaticAberrationOne;

		protected AnimationCurve _originalShakeCreativeBlur;

		protected float _originalRemapCreativeBlurZero;

		protected float _originalRemapCreativeBlurOne;

		protected AnimationCurve _originalShakeAnamorphicFlaresIntensity;

		protected float _originalRemapAnamorphicFlaresIntensityZero;

		protected float _originalRemapAnamorphicFlaresIntensityOne;

		protected override void Initialization()
		{
		}

		protected override void Shake()
		{
		}

		protected override void GrabInitialValues()
		{
		}

		public virtual void OnMMBeautifyShakeEvent(AnimationCurve bloomIntensityCurve, float remapBloomIntensityZero, float remapBloomIntensityOne, AnimationCurve bloomThresholdCurve, float remapBloomThresholdZero, float remapBloomThresholdOne, AnimationCurve chromaticCurve, float remapChromaticZero, float remapChromaticOne, AnimationCurve blurCurve, float remapBlurZero, float remapBlurOne, AnimationCurve anamorphicFlaresCurve, float remapAnamorphicFlaresZero, float remapAnamorphicFlaresOne, float duration, bool relativeValues = false, float attenuation = 1f, MMChannelData channelData = null, bool resetShakerValuesAfterShake = true, bool resetTargetValuesAfterShake = true, bool forwardDirection = true, TimescaleModes timescaleMode = TimescaleModes.Scaled, bool stop = false, bool restore = false)
		{
		}

		protected override void ResetTargetValues()
		{
		}

		protected override void ResetShakerValues()
		{
		}

		public override void StartListening()
		{
		}

		public override void StopListening()
		{
		}
	}
}
