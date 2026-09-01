using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitEditorItemEquipButton : MonoBehaviour
{
	[SerializeField]
	[Tooltip("Button whose interactable is disabled when the equip button is made active.")]
	protected Button button;

	private void OnEnable()
	{
		GameObject currentSelection = GetCurrentSelection();
		button.interactable = false;
		if (currentSelection == button.gameObject)
		{
			SetCurrentSelection(base.gameObject);
		}
	}

	private void OnDisable()
	{
		GameObject currentSelection = GetCurrentSelection();
		button.interactable = true;
		if (currentSelection == base.gameObject)
		{
			SetCurrentSelection(button.gameObject);
		}
	}

	private GameObject GetCurrentSelection()
	{
		EventSystem current = EventSystem.current;
		if (!(current != null))
		{
			return null;
		}
		return current.currentSelectedGameObject;
	}

	private void SetCurrentSelection(GameObject newSelection)
	{
		EventSystem current = EventSystem.current;
		if (current != null)
		{
			current.SetSelectedGameObject(newSelection);
		}
	}
}
