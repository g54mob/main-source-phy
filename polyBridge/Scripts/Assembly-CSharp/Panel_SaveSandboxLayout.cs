using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_SaveSandboxLayout : Panel_SaveLoadSandboxLayout
{
	[Header("Header")]
	public Button m_Cancel;

	[Header("Footer")]
	public Button m_SaveAsNewLayoutButton;

	public Button m_CreateDirButton;

	public Toggle m_SortByDateToggle;

	private PointerEvents m_SortByDateTogglePointerEvents;

	private void Awake()
	{
		m_Cancel.onClick.AddListener(base.Close);
		m_SaveAsNewLayoutButton.onClick.AddListener(OnCreateNewLayout);
		m_CreateDirButton.onClick.AddListener(base.OnCreateNewDir);
		m_SortByDateTogglePointerEvents = m_SortByDateToggle.GetComponent<PointerEvents>();
		m_SortByDateTogglePointerEvents.RegisterOnClickedDelegate(OnSortByDateToggle);
		InitDirectoryButtons();
	}

	private void OnEnable()
	{
		GetSubDirectoriesFromCurrentLayout();
		if ((bool)Prefabs.m_Instance)
		{
			PopulateSlots();
			SelectFirstSlot();
		}
		SandboxItems.CancelMovementDueToModalMenuOpening();
		SetBackButtonVisibility();
		m_SortByDateToggle.isOn = Profiles.m_ActiveProfile.m_SortSandboxLayoutsByDate;
		GameUI.m_Instance.m_GamepadLegend.HideButtonsLeft();
		ShowGamepadButtons();
		ActivePanels.Add(base.gameObject);
	}

	private void OnDisable()
	{
		ActivePanels.Remove(base.gameObject);
	}

	private void Update()
	{
		ProcessInput();
		ProcessInternalInput();
		SetBackButtonVisibility();
		if (ActivePanels.IsTopPanel(base.gameObject))
		{
			ShowGamepadButtons();
		}
	}

	public void SaveNew(string name)
	{
		if (string.IsNullOrEmpty(name))
		{
			return;
		}
		name = name.Trim();
		if (string.IsNullOrEmpty(name) || Utils.HasInvalidFileNameChars(name))
		{
			PopUpMessage.DisplayWarningOkOnly(Localize.Get("UI_INVALID_FILENAME", name));
			return;
		}
		name = SandboxLayout.AddFileExtension(name);
		if (File.Exists(Path.Combine(GetSandboxLayoutSavePathForActiveProfile(), name)))
		{
			FileSlot slot = m_FileLoader.FindSlotByDisplayName(Path.GetFileNameWithoutExtension(name));
			PopUpMessage.DisplayConfirmation(Localize.Get("POPUP_OVERWRITE_SLOT", name), slot, SaveAfterConfirmation);
			return;
		}
		name = MaybeAddSubdirectoriesToName(name);
		Sandbox.Save(name);
		GameUI.m_Instance.m_SaveSandboxLayout.gameObject.SetActive(value: false);
		GameUI.m_Instance.m_PauseMenu.CloseSilent();
	}

	protected override void PopulateSlots()
	{
		UpdateCurrentDirectoryText();
		PopulateSaveLoadSlots(Profiles.GetActiveProfileName(), SlotClickedCallback);
		Sort();
		SelectFirstSlot();
	}

	private void SlotClickedCallback(FileSlot slot)
	{
		if (slot.m_IsDirectory)
		{
			DirectorySlotClickedCallback(slot);
			return;
		}
		PopUpMessage.DisplayConfirmation(Localize.Get("POPUP_OVERWRITE_SLOT", slot.m_DisplayName.text), slot, SaveAfterConfirmation);
		InterfaceAudio.Play("ui_menu_select");
	}

	private void SaveAfterConfirmation(FileSlot slot)
	{
		if ((bool)slot)
		{
			Sandbox.Save(SandboxLayout.AddFileExtension(MaybeAddSubdirectoriesToName(slot.m_DisplayName.text)));
			GameUI.m_Instance.m_SaveSandboxLayout.gameObject.SetActive(value: false);
			GameUI.m_Instance.m_PauseMenu.CloseSilent();
		}
	}

	protected override void MaybeDoEnterReturnInput()
	{
		if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) && m_SelectedSlotIndex != -1)
		{
			SlotClickedCallback(m_FileLoader.FindSlotByIndex(m_SelectedSlotIndex));
		}
	}

	private void UpdateCurrentDirectoryText()
	{
		m_CurrentDirectoryText.text = Profiles.GetProfileSandboxLocation();
		if (m_SubDirectoriesOpened.Count > 0)
		{
			TextMeshProUGUI currentDirectoryText = m_CurrentDirectoryText;
			string text = currentDirectoryText.text;
			char directorySeparatorChar = Path.DirectorySeparatorChar;
			currentDirectoryText.text = text + string.Join(directorySeparatorChar.ToString(), m_SubDirectoriesOpened);
		}
	}

	private void OnCreateNewLayout()
	{
		PopupInputField.Display(Localize.Get("UI_INPUTFIELD_SANDBOX_LAYOUT_NAME"), string.Empty, isFilename: true, isDirectory: false, SaveNew);
	}

	private void OnSortByDateToggle()
	{
		InterfaceAudio.Play("ui_settings_toggle");
		Profiles.m_ActiveProfile.m_SortSandboxLayoutsByDate = m_SortByDateToggle.isOn;
		Profiles.SaveActiveProfile();
		Sort();
		SelectFirstSlot();
	}

	private void Sort()
	{
		if (Profiles.m_ActiveProfile.m_SortSandboxLayoutsByDate)
		{
			m_FileLoader.SortByDate();
		}
		else
		{
			m_FileLoader.SortAlphabetically();
		}
	}

	private void ShowGamepadButtons()
	{
		GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, Localize.Get("UI_SELECT"), GamepadButtonType.WEST, Localize.Get("UI_SORT_BY_DATE"), GamepadButtonType.NORTH, Localize.Get("UI_SANDBOX_SAVE_NEW_LAYOUT"), GamepadButtonType.EAST, Localize.Get("UI_CLOSE"));
	}

	private void ProcessInternalInput()
	{
		if (!GameStateCommonInput.IgnoreKeyboardInputForPanel(base.gameObject))
		{
			if (GamepadManager.ButtonJustPressed(GamepadButtonType.NORTH))
			{
				OnCreateNewLayout();
			}
			if (GamepadManager.ButtonJustPressed(GamepadButtonType.WEST))
			{
				m_SortByDateToggle.isOn = !m_SortByDateToggle.isOn;
				OnSortByDateToggle();
			}
		}
	}
}
