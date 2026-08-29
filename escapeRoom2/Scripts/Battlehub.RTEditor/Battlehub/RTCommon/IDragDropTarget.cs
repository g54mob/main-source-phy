using UnityEngine.EventSystems;

namespace Battlehub.RTCommon
{
	public interface IDragDropTarget
	{
		void BeginDrag(object[] dragObjects, PointerEventData eventData);

		void DragEnter(object[] dragObjects, PointerEventData eventData);

		void DragLeave(PointerEventData eventData);

		void Drag(object[] dragObjects, PointerEventData eventData);

		void Drop(object[] dragObjects, PointerEventData eventData);
	}
}
