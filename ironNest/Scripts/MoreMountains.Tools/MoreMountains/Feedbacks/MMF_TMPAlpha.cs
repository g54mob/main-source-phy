using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using MoreMountains.Tools;
using TMPro;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu(null)]
	[FeedbackHelp("This feedback lets you control the alpha of a target TMP over time.")]
	[FeedbackPath("TextMesh Pro/TMP Alpha")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks.TextMeshPro", null)]
	public class MMF_TMPAlpha : MMF_Feedback
	{
		public enum AlphaModes
		{
			Instant = 0,
			Interpolate = 1,
			ToDestination = 2
		}

		[CompilerGenerated]
		private sealed class _003CChangeAlpha_003Ed__23 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MMF_TMPAlpha _003C_003E4__this;

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
			public _003CChangeAlpha_003Ed__23(int _003C_003E1__state)
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

		[MMFInspectorGroup("Target", true, 12, true, false)]
		[Tooltip(" TMP_Text component to control")]
		public TMP_Text TargetTMPText;

		[MMFInspectorGroup("Alpha", true, 16, false, false)]
		[Tooltip("the selected color mode :Instant : the alpha will change instantly to the target one,Curve : the alpha will be interpolated along the curve,interpolate : lerps from the current color to the destination one ")]
		public AlphaModes AlphaMode;

		[Tooltip("how long the color of the text should change over time")]
		[MMFEnumCondition("AlphaMode", new int[] { 1, 2 })]
		public float Duration;

		[Tooltip("the alpha to apply when in instant mode")]
		[MMFEnumCondition("AlphaMode", new int[] { 0 })]
		public float InstantAlpha;

		[Tooltip("the curve to use when interpolating towards the destination alpha")]
		[MMFEnumCondition("AlphaMode", new int[] { 1, 2 })]
		public MMTweenType Curve;

		[Tooltip("the value to which the curve's 0 should be remapped")]
		[MMFEnumCondition("AlphaMode", new int[] { 1 })]
		public float CurveRemapZero;

		[Tooltip("the value to which the curve's 1 should be remapped")]
		[MMFEnumCondition("AlphaMode", new int[] { 1 })]
		public float CurveRemapOne;

		[Tooltip("the alpha to aim towards when in ToDestination mode")]
		[MMFEnumCondition("AlphaMode", new int[] { 2 })]
		public float DestinationAlpha;

		[Tooltip("if this is true, calling that feedback will trigger it, even if it's in progress. If it's false, it'll prevent any new Play until the current one is over")]
		public bool AllowAdditivePlays;

		protected float _initialAlpha;

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

		public override bool HasAutomatedTargetAcquisition => false;

		public override bool HasCustomInspectors => false;

		protected override void AutomateTargetAcquisition()
		{
		}

		protected override void CustomInitialization(MMF_Player owner)
		{
		}

		protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		[IteratorStateMachine(typeof(_003CChangeAlpha_003Ed__23))]
		protected virtual IEnumerator ChangeAlpha()
		{
			return null;
		}

		protected override void CustomStopFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		protected virtual void SetAlpha(float time)
		{
		}

		protected override void CustomRestoreInitialValues()
		{
		}
	}
}
