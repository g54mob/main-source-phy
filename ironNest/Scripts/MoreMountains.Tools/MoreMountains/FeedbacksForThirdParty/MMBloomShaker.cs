using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace MoreMountains.FeedbacksForThirdParty
{
	[AddComponentMenu("More Mountains/Feedbacks/Shakers/PostProcessing/MMBloomShaker")]
	[RequireComponent(typeof(PostProcessVolume))]
	public class MMBloomShaker : MMShaker
	{
		public bool RelativeValues;

		[MMInspectorGroup("Bloom Intensity", true, 45, false)]
		[Tooltip("the curve used to animate the intensity value on")]
		public AnimationCurve ShakeIntensity;

		[Tooltip("the value to remap the curve's 0 to")]
		public float RemapIntensityZero;

		[Tooltip("the value to remap the curve's 1 to")]
		public float RemapIntensityOne;

		[MMInspectorGroup("Bloom Threshold", true, 46, false)]
		[Tooltip("the curve used to animate the threshold value on")]
		public AnimationCurve ShakeThreshold;

		[Tooltip("the value to remap the curve's 0 to")]
		public float RemapThresholdZero;

		[Tooltip("the value to remap the curve's 1 to")]
		public float RemapThresholdOne;

		protected PostProcessVolume _volume;

		protected Bloom _bloom;

		protected float _initialIntensity;

		protected float _initialThreshold;

		protected float _originalShakeDuration;

		protected bool _originalRelativeIntensity;

		protected AnimationCurve _originalShakeIntensity;

		protected float _originalRemapIntensityZero;

		protected float _originalRemapIntensityOne;

		protected AnimationCurve _originalShakeThreshold;

		protected float _originalRemapThresholdZero;

		protected float _originalRemapThresholdOne;

		protected override void Initialization()
		{
		}

		protected override void Shake()
		{
		}

		protected override void GrabInitialValues()
		{
		}

		public virtual void OnBloomShakeEvent(AnimationCurve intensity, float duration, float remapMin, float remapMax, AnimationCurve threshold, float remapThresholdMin, float remapThresholdMax, bool relativeIntensity = false, float feedbacksIntensity = 1f, MMChannelData channelData = null, bool resetShakerValuesAfterShake = true, bool resetTargetValuesAfterShake = true, bool forwardDirection = true, TimescaleModes timescaleMode = TimescaleModes.Scaled, bool stop = false, bool restore = false)
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
