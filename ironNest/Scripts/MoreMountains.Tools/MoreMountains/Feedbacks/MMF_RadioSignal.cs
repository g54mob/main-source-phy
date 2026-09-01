using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu(null)]
	[FeedbackHelp("This feedback will let you trigger a play on a target MMRadioSignal (usually used by a MMRadioBroadcaster to emit a value that can then be listened to by MMRadioReceivers. From this feedback you can also specify a duration, timescale and multiplier.")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks.MMTools", null)]
	[FeedbackPath("GameObject/MMRadioSignal")]
	public class MMF_RadioSignal : MMF_Feedback
	{
		public static bool FeedbackTypeAuthorized;

		[MMFInspectorGroup("Radio Signal", true, 72, false, false)]
		[Tooltip("The target MMRadioSignal to trigger")]
		public MMRadioSignal TargetSignal;

		[Tooltip("the timescale to operate on")]
		public MMRadioSignal.TimeScales TimeScale;

		[Tooltip("the duration of the shake, in seconds")]
		public float Duration;

		[Tooltip("a global multiplier to apply to the end result of the combination")]
		public float GlobalMultiplier;

		public override float FeedbackDuration => 0f;

		public override bool HasRandomness => false;

		public override bool HasAutomatedTargetAcquisition => false;

		protected override void AutomateTargetAcquisition()
		{
		}

		protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		protected override void CustomStopFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		protected override void CustomRestoreInitialValues()
		{
		}
	}
}
