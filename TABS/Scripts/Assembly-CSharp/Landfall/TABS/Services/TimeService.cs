using System.Collections;
using Landfall.TABS.GameMode;
using TFBG;
using TFBGames;
using UnityEngine;
using UnityEngine.Audio;

namespace Landfall.TABS.Services
{
	public class TimeService : ITimeService, IService, IDisruptionServiceSubscriber
	{
		private struct TimeState
		{
			public float TargetTimeScale;

			public AnimationCurve TransitionCurve;

			public float TransitionTime;
		}

		private float m_currentAnimationTime;

		private MonoBehaviour m_coroutineContext;

		private Coroutine m_coroutine;

		private bool m_isPaused;

		private bool m_isLocked;

		private TimeState m_previousState;

		private TimeState m_destinationState;

		private AudioMixer m_audioMixer;

		private AnimationCurve m_lowpassCurve;

		private int m_lowFreq = 200;

		private int m_normalFreq = 22000;

		private bool m_usingAudioCurve;

		private int m_allowZeroTimeScaleForMultiplayerReferenceCount;

		private INetworkService m_networkService;

		private GameDisruptionService gameDisruptionService;

		public float TransitionProgress
		{
			get
			{
				return m_currentAnimationTime;
			}
			set
			{
				m_currentAnimationTime = value;
			}
		}

		public float CurrentTargetTimeScale => m_destinationState.TargetTimeScale;

		public bool IsZeroTimeScaleAllowedForMultiplayer { get; private set; } = true;

		public void Init(AudioMixer audioMorphMixer, AnimationCurve audioLowpassCurve)
		{
			m_audioMixer = audioMorphMixer;
			m_lowpassCurve = audioLowpassCurve;
			m_networkService = ServiceLocator.GetService<INetworkService>();
		}

		public bool IsPaused()
		{
			return m_isPaused;
		}

		public void Pause()
		{
			m_isPaused = true;
		}

		public void UnPause()
		{
			m_isPaused = false;
		}

		public void Lock()
		{
			m_isLocked = true;
		}

		public void Unlock()
		{
			m_isLocked = false;
		}

		public bool IsLocked()
		{
			return m_isLocked;
		}

		public void SetState(float targetTimeScale, float transitionTime, bool useAudioMixer = true)
		{
			SetState(targetTimeScale, transitionTime, null, useAudioMixer);
		}

		public void SetState(AnimationCurve transitionCurve, bool useAudioMixer = true)
		{
			float targetTimeScale = ExtractTargetValueFromCurve(transitionCurve);
			float transitionTime = ExtractDurationFromCurve(transitionCurve);
			SetState(targetTimeScale, transitionTime, transitionCurve, useAudioMixer);
		}

		public void ResetAudioMixer()
		{
			if (m_audioMixer != null)
			{
				m_audioMixer.SetFloat("LowPass", m_normalFreq);
			}
		}

		public void PreventZeroTimeScaleForMultiplayer()
		{
			m_allowZeroTimeScaleForMultiplayerReferenceCount++;
			IsZeroTimeScaleAllowedForMultiplayer = false;
		}

		public void AllowZeroTimeScaleForMultiplayer()
		{
			m_allowZeroTimeScaleForMultiplayerReferenceCount--;
			if (m_allowZeroTimeScaleForMultiplayerReferenceCount <= 0)
			{
				if (m_allowZeroTimeScaleForMultiplayerReferenceCount < 0)
				{
					Debug.LogWarning("AllowZeroTimeScaleForMultiplayer was called more times than PreventZeroTimeScaleForMultiplayer.");
				}
				m_allowZeroTimeScaleForMultiplayerReferenceCount = 0;
				IsZeroTimeScaleAllowedForMultiplayer = true;
			}
		}

		private void SetState(float targetTimeScale, float transitionTime, AnimationCurve transitionCurve, bool useAudioMixer)
		{
			if (!m_isLocked && (IsZeroTimeScaleAllowedForMultiplayer || m_networkService == null || !m_networkService.IsRunning || !Mathf.Approximately(targetTimeScale, 0f)))
			{
				m_usingAudioCurve = useAudioMixer;
				TimeState destinationState = new TimeState
				{
					TargetTimeScale = targetTimeScale,
					TransitionTime = transitionTime,
					TransitionCurve = transitionCurve
				};
				m_previousState = m_destinationState;
				m_destinationState = destinationState;
				ApplyState();
			}
		}

		private void ApplyState()
		{
			if (m_coroutine != null)
			{
				m_coroutineContext.StopCoroutine(m_coroutine);
				m_coroutine = null;
			}
			TimeState destinationState = m_destinationState;
			if (destinationState.TransitionTime <= 0f)
			{
				Time.timeScale = destinationState.TargetTimeScale;
				if (m_audioMixer != null && m_usingAudioCurve)
				{
					float t = m_lowpassCurve.Evaluate(Time.timeScale);
					float value = Mathf.Lerp(m_lowFreq, m_normalFreq, t);
					m_audioMixer.SetFloat("LowPass", value);
				}
			}
			else
			{
				m_coroutine = m_coroutineContext.StartCoroutine(AnimateTime(destinationState));
			}
		}

		private float ExtractDurationFromCurve(AnimationCurve curve)
		{
			Keyframe keyframe = curve.keys[curve.keys.Length - 1];
			return keyframe.time;
		}

		private float ExtractTargetValueFromCurve(AnimationCurve curve)
		{
			Keyframe keyframe = curve.keys[curve.keys.Length - 1];
			return keyframe.value;
		}

		public void OnRegister()
		{
		}

		public void OnAwake()
		{
		}

		public void OnStart()
		{
			m_coroutineContext = ServiceLocator.GetService<GameModeService>();
		}

		public void OnUpdate()
		{
		}

		public void Subscribe()
		{
			gameDisruptionService = ServiceLocator.GetService<GameDisruptionService>();
			if (!(gameDisruptionService == null))
			{
				gameDisruptionService.AddWatchedScript(this);
			}
		}

		public void Unsubscribe()
		{
		}

		private IEnumerator AnimateTime(TimeState targetState)
		{
			float initialTS = Time.timeScale;
			m_currentAnimationTime = 0f;
			bool usesCurve = targetState.TransitionCurve != null;
			while (m_currentAnimationTime < targetState.TransitionTime)
			{
				if (usesCurve)
				{
					Time.timeScale = Mathf.Max(0f, targetState.TransitionCurve.Evaluate(m_currentAnimationTime));
				}
				else
				{
					Time.timeScale = Mathf.Lerp(initialTS, targetState.TargetTimeScale, m_currentAnimationTime / targetState.TransitionTime);
				}
				m_currentAnimationTime += Time.unscaledDeltaTime;
				if (m_audioMixer != null && m_usingAudioCurve)
				{
					float t = m_lowpassCurve.Evaluate(Time.timeScale);
					float value = Mathf.Lerp(m_lowFreq, m_normalFreq, t);
					m_audioMixer.SetFloat("LowPass", value);
				}
				yield return null;
			}
			Time.timeScale = targetState.TargetTimeScale;
			if (m_audioMixer != null && m_usingAudioCurve)
			{
				float t2 = m_lowpassCurve.Evaluate(Time.timeScale);
				float value2 = Mathf.Lerp(m_lowFreq, m_normalFreq, t2);
				m_audioMixer.SetFloat("LowPass", value2);
			}
		}

		public void OnFixedUpdate()
		{
		}

		public void OnLateUpdate()
		{
		}

		public void UnRegister()
		{
		}
	}
}
