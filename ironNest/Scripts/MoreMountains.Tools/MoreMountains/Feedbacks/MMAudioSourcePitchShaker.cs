using MoreMountains.Tools;
using UnityEngine;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu("More Mountains/Feedbacks/Shakers/Audio/MMAudioSourcePitchShaker")]
	[RequireComponent(typeof(AudioSource))]
	public class MMAudioSourcePitchShaker : MMShaker
	{
		[MMInspectorGroup("Pitch", true, 57, false)]
		[Tooltip("whether or not to add to the initial value")]
		public bool RelativePitch;

		[Tooltip("the curve used to animate the intensity value on")]
		public AnimationCurve ShakePitch;

		[Tooltip("the value to remap the curve's 0 to")]
		[Range(-3f, 3f)]
		public float RemapPitchZero;

		[Tooltip("the value to remap the curve's 1 to")]
		[Range(-3f, 3f)]
		public float RemapPitchOne;

		protected AudioSource _targetAudioSource;

		protected float _initialPitch;

		protected float _originalShakeDuration;

		protected bool _originalRelativePitch;

		protected AnimationCurve _originalShakePitch;

		protected float _originalRemapPitchZero;

		protected float _originalRemapPitchOne;

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

		public virtual void OnMMAudioSourcePitchShakeEvent(AnimationCurve pitchCurve, float duration, float remapMin, float remapMax, bool relativePitch = false, float feedbacksIntensity = 1f, MMChannelData channelData = null, bool resetShakerValuesAfterShake = true, bool resetTargetValuesAfterShake = true, bool forwardDirection = true, TimescaleModes timescaleMode = TimescaleModes.Scaled, bool stop = false, bool restore = false)
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
