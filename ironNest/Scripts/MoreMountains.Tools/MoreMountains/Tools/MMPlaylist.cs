using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace MoreMountains.Tools
{
	[AddComponentMenu("More Mountains/Tools/Audio/MMPlaylist")]
	[MMRequiresConstantRepaint]
	public class MMPlaylist : MMMonoBehaviour
	{
		public enum PlaylistStates
		{
			Idle = 0,
			Playing = 1,
			Paused = 2
		}

		[CompilerGenerated]
		private sealed class _003CFade_003Ed__44 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public int index;

			public MMPlaylist _003C_003E4__this;

			public float duration;

			public float initialVolume;

			public float endVolume;

			public bool stopAtTheEnd;

			private float _003CstartTimestamp_003E5__2;

			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			[DebuggerHidden]
			public _003CFade_003Ed__44(int _003C_003E1__state)
			{
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
			}
		}

		[CompilerGenerated]
		private sealed class _003CPlaySong_003Ed__43 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MMPlaylist _003C_003E4__this;

			public int index;

			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			[DebuggerHidden]
			public _003CPlaySong_003Ed__43(int _003C_003E1__state)
			{
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
			}
		}

		[MMInspectorGroup("Playlist Songs", true, 18, false)]
		[Tooltip("the channel on which to broadcast orders for this playlist")]
		public int Channel;

		[Tooltip("the songs that this playlist will play")]
		public List<MMPlaylistSong> Songs;

		[MMInspectorGroup("Settings", true, 13, false)]
		[Tooltip("whether this should play in random order or not")]
		public bool RandomOrder;

		[Tooltip("if this is true, random seed will be randomized by the system clock")]
		[MMCondition("RandomOrder", true)]
		public bool RandomizeOrderSeed;

		[Tooltip("whether this playlist should play and loop as a whole forever or not")]
		public bool Endless;

		[Tooltip("whether this playlist should auto play on start or not")]
		public bool PlayOnStart;

		[Tooltip("a global volume multiplier to apply when playing a song")]
		public float VolumeMultiplier;

		[Tooltip("if this is true, this playlist will automatically pause/resume OnApplicationPause, useful if you've prevented your game from running in the background")]
		public bool AutoHandleApplicationPause;

		[MMInspectorGroup("Persistence", true, 32, false)]
		[Tooltip("if this is true, this playlist will persist from scene to scene")]
		public bool Persistent;

		[Tooltip("if this is true, this singleton will auto detach if it finds itself parented on awake")]
		[MMCondition("Persistent", true)]
		public bool AutomaticallyUnparentOnAwake;

		[MMInspectorGroup("Status", true, 14, false)]
		[Tooltip("the current state of the playlist, debug display only")]
		[MMReadOnly]
		public PlaylistStates DebugCurrentState;

		[Tooltip("the index we're currently playing")]
		[MMReadOnly]
		public int CurrentlyPlayingIndex;

		[Tooltip("the name of the song that is currently playing")]
		[MMReadOnly]
		public string CurrentSongName;

		[MMReadOnly]
		public MMStateMachine<PlaylistStates> PlaylistState;

		[MMInspectorGroup("Tests", true, 15, false)]
		[MMInspectorButton("Play")]
		public bool PlayButton;

		[MMInspectorButton("Pause")]
		public bool PauseButton;

		[MMInspectorButton("Stop")]
		public bool StopButton;

		[MMInspectorButton("PlayNextSong")]
		public bool NextButton;

		[Tooltip("the index of the song to play when pressing the PlayTargetSong button")]
		public int TargetSongIndex;

		[MMInspectorButton("PlayTargetSong")]
		public bool TargetSongButton;

		[MMInspectorButton("QueueTargetSong")]
		public bool QueueTargetSongButton;

		[MMInspectorButton("SetLoopTargetSong")]
		public bool SetLoopTargetSongButton;

		[MMInspectorButton("StopLoopTargetSong")]
		public bool StopLoopTargetSongButton;

		protected int _songsPlayedSoFar;

		protected int _songsPlayedThisCycle;

		protected Coroutine _coroutine;

		protected bool _shouldResumeOnApplicationPause;

		protected static MMPlaylist _instance;

		protected bool _enabled;

		protected int _queuedSong;

		protected bool _firstDeserialization;

		protected int _listCount;

		public static bool HasInstance => false;

		public static MMPlaylist Current => null;

		public static MMPlaylist Instance => null;

		protected virtual void Awake()
		{
		}

		protected virtual void InitializeSingleton()
		{
		}

		protected virtual void Start()
		{
		}

		protected virtual void Initialization()
		{
		}

		protected virtual void ChangePlaylistState(PlaylistStates newState)
		{
		}

		protected virtual void PlayFirstSong()
		{
		}

		[IteratorStateMachine(typeof(_003CPlaySong_003Ed__43))]
		protected virtual IEnumerator PlaySong(int index)
		{
			return null;
		}

		[IteratorStateMachine(typeof(_003CFade_003Ed__44))]
		protected virtual IEnumerator Fade(int index, float duration, float initialVolume, float endVolume, bool stopAtTheEnd)
		{
			return null;
		}

		protected virtual int PickNextIndex()
		{
			return 0;
		}

		protected virtual int PickPreviousIndex()
		{
			return 0;
		}

		public virtual void Play()
		{
		}

		public virtual void PlayAtIndex(int songIndex)
		{
		}

		public virtual void QueueSongAtIndex(int songIndex)
		{
		}

		public virtual void Pause()
		{
		}

		public virtual void Stop()
		{
		}

		public virtual void SetLoop(bool loop)
		{
		}

		public virtual void PlayNextSong()
		{
		}

		public virtual void PlayPreviousSong()
		{
		}

		protected virtual void PlayTargetSong()
		{
		}

		protected virtual void QueueTargetSong()
		{
		}

		protected virtual void SetLoopTargetSong()
		{
		}

		protected virtual void StopLoopTargetSong()
		{
		}

		protected virtual void OnPlayEvent(int channel)
		{
		}

		protected virtual void OnPauseEvent(int channel)
		{
		}

		protected virtual void OnStopEvent(int channel)
		{
		}

		protected virtual void OnPlayNextEvent(int channel)
		{
		}

		protected virtual void OnPlayPreviousEvent(int channel)
		{
		}

		protected virtual void OnPlayIndexEvent(int channel, int index)
		{
		}

		protected virtual void OnMMPlaylistVolumeMultiplierEvent(int channel, float newVolumeMultiplier, bool applyVolumeMultiplierInstantly = false)
		{
		}

		protected virtual void OnEnable()
		{
		}

		protected virtual void OnDisable()
		{
		}

		protected virtual void OnValidate()
		{
		}

		protected virtual void OnApplicationPause(bool pauseStatus)
		{
		}
	}
}
