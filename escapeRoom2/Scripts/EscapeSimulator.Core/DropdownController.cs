using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Dropdown))]
public class DropdownController : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
{
	[Tooltip("Indexes that should be ignored. Indexes are 0 based.")]
	public List<int> indexesToDisable = new List<int>();

	public void OnPointerClick(PointerEventData eventData)
	{
		Transform transform = GetComponentInChildren<Canvas>().transform;
		if (!transform)
		{
			return;
		}
		Transform transform2 = transform.Find("Viewport/Content");
		for (int i = 1; i < transform2.childCount; i++)
		{
			if (transform2.GetChild(i).TryGetComponent<Toggle>(out var component))
			{
				component.interactable = !indexesToDisable.Contains(i - 1);
			}
		}
	}
}
