using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UI;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu(null)]
	[FeedbackHelp("This feedback will let you change the color of a target Graphic over time.")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks", null)]
	[FeedbackPath("UI/Graphic")]
	public class MMF_Graphic : MMF_Feedback
	{
		public enum Modes
		{
			OverTime = 0,
			Instant = 1
		}

		[CompilerGenerated]
		private sealed class _003CGraphicSequence_003Ed__23 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MMF_Graphic _003C_003E4__this;

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
			public _003CGraphicSequence_003Ed__23(int _003C_003E1__state)
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

		[MMFInspectorGroup("Graphic", true, 54, true, false)]
		[Tooltip("the Graphic to affect when playing the feedback")]
		public Graphic TargetGraphic;

		[Tooltip("whether the feedback should affect the Graphic instantly or over a period of time")]
		public Modes Mode;

		[Tooltip("how long the Graphic should change over time")]
		[MMFEnumCondition("Mode", new int[] { 0 })]
		public float Duration;

		[Tooltip("whether or not that Graphic should be turned off on start")]
		public bool StartsOff;

		[Tooltip("if this is true, the target will be disabled when this feedbacks is stopped")]
		public bool DisableOnStop;

		[Tooltip("if this is true, calling that feedback will trigger it, even if it's in progress. If it's false, it'll prevent any new Play until the current one is over")]
		public bool AllowAdditivePlays;

		[Tooltip("whether or not to modify the color of the Graphic")]
		public bool ModifyColor;

		[Tooltip("the colors to apply to the Graphic over time")]
		[MMFEnumCondition("Mode", new int[] { 0 })]
		public Gradient ColorOverTime;

		[Tooltip("the color to move to in instant mode")]
		[MMFEnumCondition("Mode", new int[] { 1 })]
		public Color InstantColor;

		protected Coroutine _coroutine;

		protected Color _initialColor;

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

		public override bool HasAutomatedTargetAcquisition => false;

		protected override void AutomateTargetAcquisition()
		{
		}

		protected override void CustomInitialization(MMF_Player owner)
		{
		}

		protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		[IteratorStateMachine(typeof(_003CGraphicSequence_003Ed__23))]
		protected virtual IEnumerator GraphicSequence()
		{
			return null;
		}

		protected virtual void SetGraphicValues(float time)
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
