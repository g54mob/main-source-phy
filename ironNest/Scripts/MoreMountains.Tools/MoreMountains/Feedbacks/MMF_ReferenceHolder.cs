using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu(null)]
	[FeedbackHelp("This feedback allows you to hold a reference, that can then be used by other feedbacks to automatically set their target. It doesn't do anything when played.")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks", null)]
	[FeedbackPath("Feedbacks/MMF Reference Holder")]
	public class MMF_ReferenceHolder : MMF_Feedback
	{
		public static bool FeedbackTypeAuthorized;

		[MMFInspectorGroup("References", true, 37, true, false)]
		[Tooltip("the game object to set as the target (or on which to look for a specific component as a target) of all feedbacks that may look at this reference holder for a target")]
		public GameObject GameObjectReference;

		[Tooltip("whether or not to force this reference holder on all compatible feedbacks in the MMF Player's list")]
		public bool ForceReferenceOnAll;

		public override float FeedbackDuration => 0f;

		public override bool DisplayFullHeaderColor => false;

		protected override void CustomInitialization(MMF_Player owner)
		{
		}

		protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}
	}
}
