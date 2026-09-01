using MoreMountains.Tools;
using UnityEngine;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu("More Mountains/Feedbacks/Shakers/Audio/MMAudioFilterEchoShaker")]
	[RequireComponent(typeof(AudioEchoFilter))]
	public class MMAudioFilterEchoShaker : MMShaker
	{
		[MMInspectorGroup("Echo", true, 52, false)]
		[Tooltip("whether or not to add to the initial value")]
		public bool RelativeEcho;

		[Tooltip("the curve used to animate the intensity value on")]
		public AnimationCurve ShakeEcho;

		[Tooltip("the value to remap the curve's 0 to")]
		[Range(0f, 1f)]
		public float RemapEchoZero;

		[Tooltip("the value to remap the curve's 1 to")]
		[Range(0f, 1f)]
		public float RemapEchoOne;

		protected AudioEchoFilter _targetAudioEchoFilter;

		protected float _initialEcho;

		protected float _originalShakeDuration;

		protected bool _originalRelativeEcho;

		protected AnimationCurve _originalShakeEcho;

		protected float _originalRemapEchoZero;

		protected float _originalRemapEchoOne;

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

		public virtual void OnMMAudioFilterEchoShakeEvent(AnimationCurve echoCurve, float duration, float remapMin, float remapMax, bool relativeEcho = false, float feedbacksIntensity = 1f, MMChannelData channelData = null, bool resetShakerValuesAfterShake = true, bool resetTargetValuesAfterShake = true, bool forwardDirection = true, TimescaleModes timescaleMode = TimescaleModes.Scaled, bool stop = false, bool restore = false)
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
