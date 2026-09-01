using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Audio;

namespace MoreMountains.Tools
{
	[StructLayout((LayoutKind)0, Size = 1)]
	public struct MMSoundManagerSoundPlayEvent
	{
		public delegate AudioSource Delegate(AudioClip clip, MMSoundManagerPlayOptions options);

		private static event Delegate OnEvent
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void RuntimeInitialization()
		{
		}

		public static void Register(Delegate callback)
		{
		}

		public static void Unregister(Delegate callback)
		{
		}

		public static AudioSource Trigger(AudioClip clip, MMSoundManagerPlayOptions options)
		{
			return null;
		}

		public static AudioSource Trigger(AudioClip audioClip, MMSoundManager.MMSoundManagerTracks mmSoundManagerTrack, Vector3 location, bool loop = false, float volume = 1f, int ID = 0, bool fade = false, float fadeInitialVolume = 0f, float fadeDuration = 1f, MMTweenType fadeTween = null, bool persistent = false, AudioSource recycleAudioSource = null, AudioMixerGroup audioGroup = null, float pitch = 1f, float panStereo = 0f, float spatialBlend = 0f, bool soloSingleTrack = false, bool soloAllTracks = false, bool autoUnSoloOnEnd = false, bool bypassEffects = false, bool bypassListenerEffects = false, bool bypassReverbZones = false, int priority = 128, float reverbZoneMix = 1f, float dopplerLevel = 1f, int spread = 0, AudioRolloffMode rolloffMode = AudioRolloffMode.Logarithmic, float minDistance = 1f, float maxDistance = 500f)
		{
			return null;
		}
	}
}
