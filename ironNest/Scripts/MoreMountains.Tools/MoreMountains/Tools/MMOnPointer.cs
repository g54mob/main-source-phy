using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace MoreMountains.Tools
{
	public class MMOnPointer : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerEnterHandler, IPointerUpHandler, IPointerExitHandler, IPointerClickHandler
	{
		[Header("Pointer movement")]
		[Tooltip("an event to trigger when the pointer enters the associated game object")]
		public UnityEvent PointerEnter;

		[Tooltip("an event to trigger when the pointer exits the associated game object")]
		public UnityEvent PointerExit;

		[Header("Clicks")]
		[Tooltip("an event to trigger when the pointer is pressed down on the associated game object")]
		public UnityEvent PointerDown;

		[Tooltip("an event to trigger when the pointer is pressed up on the associated game object")]
		public UnityEvent PointerUp;

		[Tooltip("an event to trigger when the pointer is clicked on the associated game object")]
		public UnityEvent PointerClick;

		public void OnPointerEnter(PointerEventData eventData)
		{
		}

		public void OnPointerExit(PointerEventData eventData)
		{
		}

		public void OnPointerDown(PointerEventData eventData)
		{
		}

		public void OnPointerUp(PointerEventData eventData)
		{
		}

		public void OnPointerClick(PointerEventData eventData)
		{
		}
	}
}
