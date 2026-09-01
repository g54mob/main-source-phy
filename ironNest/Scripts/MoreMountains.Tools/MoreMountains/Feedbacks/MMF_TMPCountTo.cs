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
	[FeedbackHelp("This feedback will let you update a TMP text value over time, with a value going from A to B over time, on a curve")]
	[FeedbackPath("TextMesh Pro/TMP Count To")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks.TextMeshPro", null)]
	public class MMF_TMPCountTo : MMF_Feedback
	{
		[CompilerGenerated]
		private sealed class _003CCountCo_003Ed__21 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MMF_TMPCountTo _003C_003E4__this;

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
			public _003CCountCo_003Ed__21(int _003C_003E1__state)
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

		[MMFInspectorGroup("TextMeshPro Target Text", true, 12, true, false)]
		[Tooltip("the target TMP_Text component we want to change the text on")]
		public TMP_Text TargetTMPText;

		[MMFInspectorGroup("Count Settings", true, 13, false, false)]
		[Tooltip("the value from which to count from")]
		public float CountFrom;

		[Tooltip("the value to count towards")]
		public float CountTo;

		[Tooltip("the curve on which to animate the count")]
		public MMTweenType CountingCurve;

		[Tooltip("the duration of the count, in seconds")]
		public float Duration;

		[Tooltip("the format with which to display the count")]
		public string Format;

		[Tooltip("whether or not value should be floored")]
		public bool FloorValues;

		[Tooltip("the minimum frequency (in seconds) at which to refresh the text field")]
		public float MinRefreshFrequency;

		protected string _newText;

		protected float _startTime;

		protected float _lastRefreshAt;

		protected string _initialText;

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

		protected override void AutomateTargetAcquisition()
		{
		}

		protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		[IteratorStateMachine(typeof(_003CCountCo_003Ed__21))]
		protected virtual IEnumerator CountCo()
		{
			return null;
		}

		protected virtual void UpdateText(float currentValue)
		{
		}

		protected virtual float ProcessCount()
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
