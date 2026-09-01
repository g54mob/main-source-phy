using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.FeedbacksForThirdParty
{
	[AddComponentMenu(null)]
	[MovedFrom(false, null, "MoreMountains.Feedbacks.HDRP", null)]
	[FeedbackHelp("This feedback allows you to control HDRP Depth of Field focus distance or near/far ranges over time.It requires you have in your scene an object with a Volume with Depth of Field active, and a MMDepthOfFieldShaker_HDRP component.")]
	public class MMF_DepthOfField_HDRP : MMF_Feedback
	{
		public static bool FeedbackTypeAuthorized;

		[MMFInspectorGroup("Depth of Field", true, 28, false, false)]
		[Tooltip("the duration of the shake, in seconds")]
		public float Duration;

		[Tooltip("whether or not to reset shaker values after shake")]
		public bool ResetShakerValuesAfterShake;

		[Tooltip("whether or not to reset the target's values after shake")]
		public bool ResetTargetValuesAfterShake;

		[MMFInspectorGroup("Focus Distance", true, 53, false, false)]
		[Tooltip("whether or not to animate the focus distance")]
		public bool AnimateFocusDistance;

		[Tooltip("the curve used to animate the focus distance value on")]
		[MMFCondition("AnimateFocusDistance", true)]
		public AnimationCurve ShakeFocusDistance;

		[Tooltip("the value to remap the curve's 0 to")]
		[MMFCondition("AnimateFocusDistance", true)]
		public float RemapFocusDistanceZero;

		[Tooltip("the value to remap the curve's 1 to")]
		[MMFCondition("AnimateFocusDistance", true)]
		public float RemapFocusDistanceOne;

		[MMFInspectorGroup("Near Range", true, 52, false, false)]
		[Header("Near Range Start")]
		[Tooltip("whether or not to animate the near range start")]
		public bool AnimateNearRangeStart;

		[Tooltip("the curve used to animate the near range start on")]
		[MMFCondition("AnimateNearRangeStart", true)]
		public AnimationCurve ShakeNearRangeStart;

		[Tooltip("the value to remap the curve's 0 to")]
		[MMFCondition("AnimateNearRangeStart", true)]
		public float RemapNearRangeStartZero;

		[Tooltip("the value to remap the curve's 1 to")]
		[MMFCondition("AnimateNearRangeStart", true)]
		public float RemapNearRangeStartOne;

		[Header("Near Range End")]
		[Tooltip("whether or not to animate the near range end")]
		public bool AnimateNearRangeEnd;

		[Tooltip("the curve used to animate the near range end on")]
		[MMFCondition("AnimateNearRangeEnd", true)]
		public AnimationCurve ShakeNearRangeEnd;

		[Tooltip("the value to remap the curve's 0 to")]
		[MMFCondition("AnimateNearRangeEnd", true)]
		public float RemapNearRangeEndZero;

		[Tooltip("the value to remap the curve's 1 to")]
		[MMFCondition("AnimateNearRangeEnd", true)]
		public float RemapNearRangeEndOne;

		[MMFInspectorGroup("Far Range", true, 51, false, false)]
		[Header("Far Range Start")]
		[Tooltip("whether or not to animate the far range start")]
		public bool AnimateFarRangeStart;

		[Tooltip("the curve used to animate the far range start on")]
		[MMFCondition("AnimateFarRangeStart", true)]
		public AnimationCurve ShakeFarRangeStart;

		[Tooltip("the value to remap the curve's 0 to")]
		[MMFCondition("AnimateFarRangeStart", true)]
		public float RemapFarRangeStartZero;

		[Tooltip("the value to remap the curve's 1 to")]
		[MMFCondition("AnimateFarRangeStart", true)]
		public float RemapFarRangeStartOne;

		[Header("Far Range End")]
		[Tooltip("whether or not to animate the far range end")]
		public bool AnimateFarRangeEnd;

		[Tooltip("the curve used to animate the far range end on")]
		[MMFCondition("AnimateFarRangeEnd", true)]
		public AnimationCurve ShakeFarRangeEnd;

		[Tooltip("the value to remap the curve's 0 to")]
		[MMFCondition("AnimateFarRangeEnd", true)]
		public float RemapFarRangeEndZero;

		[Tooltip("the value to remap the curve's 1 to")]
		[MMFCondition("AnimateFarRangeEnd", true)]
		public float RemapFarRangeEndOne;

		public override float FeedbackDuration
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public override bool HasChannel => false;

		public override bool HasRandomness => false;

		protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		protected override void CustomStopFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		protected override void CustomRestoreInitialValues()
		{
		}

		public override void AutomaticShakerSetup()
		{
		}
	}
}
