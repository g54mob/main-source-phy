using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace Synty.Interface.FantasyMenus.Samples
{
	public class SampleOnButtonEvents : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, ISelectHandler
	{
		public UnityEvent onHighlighted;

		public UnityEvent onSelected;

		public void OnPointerEnter(PointerEventData eventData)
		{
			if (onHighlighted != null)
			{
				onHighlighted.Invoke();
			}
		}

		public void OnSelect(BaseEventData eventData)
		{
			if (onSelected != null)
			{
				onSelected.Invoke();
			}
		}
	}
}
