using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu(null)]
	[MovedFrom(false, null, "MoreMountains.Feedbacks.MMTools", null)]
	[FeedbackPath("Audio/MMSoundManager Track Fade")]
	[FeedbackHelp("This feedback will let you fade all the sounds on a specific track at once. You will need a MMSoundManager in your scene for this to work.")]
	public class MMF_MMSoundManagerTrackFade : MMF_Feedback
	{
		public static bool FeedbackTypeAuthorized;

		[MMFInspectorGroup("MMSoundManager Track Fade", true, 30, false, false)]
		[Tooltip("the track to fade the volume on")]
		public MMSoundManager.MMSoundManagerTracks Track;

		[Tooltip("the duration of the fade, in seconds")]
		public float FadeDuration;

		[Tooltip("the volume to reach at the end of the fade")]
		[Range(0.0001f, 10f)]
		public float FinalVolume;

		[Tooltip("the tween to operate the fade on")]
		public MMTweenType FadeTween;

		public override float FeedbackDuration => 0f;

		protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		protected override void CustomStopFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}
	}
}
