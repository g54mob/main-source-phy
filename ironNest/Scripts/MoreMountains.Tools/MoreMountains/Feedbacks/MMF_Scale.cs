using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.Serialization;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu(null)]
	[MovedFrom(false, null, "MoreMountains.Feedbacks", null)]
	[FeedbackPath("Transform/Scale")]
	[FeedbackHelp("This feedback will animate the target's scale on the 3 specified animation curves, for the specified duration (in seconds). You can apply a multiplier, that will multiply each animation curve value.")]
	public class MMF_Scale : MMF_Feedback
	{
		public enum Modes
		{
			Absolute = 0,
			Additive = 1,
			ToDestination = 2
		}

		public enum TimeScales
		{
			Scaled = 0,
			Unscaled = 1
		}

		[CompilerGenerated]
		private sealed class _003CAnimateScale_003Ed__39 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public Transform targetTransform;

			public MMTweenType curveX;

			public MMTweenType curveY;

			public MMTweenType curveZ;

			public float duration;

			public MMF_Scale _003C_003E4__this;

			public Vector3 vector;

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
			public _003CAnimateScale_003Ed__39(int _003C_003E1__state)
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
		private sealed class _003CScaleToDestination_003Ed__38 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MMF_Scale _003C_003E4__this;

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
			public _003CScaleToDestination_003Ed__38(int _003C_003E1__state)
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

		[MMFInspectorGroup("Scale Mode", true, 12, true, false)]
		[Tooltip("the mode this feedback should operate onAbsolute : follows the curveAdditive : adds to the current scale of the targetToDestination : sets the scale to the destination target, whatever the current scale is")]
		public Modes Mode;

		[Tooltip("the object to animate")]
		public Transform AnimateScaleTarget;

		[MMFInspectorGroup("Scale Animation", true, 13, false, false)]
		[Tooltip("the duration of the animation")]
		public float AnimateScaleDuration;

		[Tooltip("the value to remap the curve's 0 value to")]
		public float RemapCurveZero;

		[Tooltip("the value to remap the curve's 1 value to")]
		[FormerlySerializedAs("Multiplier")]
		public float RemapCurveOne;

		[Tooltip("how much should be added to the curve")]
		public float Offset;

		[Tooltip("if this is true, should animate the X scale value")]
		public bool AnimateX;

		[Tooltip("the x scale animation definition")]
		[MMFCondition("AnimateX", true)]
		public MMTweenType AnimateScaleTweenX;

		[Tooltip("if this is true, should animate the Y scale value")]
		public bool AnimateY;

		[Tooltip("the y scale animation definition")]
		[MMFCondition("AnimateY", true)]
		public MMTweenType AnimateScaleTweenY;

		[Tooltip("if this is true, should animate the z scale value")]
		public bool AnimateZ;

		[Tooltip("the z scale animation definition")]
		[MMFCondition("AnimateZ", true)]
		public MMTweenType AnimateScaleTweenZ;

		[Tooltip("if this is true, the AnimateX curve only will be used, and applied to all axis")]
		public bool UniformScaling;

		[Tooltip("if this is true, calling that feedback will trigger it, even if it's in progress. If it's false, it'll prevent any new Play until the current one is over")]
		public bool AllowAdditivePlays;

		[Tooltip("if this is true, initial and destination scales will be recomputed on every play")]
		public bool DetermineScaleOnPlay;

		[Tooltip("the scale to reach when in ToDestination mode")]
		[MMFEnumCondition("Mode", new int[] { 2 })]
		public Vector3 DestinationScale;

		[HideInInspector]
		public AnimationCurve AnimateScaleX;

		[HideInInspector]
		public AnimationCurve AnimateScaleY;

		[HideInInspector]
		public AnimationCurve AnimateScaleZ;

		protected Vector3 _initialScale;

		protected Vector3 _newScale;

		protected Coroutine _coroutine;

		public override bool HasAutomatedTargetAcquisition => false;

		public override bool CanForceInitialValue => false;

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

		protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		[IteratorStateMachine(typeof(_003CScaleToDestination_003Ed__38))]
		protected virtual IEnumerator ScaleToDestination()
		{
			return null;
		}

		[IteratorStateMachine(typeof(_003CAnimateScale_003Ed__39))]
		protected virtual IEnumerator AnimateScale(Transform targetTransform, Vector3 vector, float duration, MMTweenType curveX, MMTweenType curveY, MMTweenType curveZ, float remapCurveZero = 0f, float remapCurveOne = 1f)
		{
			return null;
		}

		protected override void CustomStopFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		public override void OnDisable()
		{
		}

		public override void OnValidate()
		{
		}

		protected override void CustomRestoreInitialValues()
		{
		}
	}
}
