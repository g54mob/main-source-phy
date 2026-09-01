using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.SimpleColorPicker.Scripts
{
	public class PaletteGradient : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IDragHandler
	{
		public ColorJoystick ColorJoystick;

		public void OnPointerDown(PointerEventData eventData)
		{
			if (RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponent<RectTransform>(), eventData.position, Cameras.MainCamera(), out var _))
			{
				ColorJoystick.OnDrag(eventData);
			}
		}

		public void OnDrag(PointerEventData eventData)
		{
			ColorJoystick.OnDrag(eventData);
		}
	}
}
