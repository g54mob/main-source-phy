using UnityEngine;
using UnityEngine.EventSystems;

namespace Riten.Native.Cursors.UI
{
	public class OnDragCursor : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler
	{
		[SerializeField]
		private NTCursors _cursor;

		[Tooltip("Higher priority means this cursor will override other cursors with lower priority")]
		[SerializeField]
		private int _priority;

		private bool _isDragging;

		private int _pushedId;

		private int _transformDepth;

		public NTCursors Cursor
		{
			get
			{
				return default(NTCursors);
			}
			set
			{
			}
		}

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
