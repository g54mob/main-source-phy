using MoreMountains.Tools;
using UnityEngine;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu("More Mountains/Feedbacks/Shakers/Camera/MMCameraFieldOfViewShaker")]
	[RequireComponent(typeof(Camera))]
	public class MMCameraFieldOfViewShaker : MMShaker
	{
		[MMInspectorGroup("Field of View", true, 34, false)]
		[Tooltip("whether or not to add to the initial value")]
		public bool RelativeFieldOfView;

		[Tooltip("the curve used to animate the intensity value on")]
		public AnimationCurve ShakeFieldOfView;

		[Tooltip("the value to remap the curve's 0 to")]
		[Range(0f, 179f)]
		public float RemapFieldOfViewZero;

		[Tooltip("the value to remap the curve's 1 to")]
		[Range(0f, 179f)]
		public float RemapFieldOfViewOne;

		protected Camera _targetCamera;

		protected float _initialFieldOfView;

		protected float _originalShakeDuration;

		protected bool _originalRelativeFieldOfView;

		protected AnimationCurve _originalShakeFieldOfView;

		protected float _originalRemapFieldOfViewZero;

		protected float _originalRemapFieldOfViewOne;

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

		public virtual void OnMMCameraFieldOfViewShakeEvent(AnimationCurve distortionCurve, float duration, float remapMin, float remapMax, bool relativeDistortion = false, float feedbacksIntensity = 1f, MMChannelData channelData = null, bool resetShakerValuesAfterShake = true, bool resetTargetValuesAfterShake = true, bool forwardDirection = true, TimescaleModes timescaleMode = TimescaleModes.Scaled, bool stop = false, bool restore = false)
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
