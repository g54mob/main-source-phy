using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu(null)]
	[MovedFrom(false, null, "MoreMountains.Feedbacks", null)]
	[FeedbackPath("Audio/AudioSource")]
	[FeedbackHelp("This feedback lets you play a target audio source, with some elements at random.")]
	public class MMF_AudioSource : MMF_Feedback
	{
		public enum Modes
		{
			Play = 0,
			Pause = 1,
			UnPause = 2,
			Stop = 3
		}

		public static bool FeedbackTypeAuthorized;

		[MMFInspectorGroup("Audiosource", true, 28, true, false)]
		[Tooltip("the target audio source to play")]
		public AudioSource TargetAudioSource;

		[Tooltip("whether we should play the audio source or stop it or pause it")]
		public Modes Mode;

		[Header("Random Sound")]
		[Tooltip("an array to pick a random sfx from")]
		public AudioClip[] RandomSfx;

		[MMFInspectorGroup("Audio Settings", true, 29, false, false)]
		[Header("Volume")]
		[Tooltip("the minimum volume to play the sound at")]
		public float MinVolume;

		[Tooltip("the maximum volume to play the sound at")]
		public float MaxVolume;

		[Header("Pitch")]
		[Tooltip("the minimum pitch to play the sound at")]
		public float MinPitch;

		[Tooltip("the maximum pitch to play the sound at")]
		public float MaxPitch;

		[Header("Mixer")]
		[Tooltip("the audiomixer to play the sound with (optional)")]
		public AudioMixerGroup SfxAudioMixerGroup;

		protected AudioClip _randomClip;

		protected float _duration;

		public override bool HasRandomness => false;

		public override bool HasAutomatedTargetAcquisition => false;

		public override float FeedbackDuration
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		protected override void AutomateTargetAcquisition()
		{
		}

		protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		protected virtual void PlayAudioSource(AudioSource audioSource, float volume, float pitch)
		{
		}

		public override void Stop(Vector3 position, float feedbacksIntensity = 1f)
		{
		}
	}
}
