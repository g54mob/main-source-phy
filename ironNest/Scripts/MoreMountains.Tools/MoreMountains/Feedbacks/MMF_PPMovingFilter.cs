using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu(null)]
	[FeedbackHelp("This feedback will trigger a post processing moving filter event, meant to be caught by a MMPostProcessingMovableFilter object")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks.MMTools", null)]
	[FeedbackPath("PostProcess/PPMovingFilter")]
	public class MMF_PPMovingFilter : MMF_Feedback
	{
		public enum Modes
		{
			Toggle = 0,
			On = 1,
			Off = 2
		}

		public static bool FeedbackTypeAuthorized;

		[MMFInspectorGroup("PostProcessing Profile Moving Filter", true, 54, false, false)]
		[Tooltip("the selected mode for this feedback")]
		public Modes Mode;

		[Tooltip("the duration of the transition")]
		public float TransitionDuration;

		[Tooltip("the curve to move along to")]
		public MMTweenType Curve;

		protected bool _active;

		protected bool _toggle;

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
