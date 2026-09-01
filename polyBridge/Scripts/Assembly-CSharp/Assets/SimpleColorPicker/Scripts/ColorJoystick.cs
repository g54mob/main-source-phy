using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.SimpleColorPicker.Scripts
{
	public class ColorJoystick : MonoBehaviour, IDragHandler, IEventSystemHandler
	{
		public Image Center;

		public RectTransform RectTransform;

		public ColorPicker ColorPicker;

		public void OnDrag(PointerEventData eventData)
		{
			if (RectTransformUtility.ScreenPointToLocalPointInRectangle(RectTransform, GameInput.GetMousePosition(), null, out var localPoint))
			{
				localPoint.x = Mathf.Max(localPoint.x, RectTransform.rect.min.x);
				localPoint.y = Mathf.Max(localPoint.y, RectTransform.rect.min.y);
				localPoint.x = Mathf.Min(localPoint.x, RectTransform.rect.max.x);
				localPoint.y = Mathf.Min(localPoint.y, RectTransform.rect.max.y);
				base.transform.localPosition = localPoint;
				Texture2D texture = ColorPicker.Texture;
				float num = localPoint.x / RectTransform.rect.width * (float)texture.width;
				float num2 = localPoint.y / RectTransform.rect.height * (float)texture.height;
				Color.RGBToHSV(ColorPicker.Color, out var _, out var _, out var _);
				Color color = Color.HSVToRGB(ColorPicker.H.Value, num / (float)texture.width, num2 / (float)texture.height);
				ColorPicker.SetColor(color, picker: false);
			}
		}
	}
}
