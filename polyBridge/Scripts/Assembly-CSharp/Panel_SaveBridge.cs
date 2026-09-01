using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Panel_SaveBridge : MonoBehaviour
{
	public Panel_FileLoader m_FileLoader;

	public RawImage m_BridgePreview;

	public Button m_Cancel;

	public Button m_SaveAsNewLayoutButton;

	public Toggle m_SortByDateToggle;

	private string m_LastLoadedBridgePreviewFilename;

	private bool m_ShowBridgePreview;

	private string m_SlotNameToSave;

	private readonly float SLOT_HEIGHT = 34f;

	private readonly int MAX_VISIBLE_SLOTS = 10;

	private int m_SelectedSlotIndex;

	private bool m_IgnoreFirstHoverChange;

	private PointerEvents m_SortByDateTogglePointerEvents;

	private void Awake()
	{
		m_Cancel.onClick.AddListener(Close);
		m_SaveAsNewLayoutButton.onClick.AddListener(OnSaveNewBridge);
		m_BridgePreview.gameObject.SetActive(value: false);
		m_SortByDateTogglePointerEvents = m_SortByDateToggle.GetComponent<PointerEvents>();
		m_SortByDateTogglePointerEvents.RegisterOnClickedDelegate(OnSortByDateToggle);
	}

	private void Update()
	{
		ProcessInput();
		FileSlot slotWhenPointerOverInfo = m_FileLoader.GetSlotWhenPointerOverInfo();
		if (slotWhenPointerOverInfo == null)
		{
			m_BridgePreview.gameObject.SetActive(value: false);
			m_ShowBridgePreview = false;
		}
		else
		{
			DisplayBridgePreviewSlot(slotWhenPointerOverInfo);
		}
		if (m_ShowBridgePreview)
		{
			m_BridgePreview.gameObject.SetActive(value: true);
		}
		if (ActivePanels.IsTopPanel(base.gameObject))
		{
			ShowGamepadButtons();
		}
	}

	private void OnEnable()
	{
		if ((bool)Prefabs.m_Instance)
		{
			PopulateSlots();
			m_FileLoader.SortAlphabetically();
			BridgeSaveSlots.ForceReservedSlotsToTop(m_FileLoader, sortByDate: false);
			m_FileLoader.MatchLayoutWithSlots();
			Sort();
			SelectFirstSlot();
		}
		ActivePanels.Add(base.gameObject);
		if (m_FileLoader.NumSlots() == 0)
		{
			base.gameObject.SetActive(value: false);
			PopupInputField.Display(Localize.Get("UI_INPUTFIELD_SAVE_SLOT_NAME"), BridgeSaveSlots.GetDefaultNewSlotName(), isFilename: true, isDirectory: false, SaveNew);
		}
		else
		{
			ShowGamepadButtons();
		}
		m_SortByDateToggle.isOn = Profiles.m_ActiveProfile.m_SortBridgeSavesByDate;
		GameUI.m_Instance.m_GamepadLegend.HideButtonsLeft();
	}

	private void OnDisable()
	{
		m_BridgePreview.gameObject.SetActive(value: false);
		ActivePanels.Remove(base.gameObject);
	}

	public void Close()
	{
		InterfaceAudio.Play("ui_window_close");
		base.gameObject.SetActive(value: false);
	}

	public void Save(string filename, bool quicksave)
	{
		BridgeSaveSlotData bridgeSaveSlotData = BridgeSaveSlots.FindByFilename(filename);
		if (bridgeSaveSlotData != null)
		{
			bridgeSaveSlotData.m_Bridge = ((GameStateManager.GetState() == GameState.SIM) ? Bridge.m_BridgeRestore.SerializeBinary() : BridgeSave.SerializeBinary());
			bridgeSaveSlotData.m_Budget = Mathf.RoundToInt(Budget.m_BridgeCost);
			bridgeSaveSlotData.m_MaxStress = GameLeaderboards.ConvertStressToScore(StressSamples.m_MaxStressNormalized);
			bridgeSaveSlotData.m_UsingUnlimitedMaterials = Budget.m_UsingForcedUnlimitedMaterial;
			bridgeSaveSlotData.m_UsingUnlimitedBudget = Budget.m_UsingForcedUnlimitedBudget;
			bridgeSaveSlotData.m_Thumb = SaveSlotImageMaker.CaptureImage(GameState.BUILD);
			bridgeSaveSlotData.m_LevelID = Game.GetLevelId();
			bridgeSaveSlotData.m_PhysicsVersion = GameManager.GetPhysicsEngineVersion();
			if (BridgeSaveSlots.Save(BridgeSaveSlots.GetDirectoryForSaveSlot(), bridgeSaveSlotData))
			{
				if (quicksave)
				{
					GameUI.ShowMessage(ScreenMessageLocation.TOP_LEFT, string.Format(Localize.Get("UI_SAVING"), Path.GetFileNameWithoutExtension(filename)), ScreenMessage.DEFAULT_DURATION_SECONDS);
				}
			}
			else
			{
				PopUpMessage.DisplayErrorOkOnly(string.Format(Localize.Get("WARN_SAVE_FAILED"), filename));
			}
		}
		Close();
		GameUI.m_Instance.m_PauseMenu.CloseSilent();
	}

	private void PopulateSlots()
	{
		m_FileLoader.DestroySlots();
		BridgeSaveSlots.ClearSlots();
		List<BridgeSaveSlotData> list = BridgeSaveSlots.LoadSlots(BridgeSaveSlots.GetDirectoryForSaveSlot(), Profiles.GetActiveProfileName());
		if (list == null)
		{
			return;
		}
		foreach (BridgeSaveSlotData item in list)
		{
			if (item.m_SlotID >= BridgeSaveSlots.NUM_RESERVED_SLOTS)
			{
				AddFileSlot(item);
			}
		}
	}

	private void SlotHoverCallback(FileSlot slot, bool hover)
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

	private void SlotClickedCallback(FileSlot slot)
	{
		m_SlotNameToSave = slot.m_DisplayName.text;
		string filename = BridgeSaveSlots.AddFileExtension(slot.m_DisplayName.text);
		if (BridgeSaveSlots.FilenameExists(BridgeSaveSlots.GetDirectoryForSaveSlot(), filename))
		{
			PopUpMessage.DisplayConfirmation(Localize.Get("POPUP_OVERWRITE_SLOT", m_SlotNameToSave), useYesNoLabels: false, SaveAfterConfirmation);
		}
		else
		{
			SaveAfterConfirmation();
		}
		InterfaceAudio.Play("ui_menu_select");
	}

	private void SaveNew(string slotName)
	{
		if (string.IsNullOrEmpty(slotName))
		{
			m_SlotNameToSave = null;
			return;
		}
		slotName = slotName.Trim();
		if (string.IsNullOrEmpty(slotName) || Utils.HasInvalidFileNameChars(slotName))
		{
			PopUpMessage.DisplayWarningOkOnly(Localize.Get("UI_INVALID_FILENAME", slotName));
			m_SlotNameToSave = null;
			return;
		}
		m_SlotNameToSave = slotName;
		string filename = BridgeSaveSlots.AddFileExtension(slotName);
		if (BridgeSaveSlots.FilenameExists(BridgeSaveSlots.GetDirectoryForSaveSlot(), filename))
		{
			PopUpMessage.Display(Localize.Get("POPUP_SAVE_SLOT_EXISTS", slotName), SaveNewAfterConfirmation);
		}
		else
		{
			SaveNewAfterConfirmation();
		}
	}

	private void SaveNewAfterConfirmation()
	{
		if (!string.IsNullOrEmpty(m_SlotNameToSave))
		{
			int slotIndex = Mathf.Max(BridgeSaveSlots.NUM_RESERVED_SLOTS, BridgeSaveSlots.GetHighestSlotID() + 1);
			BridgeSaveSlotData bridgeSaveSlotData = BridgeSaveSlots.Add(m_SlotNameToSave, slotIndex);
			if (bridgeSaveSlotData != null)
			{
				AddFileSlot(bridgeSaveSlotData);
				Save(BridgeSaveSlots.AddFileExtension(m_SlotNameToSave), quicksave: false);
				BridgeSaveSlots.RecordLastSlotSavedForFutureQuicksave(m_SlotNameToSave);
			}
		}
	}

	private void SaveAfterConfirmation()
	{
		if (!string.IsNullOrEmpty(m_SlotNameToSave))
		{
			Save(BridgeSaveSlots.AddFileExtension(m_SlotNameToSave), quicksave: false);
			BridgeSaveSlots.RecordLastSlotSavedForFutureQuicksave(m_SlotNameToSave);
		}
	}

	private void AddFileSlot(BridgeSaveSlotData bridgeSlot)
	{
		FileSlot fileSlot = m_FileLoader.AddSlot(bridgeSlot.m_SlotFilename, bridgeSlot.m_LastWriteTimeTicks, bridgeSlot.m_DisplayName, SlotClickedCallback, SlotHoverCallback);
		if ((bool)fileSlot)
		{
			fileSlot.m_InfoButton.gameObject.SetActive(value: true);
			if (bridgeSlot.m_SlotID >= BridgeSaveSlots.NUM_RESERVED_SLOTS)
			{
				fileSlot.SetOnDeleteCallback(BridgeSaveSlots.SlotDeleteCallback);
				fileSlot.SetOnRenameCallback(BridgeSaveSlots.SlotRenameCallback);
				fileSlot.m_AsteriskIcon.gameObject.SetActive(value: false);
			}
			else
			{
				fileSlot.m_AsteriskIcon.gameObject.SetActive(value: true);
			}
			fileSlot.m_Budget.text = ((bridgeSlot.m_Budget < 0) ? string.Empty : Utils.FormatCash(bridgeSlot.m_Budget));
		}
	}

	private void DisplayBridgePreviewSlot(FileSlot previewSlot)
	{
		m_ShowBridgePreview = true;
		m_BridgePreview.transform.position = GameInput.GetMousePosition();
		if (!(m_LastLoadedBridgePreviewFilename == previewSlot.m_FileName))
		{
			BridgeSaveSlotData bridgeSaveSlotData = BridgeSaveSlots.FindByFilename(previewSlot.m_FileName);
			if (bridgeSaveSlotData != null)
			{
				m_LastLoadedBridgePreviewFilename = previewSlot.m_FileName;
				BridgeSaveData bridgeSaveData = new BridgeSaveData();
				int offset = 0;
				bridgeSaveData.DeserializeBinary(bridgeSaveSlotData.m_Bridge, ref offset);
				SaveSlotImageMaker.GenerateImage(bridgeSaveSlotData, m_BridgePreview);
				m_BridgePreview.gameObject.SetActive(value: true);
			}
		}
	}

	private void ProcessInput()
	{
		if (!GameStateCommonInput.IgnoreKeyboardInputForPanel(base.gameObject))
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				Close();
			}
			if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) && m_SelectedSlotIndex != -1)
			{
				SlotClickedCallback(m_FileLoader.FindSlotByIndex(m_SelectedSlotIndex));
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
			if (GamepadManager.ButtonJustPressed(GamepadButtonType.EAST))
			{
				Close();
			}
			if (GamepadManager.ButtonJustPressed(GamepadButtonType.NORTH))
			{
				OnSaveNewBridge();
			}
			if (GamepadManager.ButtonJustPressed(GamepadButtonType.WEST))
			{
				m_SortByDateToggle.isOn = !m_SortByDateToggle.isOn;
				OnSortByDateToggle();
			}
		}
	}

	private void SetSelectedSlot(FileSlot slot)
	{
		m_SelectedSlotIndex = m_FileLoader.GetSlotIndex(slot);
		m_FileLoader.SelectSlot(slot);
	}

	private void ScrollDown()
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
			m_IgnoreFirstHoverChange = true;
		}
		else if (num2 > (float)MAX_VISIBLE_SLOTS)
		{
			m_FileLoader.m_Content.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, SLOT_HEIGHT * (float)(m_SelectedSlotIndex + 1 - MAX_VISIBLE_SLOTS));
			m_IgnoreFirstHoverChange = true;
		}
	}

	private void SelectFirstSlot()
	{
		m_SelectedSlotIndex = 0;
		m_SelectedSlotIndex = m_FileLoader.GetSlotIndex(m_FileLoader.GetFirstSlot());
	}

	public void OnSaveNewBridge()
	{
		PopupInputField.Display(Localize.Get("UI_INPUTFIELD_SAVE_SLOT_NAME"), BridgeSaveSlots.GetDefaultNewSlotName(), isFilename: true, isDirectory: false, SaveNew);
	}

	private void OnSortByDateToggle()
	{
		InterfaceAudio.Play("ui_settings_toggle");
		Profiles.m_ActiveProfile.m_SortBridgeSavesByDate = m_SortByDateToggle.isOn;
		Profiles.SaveActiveProfile();
		Sort();
		SelectFirstSlot();
	}

	private void Sort()
	{
		if (Profiles.m_ActiveProfile.m_SortBridgeSavesByDate)
		{
			m_FileLoader.SortByDate();
			BridgeSaveSlots.ForceReservedSlotsToTop(m_FileLoader, sortByDate: true);
		}
		else
		{
			PopulateSlots();
			m_FileLoader.SortAlphabetically();
			BridgeSaveSlots.ForceReservedSlotsToTop(m_FileLoader, sortByDate: false);
		}
	}

	private void ShowGamepadButtons()
	{
		GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, Localize.Get("UI_SELECT"), GamepadButtonType.WEST, Localize.Get("UI_SORT_BY_DATE"), GamepadButtonType.NORTH, Localize.Get("UI_BRIDGE_SAVE_NEW_LAYOUT"), GamepadButtonType.EAST, Localize.Get("UI_CLOSE"));
	}

	private void ForceGamepadCursorToSelecctedSlot()
	{
		if (GameInput.GetActiveGameDevice() == GameDevice.Gamepad && m_SelectedSlotIndex != -1)
		{
			GameInput.SetVirtualMousePosition(m_FileLoader.FindSlotByIndex(m_SelectedSlotIndex).m_AsteriskIcon.transform.position);
		}
	}
}
