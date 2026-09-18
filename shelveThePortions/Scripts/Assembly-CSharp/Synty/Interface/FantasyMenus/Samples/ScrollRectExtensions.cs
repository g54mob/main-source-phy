using UnityEngine;
using UnityEngine.UI;

namespace Synty.Interface.FantasyMenus.Samples
{
	public static class ScrollRectExtensions
	{
		public static void SnapChildIntoView(this ScrollRect instance, RectTransform child)
		{
			instance.content.localPosition = instance.GetSnapToPositionToBringChildIntoView(child);
		}

		public static Vector3 GetSnapToPositionToBringChildIntoView(this ScrollRect instance, RectTransform child)
		{
			Canvas.ForceUpdateCanvases();
			RectTransform rectTransform = ((instance.viewport != null) ? instance.viewport : instance.GetComponent<RectTransform>());
			Bounds bounds = RectTransformUtility.CalculateRelativeRectTransformBounds(rectTransform, child);
			float num = 0f - Mathf.Max(0f, bounds.max.y - rectTransform.rect.yMax);
			float num2 = 0f - Mathf.Min(0f, bounds.min.y - rectTransform.rect.yMin);
			float num3 = 0f - Mathf.Max(0f, bounds.max.x - rectTransform.rect.xMax);
			float num4 = 0f - Mathf.Min(0f, bounds.min.x - rectTransform.rect.xMin);
			Vector3 vector = new Vector3(instance.horizontal ? (num3 + num4) : 0f, instance.vertical ? (num2 + num) : 0f, 0f);
			return instance.content.localPosition + vector;
		}
	}
}
