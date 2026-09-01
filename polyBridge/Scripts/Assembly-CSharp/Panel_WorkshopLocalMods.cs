using System;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_WorkshopLocalMods : MonoBehaviour
{
	public GameObject m_Root;

	public Panel_WorkshopSubmitMod m_SubmitModPanel;

	[Header("Header")]
	public TextMeshProUGUI m_TopHelpText;

	public TextMeshProUGUI m_SideHelpText;

	public Button m_OpenLocalModsFolderButton;

	[Header("Body")]
	public Panel_FileLoader m_FileLoader;

	public GameObject m_LeftSideHelpText;

	public GameObject m_MiddleHelpText;

	public GameObject m_TopHelpTextParent;

	[Header("Footer")]
	public Button m_CreateNewModButton;

	public Button m_CreateNewLanguageModButton;

	public Button m_CreateNewCampaignModButton;

	private FileSlot m_SlotToDelete;

	private FileSlot m_SelectedSlot;

	private int m_SelectedSlotIndex;

	private readonly float SLOT_HEIGHT = 34f;

	private readonly int MAX_VISIBLE_SLOTS = 10;

	private void Start()
	{
		m_OpenLocalModsFolderButton.onClick.AddListener(OnOpenLocalModsFolder);
		m_CreateNewModButton.onClick.AddListener(OnCreateNewMod);
		m_CreateNewLanguageModButton.onClick.AddListener(OnCreateNewLanguageMod);
		m_CreateNewCampaignModButton.onClick.AddListener(OnCreateNewCampaignMod);
	}

	private void Update()
	{
		ProcessInput();
		UpdateHelpTextVisibility();
		if (ActivePanels.IsTopPanel(base.gameObject))
		{
			GameUI.m_Instance.m_Workshop.ShowGamepadLegend();
		}
	}

	private void OnEnable()
	{
		ActivePanels.Add(base.gameObject);
	}

	private void OnDisable()
	{
		ActivePanels.Remove(base.gameObject);
	}

	public void Open()
	{
		m_Root.SetActive(value: true);
		m_TopHelpText.text = Localize.Get("UI_WORKSHOP_LOCAL_MODS_HELP", Mods.GetLocalTestModsDirectoryPath());
		PopulateSlots();
		SelectFirstSlot();
		UpdateHelpTextVisibility();
	}

	public void Close()
	{
		m_Root.SetActive(value: false);
	}

	private void PopulateSlots()
	{
		m_FileLoader.DestroySlots();
		AddLocalMods();
		SetSelectedSlot(m_FileLoader.GetFirstSlot());
	}

	private void AddLocalMods()
	{
		string localTestModsDirectoryPath = Mods.GetLocalTestModsDirectoryPath();
		try
		{
			string[] directories = Directory.GetDirectories(localTestModsDirectoryPath, "*", SearchOption.TopDirectoryOnly);
			foreach (string text in directories)
			{
				FileSlot fileSlot = m_FileLoader.AddSlot(text, 0L, Path.GetFileName(text), SlotClickedCallback, null);
				if (fileSlot != null)
				{
					fileSlot.m_DeleteButton.gameObject.SetActive(value: true);
					fileSlot.m_UploadButton.gameObject.SetActive(value: true);
					fileSlot.SetOnDeleteCallback(SlotDeleteCallback);
					fileSlot.SetOnUploadCallback(SlotUploadCallback);
					if (ModApi.CheckForWorkshopCampaignFunctions(Mods.GetLuaFilesInMod(text)))
					{
						fileSlot.SetOnPlayCallback(SlotPlayCallback);
					}
					else
					{
						fileSlot.SetOnToggleCallback(SlotToggleCallback);
					}
					SetSlotActivatedCheckbox(fileSlot);
				}
			}
		}
		catch (Exception ex)
		{
			Debug.LogWarning("Exception in AddLocalMods: " + ex.Message);
		}
	}

	private void SlotClickedCallback(FileSlot slot)
	{
		if ((bool)slot && slot != m_SelectedSlot)
		{
			SetSelectedSlot(slot);
			InterfaceAudio.Play("ui_menu_select");
		}
	}

	private void SlotToggleCallback(FileSlot slot, bool on)
	{
		string fileName = Path.GetFileName(slot.m_FileName);
		if (on)
		{
			if (!Profiles.m_ActiveProfile.m_ActiveLocalModDirectories.Contains(fileName))
			{
				Profiles.m_ActiveProfile.m_ActiveLocalModDirectories.Add(fileName);
			}
			Mods.ActivateMod(fileName);
		}
		else
		{
			Mods.DeactivateMod(fileName);
			if (Profiles.m_ActiveProfile.m_ActiveLocalModDirectories.Contains(fileName))
			{
				Profiles.m_ActiveProfile.m_ActiveLocalModDirectories.Remove(fileName);
			}
		}
		Profiles.SaveActiveProfile();
	}

	private void SlotPlayCallback(FileSlot slot)
	{
		string fileName = Path.GetFileName(slot.m_FileName);
		WorkshopCampaigns.ActivateWorkshopCampaignMod(fileName);
		WorkshopCampaignsLevelCache.Clear();
		GameUI.m_Instance.m_Workshop.m_WorkshopCampaignPanel.Open(WorkshopCampaigns.Get(fileName), string.Empty, string.Empty);
	}

	private void SlotUploadCallback(FileSlot slot)
	{
		if (GameManager.IsSteamOffline())
		{
			PopUpMessage.DisplayErrorOkOnly(Localize.Get("UI_STEAM_OFFLINE"));
			return;
		}
		string fileName = Path.GetFileName(slot.m_FileName);
		string uploadByLocalItemID = ModsSource.GetUploadByLocalItemID(fileName);
		m_SubmitModPanel.Open(uploadByLocalItemID, fileName, slot.m_FileName, UploadComplete);
	}

	private void UploadComplete(string itemID, string sourcePath)
	{
		if (!string.IsNullOrEmpty(itemID))
		{
			string fileName = Path.GetFileName(sourcePath);
			ModsSource.SaveUpload(itemID, fileName);
			ModsSource.SaveSourceFolder(fileName, sourcePath);
		}
	}

	private void SlotDeleteCallback(FileSlot slot)
	{
		m_SlotToDelete = slot;
		PopUpMessage.DisplayWarning(Localize.Get("POPUP_DELETE_SLOT", m_SlotToDelete.m_DisplayName.text, null), useYesNoLables: true, DeleteSaveSlotCallback);
	}

	private void DeleteSaveSlotCallback()
	{
		if ((bool)m_SlotToDelete)
		{
			if (!Utils.DeleteDirectoryAndContents(m_SlotToDelete.m_FileName))
			{
				PopUpMessage.DisplayErrorOkOnly(Localize.Get("UI_FAILED_TO_DELETE_LOCAL_MOD"));
			}
			else
			{
				PopulateSlots();
			}
			m_SlotToDelete = null;
		}
	}

	private void SetSelectedSlot(FileSlot slot)
	{
		if (!(slot == null))
		{
			m_SelectedSlot = slot;
			m_SelectedSlotIndex = m_FileLoader.GetSlotIndex(slot);
			m_FileLoader.SelectSlot(slot);
		}
	}

	private void SetSlotActivatedCheckbox(FileSlot slot)
	{
		string fileName = Path.GetFileName(slot.m_FileName);
		slot.m_Toggle.isOn = Profiles.m_ActiveProfile.m_ActiveLocalModDirectories.Contains(fileName);
	}

	private void OnOpenLocalModsFolder()
	{
		InterfaceAudio.Play("ui_menubar_gen_on");
		Utils.OpenLocalPath(Mods.GetLocalTestModsDirectoryPath());
	}

	private void OnCreateNewMod()
	{
		InterfaceAudio.Play("ui_menu_select");
		PopupInputField.Display(Localize.Get("UI_LOCAL_MOD_FOLDER_ENTER_NAME"), string.Empty, isFilename: false, isDirectory: true, CreateNewMod);
	}

	private void OnCreateNewLanguageMod()
	{
		InterfaceAudio.Play("ui_menu_select");
		PopupInputField.Display(Localize.Get("UI_LOCAL_MOD_FOLDER_ENTER_NAME"), string.Empty, isFilename: false, isDirectory: true, CreateNewLanguageMod);
	}

	private void OnCreateNewCampaignMod()
	{
		InterfaceAudio.Play("ui_menu_select");
		PopupInputField.Display(Localize.Get("UI_LOCAL_MOD_FOLDER_ENTER_NAME"), string.Empty, isFilename: false, isDirectory: true, CreateNewCampaignMod);
	}

	public void CreateNewMod(string name)
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
		string text = Path.Combine(Mods.GetLocalTestModsDirectoryPath(), name);
		if (Utils.DirectoryExists(text))
		{
			PopUpMessage.DisplayWarningOkOnly(Localize.Get("UI_LOCAL_MOD_FOLDER_ALREADY_EXISTS"));
			return;
		}
		Utils.CreateDirectory(text);
		if (!Utils.DirectoryExists(text))
		{
			return;
		}
		string sourceFileName = Path.Combine(Application.streamingAssetsPath, "ModTemplates", "OnModLoad_Basic.lua");
		string destFileName = Path.Combine(text, Mods.MOD_LOAD_FILENAME);
		try
		{
			File.Copy(sourceFileName, destFileName, overwrite: true);
			PopulateSlots();
			Utils.OpenLocalPath(text);
		}
		catch (Exception ex)
		{
			Debug.Log("Failed to copy OnModLoad.lua due to: '" + ex.Message + "'");
		}
	}

	public void CreateNewLanguageMod(string name)
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
		string text = Path.Combine(Mods.GetLocalTestModsDirectoryPath(), name);
		if (Utils.DirectoryExists(text))
		{
			PopUpMessage.DisplayWarningOkOnly(Localize.Get("UI_LOCAL_MOD_FOLDER_ALREADY_EXISTS"));
			return;
		}
		Utils.CreateDirectory(text);
		if (Utils.DirectoryExists(text))
		{
			string sourceFileName = Path.Combine(Application.streamingAssetsPath, "ModTemplates", "OnModLoad_Language.lua");
			string destFileName = Path.Combine(text, Mods.MOD_LOAD_FILENAME);
			try
			{
				File.Copy(sourceFileName, destFileName, overwrite: true);
				PopulateSlots();
				Utils.OpenLocalPath(text);
			}
			catch (Exception ex)
			{
				Debug.Log("Failed to copy OnModLoad.lua due to: '" + ex.Message + "'");
			}
			ModFile_Language.CreateTemplateCSV(text);
			PopulateSlots();
			Utils.OpenLocalPath(text);
		}
	}

	public void CreateNewCampaignMod(string name)
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
		string text = Path.Combine(Mods.GetLocalTestModsDirectoryPath(), name);
		if (Utils.DirectoryExists(text))
		{
			PopUpMessage.DisplayWarningOkOnly(Localize.Get("UI_LOCAL_MOD_FOLDER_ALREADY_EXISTS"));
			return;
		}
		Utils.CreateDirectory(text);
		if (!Utils.DirectoryExists(text))
		{
			return;
		}
		string sourceFileName = Path.Combine(Application.streamingAssetsPath, "ModTemplates", "OnModLoad_Campaign.lua");
		string destFileName = Path.Combine(text, Mods.MOD_LOAD_FILENAME);
		try
		{
			File.Copy(sourceFileName, destFileName, overwrite: true);
			PopulateSlots();
			Utils.OpenLocalPath(text);
		}
		catch (Exception ex)
		{
			Debug.Log("Failed to copy OnModLoad.lua due to: '" + ex.Message + "'");
		}
	}

	private void ProcessInput()
	{
		if (GameStateCommonInput.IgnoreKeyboardInputForPanel(base.gameObject))
		{
			return;
		}
		if (Input.GetKeyDown(KeyCode.Escape) || GamepadManager.ButtonJustPressed(GamepadButtonType.EAST))
		{
			Panel_Workshop.m_Instance.Close();
			return;
		}
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
		if (GamepadManager.ButtonJustPressed(GamepadButtonType.SHOULDER_RIGHT))
		{
			GameUI.m_Instance.m_Workshop.CycleToNextTab();
		}
		if (GamepadManager.ButtonJustPressed(GamepadButtonType.SHOULDER_LEFT))
		{
			GameUI.m_Instance.m_Workshop.CycleToPrevTab();
		}
		if (GamepadManager.ButtonJustPressed(GamepadButtonType.DPAD_RIGHT))
		{
			GameUI.m_Instance.m_Workshop.CycleToNextPage();
		}
		if (GamepadManager.ButtonJustPressed(GamepadButtonType.DPAD_LEFT))
		{
			GameUI.m_Instance.m_Workshop.CycleToPrevPage();
		}
		if (GamepadManager.ButtonJustPressed(GamepadButtonType.SOUTH) && GameUI.PointerOver(typeof(FileSlot)))
		{
			FileSlot fileSlotUnderPointer = GameUI.GetFileSlotUnderPointer();
			if (fileSlotUnderPointer != null)
			{
				fileSlotUnderPointer.m_Toggle.isOn = !fileSlotUnderPointer.m_Toggle.isOn;
				SlotToggleCallback(fileSlotUnderPointer, fileSlotUnderPointer.m_Toggle.isOn);
				InterfaceAudio.PlayToggleAudio();
			}
		}
	}

	private void ScrollDown()
	{
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

	private void ScrollUp()
	{
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

	private void MaybeAutoScroll()
	{
		float num = m_FileLoader.m_Content.GetComponent<RectTransform>().anchoredPosition.y / SLOT_HEIGHT;
		float num2 = (float)(m_SelectedSlotIndex + 1) - num;
		if (num2 < 1f)
		{
			m_FileLoader.m_Content.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, SLOT_HEIGHT * (float)m_SelectedSlotIndex);
		}
		else if (num2 > (float)MAX_VISIBLE_SLOTS)
		{
			m_FileLoader.m_Content.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, SLOT_HEIGHT * (float)(m_SelectedSlotIndex + 1 - MAX_VISIBLE_SLOTS));
		}
	}

	private void SelectFirstSlot()
	{
		m_SelectedSlotIndex = 0;
		SetSelectedSlot(m_FileLoader.GetFirstSlot());
	}

	private void UpdateHelpTextVisibility()
	{
		m_LeftSideHelpText.SetActive(m_FileLoader.m_Slots.Count > 0);
		m_TopHelpTextParent.SetActive(m_FileLoader.m_Slots.Count > 0);
		m_MiddleHelpText.SetActive(m_FileLoader.m_Slots.Count == 0);
	}

	private void ForceGamepadCursorToSelecctedSlot()
	{
		if (GameInput.GetActiveGameDevice() == GameDevice.Gamepad && m_SelectedSlotIndex != -1)
		{
			GameInput.SetVirtualMousePosition(m_FileLoader.FindSlotByIndex(m_SelectedSlotIndex).m_AsteriskIcon.transform.position);
		}
	}
}
