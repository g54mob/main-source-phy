using UnityEngine;

namespace TFBGames
{
	public class UIScreenRect : MonoBehaviour
	{
		private RectTransform cachedRectTransform;

		private Canvas parentCanvas;

		private Rect screenRect;

		private Vector3 lastPosition;

		private Rect lastRect;

		private int lastChildCount = -1;

		public Rect ScreenRect
		{
			get
			{
				UpdateScreenRect();
				return screenRect;
			}
		}

		private void Awake()
		{
			cachedRectTransform = base.transform as RectTransform;
			parentCanvas = GetComponentInParent<Canvas>();
		}

		private void OnEnable()
		{
			UpdateScreenRect();
		}

		private void UpdateScreenRect()
		{
			int childCount = cachedRectTransform.childCount;
			Vector3 position = cachedRectTransform.position;
			Rect rect = cachedRectTransform.rect;
			if (lastChildCount != childCount || !Mathf.Approximately(lastPosition.x, position.x) || !Mathf.Approximately(lastPosition.y, position.y) || !Mathf.Approximately(lastPosition.z, position.z) || !Mathf.Approximately(lastRect.yMin, rect.yMin) || !Mathf.Approximately(lastRect.xMin, rect.xMin) || !Mathf.Approximately(lastRect.yMax, rect.yMax) || !Mathf.Approximately(lastRect.xMax, rect.xMax))
			{
				lastChildCount = childCount;
				lastPosition = position;
				lastRect = rect;
				screenRect = GetScreenCoordinates(cachedRectTransform, parentCanvas);
			}
		}

		private static Rect GetScreenCoordinates(RectTransform rectTransform, Canvas canvas)
		{
			Vector3[] array = new Vector3[4];
			rectTransform.GetWorldCorners(array);
			if (canvas != null && canvas.worldCamera != null)
			{
				for (int i = 0; i < 4; i++)
				{
					array[i] = canvas.worldCamera.WorldToScreenPoint(array[i]);
				}
			}
			return new Rect(array[0].x, array[0].y, array[2].x - array[0].x, array[2].y - array[0].y);
		}
	}
}
