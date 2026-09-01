using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_LoadBridge : MonoBehaviour
{
	public Panel_FileLoader m_FileLoader;

	public ProfileFilter m_ProfileFilter;

	public RawImage m_BridgePreview;

	public TextMeshProUGUI m_NoSavesText;

	public Button m_Cancel;

	public Toggle m_LoadAutoSaveToggle;

	public Toggle m_SortByDateToggle;

	private PointerEvents m_LoadAutoSaveTogglePointerEvents;

	private PointerEvents m_SortByDateTogglePointerEvents;

	private string m_LastLoadedBridgePreviewFilename;

	private readonly float SLOT_HEIGHT = 34f;

	private readonly int MAX_VISIBLE_SLOTS = 10;

	private int m_SelectedSlotIndex;

	private bool m_IgnoreFirstHoverChange;

	private void Awake()
	{
		m_Cancel.onClick.AddListener(Close);
		m_LoadAutoSaveTogglePointerEvents = m_LoadAutoSaveToggle.GetComponent<PointerEvents>();
		m_LoadAutoSaveTogglePointerEvents.RegisterOnClickedDelegate(OnLoadAutoSaveToggle);
		m_SortByDateTogglePointerEvents = m_SortByDateToggle.GetComponent<PointerEvents>();
		m_SortByDateTogglePointerEvents.RegisterOnClickedDelegate(OnSortByDateToggle);
		m_BridgePreview.gameObject.SetActive(value: false);
	}

	private void Update()
	{
		ProcessInput();
		m_NoSavesText.gameObject.SetActive(m_FileLoader.NumSlots() == 0);
		FileSlot slotWhenPointerOverInfo = m_FileLoader.GetSlotWhenPointerOverInfo();
		if (slotWhenPointerOverInfo == null)
		{
			m_BridgePreview.gameObject.SetActive(value: false);
		}
		else
		{
			DisplayBridgePreviewSlot(slotWhenPointerOverInfo);
		}
		if (ActivePanels.IsTopPanel(base.gameObject))
		{
			ShowGamepadButtons();
		}
	}

	private void DisplayBridgePreviewSlot(FileSlot previewSlot)
	{
		m_BridgePreview.transform.position = GameInput.GetMousePosition();
		if (m_LastLoadedBridgePreviewFilename == previewSlot.m_FileName)
		{
			m_BridgePreview.gameObject.SetActive(value: true);
			return;
		}
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

	private void OnEnable()
	{
		List<string> profileNames = Profiles.GetProfileNames();
		if (profileNames != null && profileNames.Count > 0)
		{
			m_ProfileFilter.Init(OnProfileFilterChanged);
			m_ProfileFilter.gameObject.SetActive(profileNames.Count > 1);
		}
		if ((bool)Prefabs.m_Instance)
		{
			PopulateSlots();
			Sort();
			SelectFirstSlot();
			ShowGamepadButtons();
		}
		m_LoadAutoSaveToggle.isOn = Profiles.m_ActiveProfile.m_AutomatiallyLoadAutoSave;
		m_SortByDateToggle.isOn = Profiles.m_ActiveProfile.m_SortBridgeSavesByDate;
		m_NoSavesText.gameObject.SetActive(m_FileLoader.NumSlots() == 0);
		m_LastLoadedBridgePreviewFilename = string.Empty;
		ActivePanels.Add(base.gameObject);
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

	private void PopulateSlots()
	{
		m_FileLoader.DestroySlots();
		BridgeSaveSlots.ClearSlots();
		List<BridgeSaveSlotData> list = BridgeSaveSlots.LoadSlots(BridgeSaveSlots.GetDirectoryForSaveSlot(), m_ProfileFilter.GetSelectedProfileName());
		if (list == null)
		{
			return;
		}
		foreach (BridgeSaveSlotData item in list)
		{
			FileSlot fileSlot = m_FileLoader.AddSlot(item.m_SlotFilename, item.m_LastWriteTimeTicks, item.m_DisplayName, SlotClickedCallback, SlotHoverCallback);
			if ((bool)fileSlot)
			{
				fileSlot.m_InfoButton.gameObject.SetActive(value: true);
				if (item.m_SlotID >= BridgeSaveSlots.NUM_RESERVED_SLOTS)
				{
					fileSlot.SetOnDeleteCallback(BridgeSaveSlots.SlotDeleteCallback);
					fileSlot.SetOnRenameCallback(BridgeSaveSlots.SlotRenameCallback);
					fileSlot.m_AsteriskIcon.gameObject.SetActive(value: false);
				}
				else
				{
					fileSlot.m_AsteriskIcon.gameObject.SetActive(value: true);
				}
				if (item.m_SlotID == 1 || item.m_SlotID == 2 || item.m_SlotID == 3)
				{
					fileSlot.SetOnDeleteCallback(BridgeSaveSlots.SlotDeleteCallback);
				}
				fileSlot.m_Budget.text = ((item.m_Budget < 0) ? string.Empty : Utils.FormatCash(item.m_Budget));
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
		if (!slot)
		{
			return;
		}
		BridgeSaveSlotData bridgeSaveSlotData = BridgeSaveSlots.FindByFilename(slot.m_FileName);
		if (bridgeSaveSlotData != null)
		{
			BridgeSaveData bridgeSaveData = Bridge.ClearAndLoadBinary(bridgeSaveSlotData.m_Bridge);
			if (bridgeSaveData != null)
			{
				BridgeCheat.CheckForCheating(Sandbox.m_CurrentLayoutData, bridgeSaveData, Game.GetLevelId());
				Bridge.Sanitize();
			}
			Budget.MaybeApplyForcedBudgets(bridgeSaveSlotData.m_UsingUnlimitedBudget, bridgeSaveSlotData.m_UsingUnlimitedMaterials);
			GameAchievements.InvalidateSpeedRunnerTimer();
			if (ShouldShowPhysicsEngineWarning(bridgeSaveSlotData))
			{
				PopUpMessage.DisplayOkOnly(Localize.Get("POPUP_PHYSICS_ENGINE_WARNING"), null, PopUpWarningCategory.OLDER_PHYSICS_ENGINE);
			}
			if (bridgeSaveSlotData.m_SlotID >= BridgeSaveSlots.NUM_RESERVED_SLOTS)
			{
				BridgeSaveSlots.RecordLastSlotSavedForFutureQuicksave(slot.m_DisplayName.text);
			}
			else
			{
				BridgeSaveSlots.ClearLastSlotSavedForFutureQuicksave();
			}
		}
		Close();
		GameUI.m_Instance.m_PauseMenu.CloseSilent();
	}

	private bool ShouldShowPhysicsEngineWarning(BridgeSaveSlotData bridgeSlot)
	{
		if (bridgeSlot.m_Version < 2)
		{
			return false;
		}
		if (bridgeSlot.m_PhysicsVersion >= GameManager.GetPhysicsEngineVersion())
		{
			return false;
		}
		return !Profiles.m_ActiveProfile.m_NeverShowAgain.Contains(PopUpWarningCategory.OLDER_PHYSICS_ENGINE);
	}

	private void OnLoadAutoSaveToggle()
	{
		InterfaceAudio.Play("ui_settings_toggle");
		Profiles.m_ActiveProfile.m_AutomatiallyLoadAutoSave = m_LoadAutoSaveToggle.isOn;
		Profiles.SaveActiveProfile();
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
			if (GamepadManager.ButtonJustPressed(GamepadButtonType.NORTH))
			{
				m_LoadAutoSaveToggle.isOn = !m_LoadAutoSaveToggle.isOn;
				OnLoadAutoSaveToggle();
			}
			if (GamepadManager.ButtonJustPressed(GamepadButtonType.WEST))
			{
				m_SortByDateToggle.isOn = !m_SortByDateToggle.isOn;
				OnSortByDateToggle();
			}
			if (GamepadManager.ButtonJustPressed(GamepadButtonType.EAST))
			{
				Close();
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

	private void OnProfileFilterChanged(string profileName)
	{
		PopulateSlots();
		Sort();
		SelectFirstSlot();
	}

	private void ShowGamepadButtons()
	{
		GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, Localize.Get("UI_SELECT"), GamepadButtonType.WEST, Localize.Get("UI_SORT_BY_DATE"), GamepadButtonType.NORTH, Localize.Get("UI_SETTINGS_AUTO_LOAD_SAVES"), GamepadButtonType.EAST, Localize.Get("UI_CLOSE"));
	}

	private void ForceGamepadCursorToSelecctedSlot()
	{
		if (GameInput.GetActiveGameDevice() == GameDevice.Gamepad && m_SelectedSlotIndex != -1)
		{
			GameInput.SetVirtualMousePosition(m_FileLoader.FindSlotByIndex(m_SelectedSlotIndex).m_AsteriskIcon.transform.position);
		}
	}
}
