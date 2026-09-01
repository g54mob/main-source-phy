using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

namespace MoreMountains.Tools
{
	[AddComponentMenu("More Mountains/Tools/Audio/MMSoundManager")]
	public class MMSoundManager : MMPersistentSingleton<MMSoundManager>, MMEventListener<MMSoundManagerTrackEvent>, MMEventListenerBase, MMEventListener<MMSoundManagerEvent>, MMEventListener<MMSoundManagerSoundControlEvent>, MMEventListener<MMSoundManagerSoundFadeEvent>, MMEventListener<MMSoundManagerAllSoundsControlEvent>, MMEventListener<MMSoundManagerTrackFadeEvent>
	{
		public enum MMSoundManagerTracks
		{
			Sfx = 0,
			Music = 1,
			UI = 2,
			Master = 3,
			Other = 4
		}

		public enum ControlTrackModes
		{
			Mute = 0,
			Unmute = 1,
			SetVolume = 2
		}

		[CompilerGenerated]
		private sealed class _003CFadeCoroutine_003Ed__54 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MMTweenType tweenType;

			public float duration;

			public float initialVolume;

			public float finalVolume;

			public AudioSource source;

			public bool freeAfterFade;

			public MMSoundManager _003C_003E4__this;

			private float _003CstartedAt_003E5__2;

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
			public _003CFadeCoroutine_003Ed__54(int _003C_003E1__state)
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
		private sealed class _003CFadeTrackCoroutine_003Ed__53 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MMTweenType tweenType;

			public float duration;

			public float initialVolume;

			public float finalVolume;

			public MMSoundManager _003C_003E4__this;

			public MMSoundManagerTracks track;

			private float _003CstartedAt_003E5__2;

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
			public _003CFadeTrackCoroutine_003Ed__53(int _003C_003E1__state)
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
		private sealed class _003CMuteAllSoundsCoroutine_003Ed__58 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public float delay;

			public MMSoundManager _003C_003E4__this;

			public bool mute;

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
			public _003CMuteAllSoundsCoroutine_003Ed__58(int _003C_003E1__state)
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
		private sealed class _003CMuteSoundsOnTrackCoroutine_003Ed__57 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public float delay;

			public MMSoundManager _003C_003E4__this;

			public MMSoundManagerTracks track;

			public bool mute;

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
			public _003CMuteSoundsOnTrackCoroutine_003Ed__57(int _003C_003E1__state)
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

		[Header("Settings")]
		[Tooltip("the current sound settings ")]
		public MMSoundManagerSettingsSO settingsSo;

		[Header("Pool")]
		[Tooltip("the size of the AudioSource pool, a reserve of ready-to-use sources that will get recycled. Should be approximately equal to the maximum amount of sounds that you expect to be playing at once")]
		public int AudioSourcePoolSize;

		[Tooltip("whether or not the pool can expand (create new audiosources on demand). In a perfect world you'd want to avoid this, and have a sufficiently big pool, to avoid costly runtime creations.")]
		public bool PoolCanExpand;

		protected MMSoundManagerAudioPool _pool;

		protected GameObject _tempAudioSourceGameObject;

		protected MMSoundManagerSound _sound;

		protected List<MMSoundManagerSound> _sounds;

		protected AudioSource _tempAudioSource;

		protected Dictionary<AudioSource, Coroutine> _fadeInSoundCoroutines;

		protected Dictionary<AudioSource, Coroutine> _fadeOutSoundCoroutines;

		protected Dictionary<MMSoundManagerTracks, Coroutine> _fadeTrackCoroutines;

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		protected static void InitializeStatics()
		{
		}

		protected override void Awake()
		{
		}

		protected virtual void Start()
		{
		}

		protected virtual void InitializeSoundManager()
		{
		}

		public virtual AudioSource PlaySound(AudioClip audioClip, MMSoundManagerPlayOptions options)
		{
			return null;
		}

		public virtual AudioSource PlaySound(AudioClip audioClip, MMSoundManagerTracks mmSoundManagerTrack, Vector3 location, bool loop = false, float volume = 1f, int ID = 0, bool fade = false, float fadeInitialVolume = 0f, float fadeDuration = 1f, MMTweenType fadeTween = null, bool persistent = false, AudioSource recycleAudioSource = null, AudioMixerGroup audioGroup = null, float pitch = 1f, float panStereo = 0f, float spatialBlend = 0f, bool soloSingleTrack = false, bool soloAllTracks = false, bool autoUnSoloOnEnd = false, bool bypassEffects = false, bool bypassListenerEffects = false, bool bypassReverbZones = false, int priority = 128, float reverbZoneMix = 1f, float dopplerLevel = 1f, int spread = 0, AudioRolloffMode rolloffMode = AudioRolloffMode.Logarithmic, float minDistance = 1f, float maxDistance = 500f, bool doNotAutoRecycleIfNotDonePlaying = false, float playbackTime = 0f, float playbackDuration = 0f, Transform attachToTransform = null, bool useSpreadCurve = false, AnimationCurve spreadCurve = null, bool useCustomRolloffCurve = false, AnimationCurve customRolloffCurve = null, bool useSpatialBlendCurve = false, AnimationCurve spatialBlendCurve = null, bool useReverbZoneMixCurve = false, AnimationCurve reverbZoneMixCurve = null)
		{
			return null;
		}

		public virtual void PauseSound(AudioSource source)
		{
		}

		public virtual void ResumeSound(AudioSource source)
		{
		}

		public virtual void StopSound(AudioSource source)
		{
		}

		public virtual void FreeSound(AudioSource source)
		{
		}

		public virtual void MuteTrack(MMSoundManagerTracks track)
		{
		}

		public virtual void UnmuteTrack(MMSoundManagerTracks track)
		{
		}

		public virtual void SetTrackVolume(MMSoundManagerTracks track, float volume)
		{
		}

		public virtual float GetTrackVolume(MMSoundManagerTracks track, bool mutedVolume)
		{
			return 0f;
		}

		public virtual void PauseTrack(MMSoundManagerTracks track)
		{
		}

		public virtual void PlayTrack(MMSoundManagerTracks track)
		{
		}

		public virtual void StopTrack(MMSoundManagerTracks track)
		{
		}

		public virtual bool HasSoundsPlaying(MMSoundManagerTracks track)
		{
			return false;
		}

		public virtual List<MMSoundManagerSound> GetSoundsPlaying(MMSoundManagerTracks track)
		{
			return null;
		}

		public virtual void FreeTrack(MMSoundManagerTracks track)
		{
		}

		public virtual void MuteMusic()
		{
		}

		public virtual void UnmuteMusic()
		{
		}

		public virtual void MuteSfx()
		{
		}

		public virtual void UnmuteSfx()
		{
		}

		public virtual void MuteUI()
		{
		}

		public virtual void UnmuteUI()
		{
		}

		public virtual void MuteMaster()
		{
		}

		public virtual void UnmuteMaster()
		{
		}

		public virtual void SetVolumeMusic(float newVolume)
		{
		}

		public virtual void SetVolumeSfx(float newVolume)
		{
		}

		public virtual void SetVolumeUI(float newVolume)
		{
		}

		public virtual void SetVolumeMaster(float newVolume)
		{
		}

		public virtual bool IsMuted(MMSoundManagerTracks track)
		{
			return false;
		}

		protected virtual void ControlTrack(MMSoundManagerTracks track, ControlTrackModes trackMode, float volume = 0.5f)
		{
		}

		public virtual void FadeTrack(MMSoundManagerTracks track, float duration, float initialVolume = 0f, float finalVolume = 1f, MMTweenType tweenType = null)
		{
		}

		public virtual void FadeSound(AudioSource source, float duration, float initialVolume, float finalVolume, MMTweenType tweenType, bool freeAfterFade = false)
		{
		}

		public virtual bool SoundIsFadingIn(AudioSource source)
		{
			return false;
		}

		public virtual bool SoundIsFadingOut(AudioSource source)
		{
			return false;
		}

		public virtual void StopFadeTrack(MMSoundManagerTracks track)
		{
		}

		public virtual void StopFadeSound(AudioSource source)
		{
		}

		[IteratorStateMachine(typeof(_003CFadeTrackCoroutine_003Ed__53))]
		protected virtual IEnumerator FadeTrackCoroutine(MMSoundManagerTracks track, float duration, float initialVolume, float finalVolume, MMTweenType tweenType)
		{
			return null;
		}

		[IteratorStateMachine(typeof(_003CFadeCoroutine_003Ed__54))]
		protected virtual IEnumerator FadeCoroutine(AudioSource source, float duration, float initialVolume, float finalVolume, MMTweenType tweenType, bool freeAfterFade = false)
		{
			return null;
		}

		public virtual void MuteSoundsOnTrack(MMSoundManagerTracks track, bool mute, float delay = 0f)
		{
		}

		public virtual void MuteAllSounds(bool mute = true)
		{
		}

		[IteratorStateMachine(typeof(_003CMuteSoundsOnTrackCoroutine_003Ed__57))]
		protected virtual IEnumerator MuteSoundsOnTrackCoroutine(MMSoundManagerTracks track, bool mute, float delay)
		{
			return null;
		}

		[IteratorStateMachine(typeof(_003CMuteAllSoundsCoroutine_003Ed__58))]
		protected virtual IEnumerator MuteAllSoundsCoroutine(float delay, bool mute = true)
		{
			return null;
		}

		public virtual AudioSource FindByID(int ID)
		{
			return null;
		}

		public virtual AudioSource FindByClip(AudioClip clip)
		{
			return null;
		}

		public virtual void PauseAllSounds()
		{
		}

		public virtual void PlayAllSounds()
		{
		}

		public virtual void StopAllSounds()
		{
		}

		public virtual void FreeAllSounds()
		{
		}

		public virtual void FreeAllSoundsButPersistent()
		{
		}

		public virtual void FreeAllLoopingSounds()
		{
		}

		protected virtual void OnSceneLoaded(Scene arg0, LoadSceneMode loadSceneMode)
		{
		}

		public virtual void OnMMEvent(MMSoundManagerTrackEvent soundManagerTrackEvent)
		{
		}

		public virtual void OnMMEvent(MMSoundManagerEvent soundManagerEvent)
		{
		}

		public virtual void SaveSettings()
		{
		}

		public virtual void LoadSettings()
		{
		}

		public virtual void ResetSettings()
		{
		}

		public virtual void OnMMEvent(MMSoundManagerSoundControlEvent soundControlEvent)
		{
		}

		public virtual void OnMMEvent(MMSoundManagerTrackFadeEvent trackFadeEvent)
		{
		}

		public virtual void OnMMEvent(MMSoundManagerSoundFadeEvent soundFadeEvent)
		{
		}

		public virtual void OnMMEvent(MMSoundManagerAllSoundsControlEvent allSoundsControlEvent)
		{
		}

		public virtual void OnMMSfxEvent(AudioClip clipToPlay, AudioMixerGroup audioGroup = null, float volume = 1f, float pitch = 1f, int priority = 128)
		{
		}

		public virtual AudioSource OnMMSoundManagerSoundPlayEvent(AudioClip clip, MMSoundManagerPlayOptions options)
		{
			return null;
		}

		protected virtual void OnEnable()
		{
		}

		protected virtual void OnDisable()
		{
		}
	}
}
