using MoreMountains.Tools;
using UnityEngine;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu("More Mountains/Feedbacks/Shakers/Audio/MMAudioSourceStereoPanShaker")]
	[RequireComponent(typeof(AudioSource))]
	public class MMAudioSourceStereoPanShaker : MMShaker
	{
		[MMInspectorGroup("Stereo Pan", true, 57, false)]
		[Tooltip("whether or not to add to the initial value")]
		public bool RelativeStereoPan;

		[Tooltip("the curve used to animate the intensity value on")]
		public AnimationCurve ShakeStereoPan;

		[Tooltip("the value to remap the curve's 0 to")]
		[Range(-1f, 1f)]
		public float RemapStereoPanZero;

		[Tooltip("the value to remap the curve's 1 to")]
		[Range(-1f, 1f)]
		public float RemapStereoPanOne;

		protected AudioSource _targetAudioSource;

		protected float _initialStereoPan;

		protected float _originalShakeDuration;

		protected bool _originalRelativeValues;

		protected AnimationCurve _originalShakeStereoPan;

		protected float _originalRemapStereoPanZero;

		protected float _originalRemapStereoPanOne;

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

		public virtual void OnMMAudioSourceStereoPanShakeEvent(AnimationCurve stereoPanCurve, float duration, float remapMin, float remapMax, bool relativeStereoPan = false, float feedbacksIntensity = 1f, MMChannelData channelData = null, bool resetShakerValuesAfterShake = true, bool resetTargetValuesAfterShake = true, bool forwardDirection = true, TimescaleModes timescaleMode = TimescaleModes.Scaled, bool stop = false, bool restore = false)
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
