using UnityEngine;
using UnityEngine.UI;

public class Panel_Help : MonoBehaviour
{
	public Button m_CancelButton;

	public Panel_HelpSlot[] m_Slots;

	private bool m_AllSlotsPreparedActionTaken;

	private int m_FramesSinceAllPrepared;

	public void Awake()
	{
		m_CancelButton.onClick.AddListener(OnCancel);
	}

	public void PauseHelpSlotsExcept(Panel_HelpSlot ignoreSlot)
	{
		Panel_HelpSlot[] slots = m_Slots;
		foreach (Panel_HelpSlot panel_HelpSlot in slots)
		{
			if (panel_HelpSlot != ignoreSlot && panel_HelpSlot.m_VideoPlayer.isPlaying)
			{
				panel_HelpSlot.m_VideoPlayer.Pause();
				panel_HelpSlot.m_PlayButton.TurnOn(on: true);
			}
		}
	}

	public void SlotClickedCallback(Panel_HelpSlot clickedSlot)
	{
		Panel_HelpSlot[] slots = m_Slots;
		foreach (Panel_HelpSlot panel_HelpSlot in slots)
		{
			if (panel_HelpSlot == clickedSlot)
			{
				panel_HelpSlot.SetHighlight(!panel_HelpSlot.IsHighlighted());
				BridgeShadow.Clear();
				if (panel_HelpSlot.IsHighlighted())
				{
					BridgeShadow.Show(panel_HelpSlot.GetBridgeSaveData());
					InterfaceAudio.Play("ui_menubar_gen_on");
				}
				else
				{
					InterfaceAudio.Play("ui_menubar_gen_off");
				}
			}
			else
			{
				panel_HelpSlot.SetHighlight(on: false);
			}
		}
	}

	private void Update()
	{
		if (!m_AllSlotsPreparedActionTaken && AllSlotsPrepared())
		{
			m_FramesSinceAllPrepared++;
			if (m_FramesSinceAllPrepared > 2)
			{
				m_AllSlotsPreparedActionTaken = true;
				int num = 0;
				Panel_HelpSlot[] slots = m_Slots;
				foreach (Panel_HelpSlot panel_HelpSlot in slots)
				{
					if (panel_HelpSlot.m_PreparedForLevelID == Game.GetLevelId())
					{
						panel_HelpSlot.Hide(hide: false);
						num++;
					}
					else
					{
						panel_HelpSlot.gameObject.SetActive(value: false);
					}
				}
				if (num == 0)
				{
					Close();
					OverlayGallery();
				}
				BridgeJointPlacement.CancelSelection();
				BridgeJointMovement.CancelSelection();
				ClipboardManager.ClearClipboard();
			}
		}
		ProcessInput();
	}

	private bool AllSlotsPrepared()
	{
		Panel_HelpSlot[] slots = m_Slots;
		for (int i = 0; i < slots.Length; i++)
		{
			if (slots[i].m_WaitingForPrepare)
			{
				return false;
			}
		}
		return true;
	}

	public void OnEnable()
	{
		ActivePanels.Add(base.gameObject);
		m_AllSlotsPreparedActionTaken = false;
		m_FramesSinceAllPrepared = 0;
		GeneratePreviews();
		GameUI.m_Instance.m_GamepadLegend.HideButtonsLeft();
		GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, Localize.Get("UI_SELECT"), GamepadButtonType.EAST, Localize.Get("UI_CLOSE"));
	}

	public void OnDisable()
	{
		ActivePanels.Remove(base.gameObject);
	}

	public void Show()
	{
		if (!base.gameObject.activeInHierarchy)
		{
			base.gameObject.SetActive(value: true);
		}
	}

	public void Close()
	{
		GameUI.m_Instance.m_Help.gameObject.SetActive(value: false);
	}

	private void OnCancel()
	{
		InterfaceAudio.Play("ui_window_close");
		Close();
	}

	public bool SlotsHavePreviews()
	{
		for (int i = 0; i < m_Slots.Length; i++)
		{
			if (!m_Slots[i].PreviewFilenameExists(i, Game.GetLevelFilename()))
			{
				return false;
			}
		}
		return true;
	}

	private void GeneratePreviews()
	{
		for (int i = 0; i < m_Slots.Length; i++)
		{
			m_Slots[i].gameObject.SetActive(value: true);
			if (!(m_Slots[i].m_PreparedForLevelID == Game.GetLevelId()))
			{
				m_Slots[i].m_WaitingForPrepare = m_Slots[i].GeneratePreview(i, Game.GetLevelId(), Game.GetLevelFilename(), GenerateCompleteCallback, SlotClickedCallback);
				m_Slots[i].Hide(hide: true);
			}
		}
	}

	private void GenerateCompleteCallback(Panel_HelpSlot slot, string levelID)
	{
		slot.m_WaitingForPrepare = false;
		slot.m_PreparedForLevelID = levelID;
	}

	private void OverlayGallery()
	{
		Gallery.LaunchForCurrentLevel();
	}

	private void ProcessInput()
	{
		if (!GameStateCommonInput.IgnoreKeyboardInputForPanel(base.gameObject) && (Input.GetKeyDown(KeyCode.Escape) || GamepadManager.ButtonJustPressed(GamepadButtonType.EAST)))
		{
			if (GamepadManager.ButtonJustPressed(GamepadButtonType.EAST))
			{
				Game.ForceIgnoreNextSelection();
			}
			OnCancel();
		}
	}
}
