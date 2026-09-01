using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using DarkTonic.MasterAudio;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Panel_LevelComplete : MonoBehaviour
{
	[Header("Panels")]
	public RectTransform m_Root;

	public RectTransform m_LevelCompleteRectTransform;

	public Panel_Replay m_ReplayPanel;

	public Panel_LevelCompleteGallery m_GalleryPanel;

	public Panel_LevelCompleteLeaderboard m_LeaderboardPanel;

	[Header("Header")]
	public TextMeshProUGUI m_LevelNameText;

	public Banner m_Banner;

	[Header("Max Stress")]
	public TextMeshProUGUI m_MassStressLabel;

	public TextMeshProUGUI m_MassStressValue;

	public TextMeshProUGUI m_MassStressHasBreaks;

	[Header("Cost")]
	public TextMeshProUGUI m_CostLabel;

	public TextMeshProUGUI m_CostValue;

	public TextMeshProUGUI m_CostOverBudgetBy;

	[Header("Mass")]
	public TextMeshProUGUI m_MassValue;

	[Header("Completion")]
	public RectTransform m_LevelCompletedOn;

	public RectTransform m_UnderBudgetOn;

	public RectTransform m_UnderBudgetOff;

	public RectTransform m_UnderBudgetAndUnbreakingOn;

	public RectTransform m_UnderBudgetAndUnbreakingOff;

	[Header("Replay")]
	public TextMeshProUGUI m_NoReplaysText;

	[Header("Buttons")]
	public Button m_RetryButton;

	public Button m_NextLevelButton;

	public Button m_ExitButton;

	public Button m_ShareButton;

	public ToolTipText m_ExitToolTipText;

	public GameObject m_NextLevelButtonWaiting;

	[Header("Voting")]
	public Panel_WorkshopVoting m_Voting;

	private bool m_Open;

	private bool m_SwitchToMainMenuOnClose;

	private float m_NumbersAnimateStartTime;

	private readonly float COST_ANIMATE_DURATION_SECONDS = 1.3f;

	private readonly float STRESS_ANIMATE_DURATION_SECONDS = 1f;

	private readonly float WEIGHT_ANIMATE_DURATION_SECONDS = 0.5f;

	private static PlaySoundResult m_CounterAudioInstance;

	private WorkshopCampaignLevel m_NextWorkshopLevel;

	private void Start()
	{
		m_RetryButton.onClick.AddListener(OnRetry);
		m_NextLevelButton.onClick.AddListener(OnNextLevel);
		m_ExitButton.onClick.AddListener(OnExit);
		m_ShareButton.onClick.AddListener(OnShare);
	}

	private void OnEnable()
	{
		ActivePanels.Add(base.gameObject);
		m_Root.anchoredPosition = new Vector2(0f, Game.IsRunningOnSteamDeck() ? (-25) : (-30));
		m_LevelCompleteRectTransform.sizeDelta = new Vector2(m_LevelCompleteRectTransform.sizeDelta.x, CampaignTutorial.IsRunning() ? 360 : 590);
		GameUI.m_Instance.m_GamepadLegend.HideButtonsLeft();
		ShowGamepadLegend();
		m_SwitchToMainMenuOnClose = false;
	}

	private void OnDisable()
	{
		ActivePanels.Remove(base.gameObject);
		StopLoopingCounterAudio();
		PreviewCache.FlushPreviewsOverCacheLimit();
		GameUI.m_Instance.m_GamepadLegend.HideButtons();
	}

	private void SetStateForNextLevelButton()
	{
		bool active = false;
		if (GameManager.GetGameMode() == GameMode.CAMPAIGN && Campaign.GetNextLayoutFilename() != string.Empty)
		{
			active = true;
		}
		else if (GameManager.GetGameMode() == GameMode.WORKSHOP)
		{
			WorkshopCampaign workshopCampaign = WorkshopCampaigns.Get(WorkshopCampaigns.m_ActiveWorkshopCampaignModId);
			if (workshopCampaign != null && workshopCampaign.GetNextLevelId() != string.Empty)
			{
				active = true;
			}
		}
		m_NextLevelButton.gameObject.SetActive(active);
	}

	public void Open()
	{
		if (m_Open)
		{
			return;
		}
		ShowWorkshopDownloading(on: false);
		base.gameObject.SetActive(value: true);
		SetStateForNextLevelButton();
		SetExitButtonTooltip();
		bool flag = m_ReplayPanel.Show(!GameUI.m_Instance.m_PolyTwitchMain.m_AutoPlayPanel.gameObject.activeInHierarchy);
		m_ReplayPanel.gameObject.SetActive(flag);
		m_ReplayPanel.DisableTimelineMarkers();
		m_NoReplaysText.gameObject.SetActive(!flag && !Game.IsCurrentLevelTutorial());
		m_ShareButton.gameObject.SetActive(flag);
		if (GameManager.GetGameMode() == GameMode.CAMPAIGN && Campaign.m_CurrentLevel != null)
		{
			TextMeshProUGUI levelNameText = m_LevelNameText;
			string text = (m_LevelNameText.text = Campaign.m_CurrentLevel.GetFullNameFormatted());
			levelNameText.text = text;
		}
		else
		{
			m_LevelNameText.text = Game.GetLevelTitle();
		}
		m_Banner.Refresh();
		m_NumbersAnimateStartTime = Time.realtimeSinceStartup;
		SetCost();
		SetMaxStress();
		SetMass();
		StartLoopingCounterAudio();
		SetCompletionIcons();
		bool num = Game.IsCurrentLevelTutorial();
		m_LeaderboardPanel.m_NoLeaderboardsInWorkshopText.gameObject.SetActive(value: false);
		if (num)
		{
			m_GalleryPanel.gameObject.SetActive(value: false);
			m_LeaderboardPanel.gameObject.SetActive(value: false);
			m_ShareButton.gameObject.SetActive(value: false);
		}
		else if (GameManager.GameModeIsCampaignOrWorkshop())
		{
			m_GalleryPanel.Open();
			m_LeaderboardPanel.gameObject.SetActive(value: true);
			if (GameLeaderboards.CurrentLevelAllowsLeaderboards())
			{
				m_LeaderboardPanel.m_Root.gameObject.SetActive(value: true);
				m_LeaderboardPanel.Open();
			}
			else
			{
				m_LeaderboardPanel.m_Root.gameObject.SetActive(value: false);
			}
		}
		else
		{
			m_GalleryPanel.gameObject.SetActive(value: false);
			m_LeaderboardPanel.gameObject.SetActive(value: false);
		}
		if (IsWorkshopLevelWithVoting())
		{
			m_Voting.gameObject.SetActive(value: true);
			m_Voting.UpdateVoteIcons(Workshop.m_LastPlayedWorkshopItem);
			m_Voting.UpdateFavoriteButtons(Workshop.m_LastPlayedWorkshopItem);
		}
		else
		{
			m_Voting.gameObject.SetActive(value: false);
		}
		m_Open = true;
	}

	public void Close()
	{
		if (m_Open)
		{
			if (GameManager.GameModeIsCampaignOrWorkshop())
			{
				m_GalleryPanel.Close();
				m_LeaderboardPanel.Close();
			}
			m_ReplayPanel.Hide();
			base.gameObject.SetActive(value: false);
			if (m_SwitchToMainMenuOnClose)
			{
				GameStateManager.SwitchToState(GameState.MAIN_MENU);
			}
			if (IsWorkshopLevelWithVoting())
			{
				m_Voting.MaybeWriteWorkshopItemVotes(Workshop.m_LastPlayedWorkshopItem);
				m_Voting.MaybeSyncVoteToSteam(Workshop.m_LastPlayedWorkshopItem);
				m_Voting.MaybeWriteWorkshopItemFavorite(Workshop.m_LastPlayedWorkshopItem);
				m_Voting.MaybeSyncFavoriteToSteam(Workshop.m_LastPlayedWorkshopItem);
			}
			m_Open = false;
		}
	}

	private void Update()
	{
		m_Root.gameObject.SetActive(!GameUI.m_Instance.m_ShareReplay.gameObject.activeInHierarchy);
		ProcessInput();
		SetCost();
		SetMaxStress();
		SetMass();
		if (ActivePanels.IsTopPanel(base.gameObject))
		{
			ShowGamepadLegend();
		}
		if (CostHasStoppedCountingUp())
		{
			StopLoopingCounterAudio();
		}
	}

	private void SetExitButtonTooltip()
	{
		if (GameManager.GetGameMode() == GameMode.CAMPAIGN)
		{
			m_ExitToolTipText.m_LocalizationKey = ToolTipLocalizationKey.PAUSEMENU_EXIT_TO_CAMPAIGN_MENU;
		}
		else if (GameManager.GetGameMode() == GameMode.WORKSHOP && Workshop.m_LastPlayedWorkshopItem != null && !WeeklyChallenges.IsAWeeklyChallenge(Workshop.m_LastPlayedWorkshopItem.GetId()))
		{
			m_ExitToolTipText.m_LocalizationKey = ToolTipLocalizationKey.TOOLTIP_EXIT_TO_WORKSHOP;
		}
		else
		{
			m_ExitToolTipText.m_LocalizationKey = ToolTipLocalizationKey.TOOLTIP_MAIN_MENU;
		}
	}

	private void OnCancel()
	{
		GameUI.m_Instance.m_TopBar.OnExitSimSilent();
		InterfaceAudio.Play("ui_menu_cancel");
		Close();
	}

	private void Retry()
	{
		if (!IsWorkshopDownloading())
		{
			if (Game.IsCurrentLevelTutorial())
			{
				GameStateSim.m_SkipBridgeRestoreOnExit = true;
				GameStateManager.SwitchToStateImmediate(GameState.BUILD);
				Campaign.LoadLevel(Campaign.m_CurrentLevel);
				CampaignTutorial.Start(Campaign.m_CurrentLevel.GetCampaignTutorialType());
			}
			else
			{
				GameUI.m_Instance.m_TopBar.OnExitSimSilent();
				InterfaceAudio.Play("ui_fail_tryAgain");
			}
			Close();
		}
	}

	private void OnRetry()
	{
		Campaign.ClearLastSolvedCampaignLevelId();
		Retry();
	}

	private void OnNextLevel()
	{
		if (!IsWorkshopDownloading())
		{
			if (PolyTwitch.SessionIsActiveWithUnviewedSuggestions())
			{
				PolyTwitch.ConfirmLeaveLevel(MoveToNextLevel);
			}
			else
			{
				MoveToNextLevel();
			}
		}
	}

	private void OnExit()
	{
		if (IsWorkshopDownloading())
		{
			return;
		}
		if (GameManager.GetGameMode() == GameMode.WORKSHOP && Workshop.m_LastPlayedWorkshopItem != null && !WeeklyChallenges.IsAWeeklyChallenge(Workshop.m_LastPlayedWorkshopItem.GetId()))
		{
			ExitToWorkshop();
			return;
		}
		if (GameManager.GetGameMode() == GameMode.SANDBOX && Sandbox.m_UnsavedChanges)
		{
			InterfaceAudio.Play("ui_window_open");
			PopUpMessage.DisplayWarning(Localize.Get("POPUP_EXIT_SANDBOX_LOSE_CHANGES"), useYesNoLables: true, ExitToMainMenu);
			return;
		}
		if (GameManager.GetGameMode() == GameMode.CAMPAIGN && Campaign.m_CurrentLevel != null)
		{
			GameStateMainMenu.m_LoadCampaignPanelForWorldID = Campaign.m_CurrentLevel.m_WorldId;
		}
		ExitToMainMenu();
	}

	private void ExitToMainMenu()
	{
		m_SwitchToMainMenuOnClose = true;
		OnCancel();
	}

	private void ExitToWorkshop()
	{
		OnCancel();
		GameUI.m_Instance.m_Workshop.Open(WorkshopView.LEVELS_AND_CAMPAIGNS);
		WorkshopCampaign workshopCampaign = WorkshopCampaigns.Get(WorkshopCampaigns.m_ActiveWorkshopCampaignModId);
		if (workshopCampaign != null)
		{
			GameUI.m_Instance.m_Workshop.m_WorkshopCampaignPanel.Open(workshopCampaign, Workshop.m_LastPlayedWorkshopItem.GetId(), string.Empty);
		}
	}

	private void MoveToNextLevel()
	{
		if (GameManager.GetGameMode() == GameMode.WORKSHOP)
		{
			MoveToNextLevelWorkshop();
		}
		else
		{
			MoveToNextLevelCampaign();
		}
	}

	private void MoveToNextLevelCampaign()
	{
		CampaignWorld worldWithLevelId = CampaignWorlds.m_Instance.GetWorldWithLevelId(Campaign.m_CurrentLevel.m_Id);
		if (CampaignWorlds.m_Instance.IsLevelLastInWorld(Campaign.m_CurrentLevel.m_Id) && !worldWithLevelId.m_AdvanceToNextWorldAutomatically)
		{
			GameStateMainMenu.m_LoadCampaignPanelForWorldID = Campaign.m_CurrentLevel.m_WorldId;
			ExitToMainMenu();
		}
		else if (Campaign.LoadNextLevel())
		{
			InterfaceAudio.Play("ui_window_close");
			Close();
		}
		else
		{
			InterfaceAudio.PlayErrorBeep();
		}
	}

	private void MoveToNextLevelWorkshop()
	{
		WorkshopCampaign workshopCampaign = WorkshopCampaigns.Get(WorkshopCampaigns.m_ActiveWorkshopCampaignModId);
		if (workshopCampaign == null)
		{
			InterfaceAudio.PlayErrorBeep();
			return;
		}
		string nextLevelId = workshopCampaign.GetNextLevelId();
		m_NextWorkshopLevel = workshopCampaign.GetWorldWithLevelId(nextLevelId).GetLevel(nextLevelId);
		if (Utils.FileExists(m_NextWorkshopLevel.m_WorkshopItem.GetLevelLayoutPathAndFilename()))
		{
			PlayNextWorkshop(success: true);
			return;
		}
		ShowWorkshopDownloading(on: true);
		m_NextWorkshopLevel.m_WorkshopItem.DownloadFromSteam(PlayNextWorkshop);
	}

	private void ShowWorkshopDownloading(bool on)
	{
		m_NextLevelButton.gameObject.SetActive(!on);
		m_NextLevelButtonWaiting.SetActive(on);
	}

	private bool IsWorkshopDownloading()
	{
		return m_NextLevelButtonWaiting.activeInHierarchy;
	}

	private void PlayNextWorkshop(bool success)
	{
		ShowWorkshopDownloading(on: false);
		if (!success)
		{
			PopUpMessage.DisplayWarningOkOnly(Localize.Get("WARN_WORKSHOP_DOWNLOAD_FAIL"));
		}
		else if (m_NextWorkshopLevel != null && m_NextWorkshopLevel.m_WorkshopItem != null)
		{
			List<string> inactiveModsInLayout = Mods.GetInactiveModsInLayout(m_NextWorkshopLevel.m_WorkshopItem.GetLevelLayoutPathAndFilename());
			if (inactiveModsInLayout.Count > 0)
			{
				GameUI.m_Instance.m_ModsRequiredPopup.Open(inactiveModsInLayout, null, DoPlayAfterModCheck);
				return;
			}
			Mods.DeactivateAutoLoadedMods();
			DoPlayAfterModCheck(null);
		}
	}

	private void DoPlayAfterModCheck(FileSlot slot)
	{
		BridgeCheat.Clear();
		BridgeCheat.m_ForceUnlimitedBudget = false;
		BridgeCheat.m_ForceUnlimitedMaterial = false;
		if (m_NextWorkshopLevel != null && m_NextWorkshopLevel.m_WorkshopItem != null)
		{
			GameStatePreloadingAssets.PreloadLevel(m_NextWorkshopLevel.m_WorkshopItem.GetLevelLayoutPathAndFilename(), slot, PreloadOpenLevelCallback);
		}
	}

	private void PreloadOpenLevelCallback(string layoutPath, FileSlot slot)
	{
		if (m_NextWorkshopLevel != null && m_NextWorkshopLevel.m_WorkshopItem != null)
		{
			BridgeCheat.Clear();
			Workshop.PlayLevel(m_NextWorkshopLevel.m_WorkshopItem, layoutPath, GameSubMode.NONE);
			Close();
		}
	}

	private void OnShare()
	{
		if (!IsWorkshopDownloading())
		{
			GameUI.m_Instance.m_ShareReplay.Show();
			InterfaceAudio.Play("ui_fail_share");
		}
	}

	private void ProcessInput()
	{
		if (!GameStateCommonInput.IgnoreKeyboardInputForPanel(base.gameObject))
		{
			if (GameInput.JustPressed(BindingType.START_SIM) || Input.GetKeyDown(KeyCode.Escape) || GamepadManager.ButtonJustPressed(GamepadButtonType.EAST))
			{
				Retry();
			}
			else if (m_NextLevelButton.gameObject.activeInHierarchy && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter) || GamepadManager.ButtonJustPressed(GamepadButtonType.WEST)))
			{
				OnNextLevel();
			}
			else if (m_RetryButton.gameObject.activeInHierarchy && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
			{
				OnRetry();
			}
			else if (GamepadManager.ButtonJustPressed(GamepadButtonType.NORTH))
			{
				OnShare();
			}
			if (GamepadManager.ButtonJustPressed(GamepadButtonType.SHOULDER_RIGHT))
			{
				CycleToNextLeaderboardType();
			}
			if (GamepadManager.ButtonJustPressed(GamepadButtonType.SHOULDER_LEFT))
			{
				CycleToPrevLeaderboardType();
			}
			if (GamepadManager.ButtonJustPressed(GamepadButtonType.DPAD_UP))
			{
				CycleToNextFilter();
			}
			if (GamepadManager.ButtonJustPressed(GamepadButtonType.DPAD_DOWN))
			{
				CycleToPrevFilter();
			}
		}
	}

	private void CycleToNextLeaderboardType()
	{
		if (m_LeaderboardPanel.m_ShowAllButton.IsOn())
		{
			ExecuteEvents.Execute(m_LeaderboardPanel.m_ShowUnbreakingButton.m_Button.gameObject, new BaseEventData(Main.m_Instance.m_EventSystem), ExecuteEvents.submitHandler);
		}
		else if (m_LeaderboardPanel.m_ShowUnbreakingButton.IsOn())
		{
			ExecuteEvents.Execute(m_LeaderboardPanel.m_ShowLowestStressButton.m_Button.gameObject, new BaseEventData(Main.m_Instance.m_EventSystem), ExecuteEvents.submitHandler);
		}
		else if (m_LeaderboardPanel.m_ShowLowestStressButton.IsOn())
		{
			ExecuteEvents.Execute(m_LeaderboardPanel.m_ShowAllButton.m_Button.gameObject, new BaseEventData(Main.m_Instance.m_EventSystem), ExecuteEvents.submitHandler);
		}
	}

	private void CycleToPrevLeaderboardType()
	{
		if (m_LeaderboardPanel.m_ShowAllButton.IsOn())
		{
			ExecuteEvents.Execute(m_LeaderboardPanel.m_ShowLowestStressButton.m_Button.gameObject, new BaseEventData(Main.m_Instance.m_EventSystem), ExecuteEvents.submitHandler);
		}
		else if (m_LeaderboardPanel.m_ShowLowestStressButton.IsOn())
		{
			ExecuteEvents.Execute(m_LeaderboardPanel.m_ShowUnbreakingButton.m_Button.gameObject, new BaseEventData(Main.m_Instance.m_EventSystem), ExecuteEvents.submitHandler);
		}
		else if (m_LeaderboardPanel.m_ShowUnbreakingButton.IsOn())
		{
			ExecuteEvents.Execute(m_LeaderboardPanel.m_ShowAllButton.m_Button.gameObject, new BaseEventData(Main.m_Instance.m_EventSystem), ExecuteEvents.submitHandler);
		}
	}

	private void CycleToNextFilter()
	{
		if (m_LeaderboardPanel.m_TopScoresButton.IsOn())
		{
			ExecuteEvents.Execute(m_LeaderboardPanel.m_AroundYouScoresButton.m_Button.gameObject, new BaseEventData(Main.m_Instance.m_EventSystem), ExecuteEvents.submitHandler);
		}
		else if (m_LeaderboardPanel.m_AroundYouScoresButton.IsOn())
		{
			ExecuteEvents.Execute(m_LeaderboardPanel.m_FriendsScoresButton.m_Button.gameObject, new BaseEventData(Main.m_Instance.m_EventSystem), ExecuteEvents.submitHandler);
		}
		else if (m_LeaderboardPanel.m_FriendsScoresButton.IsOn())
		{
			ExecuteEvents.Execute(m_LeaderboardPanel.m_TopScoresButton.m_Button.gameObject, new BaseEventData(Main.m_Instance.m_EventSystem), ExecuteEvents.submitHandler);
		}
	}

	private void CycleToPrevFilter()
	{
		if (m_LeaderboardPanel.m_TopScoresButton.IsOn())
		{
			ExecuteEvents.Execute(m_LeaderboardPanel.m_FriendsScoresButton.m_Button.gameObject, new BaseEventData(Main.m_Instance.m_EventSystem), ExecuteEvents.submitHandler);
		}
		else if (m_LeaderboardPanel.m_AroundYouScoresButton.IsOn())
		{
			ExecuteEvents.Execute(m_LeaderboardPanel.m_TopScoresButton.m_Button.gameObject, new BaseEventData(Main.m_Instance.m_EventSystem), ExecuteEvents.submitHandler);
		}
		else if (m_LeaderboardPanel.m_FriendsScoresButton.IsOn())
		{
			ExecuteEvents.Execute(m_LeaderboardPanel.m_AroundYouScoresButton.m_Button.gameObject, new BaseEventData(Main.m_Instance.m_EventSystem), ExecuteEvents.submitHandler);
		}
	}

	private void SetMaxStress()
	{
		m_MassStressLabel.gameObject.SetActive(value: true);
		m_MassStressValue.gameObject.SetActive(value: true);
		m_MassStressHasBreaks.gameObject.SetActive(value: false);
		float num = Mathf.Clamp01((Time.realtimeSinceStartup - m_NumbersAnimateStartTime) / STRESS_ANIMATE_DURATION_SECONDS);
		bool flag = Mathf.Approximately(StressSamples.m_MaxStressNormalized, 1f) || SandboxSettings.m_Unbreakable;
		int num2 = GameLeaderboards.ConvertStressToScore(StressSamples.m_MaxStressNormalized);
		if (!flag)
		{
			num2 = Mathf.Clamp(num2, 0, 9999);
		}
		float num3 = Mathf.Lerp(0f, (float)num2 / 100f, Mathf.SmoothStep(0f, 1f, num));
		m_MassStressValue.text = ((num < 0.9999f) ? (Utils.FormatInteger(num3) + "%") : Utils.FormatStress(num3));
		m_MassStressValue.color = (flag ? GameUI.m_Instance.m_RedTextColor : Color.black);
	}

	private void SetCost()
	{
		float t = Mathf.Clamp01((Time.realtimeSinceStartup - m_NumbersAnimateStartTime) / COST_ANIMATE_DURATION_SECONDS);
		int num = Mathf.RoundToInt(Mathf.Lerp(0f, GameStateSim.m_BudgetUsed, Mathf.SmoothStep(0f, 1f, t)));
		m_CostValue.text = Utils.FormatCash(num);
		bool flag = num <= Mathf.RoundToInt(Budget.m_CashBudget);
		m_CostValue.color = (flag ? Color.black : GameUI.m_Instance.m_RedTextColor);
	}

	private void SetMass()
	{
		float num = Mathf.Clamp01((Time.realtimeSinceStartup - m_NumbersAnimateStartTime) / WEIGHT_ANIMATE_DURATION_SECONDS);
		float num2 = Mathf.Lerp(0f, GameStateSim.m_MassUsed * BridgePhysics.KgToPg, Mathf.SmoothStep(0f, 1f, num));
		if (Utils.IsInteger(num2) || num < 0.9999f)
		{
			m_MassValue.text = Utils.FormatIntegerMass(num2);
		}
		else
		{
			m_MassValue.text = Utils.FormatMass(num2);
		}
	}

	private async void SetCompletionIcons()
	{
		m_LevelCompletedOn.gameObject.SetActive(value: false);
		m_UnderBudgetOn.gameObject.SetActive(value: false);
		m_UnderBudgetOff.gameObject.SetActive(value: false);
		m_UnderBudgetAndUnbreakingOn.gameObject.SetActive(value: false);
		m_UnderBudgetAndUnbreakingOff.gameObject.SetActive(value: false);
		bool underBudget = GameStateSim.m_BudgetUsed <= Mathf.RoundToInt(Budget.m_CashBudget);
		try
		{
			await Task.Delay(300);
			m_LevelCompletedOn.gameObject.SetActive(value: true);
			m_LevelCompletedOn.DOScale(1.6f * Vector3.one, 0.3f).SetEase(Ease.InOutQuad).SetLoops(2, LoopType.Yoyo)
				.SetUpdate(isIndependentUpdate: true);
			InterfaceAudio.Play("ui_icons_pop");
			await Task.Delay(400);
			m_UnderBudgetOn.gameObject.SetActive(underBudget);
			m_UnderBudgetOff.gameObject.SetActive(!m_UnderBudgetOn.gameObject.activeSelf);
			if (underBudget)
			{
				m_UnderBudgetOn.DOScale(1.6f * Vector3.one, 0.3f).SetEase(Ease.InOutQuad).SetLoops(2, LoopType.Yoyo)
					.SetUpdate(isIndependentUpdate: true);
				InterfaceAudio.Play("ui_icons_pop");
			}
			await Task.Delay(400);
			m_UnderBudgetAndUnbreakingOn.gameObject.SetActive(underBudget && GameStateSim.m_NumBridgeBreaks == 0);
			m_UnderBudgetAndUnbreakingOff.gameObject.SetActive(!m_UnderBudgetAndUnbreakingOn.gameObject.activeSelf);
			if (m_UnderBudgetAndUnbreakingOn.gameObject.activeInHierarchy)
			{
				m_UnderBudgetAndUnbreakingOn.DOScale(1.6f * Vector3.one, 0.3f).SetEase(Ease.InOutQuad).SetLoops(2, LoopType.Yoyo)
					.SetUpdate(isIndependentUpdate: true);
				InterfaceAudio.Play("ui_icons_pop");
			}
		}
		catch (Exception ex)
		{
			Debug.LogWarning("HANDLED: " + ex.Message);
		}
	}

	private bool CostHasStoppedCountingUp()
	{
		return Mathf.Approximately(Mathf.Clamp01((Time.realtimeSinceStartup - m_NumbersAnimateStartTime) / COST_ANIMATE_DURATION_SECONDS), 1f);
	}

	private void StartLoopingCounterAudio()
	{
		m_CounterAudioInstance = MasterAudio.PlaySound("ui_counter_lp");
	}

	private void StopLoopingCounterAudio()
	{
		if (m_CounterAudioInstance != null)
		{
			try
			{
				m_CounterAudioInstance.ActingVariation.Stop();
			}
			catch (Exception ex)
			{
				Debug.LogWarning("Caught exception in StopLoopingCounterAudio: " + ex.Message);
			}
			m_CounterAudioInstance = null;
		}
	}

	private bool IsWorkshopLevelWithVoting()
	{
		if (GameManager.GetGameMode() == GameMode.WORKSHOP && Workshop.m_LastPlayedWorkshopItem != null)
		{
			return !WeeklyChallenges.IsAWeeklyChallenge(Workshop.m_LastPlayedWorkshopItem.GetId());
		}
		return false;
	}

	private void ShowGamepadLegend()
	{
		if (m_LeaderboardPanel.gameObject.activeInHierarchy)
		{
			GameUI.m_Instance.m_GamepadLegend.ShowButtonsLeft(GamepadButtonType.SHOULDER_LEFT, GamepadButtonType.SHOULDER_RIGHT, Localize.Get("KEY_TAB"), GamepadButtonType.DPAD_VERTICAL, Localize.Get("UI_CHANGE_FILTER"));
		}
		else
		{
			GameUI.m_Instance.m_GamepadLegend.HideButtonsLeft();
		}
		if (Game.IsCurrentLevelTutorial() || !Profiles.m_ActiveProfile.m_Replays)
		{
			if (m_NextLevelButton.gameObject.activeInHierarchy)
			{
				GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, Localize.Get("UI_SELECT"), GamepadButtonType.WEST, Localize.Get("TOOLTIP_NEXT_LEVEL_BUTTON"), GamepadButtonType.EAST, Localize.Get("TOOLTIP_RETRY"));
			}
			else
			{
				GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, Localize.Get("UI_SELECT"), GamepadButtonType.EAST, Localize.Get("TOOLTIP_RETRY"));
			}
		}
		else if (m_NextLevelButton.gameObject.activeInHierarchy)
		{
			GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, Localize.Get("UI_SELECT"), GamepadButtonType.WEST, Localize.Get("TOOLTIP_NEXT_LEVEL_BUTTON"), GamepadButtonType.NORTH, Localize.Get("TOOLTIP_SHARE"), GamepadButtonType.EAST, Localize.Get("TOOLTIP_RETRY"));
		}
		else
		{
			GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, Localize.Get("UI_SELECT"), GamepadButtonType.NORTH, Localize.Get("TOOLTIP_SHARE"), GamepadButtonType.EAST, Localize.Get("TOOLTIP_RETRY"));
		}
	}
}
