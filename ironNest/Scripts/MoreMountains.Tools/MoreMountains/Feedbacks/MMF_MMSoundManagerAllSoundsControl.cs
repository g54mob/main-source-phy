using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu(null)]
	[MovedFrom(false, null, "MoreMountains.Feedbacks.MMTools", null)]
	[FeedbackPath("Audio/MMSoundManager All Sounds Control")]
	[FeedbackHelp("A feedback used to control all sounds playing on the MMSoundManager at once. It'll let you pause, play, stop and free (stop and returns the audiosource to the pool) sounds. You will need a MMSoundManager in your scene for this to work.")]
	public class MMF_MMSoundManagerAllSoundsControl : MMF_Feedback
	{
		public static bool FeedbackTypeAuthorized;

		[MMFInspectorGroup("MMSoundManager All Sounds Control", true, 30, false, false)]
		[Tooltip("The selected control mode")]
		public MMSoundManagerAllSoundsControlEventTypes ControlMode;

		protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}
	}
}
