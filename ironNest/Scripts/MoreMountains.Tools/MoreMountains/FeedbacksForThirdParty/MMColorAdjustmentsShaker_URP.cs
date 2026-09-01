using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace MoreMountains.FeedbacksForThirdParty
{
	[RequireComponent(typeof(Volume))]
	[AddComponentMenu("More Mountains/Feedbacks/Shakers/PostProcessing/MMColorAdjustmentsShaker_URP")]
	public class MMColorAdjustmentsShaker_URP : MMShaker
	{
		public enum ColorFilterModes
		{
			None = 0,
			Gradient = 1,
			Interpolate = 2
		}

		public bool RelativeValues;

		[MMInspectorGroup("Post Exposure", true, 43, false)]
		[Tooltip("the curve used to animate the focus distance value on")]
		public AnimationCurve ShakePostExposure;

		[Tooltip("the value to remap the curve's 0 to")]
		public float RemapPostExposureZero;

		[Tooltip("the value to remap the curve's 1 to")]
		public float RemapPostExposureOne;

		[MMInspectorGroup("Hue Shift", true, 44, false)]
		[Tooltip("the curve used to animate the aperture value on")]
		public AnimationCurve ShakeHueShift;

		[Range(-180f, 180f)]
		[Tooltip("the value to remap the curve's 0 to")]
		public float RemapHueShiftZero;

		[Tooltip("the value to remap the curve's 1 to")]
		[Range(-180f, 180f)]
		public float RemapHueShiftOne;

		[MMInspectorGroup("Saturation", true, 45, false)]
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

		[MMInspectorGroup("Color Filter", true, 48, false)]
		[Tooltip("the color filter mode to work with (none, over a gradient, or interpolate to a destination color")]
		public ColorFilterModes ColorFilterMode;

		[Tooltip("the gradient over which to modify the color filter")]
		[MMFEnumCondition("ColorFilterMode", new int[] { 1 })]
		[GradientUsage(true)]
		public Gradient ColorFilterGradient;

		[Tooltip("the destination color to match when in Interpolate mode")]
		[MMFEnumCondition("ColorFilterMode", new int[] { 2 })]
		public Color ColorFilterDestination;

		[Tooltip("the curve over which to interpolate the color filter")]
		[MMFEnumCondition("ColorFilterMode", new int[] { 2 })]
		public AnimationCurve ColorFilterCurve;

		protected Volume _volume;

		protected ColorAdjustments _colorAdjustments;

		protected float _initialPostExposure;

		protected float _initialHueShift;

		protected float _initialSaturation;

		protected float _initialContrast;

		protected Color _initialColorFilterColor;

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

		protected ColorFilterModes _originalColorFilterMode;

		protected Gradient _originalColorFilterGradient;

		protected Color _originalColorFilterDestination;

		protected AnimationCurve _originalColorFilterCurve;

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

		public virtual void OnMMColorGradingShakeEvent(AnimationCurve shakePostExposure, float remapPostExposureZero, float remapPostExposureOne, AnimationCurve shakeHueShift, float remapHueShiftZero, float remapHueShiftOne, AnimationCurve shakeSaturation, float remapSaturationZero, float remapSaturationOne, AnimationCurve shakeContrast, float remapContrastZero, float remapContrastOne, ColorFilterModes colorFilterMode, Gradient colorFilterGradient, Color colorFilterDestination, AnimationCurve colorFilterCurve, float duration, bool relativeValues = false, float attenuation = 1f, MMChannelData channelData = null, bool resetShakerValuesAfterShake = true, bool resetTargetValuesAfterShake = true, bool forwardDirection = true, TimescaleModes timescaleMode = TimescaleModes.Scaled, bool stop = false, bool restore = false)
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
