using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace MoreMountains.Tools
{
	[RequireComponent(typeof(Rect))]
	[RequireComponent(typeof(CanvasGroup))]
	[AddComponentMenu("More Mountains/Tools/Controls/MMTouchJoystick")]
	public class MMTouchJoystick : MMMonoBehaviour, IDragHandler, IEventSystemHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
	{
		public enum MaxRangeModes
		{
			Distance = 0,
			DistanceToTransform = 1
		}

		[MMInspectorGroup("Camera", true, 16, false)]
		[Tooltip("The camera to use as the reference for any ScreenToWorldPoint computations")]
		public Camera TargetCamera;

		[MMInspectorGroup("Joystick Behaviour", true, 18, false)]
		[Tooltip("Determines whether the horizontal axis of this stick should be enabled. If not, the stick will only move vertically.")]
		public bool HorizontalAxisEnabled;

		[Tooltip("Determines whether the vertical axis of this stick should be enabled. If not, the stick will only move horizontally.")]
		public bool VerticalAxisEnabled;

		[Tooltip("the mode in which to compute the range. Distance will be a flat value, DistanceToTransform will be a distance to a transform you can move around and potentially resize as you wish for various resolutions")]
		public MaxRangeModes MaxRangeMode;

		[Tooltip("The MaxRange is the maximum distance from its initial center position you can drag the joystick to.")]
		[MMEnumCondition("MaxRangeMode", new int[] { 0 })]
		public float MaxRange;

		[Tooltip("in DistanceToTransform mode, the object whose distance to the center will be used to compute the max range. Note that this is computed once, at init. Call RefreshMaxRangeDistance() to recompute it.")]
		[MMEnumCondition("MaxRangeMode", new int[] { 1 })]
		public Transform MaxRangeTransform;

		[MMInspectorGroup("Value Events", true, 19, false)]
		[Tooltip("An event to use the raw value of the joystick")]
		public JoystickEvent JoystickValue;

		[Tooltip("An event to use the normalized value of the joystick")]
		public JoystickEvent JoystickNormalizedValue;

		[Tooltip("An event to use the joystick's amplitude (the magnitude of its Vector2 output)")]
		public JoystickFloatEvent JoystickMagnitudeValue;

		[MMInspectorGroup("Touch Events", true, 8, false)]
		[Tooltip("An event triggered when tapping the joystick for the first time")]
		public UnityEvent OnPointerDownEvent;

		[Tooltip("An event triggered when dragging the stick")]
		public UnityEvent OnDragEvent;

		[Tooltip("An event triggered when releasing the stick")]
		public UnityEvent OnPointerUpEvent;

		[MMInspectorGroup("Rotating Direction Indicator", true, 20, false)]
		[Tooltip("an object you can rotate to show the direction of the joystick. Will only be visible if the movement is above a threshold")]
		public Transform RotatingIndicator;

		[Tooltip("the threshold above which the rotating indicator will appear")]
		public float RotatingIndicatorThreshold;

		[MMInspectorGroup("Knob Opacity", true, 17, false)]
		[Tooltip("the new opacity to apply to the canvas group when the button is pressed")]
		public float PressedOpacity;

		[Tooltip("whether or not to interpolate opacity changes on the knob's canvas group")]
		public bool InterpolateOpacity;

		[Tooltip("the speed at which to interpolate opacity")]
		[MMCondition("InterpolateOpacity", true)]
		public float InterpolateOpacitySpeed;

		[MMInspectorGroup("Debug Output", true, 5, false)]
		[Tooltip("the raw value of the joystick, from 0 to 1 on each axis")]
		[MMReadOnly]
		public Vector2 RawValue;

		[Tooltip("the normalized value of the joystick")]
		[MMReadOnly]
		public Vector2 NormalizedValue;

		[Tooltip("the magnitude of the stick's vector")]
		[MMReadOnly]
		public float Magnitude;

		[Tooltip("whether or not to draw gizmos associated to this stick")]
		public bool DrawGizmos;

		protected Vector2 _neutralPosition;

		protected Vector2 _newTargetPosition;

		protected Vector3 _newJoystickPosition;

		protected float _initialZPosition;

		protected float _targetOpacity;

		protected CanvasGroup _canvasGroup;

		protected float _initialOpacity;

		protected Transform _knobTransform;

		protected bool _rotatingIndicatorIsNotNull;

		protected float _maxRangeTransformDistance;

		public float ComputedMaxRange => 0f;

		public virtual RenderMode ParentCanvasRenderMode { get; protected set; }

		protected virtual void Start()
		{
		}

		public virtual void Initialize()
		{
		}

		public virtual void RefreshMaxRangeDistance()
		{
		}

		public virtual void SetKnobTransform(Transform newTransform)
		{
		}

		protected virtual void Update()
		{
		}

		protected virtual void HandleOpacity()
		{
		}

		protected virtual void RotateIndicator()
		{
		}

		public virtual void SetNeutralPosition()
		{
		}

		public virtual void SetNeutralPosition(Vector3 newPosition)
		{
		}

		public virtual void OnDrag(PointerEventData eventData)
		{
		}

		protected virtual void ClampToBounds()
		{
		}

		protected virtual Vector3 ConvertToWorld(Vector3 position)
		{
			return default(Vector3);
		}

		public virtual void ResetJoystick()
		{
		}

		protected virtual float EvaluateInputValue(float vectorPosition)
		{
			return 0f;
		}

		public virtual void OnEndDrag(PointerEventData eventData)
		{
		}

		public virtual void OnPointerUp(PointerEventData data)
		{
		}

		public virtual void OnPointerDown(PointerEventData data)
		{
		}

		protected virtual void OnEnable()
		{
		}
	}
}
