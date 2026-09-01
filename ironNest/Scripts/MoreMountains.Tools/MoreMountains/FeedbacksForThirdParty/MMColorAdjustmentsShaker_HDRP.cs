using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using UnityEngine;

namespace MoreMountains.FeedbacksForThirdParty
{
	[AddComponentMenu("More Mountains/Feedbacks/Shakers/PostProcessing/MMColorAdjustmentsShaker_HDRP")]
	public class MMColorAdjustmentsShaker_HDRP : MMShaker
	{
		public enum ColorFilterModes
		{
			None = 0,
			Gradient = 1,
			Interpolate = 2
		}

		public bool RelativeValues;

		[MMInspectorGroup("Post Exposure", true, 44, false)]
		[Tooltip("the curve used to animate the focus distance value on")]
		public AnimationCurve ShakePostExposure;

		[Tooltip("the value to remap the curve's 0 to")]
		public float RemapPostExposureZero;

		[Tooltip("the value to remap the curve's 1 to")]
		public float RemapPostExposureOne;

		[MMInspectorGroup("Hue Shift", true, 45, false)]
		[Tooltip("the curve used to animate the aperture value on")]
		public AnimationCurve ShakeHueShift;

		[Range(-180f, 180f)]
		[Tooltip("the value to remap the curve's 0 to")]
		public float RemapHueShiftZero;

		[Tooltip("the value to remap the curve's 1 to")]
		[Range(-180f, 180f)]
		public float RemapHueShiftOne;

		[MMInspectorGroup("Saturation", true, 46, false)]
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
	}
}
