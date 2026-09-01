using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace MoreMountains.FeedbacksForThirdParty
{
	[RequireComponent(typeof(Volume))]
	[AddComponentMenu("More Mountains/Feedbacks/Shakers/PostProcessing/MMWhiteBalanceShaker_URP")]
	public class MMWhiteBalanceShaker_URP : MMShaker
	{
		[Tooltip("whether or not to add to the initial value")]
		public bool RelativeValues;

		[MMInspectorGroup("Temperature", true, 55, false)]
		[Tooltip("the curve used to animate the temperature value on")]
		public AnimationCurve ShakeTemperature;

		[Tooltip("the value to remap the curve's 0 to")]
		[Range(-100f, 100f)]
		public float RemapTemperatureZero;

		[Tooltip("the value to remap the curve's 1 to")]
		[Range(-100f, 100f)]
		public float RemapTemperatureOne;

		[MMInspectorGroup("Tint", true, 56, false)]
		[Tooltip("the curve used to animate the tint value on")]
		public AnimationCurve ShakeTint;

		[Tooltip("the value to remap the curve's 0 to")]
		[Range(-100f, 100f)]
		public float RemapTintZero;

		[Tooltip("the value to remap the curve's 1 to")]
		[Range(-100f, 100f)]
		public float RemapTintOne;

		protected Volume _volume;

		protected WhiteBalance _whiteBalance;

		protected float _initialTemperature;

		protected float _initialTint;

		protected float _originalShakeDuration;

		protected bool _originalRelativeValues;

		protected AnimationCurve _originalShakeTemperature;

		protected float _originalRemapTemperatureZero;

		protected float _originalRemapTemperatureOne;

		protected AnimationCurve _originalShakeTint;

		protected float _originalRemapTintZero;

		protected float _originalRemapTintOne;

		protected override void Initialization()
		{
		}

		protected override void Shake()
		{
		}

		protected override void GrabInitialValues()
		{
		}

		public virtual void OnWhiteBalanceShakeEvent(AnimationCurve temperature, float duration, float remapTemperatureMin, float remapTemperatureMax, AnimationCurve tint, float remapTintMin, float remapTintMax, bool relativeValues = false, float attenuation = 1f, MMChannelData channelData = null, bool resetShakerValuesAfterShake = true, bool resetTargetValuesAfterShake = true, bool forwardDirection = true, TimescaleModes timescaleMode = TimescaleModes.Scaled, bool stop = false, bool restore = false)
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
