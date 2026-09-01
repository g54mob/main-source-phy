using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.FeedbacksForThirdParty
{
	[AddComponentMenu(null)]
	[FeedbackPath("Haptics/Haptics DEPRECATED!")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks.NiceVibrations", null)]
	[FeedbackHelp("This feedback has been deprecated, and is just here to avoid errors in case you were to update from an old version. Use any of the new haptic feedbacks instead.")]
	public class MMF_Haptics : MMF_Feedback
	{
		public static bool FeedbackTypeAuthorized;

		[Header("Deprecated Feedback")]
		public bool OutputDeprecationWarning;

		protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}
	}
}
