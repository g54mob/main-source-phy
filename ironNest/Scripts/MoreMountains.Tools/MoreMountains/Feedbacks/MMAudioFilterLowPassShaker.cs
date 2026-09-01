using MoreMountains.Tools;
using UnityEngine;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu("More Mountains/Feedbacks/Shakers/Audio/MMAudioFilterLowPassShaker")]
	[RequireComponent(typeof(AudioLowPassFilter))]
	public class MMAudioFilterLowPassShaker : MMShaker
	{
		[MMInspectorGroup("Low Pass", true, 54, false)]
		[Tooltip("whether or not to add to the initial value")]
		public bool RelativeLowPass;

		[Tooltip("the curve used to animate the intensity value on")]
		public AnimationCurve ShakeLowPass;

		[Tooltip("the value to remap the curve's 0 to")]
		[Range(10f, 22000f)]
		public float RemapLowPassZero;

		[Tooltip("the value to remap the curve's 1 to")]
		[Range(10f, 22000f)]
		public float RemapLowPassOne;

		protected AudioLowPassFilter _targetAudioLowPassFilter;

		protected float _initialLowPass;

		protected float _originalShakeDuration;

		protected bool _originalRelativeLowPass;

		protected AnimationCurve _originalShakeLowPass;

		protected float _originalRemapLowPassZero;

		protected float _originalRemapLowPassOne;

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

		public virtual void OnMMAudioFilterLowPassShakeEvent(AnimationCurve lowPassCurve, float duration, float remapMin, float remapMax, bool relativeLowPass = false, float feedbacksIntensity = 1f, MMChannelData channelData = null, bool resetShakerValuesAfterShake = true, bool resetTargetValuesAfterShake = true, bool forwardDirection = true, TimescaleModes timescaleMode = TimescaleModes.Scaled, bool stop = false, bool restore = false)
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
