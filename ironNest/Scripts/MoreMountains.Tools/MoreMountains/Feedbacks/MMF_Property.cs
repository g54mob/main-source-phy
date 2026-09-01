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
	[FeedbackHelp("This feedback will let you target (almost) any property, on any object in your scene. It also works on scriptable objects. Drag an object, select a property, and setup your feedback to update that property over time.")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks.MMTools", null)]
	[FeedbackPath("GameObject/Property")]
	public class MMF_Property : MMF_Feedback
	{
		public enum Modes
		{
			OverTime = 0,
			Instant = 1,
			ToDestination = 2
		}

		[CompilerGenerated]
		private sealed class _003CToDestinationSequence_003Ed__30 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MMF_Property _003C_003E4__this;

			public float intensityMultiplier;

			private float _003Cjourney_003E5__2;

			private float _003CinitialValue_003E5__3;

			private float _003CdestinationValue_003E5__4;

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
			public _003CToDestinationSequence_003Ed__30(int _003C_003E1__state)
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
		private sealed class _003CUpdateValueSequence_003Ed__31 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MMF_Property _003C_003E4__this;

			public float intensityMultiplier;

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
			public _003CUpdateValueSequence_003Ed__31(int _003C_003E1__state)
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

		[MMFInspectorGroup("Target Property", true, 12, false, false)]
		[Tooltip("the receiver to write the level to")]
		public MMPropertyReceiver Target;

		[MMFInspectorGroup("Mode", true, 29, false, false)]
		[Tooltip("whether the feedback should affect the target property instantly or over a period of time")]
		public Modes Mode;

		[Tooltip("how long the target property should change over time")]
		[MMFEnumCondition("Mode", new int[] { 0, 2 })]
		public float Duration;

		[Tooltip("whether or not that target property should be turned off on start")]
		public bool StartsOff;

		[Tooltip("whether or not the values should be relative or not")]
		public bool RelativeValues;

		[Tooltip("if this is true, calling that feedback will trigger it, even if it's in progress. If it's false, it'll prevent any new Play until the current one is over")]
		public bool AllowAdditivePlays;

		[Tooltip("if this is true, initial value will be computed for every play, otherwise only once, on initialization")]
		public bool DetermineInitialValueOnPlay;

		[MMFInspectorGroup("Level", true, 30, false, false)]
		[Tooltip("the curve to tween the intensity on")]
		[MMFEnumCondition("Mode", new int[] { 0, 2 })]
		public MMTweenType LevelCurve;

		[Tooltip("the value to remap the intensity curve's 0 to")]
		[MMFEnumCondition("Mode", new int[] { 0 })]
		public float RemapLevelZero;

		[Tooltip("the value to remap the intensity curve's 1 to")]
		[MMFEnumCondition("Mode", new int[] { 0 })]
		public float RemapLevelOne;

		[Tooltip("the value to move the intensity to in instant mode")]
		[MMFEnumCondition("Mode", new int[] { 1 })]
		public float InstantLevel;

		[Tooltip("the value towards which to animate when in ToDestination mode")]
		[MMFEnumCondition("Mode", new int[] { 2 })]
		public float ToDestinationLevel;

		protected float _initialIntensity;

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

		public override bool CanForceInitialValue => false;

		public override bool ForceInitialValueDelayed => false;

		public override bool HasCustomInspectors => false;

		protected override void CustomInitialization(MMF_Player owner)
		{
		}

		protected virtual void GetInitialIntensity()
		{
		}

		protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		[IteratorStateMachine(typeof(_003CToDestinationSequence_003Ed__30))]
		protected virtual IEnumerator ToDestinationSequence(float intensityMultiplier)
		{
			return null;
		}

		[IteratorStateMachine(typeof(_003CUpdateValueSequence_003Ed__31))]
		protected virtual IEnumerator UpdateValueSequence(float intensityMultiplier)
		{
			return null;
		}

		protected virtual void SetValues(float time, float intensityMultiplier, float remapZero, float remapOne, bool applyRelative)
		{
		}

		protected override void CustomStopFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		protected virtual void Turn(bool status)
		{
		}

		protected override void CustomRestoreInitialValues()
		{
		}
	}
}
