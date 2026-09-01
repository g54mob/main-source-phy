using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_CustomShapesLibrary : MonoBehaviour
{
	public Panel_FileLoader m_FileLoader;

	public TextMeshProUGUI m_NoSavesText;

	public Button m_Cancel;

	private static FileSlot m_SlotToDelete;

	private static FileSlot m_SlotToRename;

	private static string m_SlotRenameNewName;

	private void Awake()
	{
		m_Cancel.onClick.AddListener(Close);
	}

	private void Update()
	{
		ProcessInput();
		m_NoSavesText.gameObject.SetActive(m_FileLoader.NumSlots() == 0);
		if (ActivePanels.IsTopPanel(base.gameObject))
		{
			ShowGamepadLegend();
		}
	}

	private void OnEnable()
	{
		if ((bool)Prefabs.m_Instance)
		{
			PopulateSlots();
			Sort();
		}
		m_NoSavesText.gameObject.SetActive(m_FileLoader.NumSlots() == 0);
		ActivePanels.Add(base.gameObject);
		ShowGamepadLegend();
	}

	private void OnDisable()
	{
		ActivePanels.Remove(base.gameObject);
	}

	private void Close()
	{
		InterfaceAudio.Play("ui_window_close");
		base.gameObject.SetActive(value: false);
	}

	private void PopulateSlots()
	{
		m_FileLoader.DestroySlots();
		foreach (KeyValuePair<string, CustomShapesLibrarySlot> localSlot in CustomShapesLibrary.m_LocalSlots)
		{
			CustomShapesLibrarySlot value = localSlot.Value;
			FileSlot fileSlot = m_FileLoader.AddSlot(value.m_FullyQualifiedPath, 0L, Localize.Get(value.m_DisplayNamLocID), SlotClickedCallback, SlotHoverCallback);
			if ((bool)fileSlot)
			{
				fileSlot.m_InfoButton.gameObject.SetActive(value: false);
				fileSlot.SetOnDeleteCallback(SlotDeleteCallback);
				fileSlot.SetOnRenameCallback(SlotRenameCallback);
			}
		}
	}

	private void SlotHoverCallback(FileSlot slot, bool hover)
	{
	}

	private void SlotClickedCallback(FileSlot slot)
	{
	}

	private void SlotDeleteCallback(FileSlot slot)
	{
		m_SlotToDelete = slot;
		PopUpMessage.DisplayWarning(Localize.Get("POPUP_DELETE_SLOT", m_SlotToDelete.m_DisplayName.text, null), useYesNoLables: false, DeleteSaveSlotCallback);
	}

	private void SlotRenameCallback(FileSlot slot)
	{
		m_SlotToRename = slot;
		PopupInputField.Display(Localize.Get("POPUP_RENAME_SLOT", m_SlotToRename.m_DisplayName.text), m_SlotToRename.m_DisplayName.text, isFilename: true, isDirectory: true, RenameSaveSlot);
	}

	public void RenameSaveSlot(string newName)
	{
		if (string.IsNullOrEmpty(newName))
		{
			return;
		}
		newName = newName.Trim();
		if (string.IsNullOrEmpty(newName) || Utils.HasInvalidFileNameChars(newName))
		{
			PopUpMessage.DisplayWarningOkOnly(Localize.Get("UI_INVALID_FILENAME", newName));
			return;
		}
		CustomShapesLibrarySlot slotByFullPath = CustomShapesLibrary.GetSlotByFullPath(m_SlotToRename.m_FileName);
		if (slotByFullPath != null)
		{
			slotByFullPath.m_DisplayNamLocID = newName;
			CustomShapesLibrarySlotProxy customShapesLibrarySlotProxy = CustomShapesLibrary.LoadSlotInfo(m_SlotToRename.m_FileName);
			if (customShapesLibrarySlotProxy != null)
			{
				CustomShapesLibrary.SaveSlotInfo(m_SlotToRename.m_FileName, newName, customShapesLibrarySlotProxy.m_IconFilename, customShapesLibrarySlotProxy.m_PrefabAddress);
			}
			GameUI.m_Instance.m_SandboxCreateObjects.PopulateMyCustomShapes();
			m_SlotToRename.m_DisplayName.text = Localize.Get(newName);
			Sort();
			m_SlotToRename = null;
		}
	}

	private void DeleteSaveSlotCallback()
	{
		if ((bool)m_SlotToDelete)
		{
			if (!CustomShapesLibrary.DeleteLocalLibrarySlot(m_SlotToDelete.m_FileName))
			{
				PopUpMessage.DisplayErrorOkOnly(Localize.Get("UI_CUSTOM_SHAPE_LIBRARY_DELETE_FAIL"));
			}
			else
			{
				GameUI.m_Instance.m_SandboxCreateObjects.RemoveMyCustomShape(m_SlotToDelete.m_FileName);
				PopulateSlots();
				Sort();
			}
			m_SlotToDelete = null;
		}
	}

	private void Sort()
	{
		m_FileLoader.SortAlphabetically();
	}

	private void ProcessInput()
	{
		if (!GameStateCommonInput.IgnoreKeyboardInputForPanel(base.gameObject) && (Input.GetKeyDown(KeyCode.Escape) || GamepadManager.ButtonJustPressed(GamepadButtonType.EAST)))
		{
			Close();
		}
	}

	private void ShowGamepadLegend()
	{
		GameUI.m_Instance.m_GamepadLegend.HideButtonsLeft();
		GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, Localize.Get("UI_SELECT"), GamepadButtonType.EAST, Localize.Get("UI_CLOSE"));
	}
}
