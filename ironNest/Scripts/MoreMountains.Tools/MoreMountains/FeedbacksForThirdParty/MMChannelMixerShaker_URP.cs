using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace MoreMountains.FeedbacksForThirdParty
{
	[RequireComponent(typeof(Volume))]
	[AddComponentMenu("More Mountains/Feedbacks/Shakers/PostProcessing/MMChannelMixerShaker_URP")]
	public class MMChannelMixerShaker_URP : MMShaker
	{
		public bool RelativeValues;

		[MMInspectorGroup("Red", true, 43, false)]
		[Tooltip("the curve used to animate the red value on")]
		public AnimationCurve ShakeRed;

		[Tooltip("the value to remap the curve's 0 to")]
		[Range(-200f, 200f)]
		public float RemapRedZero;

		[Tooltip("the value to remap the curve's 1 to")]
		[Range(-200f, 200f)]
		public float RemapRedOne;

		[MMInspectorGroup("Green", true, 44, false)]
		[Tooltip("the curve used to animate the green value on")]
		public AnimationCurve ShakeGreen;

		[Tooltip("the value to remap the curve's 0 to")]
		[Range(-200f, 200f)]
		public float RemapGreenZero;

		[Tooltip("the value to remap the curve's 1 to")]
		[Range(-200f, 200f)]
		public float RemapGreenOne;

		[MMInspectorGroup("Blue", true, 45, false)]
		[Tooltip("the curve used to animate the blue value on")]
		public AnimationCurve ShakeBlue;

		[Tooltip("the value to remap the curve's 0 to")]
		[Range(-200f, 200f)]
		public float RemapBlueZero;

		[Tooltip("the value to remap the curve's 1 to")]
		[Range(-200f, 200f)]
		public float RemapBlueOne;

		protected Volume _volume;

		protected ChannelMixer _channelMixer;

		protected float _initialRed;

		protected float _initialGreen;

		protected float _initialBlue;

		protected float _initialContrast;

		protected Color _initialColorFilterColor;

		protected float _originalShakeDuration;

		protected bool _originalRelativeValues;

		protected AnimationCurve _originalShakeRed;

		protected float _originalRemapRedZero;

		protected float _originalRemapRedOne;

		protected AnimationCurve _originalShakeGreen;

		protected float _originalRemapGreenZero;

		protected float _originalRemapGreenOne;

		protected AnimationCurve _originalShakeBlue;

		protected float _originalRemapBlueZero;

		protected float _originalRemapBlueOne;

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

		public virtual void OnMMChannelMixerShakeEvent(AnimationCurve shakeRed, float remapRedZero, float remapRedOne, AnimationCurve shakeGreen, float remapGreenZero, float remapGreenOne, AnimationCurve shakeBlue, float remapBlueZero, float remapBlueOne, float duration, bool relativeValues = false, float attenuation = 1f, MMChannelData channelData = null, bool resetShakerValuesAfterShake = true, bool resetTargetValuesAfterShake = true, bool forwardDirection = true, TimescaleModes timescaleMode = TimescaleModes.Scaled, bool stop = false, bool restore = false)
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
