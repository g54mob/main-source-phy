using UnityEngine;
using UnityEngine.EventSystems;

namespace Riten.Native.Cursors.UI
{
	public class OnPressCursor : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler
	{
		[SerializeField]
		private NTCursors _cursor;

		[Tooltip("Higher priority means this cursor will override other cursors with lower priority")]
		[SerializeField]
		private int _priority;

		private bool _isPressing;

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

		public void OnPointerDown(PointerEventData eventData)
		{
		}

		public void OnPointerUp(PointerEventData eventData)
		{
		}
	}
}
