using System;
using UnityEngine;
using UnityEngine.Audio;

namespace MoreMountains.Tools
{
	[Serializable]
	public struct MMSoundManagerPlayOptions
	{
		[HideInInspector]
		public bool Initialized;

		[Header("Track")]
		public MMSoundManager.MMSoundManagerTracks MmSoundManagerTrack;

		public AudioMixerGroup AudioGroup;

		[Header("Sound")]
		public bool Loop;

		[Range(0f, 2f)]
		public float Volume;

		[Range(-3f, 3f)]
		public float Pitch;

		public int ID;

		[Header("Fade")]
		public bool Fade;

		[MMCondition("Fade", true)]
		public float FadeInitialVolume;

		[MMCondition("Fade", true)]
		public float FadeDuration;

		[MMCondition("Fade", true)]
		public MMTweenType FadeTween;

		public bool Persistent;

		public AudioSource RecycleAudioSource;

		[Header("Time")]
		public float PlaybackTime;

		public float PlaybackDuration;

		[Header("Spatial Settings")]
		[Range(-1f, 1f)]
		public float PanStereo;

		[Range(0f, 1f)]
		public float SpatialBlend;

		public Transform AttachToTransform;

		[Header("Solo")]
		public bool SoloSingleTrack;

		public bool SoloAllTracks;

		public bool AutoUnSoloOnEnd;

		public bool BypassEffects;

		public bool BypassListenerEffects;

		public bool BypassReverbZones;

		[Range(0f, 256f)]
		public int Priority;

		[Range(0f, 1.1f)]
		public float ReverbZoneMix;

		[Header("3D Sound Settings")]
		[Range(0f, 5f)]
		public float DopplerLevel;

		public Vector3 Location;

		[Range(0f, 360f)]
		public int Spread;

		public AudioRolloffMode RolloffMode;

		public float MinDistance;

		public float MaxDistance;

		public bool DoNotAutoRecycleIfNotDonePlaying;

		public bool UseCustomRolloffCurve;

		[MMCondition("UseCustomRolloffCurve", true)]
		public AnimationCurve CustomRolloffCurve;

		public bool UseSpatialBlendCurve;

		[MMCondition("UseSpatialBlendCurve", true)]
		public AnimationCurve SpatialBlendCurve;

		public bool UseReverbZoneMixCurve;

		[MMCondition("UseReverbZoneMixCurve", true)]
		public AnimationCurve ReverbZoneMixCurve;

		public bool UseSpreadCurve;

		[MMCondition("UseSpreadCurve", true)]
		public AnimationCurve SpreadCurve;

		public static MMSoundManagerPlayOptions Default => default(MMSoundManagerPlayOptions);
	}
}
