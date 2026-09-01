using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using Unity.Cinemachine;
using UnityEngine;

namespace MoreMountains.FeedbacksForThirdParty
{
	[AddComponentMenu("More Mountains/Feedbacks/Shakers/Cinemachine/MMCinemachineFieldOfViewShaker")]
	[RequireComponent(typeof(CinemachineCamera))]
	public class MMCinemachineFieldOfViewShaker : MMShaker
	{
		[MMInspectorGroup("Field of view", true, 41, false)]
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

		protected CinemachineCamera _targetCamera;

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

		protected virtual void SetFieldOfView(float newFieldOfView)
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
