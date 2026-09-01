using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu(null)]
	[FeedbackHelp("This feedback will let you change the color of a target Image over time. You can also use it to command one or many MMImageShakers.")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks", null)]
	[FeedbackPath("UI/Image")]
	public class MMF_Image : MMF_Feedback
	{
		public enum Modes
		{
			OverTime = 0,
			Instant = 1
		}

		[CompilerGenerated]
		private sealed class _003CImageSequence_003Ed__25 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MMF_Image _003C_003E4__this;

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
			public _003CImageSequence_003Ed__25(int _003C_003E1__state)
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

		[MMFInspectorGroup("Image", true, 54, true, false)]
		[Tooltip("the Image to affect when playing the feedback")]
		public Image BoundImage;

		[Tooltip("whether the feedback should affect the Image instantly or over a period of time")]
		public Modes Mode;

		[Tooltip("how long the Image should change over time")]
		[MMFEnumCondition("Mode", new int[] { 0 })]
		public float Duration;

		[Tooltip("if this is true, calling that feedback will trigger it, even if it's in progress. If it's false, it'll prevent any new Play until the current one is over")]
		public bool AllowAdditivePlays;

		[Tooltip("whether or not to modify the color of the image")]
		public bool ModifyColor;

		[Tooltip("the colors to apply to the Image over time")]
		[MMFEnumCondition("Mode", new int[] { 0 })]
		public Gradient ColorOverTime;

		[Tooltip("the color to move to in instant mode")]
		[MMFEnumCondition("Mode", new int[] { 1 })]
		public Color InstantColor;

		[Tooltip("whether or not that Image should be turned off on start")]
		[FormerlySerializedAs("StartsOff")]
		public bool DisableOnInit;

		[Tooltip("if this is true, the target will be enabled when this feedback gets played")]
		public bool EnableOnPlay;

		[Tooltip("if this is true, the target disabled after the color over time change ends")]
		public bool DisableOnSequenceEnd;

		[Tooltip("if this is true, the target will be disabled when this feedbacks is stopped")]
		public bool DisableOnStop;

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

		[IteratorStateMachine(typeof(_003CImageSequence_003Ed__25))]
		protected virtual IEnumerator ImageSequence()
		{
			return null;
		}

		protected virtual void SetImageValues(float time)
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
