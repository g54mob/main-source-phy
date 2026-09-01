using System.Collections;
using System.Collections.Generic;
using System.IO;
using DG.Tweening;
using Steamworks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_MainMenuNew : MonoBehaviour
{
	public const string TWITCH_CALL_TO_ACTION_FILENAME = ".twitch_calltoaction";

	public RectTransform m_TopButtonsParent;

	[Header("Top Strip Buttons")]
	public GameObject m_TopStrip;

	public Button m_TwitchButton;

	public Button m_RedditButton;

	public Button m_DiscordButton;

	[Header("Bottom Strip Buttons")]
	public Button m_DryCactusButton;

	[Header("Left Buttons")]
	public Button m_SettingsButton;

	public Button m_ExitButton;

	public Button m_TwitchCallToActionCloseButton;

	public GameObject m_TwitchCallToAction;

	[Header("Profile")]
	public Button m_ProfilesButton;

	public Button m_ProfileVehicleButton;

	public TextMeshProUGUI m_ProgressText;

	public Image m_ProfileImage;

	public TextMeshProUGUI m_ProfileButtonText;

	public Sprite m_DefaultProfileVehicleIcon;

	[Header("Bottom Buttons")]
	public Button m_StartButtonMasked;

	public Button m_CampaignButtonMasked;

	public Button m_CampaignStartButtonMasked;

	public Button m_WorkshopButtonMasked;

	public Button m_ModsButtonMasked;

	public Button m_SandboxButtonMasked;

	public Button m_ChallengesButtonMasked;

	public Image m_ChallengesSolvedIcon;

	public Image m_ChallengesNewIcon;

	public Image m_ChallengesNewIconShadow;

	public Button m_GalleryButtonMasked;

	[Header("Button Text")]
	public TextMeshProUGUI m_StartLevelText;

	public TextMeshProUGUI m_CamapignText;

	public TextMeshProUGUI m_CamapignStartText;

	public TextMeshProUGUI m_WorkshopText;

	public TextMeshProUGUI m_SandboxText;

	public TextMeshProUGUI m_ChallengesText;

	public TextMeshProUGUI m_GalleryText;

	public TextMeshProUGUI m_ModsText;

	[Header("Misc")]
	public Button m_PromoBannerButton;

	public RectTransform m_PromoBannerRectTransform;

	public Transform m_FeaturedLevelsParent;

	public GameObject m_FeaturedLevelPanelPrefab;

	private int m_FeaturedLevelsRefreshedForWeek;

	private static float DOWNLOAD_FEATURED_RETRY_TIME_SECONDS = 30f;

	private float m_LastDownloadFeaturedAttemptTime;

	private bool m_DownloadingFeatured;

	public static uint m_NumUpdates;

	private bool m_HasLeftMainMenu;

	private readonly string PROMO_DISABLED_KEY = "PromoDisabled";

	private void Awake()
	{
		m_ChallengesButtonMasked.gameObject.SetActive(value: true);
		m_ChallengesSolvedIcon.gameObject.SetActive(value: false);
		m_ChallengesNewIcon.gameObject.SetActive(value: false);
		m_ChallengesNewIconShadow.gameObject.SetActive(value: false);
	}

	private void Start()
	{
		m_TwitchButton.onClick.AddListener(OnTwitch);
		m_RedditButton.onClick.AddListener(OnReddit);
		m_DiscordButton.onClick.AddListener(OnDiscord);
		m_DryCactusButton.onClick.AddListener(OnDryCactus);
		m_SettingsButton.onClick.AddListener(OnSettings);
		m_ProfilesButton.onClick.AddListener(OnProfiles);
		m_ProfileVehicleButton.onClick.AddListener(OnProfiles);
		m_ExitButton.onClick.AddListener(OnExitToDesktop);
		m_TwitchCallToActionCloseButton.onClick.AddListener(OnTwitchCallToActionClose);
		m_StartButtonMasked.onClick.AddListener(OnStart);
		m_CampaignButtonMasked.onClick.AddListener(OnCampaign);
		m_CampaignStartButtonMasked.onClick.AddListener(OnStart);
		m_WorkshopButtonMasked.onClick.AddListener(OnWorkshop);
		m_ModsButtonMasked.onClick.AddListener(OnMods);
		m_SandboxButtonMasked.onClick.AddListener(OnSandbox);
		m_ChallengesButtonMasked.onClick.AddListener(OnChallenges);
		m_GalleryButtonMasked.onClick.AddListener(OnGallery);
		m_StartButtonMasked.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
		m_CampaignButtonMasked.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
		m_CampaignStartButtonMasked.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
		m_WorkshopButtonMasked.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
		m_ModsButtonMasked.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
		m_SandboxButtonMasked.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
		m_ChallengesButtonMasked.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
		m_GalleryButtonMasked.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
		m_ChallengesNewIcon.alphaHitTestMinimumThreshold = 0.1f;
		m_PromoBannerRectTransform.anchoredPosition = new Vector2(135f, m_PromoBannerRectTransform.anchoredPosition.y);
		m_PromoBannerRectTransform.DOAnchorPos(new Vector2(-116f, m_PromoBannerRectTransform.anchoredPosition.y), 0.5f);
		m_PromoBannerButton.onClick.AddListener(OnPromoButton);
		m_PromoBannerButton.gameObject.SetActive(!PlayerPrefs.HasKey(PROMO_DISABLED_KEY));
		m_LastDownloadFeaturedAttemptTime = float.MinValue;
	}

	private void OnEnable()
	{
		ActivePanels.Insert(base.gameObject);
		int currentWeek = WeeklyChallenges.GetCurrentWeek();
		if (currentWeek > 0)
		{
			UpdateWeeklyChallengeIcons(currentWeek);
		}
		if (Profiles.m_ActiveProfile != null)
		{
			UpdateProfileCard();
		}
		UpdateLoc();
		UpdateTwitchCallToAction();
		m_TopStrip.SetActive(!Game.IsSteamDeckOrMobile() && !SteamUtils.IsSteamInBigPictureMode());
		m_TopButtonsParent.anchoredPosition = new Vector2(0f, m_TopStrip.activeInHierarchy ? (-35f) : 0f);
		ShowGamepadLegend();
	}

	private void ShowGamepadLegend()
	{
		GameUI.m_Instance.m_GamepadLegend.HideBackground();
		GameUI.m_Instance.m_GamepadLegend.HideButtonsLeft();
		if (m_CampaignStartButtonMasked.gameObject.activeInHierarchy || m_StartButtonMasked.gameObject.activeInHierarchy)
		{
			GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, Localize.Get("UI_SELECT"), GamepadButtonType.NORTH, m_CampaignStartButtonMasked.gameObject.activeInHierarchy ? Localize.Get("MAINMENU_START") : Localize.Get("MAINMENU_CONTINUE_GAME"), GamepadButtonType.WEST, Localize.Get("TOOLTIP_SETTINGS"));
		}
		else
		{
			GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, Localize.Get("UI_SELECT"), GamepadButtonType.WEST, Localize.Get("TOOLTIP_SETTINGS"));
		}
	}

	private void UpdateLevelToContinue()
	{
		CampaignLevel campaignLevelToContinue = GetCampaignLevelToContinue();
		m_StartLevelText.text = ((campaignLevelToContinue != null) ? campaignLevelToContinue.m_NumberPrefix : string.Empty);
	}

	private CampaignLevel GetCampaignLevelToContinue()
	{
		CampaignLevel levelFromId = CampaignWorlds.m_Instance.GetLevelFromId(Profiles.m_ActiveProfile.m_LastLoadedCampaignLevelId);
		if (levelFromId == null)
		{
			return CampaignWorlds.m_Instance.m_Worlds[0].m_Levels[0];
		}
		if (levelFromId.m_Id != Profiles.m_ActiveProfile.m_LastSolvedCampaignLevelId)
		{
			return levelFromId;
		}
		CampaignWorld worldWithLevelId = CampaignWorlds.m_Instance.GetWorldWithLevelId(levelFromId.m_Id);
		if (CampaignWorlds.m_Instance.IsLevelLastInWorld(levelFromId.m_Id) && !worldWithLevelId.m_AdvanceToNextWorldAutomatically)
		{
			GameStateMainMenu.m_LoadCampaignPanelForWorldID = worldWithLevelId.m_Id;
			return null;
		}
		CampaignLevel nextLevel = CampaignWorlds.m_Instance.GetNextLevel(levelFromId);
		if (!(nextLevel != null))
		{
			return null;
		}
		return nextLevel;
	}

	private void OnDisable()
	{
		ActivePanels.Remove(base.gameObject);
		GameUI.m_Instance.m_GamepadLegend.HideButtons();
		if (GameInput.GetActiveGameDevice() == GameDevice.Gamepad)
		{
			GameUI.m_Instance.m_GamepadLegend.ShowBackground();
		}
	}

	private void OnDestroy()
	{
	}

	private void Update()
	{
		ProcessInput();
		UpdateLevelToContinue();
		UpdateProfileCard();
		UpdateLoc();
		if (!GameManager.IsSteamOffline())
		{
			UpdateWeeklyChallenges();
		}
		m_NumUpdates++;
		if (m_NumUpdates == 3)
		{
			GamepadManager.m_VirtualMouseUI.SuppressMouseVisual(suppress: false);
		}
		if (ActivePanels.m_Panels.Count == 0 || (ActivePanels.m_Panels.Count == 1 && ActivePanels.m_Panels[0] == GameUI.m_Instance.m_MainMenuNew))
		{
			ShowGamepadLegend();
		}
	}

	private void UpdateWeeklyChallenges()
	{
		int num = Mathf.Min(WeeklyChallenges.GetCurrentWeek(), WeeklyChallenges.GetWeekWithMostRecentChallenge());
		if (!m_DownloadingFeatured && num != m_FeaturedLevelsRefreshedForWeek && Time.realtimeSinceStartup - m_LastDownloadFeaturedAttemptTime > DOWNLOAD_FEATURED_RETRY_TIME_SECONDS)
		{
			List<string> weeklyChallengeIdsForSeason = WeeklyChallenges.GetWeeklyChallengeIdsForSeason(WeeklyChallenges.GetSeasonForWeek(num));
			if (weeklyChallengeIdsForSeason.Count > 0)
			{
				WeeklyChallenges.BatchDownloadWorkshopItems(weeklyChallengeIdsForSeason, num, OnDownloadFeaturedComplete);
				m_DownloadingFeatured = true;
				m_LastDownloadFeaturedAttemptTime = Time.realtimeSinceStartup;
			}
		}
	}

	private void OnDownloadFeaturedComplete(bool success, int currentWeek)
	{
		m_DownloadingFeatured = false;
		if (success)
		{
			UpdateWeeklyChallengeIcons(currentWeek);
			m_FeaturedLevelsRefreshedForWeek = currentWeek;
		}
	}

	private void UpdateWeeklyChallengeIcons(int currentWeek)
	{
		WeeklyChallengeStub weeklyChallengeStub = WeeklyChallenges.GetWeeklyChallengeStub(currentWeek);
		if (weeklyChallengeStub != null)
		{
			m_ChallengesNewIcon.gameObject.SetActive(!Profiles.m_ActiveProfile.m_OpenedWeeklyChallengeItemIds.Contains(weeklyChallengeStub.m_ItemID));
			m_ChallengesNewIconShadow.gameObject.SetActive(m_ChallengesNewIcon.gameObject.activeInHierarchy);
			m_ChallengesSolvedIcon.gameObject.SetActive(WeeklyChallengesProgress.HasCompletedLevel(weeklyChallengeStub.m_ItemID));
		}
	}

	private void UpdateLoc()
	{
		if (((Profiles.m_ActiveProfile != null) ? CampaignWorlds.m_Instance.GetLevelFromId(Profiles.m_ActiveProfile.m_LastLoadedCampaignLevelId) : null) == null && Campaign.m_CampaignProgress.GetNumCompletedLevels() == 0)
		{
			m_StartButtonMasked.gameObject.SetActive(value: false);
			m_CampaignButtonMasked.gameObject.SetActive(value: false);
			m_CampaignStartButtonMasked.gameObject.SetActive(value: true);
		}
		else
		{
			m_StartButtonMasked.gameObject.SetActive(value: true);
			m_CampaignButtonMasked.gameObject.SetActive(value: true);
			m_CampaignStartButtonMasked.gameObject.SetActive(value: false);
		}
		m_CamapignText.text = Localize.Get("MAINMENU_CAMPAIGN");
		m_WorkshopText.text = Localize.Get("MAINMENU_WORKSHOP");
		m_SandboxText.text = Localize.Get("MAINMENU_SANDBOX");
		m_ChallengesText.text = Localize.Get("MAINMENU_WEEKLIES");
		m_GalleryText.text = Localize.Get("MAINMENU_GALLERY");
		m_ModsText.text = Localize.Get("MAINMENU_MODS");
	}

	private void UpdateProfileCard()
	{
		m_ProfileButtonText.text = Profiles.GetActiveProfileName();
		m_ProgressText.text = Campaign.m_CampaignProgress.GetNumCompletedLevels().ToString();
		m_ProfileImage.sprite = Profiles.GetSpriteForVehicle(Profiles.m_ActiveProfile.m_AvatarAddressable, Profiles.m_ActiveProfile.m_AvatarSkin);
	}

	public void ForceWeeklyChallengeRefresh()
	{
		m_FeaturedLevelsRefreshedForWeek = 0;
	}

	public void Open()
	{
		UpdateLevelToContinue();
		base.gameObject.SetActive(value: true);
	}

	public void Close()
	{
		base.gameObject.SetActive(value: false);
	}

	public void OpenCampaignPanelForDefaultLevel()
	{
		GameUI.m_Instance.m_Campaign.Open(CampaignWorlds.m_Instance.m_Worlds[0].m_Levels[0].m_Id, CampaignWorlds.m_Instance.m_Worlds[0].m_Id);
	}

	private void UpdateHoverButton(HoverButton hoverButton)
	{
		Color color = hoverButton.m_Background.color;
		hoverButton.m_Background.color = (hoverButton.m_PointerEvents.m_IsHovering ? GameUI.m_Instance.m_MenuSlotHoverColor : GameUI.m_Instance.m_MenuSlotColor);
		if (color == GameUI.m_Instance.m_MenuSlotColor && hoverButton.m_PointerEvents.m_IsHovering)
		{
			InterfaceAudio.Play("ui_menu_hover");
		}
	}

	private void OnStart()
	{
		CampaignLevel campaignLevelToContinue = GetCampaignLevelToContinue();
		if (campaignLevelToContinue == null)
		{
			OnCampaign();
			return;
		}
		InterfaceAudio.Play("ui_menu_select");
		BridgeCheat.Clear();
		BridgeCheat.m_ForceUnlimitedBudget = campaignLevelToContinue.m_UnlimitedBudget;
		BridgeCheat.m_ForceUnlimitedMaterial = campaignLevelToContinue.m_UnlimitedMaterial;
		Campaign.m_LevelBeingPreloaded = campaignLevelToContinue;
		GameStatePreloadingAssets.PreloadLevel(campaignLevelToContinue.GetLayoutPath(), null, Campaign.DonePreloadFromMainMenu);
		Close();
	}

	private void OnCampaign()
	{
		GameUI.m_Instance.m_MainMenuNew.Close();
		InterfaceAudio.Play("ui_menu_select");
		CampaignLevel campaignLevelToContinue = GetCampaignLevelToContinue();
		if (campaignLevelToContinue == null)
		{
			CampaignLevel levelFromId = CampaignWorlds.m_Instance.GetLevelFromId(Profiles.m_ActiveProfile.m_LastLoadedCampaignLevelId);
			if (levelFromId != null)
			{
				GameUI.m_Instance.m_Campaign.Open(levelFromId.m_Id, levelFromId.m_WorldId);
			}
			else
			{
				OpenCampaignPanelForDefaultLevel();
			}
		}
		else
		{
			CampaignWorld worldById = CampaignWorlds.m_Instance.GetWorldById(campaignLevelToContinue.m_WorldId);
			if ((bool)worldById && worldById.IsLocked())
			{
				OpenCampaignPanelForDefaultLevel();
			}
			else
			{
				GameUI.m_Instance.m_Campaign.Open(campaignLevelToContinue.m_Id, campaignLevelToContinue.m_WorldId);
			}
		}
	}

	private void OnTwitch()
	{
		InterfaceAudio.Play("ui_menu_select");
		GameUI.m_Instance.m_Settings.OpenTwitchSettings();
		GameUI.m_Instance.m_MainMenuNew.Close();
	}

	private void OnReddit()
	{
		InterfaceAudio.Play("ui_menu_select");
		Application.OpenURL("https://www.reddit.com/r/PolyBridge/");
		StartCoroutine(TemporarilyDisableLinks());
	}

	private void OnDiscord()
	{
		InterfaceAudio.Play("ui_menu_select");
		Application.OpenURL("https://discord.gg/aA8F7Dq");
		StartCoroutine(TemporarilyDisableLinks());
	}

	private void OnDryCactus()
	{
		GameUI.m_Instance.m_MainMenuNew.Close();
		GameUI.m_Instance.m_Credits.Open();
	}

	private IEnumerator TemporarilyDisableLinks()
	{
		m_TwitchButton.enabled = false;
		m_RedditButton.enabled = false;
		m_DiscordButton.enabled = false;
		yield return new WaitForSeconds(3f);
		m_TwitchButton.enabled = true;
		m_RedditButton.enabled = true;
		m_DiscordButton.enabled = true;
	}

	private void OnSandbox()
	{
		InterfaceAudio.Play("ui_menu_select");
		Prefabs.m_Instance.UnloadAssetsNotInLayout(string.Empty);
		BridgeCheat.Clear();
		Sandbox.Clear();
		GameUI.m_Instance.m_EventEditor.m_SetEventEditorToDefaultLocation = true;
		GameManager.SetGameMode(GameMode.SANDBOX, GameSubMode.NONE);
		Campaign.m_CurrentLevel = null;
		GameStateManager.SwitchToStateImmediate(GameState.SANDBOX);
		Close();
	}

	private void OnChallenges()
	{
		if (GameManager.IsSteamOffline())
		{
			PopUpMessage.DisplayErrorOkOnly(Localize.Get("UI_STEAM_OFFLINE"));
			return;
		}
		WorkshopItem mostRecentWeeklyChallenge = WeeklyChallenges.GetMostRecentWeeklyChallenge();
		if (mostRecentWeeklyChallenge == null)
		{
			InterfaceAudio.PlayErrorBeep();
			return;
		}
		InterfaceAudio.Play("ui_menu_select");
		GameUI.m_Instance.m_MainMenuNew.Close();
		GameUI.m_Instance.m_WeeklyChallenges.Open(mostRecentWeeklyChallenge.GetId());
	}

	private void OnGallery()
	{
		GameUI.m_Instance.m_MainMenuNew.Close();
		InterfaceAudio.Play("ui_menu_select");
		GameUI.m_Instance.m_Gallery.OpenFromMainMenu(GameStateMainMenu.m_World.m_Id);
		GameUI.m_Instance.m_Gallery.m_ReturnToMainMenu = true;
	}

	private void OnWorkshop()
	{
		GameUI.m_Instance.m_MainMenuNew.Close();
		GameUI.m_Instance.m_Workshop.Open(WorkshopView.LEVELS_AND_CAMPAIGNS);
		InterfaceAudio.Play("ui_menu_select");
	}

	private void OnMods()
	{
		GameUI.m_Instance.m_MainMenuNew.Close();
		GameUI.m_Instance.m_Workshop.Open(WorkshopView.MODS);
		InterfaceAudio.Play("ui_menu_select");
	}

	private void OnSettings()
	{
		GameUI.m_Instance.m_MainMenuNew.Close();
		InterfaceAudio.Play("ui_menu_select");
		GameUI.m_Instance.m_Settings.Open();
	}

	private void OnProfiles()
	{
		GameUI.m_Instance.m_MainMenuNew.Close();
		InterfaceAudio.Play("ui_menu_select");
		GameUI.m_Instance.m_ProfileSelect.Open();
	}

	private void OnExitToDesktop()
	{
		GameManager.QuitWithoutConfirmation();
	}

	private void OnPromoButton()
	{
		InterfaceAudio.Play("ui_menu_select");
		PlayerPrefs.SetInt("ReleasedBannerClicked", 1);
		if (SteamManager.IsLoggedOn())
		{
			SteamFriends.OpenStoreOverlay(3449470);
		}
		else
		{
			Application.OpenURL("https://store.steampowered.com/app/3449470");
		}
	}

	private void ProcessInput()
	{
		if (GameStateCommonInput.IgnoreKeyboardInputForPanel(base.gameObject))
		{
			return;
		}
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			PopUpMessage.DisplayConfirmation(Localize.Get("POPUP_EXIT_GAME"), useYesNoLabels: false, GameManager.QuitWithoutConfirmation);
		}
		if (GamepadManager.ButtonJustPressed(GamepadButtonType.WEST))
		{
			OnSettings();
		}
		if (GamepadManager.ButtonJustPressed(GamepadButtonType.NORTH) && (m_CampaignStartButtonMasked.gameObject.activeInHierarchy || m_StartButtonMasked.gameObject.activeInHierarchy))
		{
			OnStart();
		}
		if ((Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.S)) && !string.IsNullOrEmpty(Profiles.m_ActiveProfile.m_LastLoadedSandbox))
		{
			GameStatePreloadingAssets.PreloadLevel(Path.Combine(SandboxLayout.GetSavePath(Profiles.GetActiveProfileName()), Profiles.m_ActiveProfile.m_LastLoadedSandbox), null, PreloadAutoLoadSandboxCallback);
		}
		if (Input.GetKeyDown(KeyCode.C))
		{
			CampaignLevel levelFromId = CampaignWorlds.m_Instance.GetLevelFromId(Profiles.m_ActiveProfile.m_LastLoadedCampaignLevelId);
			if (levelFromId != null)
			{
				Campaign.m_LevelBeingPreloaded = levelFromId;
				GameStatePreloadingAssets.PreloadLevel(levelFromId.GetLayoutPath(), null, Campaign.DonePreloadFromMainMenu);
			}
		}
		if (Input.GetKeyDown(KeyCode.P))
		{
			if (PlayerPrefs.HasKey(PROMO_DISABLED_KEY))
			{
				PlayerPrefs.DeleteKey(PROMO_DISABLED_KEY);
			}
			else
			{
				PlayerPrefs.SetInt(PROMO_DISABLED_KEY, 1);
			}
			m_PromoBannerButton.gameObject.SetActive(!PlayerPrefs.HasKey(PROMO_DISABLED_KEY));
		}
	}

	private static void PreloadAutoLoadSandboxCallback(string layoutPath, FileSlot slot)
	{
		GameManager.SetGameMode(GameMode.SANDBOX, GameSubMode.NONE);
		GameStateManager.SwitchToStateImmediate(GameState.LOADING_LEVEL_IMMEDIATE);
		if (Sandbox.LoadLayout(layoutPath))
		{
			Sandbox.m_CurrentLayoutName = Path.GetFileName(layoutPath);
			GameStateManager.SwitchToState(GameState.SANDBOX);
		}
	}

	private void UpdateTwitchCallToAction()
	{
		m_TwitchCallToAction.SetActive(value: false);
	}

	private void OnTwitchCallToActionClose()
	{
		File.Create(Path.Combine(Application.persistentDataPath, ".twitch_calltoaction"));
		m_TwitchCallToAction.SetActive(value: false);
	}
}
