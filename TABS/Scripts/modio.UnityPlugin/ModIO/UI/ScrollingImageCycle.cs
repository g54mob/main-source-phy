using UnityEngine;
using UnityEngine.UI;

namespace ModIO.UI
{
	public class ScrollingImageCycle : MonoBehaviour
	{
		public float pixelsPerSecond = 10f;

		public float secondsBetweenRepeat = 1f;

		private float secondsUntilScroll;

		private void OnEnable()
		{
			RectTransform rectTransform = base.transform as RectTransform;
			Vector2 anchoredPosition = rectTransform.anchoredPosition;
			anchoredPosition.x = rectTransform.rect.width * -1f;
			rectTransform.anchoredPosition = anchoredPosition;
		}

		private void Update()
		{
			if (secondsUntilScroll <= 0f)
			{
				RectTransform rectTransform = base.transform as RectTransform;
				Rect rect = ((RectTransform)rectTransform.parent).rect;
				Vector2 anchoredPosition = rectTransform.anchoredPosition;
				anchoredPosition.x += pixelsPerSecond * Time.unscaledDeltaTime;
				if (rect.width < anchoredPosition.x)
				{
					secondsUntilScroll = secondsBetweenRepeat;
					anchoredPosition.x = rectTransform.rect.width * -1f;
					base.gameObject.GetComponent<Graphic>().enabled = false;
				}
				rectTransform.anchoredPosition = anchoredPosition;
			}
			else
			{
				secondsUntilScroll -= Time.unscaledDeltaTime;
				if (secondsUntilScroll <= 0f)
				{
					base.gameObject.GetComponent<Graphic>().enabled = true;
				}
			}
		}
	}
}
