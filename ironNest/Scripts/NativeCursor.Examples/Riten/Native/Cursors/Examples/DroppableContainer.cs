using UnityEngine;
using UnityEngine.EventSystems;

namespace Riten.Native.Cursors.Examples
{
	public class DroppableContainer : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
	{
		private static DroppableContainer _current;

		[SerializeField]
		private bool _canDrop;

		public static DroppableContainer current => null;

		public bool canDrop => false;

		public void OnPointerEnter(PointerEventData eventData)
		{
		}

		public void OnPointerExit(PointerEventData eventData)
		{
		}
	}
}
