using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu(null)]
	[FeedbackHelp("This feedback will let you output a message to the console, using a custom MM debug method, or Log, Assertion, Error or Warning logs.")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks.MMTools", null)]
	[FeedbackPath("Debug/Log")]
	public class MMF_DebugLog : MMF_Feedback
	{
		public enum DebugLogModes
		{
			DebugLogTime = 0,
			Log = 1,
			Assertion = 2,
			Error = 3,
			Warning = 4
		}

		public static bool FeedbackTypeAuthorized;

		[MMFInspectorGroup("Debug", true, 17, false, false)]
		[Tooltip("the selected debug mode")]
		public DebugLogModes DebugLogMode;

		[Tooltip("the message to display")]
		[TextArea]
		public string DebugMessage;

		[Tooltip("the color of the message when in DebugLogTime mode")]
		[MMFEnumCondition("DebugLogMode", new int[] { 0 })]
		public Color DebugColor;

		[Tooltip("whether or not to display the frame count when in DebugLogTime mode")]
		[MMFEnumCondition("DebugLogMode", new int[] { 0 })]
		public bool DisplayFrameCount;

		public override float FeedbackDuration => 0f;

		protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}
	}
}
