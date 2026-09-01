using UnityEngine;

namespace MoreMountains.Tools
{
	[AddComponentMenu("More Mountains/Tools/Audio/MMPlaylistRemote")]
	public class MMPlaylistRemote : MonoBehaviour
	{
		public int Channel;

		public int TrackNumber;

		[Header("Triggers")]
		public bool PlaySelectedTrackOnTriggerEnter;

		public bool PlaySelectedTrackOnTriggerExit;

		public string TriggerTag;

		[Header("Test")]
		[MMInspectorButton("Play")]
		public bool PlayButton;

		[MMInspectorButton("Pause")]
		public bool PauseButton;

		[MMInspectorButton("Stop")]
		public bool StopButton;

		[MMInspectorButton("PlayNextTrack")]
		public bool NextButton;

		[MMInspectorButton("PlaySelectedTrack")]
		public bool SelectedTrackButton;

		public virtual void Play()
		{
		}

		public virtual void Pause()
		{
		}

		public virtual void Stop()
		{
		}

		public virtual void PlayNextTrack()
		{
		}

		public virtual void PlaySelectedTrack()
		{
		}

		public virtual void PlayTrack(int trackIndex)
		{
		}

		protected virtual void OnTriggerEnter(Collider collider)
		{
		}

		protected virtual void OnTriggerExit(Collider collider)
		{
		}

		protected virtual void OnTriggerEnter2D(Collider2D collider)
		{
		}

		protected virtual void OnTriggerExit2D(Collider2D collider)
		{
		}
	}
}
