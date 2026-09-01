using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu(null)]
	[FeedbackHelp("This feedback will let you change the speed of a target animator, either once, or instantly and then reset it, or interpolate it over time")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks", null)]
	[FeedbackPath("Animation/Animator Speed")]
	public class MMF_AnimatorSpeed : MMF_Feedback
	{
		public enum SpeedModes
		{
			Once = 0,
			InstantThenReset = 1,
			OverTime = 2
		}

		[CompilerGenerated]
		private sealed class _003CChangeSpeedCo_003Ed__19 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MMF_AnimatorSpeed _003C_003E4__this;

			private float _003CnewTargetSpeed_003E5__2;

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
			public _003CChangeSpeedCo_003Ed__19(int _003C_003E1__state)
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

		[MMFInspectorGroup("Animation", true, 12, true, false)]
		[Tooltip("the animator whose parameters you want to update")]
		public Animator BoundAnimator;

		[MMFInspectorGroup("Speed", true, 14, true, false)]
		[Tooltip("whether to change the speed of the target animator once, instantly and reset it later, or have it change over time")]
		public SpeedModes Mode;

		[Tooltip("the new minimum speed at which to set the animator - value will be randomized between min and max")]
		public float NewSpeedMin;

		[Tooltip("the new maximum speed at which to set the animator - value will be randomized between min and max")]
		public float NewSpeedMax;

		[Tooltip("when in instant then reset or over time modes, the duration of the effect")]
		[MMFEnumCondition("Mode", new int[] { 1, 2 })]
		public float Duration;

		[Tooltip("when in over time mode, the curve against which to evaluate the new speed")]
		[MMFEnumCondition("Mode", new int[] { 2 })]
		public AnimationCurve Curve;

		protected Coroutine _coroutine;

		protected float _initialSpeed;

		protected float _startedAt;

		public override bool HasRandomness => false;

		public override bool CanForceInitialValue => false;

		public override bool HasAutomatedTargetAcquisition => false;

		protected override void AutomateTargetAcquisition()
		{
		}

		protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		[IteratorStateMachine(typeof(_003CChangeSpeedCo_003Ed__19))]
		protected virtual IEnumerator ChangeSpeedCo()
		{
			return null;
		}

		protected virtual float DetermineNewSpeed()
		{
			return 0f;
		}

		protected override void CustomStopFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		protected override void CustomRestoreInitialValues()
		{
		}
	}
}
