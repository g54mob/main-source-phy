using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using UnityEngine;

namespace MoreMountains.FeedbacksForThirdParty
{
	[AddComponentMenu("More Mountains/Feedbacks/Shakers/PostProcessing/MMBloomShaker_HDRP")]
	public class MMBloomShaker_HDRP : MMShaker
	{
		public bool RelativeValues;

		[MMInspectorGroup("Bloom Intensity", true, 42, false)]
		[Tooltip("the curve used to animate the intensity value on")]
		public AnimationCurve ShakeIntensity;

		[Tooltip("the value to remap the curve's 0 to")]
		public float RemapIntensityZero;

		[Tooltip("the value to remap the curve's 1 to")]
		public float RemapIntensityOne;

		[MMInspectorGroup("Bloom Threshold", true, 43, false)]
		[Tooltip("the curve used to animate the threshold value on")]
		public AnimationCurve ShakeThreshold;

		[Tooltip("the value to remap the curve's 0 to")]
		public float RemapThresholdZero;

		[Tooltip("the value to remap the curve's 1 to")]
		public float RemapThresholdOne;
	}
}
