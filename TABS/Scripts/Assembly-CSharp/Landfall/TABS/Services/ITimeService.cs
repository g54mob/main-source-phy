using UnityEngine;
using UnityEngine.Audio;

namespace Landfall.TABS.Services
{
	public interface ITimeService : IService
	{
		float CurrentTargetTimeScale { get; }

		float TransitionProgress { get; set; }

		bool IsZeroTimeScaleAllowedForMultiplayer { get; }

		void Init(AudioMixer audioMorphMixer, AnimationCurve audioLowpassCurve);

		bool IsPaused();

		void Pause();

		void UnPause();

		void Lock();

		void Unlock();

		bool IsLocked();

		void SetState(float targetTimeScale, float transitionTime, bool useAudioMixer = true);

		void SetState(AnimationCurve transitionCurve, bool useAudioMixer = true);

		void ResetAudioMixer();

		void PreventZeroTimeScaleForMultiplayer();

		void AllowZeroTimeScaleForMultiplayer();
	}
}
