using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Kamgam.UGUIComponentsForSettings
{
	[RequireComponent(typeof(Selectable))]
	public class ScrollIntoViewOnSelectUGUI : MonoBehaviour, ISelectHandler, IEventSystemHandler
	{
		[Tooltip("If turned off then it will do nothing.")]
		public bool Enabled;

		[Tooltip("Additional margins in clockwise order: TOP, RIGHT, BOTTOM, LEFT")]
		public Vector4 MarginTRBL;

		public void OnSelect(BaseEventData eventData)
		{
		}

		public static void BringChildIntoView(ScrollRect instance, RectTransform child, Vector4 margin)
		{
		}

		public static Rect TransformRectFrom(Transform to, Transform from)
		{
			return default(Rect);
		}
	}
}
