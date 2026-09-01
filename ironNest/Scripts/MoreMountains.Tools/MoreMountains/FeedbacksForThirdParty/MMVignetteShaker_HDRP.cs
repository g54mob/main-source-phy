using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using UnityEngine;

namespace MoreMountains.FeedbacksForThirdParty
{
	[AddComponentMenu("More Mountains/Feedbacks/Shakers/PostProcessing/MMVignetteShaker_HDRP")]
	public class MMVignetteShaker_HDRP : MMShaker
	{
		[Header("Intensity")]
		[MMInspectorGroup("Vignette Intensity", true, 46, false)]
		[Tooltip("whether or not to add to the initial value")]
		public bool RelativeIntensity;

		[Tooltip("the curve used to animate the intensity value on")]
		public AnimationCurve ShakeIntensity;

		[Tooltip("the value to remap the curve's 0 to")]
		[Range(0f, 1f)]
		public float RemapIntensityZero;

		[Tooltip("the value to remap the curve's 1 to")]
		[Range(0f, 1f)]
		public float RemapIntensityOne;

		[MMFInspectorGroup("Vignette Color", true, 60, false, false)]
		[Tooltip("whether or not to also animate the vignette's color")]
		public bool InterpolateColor;

		[Tooltip("the curve to animate the color on")]
		public AnimationCurve ColorCurve;

		[Tooltip("the value to remap the curve's 0 to")]
		[Range(0f, 1f)]
		public float RemapColorZero;

		[Tooltip("the value to remap the curve's 1 to")]
		[Range(0f, 1f)]
		public float RemapColorOne;

		[Tooltip("the color to lerp towards")]
		public Color TargetColor;
	}
}
