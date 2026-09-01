using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu(null)]
	[FeedbackHelp("This feedback will let you transition to a target AudioMixer Snapshot over a specified time")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks", null)]
	[FeedbackPath("Audio/AudioMixer Snapshot Transition")]
	public class MMF_AudioMixerSnapshotTransition : MMF_Feedback
	{
		public static bool FeedbackTypeAuthorized;

		[MMFInspectorGroup("AudioMixer Snapshot", true, 44, false, false)]
		[Tooltip("the target audio mixer snapshot we want to transition to")]
		public AudioMixerSnapshot TargetSnapshot;

		[Tooltip("the audio mixer snapshot we want to transition from, optional, only needed if you plan to play this feedback in reverse")]
		public AudioMixerSnapshot OriginalSnapshot;

		[Tooltip("the duration, in seconds, over which to transition to the selected snapshot")]
		public float TransitionDuration;

		protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}
	}
}
