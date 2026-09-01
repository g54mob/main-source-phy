using UnityEngine;

namespace TFBGames
{
	public static class RectTransformExtensions
	{
		public static void SetLeft(this RectTransform rectTransform, float left)
		{
			rectTransform.offsetMin = new Vector2(left, rectTransform.offsetMin.y);
		}

		public static void SetRight(this RectTransform rectTransform, float right)
		{
			rectTransform.offsetMax = new Vector2(0f - right, rectTransform.offsetMax.y);
		}

		public static void SetTop(this RectTransform rectTransform, float top)
		{
			rectTransform.offsetMax = new Vector2(rectTransform.offsetMax.x, 0f - top);
		}

		public static void SetBottom(this RectTransform rectTransform, float bottom)
		{
			rectTransform.offsetMin = new Vector2(rectTransform.offsetMin.x, bottom);
		}

		public static void SetWidthAndHeight(this RectTransform rectTransform, float width, float height)
		{
			rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
			rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
		}

		public static void StretchAnchors(this RectTransform rectTransform)
		{
			RectTransform rectTransform2 = (RectTransform)rectTransform.parent;
			rectTransform.anchoredPosition = rectTransform2.position;
			rectTransform.anchorMin = new Vector2(0f, 0f);
			rectTransform.anchorMax = new Vector2(1f, 1f);
			rectTransform.pivot = new Vector2(0.5f, 0.5f);
			rectTransform.sizeDelta = rectTransform2.rect.size;
			rectTransform.SetLeft(0f);
			rectTransform.SetRight(0f);
			rectTransform.SetTop(0f);
			rectTransform.SetBottom(0f);
		}
	}
}
