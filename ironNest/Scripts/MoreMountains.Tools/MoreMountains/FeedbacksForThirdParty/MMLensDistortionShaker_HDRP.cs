using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using UnityEngine;

namespace MoreMountains.FeedbacksForThirdParty
{
	[AddComponentMenu("More Mountains/Feedbacks/Shakers/PostProcessing/MMLensDistortionShaker_HDRP")]
	public class MMLensDistortionShaker_HDRP : MMShaker
	{
		[MMInspectorGroup("Lens Distortion Intensity", true, 47, false)]
		[Tooltip("whether or not to add to the initial value")]
		public bool RelativeIntensity;

		[Tooltip("the curve used to animate the intensity value on")]
		public AnimationCurve ShakeIntensity;

		[Tooltip("the value to remap the curve's 0 to")]
		[Range(-100f, 100f)]
		public float RemapIntensityZero;

		[Tooltip("the value to remap the curve's 1 to")]
		[Range(-100f, 100f)]
		public float RemapIntensityOne;
	}
}
