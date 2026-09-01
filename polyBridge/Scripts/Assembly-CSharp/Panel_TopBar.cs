using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_TopBar : MonoBehaviour
{
	[Header("Mode Toggles")]
	public ModeToggle m_ModeToggle;

	public RectTransform m_ModeToggleRectTransform;

	public ModeToggle m_DecorViewToggle;

	[Header("UndoRedo")]
	public GameObject m_SandboxUndoRedoPanel;

	public Button m_TrashButton;

	public Button m_UndoButton;

	public Button m_RedoButton;

	[Header("Simulation")]
	public BridgeSimSpeedSlider m_BridgeSimSpeedSlider;

	public GameObject m_ButtonContainerSpeed;

	public GameObject m_ButtonContainerPauseResume;

	public Button m_SimButton;

	public Button m_ExitSimButton;

	public Button m_PauseSimButton;

	public Button m_UnPauseSimButton;

	public Image m_SliderHandle;

	public TextMeshProUGUI m_SimSpeedLabel;

	[Header("Cheat")]
	public Image m_CheatImage;

	[Header("God Mode")]
	public GameObject m_GodModeParent;

	public Button m_GodModeButton;

	public Button m_GodModeSelectedButton;

	[Header("Show Decor")]
	public GameObject m_ShowDecorParent;

	public Button m_ShowDecorButton;

	public Button m_ShowDecorSelectedButton;

	[Header("LevelNav")]
	public GameObject m_LevelNavButtons;

	public Button m_NextLevel;

	public Button m_PrevLevel;

	[Header("Settings/Info")]
	public Button m_MainMenuButton;

	public Button m_LevelInfo;

	public GameObject m_LevelInfoTutorialArrow;

	public Button m_ReplayButton;

	public Button m_HelpButton;

	public Button m_GamepadHelpButton;

	public Button m_ClearPreviewButton;

	public Image m_HelpButtonImage;

	public RectTransform m_HelpButtonRectTransform;

	public RectTransform m_ClearPreviewContainerRectTransform;

	[Header("Cost")]
	public GameObject m_CostAndBudget;

	public PointerEvents m_OverbudgetPointerEvents;

	public TextMeshProUGUI m_CostText;

	public TextMeshProUGUI m_BudgetText;

	public RectTransform m_CostBudgetDivider;

	[Header("Cost")]
	public Image m_CostBar;

	public Image m_CostBarGold;

	public Image m_CostBarGreen;

	public Image m_CostBarGoldWrap;

	public Image m_CostBarRed;

	[Header("Cost Colors")]
	public Color m_CostGreen;

	public Color m_CostRed;

	public Color m_CostGold;

	public Color m_CostLightRed;

	[Header("Cost Colors Colorblind")]
	public Color m_CostGreenColorBlind;

	public Color m_CostGoldColorBlind;

	public Color m_CostRedColorBlind;

	public Color m_CostLightRedColorBlind;

	[Header("Photo Mode")]
	public GameObject m_PhotoModeParent;

	public TextMeshProUGUI m_PhotoModeHeaderText;

	public Button m_PhotoModeCancel;

	public Button m_RightView;

	public Button m_LeftView;

	public Button m_CenterPitchedDownView;

	public Button m_CenterView;

	[Header("Misc")]
	public ScreenMessage m_MessageTopLeft;

	public ScreenMessage m_MessageTopCenter;

	public GameObject m_LeaderboardReplayMode;

	public PointerEvents m_CheatIconPointerEvents;

	[NonSerialized]
	public bool m_PausedSim;

	private int m_LastSetBudgetForBudgetText = int.MaxValue;

	private int m_LastSetCostForBridgeCostText = int.MaxValue;

	public static bool m_LevelNavArrowsEnabled;

	private void Awake()
	{
		m_NextLevel.gameObject.SetActive(value: false);
		m_PrevLevel.gameObject.SetActive(value: false);
		m_PhotoModeParent.SetActive(value: false);
		m_DecorViewToggle.gameObject.SetActive(value: false);
		m_SandboxUndoRedoPanel.SetActive(value: false);
	}

	private void Start()
	{
		m_ModeToggle.SetCallback(ModeRefresh);
		m_DecorViewToggle.SetCallback(DecorViewRefresh);
		m_ReplayButton.onClick.AddListener(OnViewReplay);
		m_HelpButton.onClick.AddListener(OnHelp);
		m_GamepadHelpButton.onClick.AddListener(OnGamepadHelp);
		m_ClearPreviewButton.onClick.AddListener(OnClearPreview);
		m_TrashButton.onClick.AddListener(OnClear);
		m_UndoButton.onClick.AddListener(OnUndo);
		m_RedoButton.onClick.AddListener(OnRedo);
		m_RightView.onClick.AddListener(OnRightView);
		m_LeftView.onClick.AddListener(OnLeftView);
		m_CenterPitchedDownView.onClick.AddListener(OnCenterPitchedDownView);
		m_CenterView.onClick.AddListener(OnCenterView);
		m_PhotoModeCancel.onClick.AddListener(OnPhotoModeCancel);
		m_LevelInfoTutorialArrow.SetActive(value: false);
	}

	private void OnEnable()
	{
		UpdateGamepadHelp();
		m_CostBarGreen.gameObject.SetActive(value: false);
		m_CostBarRed.gameObject.SetActive(value: false);
		m_CostBarGold.gameObject.SetActive(value: false);
		m_CostBarGoldWrap.gameObject.SetActive(value: false);
	}

	public void UpdateManual()
	{
		ProcessInput();
		UpdateTooltips();
		UpdateCheatTooltip();
		UpdateClearPreviewButton();
		UpdateGamepadHelp();
		if (GameStateManager.GetState() == GameState.SANDBOX || GameStateManager.GetState() == GameState.DECOR)
		{
			GameUI.m_Instance.m_TopBar.UpdateSandboxUndoRedoButtons();
		}
		if ((GameStateManager.GetState() == GameState.BUILD || GameStateManager.GetState() == GameState.SANDBOX) && !WorkshopPreview.m_IsTakingScreenshot)
		{
			SetBudgetText(Budget.m_CashBudget);
			bool activeInHierarchy = m_CostBarRed.gameObject.activeInHierarchy;
			UpdateBridgeCost();
			if (!activeInHierarchy && m_CostBarRed.gameObject.activeInHierarchy)
			{
				InterfaceAudio.Play("ui_overBudget");
			}
		}
		m_ModeToggle.UpdateManual();
		m_DecorViewToggle.UpdateManual();
		m_CheatImage.gameObject.SetActive(BridgeCheat.m_Cheated || Mods.m_IsUsingGameplayMod);
	}

	public void SetSimSpeedLabel(float timeScale)
	{
		m_SimSpeedLabel.text = Mathf.RoundToInt(timeScale * 100f) + "%";
	}

	public void OnSim()
	{
		if (CameraInterpolate.IsActive())
		{
			return;
		}
		if (!PolyTwitchAutoPlay.m_SimStartedAutomatically)
		{
			GameUI.m_Instance.m_PolyTwitchMain.m_SettingsPanel.m_AutoPlayToggle.isOn = false;
			if (Profiles.m_ActiveProfile.m_TwitchAutoPlay)
			{
				Profiles.m_ActiveProfile.m_TwitchAutoPlay = false;
				Profiles.SaveActiveProfile();
			}
		}
		BridgeTrace.CompleteFillingInstantly();
		Budget.UpdateBridgeCost();
		UpdateBridgeCost();
		GameStateManager.SwitchToState(GameState.SIM);
		AudioMixerManager.UnPauseSimulationSFX();
		InterfaceAudio.Play("ui_simulation_start");
	}

	public void OnClickExitSim()
	{
		if (PolyTwitchAutoPlay.m_Running)
		{
			PolyTwitchAutoPlay.TurnOff();
		}
		OnExitSim();
	}

	public void OnSandboxEnter()
	{
		m_LastSetCostForBridgeCostText = int.MaxValue;
	}

	public void OnExitSim()
	{
		if (!CameraInterpolate.IsActive())
		{
			GameStateManager.SwitchToState(GameStateManager.GetPrevState());
			m_PausedSim = false;
			InterfaceAudio.Play("ui_simulation_stop");
		}
	}

	public void OnExitSimSilent()
	{
		if (!CameraInterpolate.IsActive())
		{
			GameStateManager.SwitchToState(GameStateManager.GetPrevState());
			m_PausedSim = false;
		}
	}

	public void OnPauseSim()
	{
		if (GameStateManager.GetState() == GameState.SIM)
		{
			Game.SetTimeScale(0f);
			m_PausedSim = true;
			m_PauseSimButton.gameObject.SetActive(value: false);
			m_UnPauseSimButton.gameObject.SetActive(value: true);
			AudioMixerManager.PauseSimulationSFX();
			InterfaceAudio.Play("ui_simulation_pause");
		}
	}

	public void OnUnPauseSim()
	{
		if (GameStateManager.GetState() == GameState.SIM)
		{
			Game.SetTimeScale(BridgeSimSpeed.GetTimeScaleForSimulation());
			m_PausedSim = false;
			m_PauseSimButton.gameObject.SetActive(value: true);
			m_UnPauseSimButton.gameObject.SetActive(value: false);
			AudioMixerManager.UnPauseSimulationSFX();
			InterfaceAudio.Play("ui_simulation_pause");
			BridgeEffects.StopErrorFX();
		}
	}

	public void TogglePauseSim()
	{
		if (m_PausedSim)
		{
			OnUnPauseSim();
		}
		else
		{
			OnPauseSim();
		}
	}

	public void OnFaster()
	{
		if (BridgeSimSpeed.m_SimulationSpeedIndex == BridgeSimSpeed.m_SimulationSpeeds.Count - 1)
		{
			InterfaceAudio.PlayErrorBeep();
			return;
		}
		int num = BridgeSimSpeed.m_SimulationSpeedIndex + 1;
		if (num >= BridgeSimSpeed.m_SimulationSpeeds.Count)
		{
			num = 0;
		}
		BridgeSimSpeed.SetSimulationSpeedIndex(num);
		ApplyChangesAfterSimulationSpeedChange();
		InterfaceAudio.Play("ui_simulationSpeed_up");
	}

	public void ApplyChangesAfterSimulationSpeedChange()
	{
		if (GameStateManager.GetState() == GameState.SIM && !m_PausedSim)
		{
			BridgeSimSpeed.SetTimeScaleForSimulation();
		}
		BridgeSimSpeed.SetPitchForSimulation();
		UpdateMuteStateOfSimulationSFX();
	}

	public void OnSlower()
	{
		if (BridgeSimSpeed.m_SimulationSpeedIndex == 0)
		{
			InterfaceAudio.PlayErrorBeep();
			return;
		}
		int num = BridgeSimSpeed.m_SimulationSpeedIndex - 1;
		if (num < 0)
		{
			num = BridgeSimSpeed.m_SimulationSpeeds.Count - 1;
		}
		BridgeSimSpeed.SetSimulationSpeedIndex(num);
		ApplyChangesAfterSimulationSpeedChange();
		InterfaceAudio.Play("ui_simulationSpeed_down");
	}

	public void OnGodMode()
	{
		OnGodModeSilent();
		InterfaceAudio.Play("ui_menubar_gen_on");
	}

	public void OnGodModeSilent()
	{
		m_GodModeButton.gameObject.SetActive(value: false);
		m_GodModeSelectedButton.gameObject.SetActive(value: true);
		Profiles.m_ActiveProfile.m_GodMode = true;
		Profiles.SaveActiveProfile();
	}

	public void OnGodModeSelected()
	{
		OnGodModeSelectedSilent();
		InterfaceAudio.Play("ui_menubar_gen_off");
	}

	public void OnGodModeSelectedSilent()
	{
		m_GodModeButton.gameObject.SetActive(value: true);
		m_GodModeSelectedButton.gameObject.SetActive(value: false);
		Profiles.m_ActiveProfile.m_GodMode = false;
		Profiles.SaveActiveProfile();
	}

	public void OnShowDecor()
	{
		OnShowDecorSilent();
		InterfaceAudio.Play("ui_menubar_gen_on");
	}

	public void OnShowDecorSilent()
	{
		m_ShowDecorButton.gameObject.SetActive(value: false);
		m_ShowDecorSelectedButton.gameObject.SetActive(value: true);
		Profiles.m_ActiveProfile.m_ShowDecor = true;
		Profiles.SaveActiveProfile();
		Decors.SetVisibility(GameStateManager.GetState());
		if (GameStateManager.GetState() == GameState.SANDBOX)
		{
			GameStateSandbox.UpdateMainCameraDecorMask();
		}
	}

	public void OnShowDecorSelected()
	{
		OnShowDecorSelectedSilent();
		InterfaceAudio.Play("ui_menubar_gen_off");
	}

	public void OnShowDecorSelectedSilent()
	{
		m_ShowDecorButton.gameObject.SetActive(value: true);
		m_ShowDecorSelectedButton.gameObject.SetActive(value: false);
		Profiles.m_ActiveProfile.m_ShowDecor = false;
		Profiles.SaveActiveProfile();
		Decors.SetVisibility(GameStateManager.GetState());
		if (GameStateManager.GetState() == GameState.SANDBOX)
		{
			GameStateSandbox.UpdateMainCameraDecorMask();
		}
	}

	public void OnViewReplay()
	{
		if (Cameras.m_AsyncCapture.m_NumFrames <= 0 || !Cameras.m_AsyncCapture.m_Initialized)
		{
			InterfaceAudio.PlayErrorBeep();
			return;
		}
		bool flag = Mathf.Approximately(Time.timeScale, 0f);
		bool resumeRecordingReplayOnExit = Cameras.IsRecordingReplay();
		OnPauseSim();
		Cameras.PauseRecording();
		GameUI.m_Instance.m_ShareReplay.Show();
		GameUI.m_Instance.m_ShareReplay.m_UnPauseOnExit = !flag;
		GameUI.m_Instance.m_ShareReplay.m_ResumeRecordingReplayOnExit = resumeRecordingReplayOnExit;
		InterfaceAudio.Play("ui_window_open");
	}

	public void OnNextLevel()
	{
		CampaignLevel nextLevel = CampaignWorlds.m_Instance.GetNextLevel(Campaign.m_CurrentLevel);
		if (nextLevel == null || Campaign.m_CampaignProgress.IsLocked(nextLevel.m_Id))
		{
			InterfaceAudio.PlayErrorBeep();
		}
		else if (PolyTwitch.SessionIsActiveWithUnviewedSuggestions())
		{
			PolyTwitch.ConfirmLeaveLevel(MoveToNextLevel);
		}
		else
		{
			MoveToNextLevel();
		}
	}

	public void OnPrevLevel()
	{
		CampaignLevel prevLevel = CampaignWorlds.m_Instance.GetPrevLevel(Campaign.m_CurrentLevel);
		if (prevLevel == null || Campaign.m_CampaignProgress.IsLocked(prevLevel.m_Id))
		{
			InterfaceAudio.PlayErrorBeep();
		}
		else if (PolyTwitch.SessionIsActiveWithUnviewedSuggestions())
		{
			PolyTwitch.ConfirmLeaveLevel(MoveToPrevLevel);
		}
		else
		{
			MoveToPrevLevel();
		}
	}

	public void OnMenu()
	{
		if (GameStateManager.GetPendingState() != GameState.SIM)
		{
			if (!GameUI.m_Instance.m_PauseMenu.gameObject.activeInHierarchy)
			{
				InterfaceAudio.Play("ui_window_open");
			}
			else
			{
				InterfaceAudio.Play("ui_window_close");
			}
			GameUI.m_Instance.m_PauseMenu.gameObject.SetActive(!GameUI.m_Instance.m_PauseMenu.gameObject.activeInHierarchy);
		}
	}

	public void OnSandbox()
	{
		if (!CameraInterpolate.IsActive())
		{
			BridgeTrace.CompleteFillingInstantly();
			if (GameUI.m_Instance.m_SandboxMenu.m_SandboxTabsPanel.DecorIsActiveTab())
			{
				GameStateManager.SwitchToState(GameState.DECOR);
			}
			else
			{
				GameStateManager.SwitchToState(GameState.SANDBOX);
			}
		}
	}

	public void OnWorkshop()
	{
		if (!CameraInterpolate.IsActive())
		{
			GameUI.m_Instance.m_Workshop.Open(WorkshopView.LEVELS_AND_CAMPAIGNS);
			InterfaceAudio.Play("ui_menubar_gen_off");
		}
	}

	public void OnBuild()
	{
		if (!CameraInterpolate.IsActive())
		{
			GameStateManager.SwitchToState(GameState.BUILD);
		}
	}

	public void OnLevelInfo()
	{
		GameUI.ToggleLevelInfoPanel();
	}

	public void ForceBudgetTextToUpdate()
	{
		m_LastSetBudgetForBudgetText = int.MaxValue;
	}

	public void SetBudgetText(int budget)
	{
		if (m_LastSetBudgetForBudgetText != budget)
		{
			m_BudgetText.text = Utils.FormatCash(budget);
			m_LastSetBudgetForBudgetText = budget;
		}
	}

	public void UpdateLevelNavButtons()
	{
		if (GameManager.GetGameMode() == GameMode.CAMPAIGN && Campaign.m_CurrentLevel != null)
		{
			m_NextLevel.gameObject.SetActive(m_LevelNavArrowsEnabled && Campaign.GetNextLayoutFilename() != string.Empty);
			m_PrevLevel.gameObject.SetActive(m_LevelNavArrowsEnabled && CampaignWorlds.m_Instance.GetPrevLevel(Campaign.m_CurrentLevel) != null);
		}
		else
		{
			m_NextLevel.gameObject.SetActive(value: false);
			m_PrevLevel.gameObject.SetActive(value: false);
		}
	}

	public void OnLoad()
	{
		if (GameManager.GetGameMode() == GameMode.SANDBOX)
		{
			GameUI.m_Instance.m_LoadSandboxLayout.gameObject.SetActive(value: true);
			GameUI.m_Instance.m_SaveSandboxLayout.gameObject.SetActive(value: false);
		}
		else
		{
			GameUI.m_Instance.m_LoadBridge.gameObject.SetActive(value: true);
			GameUI.m_Instance.m_SaveBridge.gameObject.SetActive(value: false);
		}
		InterfaceAudio.Play("ui_menu_select");
	}

	public void OnSaveAs()
	{
		if (GameManager.GetGameMode() == GameMode.SANDBOX)
		{
			GameUI.m_Instance.m_SaveSandboxLayout.gameObject.SetActive(value: true);
			GameUI.m_Instance.m_LoadSandboxLayout.gameObject.SetActive(value: false);
		}
		else
		{
			GameUI.m_Instance.m_SaveBridge.gameObject.SetActive(value: true);
			GameUI.m_Instance.m_LoadBridge.gameObject.SetActive(value: false);
		}
		InterfaceAudio.Play("ui_menu_select");
	}

	public void OnLayoutLoaded()
	{
		SetBudgetText(Budget.m_CashBudget);
		UpdateBridgeCost();
		m_ClearPreviewContainerRectTransform.gameObject.SetActive(value: false);
	}

	public void SwitchViewToDecorFront(float transitionTimeSeconds)
	{
		PointsOfView.m_PointsOfView[PointOfViewType.DECOR_CENTER].FrameObjects(Game.GetLevelId());
		PointsOfView.RotateTo(PointOfViewType.DECOR_CENTER, transitionTimeSeconds);
		GameStateDecor.m_PointOfViewType = PointOfViewType.DECOR_CENTER;
	}

	public void SwitchViewToDecorTop(float transitionTimeSeconds)
	{
		PointsOfView.m_PointsOfView[PointOfViewType.DECOR_TOP].FrameObjects(Game.GetLevelId());
		PointsOfView.RotateTo(PointOfViewType.DECOR_TOP, transitionTimeSeconds);
		GameStateDecor.m_PointOfViewType = PointOfViewType.DECOR_TOP;
	}

	public void UpdateSandboxUndoRedoButtons()
	{
		m_UndoButton.interactable = SandboxUndo.CanUndo();
		m_RedoButton.interactable = SandboxUndo.CanRedo();
	}

	public bool MaybeShowBudgetToolTip()
	{
		if (Budget.m_CashBudget != Budget.UNLIMITED_CASH_BUDGET && GameUI.PointerOver(typeof(CostAndBudgetToolTip)))
		{
			GameUI.m_Instance.m_BudgetToolTip.Enable();
			return true;
		}
		GameUI.m_Instance.m_BudgetToolTip.Disable();
		return false;
	}

	public void UpdateBridgeCost()
	{
		int num = Mathf.RoundToInt(Budget.m_BridgeCost);
		if (m_LastSetCostForBridgeCostText != num)
		{
			m_CostText.text = Utils.FormatCash(num);
			m_LastSetCostForBridgeCostText = num;
		}
		m_CostBarGreen.gameObject.SetActive(value: false);
		m_CostBarRed.gameObject.SetActive(value: false);
		m_CostBarGold.gameObject.SetActive(value: false);
		m_CostBarGoldWrap.gameObject.SetActive(value: false);
		m_CostBarGold.color = (Profiles.m_ActiveProfile.m_ColorBlindModeOn ? m_CostGoldColorBlind : m_CostGold);
		m_CostBarGoldWrap.color = (Profiles.m_ActiveProfile.m_ColorBlindModeOn ? m_CostGoldColorBlind : m_CostGold);
		if (Budget.m_CashBudget == Budget.UNLIMITED_CASH_BUDGET)
		{
			m_CostBar.color = Color.white;
			return;
		}
		if (Mathf.Approximately(Budget.m_CashBudget, 0f))
		{
			m_CostBarGreen.gameObject.SetActive(value: false);
			m_CostBarRed.gameObject.SetActive(value: true);
			m_CostBarRed.fillAmount = 1f;
			return;
		}
		if (num <= Budget.m_CashBudget)
		{
			m_CostBar.color = Color.white;
			m_CostBarGreen.color = (Profiles.m_ActiveProfile.m_ColorBlindModeOn ? m_CostGreenColorBlind : m_CostGreen);
			m_CostBarGreen.gameObject.SetActive(value: true);
			m_CostBarGreen.fillAmount = Mathf.Clamp01((float)num / (float)Budget.m_CashBudget);
		}
		else
		{
			m_CostBar.color = (Profiles.m_ActiveProfile.m_ColorBlindModeOn ? m_CostLightRedColorBlind : m_CostLightRed);
			m_CostBarRed.color = (Profiles.m_ActiveProfile.m_ColorBlindModeOn ? m_CostRedColorBlind : m_CostRed);
			m_CostBarRed.gameObject.SetActive(value: true);
			m_CostBarRed.fillAmount = Mathf.Clamp01((float)(num - Budget.m_CashBudget) / (float)Budget.m_CashBudget);
		}
		float num2 = 0f;
		num2 = (BridgeJointPlacement.InPlacementMode() ? BridgeJointPlacement.GetPlacementCost() : ((!BridgePillarPlacement.InPlacementMode()) ? ClipboardManager.GetCost() : BridgePillarPlacement.GetPlacementCost()));
		if (!Mathf.Approximately(num2, 0f) && !Mathf.Approximately(Budget.m_CashBudget, 0f))
		{
			m_CostBarGold.gameObject.SetActive(value: true);
			int num3 = Mathf.RoundToInt(Budget.m_BridgeCost + num2);
			if (num <= Budget.m_CashBudget)
			{
				m_CostBarGold.fillAmount = Mathf.Clamp01((float)num3 / (float)Budget.m_CashBudget);
			}
			else
			{
				m_CostBarGold.fillAmount = Mathf.Clamp01((float)(num3 - Budget.m_CashBudget) / (float)Budget.m_CashBudget);
			}
			float num4 = num3 - Budget.m_CashBudget;
			if (num4 > 0f)
			{
				m_CostBarGoldWrap.gameObject.SetActive(value: true);
				m_CostBarGoldWrap.fillAmount = Mathf.Clamp01(num4 / (float)Budget.m_CashBudget);
			}
		}
	}

	public void EnableForSim()
	{
		base.gameObject.SetActive(!GameUI.m_DisableHud);
		m_CostAndBudget.SetActive(value: true);
		m_ButtonContainerSpeed.SetActive(value: true);
		m_ButtonContainerPauseResume.SetActive(value: true);
		m_LevelInfo.gameObject.SetActive(value: true);
		m_LevelNavButtons.SetActive(GameManager.GetGameMode() == GameMode.CAMPAIGN);
		m_ModeToggle.gameObject.SetActive(value: false);
		m_SimButton.gameObject.SetActive(value: false);
		m_ExitSimButton.gameObject.SetActive(value: true);
		m_PauseSimButton.gameObject.SetActive(value: true);
		m_UnPauseSimButton.gameObject.SetActive(value: false);
		m_HelpButton.gameObject.SetActive(value: false);
		m_GodModeParent.SetActive(value: false);
		m_ShowDecorParent.SetActive(value: false);
		GameUI.m_Instance.m_TopBar.m_ReplayButton.gameObject.SetActive(Profiles.m_ActiveProfile.m_Replays && !Game.IsCurrentLevelTutorial());
	}

	private void UpdateCheatTooltip()
	{
		if (m_CheatIconPointerEvents.m_IsHovering)
		{
			GameUI.ToolTipEnable(string.Format("{0}\n{1}", BridgeCheat.GetLocalizedCheatReason(), Localize.Get("TOOLTIP_CHEATING")), null, 10f, 0f);
			if (GameStateManager.GetState() == GameState.BUILD)
			{
				BridgeEffects.PlayErrorEffectAtFirstIllegalNodePosition();
			}
		}
		else if (GameStateManager.GetState() == GameState.BUILD)
		{
			BridgeEffects.StopErrorFX();
		}
	}

	private void UpdateClearPreviewButton()
	{
		m_HelpButtonImage.color = (BridgeShadow.IsActive() ? GameUI.m_Instance.m_GoldColor : Color.white);
		m_ClearPreviewContainerRectTransform.anchoredPosition = m_HelpButtonRectTransform.anchoredPosition;
		m_ClearPreviewContainerRectTransform.gameObject.SetActive(GameStateManager.GetState() == GameState.BUILD && !Game.IsCurrentLevelTutorial() && BridgeShadow.IsActive() && !GameUI.m_Instance.m_Help.gameObject.activeInHierarchy);
	}

	private void UpdateGamepadHelp()
	{
		m_GamepadHelpButton.gameObject.SetActive(GameInput.GetActiveGameDevice() == GameDevice.Gamepad && (GameStateManager.GetState() == GameState.BUILD || GameStateManager.GetState() == GameState.SIM || GameStateManager.GetState() == GameState.SANDBOX));
	}

	private void ProcessInput()
	{
		if (GamepadManager.ButtonJustPressed(GamepadButtonType.SELECT))
		{
			OnMenu();
		}
	}

	private void UpdateTooltips()
	{
		if (m_OverbudgetPointerEvents.m_IsHovering)
		{
			GameUI.ToolTipForceEnable($"Max: {Utils.FormatCash(Budget.GetHardBudgetLimit())}");
		}
	}

	private void MoveToNextLevel()
	{
		if (!Prefabs.AsyncLoadInProgress())
		{
			InterfaceAudio.Play("ui_nextLevel");
			MoveLevelPre();
			Campaign.LoadNextLevel();
		}
	}

	private void MoveToPrevLevel()
	{
		if (!Prefabs.AsyncLoadInProgress())
		{
			InterfaceAudio.Play("ui_nextLevel");
			MoveLevelPre();
			Campaign.LoadPreviousLevel();
		}
	}

	private void MoveLevelPre()
	{
		if (Game.IsCurrentLevelTutorial())
		{
			CampaignTutorial.End();
		}
		BridgeTrace.CompleteFillingInstantly();
		if (GameStateManager.GetState() == GameState.SIM)
		{
			GameStateSim.Exit(GameState.BUILD);
			GameStateManager.BashState(GameState.BUILD);
			GameStateBuild.Enter(GameState.SIM);
		}
		GameStateBuild.Exit(GameState.BUILD);
	}

	private void UpdateMuteStateOfSimulationSFX()
	{
		if (Mathf.Approximately(Time.timeScale, 0f))
		{
			AudioMixerManager.PauseSimulationSFX();
		}
		else
		{
			AudioMixerManager.UnPauseSimulationSFX();
		}
	}

	private void OnHelp()
	{
		if (GameManager.GetGameMode() == GameMode.CAMPAIGN && Campaign.m_CurrentLevel != null && Campaign.m_CurrentLevel.HasHelpPreviews() && GameUI.m_Instance.m_Help.SlotsHavePreviews())
		{
			InterfaceAudio.Play("ui_window_open");
			GameUI.m_Instance.m_Help.Show();
		}
		else if (GameManager.GameModeIsCampaignOrWorkshop())
		{
			InterfaceAudio.Play("ui_window_open");
			Gallery.LaunchForCurrentLevel();
		}
		else
		{
			InterfaceAudio.PlayErrorBeep();
		}
	}

	private void OnGamepadHelp()
	{
		InterfaceAudio.Play("ui_window_open");
		GameUI.m_Instance.m_GamepadHelp.Show();
	}

	private void OnClearPreview()
	{
		BridgeShadow.Clear();
		m_ClearPreviewContainerRectTransform.gameObject.SetActive(value: false);
		InterfaceAudio.Play("ui_menubar_gen_off");
	}

	private void OnDecorViewFront()
	{
		SwitchViewToDecorFront(0f);
	}

	private void OnDecorViewTop()
	{
		SwitchViewToDecorTop(0f);
	}

	private void ModeRefresh()
	{
		if (m_ModeToggle.GetState() == ToggleSliderState.ON)
		{
			OnBuild();
		}
		if (m_ModeToggle.GetState() == ToggleSliderState.OFF)
		{
			OnSandbox();
		}
	}

	private void DecorViewRefresh()
	{
		if (m_DecorViewToggle.GetState() == ToggleSliderState.ON)
		{
			OnDecorViewTop();
		}
		if (m_DecorViewToggle.GetState() == ToggleSliderState.OFF)
		{
			OnDecorViewFront();
		}
	}

	private void OnClear()
	{
		GameUI.m_Instance.m_BuildToolBar.OnClear();
	}

	private void OnUndo()
	{
		GameUI.m_Instance.m_BuildToolBar.OnUndo();
	}

	private void OnRedo()
	{
		GameUI.m_Instance.m_BuildToolBar.OnRedo();
	}

	private void OnRightView()
	{
		PointsOfView.m_PointsOfView[PointOfViewType.SIM_RIGHT].FrameObjects(Game.GetLevelId());
		PointsOfView.RotateTo(PointOfViewType.SIM_RIGHT, GameSettings.TransitionTimeSeconds());
	}

	private void OnLeftView()
	{
		PointsOfView.m_PointsOfView[PointOfViewType.SIM_LEFT].FrameObjects(Game.GetLevelId());
		PointsOfView.RotateTo(PointOfViewType.SIM_LEFT, GameSettings.TransitionTimeSeconds());
	}

	private void OnCenterPitchedDownView()
	{
		PointsOfView.m_PointsOfView[PointOfViewType.SIM_CENTER_PITCHED_DOWN].FrameObjects(Game.GetLevelId());
		PointsOfView.RotateTo(PointOfViewType.SIM_CENTER_PITCHED_DOWN, GameSettings.TransitionTimeSeconds());
	}

	private void OnCenterView()
	{
		PointsOfView.m_PointsOfView[PointOfViewType.SIM_CENTER].FrameObjects(Game.GetLevelId());
		PointsOfView.RotateTo(PointOfViewType.SIM_CENTER, GameSettings.TransitionTimeSeconds());
	}

	private void OnPhotoModeCancel()
	{
		InterfaceAudio.Play("ui_menu_select");
		GameStatePhoto.LeavePhotoMode();
	}
}
