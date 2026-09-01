using UnityEngine;

namespace ModIO.UI
{
	[RequireComponent(typeof(RectTransform))]
	public class SetRectTransformOnEnable : MonoBehaviour
	{
		public bool setAnchorMin;

		public Vector2 anchorMin = Vector2.zero;

		public bool setAnchorMax;

		public Vector2 anchorMax = Vector2.zero;

		public bool setOffsetMin;

		public Vector2 offsetMin = Vector2.zero;

		public bool setOffsetMax;

		public Vector2 offsetMax = Vector2.zero;

		public bool setPivot;

		public Vector2 pivot = Vector2.zero;

		public bool setAnchoredPos;

		public Vector2 anchoredPos = Vector2.zero;

		private void OnEnable()
		{
			RectTransform rectTransform = (RectTransform)base.transform;
			if (setAnchorMin)
			{
				rectTransform.anchorMin = anchorMin;
			}
			if (setAnchorMax)
			{
				rectTransform.anchorMax = anchorMax;
			}
			if (setPivot)
			{
				rectTransform.pivot = pivot;
			}
			if (setAnchoredPos)
			{
				rectTransform.anchoredPosition = anchoredPos;
			}
			if (setOffsetMin)
			{
				rectTransform.offsetMin = offsetMin;
			}
			if (setOffsetMax)
			{
				rectTransform.offsetMax = offsetMax;
			}
		}
	}
}
