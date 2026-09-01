using UnityEngine;
using UnityEngine.EventSystems;

namespace Landfall.TABC
{
	public class Draggable : MonoBehaviour, IDragHandler, IEventSystemHandler, IBeginDragHandler, IEndDragHandler
	{
		private void Start()
		{
		}

		public void OnBeginDrag(PointerEventData eventData)
		{
			DragHandler.instance.StartDrag(GetComponent<UnitButton>().data.dataInstance, GetComponent<UnitButton>());
		}

		public void OnEndDrag(PointerEventData eventData)
		{
		}

		public void OnDrag(PointerEventData eventData)
		{
		}
	}
}
