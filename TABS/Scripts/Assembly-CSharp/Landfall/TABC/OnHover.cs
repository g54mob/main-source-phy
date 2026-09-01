using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace Landfall.TABC
{
	public class OnHover : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
	{
		public UnityEvent hoverEvent;

		public UnityEvent hoverExitEvent;

		public void OnPointerEnter(PointerEventData eventData)
		{
			hoverEvent.Invoke();
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			hoverExitEvent.Invoke();
		}
	}
}
