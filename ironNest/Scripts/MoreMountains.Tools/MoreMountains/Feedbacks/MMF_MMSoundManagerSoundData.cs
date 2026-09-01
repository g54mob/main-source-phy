using System;
using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.Audio;

namespace MoreMountains.Feedbacks
{
	[Serializable]
	[CreateAssetMenu(menuName = "MoreMountains/Audio/MMF_SoundData")]
	public class MMF_MMSoundManagerSoundData : ScriptableObject
	{
		[Header("Sound")]
		[Tooltip("the sound clip to play")]
		public AudioClip Sfx;

		[Header("Random Sound")]
		[Tooltip("an array to pick a random sfx from")]
		public AudioClip[] RandomSfx;

		[Tooltip("if this is true, random sfx audio clips will be played in sequential order instead of at random")]
		public bool SequentialOrder;

		[Tooltip("if we're in sequential order, determines whether or not to hold at the last index, until either a cooldown is met, or the ResetSequentialIndex method is called")]
		[MMFCondition("SequentialOrder", true)]
		public bool SequentialOrderHoldLast;

		[Tooltip("if we're in sequential order hold last mode, index will reset to 0 automatically after this duration, unless it's 0, in which case it'll be ignored")]
		[MMFCondition("SequentialOrderHoldLast", true)]
		public float SequentialOrderHoldCooldownDuration;

		[Tooltip("if this is true, sfx will be picked at random until all have been played. once this happens, the list is shuffled again, and it starts over")]
		public bool RandomUnique;

		[Header("Sound Properties")]
		[Header("Volume")]
		[Tooltip("the minimum volume to play the sound at")]
		[Range(0f, 2f)]
		public float MinVolume;

		[Tooltip("the maximum volume to play the sound at")]
		[Range(0f, 2f)]
		public float MaxVolume;

		[Header("Pitch")]
		[Tooltip("the minimum pitch to play the sound at")]
		[Range(-3f, 3f)]
		public float MinPitch;

		[Tooltip("the maximum pitch to play the sound at")]
		[Range(-3f, 3f)]
		public float MaxPitch;

		[Header("Time")]
		[Tooltip("a timestamp (in seconds, randomized between the defined min and max) at which the sound will start playing, equivalent to the Audiosource API's Time)")]
		[MMFVector(new string[] { "Min", "Max" })]
		public Vector2 PlaybackTime;

		[Tooltip("a duration (in seconds, randomized between the defined min and max) for which the sound will play before stopping. Ignored if min and max are zero.")]
		[MMVector(new string[] { "Min", "Max" })]
		public Vector2 PlaybackDuration;

		[Header("Sound Manager Options")]
		[Tooltip("the track on which to play the sound. Pick the one that matches the nature of your sound")]
		public MMSoundManager.MMSoundManagerTracks MmSoundManagerTrack;

		[Tooltip("the ID of the sound. This is useful if you plan on using sound control feedbacks on it afterwards.")]
		public int ID;

		[Tooltip("the AudioGroup on which to play the sound. If you're already targeting a preset track, you can leave it blank, otherwise the group you specify here will override it.")]
		public AudioMixerGroup AudioGroup;

		[Tooltip("if (for some reason) you've already got an audiosource and wouldn't like to use the built-in pool system, you can specify it here")]
		public AudioSource RecycleAudioSource;

		[Tooltip("whether or not this sound should loop")]
		public bool Loop;

		[Tooltip("whether or not this sound should continue playing when transitioning to another scene")]
		public bool Persistent;

		[Tooltip("whether or not this sound should play if the same sound clip is already playing")]
		public bool DoNotPlayIfClipAlreadyPlaying;

		[Tooltip("if this is true, this sound will stop playing when stopping the feedback")]
		public bool StopSoundOnFeedbackStop;

		[Header("Fade")]
		[Tooltip("whether or not to fade this sound in when playing it")]
		public bool Fade;

		[Tooltip("if fading, the volume at which to start the fade")]
		[MMCondition("Fade", true)]
		public float FadeInitialVolume;

		[Tooltip("if fading, the duration of the fade, in seconds")]
		[MMCondition("Fade", true)]
		public float FadeDuration;

		[Tooltip("if fading, the tween over which to fade the sound ")]
		[MMCondition("Fade", true)]
		public MMTweenType FadeTween;

		[Header("Solo")]
		[Tooltip("whether or not this sound should play in solo mode over its destination track. If yes, all other sounds on that track will be muted when this sound starts playing")]
		public bool SoloSingleTrack;

		[Tooltip("whether or not this sound should play in solo mode over all other tracks. If yes, all other tracks will be muted when this sound starts playing")]
		public bool SoloAllTracks;

		[Tooltip("if in any of the above solo modes, AutoUnSoloOnEnd will unmute the track(s) automatically once that sound stops playing")]
		public bool AutoUnSoloOnEnd;

		[Header("Spatial Settings")]
		[Tooltip("Pans a playing sound in a stereo way (left or right). This only applies to sounds that are Mono or Stereo.")]
		[Range(-1f, 1f)]
		public float PanStereo;

		[Tooltip("Sets how much this AudioSource is affected by 3D spatialisation calculations (attenuation, doppler etc). 0.0 makes the sound full 2D, 1.0 makes it full 3D.")]
		[Range(0f, 1f)]
		public float SpatialBlend;

		[Tooltip("a Transform this sound can 'attach' to and follow it along as it plays")]
		public Transform AttachToTransform;

		[Header("Effects")]
		[Tooltip("Bypass effects (Applied from filter components or global listener filters).")]
		public bool BypassEffects;

		[Tooltip("When set global effects on the AudioListener will not be applied to the audio signal generated by the AudioSource. Does not apply if the AudioSource is playing into a mixer group.")]
		public bool BypassListenerEffects;

		[Tooltip("When set doesn't route the signal from an AudioSource into the global reverb associated with reverb zones.")]
		public bool BypassReverbZones;

		[Tooltip("Sets the priority of the AudioSource.")]
		[Range(0f, 256f)]
		public int Priority;

		[Tooltip("The amount by which the signal from the AudioSource will be mixed into the global reverb associated with the Reverb Zones.")]
		[Range(0f, 1.1f)]
		public float ReverbZoneMix;

		[Header("3D Sound Settings")]
		[Tooltip("Sets the Doppler scale for this AudioSource.")]
		[Range(0f, 5f)]
		public float DopplerLevel;

		[Tooltip("Sets the spread angle (in degrees) of a 3d stereo or multichannel sound in speaker space.")]
		[Range(0f, 360f)]
		public int Spread;

		[Tooltip("Sets/Gets how the AudioSource attenuates over distance.")]
		public AudioRolloffMode RolloffMode;

		[Tooltip("Within the Min distance the AudioSource will cease to grow louder in volume.")]
		public float MinDistance;

		[Tooltip("(Logarithmic rolloff) MaxDistance is the distance a sound stops attenuating at.")]
		public float MaxDistance;

		[Tooltip("whether or not to use a custom curve for custom volume rolloff")]
		public bool UseCustomRolloffCurve;

		[Tooltip("the curve to use for custom volume rolloff if UseCustomRolloffCurve is true")]
		[MMCondition("UseCustomRolloffCurve", true)]
		public AnimationCurve CustomRolloffCurve;

		[Tooltip("whether or not to use a custom curve for spatial blend")]
		public bool UseSpatialBlendCurve;

		[Tooltip("the curve to use for custom spatial blend if UseSpatialBlendCurve is true")]
		[MMCondition("UseSpatialBlendCurve", true)]
		public AnimationCurve SpatialBlendCurve;

		[Tooltip("whether or not to use a custom curve for reverb zone mix")]
		public bool UseReverbZoneMixCurve;

		[Tooltip("the curve to use for custom reverb zone mix if UseReverbZoneMixCurve is true")]
		[MMCondition("UseReverbZoneMixCurve", true)]
		public AnimationCurve ReverbZoneMixCurve;

		[Tooltip("whether or not to use a custom curve for spread")]
		public bool UseSpreadCurve;

		[Tooltip("the curve to use for custom spread if UseSpreadCurve is true")]
		[MMCondition("UseSpreadCurve", true)]
		public AnimationCurve SpreadCurve;
	}
}
