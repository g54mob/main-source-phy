using UnityEngine;

[RequireComponent(typeof(Canvas), typeof(CanvasGroup))]
public class CanvasGroupHandler : MonoBehaviour
{
	private CanvasGroup canvasGroup;

	private bool _interactable;

	private bool _blocksRaycasts;

	private bool isZoomDisablingInteractable;

	private bool izZoomDisablingBlocksRaycasts;

	public bool interactable
	{
		get
		{
			return _interactable;
		}
		set
		{
			_interactable = value;
			updateState();
		}
	}

	public bool blocksRaycasts
	{
		get
		{
			return _blocksRaycasts;
		}
		set
		{
			_blocksRaycasts = value;
			updateState();
		}
	}

	private void updateState()
	{
		if (canvasGroup != null)
		{
			canvasGroup.interactable = !isZoomDisablingInteractable && interactable;
			canvasGroup.blocksRaycasts = !izZoomDisablingBlocksRaycasts && blocksRaycasts;
		}
	}

	private bool isAncester(Transform zoomObject)
	{
		return isAncester(zoomObject, base.transform);
		static bool isAncester(Transform transform, Transform parent)
		{
			if (parent == null || transform == null || parent.transform.parent == null)
			{
				return false;
			}
			if (transform == parent.transform)
			{
				return true;
			}
			return isAncester(transform, parent.transform.parent);
		}
	}

	public void init(CanvasGroup _canvasGroup)
	{
		canvasGroup = _canvasGroup;
		_interactable = canvasGroup.interactable;
		_blocksRaycasts = canvasGroup.blocksRaycasts;
	}

	public void zoomDisableInteraction(Transform zoomObject)
	{
		if (!isAncester(zoomObject))
		{
			isZoomDisablingInteractable = true;
			izZoomDisablingBlocksRaycasts = true;
			updateState();
		}
	}

	public void zoomEnableInteraction()
	{
		isZoomDisablingInteractable = false;
		izZoomDisablingBlocksRaycasts = false;
		updateState();
	}
}
