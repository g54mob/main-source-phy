using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.Serialization;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu(null)]
	[MovedFrom(false, null, "MoreMountains.Feedbacks", null)]
	[FeedbackPath("Transform/Squash and Stretch")]
	[FeedbackHelp("This feedback will let you modify the scale of an object on an axis while the other two axis (or only one) get automatically modified to conserve mass.")]
	public class MMF_SquashAndStretch : MMF_Feedback
	{
		public enum Modes
		{
			Absolute = 0,
			Additive = 1,
			ToDestination = 2
		}

		public enum PossibleAxis
		{
			XtoYZ = 0,
			XtoY = 1,
			XtoZ = 2,
			YtoXZ = 3,
			YtoX = 4,
			YtoZ = 5,
			ZtoXZ = 6,
			ZtoX = 7,
			ZtoY = 8
		}

		public enum TimeScales
		{
			Scaled = 0,
			Unscaled = 1
		}

		[CompilerGenerated]
		private sealed class _003CAnimateScale_003Ed__32 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public Transform targetTransform;

			public float duration;

			public MMF_SquashAndStretch _003C_003E4__this;

			public AnimationCurve curve;

			public float remapCurveZero;

			public float remapCurveOne;

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
			public _003CAnimateScale_003Ed__32(int _003C_003E1__state)
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
		private sealed class _003CScaleToDestination_003Ed__31 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MMF_SquashAndStretch _003C_003E4__this;

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
			public _003CScaleToDestination_003Ed__31(int _003C_003E1__state)
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

		[MMFInspectorGroup("Squash & Stretch", true, 54, true, false)]
		[Tooltip("the object to animate")]
		public Transform SquashAndStretchTarget;

		[Tooltip("the mode this feedback should operate onAbsolute : follows the curveAdditive : adds to the current scale of the targetToDestination : sets the scale to the destination target, whatever the current scale is")]
		public Modes Mode;

		public PossibleAxis Axis;

		[Tooltip("the duration of the animation")]
		public float AnimateScaleDuration;

		[Tooltip("the value to remap the curve's 0 value to")]
		public float RemapCurveZero;

		[Tooltip("the value to remap the curve's 1 value to")]
		[FormerlySerializedAs("Multiplier")]
		public float RemapCurveOne;

		[Tooltip("how much should be added to the curve")]
		public float Offset;

		[Tooltip("the curve along which to animate the scale")]
		public AnimationCurve AnimateCurve;

		[Tooltip("if this is true, calling that feedback will trigger it, even if it's in progress. If it's false, it'll prevent any new Play until the current one is over")]
		public bool AllowAdditivePlays;

		[Tooltip("if this is true, initial and destination scales will be recomputed on every play")]
		public bool DetermineScaleOnPlay;

		[Tooltip("the scale to reach when in ToDestination mode")]
		[MMFEnumCondition("Mode", new int[] { 2 })]
		public float DestinationScale;

		protected Vector3 _initialScale;

		protected float _initialAxisScale;

		protected Vector3 _newScale;

		protected Coroutine _coroutine;

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

		public override bool HasRandomness => false;

		protected override void AutomateTargetAcquisition()
		{
		}

		protected override void CustomInitialization(MMF_Player owner)
		{
		}

		protected virtual void GetInitialScale()
		{
		}

		protected virtual void GetAxisScale()
		{
		}

		protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		[IteratorStateMachine(typeof(_003CScaleToDestination_003Ed__31))]
		protected virtual IEnumerator ScaleToDestination()
		{
			return null;
		}

		[IteratorStateMachine(typeof(_003CAnimateScale_003Ed__32))]
		protected virtual IEnumerator AnimateScale(Transform targetTransform, float duration, AnimationCurve curve, PossibleAxis axis, float remapCurveZero = 0f, float remapCurveOne = 1f)
		{
			return null;
		}

		protected virtual void ComputeAndApplyScale(float percent, AnimationCurve curve, float remapCurveZero, float remapCurveOne, Transform targetTransform)
		{
		}

		protected virtual void ApplyScale(float newScale)
		{
		}

		protected override void CustomStopFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		public override void OnDisable()
		{
		}

		protected override void CustomRestoreInitialValues()
		{
		}
	}
}
