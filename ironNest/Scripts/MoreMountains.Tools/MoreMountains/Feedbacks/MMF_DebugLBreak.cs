using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu(null)]
	[FeedbackHelp("This feedback will will force a break, pausing the editor")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks.MMTools", null)]
	[FeedbackPath("Debug/Break")]
	public class MMF_DebugLBreak : MMF_Feedback
	{
		public static bool FeedbackTypeAuthorized;

		public override float FeedbackDuration => 0f;

		protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}
	}
}
