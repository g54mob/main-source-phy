using MoreMountains.Tools;
using UnityEngine;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu("More Mountains/Feedbacks/Shakers/Audio/MMAudioFilterReverbShaker")]
	[RequireComponent(typeof(AudioReverbFilter))]
	public class MMAudioFilterReverbShaker : MMShaker
	{
		[MMInspectorGroup("Reverb", true, 55, false)]
		[Tooltip("whether or not to add to the initial value")]
		public bool RelativeReverb;

		[Tooltip("the curve used to animate the intensity value on")]
		public AnimationCurve ShakeReverb;

		[Tooltip("the value to remap the curve's 0 to")]
		[Range(-10000f, 2000f)]
		public float RemapReverbZero;

		[Tooltip("the value to remap the curve's 1 to")]
		[Range(-10000f, 2000f)]
		public float RemapReverbOne;

		protected AudioReverbFilter _targetAudioReverbFilter;

		protected float _initialReverb;

		protected float _originalShakeDuration;

		protected bool _originalRelativeReverb;

		protected AnimationCurve _originalShakeReverb;

		protected float _originalRemapReverbZero;

		protected float _originalRemapReverbOne;

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

		public virtual void OnMMAudioFilterReverbShakeEvent(AnimationCurve reverbCurve, float duration, float remapMin, float remapMax, bool relativeReverb = false, float feedbacksIntensity = 1f, MMChannelData channelData = null, bool resetShakerValuesAfterShake = true, bool resetTargetValuesAfterShake = true, bool forwardDirection = true, TimescaleModes timescaleMode = TimescaleModes.Scaled, bool stop = false, bool restore = false)
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
