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
	[FeedbackHelp("This feedback will let you animate the rotation of a transform to look at a target over time. You can also use it to broadcast a MMLookAtShake event, that MMLookAtShakers on the right channel will be able to listen for and act upon.")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks", null)]
	[FeedbackPath("Transform/LookAt")]
	public class MMF_LookAt : MMF_Feedback
	{
		public enum Modes
		{
			Direct = 0,
			Event = 1
		}

		public enum LookAtTargetModes
		{
			Transform = 0,
			TargetWorldPosition = 1,
			Direction = 2
		}

		public enum UpwardVectors
		{
			Forward = 0,
			Up = 1,
			Right = 2
		}

		[CompilerGenerated]
		private sealed class _003CAnimateLookAt_003Ed__35 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MMF_LookAt _003C_003E4__this;

			private float _003Cduration_003E5__2;

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
			public _003CAnimateLookAt_003Ed__35(int _003C_003E1__state)
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

		[MMFInspectorGroup("Look at settings", true, 37, true, false)]
		[Tooltip("the duration of this feedback, in seconds")]
		public float Duration;

		[Tooltip("the curve over which to animate the look at transition")]
		public MMTweenType LookAtTween;

		[Tooltip("whether or not to lock rotation on the x axis")]
		public bool LockXAxis;

		[Tooltip("whether or not to lock rotation on the y axis")]
		public bool LockYAxis;

		[Tooltip("whether or not to lock rotation on the z axis")]
		public bool LockZAxis;

		[MMFInspectorGroup("What we want to rotate", true, 37, true, false)]
		[Tooltip("whether to make a certain transform look at a target, or to broadcast an event")]
		public Modes Mode;

		[Tooltip("in Direct mode, the transform to rotate to have it look at our target")]
		[MMFEnumCondition("Mode", new int[] { 0 })]
		public Transform TransformToRotate;

		[Tooltip("the vector representing the up direction on the object we want to rotate and look at our target")]
		[MMFEnumCondition("Mode", new int[] { 0 })]
		public UpwardVectors UpwardVector;

		[Tooltip("whether or not to reset shaker values after shake")]
		[MMFEnumCondition("Mode", new int[] { 1 })]
		public bool ResetShakerValuesAfterShake;

		[Tooltip("whether or not to reset the target's values after shake")]
		[MMFEnumCondition("Mode", new int[] { 1 })]
		public bool ResetTargetValuesAfterShake;

		[MMFInspectorGroup("What we want to look at", true, 37, true, false)]
		[Tooltip("the different target modes : either a specific transform to look at, the coordinates of a world position, or a direction vector")]
		public LookAtTargetModes LookAtTargetMode;

		[Tooltip("the transform we want to look at")]
		[MMFEnumCondition("LookAtTargetMode", new int[] { 0 })]
		public Transform LookAtTarget;

		[Tooltip("the coordinates of a point the world that we want to look at")]
		[MMFEnumCondition("LookAtTargetMode", new int[] { 1 })]
		public Vector3 LookAtTargetWorldPosition;

		[Tooltip("a direction (from our rotating object) that we want to look at")]
		[MMFEnumCondition("LookAtTargetMode", new int[] { 2 })]
		public Vector3 LookAtDirection;

		protected Coroutine _coroutine;

		protected Quaternion _initialDirectTargetTransformRotation;

		protected Quaternion _newRotation;

		protected Vector3 _lookAtPosition;

		protected Vector3 _upwards;

		protected Vector3 _direction;

		protected Quaternion _initialRotation;

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

		public override bool HasChannel => false;

		public override bool HasRange => false;

		protected override void CustomInitialization(MMF_Player owner)
		{
		}

		protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		protected virtual void InitiateLookAt(Vector3 position)
		{
		}

		[IteratorStateMachine(typeof(_003CAnimateLookAt_003Ed__35))]
		protected virtual IEnumerator AnimateLookAt()
		{
			return null;
		}

		protected virtual void ApplyRotation(float percent)
		{
		}

		protected override void CustomStopFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		protected virtual void ClearCoroutine()
		{
		}

		protected override void CustomRestoreInitialValues()
		{
		}
	}
}
