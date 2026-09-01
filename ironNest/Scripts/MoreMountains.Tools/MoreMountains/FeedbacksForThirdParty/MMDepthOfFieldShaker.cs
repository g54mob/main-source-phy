using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace MoreMountains.FeedbacksForThirdParty
{
	[AddComponentMenu("More Mountains/Feedbacks/Shakers/PostProcessing/MMDepthOfFieldShaker")]
	[RequireComponent(typeof(PostProcessVolume))]
	public class MMDepthOfFieldShaker : MMShaker
	{
		public bool RelativeValues;

		[MMInspectorGroup("Focus Distance", true, 49, false)]
		[Tooltip("the curve used to animate the focus distance value on")]
		public AnimationCurve ShakeFocusDistance;

		[Tooltip("the value to remap the curve's 0 to")]
		public float RemapFocusDistanceZero;

		[Tooltip("the value to remap the curve's 1 to")]
		public float RemapFocusDistanceOne;

		[MMInspectorGroup("Aperture", true, 50, false)]
		[Tooltip("the curve used to animate the aperture value on")]
		public AnimationCurve ShakeAperture;

		[Tooltip("the value to remap the curve's 0 to")]
		[Range(0.1f, 32f)]
		public float RemapApertureZero;

		[Tooltip("the value to remap the curve's 1 to")]
		[Range(0.1f, 32f)]
		public float RemapApertureOne;

		[MMInspectorGroup("Focal Length", true, 51, false)]
		[Tooltip("the curve used to animate the focal length value on")]
		public AnimationCurve ShakeFocalLength;

		[Tooltip("the value to remap the curve's 0 to")]
		[Range(0f, 300f)]
		public float RemapFocalLengthZero;

		[Tooltip("the value to remap the curve's 1 to")]
		[Range(0f, 300f)]
		public float RemapFocalLengthOne;

		protected PostProcessVolume _volume;

		protected DepthOfField _depthOfField;

		protected float _initialFocusDistance;

		protected float _initialAperture;

		protected float _initialFocalLength;

		protected float _originalShakeDuration;

		protected bool _originalRelativeValues;

		protected AnimationCurve _originalShakeFocusDistance;

		protected float _originalRemapFocusDistanceZero;

		protected float _originalRemapFocusDistanceOne;

		protected AnimationCurve _originalShakeAperture;

		protected float _originalRemapApertureZero;

		protected float _originalRemapApertureOne;

		protected AnimationCurve _originalShakeFocalLength;

		protected float _originalRemapFocalLengthZero;

		protected float _originalRemapFocalLengthOne;

		protected override void Initialization()
		{
		}

		protected override void Shake()
		{
		}

		protected virtual void Reset()
		{
		}

		protected override void GrabInitialValues()
		{
		}

		public virtual void OnDepthOfFieldShakeEvent(AnimationCurve focusDistance, float duration, float remapFocusDistanceMin, float remapFocusDistanceMax, AnimationCurve aperture, float remapApertureMin, float remapApertureMax, AnimationCurve focalLength, float remapFocalLengthMin, float remapFocalLengthMax, bool relativeValues = false, float feedbacksIntensity = 1f, MMChannelData channelData = null, bool resetShakerValuesAfterShake = true, bool resetTargetValuesAfterShake = true, bool forwardDirection = true, TimescaleModes timescaleMode = TimescaleModes.Scaled, bool stop = false, bool restore = false)
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
