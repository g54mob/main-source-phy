using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace MoreMountains.Feedbacks
{
	public abstract class MMF_FeedbackBase : MMF_Feedback
	{
		public enum Modes
		{
			OverTime = 0,
			Instant = 1
		}

		[CompilerGenerated]
		private sealed class _003CUpdateValueSequence_003Ed__27 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MMF_FeedbackBase _003C_003E4__this;

			public float feedbacksIntensity;

			public Vector3 position;

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
			public _003CUpdateValueSequence_003Ed__27(int _003C_003E1__state)
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

		[MMFInspectorGroup("Mode", true, 64, false, false)]
		[Tooltip("whether the feedback should affect the target property instantly or over a period of time")]
		public Modes Mode;

		[Tooltip("how long the target property should change over time")]
		[MMFEnumCondition("Mode", new int[] { 0 })]
		public float Duration;

		[Tooltip("whether or not that target property should be turned off on start")]
		public bool StartsOff;

		[Tooltip("whether or not that target property should be turned off once the feedback is done playing")]
		public bool EndsOff;

		[Tooltip("whether or not the values should be relative or not")]
		public bool RelativeValues;

		[Tooltip("if this is true, calling that feedback will trigger it, even if it's in progress. If it's false, it'll prevent any new Play until the current one is over")]
		public bool AllowAdditivePlays;

		[Tooltip("if this is true, the target object will be disabled on stop")]
		public bool DisableOnStop;

		[Tooltip("if this is true, this feedback will only play if its target is active in hierarchy")]
		public bool OnlyPlayIfTargetIsActive;

		protected List<MMF_FeedbackBaseTarget> _targets;

		protected Coroutine _coroutine;

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

		public override bool HasCustomInspectors => false;

		protected override void CustomInitialization(MMF_Player owner)
		{
		}

		public virtual void PrepareTargets()
		{
		}

		public override void OnValidate()
		{
		}

		protected abstract void FillTargets();

		protected virtual void InitializeTargets()
		{
		}

		protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		protected virtual void Instant()
		{
		}

		protected override void CustomRestoreInitialValues()
		{
		}

		[IteratorStateMachine(typeof(_003CUpdateValueSequence_003Ed__27))]
		protected virtual IEnumerator UpdateValueSequence(float feedbacksIntensity, Vector3 position)
		{
			return null;
		}

		protected virtual void SetValues(float time, float feedbacksIntensity, Vector3 position)
		{
		}

		protected override void CustomStopFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		protected virtual void Turn(bool status)
		{
		}

		protected virtual bool CanPlay()
		{
			return false;
		}
	}
}
