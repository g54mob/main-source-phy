using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu(null)]
	[FeedbackHelp("This feedback will let you pilot a MMPlaylist")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks.MMTools", null)]
	[FeedbackPath("Audio/MMPlaylist")]
	public class MMF_Playlist : MMF_Feedback
	{
		public enum Modes
		{
			Play = 0,
			PlayNext = 1,
			PlayPrevious = 2,
			Stop = 3,
			Pause = 4,
			PlaySongAt = 5,
			SetVolumeMultiplier = 6,
			ChangePlaylist = 7
		}

		public static bool FeedbackTypeAuthorized;

		[MMFInspectorGroup("MMPlaylist", true, 13, false, false)]
		[Tooltip("the action to call on the playlist")]
		public Modes Mode;

		[Tooltip("the index of the song to play")]
		[MMEnumCondition("Mode", new int[] { 5 })]
		public int SongIndex;

		[Tooltip("the volume multiplier to apply")]
		[MMEnumCondition("Mode", new int[] { 6 })]
		public float VolumeMultiplier;

		[Tooltip("whether to apply the volume multiplier instantly (true) or only when the next song starts playing (false)")]
		[MMEnumCondition("Mode", new int[] { 6 })]
		public bool ApplyVolumeMultiplierInstantly;

		[Tooltip("in change playlist mode, the playlist to which to switch to. Only works with MMSMPlaylistManager")]
		[MMEnumCondition("Mode", new int[] { 7 })]
		public MMSMPlaylist NewPlaylist;

		[Tooltip("in change playlist mode, whether or not to play the new playlist after the switch. Only works with MMSMPlaylistManager")]
		[MMEnumCondition("Mode", new int[] { 7 })]
		public bool ChangePlaylistAndPlay;

		protected Coroutine _coroutine;

		protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}
	}
}
