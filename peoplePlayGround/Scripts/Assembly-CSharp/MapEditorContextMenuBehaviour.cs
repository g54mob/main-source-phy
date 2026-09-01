using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapEditorContextMenuBehaviour : MonoBehaviour
{
	public Button DeleteButton;

	public Button CopyButton;

	public Button PasteButton;

	public Button PropertiesButton;

	private List<MapEditorSelectable> selectablesWhenShown = new List<MapEditorSelectable>();

	private List<MapEditorObjectBehaviour> selectedObjects = new List<MapEditorObjectBehaviour>();

	public bool IsShown => base.gameObject.activeSelf;

	private void Start()
	{
		DeleteButton.onClick.AddListener(delegate
		{
			DeleteAction();
		});
		Hide();
	}

	private void Update()
	{
		if (IsShown)
		{
			bool interactable = selectedObjects.Count != 0;
			DeleteButton.interactable = interactable;
			CopyButton.interactable = interactable;
			PasteButton.interactable = false;
		}
	}

	public void Show(Vector2 screenPosition)
	{
		RectTransform rectTransform = GetComponent<RectTransform>();
		CanvasScaler canvasScaler = Object.FindObjectOfType<CanvasScaler>();
		base.transform.position = Vector3.zero;
		rectTransform.ForceUpdateRectTransforms();
		LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
		rectTransform.ForceUpdateRectTransforms();
		clampToScreen(screenPosition);
		selectablesWhenShown.Clear();
		selectablesWhenShown.AddRange(MapEditorSelectionManager.Instance.CurrentlySelected);
		selectedObjects.Clear();
		foreach (MapEditorSelectable item in selectablesWhenShown)
		{
			if ((bool)item && item.TryGetComponent<MapEditorObjectBehaviour>(out var component))
			{
				selectedObjects.Add(component);
			}
		}
		base.gameObject.SetActive(value: true);
		void clampToScreen(Vector2 vector)
		{
			base.transform.position = vector;
			Vector2 vector2 = rectTransform.sizeDelta * 2f / (canvasScaler.referenceResolution.x / Mathf.Lerp(Screen.width, Screen.height, canvasScaler.matchWidthOrHeight));
			vector.x = Mathf.Clamp(vector.x, 0f, (float)Screen.width - vector2.x);
			vector.y = Mathf.Clamp(vector.y, vector2.y, Screen.height);
			base.transform.position = vector;
		}
	}

	public void DeleteAction()
	{
		foreach (MapEditorObjectBehaviour selectedObject in selectedObjects)
		{
			selectedObject.Delete();
		}
		Hide();
	}

	public void Hide()
	{
		base.gameObject.SetActive(value: false);
	}
}
