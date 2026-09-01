using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace MoreMountains.FeedbacksForThirdParty
{
	[AddComponentMenu("More Mountains/Feedbacks/Shakers/PostProcessing/MMLensDistortionShaker")]
	[RequireComponent(typeof(PostProcessVolume))]
	public class MMLensDistortionShaker : MMShaker
	{
		[MMInspectorGroup("Lens Distortion Intensity", true, 51, false)]
		[Tooltip("whether or not to add to the initial value")]
		public bool RelativeIntensity;

		[Tooltip("the curve used to animate the intensity value on")]
		public AnimationCurve ShakeIntensity;

		[Tooltip("the value to remap the curve's 0 to")]
		[Range(-100f, 100f)]
		public float RemapIntensityZero;

		[Tooltip("the value to remap the curve's 1 to")]
		[Range(-100f, 100f)]
		public float RemapIntensityOne;

		protected PostProcessVolume _volume;

		protected LensDistortion _lensDistortion;

		protected float _initialIntensity;

		protected float _originalShakeDuration;

		protected AnimationCurve _originalShakeIntensity;

		protected float _originalRemapIntensityZero;

		protected float _originalRemapIntensityOne;

		protected bool _originalRelativeIntensity;

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

		public virtual void OnMMLensDistortionShakeEvent(AnimationCurve intensity, float duration, float remapMin, float remapMax, bool relativeIntensity = false, float feedbacksIntensity = 1f, MMChannelData channelData = null, bool resetShakerValuesAfterShake = true, bool resetTargetValuesAfterShake = true, bool forwardDirection = true, TimescaleModes timescaleMode = TimescaleModes.Scaled, bool stop = false, bool restore = false)
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
