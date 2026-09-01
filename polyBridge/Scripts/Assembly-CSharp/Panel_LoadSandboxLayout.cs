using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_LoadSandboxLayout : Panel_SaveLoadSandboxLayout
{
	[Header("Header")]
	public Button m_Cancel;

	public ProfileFilter m_ProfileFilter;

	[Header("Footer")]
	public Toggle m_SortByDateToggle;

	private PointerEvents m_SortByDateTogglePointerEvents;

	private void Awake()
	{
		m_Cancel.onClick.AddListener(base.Close);
		m_SortByDateTogglePointerEvents = m_SortByDateToggle.GetComponent<PointerEvents>();
		m_SortByDateTogglePointerEvents.RegisterOnClickedDelegate(OnSortByDateToggle);
		InitDirectoryButtons();
		PopulateSlots();
	}

	private void Update()
	{
		ProcessInput();
		ProcessInternalInput();
		if (GetCurrentDirectoryInfo(Profiles.GetActiveProfileName()) == null)
		{
			GetSubDirectoriesFromCurrentLayout();
			PopulateSlots();
		}
		SetBackButtonVisibility();
		if (ActivePanels.IsTopPanel(base.gameObject))
		{
			ShowGamepadButtons();
		}
	}

	private void OnEnable()
	{
		List<string> profileNames = Profiles.GetProfileNames();
		if (profileNames != null && profileNames.Count > 0)
		{
			m_ProfileFilter.Init(OnProfileFilterChanged);
			m_ProfileFilter.gameObject.SetActive(profileNames.Count > 1);
		}
		GetSubDirectoriesFromCurrentLayout();
		if ((bool)Prefabs.m_Instance)
		{
			PopulateSlots();
		}
		m_SortByDateToggle.isOn = Profiles.m_ActiveProfile.m_SortSandboxLayoutsByDate;
		SandboxItems.CancelMovementDueToModalMenuOpening();
		SetBackButtonVisibility();
		GameUI.m_Instance.m_GamepadLegend.HideButtonsLeft();
		ShowGamepadButtons();
		ActivePanels.Add(base.gameObject);
	}

	private void OnDisable()
	{
		ActivePanels.Remove(base.gameObject);
	}

	protected override void PopulateSlots()
	{
		m_FileLoader.DestroySlots();
		UpdateCurrentDirectoryText();
		PopulateLocalSandboxSlots();
		Sort();
		SelectFirstSlot();
	}

	private void PopulateLocalSandboxSlots()
	{
		PopulateSaveLoadSlots(m_ProfileFilter.GetSelectedProfileName(), LocalSlotClickedCallback);
	}

	private void LocalSlotClickedCallback(FileSlot slot)
	{
		if ((bool)slot)
		{
			if (slot.m_IsDirectory)
			{
				DirectorySlotClickedCallback(slot);
			}
			else if (Sandbox.m_UnsavedChanges)
			{
				InterfaceAudio.Play("ui_window_open");
				PopUpMessage.DisplayWarning(Localize.Get("POPUP_EXIT_SANDBOX_LOSE_CHANGES"), useYesNoLabels: true, slot, LocalSlotLoad);
			}
			else
			{
				InterfaceAudio.Play("ui_menu_select");
				LocalSlotLoad(slot);
			}
		}
	}

	private void LocalSlotLoad(FileSlot slot)
	{
		if ((bool)slot)
		{
			List<string> inactiveModsInLayout = Mods.GetInactiveModsInLayout(Path.Combine(SandboxLayout.GetSavePath(m_ProfileFilter.GetSelectedProfileName()), MaybeAddSubdirectoriesToName(slot.m_FileName)));
			if (inactiveModsInLayout.Count > 0)
			{
				GameUI.m_Instance.m_ModsRequiredPopup.Open(inactiveModsInLayout, slot, DoLocalSlotLoad);
			}
			else
			{
				DoLocalSlotLoad(slot);
			}
		}
	}

	private void DoLocalSlotLoad(FileSlot slot)
	{
		if ((bool)slot)
		{
			if (Prefabs.AsyncLoadInProgress())
			{
				InterfaceAudio.PlayErrorBeep();
				return;
			}
			BridgeCheat.Clear();
			GameStatePreloadingAssets.PreloadLevel(Path.Combine(SandboxLayout.GetSavePath(m_ProfileFilter.GetSelectedProfileName()), MaybeAddSubdirectoriesToName(slot.m_FileName)), slot, PreloadLocalSlotCallback);
		}
	}

	private void PreloadLocalSlotCallback(string layoutPath, FileSlot slot)
	{
		if (GameStateManager.GetState() == GameState.DECOR)
		{
			GameStateManager.SwitchToState(GameState.SANDBOX);
		}
		Sandbox.LoadLayout(layoutPath);
		Sandbox.m_CurrentLayoutName = MaybeAddSubdirectoriesToName(slot.m_DisplayName.text);
		GameUI.m_Instance.m_LevelInfoLite.gameObject.SetActive(value: false);
		Close();
		GameUI.m_Instance.m_PauseMenu.CloseSilent();
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

	protected override void MaybeDoEnterReturnInput()
	{
		if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) && m_SelectedSlotIndex != -1)
		{
			LocalSlotClickedCallback(m_FileLoader.FindSlotByIndex(m_SelectedSlotIndex));
			Outlines.ManualUpdate();
		}
	}

	protected override void MaybeDoLeftRightInput()
	{
		base.MaybeDoLeftRightInput();
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

	private void OnProfileFilterChanged(string profileName)
	{
		GetSubDirectoriesFromCurrentLayout();
		PopulateSlots();
	}

	private void ProcessInternalInput()
	{
		if (!GameStateCommonInput.IgnoreKeyboardInputForPanel(base.gameObject) && GamepadManager.ButtonJustPressed(GamepadButtonType.WEST))
		{
			m_SortByDateToggle.isOn = !m_SortByDateToggle.isOn;
			OnSortByDateToggle();
		}
	}

	private void ShowGamepadButtons()
	{
		GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, Localize.Get("UI_SELECT"), GamepadButtonType.WEST, Localize.Get("UI_SORT_BY_DATE"), GamepadButtonType.EAST, Localize.Get("UI_CLOSE"));
	}
}
