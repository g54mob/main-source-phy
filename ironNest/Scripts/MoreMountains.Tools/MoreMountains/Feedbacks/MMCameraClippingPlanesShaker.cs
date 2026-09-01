using MoreMountains.Tools;
using UnityEngine;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu("More Mountains/Feedbacks/Shakers/Camera/MMCameraClippingPlanesShaker")]
	[RequireComponent(typeof(Camera))]
	public class MMCameraClippingPlanesShaker : MMShaker
	{
		[MMInspectorGroup("Clipping Planes", true, 31, false)]
		[Tooltip("whether or not to add to the initial value")]
		public bool RelativeClippingPlanes;

		[MMInspectorGroup("Near Plane", true, 32, false)]
		[Tooltip("the curve used to animate the intensity value on")]
		public AnimationCurve ShakeNear;

		[Tooltip("the value to remap the curve's 0 to")]
		public float RemapNearZero;

		[Tooltip("the value to remap the curve's 1 to")]
		public float RemapNearOne;

		[MMInspectorGroup("Far Plane", true, 33, false)]
		[Tooltip("the curve used to animate the intensity value on")]
		public AnimationCurve ShakeFar;

		[Tooltip("the value to remap the curve's 0 to")]
		public float RemapFarZero;

		[Tooltip("the value to remap the curve's 1 to")]
		public float RemapFarOne;

		protected Camera _targetCamera;

		protected float _initialNear;

		protected float _initialFar;

		protected float _originalShakeDuration;

		protected bool _originalRelativeClippingPlanes;

		protected AnimationCurve _originalShakeNear;

		protected float _originalRemapNearZero;

		protected float _originalRemapNearOne;

		protected AnimationCurve _originalShakeFar;

		protected float _originalRemapFarZero;

		protected float _originalRemapFarOne;

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

		public virtual void OnMMCameraClippingPlanesShakeEvent(AnimationCurve animNearCurve, float duration, float remapNearMin, float remapNearMax, AnimationCurve animFarCurve, float remapFarMin, float remapFarMax, bool relativeValues = false, float feedbacksIntensity = 1f, MMChannelData channelData = null, bool resetShakerValuesAfterShake = true, bool resetTargetValuesAfterShake = true, bool forwardDirection = true, TimescaleModes timescaleMode = TimescaleModes.Scaled, bool stop = false, bool restore = false)
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
