using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using Unity.Cinemachine;
using UnityEngine;

namespace MoreMountains.FeedbacksForThirdParty
{
	[AddComponentMenu("More Mountains/Feedbacks/Shakers/Cinemachine/MMCinemachineOrthographicSizeShaker")]
	[RequireComponent(typeof(CinemachineCamera))]
	public class MMCinemachineOrthographicSizeShaker : MMShaker
	{
		[MMInspectorGroup("Orthographic Size", true, 43, false)]
		[Tooltip("whether or not to add to the initial value")]
		public bool RelativeOrthographicSize;

		[Tooltip("the curve used to animate the intensity value on")]
		public AnimationCurve ShakeOrthographicSize;

		[Tooltip("the value to remap the curve's 0 to")]
		public float RemapOrthographicSizeZero;

		[Tooltip("the value to remap the curve's 1 to")]
		public float RemapOrthographicSizeOne;

		protected CinemachineCamera _targetCamera;

		protected float _initialOrthographicSize;

		protected float _originalShakeDuration;

		protected bool _originalRelativeOrthographicSize;

		protected AnimationCurve _originalShakeOrthographicSize;

		protected float _originalRemapOrthographicSizeZero;

		protected float _originalRemapOrthographicSizeOne;

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

		public virtual void OnMMCameraOrthographicSizeShakeEvent(AnimationCurve distortionCurve, float duration, float remapMin, float remapMax, bool relativeDistortion = false, float feedbacksIntensity = 1f, MMChannelData channelData = null, bool resetShakerValuesAfterShake = true, bool resetTargetValuesAfterShake = true, bool forwardDirection = true, TimescaleModes timescaleMode = TimescaleModes.Scaled, bool stop = false, bool restore = false)
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
