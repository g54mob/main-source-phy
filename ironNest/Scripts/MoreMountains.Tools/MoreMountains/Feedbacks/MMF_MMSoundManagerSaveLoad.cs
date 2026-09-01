using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu(null)]
	[MovedFrom(false, null, "MoreMountains.Feedbacks.MMTools", null)]
	[FeedbackPath("Audio/MMSoundManager Save and Load")]
	[FeedbackHelp("This feedback will let you trigger save, load, and reset on MMSoundManager settings. You will need a MMSoundManager in your scene for this to work.")]
	public class MMF_MMSoundManagerSaveLoad : MMF_Feedback
	{
		public enum Modes
		{
			Save = 0,
			Load = 1,
			Reset = 2
		}

		public static bool FeedbackTypeAuthorized;

		[MMFInspectorGroup("MMSoundManager Save and Load", true, 30, false, false)]
		[Tooltip("the selected mode to interact with save settings on the MMSoundManager")]
		public Modes Mode;

		protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}
	}
}
