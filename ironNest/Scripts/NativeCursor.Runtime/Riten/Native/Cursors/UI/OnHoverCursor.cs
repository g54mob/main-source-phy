using UnityEngine;
using UnityEngine.EventSystems;

namespace Riten.Native.Cursors.UI
{
	public class OnHoverCursor : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
	{
		[SerializeField]
		private NTCursors _cursor;

		[Tooltip("Higher priority means this cursor will override other cursors with lower priority")]
		[SerializeField]
		private int _priority;

		private bool _isHovering;

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

		private void OnTransformParentChanged()
		{
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
		}

		public void OnPointerExit(PointerEventData eventData)
		{
		}
	}
}
