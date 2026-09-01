using UnityEngine;
using UnityEngine.EventSystems;

namespace MoreMountains.Tools
{
	[AddComponentMenu("More Mountains/Tools/Controls/MMTouchFollowerJoystick")]
	public class MMTouchFollowerJoystick : MMTouchJoystick
	{
		[MMInspectorGroup("Follower Joystick", true, 23, false)]
		[Tooltip("the canvas group to use as the joystick's knob - the part that moves under your thumb")]
		public CanvasGroup KnobCanvasGroup;

		[Tooltip("the canvas group to use as the joystick's background")]
		public CanvasGroup BackgroundCanvasGroup;

		[Tooltip("if this is true, the joystick will return back to its initial position when released")]
		public bool ResetPositionToInitialOnRelease;

		[Tooltip("if this is true, the background will follow its target with interpolation, otherwise it'll be instant movement")]
		public bool InterpolateFollowMovement;

		[Tooltip("if in interpolate mode, this defines the speed at which the backgrounds follows the knob")]
		[MMCondition("InterpolateFollowMovement", true)]
		public float InterpolateFollowMovementSpeed;

		[Tooltip("whether or not to add a spring to the interpolation of the background movement")]
		[MMCondition("InterpolateFollowMovement", true)]
		public bool SpringFollowInterpolation;

		[Tooltip("when in SpringFollowInterpolation mode, the amount of damping to apply to the spring")]
		[MMCondition("SpringFollowInterpolation", true)]
		public float SpringDamping;

		[Tooltip("when in SpringFollowInterpolation mode, the frequency to apply to the spring")]
		[MMCondition("SpringFollowInterpolation", true)]
		public float SpringFrequency;

		[MMInspectorGroup("Background Constraints", true, 24, false)]
		[Tooltip("if this is true, the joystick won't be able to travel beyond the bounds of the top level canvas")]
		public bool ShouldConstrainBackground;

		[Tooltip("the rect to consider as a background constraint zone, if left empty, will be auto created")]
		public RectTransform BackgroundConstraintRectTransform;

		[Tooltip("the left padding to apply to the background constraint")]
		public float BackgroundConstraintPaddingLeft;

		[Tooltip("the right padding to apply to the background constraint")]
		public float BackgroundConstraintPaddingRight;

		[Tooltip("the top padding to apply to the background constraint")]
		public float BackgroundConstraintPaddingTop;

		[Tooltip("the bottom padding to apply to the background constraint")]
		public float BackgroundConstraintPaddingBottom;

		protected Vector3 _initialPosition;

		protected Vector3 _newPosition;

		protected RectTransform _rectTransform;

		protected RectTransform _backgroundRectTransform;

		protected Vector3[] _innerRectCorners;

		protected Vector3 _newBackgroundPosition;

		protected Vector3 _backgroundPositionTarget;

		protected Vector3 _innerRectTransformBottomLeft;

		protected Vector3 _innerRectTransformTopLeft;

		protected Vector3 _innerRectTransformTopRight;

		protected Vector3 _innerRectTransformBottomRight;

		protected Vector3 _springVelocity;

		protected override void Start()
		{
		}

		public override void Initialize()
		{
		}

		protected override void Update()
		{
		}

		protected virtual void HandleMovementInterpolation()
		{
		}

		protected virtual void CreateInnerRect()
		{
		}

		public override void OnPointerDown(PointerEventData data)
		{
		}

		public override void OnDrag(PointerEventData eventData)
		{
		}

		protected virtual void ComputeJoystickValue()
		{
		}

		protected virtual void ConstrainBackground()
		{
		}

		public override void OnPointerUp(PointerEventData data)
		{
		}

		protected override void ClampToBounds()
		{
		}
	}
}
