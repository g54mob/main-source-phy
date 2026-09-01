using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.Video;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu(null)]
	[FeedbackHelp("This feedback lets you control video players in all sorts of ways (Play, Pause, Toggle, Stop, Prepare, StepForward, StepBackward, SetPlaybackSpeed, SetDirectAudioVolume, SetDirectAudioMute, GoToFrame, ToggleLoop)")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks", null)]
	[FeedbackPath("UI/Video Player")]
	public class MMF_VideoPlayer : MMF_Feedback
	{
		public enum VideoActions
		{
			Play = 0,
			Pause = 1,
			Toggle = 2,
			Stop = 3,
			Prepare = 4,
			StepForward = 5,
			StepBackward = 6,
			SetPlaybackSpeed = 7,
			SetDirectAudioVolume = 8,
			SetDirectAudioMute = 9,
			GoToFrame = 10,
			ToggleLoop = 11
		}

		public static bool FeedbackTypeAuthorized;

		[MMFInspectorGroup("Video Player", true, 58, true, false)]
		[Tooltip("the Video Player to control with this feedback")]
		public VideoPlayer TargetVideoPlayer;

		[Tooltip("the Video Player to control with this feedback")]
		public VideoActions VideoAction;

		[Tooltip("the frame at which to jump when in GoToFrame mode")]
		[MMFEnumCondition("VideoAction", new int[] { 10 })]
		public long TargetFrame;

		[Tooltip("the new playback speed (between 0 and 10)")]
		[MMFEnumCondition("VideoAction", new int[] { 7 })]
		public float PlaybackSpeed;

		[Tooltip("the track index on which to control volume")]
		[MMFEnumCondition("VideoAction", new int[] { 9, 8 })]
		public int TrackIndex;

		[Tooltip("the new volume for the specified track, between 0 and 1")]
		[MMFEnumCondition("VideoAction", new int[] { 8 })]
		public float Volume;

		[Tooltip("whether to mute the track or not when that feedback plays")]
		[MMFEnumCondition("VideoAction", new int[] { 9 })]
		public bool Mute;

		public override bool HasAutomatedTargetAcquisition => false;

		protected override void AutomateTargetAcquisition()
		{
		}

		protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}
	}
}
