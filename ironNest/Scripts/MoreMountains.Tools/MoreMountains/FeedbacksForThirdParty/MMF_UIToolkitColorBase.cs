using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.Serialization;

namespace MoreMountains.FeedbacksForThirdParty
{
	[AddComponentMenu(null)]
	public class MMF_UIToolkitColorBase : MMF_UIToolkit
	{
		public enum Modes
		{
			OverTime = 0,
			Instant = 1
		}

		[CompilerGenerated]
		private sealed class _003CImageSequence_003Ed__22 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MMF_UIToolkitColorBase _003C_003E4__this;

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
			public _003CImageSequence_003Ed__22(int _003C_003E1__state)
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

		[MMFInspectorGroup("Color", true, 55, true, false)]
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

		[Tooltip("if this is true, the initial color will be applied to the gradient start")]
		[MMFEnumCondition("Mode", new int[] { 0 })]
		public bool ApplyInitialColorToGradientStart;

		[Tooltip("if this is true, the initial color will be applied to the gradient end")]
		[MMFEnumCondition("Mode", new int[] { 0 })]
		public bool ApplyInitialColorToGradientEnd;

		[FormerlySerializedAs("GrabInitialColorsOnPlay")]
		[Tooltip("if this is true, the initial color will be applied to the gradient start and end on play")]
		[MMFEnumCondition("Mode", new int[] { 0 })]
		public bool ApplyInitialColorsOnPlay;

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

		protected override void CustomInitialization(MMF_Player owner)
		{
		}

		protected virtual void HandleApplyInitialColors()
		{
		}

		protected virtual void ApplyColor(Color newColor)
		{
		}

		protected virtual Color GetInitialColor()
		{
			return default(Color);
		}

		protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		[IteratorStateMachine(typeof(_003CImageSequence_003Ed__22))]
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

		protected override void CustomRestoreInitialValues()
		{
		}
	}
}
