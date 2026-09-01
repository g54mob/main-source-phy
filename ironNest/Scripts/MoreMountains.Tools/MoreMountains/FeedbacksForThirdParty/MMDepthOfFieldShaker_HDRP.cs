using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using UnityEngine;

namespace MoreMountains.FeedbacksForThirdParty
{
	[AddComponentMenu("More Mountains/Feedbacks/Shakers/PostProcessing/MMDepthOfFieldShaker_HDRP")]
	public class MMDepthOfFieldShaker_HDRP : MMShaker
	{
		[MMInspectorGroup("Focus Distance", true, 53, false)]
		[Tooltip("whether or not to animate the focus distance")]
		public bool AnimateFocusDistance;

		[Tooltip("the curve used to animate the focus distance value on")]
		[MMCondition("AnimateFocusDistance", true)]
		public AnimationCurve ShakeFocusDistance;

		[Tooltip("the value to remap the curve's 0 to")]
		[MMCondition("AnimateFocusDistance", true)]
		public float RemapFocusDistanceZero;

		[Tooltip("the value to remap the curve's 1 to")]
		[MMCondition("AnimateFocusDistance", true)]
		public float RemapFocusDistanceOne;

		[MMInspectorGroup("Near Range", true, 52, false)]
		[Header("Near Range Start")]
		[Tooltip("whether or not to animate the near range start")]
		public bool AnimateNearRangeStart;

		[Tooltip("the curve used to animate the near range start on")]
		[MMCondition("AnimateNearRangeStart", true)]
		public AnimationCurve ShakeNearRangeStart;

		[Tooltip("the value to remap the curve's 0 to")]
		[MMCondition("AnimateNearRangeStart", true)]
		public float RemapNearRangeStartZero;

		[Tooltip("the value to remap the curve's 1 to")]
		[MMCondition("AnimateNearRangeStart", true)]
		public float RemapNearRangeStartOne;

		[Header("Near Range End")]
		[Tooltip("whether or not to animate the near range end")]
		public bool AnimateNearRangeEnd;

		[Tooltip("the curve used to animate the near range end on")]
		[MMCondition("AnimateNearRangeEnd", true)]
		public AnimationCurve ShakeNearRangeEnd;

		[Tooltip("the value to remap the curve's 0 to")]
		[MMCondition("AnimateNearRangeEnd", true)]
		public float RemapNearRangeEndZero;

		[Tooltip("the value to remap the curve's 1 to")]
		[MMCondition("AnimateNearRangeEnd", true)]
		public float RemapNearRangeEndOne;

		[MMInspectorGroup("Far Range", true, 51, false)]
		[Header("Far Range Start")]
		[Tooltip("whether or not to animate the far range start")]
		public bool AnimateFarRangeStart;

		[Tooltip("the curve used to animate the far range start on")]
		[MMCondition("AnimateFarRangeStart", true)]
		public AnimationCurve ShakeFarRangeStart;

		[Tooltip("the value to remap the curve's 0 to")]
		[MMCondition("AnimateFarRangeStart", true)]
		public float RemapFarRangeStartZero;

		[Tooltip("the value to remap the curve's 1 to")]
		[MMCondition("AnimateFarRangeStart", true)]
		public float RemapFarRangeStartOne;

		[Header("Far Range End")]
		[Tooltip("whether or not to animate the far range end")]
		public bool AnimateFarRangeEnd;

		[Tooltip("the curve used to animate the far range end on")]
		[MMCondition("AnimateFarRangeEnd", true)]
		public AnimationCurve ShakeFarRangeEnd;

		[Tooltip("the value to remap the curve's 0 to")]
		[MMCondition("AnimateFarRangeEnd", true)]
		public float RemapFarRangeEndZero;

		[Tooltip("the value to remap the curve's 1 to")]
		[MMCondition("AnimateFarRangeEnd", true)]
		public float RemapFarRangeEndOne;
	}
}
