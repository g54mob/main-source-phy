using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using UnityEngine;

namespace MoreMountains.FeedbacksForThirdParty
{
	[AddComponentMenu("More Mountains/Feedbacks/Shakers/PostProcessing/MMPaniniProjectionShaker_HDRP")]
	public class MMPaniniProjectionShaker_HDRP : MMShaker
	{
		[MMInspectorGroup("Panini Projection Distance", true, 49, false)]
		[Tooltip("whether or not to add to the initial value")]
		public bool RelativeDistance;

		[Tooltip("the curve used to animate the distance value on")]
		public AnimationCurve ShakeDistance;

		[Tooltip("the value to remap the curve's 0 to")]
		[Range(0f, 1f)]
		public float RemapDistanceZero;

		[Tooltip("the value to remap the curve's 1 to")]
		[Range(0f, 1f)]
		public float RemapDistanceOne;
	}
}
