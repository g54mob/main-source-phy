using UnityEngine;
using UnityEngine.EventSystems;

namespace MoreMountains.Tools
{
	[AddComponentMenu("More Mountains/Tools/Controls/MMTouchRepositionableJoystick")]
	public class MMTouchRepositionableJoystick : MMTouchJoystick, IPointerDownHandler, IEventSystemHandler
	{
		[MMInspectorGroup("Repositionable Joystick", true, 22, false)]
		[Tooltip("the canvas group to use as the joystick's knob")]
		public CanvasGroup KnobCanvasGroup;

		[Tooltip("the canvas group to use as the joystick's background")]
		public CanvasGroup BackgroundCanvasGroup;

		[Tooltip("if this is true, the joystick won't be able to travel beyond the bounds of the top level canvas")]
		public bool ConstrainToInitialRectangle;

		[Tooltip("if this is true, the joystick will return back to its initial position when released")]
		public bool ResetPositionToInitialOnRelease;

		protected Vector3 _initialPosition;

		protected Vector3 _newPosition;

		protected CanvasGroup _knobCanvasGroup;

		protected RectTransform _rectTransform;

		protected override void Start()
		{
		}

		public override void Initialize()
		{
		}

		public override void OnPointerDown(PointerEventData data)
		{
		}

		protected virtual bool WithinBounds()
		{
			return false;
		}

		public override void OnPointerUp(PointerEventData eventData)
		{
		}
	}
}
