using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu(null)]
	[MovedFrom(false, null, "MoreMountains.Feedbacks.MMTools", null)]
	[FeedbackPath("Audio/MMSoundManager Track Control")]
	[FeedbackHelp("This feedback will let you control all sounds playing on a specific track (master, UI, music, sfx), and play, pause, mute, unmute, resume, stop, free them all at once. You will need a MMSoundManager in your scene for this to work.")]
	public class MMF_MMSoundManagerTrackControl : MMF_Feedback
	{
		public enum ControlModes
		{
			Mute = 0,
			UnMute = 1,
			SetVolume = 2,
			Pause = 3,
			Play = 4,
			Stop = 5,
			Free = 6
		}

		public static bool FeedbackTypeAuthorized;

		[MMFInspectorGroup("MMSoundManager Track Control", true, 30, false, false)]
		[Tooltip("the track to mute/unmute/pause/play/stop/free/etc")]
		public MMSoundManager.MMSoundManagerTracks Track;

		[Tooltip("the selected control mode to interact with the track. Free will stop all sounds and return them to the pool")]
		public ControlModes ControlMode;

		[Tooltip("if setting the volume, the volume to assign to the track")]
		[MMEnumCondition("ControlMode", new int[] { 2 })]
		public float Volume;

		protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}
	}
}
