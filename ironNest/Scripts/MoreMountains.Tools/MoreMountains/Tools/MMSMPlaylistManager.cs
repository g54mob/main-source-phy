using UnityEngine;

namespace MoreMountains.Tools
{
	public class MMSMPlaylistManager : MMMonoBehaviour
	{
		public enum PlaylistManagerStates
		{
			Idle = 0,
			Playing = 1,
			Paused = 2
		}

		public delegate void PlaylistEvent();

		[MMInspectorGroup("Settings", true, 18, false)]
		[Tooltip("the channel used to target this playlist manager by playlist remote or playlist feedbacks")]
		public int Channel;

		[Tooltip("the current playlist this manager will play")]
		public MMSMPlaylist Playlist;

		[Tooltip("whether this playlist manager should auto play on start or not")]
		public bool PlayOnStart;

		[Tooltip("a global volume multiplier to apply when playing a song")]
		[Range(0f, 1f)]
		public float VolumeMultiplier;

		[Tooltip("a pitch multiplier to apply to all songs when playing them")]
		[Range(0f, 20f)]
		public float PitchMultiplier;

		[Tooltip("if this is true, this playlist manager will persist from scene to scene and will keep playing")]
		public bool Persistent;

		[Tooltip("if this is true, this singleton will auto detach if it finds itself parented on awake")]
		[MMCondition("Persistent", true)]
		public bool AutomaticallyUnparentOnAwake;

		[Tooltip("if this is true, this playlist will automatically pause/resume OnApplicationPause, useful if you've prevented your game from running in the background")]
		public bool AutoHandleApplicationPause;

		[MMInspectorGroup("Fade", true, 12, false)]
		[Tooltip("whether or not sounds should fade in when they start playing")]
		public bool FadeIn;

		[Tooltip("whether or not sounds should fade out when they stop playing")]
		public bool FadeOut;

		[Tooltip("the duration of the fade, in seconds")]
		public float FadeDuration;

		[Tooltip("the tween to use when fading the sound")]
		public MMTweenType FadeTween;

		[MMInspectorGroup("Time", true, 20, false)]
		[Tooltip("whether or not the playlist manager should have its pitch multiplier value driven by the current timescale. If set to true, songs would appear to slow down when time is slowed down, and to speed up when time scale is higher than normal")]
		public bool BindPitchToTimeScale;

		[Tooltip("the values to remap timescale from (min and max) - when timescale is equal to TimescaleRemapFrom.x, the pitch multiplier will be TimescaleRemapTo.x")]
		[MMCondition("BindPitchToTimeScale", true)]
		public Vector2 TimescaleRemapFrom;

		[Tooltip("the values to remap timescale to (min and max) - when timescale is equal to TimescaleRemapFrom.x, the pitch multiplier will be TimescaleRemapTo.x")]
		[MMCondition("BindPitchToTimeScale", true)]
		public Vector2 TimescaleRemapTo;

		[MMInspectorGroup("Status", true, 14, false)]
		[Tooltip("the current state of the playlist, debug display only")]
		[MMReadOnly]
		public PlaylistManagerStates DebugCurrentManagerState;

		[Tooltip("the index we're currently playing")]
		[MMReadOnly]
		public int CurrentSongIndex;

		[Tooltip("the name of the song that is currently playing")]
		[MMReadOnly]
		public string CurrentSongName;

		[MMReadOnly]
		public MMStateMachine<PlaylistManagerStates> PlaylistManagerState;

		[Tooltip("the time of the currently playing song")]
		[MMReadOnly]
		public float CurrentTime;

		[Tooltip("the time (in seconds) left on the song currently playing")]
		[MMReadOnly]
		public float CurrentTimeLeft;

		[Tooltip("the total duration of the song currently playing")]
		[MMReadOnly]
		public float CurrentClipDuration;

		[Tooltip("the current normalized progress of the song currently playing")]
		[Range(0f, 1f)]
		public float CurrentProgress;

		[MMInspectorGroup("Test Controls", true, 15, false)]
		[MMInspectorButton("Play")]
		public bool PlayButton;

		[MMInspectorButton("Stop")]
		public bool StopButton;

		[MMInspectorButton("Pause")]
		public bool PauseButton;

		[MMInspectorButton("PlayPreviousSong")]
		public bool PreviousButton;

		[MMInspectorButton("PlayNextSong")]
		public bool NextButton;

		[Tooltip("the index of the song to play when pressing the PlayTargetSong button")]
		public int TargetSongIndex;

		[MMInspectorButton("PlayTargetSong")]
		public bool TargetSongButton;

		[MMInspectorButton("QueueTargetSong")]
		public bool QueueTargetSongButton;

		[MMInspectorButton("SetCurrentSongToLoop")]
		public bool SetLoopTargetSongButton;

		[MMInspectorButton("StopCurrentSongFromLooping")]
		public bool StopLoopTargetSongButton;

		[Tooltip("a playlist you can set to use with the SetTargetPlaylist and PlayTargetPlaylist buttons")]
		public MMSMPlaylist TestPlaylist;

		[MMInspectorButton("SetTargetPlaylist")]
		public bool SetTargetPlaylistButton;

		[MMInspectorButton("PlayTargetPlaylist")]
		public bool PlayTargetPlaylistButton;

		[MMInspectorButton("ResetPlayCount")]
		public bool ResetPlayCountButton;

		[Tooltip("a slider used to test volume control")]
		[Range(0f, 2f)]
		public float TestVolumeControl;

		[Tooltip("a slider used to test speed control")]
		[Range(0f, 20f)]
		public float TestPlaybackSpeedControl;

		public PlaylistEvent OnSongStart;

		public PlaylistEvent OnSongEnd;

		public PlaylistEvent OnPause;

		public PlaylistEvent OnStop;

		public PlaylistEvent OnPlaylistChange;

		public PlaylistEvent OnPlaylistEnd;

		protected bool _shouldResumeOnApplicationPause;

		protected static MMSMPlaylistManager _instance;

		protected int _queuedSongIndex;

		protected AudioSource _currentlyPlayingAudioSource;

		protected MMSoundManagerPlayOptions _options;

		protected float _lastTestVolumeControl;

		protected float _lastTestPlaybackSpeedControl;

		internal bool _listeningToEvents;

		public virtual bool IsPlaying => false;

		public static bool HasInstance => false;

		public static MMSMPlaylistManager Current => null;

		public static MMSMPlaylistManager Instance => null;

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

		protected virtual void InitializeRandomSeed()
		{
		}

		protected virtual void InitializePlaylistManagerState()
		{
		}

		protected virtual void ChangePlaylistManagerState(PlaylistManagerStates newManagerState)
		{
		}

		protected virtual void Update()
		{
		}

		protected virtual void HandleTimescale()
		{
		}

		protected virtual void UpdateTimeAndProgress()
		{
		}

		protected virtual void PlayFirstSong()
		{
		}

		protected virtual void HandleEndOfSong()
		{
		}

		protected virtual void HandleNextSong(int direction, bool bypassLoop)
		{
		}

		protected virtual void HandleEndOfPlaylist()
		{
		}

		public virtual void Play()
		{
		}

		public virtual void PlaySongAt(int songIndex)
		{
		}

		public virtual void Pause()
		{
		}

		public virtual void Stop()
		{
		}

		public virtual void StopWithFade(bool withFade = true)
		{
		}

		public virtual void SetCurrentSongLoop(bool loop)
		{
		}

		public virtual void PlayNextSong()
		{
		}

		public virtual void PlayPreviousSong()
		{
		}

		public virtual void QueueSongAtIndex(int songIndex)
		{
		}

		public virtual void ChangePlaylist(MMSMPlaylist newPlaylist)
		{
		}

		public virtual void ChangePlaylistAndPlay(MMSMPlaylist newPlaylist)
		{
		}

		public virtual void ResetPlayCount()
		{
		}

		public virtual void SetVolumeMultiplier(float newVolumeMultiplier)
		{
		}

		public virtual void SetPitchMultiplier(float newPitchMultiplier)
		{
		}

		protected virtual void SetTargetPlaylist()
		{
		}

		protected virtual void PlayTargetPlaylist()
		{
		}

		protected virtual void QueueTargetSong()
		{
		}

		protected virtual void PlayTargetSong()
		{
		}

		protected virtual void SetCurrentSongToLoop()
		{
		}

		protected virtual void StopCurrentSongFromLooping()
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

		protected virtual void OnMMPlaylistPitchMultiplierEvent(int channel, float newPitchMultiplier, bool applyPitchMultiplierInstantly = false)
		{
		}

		protected virtual void OnMMPlaylistChangeEvent(int channel, MMSMPlaylist newPlaylist, bool andPlay)
		{
		}

		public virtual void StartListening()
		{
		}

		public virtual void StopListening()
		{
		}

		protected virtual void OnDestroy()
		{
		}

		protected virtual void OnApplicationPause(bool pauseStatus)
		{
		}
	}
}
