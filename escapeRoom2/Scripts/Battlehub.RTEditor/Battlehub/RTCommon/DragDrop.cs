using Battlehub.Utils;
using UnityEngine.EventSystems;

namespace Battlehub.RTCommon
{
	public class DragDrop : IDragDrop
	{
		private object m_cursorLocker = new object();

		private KnownCursor m_currentCursorType;

		private object m_source;

		private IRTE m_editor;

		public object[] DragObjects { get; private set; }

		public bool InProgress
		{
			get
			{
				if (DragObjects != null)
				{
					return DragObjects.Length != 0;
				}
				return false;
			}
		}

		public object Source => m_source;

		public object DragObject
		{
			get
			{
				if (DragObjects == null || DragObjects.Length == 0)
				{
					return null;
				}
				return DragObjects[0];
			}
		}

		public event DragDropEventHander BeginDrag;

		public event DragDropEventHander Drag;

		public event DragDropEventHander Drop;

		public DragDrop(IRTE rte)
		{
			m_editor = rte;
		}

		public void Reset()
		{
			DragObjects = null;
			ResetCursor();
		}

		public void SetCursor(KnownCursor cursorType)
		{
			if (m_currentCursorType != cursorType && m_editor.CursorHelper.SetCursor(m_cursorLocker, cursorType))
			{
				m_currentCursorType = cursorType;
			}
		}

		public void ResetCursor()
		{
			m_editor.CursorHelper.ResetCursor(m_cursorLocker);
			m_currentCursorType = KnownCursor.None;
		}

		public void RaiseBeginDrag(object source, object[] dragItems, PointerEventData pointerEventData, KnownCursor initialCursor = KnownCursor.DropNotAllowed)
		{
			if (dragItems != null && !m_editor.IsBusy)
			{
				m_currentCursorType = KnownCursor.None;
				m_source = source;
				DragObjects = dragItems;
				if (initialCursor != KnownCursor.None)
				{
					SetCursor(initialCursor);
				}
				if (this.BeginDrag != null)
				{
					this.BeginDrag(pointerEventData);
				}
			}
		}

		public void RaiseDrag(PointerEventData eventData)
		{
			if (InProgress && this.Drag != null)
			{
				this.Drag(eventData);
			}
		}

		public void RaiseDrop(PointerEventData pointerEventData)
		{
			if (InProgress)
			{
				if (this.Drop != null)
				{
					this.Drop(pointerEventData);
				}
				ResetCursor();
				DragObjects = null;
				m_source = null;
			}
		}
	}
}
