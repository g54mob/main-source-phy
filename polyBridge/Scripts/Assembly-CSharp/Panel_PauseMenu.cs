using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_PauseMenu : MonoBehaviour
{
	[Header("Panel")]
	public RectTransform m_Panel;

	public RectTransform m_VerticalLayoutRectTransform;

	public SandboxPanelResizer m_PanelResizer;

	[Header("Mute")]
	public Button m_SoundOn;

	public Button m_SoundOff;

	public Button m_Cancel;

	[Header("Slots")]
	public MenuSlot m_SkipTutorial;

	public MenuSlot m_New;

	public MenuSlot m_QuickSave;

	public MenuSlot m_Save;

	public MenuSlot m_Load;

	public MenuSlot m_Settings;

	public MenuSlot m_Campaign;

	public MenuSlot m_Workshop;

	public MenuSlot m_MainMenu;

	public TextMeshProUGUI m_WorkshopButtonText;

	private bool m_IsQuitting;

	private bool m_LoadMainMenuOnClose;

	private int m_LateUpdateCounter;

	private void Start()
	{
		m_SoundOn.onClick.AddListener(OnSoundOn);
		m_SoundOff.onClick.AddListener(OnSoundOff);
		m_Cancel.onClick.AddListener(Close);
		m_New.m_Button.onClick.AddListener(OnNew);
		m_QuickSave.m_Button.onClick.AddListener(OnQuickSave);
		m_Save.m_Button.onClick.AddListener(OnSaveGame);
		m_Load.m_Button.onClick.AddListener(OnLoadGame);
		m_Settings.m_Button.onClick.AddListener(OnSettings);
		m_Campaign.m_Button.onClick.AddListener(OnCampaign);
		m_Workshop.m_Button.onClick.AddListener(OnWorkshop);
		m_MainMenu.m_Button.onClick.AddListener(OnMainMenu);
		m_SkipTutorial.m_Button.onClick.AddListener(OnSkipTutorial);
	}

	private void Update()
	{
		ProcessInput();
		MenuSlots.UpdateSelectedSlot();
		UpdateQuickSaveLabel();
		m_New.m_Text.text = Localize.Get("PAUSE_MENU_NEW_LAYOUT");
		m_Save.m_Text.text = ((GameManager.GetGameMode() == GameMode.SANDBOX) ? Localize.Get("PAUSEMENU_SAVE_LAYOUT") : Localize.Get("PAUSEMENU_SAVE_BRIDGE"));
		m_Load.m_Text.text = ((GameManager.GetGameMode() == GameMode.SANDBOX) ? Localize.Get("PAUSEMENU_LOAD_LAYOUT") : Localize.Get("PAUSEMENU_LOAD_BRIDGE"));
		m_QuickSave.gameObject.SetActive(IsQuickSaveAllowed());
		if (ActivePanels.IsTopPanel(base.gameObject))
		{
			ShowGamepadButtons();
		}
		UpdatePanelHeight();
	}

	private void LateUpdate()
	{
		m_LateUpdateCounter++;
		if (m_Panel != null)
		{
			m_Panel.gameObject.SetActive(ActivePanels.IsTopPanel(base.gameObject));
		}
		if (m_LateUpdateCounter > 1)
		{
			MenuSlots.ResetSelectedSlot();
		}
		MenuSlots.UpdateSelectedSlot();
	}

	private void OnEnable()
	{
		AudioMixerManager.PauseSFX();
		MenuSlots.ForceClearSelection();
		InitMenuSlots();
		Game.SetTimeScale(0f);
		if (GameStateManager.GetState() == GameState.SIM)
		{
			GameStateSim.Pause();
		}
		ActivePanels.Add(base.gameObject);
		if (Game.IsCurrentLevelTutorial() && CampaignTutorial.IsRunning())
		{
			GameUI.m_Instance.m_CampaignTutorial.PauseTweens();
		}
		if (GameUI.m_Instance.m_LevelInfo.gameObject.activeInHierarchy)
		{
			GameUI.m_Instance.m_LevelInfo.gameObject.SetActive(value: false);
		}
		SandboxItems.CancelMovementDueToModalMenuOpening();
		EventEditor.ForceClearMovingIcon();
		GameUI.m_Instance.m_Help.gameObject.SetActive(value: false);
		GameUI.m_Instance.m_GamepadHelp.gameObject.SetActive(value: false);
		m_LoadMainMenuOnClose = false;
		m_LateUpdateCounter = 0;
		SetMuteButtonBasedOnProfile();
		GameUI.m_Instance.m_GamepadLegend.HideButtonsLeft();
		ShowGamepadButtons();
	}

	private void OnDisable()
	{
		if (GameStateManager.GetState() == GameState.SIM && !GameUI.m_Instance.m_Settings.gameObject.activeInHierarchy)
		{
			GameStateSim.UnPause();
		}
		if (Bridge.IsSimulationPaused())
		{
			AudioMixerManager.UnPauseNonSimulationSFX();
		}
		else
		{
			AudioMixerManager.UnPauseSFX();
		}
		ActivePanels.Remove(base.gameObject);
		if (Game.IsCurrentLevelTutorial() && CampaignTutorial.IsRunning() && !m_IsQuitting)
		{
			GameUI.m_Instance.m_CampaignTutorial.ResumeTweens();
		}
	}

	private void OnApplicationQuit()
	{
		m_IsQuitting = true;
	}

	private void InitMenuSlots()
	{
		MenuSlots.Init();
		if (Game.IsCurrentLevelTutorial() && Game.IsCurrentLevelTutorial())
		{
			MenuSlots.Add(m_SkipTutorial);
		}
		m_SkipTutorial.gameObject.SetActive(Game.IsCurrentLevelTutorial() && Game.IsCurrentLevelTutorial());
		if (!Game.IsCurrentLevelTutorial() && (GameStateManager.GetState() == GameState.BUILD || GameStateManager.GetState() == GameState.SANDBOX || GameStateManager.GetState() == GameState.DECOR))
		{
			m_Save.m_Text.text = ((GameManager.GetGameMode() == GameMode.SANDBOX) ? Localize.Get("PAUSEMENU_SAVE_LAYOUT") : Localize.Get("PAUSEMENU_SAVE_BRIDGE"));
			m_Load.m_Text.text = ((GameManager.GetGameMode() == GameMode.SANDBOX) ? Localize.Get("PAUSEMENU_LOAD_LAYOUT") : Localize.Get("PAUSEMENU_LOAD_BRIDGE"));
			if (GameManager.GetGameMode() == GameMode.SANDBOX)
			{
				MenuSlots.Add(m_New);
				m_New.gameObject.SetActive(value: true);
			}
			else
			{
				m_New.gameObject.SetActive(value: false);
			}
			MenuSlots.Add(m_Save);
			MenuSlots.Add(m_Load);
			UpdateQuickSaveLabel();
			if (IsQuickSaveAllowed())
			{
				m_QuickSave.gameObject.SetActive(value: true);
				MenuSlots.Add(m_QuickSave);
			}
			else
			{
				m_QuickSave.gameObject.SetActive(value: false);
			}
			m_Save.gameObject.SetActive(value: true);
			m_Load.gameObject.SetActive(value: true);
		}
		else
		{
			m_New.gameObject.SetActive(value: false);
			m_QuickSave.gameObject.SetActive(value: false);
			m_Save.gameObject.SetActive(value: false);
			m_Load.gameObject.SetActive(value: false);
		}
		MenuSlots.Add(m_Settings);
		if (GameManager.GetGameMode() == GameMode.WORKSHOP)
		{
			MenuSlots.Add(m_Workshop);
			m_Workshop.gameObject.SetActive(value: true);
			m_WorkshopButtonText.text = GetWorkshopButtonText(Game.GetLevelId());
		}
		else
		{
			m_Workshop.gameObject.SetActive(value: false);
		}
		if (GameManager.GetGameMode() == GameMode.CAMPAIGN && !Game.IsCurrentLevelTutorial())
		{
			MenuSlots.Add(m_Campaign);
			m_Campaign.gameObject.SetActive(value: true);
		}
		else
		{
			m_Campaign.gameObject.SetActive(value: false);
		}
		MenuSlots.Add(m_MainMenu);
		m_PanelResizer.ForceUpdate();
	}

	private string GetWorkshopButtonText(string itemId)
	{
		if (WeeklyChallenges.IsAWeeklyChallenge(itemId))
		{
			return Localize.Get("PAUSEMENU_WEEKLY_CHALLENGES_MENU");
		}
		return Localize.Get("PAUSEMENU_WORKSHOP_MENU");
	}

	private void OnQuickSave()
	{
		Game.DoQuickSave();
	}

	private void OnNew()
	{
		InterfaceAudio.Play("ui_menu_select");
		if (Sandbox.m_UnsavedChanges)
		{
			PopUpMessage.DisplayWarning(Localize.Get("POPUP_NEW_SANDBOX_LOSE_CHANGES"), useYesNoLables: true, StartNewSandboxLayout);
		}
		else
		{
			StartNewSandboxLayout();
		}
	}

	private void StartNewSandboxLayout()
	{
		if (GameStateManager.GetState() == GameState.BUILD || GameStateManager.GetState() == GameState.DECOR)
		{
			GameStateManager.SwitchToStateImmediate(GameState.SANDBOX);
		}
		if (GameStateManager.GetState() != GameState.SANDBOX)
		{
			InterfaceAudio.PlayErrorBeep();
			return;
		}
		string randomNonLegacyAddressableName = ThemeStubs.m_Instance.GetRandomNonLegacyAddressableName();
		if (Prefabs.AsyncPrefabExists(randomNonLegacyAddressableName))
		{
			Sandbox.StartNewSandbox(ThemeStubs.m_Instance.GetIdFromName(randomNonLegacyAddressableName));
			GameStateSandbox.EnterWithPreloadedTheme(spawnRandomVehicle: true);
			Close();
		}
		else
		{
			GameStatePreloadingAssets.PreloadTheme(randomNonLegacyAddressableName, null, PreloadThemeCallback);
		}
	}

	private void PreloadThemeCallback(string themeAdressableName, FileSlot slot)
	{
		Sandbox.StartNewSandbox(ThemeStubs.m_Instance.GetIdFromName(themeAdressableName));
		GameStateSandbox.EnterWithPreloadedTheme(spawnRandomVehicle: true);
		Close();
	}

	private void OnSaveGame()
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
		InterfaceAudio.Play("ui_window_open");
	}

	private void OnLoadGame()
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
		InterfaceAudio.Play("ui_window_open");
	}

	private void OnSettings()
	{
		InterfaceAudio.Play("ui_window_open");
		GameUI.m_Instance.m_Settings.Open();
	}

	private void OnCampaign()
	{
		InterfaceAudio.Play("ui_window_open");
		if (GameManager.GetGameMode() == GameMode.CAMPAIGN && Campaign.m_CurrentLevel != null)
		{
			GameStateMainMenu.m_LoadCampaignPanelForWorldID = Campaign.m_CurrentLevel.m_WorldId;
		}
		if (PolyTwitch.SessionIsActiveWithUnviewedSuggestions())
		{
			PolyTwitch.ConfirmLeaveLevel(ExitToMainMenu);
		}
		else
		{
			ExitToMainMenu();
		}
	}

	private void OnWorkshop()
	{
		InterfaceAudio.Play("ui_window_open");
		if (GameStateManager.GetState() == GameState.SIM)
		{
			GameStateManager.SwitchToStateImmediate(GameState.BUILD);
		}
		if (Workshop.m_LastPlayedWorkshopItem != null && WeeklyChallenges.IsAWeeklyChallenge(Workshop.m_LastPlayedWorkshopItem.GetId()))
		{
			GameUI.m_Instance.m_WeeklyChallenges.Open(Workshop.m_LastPlayedWorkshopItem.GetId());
		}
		else
		{
			GameUI.m_Instance.m_Workshop.Open(WorkshopView.LEVELS_AND_CAMPAIGNS);
			WorkshopCampaign workshopCampaign = WorkshopCampaigns.Get(WorkshopCampaigns.m_ActiveWorkshopCampaignModId);
			if (workshopCampaign != null)
			{
				GameUI.m_Instance.m_Workshop.m_WorkshopCampaignPanel.Open(workshopCampaign, Workshop.m_LastPlayedWorkshopItem.GetId(), string.Empty);
				GameUI.m_Instance.m_Workshop.m_WorkshopCampaignPanel.m_ReturnToGameOnClose = true;
			}
			else
			{
				_ = Workshop.m_LastPlayedWorkshopItem;
			}
		}
		Close();
	}

	private void OnMainMenu()
	{
		if (PolyTwitch.SessionIsActiveWithUnviewedSuggestions())
		{
			InterfaceAudio.Play("ui_window_open");
			PolyTwitch.ConfirmLeaveLevel(ExitToMainMenu);
		}
		else if (GameManager.GetGameMode() == GameMode.SANDBOX && Sandbox.m_UnsavedChanges)
		{
			InterfaceAudio.Play("ui_window_open");
			PopUpMessage.DisplayWarning(Localize.Get("POPUP_EXIT_SANDBOX_LOSE_CHANGES"), useYesNoLabels: true, ExitToMainMenu, null);
		}
		else
		{
			ExitToMainMenu();
		}
	}

	private void OnSkipTutorial()
	{
		if (Game.IsCurrentLevelTutorial())
		{
			InterfaceAudio.Play("ui_menu_select");
			CampaignTutorial.End();
			CampaignLevel nextLevel = CampaignWorlds.m_Instance.GetNextLevel(Campaign.m_CurrentLevel);
			if (nextLevel == null)
			{
				return;
			}
			if (Campaign.LoadLevel(nextLevel))
			{
				if (GameStateManager.GetState() == GameState.BUILD)
				{
					GameStateBuild.Exit(GameState.INVALID);
					GameStateBuild.m_ShowLevelInfoPanelOnEnter = true;
					GameStateBuild.Enter(GameState.INVALID);
				}
				if (GameStateManager.GetState() == GameState.SIM)
				{
					GameStateSim.m_SkipBridgeRestoreOnExit = true;
					GameStateManager.SwitchToStateImmediate(GameState.BUILD);
				}
			}
		}
		Close();
	}

	private void OnSoundOn()
	{
		AudioVolume.Mute(on: true);
		Profiles.m_ActiveProfile.m_Mute = true;
		SetMuteButtonBasedOnProfile();
		Profiles.SaveActiveProfile();
	}

	private void OnSoundOff()
	{
		AudioVolume.Mute(on: false);
		Profiles.m_ActiveProfile.m_Mute = false;
		SetMuteButtonBasedOnProfile();
		Profiles.SaveActiveProfile();
	}

	private void ExitToMainMenu()
	{
		m_LoadMainMenuOnClose = true;
		if (Game.IsCurrentLevelTutorial())
		{
			CampaignTutorial.End();
		}
		Close();
	}

	public void CloseSilent()
	{
		GameUI.m_Instance.m_PauseMenu.gameObject.SetActive(value: false);
	}

	public void Close()
	{
		InterfaceAudio.Play("ui_window_close");
		GameUI.m_Instance.m_PauseMenu.gameObject.SetActive(value: false);
		if (m_LoadMainMenuOnClose)
		{
			GameStateManager.SwitchToState(GameState.MAIN_MENU);
		}
	}

	private void ProcessInput()
	{
		if (!GameStateCommonInput.IgnoreKeyboardInputForPanel(base.gameObject) && m_Panel.gameObject.activeInHierarchy)
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				Close();
			}
			else if (GameInput.JustPressed(BindingType.QUICKSAVE))
			{
				Game.DoQuickSave();
				Close();
			}
			else if (GamepadManager.ButtonJustPressed(GamepadButtonType.EAST))
			{
				Game.ForceIgnoreNextSelection();
				Close();
			}
			else
			{
				MenuSlots.ProcessInput();
			}
		}
	}

	private void SetMuteButtonBasedOnProfile()
	{
		m_SoundOn.gameObject.SetActive(!Profiles.m_ActiveProfile.m_Mute);
		m_SoundOff.gameObject.SetActive(Profiles.m_ActiveProfile.m_Mute);
	}

	private void UpdatePanelHeight()
	{
		m_Panel.sizeDelta = new Vector2(m_Panel.sizeDelta.x, m_VerticalLayoutRectTransform.sizeDelta.y + 50f);
	}

	private bool IsQuickSaveAllowed()
	{
		if (GameManager.GetGameMode() != GameMode.SANDBOX || string.IsNullOrEmpty(Sandbox.m_CurrentLayoutName))
		{
			if (GameManager.GetGameMode() != GameMode.SANDBOX)
			{
				return BridgeSaveSlots.CanQuickSave();
			}
			return false;
		}
		return true;
	}

	private void ShowGamepadButtons()
	{
		GameUI.m_Instance.m_GamepadLegend.HideButtonsLeft();
		GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, Localize.Get("UI_SELECT"), GamepadButtonType.EAST, Localize.Get("UI_CLOSE"));
	}

	private void UpdateQuickSaveLabel()
	{
		if (GameInput.GetActiveGameDevice() == GameDevice.Gamepad)
		{
			m_QuickSave.m_Text.text = Localize.Get("UI_QUICKSAVE");
		}
		else
		{
			m_QuickSave.m_Text.text = string.Format(Localize.Get("UI_TOOLTIP_QUICKSAVE"), Bindings.GetBinding(BindingType.QUICKSAVE).GetTooltipBindingString());
		}
	}
}
