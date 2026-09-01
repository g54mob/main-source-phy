using MoreMountains.Tools;
using UnityEngine;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu("More Mountains/Feedbacks/Shakers/Audio/MMAudioFilterHighPassShaker")]
	[RequireComponent(typeof(AudioHighPassFilter))]
	public class MMAudioFilterHighPassShaker : MMShaker
	{
		[MMInspectorGroup("High Pass", true, 53, false)]
		[Tooltip("whether or not to add to the initial value")]
		public bool RelativeHighPass;

		[Tooltip("the curve used to animate the intensity value on")]
		public AnimationCurve ShakeHighPass;

		[Tooltip("the value to remap the curve's 0 to")]
		[Range(10f, 22000f)]
		public float RemapHighPassZero;

		[Tooltip("the value to remap the curve's 1 to")]
		[Range(10f, 22000f)]
		public float RemapHighPassOne;

		protected AudioHighPassFilter _targetAudioHighPassFilter;

		protected float _initialHighPass;

		protected float _originalShakeDuration;

		protected bool _originalRelativeHighPass;

		protected AnimationCurve _originalShakeHighPass;

		protected float _originalRemapHighPassZero;

		protected float _originalRemapHighPassOne;

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

		public virtual void OnMMAudioFilterHighPassShakeEvent(AnimationCurve highPassCurve, float duration, float remapMin, float remapMax, bool relativeHighPass = false, float feedbacksIntensity = 1f, MMChannelData channelData = null, bool resetShakerValuesAfterShake = true, bool resetTargetValuesAfterShake = true, bool forwardDirection = true, TimescaleModes timescaleMode = TimescaleModes.Scaled, bool stop = false, bool restore = false)
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
