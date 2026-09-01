using MoreMountains.Tools;
using UnityEngine;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu("More Mountains/Feedbacks/Shakers/Audio/MMAudioSourceVolumeShaker")]
	[RequireComponent(typeof(AudioSource))]
	public class MMAudioSourceVolumeShaker : MMShaker
	{
		[MMInspectorGroup("Volume", true, 59, false)]
		[Tooltip("whether or not to add to the initial value")]
		public bool RelativeVolume;

		[Tooltip("the curve used to animate the intensity value on")]
		public AnimationCurve ShakeVolume;

		[Tooltip("the value to remap the curve's 0 to")]
		[Range(-1f, 1f)]
		public float RemapVolumeZero;

		[Tooltip("the value to remap the curve's 1 to")]
		[Range(-1f, 1f)]
		public float RemapVolumeOne;

		protected AudioSource _targetAudioSource;

		protected float _initialVolume;

		protected float _originalShakeDuration;

		protected bool _originalRelativeValues;

		protected AnimationCurve _originalShakeVolume;

		protected float _originalRemapVolumeZero;

		protected float _originalRemapVolumeOne;

		protected override void Initialization()
		{
		}

		protected virtual void Reset()
		{
		}

		protected override void Shake()
		{
		}

		protected override void GrabInitialValues()
		{
		}

		public virtual void OnMMAudioSourceVolumeShakeEvent(AnimationCurve volumeCurve, float duration, float remapMin, float remapMax, bool relativeVolume = false, float feedbacksIntensity = 1f, MMChannelData channelData = null, bool resetShakerValuesAfterShake = true, bool resetTargetValuesAfterShake = true, bool forwardDirection = true, TimescaleModes timescaleMode = TimescaleModes.Scaled, bool stop = false, bool restore = false)
		{
		}

		protected override void ResetTargetValues()
		{
		}

		protected override void ResetShakerValues()
		{
		}

		public override void StartListening()
		{
		}

		public override void StopListening()
		{
		}
	}
}
