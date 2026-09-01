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
	[FeedbackHelp("This feedback will animate the target's rotation on the 3 specified animation curves (one per axis), for the specified duration (in seconds).")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks", null)]
	[FeedbackPath("Transform/Rotation")]
	public class MMF_Rotation : MMF_Feedback
	{
		public enum Modes
		{
			Absolute = 0,
			Additive = 1,
			ToDestination = 2
		}

		public enum TimeScales
		{
			Scaled = 0,
			Unscaled = 1
		}

		[CompilerGenerated]
		private sealed class _003CAnimateRotation_003Ed__43 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public Transform targetTransform;

			public MMTweenType curveX;

			public MMTweenType curveY;

			public MMTweenType curveZ;

			public float duration;

			public MMF_Rotation _003C_003E4__this;

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
			public _003CAnimateRotation_003Ed__43(int _003C_003E1__state)
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

		[CompilerGenerated]
		private sealed class _003CRotateToDestination_003Ed__42 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MMF_Rotation _003C_003E4__this;

			private Vector3 _003CdestinationAngles_003E5__2;

			private float _003Cjourney_003E5__3;

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
			public _003CRotateToDestination_003Ed__42(int _003C_003E1__state)
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

		[MMFInspectorGroup("Rotation Target", true, 61, true, false)]
		[Tooltip("the object whose rotation you want to animate")]
		public Transform AnimateRotationTarget;

		[MMFInspectorGroup("Transition", true, 63, false, false)]
		[Tooltip("whether this feedback should animate in absolute values or additive")]
		public Modes Mode;

		[Tooltip("whether this feedback should play on local or world rotation")]
		public Space RotationSpace;

		[Tooltip("the duration of the transition")]
		public float AnimateRotationDuration;

		[Tooltip("the value to remap the curve's 0 value to")]
		[MMFEnumCondition("Mode", new int[] { 0, 1 })]
		public float RemapCurveZero;

		[Tooltip("the value to remap the curve's 1 value to")]
		[MMFEnumCondition("Mode", new int[] { 0, 1 })]
		public float RemapCurveOne;

		[Tooltip("if this is true, should animate the X rotation")]
		[MMFEnumCondition("Mode", new int[] { 0, 1 })]
		public bool AnimateX;

		[Tooltip("how the x part of the rotation should animate over time, in degrees")]
		[MMFCondition("AnimateX")]
		public MMTweenType AnimateRotationTweenX;

		[Tooltip("if this is true, should animate the Y rotation")]
		[MMFEnumCondition("Mode", new int[] { 0, 1 })]
		public bool AnimateY;

		[Tooltip("how the y part of the rotation should animate over time, in degrees")]
		[MMFCondition("AnimateY")]
		public MMTweenType AnimateRotationTweenY;

		[Tooltip("if this is true, should animate the Z rotation")]
		[MMFEnumCondition("Mode", new int[] { 0, 1 })]
		public bool AnimateZ;

		[Tooltip("how the z part of the rotation should animate over time, in degrees")]
		[MMFCondition("AnimateZ")]
		public MMTweenType AnimateRotationTweenZ;

		[Tooltip("if this is true, calling that feedback will trigger it, even if it's in progress. If it's false, it'll prevent any new Play until the current one is over")]
		public bool AllowAdditivePlays;

		[Tooltip("if this is true, initial and destination rotations will be recomputed on every play")]
		public bool DetermineRotationOnPlay;

		[Header("To Destination")]
		[Tooltip("the space in which the ToDestination mode should operate")]
		[MMFEnumCondition("Mode", new int[] { 2 })]
		public Space ToDestinationSpace;

		[Tooltip("the angles to match when in ToDestination mode")]
		[MMFEnumCondition("Mode", new int[] { 2 })]
		public Vector3 DestinationAngles;

		[Tooltip("how the x part of the rotation should animate over time, in degrees")]
		[MMFEnumCondition("Mode", new int[] { 2 })]
		public MMTweenType ToDestinationTween;

		[HideInInspector]
		public AnimationCurve AnimateRotationX;

		[HideInInspector]
		public AnimationCurve AnimateRotationY;

		[HideInInspector]
		public AnimationCurve AnimateRotationZ;

		[HideInInspector]
		public AnimationCurve ToDestinationCurve;

		protected Quaternion _initialRotation;

		protected Vector3 _initialToDestinationAngles;

		protected Quaternion _destinationRotation;

		protected Coroutine _coroutine;

		public override bool HasAutomatedTargetAcquisition => false;

		public override bool CanForceInitialValue => false;

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

		protected virtual void GetInitialRotation()
		{
		}

		protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		protected virtual void ClearCoroutine()
		{
		}

		[IteratorStateMachine(typeof(_003CRotateToDestination_003Ed__42))]
		protected virtual IEnumerator RotateToDestination()
		{
			return null;
		}

		[IteratorStateMachine(typeof(_003CAnimateRotation_003Ed__43))]
		protected virtual IEnumerator AnimateRotation(Transform targetTransform, Vector3 vector, float duration, MMTweenType curveX, MMTweenType curveY, MMTweenType curveZ, float remapZero, float remapOne)
		{
			return null;
		}

		protected virtual void ApplyRotation(Transform targetTransform, float remapZero, float remapOne, MMTweenType curveX, MMTweenType curveY, MMTweenType curveZ, float percent)
		{
		}

		protected override void CustomStopFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		public override void OnDisable()
		{
		}

		public override void OnValidate()
		{
		}

		protected override void CustomRestoreInitialValues()
		{
		}
	}
}
