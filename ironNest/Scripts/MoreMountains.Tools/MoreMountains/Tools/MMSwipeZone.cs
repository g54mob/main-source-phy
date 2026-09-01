using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace MoreMountains.Tools
{
	[RequireComponent(typeof(RectTransform))]
	[AddComponentMenu("More Mountains/Tools/Controls/MMSwipeZone")]
	public class MMSwipeZone : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler, IPointerExitHandler, IPointerEnterHandler
	{
		[Tooltip("the minimal length of a swipe")]
		public float MinimalSwipeLength;

		[Tooltip("the maximum press length of a swipe")]
		public float MaximumPressLength;

		[Tooltip("The method(s) to call when the zone is swiped")]
		public SwipeEvent ZoneSwiped;

		[Tooltip("The method(s) to call while the zone is being pressed")]
		public UnityEvent ZonePressed;

		[Header("Mouse Mode")]
		[MMInformation("If you set this to true, you'll need to actually press the button for it to be triggered, otherwise a simple hover will trigger it (better for touch input).", MMInformationAttribute.InformationType.Info, false)]
		[Tooltip("If you set this to true, you'll need to actually press the button for it to be triggered, otherwise a simple hover will trigger it (better for touch input).")]
		public bool MouseMode;

		protected Vector2 _firstTouchPosition;

		protected float _angle;

		protected float _length;

		protected Vector2 _destination;

		protected Vector2 _deltaSwipe;

		protected MMPossibleSwipeDirections _swipeDirection;

		protected float _lastPointerUpAt;

		protected float _swipeStartedAt;

		protected float _swipeEndedAt;

		protected virtual void Swipe()
		{
		}

		protected virtual void Press()
		{
		}

		public virtual void OnPointerDown(PointerEventData data)
		{
		}

		public virtual void OnPointerUp(PointerEventData data)
		{
		}

		public virtual void OnPointerEnter(PointerEventData data)
		{
		}

		public virtual void OnPointerExit(PointerEventData data)
		{
		}

		protected virtual MMPossibleSwipeDirections AngleToSwipeDirection(float angle)
		{
			return default(MMPossibleSwipeDirections);
		}
	}
}
