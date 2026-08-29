using UnityEngine;
using UnityEngine.EventSystems;

namespace SimpleFileBrowser
{
	public class FileBrowserMovement : MonoBehaviour
	{
		private FileBrowser fileBrowser;

		private RectTransform canvasTR;

		private Camera canvasCam;

		[SerializeField]
		private RectTransform window;

		[SerializeField]
		private RecycledListView listView;

		private Vector2 initialTouchPos = Vector2.zero;

		private Vector2 initialAnchoredPos;

		private Vector2 initialSizeDelta;

		public void Initialize(FileBrowser fileBrowser)
		{
			this.fileBrowser = fileBrowser;
			canvasTR = fileBrowser.GetComponent<RectTransform>();
		}

		public void OnDragStarted(BaseEventData data)
		{
			PointerEventData pointerEventData = (PointerEventData)data;
			canvasCam = pointerEventData.pressEventCamera;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(window, pointerEventData.pressPosition, canvasCam, out initialTouchPos);
		}

		public void OnDrag(BaseEventData data)
		{
			PointerEventData pointerEventData = (PointerEventData)data;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(window, pointerEventData.position, canvasCam, out var localPoint);
			window.anchoredPosition += localPoint - initialTouchPos;
		}

		public void OnEndDrag(BaseEventData data)
		{
			fileBrowser.EnsureWindowIsWithinBounds();
		}

		public void OnResizeStarted(BaseEventData data)
		{
			PointerEventData pointerEventData = (PointerEventData)data;
			canvasCam = pointerEventData.pressEventCamera;
			initialAnchoredPos = window.anchoredPosition;
			initialSizeDelta = window.sizeDelta;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasTR, pointerEventData.pressPosition, canvasCam, out initialTouchPos);
		}

		public void OnResize(BaseEventData data)
		{
			PointerEventData pointerEventData = (PointerEventData)data;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasTR, pointerEventData.position, canvasCam, out var localPoint);
			Vector2 vector = localPoint - initialTouchPos;
			Vector2 vector2 = initialSizeDelta + new Vector2(vector.x, 0f - vector.y);
			Vector2 sizeDelta = canvasTR.sizeDelta;
			if (vector2.x < (float)fileBrowser.minWidth)
			{
				vector2.x = fileBrowser.minWidth;
			}
			if (vector2.y < (float)fileBrowser.minHeight)
			{
				vector2.y = fileBrowser.minHeight;
			}
			if (vector2.x > sizeDelta.x)
			{
				vector2.x = sizeDelta.x;
			}
			if (vector2.y > sizeDelta.y)
			{
				vector2.y = sizeDelta.y;
			}
			vector2.x = (int)vector2.x;
			vector2.y = (int)vector2.y;
			vector = vector2 - initialSizeDelta;
			window.anchoredPosition = initialAnchoredPos + new Vector2(vector.x * 0.5f, vector.y * -0.5f);
			if (window.sizeDelta != vector2)
			{
				window.sizeDelta = vector2;
				fileBrowser.OnWindowDimensionsChanged(vector2);
			}
			listView.OnViewportDimensionsChanged();
		}

		public void OnEndResize(BaseEventData data)
		{
			fileBrowser.EnsureWindowIsWithinBounds();
		}
	}
}
