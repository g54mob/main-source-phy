using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu("More Mountains/Feedbacks/Shakers/Lights/MMLight2DShaker_URP")]
	[RequireComponent(typeof(Light2D))]
	public class MMLight2DShaker_URP : MMShaker
	{
		[MMInspectorGroup("Light", true, 37, false)]
		[Tooltip("the light to affect when playing the feedback")]
		public Light2D BoundLight;

		[Tooltip("whether or not that light should be turned off on start")]
		public bool StartsOff;

		[Tooltip("whether or not the values should be relative or not")]
		public bool RelativeValues;

		[MMInspectorGroup("Color", true, 41, false)]
		[Tooltip("whether or not this shaker should modify color")]
		public bool ModifyColor;

		[Tooltip("the colors to apply to the light over time")]
		public Gradient ColorOverTime;

		[MMInspectorGroup("Intensity", true, 40, false)]
		[Tooltip("the intensity to apply to the light over time")]
		public AnimationCurve IntensityCurve;

		[Tooltip("the value to remap the intensity curve's 0 to")]
		public float RemapIntensityZero;

		[Tooltip("the value to remap the intensity curve's 1 to")]
		public float RemapIntensityOne;

		[MMInspectorGroup("Range", true, 39, false)]
		[Tooltip("the range to apply to the light over time")]
		public AnimationCurve FalloffCurve;

		[Tooltip("the value to remap the range curve's 0 to")]
		public float FalloffRangeZero;

		[Tooltip("the value to remap the range curve's 0 to")]
		public float RemapFalloffOne;

		[MMInspectorGroup("Shadow Strength", true, 38, false)]
		[Tooltip("the range to apply to the light over time")]
		public AnimationCurve ShadowStrengthCurve;

		[Tooltip("the value to remap the shadow strength's curve's 0 to")]
		public float RemapShadowStrengthZero;

		[Tooltip("the value to remap the shadow strength's curve's 1 to")]
		public float RemapShadowStrengthOne;

		protected Color _initialColor;

		protected float _initialRange;

		protected float _initialIntensity;

		protected float _initialShadowStrength;

		protected bool _originalRelativeValues;

		protected bool _originalModifyColor;

		protected float _originalShakeDuration;

		protected Gradient _originalColorOverTime;

		protected AnimationCurve _originalIntensityCurve;

		protected float _originalRemapIntensityZero;

		protected float _originalRemapIntensityOne;

		protected AnimationCurve _originalRangeCurve;

		protected float _originalRemapRangeZero;

		protected float _originalRemapRangeOne;

		protected AnimationCurve _originalShadowStrengthCurve;

		protected float _originalRemapShadowStrengthZero;

		protected float _originalRemapShadowStrengthOne;

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

		public virtual void OnMMLight2DShakeEvent(float shakeDuration, bool relativeValues, bool modifyColor, Gradient colorOverTime, AnimationCurve intensityCurve, float remapIntensityZero, float remapIntensityOne, AnimationCurve rangeCurve, float remapRangeZero, float remapRangeOne, AnimationCurve shadowStrengthCurve, float remapShadowStrengthZero, float remapShadowStrengthOne, float feedbacksIntensity = 1f, MMChannelData channelData = null, bool resetShakerValuesAfterShake = true, bool resetTargetValuesAfterShake = true, bool useRange = false, float eventRange = 0f, Vector3 eventOriginPosition = default(Vector3))
		{
		}
	}
}
