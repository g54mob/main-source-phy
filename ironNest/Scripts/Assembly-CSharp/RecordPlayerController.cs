using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

[DisallowMultipleComponent]
[AddComponentMenu("Gameplay/Record Player Controller")]
public class RecordPlayerController : MonoBehaviour
{
	private enum NewspaperPlaybackState
	{
		Idle = 0,
		FadingRecordOut = 1,
		WaitingForCue = 2,
		DelayingCue = 3,
		FadingCueIn = 4,
		CuePlaying = 5,
		FadingCueOut = 6,
		RestoringRecord = 7
	}

	[CompilerGenerated]
	private sealed class _003CButtonActivationRoutine_003Ed__79 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public RecordPlayerController _003C_003E4__this;

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
		public _003CButtonActivationRoutine_003Ed__79(int _003C_003E1__state)
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
	private sealed class _003CCrossfadeRoutine_003Ed__100 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public RecordPlayerController _003C_003E4__this;

		public AudioSource incoming;

		public AudioClip incomingClip;

		public AudioSource outgoing;

		private float _003Celapsed_003E5__2;

		private float _003Cduration_003E5__3;

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
		public _003CCrossfadeRoutine_003Ed__100(int _003C_003E1__state)
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
	private sealed class _003CFadeNewspaperCueIn_003Ed__90 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public RecordPlayerController _003C_003E4__this;

		private float _003Celapsed_003E5__2;

		private float _003Cduration_003E5__3;

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
		public _003CFadeNewspaperCueIn_003Ed__90(int _003C_003E1__state)
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
	private sealed class _003CFadeNewspaperCueOut_003Ed__91 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public RecordPlayerController _003C_003E4__this;

		private float _003CstartWeight_003E5__2;

		private float _003Celapsed_003E5__3;

		private float _003Cduration_003E5__4;

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
		public _003CFadeNewspaperCueOut_003Ed__91(int _003C_003E1__state)
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
	private sealed class _003CFadeRecordOutForNewspaper_003Ed__88 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public RecordPlayerController _003C_003E4__this;

		private float _003Celapsed_003E5__2;

		private float _003Cduration_003E5__3;

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
		public _003CFadeRecordOutForNewspaper_003Ed__88(int _003C_003E1__state)
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
	private sealed class _003CNewspaperStartDelayRoutine_003Ed__89 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public RecordPlayerController _003C_003E4__this;

		private float _003Celapsed_003E5__2;

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
		public _003CNewspaperStartDelayRoutine_003Ed__89(int _003C_003E1__state)
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
	private sealed class _003CRestoreRecordAfterNewspaper_003Ed__92 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public RecordPlayerController _003C_003E4__this;

		private bool _003CrestoreAPlaying_003E5__2;

		private bool _003CrestoreBPlaying_003E5__3;

		private float _003Celapsed_003E5__4;

		private float _003Cduration_003E5__5;

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
		public _003CRestoreRecordAfterNewspaper_003Ed__92(int _003C_003E1__state)
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
	private sealed class _003CResumeNewspaperCrossfade_003Ed__94 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public RecordPlayerController _003C_003E4__this;

		public AudioSource outgoing;

		public AudioSource incoming;

		private float _003CstartOutgoingWeight_003E5__2;

		private float _003CstartIncomingWeight_003E5__3;

		private float _003Celapsed_003E5__4;

		private float _003Cduration_003E5__5;

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
		public _003CResumeNewspaperCrossfade_003Ed__94(int _003C_003E1__state)
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
	private sealed class _003CRunNewspaperTransition_003Ed__87 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public RecordPlayerController _003C_003E4__this;

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
		public _003CRunNewspaperTransition_003Ed__87(int _003C_003E1__state)
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
	private sealed class _003CStartDelayRoutine_003Ed__80 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public RecordPlayerController _003C_003E4__this;

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
		public _003CStartDelayRoutine_003Ed__80(int _003C_003E1__state)
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
	private sealed class _003CStopDelayRoutine_003Ed__81 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public RecordPlayerController _003C_003E4__this;

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
		public _003CStopDelayRoutine_003Ed__81(int _003C_003E1__state)
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

	[Header("References (Required)")]
	[Tooltip("The ItemSlot that accepts record DraggableItems.\n\nThis controller subscribes to onSlotFilled and onSlotCleared on this slot.\nRequired — controller will log an error and disable itself if missing.")]
	[SerializeField]
	private ItemSlot slot;

	[Tooltip("Primary AudioSource (Source A).\n\nAlternates with Source B during crossfade transitions.\nRequired settings:\n- PlayOnAwake: false\n- Loop:        false")]
	[SerializeField]
	private AudioSource audioSourceA;

	[Tooltip("Secondary AudioSource (Source B).\n\nUsed as the incoming source during crossfade transitions.\nRequired settings:\n- PlayOnAwake: false\n- Loop:        false\n\nCan live on the same GameObject as Source A.")]
	[SerializeField]
	private AudioSource audioSourceB;

	[Tooltip("Dedicated AudioSource for end-of-mission newspaper music.")]
	[SerializeField]
	private AudioSource newspaperAudioSource;

	[Tooltip("The LookAtTarget used as the play/stop toggle button.\n\n- SetActive(false) on Awake and whenever the slot is empty.\n- SetActive(true)  after buttonActivationDelaySeconds once a record is placed.\n- onClickDown is wired to TogglePlayStop automatically.")]
	[SerializeField]
	private LookAtTarget playButton;

	[Header("Button Activation Delay")]
	[Tooltip("Seconds (realtime) after a record is placed into the slot before the play\nbutton becomes pressable.\n\nUseful for diegetic 'record settling on platter' animations.\nIf the record is removed before this delay completes it is cancelled and\nthe button remains inactive.\n\nUses realtime seconds — not affected by Time.timeScale or game pause.\n\nSet to 0 for instant activation.\n\nSafe default: 0.")]
	[SerializeField]
	[Min(0f)]
	private float buttonActivationDelaySeconds;

	[Header("Playback Delays")]
	[Tooltip("Seconds (realtime) between the player pressing Play and audio starting.\n\nOnPlaybackStarted fires immediately on button press so animations can begin.\nAudio is held for this duration. Update() track-advance logic is fully\nsuppressed during this window.\n\nUses realtime seconds — not affected by Time.timeScale or game pause.\n\nSet to 0 for no delay.\n\nSafe default: 0.")]
	[SerializeField]
	[Min(0f)]
	private float playbackStartDelaySeconds;

	[Tooltip("Seconds (realtime) between the player pressing Stop and audio halting.\n\nOnPlaybackStopped fires immediately on button press so animations can begin.\nAudio continues for this duration before being silenced.\n\nDoes NOT apply when:\n- A record is physically removed (always immediate ForceStop).\n- A non-looping record reaches its natural end.\n\nUses realtime seconds — not affected by Time.timeScale or game pause.\n\nSet to 0 for no delay.\n\nSafe default: 0.")]
	[SerializeField]
	[Min(0f)]
	private float playbackStopDelaySeconds;

	[Header("Crossfade Settings")]
	[Tooltip("Seconds before the active track's natural end at which the next track\nbegins playing and starts fading in.\n\nShould be >= fadeDuration for a clean overlap.\nIf remaining track time is shorter than this value when evaluated,\nthe crossfade is skipped and a hard cut is used.\n\nSafe default: 3.0")]
	[SerializeField]
	[Min(0f)]
	private float overlapSeconds;

	[Tooltip("Duration in seconds of the simultaneous fade-out (outgoing) and\nfade-in (incoming) during a crossfade transition.\n\nShould be <= overlapSeconds.\n\nSafe default: 2.0")]
	[SerializeField]
	[Min(0f)]
	private float fadeDuration;

	[Header("Newspaper Music")]
	[Tooltip("Seconds (realtime) to fade the PREVIOUS RECORD out when the newspaper\nsequence begins.\n\nThis is the very first step: whatever the record player is currently\ndoing (or silent) fades to 0 over this duration before anything\nnewspaper-related starts.\n\nDoes NOT affect the newspaper cue's own fade-out — see\nnewspaperMusicFadeOutSeconds for that.\n\nUses realtime seconds — not affected by Time.timeScale or game pause.\n\nSet to 0 for an instant cut.\n\nSafe default: 1.")]
	[SerializeField]
	[Min(0f)]
	private float previousRecordFadeOutSeconds;

	[Tooltip("Seconds (realtime) of silence after the previous record has fully\nfaded out (previousRecordFadeOutSeconds) and\nbefore the newspaper cue begins fading in.\n\nAffects ONLY the newspaper music cue. It does NOT affect record playback,\nplaybackStartDelaySeconds, buttonActivationDelaySeconds, or any other\ndelay in this controller.\n\nTiming notes:\n- If PlayNewspaperMusic's clip is supplied before the record finishes\n  fading out, this delay starts counting immediately once the record\n  fade-out completes.\n- If no clip has been supplied yet (state is WaitingForCue) when the\n  record finishes fading out, this delay does not start counting until\n  PlayNewspaperMusic(clip, ...) actually supplies a clip.\n- If DismissNewspaperMusic() is called (or the sequence is otherwise\n  dismissed) while this delay is in progress, the remaining wait is\n  skipped immediately — the cue never plays, and the sequence proceeds\n  straight to restoring the previous record (or Idle).\n\nUses realtime seconds — not affected by Time.timeScale or game pause.\n\nSet to 0 for no delay (cue fades in immediately after the record\nfade-out completes).\n\nSafe default: 0.")]
	[SerializeField]
	[Min(0f)]
	private float newspaperStartDelaySeconds;

	[Tooltip("Seconds (realtime) to fade the NEWSPAPER CUE in once it starts playing\n(after previousRecordFadeOutSeconds and, if set, newspaperStartDelaySeconds\nhave both elapsed).\n\nUses a smoothstep curve (eases in gradually rather than linearly), so the\ncue is barely audible for roughly the first third of this duration.\n\nDoes NOT affect the previous record's fade-out or fade-back-in — see\npreviousRecordFadeOutSeconds and previousRecordFadeBackInSeconds.\n\nUses realtime seconds — not affected by Time.timeScale or game pause.\n\nSet to 0 for an instant cut to full volume.\n\nSafe default: 1.")]
	[SerializeField]
	[Min(0f)]
	private float newspaperMusicFadeInSeconds;

	[Tooltip("Seconds (realtime) to fade the NEWSPAPER CUE out once it is dismissed\n(DismissNewspaperMusic() is called, or the sequence is stopped while\nthe cue is playing or still fading in).\n\nUses a smoothstep curve starting from whatever volume the cue was at\nwhen the dismiss happened (so an interrupted fade-in fades back out\nsmoothly rather than jumping).\n\nDoes NOT affect the previous record's fade-out at the start of the\nsequence — see previousRecordFadeOutSeconds for that.\n\nUses realtime seconds — not affected by Time.timeScale or game pause.\n\nSet to 0 for an instant cut to silence.\n\nSafe default: 1.")]
	[SerializeField]
	[Min(0f)]
	private float newspaperMusicFadeOutSeconds;

	[Tooltip("Seconds (realtime) to fade the PREVIOUS RECORD back in once the\nnewspaper cue has finished fading out and the record is being\nrestored.\n\nOnly relevant when the record is actually restorable (the record\nwasn't removed/changed while the newspaper cue was playing, and\nrestorePreviousMusic was true when PlayNewspaperMusic was called).\nIf nothing is restored, this value has no effect.\n\nDoes NOT affect the newspaper cue's own fade-in — see\nnewspaperMusicFadeInSeconds for that.\n\nUses realtime seconds — not affected by Time.timeScale or game pause.\n\nSet to 0 for an instant cut back to full volume.\n\nSafe default: 1.")]
	[SerializeField]
	[Min(0f)]
	private float previousRecordFadeBackInSeconds;

	[Header("Volume")]
	[Tooltip("Master volume multiplier applied to both AudioSources at all times.\nRange: 0 (silent) to 1 (full volume).\n\nThis value is the authoritative volume state used by all internal\nplayback and crossfade code. It can be driven at runtime via\nSetMasterVolume(float), e.g. from a RecordPlayerVolumeDialBridge.\n\nSafe default: 1.")]
	[SerializeField]
	[Range(0f, 1f)]
	private float masterVolume;

	[Header("Events")]
	[Tooltip("Fired immediately when the player presses Play, BEFORE the start delay.\nUse to trigger spin-up / needle-drop animations.\nAudio begins after playbackStartDelaySeconds.")]
	public UnityEvent OnPlaybackStarted;

	[Tooltip("Fired immediately when playback stops. Two cases:\n1. Player presses Stop (before stop delay; audio trails off).\n2. Final track ends on a non-looping record (no stop delay).\n\nNOT fired when a record is physically removed — use OnRecordRemoved.")]
	public UnityEvent OnPlaybackStopped;

	[Tooltip("Fired when a record is pulled out of the slot while audio is playing.\nUse to trigger scratch SFX or abort animations.\n\nOnPlaybackStopped is NOT fired in this case.\nAll pending delays are cancelled immediately.")]
	public UnityEvent OnRecordRemoved;

	[Tooltip("Fired each time the track index advances, including the wrap to 0.\nArgument: new zero-based track index.\n\nUse to update diegetic track-name displays, trigger lighting cues, etc.")]
	public UnityEvent<int> OnTrackChanged;

	[Header("Debug")]
	[Tooltip("Logs slot changes, state transitions, delays, track advances, and crossfade\nevents to the Console.\n\nSafe default: false.")]
	[SerializeField]
	private bool debugLogs;

	private RecordItem _currentRecord;

	private bool _isPlaying;

	private int _trackIndex;

	private bool _isLastTrack;

	private bool _useAAsActive;

	private float _activeWeight;

	private float _inactiveWeight;

	private Coroutine _crossfadeRoutine;

	private bool _crossfadePending;

	private Coroutine _buttonActivationRoutine;

	private Coroutine _startDelayRoutine;

	private Coroutine _stopDelayRoutine;

	private float _savedActiveTime;

	private float _savedInactiveTime;

	private bool _wasPausedByTimeScale;

	private NewspaperPlaybackState newspaperPlaybackState;

	private Coroutine newspaperTransitionRoutine;

	private AudioClip newspaperClip;

	private RecordItem newspaperSavedRecord;

	private bool newspaperOverrideActive;

	private bool newspaperDismissRequested;

	private bool newspaperRestorePreviousMusic;

	private bool newspaperSavedWasPlaying;

	private bool newspaperSavedAWasPlaying;

	private bool newspaperSavedBWasPlaying;

	private bool newspaperSavedStartDelay;

	private bool newspaperSavedCrossfade;

	private bool newspaperSavedUseAAsActive;

	private float newspaperSavedATime;

	private float newspaperSavedBTime;

	private float newspaperSavedAWeight;

	private float newspaperSavedBWeight;

	private float newspaperWeight;

	public float MasterVolume => 0f;

	private AudioSource ActiveSource => null;

	private AudioSource InactiveSource => null;

	public void SetMasterVolume(float volume)
	{
	}

	public void FadeOutForNewspaper()
	{
	}

	public void PlayNewspaperMusic(AudioClip clip, bool restorePreviousMusic)
	{
	}

	public void DismissNewspaperMusic()
	{
	}

	private void Awake()
	{
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void Update()
	{
	}

	private void SavePlaybackPositions()
	{
	}

	private void RestorePlaybackPositions()
	{
	}

	private void OnApplicationPause(bool pauseStatus)
	{
	}

	private void HandleSlotFilled()
	{
	}

	private void HandleSlotCleared()
	{
	}

	public void TogglePlayStop()
	{
	}

	private void StartPlayback()
	{
	}

	private void StopPlayback()
	{
	}

	public void ForceStop()
	{
	}

	private void HaltAudioImmediate()
	{
	}

	[IteratorStateMachine(typeof(_003CButtonActivationRoutine_003Ed__79))]
	private IEnumerator ButtonActivationRoutine()
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CStartDelayRoutine_003Ed__80))]
	private IEnumerator StartDelayRoutine()
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CStopDelayRoutine_003Ed__81))]
	private IEnumerator StopDelayRoutine()
	{
		return null;
	}

	private void CancelButtonActivation()
	{
	}

	private void CancelStartDelay()
	{
	}

	private void CancelStopDelay()
	{
	}

	private void BeginNewspaperOverride()
	{
	}

	private void StartNewspaperTransition()
	{
	}

	[IteratorStateMachine(typeof(_003CRunNewspaperTransition_003Ed__87))]
	private IEnumerator RunNewspaperTransition()
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CFadeRecordOutForNewspaper_003Ed__88))]
	private IEnumerator FadeRecordOutForNewspaper()
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CNewspaperStartDelayRoutine_003Ed__89))]
	private IEnumerator NewspaperStartDelayRoutine()
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CFadeNewspaperCueIn_003Ed__90))]
	private IEnumerator FadeNewspaperCueIn()
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CFadeNewspaperCueOut_003Ed__91))]
	private IEnumerator FadeNewspaperCueOut()
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CRestoreRecordAfterNewspaper_003Ed__92))]
	private IEnumerator RestoreRecordAfterNewspaper()
	{
		return null;
	}

	private void RestoreNewspaperSource(AudioSource source, float savedTime, bool shouldPlay)
	{
	}

	[IteratorStateMachine(typeof(_003CResumeNewspaperCrossfade_003Ed__94))]
	private IEnumerator ResumeNewspaperCrossfade(AudioSource outgoing, AudioSource incoming)
	{
		return null;
	}

	private bool CanRestoreNewspaperRecord()
	{
		return false;
	}

	private void DiscardSavedRecordPlayback()
	{
	}

	private void FinishNewspaperOverride()
	{
	}

	private void CopyNewspaperAudioSourceSettings()
	{
	}

	private void AdvanceTrack(bool crossfade)
	{
	}

	[IteratorStateMachine(typeof(_003CCrossfadeRoutine_003Ed__100))]
	private IEnumerator CrossfadeRoutine(AudioClip incomingClip, AudioSource outgoing, AudioSource incoming)
	{
		return null;
	}

	private void StopCrossfadeRoutine()
	{
	}

	private void ApplyMasterVolumeToSources()
	{
	}

	private void UpdateIsLastTrack()
	{
	}

	private void PlayClipOnSource(AudioSource source, AudioClip clip, float weight)
	{
	}

	private void StopSourceImmediate(AudioSource source, bool isActive)
	{
	}

	private void SetButtonActive(bool active)
	{
	}

	private static float SmoothStep01(float t)
	{
		return 0f;
	}
}
