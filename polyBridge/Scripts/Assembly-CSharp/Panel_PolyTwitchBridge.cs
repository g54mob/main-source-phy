using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_PolyTwitchBridge : MonoBehaviour
{
	[Header("Text")]
	public TextMeshProUGUI m_UserName;

	public TextMeshProUGUI m_BridgeCostText;

	[Header("Images")]
	public RawImage m_ThumbnailRawImage;

	[Header("Cycle Buttons")]
	public Button m_NextSuggestion;

	public Button m_PrevSuggestion;

	[Header("Menu Buttons")]
	public Button m_LoadBridgeButton;

	public Button m_SkipSuggestionButton;

	public Button m_DiscardSuggestionButton;

	public Button m_MuteButton;

	public Button m_CloseButton;

	[NonSerialized]
	public PolyTwitchSuggestion m_Suggestion;

	private BridgeSaveData m_RestoreBridgeSaveData;

	private void OnEnable()
	{
		m_NextSuggestion.onClick.AddListener(OnNextSuggestion);
		m_PrevSuggestion.onClick.AddListener(OnPrevSuggestion);
		m_LoadBridgeButton.onClick.AddListener(OnLoadBridge);
		m_SkipSuggestionButton.onClick.AddListener(OnSkipSuggestion);
		m_DiscardSuggestionButton.onClick.AddListener(OnDiscardSuggestion);
		m_MuteButton.onClick.AddListener(OnBanPlayer);
		m_CloseButton.onClick.AddListener(Close);
		m_BridgeCostText.text = string.Empty;
		UpdateNextPrevButtons();
		ActivePanels.Add(base.gameObject);
	}

	private void OnDisable()
	{
		m_NextSuggestion.onClick.RemoveAllListeners();
		m_PrevSuggestion.onClick.RemoveAllListeners();
		m_LoadBridgeButton.onClick.RemoveAllListeners();
		m_SkipSuggestionButton.onClick.RemoveAllListeners();
		m_DiscardSuggestionButton.onClick.RemoveAllListeners();
		m_MuteButton.onClick.RemoveAllListeners();
		m_CloseButton.onClick.RemoveAllListeners();
		ActivePanels.Remove(base.gameObject);
	}

	private void Update()
	{
		ProcessInput();
		m_DiscardSuggestionButton.gameObject.SetActive(!PolyTwitchAutoPlay.m_Running);
		if (m_Suggestion != null)
		{
			if (!m_Suggestion.HasBeenViewed())
			{
				m_Suggestion.SetStatus(PolyTwitchSuggestionStatus.VIEWED);
				PolyTwitchSuggestions.UpdateSuggestionPositionInList(m_Suggestion);
			}
			GameUI.SetAndEnableText(m_UserName, m_Suggestion.m_Username);
		}
		UpdateNextPrevButtons();
	}

	public void ViewSuggestion(PolyTwitchSuggestion suggestion)
	{
		m_Suggestion = suggestion;
		m_UserName.text = suggestion.m_Username;
		m_BridgeCostText.text = string.Empty;
		m_RestoreBridgeSaveData = BridgeSave.Serialize();
		PolyTwitch.m_IsTakingScreenshot = true;
		WorkshopPreview.Create(showBridge: true, showPrebuilds: true, PointOfViewType.SIM_CENTER_PITCHED_DOWN, GameStateManager.GetState(), suggestion.m_BridgeSaveData, OnOverlayCaptured, OnCreatePreviewComplete);
	}

	public void MaybeAutoSaveCurrentBridge()
	{
		BridgeSaveData bridgeSaveData = BridgeSave.Serialize();
		string text = Utils.MD5HashFor(bridgeSaveData.SerializeBinary());
		if (PolyTwitch.m_LastLoadedSuggestion != null && text == PolyTwitch.m_LastLoadedSuggestion.m_BridgeHash)
		{
			return;
		}
		PolyTwitchHistorySlot mostRecentSaveSlot = GameUI.m_Instance.m_PolyTwitchMain.m_HistoryPanel.GetMostRecentSaveSlot();
		if (!mostRecentSaveSlot || ((bool)mostRecentSaveSlot && mostRecentSaveSlot.m_AutoSave.m_BridgeSaveDataHash != text))
		{
			if (PolyTwitchAutoSaves.m_Saves.Count == PolyTwitchAutoSaves.MAX_SAVES)
			{
				PolyTwitchAutoSaves.RemoveOldestAutoSave();
			}
			PolyTwitchAutoSaves.Create(bridgeSaveData);
		}
	}

	private void UpdateNextPrevButtons()
	{
	}

	private void OnNextSuggestion()
	{
		PolyTwitchSuggestion nextSuggestion = PolyTwitchSuggestions.GetNextSuggestion(m_Suggestion);
		if (nextSuggestion != null && !PolyTwitch.m_IsTakingScreenshot)
		{
			ViewSuggestion(nextSuggestion);
			InterfaceAudio.Play("ui_nextLevel");
		}
		else
		{
			InterfaceAudio.PlayErrorBeep();
		}
	}

	private void OnPrevSuggestion()
	{
		PolyTwitchSuggestion prevSuggestion = PolyTwitchSuggestions.GetPrevSuggestion(m_Suggestion);
		if (prevSuggestion != null && !PolyTwitch.m_IsTakingScreenshot)
		{
			ViewSuggestion(prevSuggestion);
			InterfaceAudio.Play("ui_previousLevel");
		}
		else
		{
			InterfaceAudio.PlayErrorBeep();
		}
	}

	private void OnLoadBridge()
	{
		if (GameStateManager.GetState() == GameState.SIM)
		{
			if (PolyTwitchAutoPlay.m_Running)
			{
				PolyTwitchAutoPlay.TurnOff();
			}
			GameStateBuild.m_LoadSuggestionOnEnter = m_Suggestion;
			GameUI.m_Instance.m_TopBar.OnExitSim();
			Close();
		}
		else
		{
			MaybeAutoSaveCurrentBridge();
			Bridge.ClearAndLoad(m_Suggestion.m_BridgeSaveData);
			PolyTwitch.m_LastLoadedSuggestion = m_Suggestion;
			Close();
		}
	}

	private void AutoSaveCurrentBridge()
	{
	}

	private void OnSkipSuggestion()
	{
		if (PolyTwitch.m_IsTakingScreenshot)
		{
			InterfaceAudio.PlayErrorBeep();
			return;
		}
		PolyTwitchSuggestion nextSuggestion = PolyTwitchSuggestions.GetNextSuggestion(m_Suggestion);
		if (nextSuggestion != null)
		{
			ViewSuggestion(nextSuggestion);
			InterfaceAudio.Play("ui_menu_select");
		}
		else
		{
			Close();
		}
	}

	private void OnDiscardSuggestion()
	{
		PolyTwitchSuggestion nextSuggestion = PolyTwitchSuggestions.GetNextSuggestion(m_Suggestion);
		PolyTwitchSuggestions.Delete(m_Suggestion);
		if (nextSuggestion != null)
		{
			ViewSuggestion(nextSuggestion);
			InterfaceAudio.Play("ui_menu_select");
		}
		else
		{
			Close();
		}
	}

	private void OnBanPlayer()
	{
		if (PolyTwitch.m_IsTakingScreenshot)
		{
			InterfaceAudio.PlayErrorBeep();
			return;
		}
		PolyTwitchBans.BanPlayer(m_Suggestion.m_Username, m_Suggestion.m_OwnerId);
		PolyTwitchSuggestion nextSuggestion = PolyTwitchSuggestions.GetNextSuggestion(m_Suggestion);
		if (nextSuggestion != null)
		{
			ViewSuggestion(nextSuggestion);
			InterfaceAudio.Play("ui_menu_select");
		}
		else
		{
			Close();
		}
		Profiles.SaveActiveProfile();
	}

	private void Close()
	{
		GameUI.m_Instance.m_PolyTwitchBridge.gameObject.SetActive(value: false);
		InterfaceAudio.Play("ui_menu_cancel");
	}

	private void ProcessInput()
	{
		if (!GameStateCommonInput.IgnoreKeyboardInputForPanel(base.gameObject) && (Input.GetKeyDown(KeyCode.Escape) || GamepadManager.ButtonJustPressed(GamepadButtonType.EAST)))
		{
			Close();
		}
	}

	private void OnOverlayCaptured(BridgeSaveData suggestion)
	{
		Bridge.ClearAndLoad(suggestion);
	}

	public void OnCreatePreviewComplete()
	{
		PolyTwitch.m_IsTakingScreenshot = false;
		m_ThumbnailRawImage.texture = WorkshopPreview.m_PreviewTexture2D;
		m_ThumbnailRawImage.gameObject.SetActive(value: true);
		Budget.UpdateBridgeCost();
		m_BridgeCostText.text = Utils.FormatCash(Mathf.RoundToInt(Budget.m_BridgeCost));
		Bridge.ClearAndLoad(m_RestoreBridgeSaveData);
	}
}
