using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu(null)]
	[FeedbackHelp("This feedback will animate the target's position (not its rotation), on an arc around the specified rotation center, for the specified duration (in seconds).")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks", null)]
	[FeedbackPath("Transform/Rotate Position Around")]
	public class MMF_RotatePositionAround : MMF_Feedback
	{
		public enum TimeScales
		{
			Scaled = 0,
			Unscaled = 1
		}

		[CompilerGenerated]
		private sealed class _003CAnimateRotation_003Ed__30 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public Transform targetTransform;

			public AnimationCurve curveX;

			public AnimationCurve curveY;

			public AnimationCurve curveZ;

			public float duration;

			public MMF_RotatePositionAround _003C_003E4__this;

			public float remapZero;

			public float remapOne;

			private float _003Cjourney_003E5__2;

			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			[DebuggerHidden]
			public _003CAnimateRotation_003Ed__30(int _003C_003E1__state)
			{
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
			}
		}

		public static bool FeedbackTypeAuthorized;

		[MMFInspectorGroup("Animation Targets", true, 61, true, false)]
		[Tooltip("the object whose rotation you want to animate")]
		public Transform AnimateRotationTarget;

		[Tooltip("the object around which to rotate AnimateRotationTarget")]
		public Transform AnimateRotationCenter;

		[MMFInspectorGroup("Transition", true, 63, false, false)]
		[Tooltip("the duration of the transition")]
		public float AnimateRotationDuration;

		[Tooltip("the value to remap the curve's 0 value to")]
		public float RemapCurveZero;

		[Tooltip("the value to remap the curve's 1 value to")]
		public float RemapCurveOne;

		[Tooltip("if this is true, should animate movement on the X axis")]
		public bool AnimateX;

		[Tooltip("how the x part of the movement should animate over time, in degrees")]
		[MMCondition("AnimateX", true)]
		public AnimationCurve AnimateRotationX;

		[Tooltip("if this is true, should animate movement on the Y axis")]
		public bool AnimateY;

		[Tooltip("how the y part of the rotation should animate over time, in degrees")]
		[MMCondition("AnimateY", true)]
		public AnimationCurve AnimateRotationY;

		[Tooltip("if this is true, should animate movement on the Z axis")]
		public bool AnimateZ;

		[Tooltip("how the z part of the rotation should animate over time, in degrees")]
		[MMCondition("AnimateZ", true)]
		public AnimationCurve AnimateRotationZ;

		[Tooltip("if this is true, calling that feedback will trigger it, even if it's in progress. If it's false, it'll prevent any new Play until the current one is over")]
		public bool AllowAdditivePlays;

		[Tooltip("if this is true, initial and destination rotations will be recomputed on every play")]
		public bool DetermineRotationOnPlay;

		protected Vector3 _initialPosition;

		protected Vector3 _rotationAngles;

		protected Coroutine _coroutine;

		public override bool HasAutomatedTargetAcquisition => false;

		public override float FeedbackDuration
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public override bool HasRandomness => false;

		protected override void AutomateTargetAcquisition()
		{
		}

		protected override void CustomInitialization(MMF_Player owner)
		{
		}

		protected virtual void GetInitialPosition()
		{
		}

		protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		protected virtual void ClearCoroutine()
		{
		}

		[IteratorStateMachine(typeof(_003CAnimateRotation_003Ed__30))]
		protected virtual IEnumerator AnimateRotation(Transform targetTransform, Vector3 vector, float duration, AnimationCurve curveX, AnimationCurve curveY, AnimationCurve curveZ, float remapZero, float remapOne)
		{
			return null;
		}

		protected virtual void ApplyRotation(Transform targetTransform, float remapZero, float remapOne, AnimationCurve curveX, AnimationCurve curveY, AnimationCurve curveZ, float percent)
		{
		}

		protected override void CustomStopFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		protected override void CustomRestoreInitialValues()
		{
		}

		public override void OnDisable()
		{
		}
	}
}
