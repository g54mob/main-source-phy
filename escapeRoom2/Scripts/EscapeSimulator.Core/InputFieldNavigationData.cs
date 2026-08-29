using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

internal class InputFieldNavigationData
{
	public static string currentFocusedInputFieldName;

	public bool isInputFieldFocused;

	private readonly List<InputField> currentPropertyInputFields = new List<InputField>();

	public void update(bool active)
	{
		InputField inputField = currentPropertyInputFields.Find((InputField f) => f.gameObject.name == currentFocusedInputFieldName);
		if (active && inputField != null)
		{
			foreach (InputField currentPropertyInputField in currentPropertyInputFields)
			{
				if (!currentPropertyInputField.isFocused && currentPropertyInputField.name == currentFocusedInputFieldName)
				{
					currentPropertyInputField.ActivateInputField();
				}
			}
		}
		isInputFieldFocused = inputField != null && inputField.isFocused;
		if (isInputFieldFocused && Input.GetKeyDown(KeyCode.Tab) && EventSystem.current.currentSelectedGameObject.TryGetComponent<InputField>(out var component))
		{
			Selectable selectable = (RoomEditor.isCtrlPressed() ? component.FindSelectableOnUp() : component.FindSelectableOnDown());
			if (selectable != null && selectable.TryGetComponent<InputField>(out var component2))
			{
				component.DeactivateInputField();
				currentFocusedInputFieldName = component2.name;
			}
		}
	}

	public void removeOnEndEditListeners()
	{
		foreach (InputField currentPropertyInputField in currentPropertyInputFields)
		{
			currentPropertyInputField.onEndEdit.RemoveAllListeners();
		}
	}

	public void initNavigation(Transform parent, string windowName)
	{
		bool activeSelf = parent.gameObject.activeSelf;
		parent.gameObject.SetActive(value: true);
		InputField[] componentsInChildren = parent.GetComponentsInChildren<InputField>(includeInactive: false);
		parent.gameObject.SetActive(activeSelf);
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].name = $"{windowName}-{componentsInChildren[i].name}-{i}";
		}
		currentPropertyInputFields.Clear();
		currentPropertyInputFields.AddRange(componentsInChildren);
		for (int j = 0; j < componentsInChildren.Length; j++)
		{
			InputField obj = componentsInChildren[j];
			InputField selectOnUp = ((j == 0) ? componentsInChildren[^1] : componentsInChildren[j - 1]);
			InputField selectOnDown = ((j == componentsInChildren.Length - 1) ? componentsInChildren[0] : componentsInChildren[j + 1]);
			Navigation navigation = obj.navigation;
			navigation.mode = Navigation.Mode.Explicit;
			navigation.selectOnDown = selectOnDown;
			navigation.selectOnUp = selectOnUp;
			obj.navigation = navigation;
		}
	}

	public void clear()
	{
		isInputFieldFocused = false;
		currentFocusedInputFieldName = "";
		currentPropertyInputFields.Clear();
	}
}
