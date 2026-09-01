using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace MoreMountains.Tools
{
	[RequireComponent(typeof(Rect))]
	[RequireComponent(typeof(CanvasGroup))]
	[AddComponentMenu("More Mountains/Tools/Controls/MMTouchAxis")]
	public class MMTouchAxis : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler, IPointerExitHandler, IPointerEnterHandler
	{
		public enum ButtonStates
		{
			Off = 0,
			ButtonDown = 1,
			ButtonPressed = 2,
			ButtonUp = 3
		}

		[Header("Binding")]
		[Tooltip("The method(s) to call when the axis gets pressed down")]
		public UnityEvent AxisPressedFirstTime;

		[Tooltip("The method(s) to call when the axis gets released")]
		public UnityEvent AxisReleased;

		[Tooltip("The method(s) to call while the axis is being pressed")]
		public AxisEvent AxisPressed;

		[Header("Pressed Behaviour")]
		[MMInformation("Here you can set the opacity of the button when it's pressed. Useful for visual feedback.", MMInformationAttribute.InformationType.Info, false)]
		[Tooltip("the new opacity to apply to the canvas group when the axis is pressed")]
		public float PressedOpacity;

		[Tooltip("the value to send the bound method when the axis is pressed")]
		public float AxisValue;

		[Header("Mouse Mode")]
		[MMInformation("If you set this to true, you'll need to actually press the axis for it to be triggered, otherwise a simple hover will trigger it (better for touch input).", MMInformationAttribute.InformationType.Info, false)]
		[Tooltip("If you set this to true, you'll need to actually press the axis for it to be triggered, otherwise a simple hover will trigger it (better for touch input).")]
		public bool MouseMode;

		protected CanvasGroup _canvasGroup;

		protected float _initialOpacity;

		public virtual ButtonStates CurrentState { get; protected set; }

		protected virtual void Awake()
		{
		}

		protected virtual void Update()
		{
		}

		protected virtual void LateUpdate()
		{
		}

		public virtual void OnPointerDown(PointerEventData data)
		{
		}

		public virtual void OnPointerUp(PointerEventData data)
		{
		}

		protected virtual void OnEnable()
		{
		}

		protected virtual void ResetButton()
		{
		}

		public virtual void OnPointerEnter(PointerEventData data)
		{
		}

		public virtual void OnPointerExit(PointerEventData data)
		{
		}
	}
}
