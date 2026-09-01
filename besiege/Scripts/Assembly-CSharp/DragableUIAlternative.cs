using UnityEngine;
using UnityEngine.EventSystems;

public class DragableUIAlternative : UIBehaviour, IBeginDragHandler, IDragHandler, IEventSystemHandler
{
	public RectTransform dragObject;

	protected Vector3 dragMouseOffset = Vector3.zero;

	protected Vector2 minDiff;

	protected Vector2 maxDiff;

	private Vector3 originalPanelPosition;

	private RectTransform dragObjectInternal
	{
		get
		{
			if (dragObject == null)
			{
				return base.transform as RectTransform;
			}
			return dragObject;
		}
	}

	public void OnBeginDrag(PointerEventData data)
	{
		CalculateBounds();
		originalPanelPosition = dragObjectInternal.transform.position;
		Vector3 vector = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);
		dragMouseOffset = vector - originalPanelPosition;
	}

	public void OnDrag(PointerEventData data)
	{
		Vector3 vector = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);
		Vector3 pos = vector - dragMouseOffset;
		pos = ClampInMoveArea(pos);
		dragObjectInternal.transform.position = pos;
	}

	protected void CalculateBounds()
	{
		Vector3 position = dragObjectInternal.transform.position;
		Vector3[] array = new Vector3[4];
		dragObjectInternal.GetWorldCorners(array);
		minDiff = new Vector2(position.x - array[0].x, position.y - array[0].y);
		maxDiff = new Vector2(position.x - array[2].x, position.y - array[2].y);
	}

	private Vector3 ClampInMoveArea(Vector3 pos)
	{
		return new Vector3(Mathf.Clamp(pos.x, minDiff.x, (float)Screen.width + maxDiff.x), Mathf.Clamp(pos.y, minDiff.y, (float)Screen.height + maxDiff.y), pos.z);
	}
}
