using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace MoreMountains.FeedbacksForThirdParty
{
	[AddComponentMenu("More Mountains/Feedbacks/Shakers/PostProcessing/MMVignetteShaker")]
	[RequireComponent(typeof(PostProcessVolume))]
	public class MMVignetteShaker : MMShaker
	{
		[MMInspectorGroup("Vignette Intensity", true, 53, false)]
		[Tooltip("whether or not to add to the initial value")]
		public bool RelativeIntensity;

		[Tooltip("the curve used to animate the intensity value on")]
		public AnimationCurve ShakeIntensity;

		[Tooltip("the value to remap the curve's 0 to")]
		[Range(0f, 1f)]
		public float RemapIntensityZero;

		[Tooltip("the value to remap the curve's 1 to")]
		[Range(0f, 1f)]
		public float RemapIntensityOne;

		[MMInspectorGroup("Vignette Color", true, 51, false)]
		[Tooltip("whether or not to also animate the vignette's color")]
		public bool InterpolateColor;

		[Tooltip("the curve to animate the color on")]
		public AnimationCurve ColorCurve;

		[Tooltip("the value to remap the curve's 0 to")]
		[Range(0f, 1f)]
		public float RemapColorZero;

		[Tooltip("the value to remap the curve's 1 to")]
		[Range(0f, 1f)]
		public float RemapColorOne;

		[Tooltip("the color to lerp towards")]
		public Color TargetColor;

		protected PostProcessVolume _volume;

		protected Vignette _vignette;

		protected float _initialIntensity;

		protected float _originalShakeDuration;

		protected AnimationCurve _originalShakeIntensity;

		protected float _originalRemapIntensityZero;

		protected float _originalRemapIntensityOne;

		protected bool _originalRelativeIntensity;

		protected bool _originalInterpolateColor;

		protected AnimationCurve _originalColorCurve;

		protected float _originalRemapColorZero;

		protected float _originalRemapColorOne;

		protected Color _originalTargetColor;

		protected Color _initialColor;

		protected override void Initialization()
		{
		}

		public virtual void SetVignette(float newValue)
		{
		}

		protected override void Shake()
		{
		}

		protected override void GrabInitialValues()
		{
		}

		public virtual void OnVignetteShakeEvent(AnimationCurve intensity, float duration, float remapMin, float remapMax, bool relativeIntensity = false, float feedbacksIntensity = 1f, MMChannelData channelData = null, bool resetShakerValuesAfterShake = true, bool resetTargetValuesAfterShake = true, bool forwardDirection = true, TimescaleModes timescaleMode = TimescaleModes.Scaled, bool stop = false, bool restore = false, bool interpolateColor = false, AnimationCurve colorCurve = null, float remapColorZero = 0f, float remapColorOne = 1f, Color targetColor = default(Color))
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
