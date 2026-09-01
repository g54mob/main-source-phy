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
	[FeedbackHelp("This feedback will animate the target object's position over time, for the specified duration, from the chosen initial position to the chosen destination. These can either be relative Vector3 offsets from the Feedback's position, or Transforms. If you specify transforms, the Vector3 values will be ignored.")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks", null)]
	[FeedbackPath("Transform/Position")]
	public class MMF_Position : MMF_Feedback
	{
		public enum Spaces
		{
			World = 0,
			Local = 1,
			RectTransform = 2,
			Self = 3
		}

		public enum Modes
		{
			AtoB = 0,
			AlongCurve = 1,
			ToDestination = 2
		}

		public enum TimeScales
		{
			Scaled = 0,
			Unscaled = 1
		}

		[CompilerGenerated]
		private sealed class _003CMoveAlongCurve_003Ed__54 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MMF_Position _003C_003E4__this;

			public float duration;

			public GameObject movingObject;

			public Vector3 initialPosition;

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
			public _003CMoveAlongCurve_003Ed__54(int _003C_003E1__state)
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
		private sealed class _003CMoveFromTo_003Ed__56 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MMF_Position _003C_003E4__this;

			public float duration;

			public MMTweenType tweenType;

			public Vector3 pointA;

			public Vector3 pointB;

			public GameObject movingObject;

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
			public _003CMoveFromTo_003Ed__56(int _003C_003E1__state)
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

		[MMFInspectorGroup("Position Target", true, 61, true, false)]
		[Tooltip("the object this feedback will animate the position for")]
		public GameObject AnimatePositionTarget;

		[MMFInspectorGroup("Transition", true, 63, false, false)]
		[Tooltip("the mode this animation should follow (either going from A to B, or moving along a curve)")]
		public Modes Mode;

		[Tooltip("the space in which to move the position in")]
		public Spaces Space;

		[Tooltip("whether or not to randomize remap values between their base and alt values on play, useful to add some variety every time you play this feedback")]
		[MMFEnumCondition("Mode", new int[] { 1 })]
		public bool RandomizeRemap;

		[Tooltip("the duration of the animation on play")]
		public float AnimatePositionDuration;

		[Tooltip("the MMTween curve definition to use instead of the animation curve to define the acceleration of the movement")]
		[MMFEnumCondition("Mode", new int[] { 0, 2 })]
		public MMTweenType AnimatePositionTween;

		[MMFEnumCondition("Mode", new int[] { 1 })]
		[Tooltip("the value to remap the curve's 0 value to")]
		public float RemapCurveZero;

		[MMFCondition("RandomizeRemap", true)]
		[MMFEnumCondition("Mode", new int[] { 1 })]
		[Tooltip("in randomize remap mode, the value to remap the curve's 0 value to (randomized between this and RemapCurveZero")]
		public float RemapCurveZeroAlt;

		[Tooltip("the value to remap the curve's 1 value to")]
		[MMFEnumCondition("Mode", new int[] { 1 })]
		[FormerlySerializedAs("CurveMultiplier")]
		public float RemapCurveOne;

		[Tooltip("in randomize remap mode, the value to remap the curve's 1 value to (randomized between this and RemapCurveOne)")]
		[MMFCondition("RandomizeRemap", true)]
		[MMFEnumCondition("Mode", new int[] { 1 })]
		public float RemapCurveOneAlt;

		[Tooltip("if this is true, the x position will be animated")]
		[MMFEnumCondition("Mode", new int[] { 1 })]
		public bool AnimateX;

		[Tooltip("the acceleration of the movement")]
		[MMFCondition("AnimateX", true)]
		public MMTweenType AnimatePositionTweenX;

		[Tooltip("if this is true, the y position will be animated")]
		[MMFEnumCondition("Mode", new int[] { 1 })]
		public bool AnimateY;

		[Tooltip("the acceleration of the movement")]
		[MMFCondition("AnimateY", true)]
		public MMTweenType AnimatePositionTweenY;

		[Tooltip("if this is true, the z position will be animated")]
		[MMFEnumCondition("Mode", new int[] { 1 })]
		public bool AnimateZ;

		[Tooltip("the acceleration of the movement")]
		[MMFCondition("AnimateZ", true)]
		public MMTweenType AnimatePositionTweenZ;

		[Tooltip("if this is true, calling that feedback will trigger it, even if it's in progress. If it's false, it'll prevent any new Play until the current one is over")]
		public bool AllowAdditivePlays;

		[MMFInspectorGroup("Positions", true, 64, false, false)]
		[Tooltip("if this is true, movement will be relative to the object's initial position. So moving its y position along a curve going from 0 to 1 will move it up one unit. If this is false, in that same example, it'll be moved from 0 to 1 in absolute coordinates.")]
		public bool RelativePosition;

		[Tooltip("if this is true, initial and destination positions will be recomputed on every play")]
		public bool DeterminePositionsOnPlay;

		[Tooltip("the initial position")]
		[MMFEnumCondition("Mode", new int[] { 0, 1 })]
		public Vector3 InitialPosition;

		[Tooltip("the destination position")]
		[MMFEnumCondition("Mode", new int[] { 0, 2 })]
		public Vector3 DestinationPosition;

		[Tooltip("the initial transform - if set, takes precedence over the Vector3 above")]
		[MMFEnumCondition("Mode", new int[] { 0, 1 })]
		public Transform InitialPositionTransform;

		[Tooltip("the destination transform - if set, takes precedence over the Vector3 above")]
		[MMFEnumCondition("Mode", new int[] { 0, 2 })]
		public Transform DestinationPositionTransform;

		[HideInInspector]
		public AnimationCurve AnimatePositionCurveX;

		[HideInInspector]
		public AnimationCurve AnimatePositionCurveY;

		[HideInInspector]
		public AnimationCurve AnimatePositionCurveZ;

		[HideInInspector]
		public AnimationCurve AnimatePositionCurve;

		protected Vector3 _newPosition;

		protected Vector3 _currentPosition;

		protected RectTransform _rectTransform;

		protected Vector3 _initialPosition;

		protected Vector3 _destinationPosition;

		protected Coroutine _coroutine;

		protected Vector3 _workInitialPosition;

		protected Vector3 _workDestinationPosition;

		protected float _remapCurveZero;

		protected float _remapCurveOne;

		public override bool HasRandomness => false;

		public override bool CanForceInitialValue => false;

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

		protected override void AutomateTargetAcquisition()
		{
		}

		protected override void CustomInitialization(MMF_Player owner)
		{
		}

		protected virtual void DeterminePositions()
		{
		}

		protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		[IteratorStateMachine(typeof(_003CMoveAlongCurve_003Ed__54))]
		protected virtual IEnumerator MoveAlongCurve(GameObject movingObject, Vector3 initialPosition, float duration, float intensityMultiplier)
		{
			return null;
		}

		protected virtual void ComputeNewCurvePosition(GameObject movingObject, Vector3 initialPosition, float percent, float intensityMultiplier)
		{
		}

		[IteratorStateMachine(typeof(_003CMoveFromTo_003Ed__56))]
		protected virtual IEnumerator MoveFromTo(GameObject movingObject, Vector3 pointA, Vector3 pointB, float duration, MMTweenType tweenType)
		{
			return null;
		}

		protected virtual Vector3 GetPosition(Transform target)
		{
			return default(Vector3);
		}

		protected virtual void SetPosition(Transform target, Vector3 newPosition)
		{
		}

		protected override void CustomStopFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		protected override void CustomRestoreInitialValues()
		{
		}

		public override void OnDisable()
		{
		}

		public override void OnValidate()
		{
		}
	}
}
