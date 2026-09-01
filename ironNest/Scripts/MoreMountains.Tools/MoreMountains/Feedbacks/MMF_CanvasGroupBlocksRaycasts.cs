using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu(null)]
	[FeedbackHelp("This feedback will let you turn the BlocksRaycast parameter of a target CanvasGroup on or off on play")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks", null)]
	[FeedbackPath("UI/CanvasGroup BlocksRaycasts")]
	public class MMF_CanvasGroupBlocksRaycasts : MMF_Feedback
	{
		public static bool FeedbackTypeAuthorized;

		[MMFInspectorGroup("Block Raycasts", true, 54, true, false)]
		[Tooltip("the target canvas group we want to control the BlocksRaycasts parameter on")]
		public CanvasGroup TargetCanvasGroup;

		[Tooltip("if this is true, on play, the target canvas group will block raycasts, if false it won't")]
		public bool ShouldBlockRaycasts;

		protected bool _initialState;

		public override bool HasAutomatedTargetAcquisition => false;

		protected override void AutomateTargetAcquisition()
		{
		}

		protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		protected override void CustomRestoreInitialValues()
		{
		}
	}
}
