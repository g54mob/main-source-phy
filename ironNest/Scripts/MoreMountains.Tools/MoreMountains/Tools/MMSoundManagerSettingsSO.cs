using System;
using UnityEngine;
using UnityEngine.Audio;

namespace MoreMountains.Tools
{
	[Serializable]
	[CreateAssetMenu(menuName = "MoreMountains/Audio/MMSoundManagerSettings")]
	public class MMSoundManagerSettingsSO : ScriptableObject
	{
		[Header("Audio Mixer")]
		[Tooltip("the audio mixer to use when playing sounds")]
		public AudioMixer TargetAudioMixer;

		[Tooltip("the master group")]
		public AudioMixerGroup MasterAudioMixerGroup;

		[Tooltip("the group on which to play all music sounds")]
		public AudioMixerGroup MusicAudioMixerGroup;

		[Tooltip("the group on which to play all sound effects")]
		public AudioMixerGroup SfxAudioMixerGroup;

		[Tooltip("the group on which to play all UI sounds")]
		public AudioMixerGroup UIAudioMixerGroup;

		[Tooltip("the multiplier to apply when converting normalized volume values to audio mixer values")]
		public float MixerValuesMultiplier;

		[Header("Settings Unfold")]
		[Tooltip("the full settings for this MMSoundManager")]
		public MMSoundManagerSettings Settings;

		protected const string _saveFolderName = "MMSoundManager/";

		protected const string _saveFileName = "mmsound.settings";

		public virtual void SaveSoundSettings()
		{
		}

		public virtual void LoadSoundSettings()
		{
		}

		public virtual void ResetSoundSettings()
		{
		}

		public virtual void SetTrackVolume(MMSoundManager.MMSoundManagerTracks track, float volume)
		{
		}

		public virtual float GetTrackVolume(MMSoundManager.MMSoundManagerTracks track)
		{
			return 0f;
		}

		public virtual void GetTrackVolumes()
		{
		}

		protected virtual void ApplyTrackVolumes()
		{
		}

		public virtual float NormalizedToMixerVolume(float normalizedVolume)
		{
			return 0f;
		}

		public virtual float MixerVolumeToNormalized(float mixerVolume)
		{
			return 0f;
		}
	}
}
