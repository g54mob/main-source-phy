using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace MoreMountains.FeedbacksForThirdParty
{
	[AddComponentMenu("More Mountains/Feedbacks/Shakers/PostProcessing/MMColorGradingShaker")]
	[RequireComponent(typeof(PostProcessVolume))]
	public class MMColorGradingShaker : MMShaker
	{
		public bool RelativeValues;

		[MMInspectorGroup("Post Exposure", true, 40, false)]
		[Tooltip("the curve used to animate the focus distance value on")]
		public AnimationCurve ShakePostExposure;

		[Tooltip("the value to remap the curve's 0 to")]
		public float RemapPostExposureZero;

		[Tooltip("the value to remap the curve's 1 to")]
		public float RemapPostExposureOne;

		[MMInspectorGroup("Hue Shift", true, 49, false)]
		[Tooltip("the curve used to animate the aperture value on")]
		public AnimationCurve ShakeHueShift;

		[Tooltip("the value to remap the curve's 0 to")]
		[Range(-180f, 180f)]
		public float RemapHueShiftZero;

		[Tooltip("the value to remap the curve's 1 to")]
		[Range(-180f, 180f)]
		public float RemapHueShiftOne;

		[MMInspectorGroup("Saturation", true, 48, false)]
		[Tooltip("the curve used to animate the focal length value on")]
		public AnimationCurve ShakeSaturation;

		[Tooltip("the value to remap the curve's 0 to")]
		[Range(-100f, 100f)]
		public float RemapSaturationZero;

		[Tooltip("the value to remap the curve's 1 to")]
		[Range(-100f, 100f)]
		public float RemapSaturationOne;

		[MMInspectorGroup("Contrast", true, 47, false)]
		[Tooltip("the curve used to animate the focal length value on")]
		public AnimationCurve ShakeContrast;

		[Tooltip("the value to remap the curve's 0 to")]
		[Range(-100f, 100f)]
		public float RemapContrastZero;

		[Tooltip("the value to remap the curve's 1 to")]
		[Range(-100f, 100f)]
		public float RemapContrastOne;

		[MMFInspectorGroup("Color Filter", true, 50, false, false)]
		[Tooltip("if this is true, the color filter will be animated over the gradient below")]
		public bool ShakeColorFilter;

		[Tooltip("the gradient to use to animate the color filter over time")]
		[GradientUsage(true)]
		public Gradient ColorFilterGradient;

		protected PostProcessVolume _volume;

		protected ColorGrading _colorGrading;

		protected float _initialPostExposure;

		protected float _initialHueShift;

		protected float _initialSaturation;

		protected float _initialContrast;

		protected Color _initialColorFilter;

		protected float _originalShakeDuration;

		protected bool _originalRelativeValues;

		protected AnimationCurve _originalShakePostExposure;

		protected float _originalRemapPostExposureZero;

		protected float _originalRemapPostExposureOne;

		protected AnimationCurve _originalShakeHueShift;

		protected float _originalRemapHueShiftZero;

		protected float _originalRemapHueShiftOne;

		protected AnimationCurve _originalShakeSaturation;

		protected float _originalRemapSaturationZero;

		protected float _originalRemapSaturationOne;

		protected AnimationCurve _originalShakeContrast;

		protected float _originalRemapContrastZero;

		protected float _originalRemapContrastOne;

		protected bool _originalShakeColorFilter;

		protected Gradient _originalColorFilter;

		protected Color _newColorFilter;

		protected override void Initialization()
		{
		}

		protected virtual void Reset()
		{
		}

		protected override void Shake()
		{
		}

		protected override void GrabInitialValues()
		{
		}

		public virtual void OnMMColorGradingShakeEvent(AnimationCurve shakePostExposure, float remapPostExposureZero, float remapPostExposureOne, AnimationCurve shakeHueShift, float remapHueShiftZero, float remapHueShiftOne, AnimationCurve shakeSaturation, float remapSaturationZero, float remapSaturationOne, AnimationCurve shakeContrast, float remapContrastZero, float remapContrastOne, bool shakeColorFilter, Gradient colorFilterGradient, float duration, bool relativeValues = false, float feedbacksIntensity = 1f, MMChannelData channelData = null, bool resetShakerValuesAfterShake = true, bool resetTargetValuesAfterShake = true, bool forwardDirection = true, TimescaleModes timescaleMode = TimescaleModes.Scaled, bool stop = false, bool restore = false)
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
