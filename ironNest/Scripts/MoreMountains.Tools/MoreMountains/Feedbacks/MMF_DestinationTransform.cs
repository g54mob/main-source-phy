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
	[FeedbackHelp("This feedback will let you animate the position/rotation/scale of a target transform to match the one of a destination transform.")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks", null)]
	[FeedbackPath("Transform/Destination")]
	public class MMF_DestinationTransform : MMF_Feedback
	{
		public enum TimeScales
		{
			Scaled = 0,
			Unscaled = 1
		}

		[CompilerGenerated]
		private sealed class _003CAnimateToDestination_003Ed__49 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MMF_DestinationTransform _003C_003E4__this;

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
			public _003CAnimateToDestination_003Ed__49(int _003C_003E1__state)
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

		[MMFInspectorGroup("Target to animate", true, 61, true, false)]
		[Tooltip("the target transform we want to animate properties on")]
		public Transform TargetTransform;

		[Tooltip("whether or not we want to force an origin transform. If not, the current position of the target transform will be used as origin instead")]
		public bool ForceOrigin;

		[Tooltip("the transform to use as origin in ForceOrigin mode")]
		[MMFCondition("ForceOrigin", true)]
		public Transform Origin;

		[Tooltip("the destination transform whose properties we want to match")]
		public Transform Destination;

		[MMFInspectorGroup("Transition", true, 63, false, false)]
		[Tooltip("a global curve to animate all properties on, unless dedicated ones are specified")]
		public MMTweenType GlobalAnimationTween;

		[Tooltip("the duration of the transition, in seconds")]
		public float Duration;

		[MMFInspectorGroup("Axis Locks", true, 64, false, false)]
		[Tooltip("whether or not to animate the X Position")]
		public bool AnimatePositionX;

		[Tooltip("whether or not to animate the Y Position")]
		public bool AnimatePositionY;

		[Tooltip("whether or not to animate the Z Position")]
		public bool AnimatePositionZ;

		[Tooltip("whether or not to animate the X rotation")]
		public bool AnimateRotationX;

		[Tooltip("whether or not to animate the Y rotation")]
		public bool AnimateRotationY;

		[Tooltip("whether or not to animate the Z rotation")]
		public bool AnimateRotationZ;

		[Tooltip("whether or not to animate the W rotation")]
		public bool AnimateRotationW;

		[Tooltip("whether or not to animate the X scale")]
		public bool AnimateScaleX;

		[Tooltip("whether or not to animate the Y scale")]
		public bool AnimateScaleY;

		[Tooltip("whether or not to animate the Z scale")]
		public bool AnimateScaleZ;

		[MMFInspectorGroup("Separate Curves", true, 65, false, false)]
		[Tooltip("whether or not to use a separate animation curve to animate the position")]
		public bool SeparatePositionCurve;

		[Tooltip("the curve to use to animate the position on")]
		[MMFCondition("SeparatePositionCurve", true)]
		public MMTweenType AnimatePositionTween;

		[Tooltip("whether or not to use a separate animation curve to animate the rotation")]
		public bool SeparateRotationCurve;

		[Tooltip("the curve to use to animate the rotation on")]
		[MMFCondition("SeparateRotationCurve", true)]
		public MMTweenType AnimateRotationTween;

		[Tooltip("whether or not to use a separate animation curve to animate the scale")]
		public bool SeparateScaleCurve;

		[Tooltip("the curve to use to animate the scale on")]
		[MMFCondition("SeparateScaleCurve", true)]
		public MMTweenType AnimateScaleTween;

		[HideInInspector]
		public AnimationCurve GlobalAnimationCurve;

		[HideInInspector]
		public AnimationCurve AnimateScaleCurve;

		[HideInInspector]
		public AnimationCurve AnimatePositionCurve;

		[HideInInspector]
		public AnimationCurve AnimateRotationCurve;

		protected Coroutine _coroutine;

		protected Vector3 _newPosition;

		protected Quaternion _newRotation;

		protected Vector3 _newScale;

		protected Vector3 _pointAPosition;

		protected Vector3 _pointBPosition;

		protected Quaternion _pointARotation;

		protected Quaternion _pointBRotation;

		protected Vector3 _pointAScale;

		protected Vector3 _pointBScale;

		protected MMTweenType _animationTweenType;

		protected Vector3 _initialPosition;

		protected Vector3 _initialScale;

		protected Quaternion _initialRotation;

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

		protected override void AutomateTargetAcquisition()
		{
		}

		protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		[IteratorStateMachine(typeof(_003CAnimateToDestination_003Ed__49))]
		protected virtual IEnumerator AnimateToDestination()
		{
			return null;
		}

		protected virtual void ChangeTransformValues(float percent)
		{
		}

		protected override void CustomStopFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		protected override void CustomRestoreInitialValues()
		{
		}

		public override void OnValidate()
		{
		}
	}
}
