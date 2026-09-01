using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_SaveLoadSandboxLayout : MonoBehaviour
{
	[Header("Header")]
	public Button m_OpenDirButton;

	public Button m_OpenDirFromPathButton;

	public Button m_BackButton;

	public Button m_BackButtonDisabled;

	public Panel_FileLoader m_FileLoader;

	public TextMeshProUGUI m_CurrentDirectoryText;

	protected FileSlot m_SlotToSaveLoad;

	protected FileSlot m_SlotToDelete;

	protected FileSlot m_SlotToRename;

	protected readonly float SLOT_HEIGHT = 34f;

	protected readonly int MAX_VISIBLE_SLOTS = 10;

	protected int m_SelectedSlotIndex;

	protected bool m_IgnoreFirstHoverChange;

	protected List<string> m_SubDirectoriesOpened = new List<string>();

	public void InitDirectoryButtons()
	{
		m_OpenDirButton.onClick.AddListener(OpenDir);
		m_OpenDirFromPathButton.onClick.AddListener(OpenDir);
		m_BackButton.onClick.AddListener(OnBackDir);
	}

	public void OnCreateNewDir()
	{
		PopupInputField.Display(Localize.Get("UI_INPUTFIELD_NEW_DIR_NAME"), string.Empty, isFilename: false, isDirectory: true, CreateNewDir);
	}

	public void CreateNewDir(string name)
	{
		if (string.IsNullOrEmpty(name))
		{
			return;
		}
		name = name.Trim();
		if (string.IsNullOrEmpty(name) || Utils.HasInvalidPathChars(name))
		{
			PopUpMessage.DisplayWarningOkOnly(Localize.Get("UI_INVALID_FOLDERNAME", name));
			return;
		}
		string path = Path.Combine(GetSandboxLayoutSavePathForActiveProfile(), name);
		if (Directory.Exists(path))
		{
			PopUpMessage.Display(Localize.Get("POPUP_CREATE_DIR_EXISTS", name), null);
			return;
		}
		Utils.CreateDirectory(path);
		m_SubDirectoriesOpened.Add(name);
		PopulateSlots();
	}

	protected void SlotHoverCallback(FileSlot slot, bool hover)
	{
		if (!hover)
		{
			return;
		}
		if (!m_IgnoreFirstHoverChange)
		{
			SetSelectedSlot(slot);
			if (GameInput.GetActiveGameDevice() != GameDevice.Gamepad)
			{
				InterfaceAudio.Play("ui_menu_hover");
			}
		}
		m_IgnoreFirstHoverChange = false;
	}

	protected void SetSelectedSlot(FileSlot slot)
	{
		m_SelectedSlotIndex = m_FileLoader.GetSlotIndex(slot);
		m_FileLoader.SelectSlot(slot);
	}

	protected void SlotRenameCallback(FileSlot slot)
	{
		m_SlotToRename = slot;
		if (m_SlotToRename.m_IsDirectory)
		{
			DirectoryRenameCallback();
		}
		else
		{
			PopupInputField.Display(Localize.Get("POPUP_RENAME_SLOT", m_SlotToRename.m_DisplayName.text), m_SlotToRename.m_DisplayName.text, isFilename: true, isDirectory: false, RenameAfterConfirmation);
		}
	}

	protected void SlotDeleteCallback(FileSlot slot)
	{
		m_SlotToDelete = slot;
		if (m_SlotToDelete.m_IsDirectory)
		{
			DirectoryDeleteCallback();
		}
		else
		{
			PopUpMessage.DisplayWarning(Localize.Get("POPUP_DELETE_SLOT", m_SlotToDelete.m_DisplayName.text), useYesNoLables: true, DeleteAfterConfirmation);
		}
	}

	protected void DeleteAfterConfirmation()
	{
		string text = Path.Combine(GetSandboxLayoutSavePathForActiveProfile(), m_SlotToDelete.m_FileName);
		Utils.DeleteFile(text);
		if (m_SlotToRename == null)
		{
			MaybeDeleteCurrentLayoutTextUI(text);
		}
		m_FileLoader.DeleteSlot(m_SlotToDelete);
		m_SlotToDelete = null;
	}

	protected void RenameAfterConfirmation(string name)
	{
		if (string.IsNullOrEmpty(name))
		{
			return;
		}
		name = name.Trim();
		if (string.IsNullOrEmpty(name) || Utils.HasInvalidFileNameChars(name))
		{
			PopUpMessage.DisplayWarningOkOnly(Localize.Get("UI_INVALID_FILENAME", name));
			m_SlotToRename = null;
			return;
		}
		string sandboxLayoutSavePathForActiveProfile = GetSandboxLayoutSavePathForActiveProfile();
		string text = Path.Combine(sandboxLayoutSavePathForActiveProfile, m_SlotToRename.m_FileName);
		string text2 = Path.Combine(sandboxLayoutSavePathForActiveProfile, (Path.GetExtension(name) == SandboxLayout.SAVE_EXTENSION) ? name : (name + SandboxLayout.SAVE_EXTENSION));
		m_SlotToDelete = (Utils.FileExists(text2) ? m_FileLoader.FindSlotByDisplayName(Path.GetFileNameWithoutExtension(name)) : null);
		if ((bool)m_SlotToDelete)
		{
			PopUpMessage.DisplayConfirmation(Localize.Get("POPUP_OVERWRITE_SLOT", name), useYesNoLabels: false, DeleteAndRenameAfterConfirmation);
			return;
		}
		Utils.RenameFile(text, text2);
		MaybeRenameCurrentLayoutTextUI(text, text2);
		m_SlotToRename.m_FileName = Path.GetFileName(text2);
		m_SlotToRename.m_DisplayName.text = name;
		m_SlotToRename = null;
	}

	protected void DeleteAndRenameAfterConfirmation()
	{
		string text = Sandbox.m_CurrentLayoutName;
		if (Sandbox.m_CurrentLayoutName == MaybeAddSubdirectoriesToName(m_SlotToRename.m_DisplayName.text))
		{
			text = m_SlotToDelete.m_DisplayName.text;
		}
		else if (Sandbox.m_CurrentLayoutName == MaybeAddSubdirectoriesToName(m_SlotToDelete.m_DisplayName.text))
		{
			text = "";
		}
		string text2 = m_SlotToDelete.m_DisplayName.text;
		DeleteAfterConfirmation();
		RenameAfterConfirmation(text2);
		Sandbox.m_CurrentLayoutName = MaybeAddSubdirectoriesToName(text);
	}

	protected void ScrollDown()
	{
		if (m_SelectedSlotIndex == 0)
		{
			FileSlot fileSlot = m_FileLoader.FindSlotByIndex(0);
			if (fileSlot != null && !fileSlot.m_SelectedHighlight.gameObject.activeInHierarchy)
			{
				fileSlot.m_SelectedHighlight.gameObject.SetActive(value: true);
				InterfaceAudio.Play("ui_menu_hover");
				return;
			}
		}
		m_SelectedSlotIndex++;
		if (m_SelectedSlotIndex >= m_FileLoader.NumSlots())
		{
			m_SelectedSlotIndex = m_FileLoader.NumSlots() - 1;
			InterfaceAudio.PlayErrorBeep();
		}
		else
		{
			SetSelectedSlot(m_FileLoader.FindSlotByIndex(m_SelectedSlotIndex));
			InterfaceAudio.Play("ui_menu_hover");
			MaybeAutoScroll();
		}
	}

	protected void ScrollUp()
	{
		if (m_SelectedSlotIndex == 0)
		{
			FileSlot fileSlot = m_FileLoader.FindSlotByIndex(0);
			if (fileSlot != null && !fileSlot.m_SelectedHighlight.gameObject.activeInHierarchy)
			{
				fileSlot.m_SelectedHighlight.gameObject.SetActive(value: true);
				InterfaceAudio.Play("ui_menu_hover");
				return;
			}
		}
		m_SelectedSlotIndex--;
		if (m_SelectedSlotIndex < 0)
		{
			m_SelectedSlotIndex = 0;
			InterfaceAudio.PlayErrorBeep();
		}
		else
		{
			SetSelectedSlot(m_FileLoader.FindSlotByIndex(m_SelectedSlotIndex));
			InterfaceAudio.Play("ui_menu_hover");
			MaybeAutoScroll();
		}
	}

	protected void MaybeAutoScroll()
	{
		float num = m_FileLoader.m_Content.GetComponent<RectTransform>().anchoredPosition.y / SLOT_HEIGHT;
		float num2 = (float)(m_SelectedSlotIndex + 1) - num;
		if (num2 < 1f)
		{
			m_FileLoader.m_Content.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, SLOT_HEIGHT * (float)m_SelectedSlotIndex);
			m_IgnoreFirstHoverChange = true;
		}
		else if (num2 > (float)MAX_VISIBLE_SLOTS)
		{
			m_FileLoader.m_Content.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, SLOT_HEIGHT * (float)(m_SelectedSlotIndex + 1 - MAX_VISIBLE_SLOTS));
			m_IgnoreFirstHoverChange = true;
		}
	}

	protected void SelectFirstSlot()
	{
		m_SelectedSlotIndex = 0;
		m_SelectedSlotIndex = m_FileLoader.GetSlotIndex(m_FileLoader.GetFirstSlot());
	}

	protected void Close()
	{
		InterfaceAudio.Play("ui_window_close");
		base.gameObject.SetActive(value: false);
	}

	protected virtual void PopulateSlots()
	{
	}

	protected void PopulateSaveLoadSlots(string profileName, FileSlot.OnClickedDelegate onClickCallback)
	{
		m_FileLoader.DestroySlots();
		DirectoryInfo currentDirectoryInfo = GetCurrentDirectoryInfo(profileName);
		if (currentDirectoryInfo == null)
		{
			return;
		}
		DirectoryInfo[] directories = currentDirectoryInfo.GetDirectories();
		foreach (DirectoryInfo directoryInfo in directories)
		{
			FileSlot fileSlot = m_FileLoader.AddSlot(directoryInfo.Name, directoryInfo.LastWriteTime.Ticks, directoryInfo.Name, onClickCallback, SlotHoverCallback);
			if ((bool)fileSlot)
			{
				fileSlot.m_IsDirectory = true;
				fileSlot.SetOnDeleteCallback(SlotDeleteCallback);
				fileSlot.SetOnRenameCallback(SlotRenameCallback);
				fileSlot.m_AsteriskIcon.gameObject.SetActive(value: true);
			}
		}
		FileInfo[] files = currentDirectoryInfo.GetFiles("*" + SandboxLayout.SAVE_EXTENSION);
		foreach (FileInfo fileInfo in files)
		{
			FileSlot fileSlot2 = m_FileLoader.AddSlot(fileInfo.Name, fileInfo.LastWriteTime.Ticks, Path.GetFileNameWithoutExtension(fileInfo.Name), onClickCallback, SlotHoverCallback);
			if ((bool)fileSlot2)
			{
				fileSlot2.SetOnDeleteCallback(SlotDeleteCallback);
				fileSlot2.SetOnRenameCallback(SlotRenameCallback);
			}
		}
	}

	protected void ProcessInput()
	{
		if (!GameStateCommonInput.IgnoreKeyboardInputForPanel(base.gameObject))
		{
			if (Input.GetKeyDown(KeyCode.Escape) || GamepadManager.ButtonJustPressed(GamepadButtonType.EAST))
			{
				Close();
			}
			MaybeDoEnterReturnInput();
			if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
			{
				ScrollUp();
				GameUI.m_NextAutoScrollTime = Time.unscaledTime + GameUI.AUTOSCROLL_START_DELAY;
			}
			if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
			{
				ScrollDown();
				GameUI.m_NextAutoScrollTime = Time.unscaledTime + GameUI.AUTOSCROLL_START_DELAY;
			}
			if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && Time.unscaledTime > GameUI.m_NextAutoScrollTime)
			{
				ScrollUp();
				GameUI.m_NextAutoScrollTime = Time.unscaledTime + GameUI.AUTOSCROLL_DELAY;
			}
			if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && Time.unscaledTime > GameUI.m_NextAutoScrollTime)
			{
				ScrollDown();
				GameUI.m_NextAutoScrollTime = Time.unscaledTime + GameUI.AUTOSCROLL_DELAY;
			}
			if (GamepadManager.ButtonJustPressed(GamepadButtonType.DPAD_UP) || GamepadRepeater.JustRepeated(GamepadButtonType.DPAD_UP))
			{
				ScrollUp();
				ForceGamepadCursorToSelecctedSlot();
			}
			if (GamepadManager.ButtonJustPressed(GamepadButtonType.DPAD_DOWN) || GamepadRepeater.JustRepeated(GamepadButtonType.DPAD_DOWN))
			{
				ScrollDown();
				ForceGamepadCursorToSelecctedSlot();
			}
			MaybeDoLeftRightInput();
		}
	}

	protected virtual void MaybeDoEnterReturnInput()
	{
	}

	protected virtual void MaybeDoLeftRightInput()
	{
		if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
		{
			GoToParentDirectory();
			InterfaceAudio.Play("ui_menu_select");
		}
		if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && m_SelectedSlotIndex != -1)
		{
			FileSlot fileSlot = m_FileLoader.FindSlotByIndex(m_SelectedSlotIndex);
			if (fileSlot != null && fileSlot.m_IsDirectory)
			{
				DirectorySlotClickedCallback(fileSlot);
			}
		}
	}

	protected string GetSandboxLayoutSavePathForProfile(string profileName)
	{
		string text = SandboxLayout.GetSavePath(profileName);
		for (int i = 0; i < m_SubDirectoriesOpened.Count; i++)
		{
			text = Path.Combine(text, m_SubDirectoriesOpened[i]);
		}
		return text;
	}

	protected string GetSandboxLayoutSavePathForActiveProfile()
	{
		return GetSandboxLayoutSavePathForProfile(Profiles.GetActiveProfileName());
	}

	protected DirectoryInfo GetCurrentDirectoryInfo(string profileName)
	{
		string sandboxLayoutSavePathForProfile = GetSandboxLayoutSavePathForProfile(profileName);
		if (!Directory.Exists(sandboxLayoutSavePathForProfile))
		{
			return null;
		}
		return new DirectoryInfo(sandboxLayoutSavePathForProfile);
	}

	protected void DirectoryRenameCallback()
	{
		PopupInputField.Display(Localize.Get("POPUP_RENAME_DIR", m_SlotToRename.m_FileName), Utils.RemoveInvalidCharsFromPath(m_SlotToRename.m_DisplayName.text), isFilename: false, isDirectory: true, RenameDirAfterConfirmation);
	}

	protected void RenameDirAfterConfirmation(string newName)
	{
		if (base.name == null)
		{
			return;
		}
		base.name = base.name.Trim();
		if (Utils.HasInvalidPathChars(base.name))
		{
			PopUpMessage.DisplayWarningOkOnly(Localize.Get("UI_INVALID_FOLDERNAME", base.name));
			m_SlotToRename = null;
			return;
		}
		string text = Path.Combine(GetSandboxLayoutSavePathForActiveProfile(), m_SlotToRename.m_FileName);
		if (!Directory.Exists(text))
		{
			m_SlotToRename = null;
			return;
		}
		string text2 = Path.Combine(GetSandboxLayoutSavePathForActiveProfile(), newName);
		if (Directory.Exists(text2))
		{
			m_SlotToRename = null;
			PopUpMessage.Display(Localize.Get("POPUP_RENAME_DIR_EXISTS", newName), null);
			return;
		}
		Utils.RenameDirectory(text, text2);
		MaybeRenameCurrentLayoutTextUI(text, text2);
		m_SlotToRename.m_FileName = newName;
		m_SlotToRename.m_DisplayName.text = $"[{newName}]";
		m_SlotToRename = null;
	}

	protected void DirectoryDeleteCallback()
	{
		PopUpMessage.DisplayWarning(Localize.Get("POPUP_DELETE_DIR", m_SlotToDelete.m_FileName), useYesNoLables: true, DeleteDirAfterConfirmation);
	}

	protected void DeleteDirAfterConfirmation()
	{
		string path = Path.Combine(GetSandboxLayoutSavePathForActiveProfile(), m_SlotToDelete.m_FileName);
		if (!Directory.Exists(path))
		{
			m_SlotToDelete = null;
			return;
		}
		DirectoryInfo directoryInfo = new DirectoryInfo(path);
		if (directoryInfo == null)
		{
			m_SlotToDelete = null;
			return;
		}
		int num = directoryInfo.GetFiles("*", SearchOption.AllDirectories).Length;
		if (num == 0)
		{
			DeleteDirAfterSecondConfirm();
			return;
		}
		GameUI.m_Instance.m_PopUpMessage.gameObject.SetActive(value: false);
		PopUpMessage.DisplayConfirmation(Localize.Get("POPUP_DELETE_NON_EMPTY_DIR", num.ToString()), useYesNoLabels: false, DeleteDirAfterSecondConfirm);
	}

	protected void DeleteDirAfterSecondConfirm()
	{
		string text = Path.Combine(GetSandboxLayoutSavePathForActiveProfile(), m_SlotToDelete.m_FileName);
		if (!Directory.Exists(text))
		{
			m_SlotToDelete = null;
			return;
		}
		Utils.DeleteDirectoryAndContents(text);
		MaybeDeleteCurrentLayoutTextUI(text);
		m_FileLoader.DeleteSlot(m_SlotToDelete);
		m_SlotToDelete = null;
	}

	protected string MaybeAddSubdirectoriesToName(string name)
	{
		if (m_SubDirectoriesOpened.Count > 0)
		{
			string path = m_SubDirectoriesOpened[0];
			for (int i = 1; i < m_SubDirectoriesOpened.Count; i++)
			{
				path = Path.Combine(path, m_SubDirectoriesOpened[i]);
			}
			name = Path.Combine(path, name);
		}
		return name;
	}

	protected void DirectorySlotClickedCallback(FileSlot slot)
	{
		if (!(slot == null))
		{
			InterfaceAudio.Play("ui_menu_select");
			m_SubDirectoriesOpened.Add(slot.m_FileName);
			PopulateSlots();
		}
	}

	protected void GetSubDirectoriesFromCurrentLayout()
	{
		m_SubDirectoriesOpened.Clear();
		if (!string.IsNullOrEmpty(Sandbox.m_CurrentLayoutName))
		{
			string[] array = Sandbox.m_CurrentLayoutName.Split(Path.DirectorySeparatorChar);
			for (int i = 0; i < array.Length - 1; i++)
			{
				m_SubDirectoriesOpened.Add(array[i]);
			}
		}
	}

	protected void SetBackButtonVisibility()
	{
		m_BackButton.gameObject.SetActive(m_SubDirectoriesOpened.Count > 0);
		m_BackButtonDisabled.gameObject.SetActive(m_SubDirectoriesOpened.Count <= 0);
		m_BackButton.GetComponent<PanelResizeHorizontal>().ForceUpdate();
		m_BackButtonDisabled.GetComponent<PanelResizeHorizontal>().ForceUpdate();
	}

	private void MaybeDeleteCurrentLayoutTextUI(string oldPath)
	{
		if (!string.IsNullOrEmpty(Sandbox.m_CurrentLayoutName))
		{
			string text = Path.Combine(SandboxLayout.GetSavePath(Profiles.GetActiveProfileName()), Sandbox.m_CurrentLayoutName + SandboxLayout.SAVE_EXTENSION);
			if (text == oldPath || text.StartsWith(oldPath))
			{
				Sandbox.m_CurrentLayoutName = "";
				GameUI.m_Instance.m_TopBar.m_MessageTopLeft.UnpinMessage();
			}
		}
	}

	private void MaybeRenameCurrentLayoutTextUI(string oldPath, string newPath)
	{
		if (string.IsNullOrEmpty(Sandbox.m_CurrentLayoutName))
		{
			return;
		}
		string text = Path.Combine(SandboxLayout.GetSavePath(Profiles.GetActiveProfileName()), Sandbox.m_CurrentLayoutName + SandboxLayout.SAVE_EXTENSION);
		if (text == oldPath || text.StartsWith(oldPath))
		{
			string text2 = text;
			text2 = text2.Replace(oldPath, newPath);
			text2 = text2.Replace(SandboxLayout.GetSavePath(Profiles.GetActiveProfileName()), "");
			text2 = text2.Replace(SandboxLayout.SAVE_EXTENSION, "");
			if (text2.Length > 0)
			{
				text2 = text2.Substring(1, text2.Length - 1);
			}
			Sandbox.m_CurrentLayoutName = text2;
		}
	}

	private void GoToParentDirectory()
	{
		if (m_SubDirectoriesOpened.Count > 0)
		{
			_ = m_SubDirectoriesOpened[m_SubDirectoriesOpened.Count - 1];
			m_SubDirectoriesOpened.RemoveAt(m_SubDirectoriesOpened.Count - 1);
			PopulateSlots();
			LayoutRebuilder.ForceRebuildLayoutImmediate(m_FileLoader.m_Content.GetComponent<RectTransform>());
			m_SelectedSlotIndex = 0;
			MaybeAutoScroll();
			m_SelectedSlotIndex = 0;
		}
	}

	private void OpenDir()
	{
		string text = SandboxLayout.GetSavePath(Profiles.GetActiveProfileName());
		for (int i = 0; i < m_SubDirectoriesOpened.Count; i++)
		{
			text = Path.Combine(text, m_SubDirectoriesOpened[i]);
		}
		Utils.OpenLocalPath(text);
	}

	private void OnBackDir()
	{
		GoToParentDirectory();
		InterfaceAudio.Play("ui_menu_select");
	}

	private void ForceGamepadCursorToSelecctedSlot()
	{
		if (GameInput.GetActiveGameDevice() == GameDevice.Gamepad && m_SelectedSlotIndex != -1)
		{
			GameInput.SetVirtualMousePosition(m_FileLoader.FindSlotByIndex(m_SelectedSlotIndex).m_AsteriskIcon.transform.position);
		}
	}
}
