using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu(null)]
	[FeedbackHelp("This feedback lets you change the parent of a transform.")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks", null)]
	[FeedbackPath("Transform/Set Parent")]
	public class MMF_SetParent : MMF_Feedback
	{
		public static bool FeedbackTypeAuthorized;

		[MMFInspectorGroup("Parenting", true, 12, true, false)]
		[Tooltip("the object we want to change the parent of")]
		public Transform ObjectToParent;

		[Tooltip("the object ObjectToParent should now be parented to after playing this feedback")]
		public Transform NewParent;

		[Tooltip("if true, the parent-relative position, scale and rotation are modified such that the object keeps the same world space position, rotation and scale as before")]
		public bool WorldPositionStays;

		public override bool HasAutomatedTargetAcquisition => false;

		protected override void AutomateTargetAcquisition()
		{
		}

		protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}
	}
}
