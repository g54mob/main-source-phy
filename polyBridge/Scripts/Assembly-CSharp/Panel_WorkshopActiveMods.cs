using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Panel_WorkshopActiveMods : MonoBehaviour
{
	public GameObject m_Root;

	[Header("Header")]
	public Button m_DeactivateAllModsButton;

	[Header("Body")]
	public Panel_FileLoader m_FileLoader;

	public GameObject m_LeftSideHelpText;

	public GameObject m_MiddleHelpText;

	private FileSlot m_SelectedSlot;

	private int m_SelectedSlotIndex;

	private readonly float SLOT_HEIGHT = 34f;

	private readonly int MAX_VISIBLE_SLOTS = 10;

	private bool m_SaveProfileOnExit;

	private void Start()
	{
		m_DeactivateAllModsButton.onClick.AddListener(OnDeactivateAllMods);
	}

	private void Update()
	{
		ProcessInput();
		UpdateVisibility();
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
		if (m_SaveProfileOnExit)
		{
			Profiles.SaveActiveProfile();
			m_SaveProfileOnExit = false;
		}
	}

	public void Open()
	{
		m_Root.SetActive(value: true);
		PopulateSlots();
		SelectFirstSlot();
		UpdateVisibility();
	}

	public void Close()
	{
		m_Root.SetActive(value: false);
	}

	private void PopulateSlots()
	{
		m_FileLoader.DestroySlots();
		AddSubscribedMods();
		SetSelectedSlot(m_FileLoader.GetFirstSlot());
	}

	private void AddSubscribedMods()
	{
		foreach (KeyValuePair<string, SteamItemInfo> subscribedItem in Workshop.m_SubscribedItems)
		{
			if (subscribedItem.Value.m_IsMod)
			{
				FileSlot fileSlot = m_FileLoader.AddSlot(subscribedItem.Value.m_InstallPath, 0L, subscribedItem.Value.m_Title, SlotClickedCallback, null);
				if (fileSlot != null)
				{
					fileSlot.m_InfoButton.gameObject.SetActive(value: true);
					fileSlot.m_InfoButton.GetComponentInChildren<ToolTipText>().m_RawLocalizationKey = subscribedItem.Value.m_Description;
					fileSlot.SetOnToggleCallback(SlotToggleCallback);
					fileSlot.m_Toggle.isOn = Mods.ModIsActive(subscribedItem.Value.m_ID);
				}
			}
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
			Mods.ActivateMod(fileName);
		}
		else
		{
			Mods.DeactivateMod(fileName);
		}
		m_SaveProfileOnExit = true;
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

	private void UpdateVisibility()
	{
		m_DeactivateAllModsButton.gameObject.SetActive(m_FileLoader.m_Slots.Count > 0);
		m_LeftSideHelpText.SetActive(m_FileLoader.m_Slots.Count > 0);
		m_MiddleHelpText.SetActive(m_FileLoader.m_Slots.Count == 0);
	}

	private void OnDeactivateAllMods()
	{
		PopUpMessage.DisplayConfirmation(Localize.Get("UI_MODS_DEACTIVATE_ALL_CONFIRM"), useYesNoLabels: true, DoDeactivateAllMods);
	}

	private void DoDeactivateAllMods()
	{
		foreach (FileSlot slot in m_FileLoader.m_Slots)
		{
			Mods.DeactivateMod(Path.GetFileName(slot.m_FileName));
			slot.m_Toggle.isOn = false;
			m_SaveProfileOnExit = true;
		}
		GameUI.m_Instance.m_Workshop.ClearActiveModIcons();
	}

	private void ForceGamepadCursorToSelecctedSlot()
	{
		if (GameInput.GetActiveGameDevice() == GameDevice.Gamepad && m_SelectedSlotIndex != -1)
		{
			GameInput.SetVirtualMousePosition(m_FileLoader.FindSlotByIndex(m_SelectedSlotIndex).m_AsteriskIcon.transform.position);
		}
	}
}
