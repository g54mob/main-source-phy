using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using UnityEngine;

namespace MoreMountains.FeedbacksForThirdParty
{
	[AddComponentMenu(null)]
	public class MMF_UIToolkitFloatBase : MMF_UIToolkit
	{
		public enum Modes
		{
			Instant = 0,
			Interpolate = 1,
			ToDestination = 2
		}

		[CompilerGenerated]
		private sealed class _003CChangeValue_003Ed__19 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MMF_UIToolkitFloatBase _003C_003E4__this;

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
			public _003CChangeValue_003Ed__19(int _003C_003E1__state)
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

		[MMFInspectorGroup("Value", true, 16, false, false)]
		[Tooltip("the selected mode :Instant : the value will change instantly to the target one,Curve : the value will be interpolated along the curve,interpolate : lerps from the current value to the destination one ")]
		public Modes Mode;

		[Tooltip("whether or not the value should be applied relatively to the initial value")]
		[MMFEnumCondition("Mode", new int[] { 1, 0 })]
		public bool RelativeValue;

		[Tooltip("if this is true, calling that feedback will trigger it, even if it's in progress. If it's false, it'll prevent any new Play until the current one is over")]
		public bool AllowAdditivePlays;

		[Tooltip("how long the color of the text should change over time")]
		[MMFEnumCondition("Mode", new int[] { 1, 2 })]
		public float Duration;

		[Tooltip("the value to apply when in instant mode")]
		[MMFEnumCondition("Mode", new int[] { 0 })]
		public float InstantValue;

		[Tooltip("the curve to use when interpolating towards the destination value")]
		[MMFEnumCondition("Mode", new int[] { 1, 2 })]
		public MMTweenType Curve;

		[Tooltip("the value to which the curve's 0 should be remapped")]
		[MMFEnumCondition("Mode", new int[] { 1 })]
		public float CurveRemapZero;

		[Tooltip("the value to which the curve's 1 should be remapped")]
		[MMFEnumCondition("Mode", new int[] { 1 })]
		public float CurveRemapOne;

		[Tooltip("the value to aim towards when in ToDestination mode")]
		[MMFEnumCondition("Mode", new int[] { 2 })]
		public float DestinationValue;

		protected float _initialValue;

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

		public override bool HasCustomInspectors => false;

		protected override void CustomInitialization(MMF_Player owner)
		{
		}

		protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		[IteratorStateMachine(typeof(_003CChangeValue_003Ed__19))]
		protected virtual IEnumerator ChangeValue()
		{
			return null;
		}

		protected override void CustomStopFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		protected virtual void ApplyTime(float time)
		{
		}

		protected virtual void SetValue(float newValue)
		{
		}

		protected virtual float GetInitialValue()
		{
			return 0f;
		}

		protected override void CustomRestoreInitialValues()
		{
		}
	}
}
