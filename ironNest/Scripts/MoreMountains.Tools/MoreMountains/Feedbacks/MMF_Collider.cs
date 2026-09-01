using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu(null)]
	[FeedbackHelp("This feedback will let you enable/disable/toggle a target collider, or change its trigger status")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks", null)]
	[FeedbackPath("GameObject/Collider")]
	public class MMF_Collider : MMF_Feedback
	{
		public enum Modes
		{
			Enable = 0,
			Disable = 1,
			ToggleActive = 2,
			Trigger = 3,
			NonTrigger = 4,
			ToggleTrigger = 5
		}

		public static bool FeedbackTypeAuthorized;

		[MMFInspectorGroup("Collider", true, 12, true, false)]
		[Tooltip("the collider to act upon")]
		public Collider TargetCollider;

		public Modes Mode;

		protected bool _initialState;

		public override bool HasAutomatedTargetAcquisition => false;

		protected override void AutomateTargetAcquisition()
		{
		}

		protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		protected virtual void ApplyChanges(Modes mode)
		{
		}

		protected override void CustomRestoreInitialValues()
		{
		}
	}
}
