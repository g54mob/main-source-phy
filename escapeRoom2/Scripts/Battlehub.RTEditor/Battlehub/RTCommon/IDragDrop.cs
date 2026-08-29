using Battlehub.Utils;
using UnityEngine.EventSystems;

namespace Battlehub.RTCommon
{
	public interface IDragDrop
	{
		object[] DragObjects { get; }

		object Source { get; }

		bool InProgress { get; }

		event DragDropEventHander BeginDrag;

		event DragDropEventHander Drag;

		event DragDropEventHander Drop;

		void Reset();

		void SetCursor(KnownCursor cursorType);

		void RaiseBeginDrag(object source, object[] dragItems, PointerEventData pointerEventData, KnownCursor initialCursor = KnownCursor.DropNotAllowed);

		void RaiseDrag(PointerEventData eventData);

		void RaiseDrop(PointerEventData pointerEventData);
	}
}
