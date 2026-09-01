using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.Feedbacks
{
	[ExecuteAlways]
	[AddComponentMenu(null)]
	[FeedbackPath("Audio/Sound")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks.MMTools", null)]
	[FeedbackHelp("WARNING: this is a very simple feedback, that will let you play a sound. Nothing wrong with it being simple of course, but if you want more features, you'll want to look at the MMSoundManager Sound feedback.\n\nThis feedback lets you play the specified AudioClip, either via event (you'll need something in your scene to catch a MMSfxEvent, for example a MMSoundManager), or cached (AudioSource gets created on init, and is then ready to be played), or on demand (instantiated on Play). For all these methods you can define a random volume between min/max boundaries (just set the same value in both fields if you don't want randomness), random pitch, and an optional AudioMixerGroup.")]
	public class MMF_Sound : MMF_Feedback
	{
		public enum PlayMethods
		{
			Event = 0,
			Cached = 1,
			OnDemand = 2,
			Pool = 3
		}

		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CTestPlaySound_003Ed__52 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncVoidMethodBuilder _003C_003Et__builder;

			public MMF_Sound _003C_003E4__this;

			private GameObject _003CtemporaryAudioHost_003E5__2;

			private TaskAwaiter _003C_003Eu__1;

			private void MoveNext()
			{
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		public static bool FeedbackTypeAuthorized;

		[MMFInspectorGroup("Sound", true, 14, true, false)]
		[Tooltip("the sound clip to play")]
		public AudioClip Sfx;

		[Tooltip("an array to pick a random sfx from")]
		public AudioClip[] RandomSfx;

		public MMF_Button TestPlayButton;

		public MMF_Button TestStopButton;

		[MMFInspectorGroup("Play Method", true, 27, false, false)]
		[Tooltip("the play method to use when playing the sound (event, cached or on demand)")]
		public PlayMethods PlayMethod;

		[Tooltip("the size of the pool when in Pool mode")]
		[MMFEnumCondition("PlayMethod", new int[] { 3 })]
		public int PoolSize;

		[Tooltip("in event mode, whether to use legacy events (MMSfxEvent) or the current events (MMSoundManagerSoundPlayEvent)")]
		[MMFEnumCondition("PlayMethod", new int[] { 0 })]
		public bool UseLegacyEventsMode;

		[Tooltip("if this is true, calling Stop on this feedback will also stop the sound from playing further")]
		public bool StopSoundOnFeedbackStop;

		[MMFInspectorGroup("Sound Properties", true, 28, false, false)]
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

		[Header("Mixer")]
		[Tooltip("the audiomixer to play the sound with (optional)")]
		public AudioMixerGroup SfxAudioMixerGroup;

		[Tooltip("the audiosource priority, to be specified if needed between 0 (highest) and 256")]
		public int Priority;

		[MMFInspectorGroup("Spatial Settings", true, 33, false, true)]
		[Tooltip("Pans a playing sound in a stereo way (left or right). This only applies to sounds that are Mono or Stereo.")]
		[Range(-1f, 1f)]
		public float PanStereo;

		[Tooltip("Sets how much this AudioSource is affected by 3D spatialisation calculations (attenuation, doppler etc). 0.0 makes the sound full 2D, 1.0 makes it full 3D.")]
		[Range(0f, 1f)]
		public float SpatialBlend;

		[MMFInspectorGroup("3D Sound Settings", true, 37, false, true)]
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
		[MMFCondition("UseCustomRolloffCurve", true)]
		public AnimationCurve CustomRolloffCurve;

		[Tooltip("whether or not to use a custom curve for spatial blend")]
		public bool UseSpatialBlendCurve;

		[Tooltip("the curve to use for custom spatial blend if UseSpatialBlendCurve is true")]
		[MMFCondition("UseSpatialBlendCurve", true)]
		public AnimationCurve SpatialBlendCurve;

		[Tooltip("whether or not to use a custom curve for reverb zone mix")]
		public bool UseReverbZoneMixCurve;

		[Tooltip("the curve to use for custom reverb zone mix if UseReverbZoneMixCurve is true")]
		[MMFCondition("UseReverbZoneMixCurve", true)]
		public AnimationCurve ReverbZoneMixCurve;

		[Tooltip("whether or not to use a custom curve for spread")]
		public bool UseSpreadCurve;

		[Tooltip("the curve to use for custom spread if UseSpreadCurve is true")]
		[MMFCondition("UseSpreadCurve", true)]
		public AnimationCurve SpreadCurve;

		protected AudioClip _randomClip;

		protected AudioSource _cachedAudioSource;

		protected AudioSource[] _pool;

		protected AudioSource _tempAudioSource;

		protected float _duration;

		protected AudioSource _editorAudioSource;

		protected AudioSource _audioSource;

		protected AudioClip _lastPlayedClip;

		public override bool HasRandomness => false;

		public override float FeedbackDuration => 0f;

		public override void InitializeCustomAttributes()
		{
		}

		protected override void CustomInitialization(MMF_Player owner)
		{
		}

		protected virtual AudioSource CreateAudioSource(GameObject owner, string audioSourceName)
		{
			return null;
		}

		protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		protected virtual float GetDuration()
		{
			return 0f;
		}

		protected virtual void PlaySound(AudioClip sfx, Vector3 position, float intensity)
		{
		}

		protected override void CustomStopFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		protected virtual void PlayAudioSource(AudioSource audioSource, AudioClip sfx, float volume, float pitch, int timeSamples, AudioMixerGroup audioMixerGroup = null, int priority = 128)
		{
		}

		protected virtual AudioSource GetAudioSourceFromPool()
		{
			return null;
		}

		[AsyncStateMachine(typeof(_003CTestPlaySound_003Ed__52))]
		protected virtual void TestPlaySound()
		{
		}

		protected virtual void TestStopSound()
		{
		}

		public override void AutomaticShakerSetup()
		{
		}
	}
}
