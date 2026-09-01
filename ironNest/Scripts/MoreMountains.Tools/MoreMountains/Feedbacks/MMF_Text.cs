using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UI;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu(null)]
	[FeedbackHelp("This feedback lets you control the contents of a target Text over time.")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks", null)]
	[FeedbackPath("UI/Text")]
	public class MMF_Text : MMF_Feedback
	{
		public enum ColorModes
		{
			Instant = 0,
			Gradient = 1,
			Interpolate = 2
		}

		public static bool FeedbackTypeAuthorized;

		[MMFInspectorGroup("Text", true, 76, true, false)]
		[Tooltip(" Text component to control")]
		public Text TargetText;

		[Tooltip("the new text to replace the old one with")]
		[TextArea]
		public string NewText;

		protected string _initialText;

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
