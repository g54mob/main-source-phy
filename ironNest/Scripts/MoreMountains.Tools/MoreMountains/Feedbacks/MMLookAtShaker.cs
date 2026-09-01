using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using MoreMountains.Tools;
using UnityEngine;

namespace MoreMountains.Feedbacks
{
	public class MMLookAtShaker : MMShaker
	{
		[StructLayout((LayoutKind)0, Size = 1)]
		public struct MMLookAtShakeEvent
		{
			public delegate void Delegate(float duration, bool lockXAxis, bool lockYAxis, bool lockZAxis, MMF_LookAt.UpwardVectors upwardVector, MMF_LookAt.LookAtTargetModes lookAtTargetMode, Transform lookAtTarget, Vector3 lookAtTargetWorldPosition, Vector3 lookAtDirection, Transform transformToRotate, MMTweenType lookAtTween, bool useRange = false, float rangeDistance = 0f, bool useRangeFalloff = false, AnimationCurve rangeFalloff = null, Vector2 remapRangeFalloff = default(Vector2), Vector3 rangePosition = default(Vector3), float feedbacksIntensity = 1f, MMChannelData channelData = null, bool resetShakerValuesAfterShake = true, bool resetTargetValuesAfterShake = true, bool forwardDirection = true, TimescaleModes timescaleMode = TimescaleModes.Scaled, bool stop = false);

			private static event Delegate OnEvent
			{
				[CompilerGenerated]
				add
				{
				}
				[CompilerGenerated]
				remove
				{
				}
			}

			[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
			private static void RuntimeInitialization()
			{
			}

			public static void Register(Delegate callback)
			{
			}

			public static void Unregister(Delegate callback)
			{
			}

			public static void Trigger(float duration, bool lockXAxis, bool lockYAxis, bool lockZAxis, MMF_LookAt.UpwardVectors upwardVector, MMF_LookAt.LookAtTargetModes lookAtTargetMode, Transform lookAtTarget, Vector3 lookAtTargetWorldPosition, Vector3 lookAtDirection, Transform transformToRotate, MMTweenType lookAtTween, bool useRange = false, float rangeDistance = 0f, bool useRangeFalloff = false, AnimationCurve rangeFalloff = null, Vector2 remapRangeFalloff = default(Vector2), Vector3 rangePosition = default(Vector3), float feedbacksIntensity = 1f, MMChannelData channelData = null, bool resetShakerValuesAfterShake = true, bool resetTargetValuesAfterShake = true, bool forwardDirection = true, TimescaleModes timescaleMode = TimescaleModes.Scaled, bool stop = false)
			{
			}
		}

		[MMInspectorGroup("Look at settings", true, 37, false)]
		[Tooltip("the duration of this shake, in seconds")]
		public float Duration;

		[Tooltip("the curve over which to animate the look at transition")]
		public MMTweenType LookAtTween;

		[Tooltip("whether or not to lock rotation on the x axis")]
		public bool LockXAxis;

		[Tooltip("whether or not to lock rotation on the y axis")]
		public bool LockYAxis;

		[Tooltip("whether or not to lock rotation on the z axis")]
		public bool LockZAxis;

		[MMInspectorGroup("What we want to rotate", true, 37, false)]
		[Tooltip("in Direct mode, the transform to rotate to have it look at our target - if left empty, will be the transform this shaker is on")]
		public Transform TransformToRotate;

		public MMF_LookAt.UpwardVectors UpwardVector;

		[MMInspectorGroup("What we want to look at", true, 37, false)]
		[Tooltip("the different target modes : either a specific transform to look at, the coordinates of a world position, or a direction vector")]
		public MMF_LookAt.LookAtTargetModes LookAtTargetMode;

		[Tooltip("the transform we want to look at")]
		[MMFEnumCondition("LookAtTargetMode", new int[] { 0 })]
		public Transform LookAtTarget;

		[Tooltip("the coordinates of a point the world that we want to look at")]
		[MMFEnumCondition("LookAtTargetMode", new int[] { 1 })]
		public Vector3 LookAtTargetWorldPosition;

		[Tooltip("a direction (from our rotating object) that we want to look at")]
		[MMFEnumCondition("LookAtTargetMode", new int[] { 2 })]
		public Vector3 LookAtDirection;

		[MMInspectorGroup("Test", true, 46, false)]
		[MMInspectorButton("StartShaking")]
		public bool StartShakingButton;

		protected Quaternion _newRotation;

		protected Vector3 _lookAtPosition;

		protected Vector3 _upwards;

		protected Vector3 _direction;

		protected Quaternion _initialRotation;

		protected float _originalDuration;

		protected MMTweenType _originalLookAtTween;

		protected bool _originalLockXAxis;

		protected bool _originalLockYAxis;

		protected bool _originalLockZAxis;

		protected MMF_LookAt.UpwardVectors _originalUpwardVector;

		protected MMF_LookAt.LookAtTargetModes _originalLookAtTargetMode;

		protected Transform _originalLookAtTarget;

		protected Vector3 _originalLookAtTargetWorldPosition;

		protected Vector3 _originalLookAtDirection;

		protected override void Initialization()
		{
		}

		protected virtual void Reset()
		{
		}

		protected override void Shake()
		{
		}

		protected override void ShakeComplete()
		{
		}

		protected virtual void ApplyRotation(float journey)
		{
		}

		public virtual void OnMMLookAtShakeEvent(float duration, bool lockXAxis, bool lockYAxis, bool lockZAxis, MMF_LookAt.UpwardVectors upwardVector, MMF_LookAt.LookAtTargetModes lookAtTargetMode, Transform lookAtTarget, Vector3 lookAtTargetWorldPosition, Vector3 lookAtDirection, Transform transformToRotate, MMTweenType lookAtTween, bool useRange = false, float rangeDistance = 0f, bool useRangeFalloff = false, AnimationCurve rangeFalloff = null, Vector2 remapRangeFalloff = default(Vector2), Vector3 rangePosition = default(Vector3), float feedbacksIntensity = 1f, MMChannelData channelData = null, bool resetShakerValuesAfterShake = true, bool resetTargetValuesAfterShake = true, bool forwardDirection = true, TimescaleModes timescaleMode = TimescaleModes.Scaled, bool stop = false)
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
