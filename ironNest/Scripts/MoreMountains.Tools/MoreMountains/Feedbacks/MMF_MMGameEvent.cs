using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu(null)]
	[FeedbackHelp("This feedback will trigger a MMGameEvent of the specified name when played")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks.MMTools", null)]
	[FeedbackPath("Events/MMGameEvent")]
	public class MMF_MMGameEvent : MMF_Feedback
	{
		public static bool FeedbackTypeAuthorized;

		[MMFInspectorGroup("MMGameEvent", true, 57, true, false)]
		public string MMGameEventName;

		[MMFInspectorGroup("Optional Payload", true, 58, true, false)]
		public int IntParameter;

		public Vector2 Vector2Parameter;

		public Vector3 Vector3Parameter;

		public bool BoolParameter;

		public string StringParameter;

		protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}
	}
}
