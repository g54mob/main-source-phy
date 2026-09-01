using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableDropZone : MonoBehaviour
{
	public GameObject spacerRef;

	[SerializeField]
	[Tooltip("Set the AutoScrollRect to focus on the spacer when the spacer's enabled.")]
	protected bool setFocusOnSpacer;

	[SerializeField]
	[Tooltip("Used for scrolling to the spacer.")]
	protected UIAutoScrollRect autoScrollRect;

	private GameObject spacer;

	private Transform spacerTransform;

	private void Start()
	{
		SpawnNewSpacer();
	}

	public void SpawnNewSpacer()
	{
		if ((bool)spacer)
		{
			Object.Destroy(spacer);
		}
		spacer = Object.Instantiate(spacerRef, base.transform.position, base.transform.rotation, base.transform);
		spacerTransform = spacer.transform;
	}

	public int OnHoveredOver(GameObject hoveringObject)
	{
		EnableSpacer(enable: true);
		int num = 0;
		RectTransform rectTransform = hoveringObject.transform as RectTransform;
		int i = 0;
		for (int childCount = base.transform.childCount; i < childCount; i++)
		{
			RectTransform rectTransform2 = base.transform.GetChild(i) as RectTransform;
			if (rectTransform.position.y + rectTransform.sizeDelta.y * 0.25f > rectTransform2.position.y)
			{
				break;
			}
			num++;
		}
		if (spacerTransform.GetSiblingIndex() != num)
		{
			SetSpacerSiblingIndex(num);
		}
		return num;
	}

	public void EndHover()
	{
		EnableSpacer(enable: false);
	}

	public Transform GetSpacer()
	{
		EnableSpacer(enable: true);
		SetSpacerSiblingIndex(0);
		return spacerTransform;
	}

	public Transform MoveSpacerUp()
	{
		return MoveSpacer(-1);
	}

	public Transform MoveSpacerDown()
	{
		return MoveSpacer(1);
	}

	private Transform MoveSpacer(int move)
	{
		int childCount = base.transform.childCount;
		int siblingIndex = spacerTransform.GetSiblingIndex();
		siblingIndex += move;
		if (siblingIndex < 0)
		{
			siblingIndex = childCount - 1;
		}
		if (siblingIndex >= childCount)
		{
			siblingIndex = 0;
		}
		SetSpacerSiblingIndex(siblingIndex);
		return spacerTransform;
	}

	private void EnableSpacer(bool enable)
	{
		if (spacer.activeSelf != enable)
		{
			EventSystem current = EventSystem.current;
			if (!setFocusOnSpacer || current == null)
			{
				spacer.SetActive(enable);
				return;
			}
			spacer.SetActive(enable);
			autoScrollRect.SetSelectionOverride(enable ? spacer : null);
		}
	}

	private void SetSpacerSiblingIndex(int index)
	{
		spacerTransform.SetSiblingIndex(index);
		Canvas.ForceUpdateCanvases();
		if (setFocusOnSpacer)
		{
			autoScrollRect.RequestUpdate();
		}
	}
}
