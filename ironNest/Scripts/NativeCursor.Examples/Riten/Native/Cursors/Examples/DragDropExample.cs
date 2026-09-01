using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Riten.Native.Cursors.Examples
{
	public class DragDropExample : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler
	{
		[SerializeField]
		private Image _graphic;

		private DroppableContainer _current;

		private DroppableContainer _lastHovered;

		private int _pushedCursor;

		private void Awake()
		{
		}

		public void OnBeginDrag(PointerEventData eventData)
		{
		}

		public void OnDrag(PointerEventData eventData)
		{
		}

		public void OnEndDrag(PointerEventData eventData)
		{
		}
	}
}
