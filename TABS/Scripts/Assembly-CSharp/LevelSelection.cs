using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BitCode.Networking;
using DM;
using Landfall.TABS;
using Landfall.TABS.GameMode;
using Landfall.TABS.GameState;
using Landfall.TABS.Services;
using Landfall.TABS.Workshop;
using Landfall.TABS_Input;
using LevelCreator;
using TFBGames;
using TMPro;
using UIStateManager;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelSelection : LevelSelector
{
	private const string ResetProgressHeaderLocalizationKey = "RESET_PROGRESS";

	private const string ResetProgressQuestionLocalizationKey = "ARE_YOU_SURE_YOU_WANT_TO_RESET_ALL_YOUR_PROGRESS";

	private const string ResetCampaignButtonLocalizationKey = "RESET_CAMPAIGN_PROGRESS";

	private const string ResetSecretsLocalizationKey = "RESET_UNLOCKED_SECRETS";

	private const string CancelButtonLocalizationKey = "BUTTON_CANCEL";

	[SerializeField]
	private UIComponentMainMenu mainMenuButtonsComponents;

	[SerializeField]
	private TextMeshProUGUI m_permissionsMessage;

	[SerializeField]
	private GameObject m_WorkshopButton;

	[SerializeField]
	private GameObject m_WorkshopCanvas;

	[SerializeField]
	private GameObject m_CampaignTabBar;

	[Header("Campaign Tabs")]
	[SerializeField]
	private Toggle m_tabLandfall;

	[SerializeField]
	private Toggle m_tabDownloaded;

	[SerializeField]
	private Toggle m_tabBattles;

	[SerializeField]
	private Toggle m_tabLocal;

	[Header("Sandbox Tabs")]
	[SerializeField]
	private Toggle m_ToggleMain;

	[SerializeField]
	private Toggle m_ToggleSimulation;

	[SerializeField]
	private GameObject sanboxTABS;

	[SerializeField]
	private GameObject campaignTABS;

	private List<TABSCampaignAsset> m_LandfallCampaigns;

	private TABSCampaignAsset[] m_LoadedCustomCampaigns;

	private TABSCampaignAsset[] m_LoadedLocalCustomCampaigns;

	private TABSCampaignLevelAsset[] m_LoadedCustomLevels;

	private TABSCampaignAsset m_CustomBattlesCampaign;

	private IAccountPermissions m_accountPermissions;

	private PermissionsHelper m_PermissionsHelper;

	private bool selectButton;

	private TABSCampaignAsset m_SelectedCampaignLevel;

	private CampaignPlayCellUI m_SelectedCampaignUICell;

	private List<TABSCampaignLevelAsset> m_CorruptMaps;

	private ModalPanel m_modalPanel;

	private int? m_openModalPanelID;

	private ISaveLoaderService m_saveLoad;

	private IPlayerPrefsPlatform m_PlayerPrefs;

	[SerializeField]
	private UIMapSelector uiMapSelector;

	private bool inGameScene;

	[SerializeField]
	private MainMenuStateHandler mainMenuStateHandler;

	[SerializeField]
	protected CodeAnimation stateCodeAnimation;

	[SerializeField]
	protected CodeAnimation m_BackgroundCodeAnimation;

	[SerializeField]
	protected GameObject m_ResetMouseAndKeyBoard;

	[SerializeField]
	protected GameObject m_ResetGamepad;

	[SerializeField]
	protected Button m_BackEscapeMenu;

	[SerializeField]
	protected Button m_BackSettingsSidePanel;

	[SerializeField]
	private GameObject m_backButtonContainer;

	private bool m_OpenedFromPlacementUI;

	protected bool allowAnimation = true;

	private SocialProfileService m_socialService;

	[Header("Project Mars")]
	[SerializeField]
	private ProjectMarsHandler m_projectMarsHandler;

	private InterfaceStateManager m_stateManager;

	private ITimeService m_timeService;

	private List<TABSCampaignAsset> availableCampaigns;

	private bool m_didLoadLevel;

	public System.Action OpenMenu;

	public System.Action CloseMenu;

	protected override void Awake()
	{
		base.Awake();
		if (m_mainMenuUIHandler == null)
		{
			inGameScene = true;
		}
		if (m_WorkshopButton != null)
		{
			m_WorkshopButton.GetComponent<Button>().onClick.AddListener(OnWorkshopClicked);
		}
	}

	protected override void Start()
	{
		base.Start();
		if (leftPageButton != null)
		{
			leftPageButton.onClick.AddListener(PreviousPage);
		}
		if (smallLeftPageButton != null)
		{
			smallLeftPageButton.onClick.AddListener(PreviousPage);
		}
		if (rightPageButton != null)
		{
			rightPageButton.onClick.AddListener(NextPage);
		}
		if (smallRightPageButton != null)
		{
			smallRightPageButton.onClick.AddListener(NextPage);
		}
		m_tabLandfall.onValueChanged.AddListener(delegate(bool isOn)
		{
			if (isOn)
			{
				UpdateFilter(0);
			}
		});
		m_tabDownloaded.onValueChanged.AddListener(delegate(bool isOn)
		{
			if (isOn)
			{
				UpdateFilter(1);
			}
		});
		m_tabBattles.onValueChanged.AddListener(delegate(bool isOn)
		{
			if (isOn)
			{
				UpdateFilter(2);
				m_SelectedCampaignLevel = m_CustomBattlesCampaign;
			}
		});
		m_tabLocal.onValueChanged.AddListener(delegate(bool isOn)
		{
			if (isOn)
			{
				UpdateFilter(3);
			}
		});
		m_stateManager = UnityEngine.Object.FindObjectOfType<InterfaceStateManager>();
		m_timeService = ServiceLocator.GetService<ITimeService>();
		m_socialService = ServiceLocator.GetService<SocialProfileService>();
		if (m_socialService != null)
		{
			m_socialService.ReceivedInvitation += OnReceivedInvitation;
		}
		if (NetworkBattleUICloser.Instance != null)
		{
			NetworkBattleUICloser.Instance.RegisterComponent(this, OnMultiplayerMatchEnded);
		}
		ShowPermissionsMessage(visible: false);
		if (stateCodeAnimation == null)
		{
			stateCodeAnimation = GetComponent<CodeAnimation>();
		}
		m_modalPanel = ServiceLocator.GetService<ModalPanel>();
		m_saveLoad = ServiceLocator.GetService<ISaveLoaderService>();
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		if (m_socialService != null)
		{
			m_socialService.ReceivedInvitation -= OnReceivedInvitation;
		}
		if (NetworkBattleUICloser.Instance != null)
		{
			NetworkBattleUICloser.Instance.UnregisterComponent(this);
		}
	}

	public void EnableBackButtonContainer(bool enable)
	{
		m_backButtonContainer.SetActive(enable);
	}

	public void OnBackButtonClicked()
	{
		Type type = ServiceLocator.GetService<GameModeService>().CurrentGameMode.GetType();
		bool flag = TABSSceneManager.IsInCustomContentPage();
		if ((type == typeof(SandboxGameMode) || type == typeof(CampaignGameMode) || type == typeof(LocalMultiplayerGameMode) || type == typeof(OnlineMultiplayerGameMode)) && !flag)
		{
			if (m_OpenedFromPlacementUI)
			{
				m_BackSettingsSidePanel.onClick.Invoke();
			}
			else
			{
				m_BackEscapeMenu.onClick.Invoke();
			}
			return;
		}
		switch (levelType)
		{
		case LevelType.Sandbox:
		case LevelType.Campaign:
		case LevelType.LocalMultiplayer:
		case LevelType.OnlineMultiplayer:
			if (flag)
			{
				base.Close();
			}
			else
			{
				m_stateManager.OpenUIComponent(mainMenuButtonsComponents);
			}
			break;
		case LevelType.CampaignLevel:
			levelType = LevelType.Campaign;
			SetAnimatorDirection();
			if (stateCodeAnimation != null)
			{
				stateCodeAnimation.PlayIn();
			}
			Populate();
			break;
		case LevelType.SandboxBattleRoot:
			break;
		}
	}

	public void OnResetButtonClicked()
	{
		m_openModalPanelID = m_modalPanel.ResetProgressChoice("RESET_PROGRESS", "ARE_YOU_SURE_YOU_WANT_TO_RESET_ALL_YOUR_PROGRESS", ClearModalPanelPopUpID, ResetCampaignProgress, ResetSecretsProgress, "BUTTON_CANCEL", "RESET_CAMPAIGN_PROGRESS", "RESET_UNLOCKED_SECRETS");
	}

	private void ClearModalPanelPopUpID()
	{
		m_openModalPanelID = null;
	}

	private void ResetSecretsProgress()
	{
		m_saveLoad.ClearSecrets();
	}

	private void ResetCampaignProgress()
	{
		m_saveLoad.ClearLevels(availableCampaigns);
		if (levelType == LevelType.CampaignLevel)
		{
			OnBackButtonClicked();
		}
	}

	protected override void Populate(int page = 0)
	{
		base.Populate(page);
		Clear();
		switch (levelType)
		{
		case LevelType.Sandbox:
			CampaignPlayerDataHolder.StartedPlayingSandbox();
			PopulateSandbox(page);
			SetUIForSandbox();
			break;
		case LevelType.Campaign:
			SetUIForCampaignSelection();
			PopulateCampaignSelector();
			break;
		case LevelType.CampaignLevel:
			PopulateCampaignLevelSelector();
			SetUIForSelectedCampaign();
			break;
		case LevelType.LocalMultiplayer:
			CampaignPlayerDataHolder.StartedPlayingLocalMultiplayer();
			PopulateSandbox(page);
			SetUIForSandbox();
			break;
		case LevelType.OnlineMultiplayer:
			CampaignPlayerDataHolder.StartedPlayingOnlineMultiplayer();
			PopulateSandbox(page);
			SetUIForSandbox();
			break;
		case LevelType.SandboxBattleRoot:
			break;
		}
	}

	private void OnReceivedInvitation(IGameInvitation invite)
	{
		SafeClose();
	}

	private void OnMultiplayerMatchEnded()
	{
		SafeClose();
	}

	private void SafeClose()
	{
		if (base.IsOpen)
		{
			OnBackButtonClicked();
		}
	}

	private void SetAnimatorDirection()
	{
		switch (levelType)
		{
		case LevelType.Sandbox:
		case LevelType.Campaign:
		case LevelType.LocalMultiplayer:
		case LevelType.OnlineMultiplayer:
		{
			if (!(stateCodeAnimation != null))
			{
				break;
			}
			CodeAnimationInstance[] animations = stateCodeAnimation.animations;
			foreach (CodeAnimationInstance codeAnimationInstance2 in animations)
			{
				if (codeAnimationInstance2.multiplier > 0f)
				{
					codeAnimationInstance2.multiplier *= -1f;
				}
			}
			break;
		}
		case LevelType.CampaignLevel:
		{
			if (!(stateCodeAnimation != null))
			{
				break;
			}
			CodeAnimationInstance[] animations = stateCodeAnimation.animations;
			foreach (CodeAnimationInstance codeAnimationInstance in animations)
			{
				if (codeAnimationInstance.multiplier < 0f)
				{
					codeAnimationInstance.multiplier *= -1f;
				}
			}
			break;
		}
		case LevelType.SandboxBattleRoot:
			break;
		}
	}

	public void SetForSandbox()
	{
		levelType = LevelType.Sandbox;
		OpenPage();
	}

	public void SetForCampaign()
	{
		levelType = LevelType.Campaign;
		OpenPage();
	}

	public void SetForLocalMultiplayer()
	{
		levelType = LevelType.LocalMultiplayer;
		OpenPage();
	}

	public void SetForProjectMars()
	{
		levelType = LevelType.OnlineMultiplayer;
		OpenPage();
	}

	public void SetLevelGridForCurrentGameType()
	{
		switch (CampaignPlayerDataHolder.CurrentGameModeState)
		{
		case GameModeState.Campaign:
			levelType = LevelType.CampaignLevel;
			break;
		case GameModeState.LocalMultiplayer:
			levelType = LevelType.LocalMultiplayer;
			break;
		case GameModeState.OnlineMultiplayer:
			levelType = LevelType.OnlineMultiplayer;
			break;
		default:
			levelType = LevelType.Sandbox;
			break;
		}
		if (m_SelectedCampaignUICell == null)
		{
			m_SelectedCampaignLevel = CampaignPlayerDataHolder.SelectedCampaign;
		}
		OpenPage();
	}

	public override void OpenPage()
	{
		m_didLoadLevel = false;
		if (stateCodeAnimation != null)
		{
			SetAnimatorDirection();
			stateCodeAnimation.PlayIn();
		}
		switch (levelType)
		{
		case LevelType.Campaign:
			OpenCampaign();
			break;
		case LevelType.CampaignLevel:
			OpenCampaignLevels();
			break;
		}
		if (m_PageTabs != null && m_PageTabs.Length != 0)
		{
			m_TabIndex = Mathf.Clamp(m_TabIndex, 0, m_PageTabs.Length - 1);
			m_PageTabs[m_TabIndex].OnSubmit(new BaseEventData(EventSystem.current));
		}
		base.OpenPage();
	}

	public void SetAnimationPosition()
	{
		SetAnimatorDirection();
		if (stateCodeAnimation != null)
		{
			stateCodeAnimation.PlayIn();
		}
	}

	private void OpenCampaignLevels()
	{
		m_CorruptMaps = new List<TABSCampaignLevelAsset>();
		if (m_SelectedCampaignUICell != null)
		{
			m_SelectedCampaignLevel = m_SelectedCampaignUICell.CurrentCampaign;
		}
		ChangePage(0);
	}

	private void OpenCampaign()
	{
		if (unitDatabase != null && m_LandfallCampaigns == null)
		{
			m_LandfallCampaigns = unitDatabase.GetAllCampaigns().ToList();
		}
		List<TABSCampaignAsset> list = unitDatabase.GetUserCampaigns().ToList();
		List<TABSCampaignAsset> list2 = new List<TABSCampaignAsset>();
		foreach (TABSCampaignAsset item in list)
		{
			if (item.IsModCampaign)
			{
				list2.Add(item);
			}
		}
		m_LoadedCustomCampaigns = list2.ToArray();
		m_LoadedCustomLevels = unitDatabase.GetUserCampaignLevels().ToArray();
		List<TABSCampaignAsset> list3 = new List<TABSCampaignAsset>();
		foreach (TABSCampaignAsset item2 in list)
		{
			if (!item2.IsModCampaign)
			{
				list3.Add(item2);
			}
		}
		m_LoadedLocalCustomCampaigns = list3.ToArray();
	}

	private void SetUIForSandbox()
	{
		m_PageTabs = new Toggle[2] { m_ToggleMain, m_ToggleSimulation };
		if (m_selectionTitle != null)
		{
			m_selectionTitle.Localized = true;
			m_selectionTitle.LocaleID = "LABEL_TITLE_SANDBOX";
		}
		SetTabsActive(sandbox: true, campaign: false);
	}

	private void SetUIForCampaignSelection()
	{
		m_PageTabs = new Toggle[2] { m_tabLandfall, m_tabDownloaded };
		if (m_selectionTitle != null)
		{
			m_selectionTitle.Localized = true;
			m_selectionTitle.LocaleID = "LABEL_TITLE_CAMPAIGN";
		}
		if (sanboxTABS != null)
		{
			sanboxTABS.SetActive(value: false);
		}
		bool flag = false;
		flag = true;
		m_accountPermissions = ServiceLocator.GetService<IAccountPermissions>();
		m_PermissionsHelper = ServiceLocator.GetService<PermissionsHelper>();
		m_tabDownloaded.gameObject.SetActive(m_accountPermissions.IsSignedIn && m_PermissionsHelper.CanViewDownloadTabs);
		SetTabsActive(sandbox: false, flag);
	}

	private void SetUIForSelectedCampaign()
	{
		if (m_selectionTitle != null)
		{
			m_selectionTitle.Localized = !m_SelectedCampaignLevel.IsCustomCampaign;
			m_selectionTitle.LocaleID = m_SelectedCampaignLevel.Entity.Name;
		}
		SetTabsActive(sandbox: false, campaign: false);
	}

	private void SetTabsActive(bool sandbox, bool campaign)
	{
		if (sanboxTABS != null)
		{
			sanboxTABS.SetActive(sandbox);
		}
		if (campaignTABS != null)
		{
			campaignTABS.SetActive(campaign);
		}
	}

	private void PopulateSandbox(int page)
	{
		m_LevelButtons = new List<Button>();
		MapAsset[] array = unitDatabase.GetMapAssetsByType(mapType, onlyUnlocked: true).ToArray();
		totalPages = Mathf.CeilToInt((float)array.Length / 12f);
		int num = array.Length - 12 * page;
		if (m_PageCounter != null)
		{
			m_PageCounter.Set(page + 1, totalPages);
			SetPageButtons(totalPages);
		}
		bool flag = false;
		for (int i = 0; i < Mathf.Min(12, num); i++)
		{
			int num2 = array.Length - (num - i);
			MapAsset map = array[num2];
			if (map.MapName == "LevelScene")
			{
				continue;
			}
			GameObject buttonInstance = UnityEngine.Object.Instantiate(m_templateLevelButton, m_Grid);
			buttonInstance.SetActive(value: true);
			MapAsset currentMap = array[num2];
			currentMap.Entity.GetSpriteIconAsync(delegate(Sprite sprite)
			{
				if (buttonInstance != null)
				{
					buttonInstance.GetComponent<MapGrid>().Setup(sprite, currentMap.Entity.Name, localized: true);
				}
			});
			Button component = buttonInstance.GetComponent<Button>();
			component.onClick.AddListener(delegate
			{
				bool flag2 = true;
				GameModeService service = ServiceLocator.GetService<GameModeService>();
				if (CampaignPlayerDataHolder.CurrentGameModeState == GameModeState.Sandbox)
				{
					service.SetGameMode<SandboxGameMode>();
				}
				else if (CampaignPlayerDataHolder.CurrentGameModeState == GameModeState.LocalMultiplayer)
				{
					service.SetGameMode<LocalMultiplayerGameMode>();
				}
				else if (CampaignPlayerDataHolder.CurrentGameModeState == GameModeState.OnlineMultiplayer)
				{
					flag2 = false;
					if (m_projectMarsHandler != null && m_stateManager != null)
					{
						base.Close?.Invoke();
						m_projectMarsHandler.CheckPermissionsOnOpen = false;
						if (!m_projectMarsHandler.IsActive)
						{
							m_stateManager.OpenUIComponent(m_projectMarsHandler);
						}
						bool isPublicSession = m_projectMarsHandler.IsPublicSession;
						m_projectMarsHandler.CreateMars(mapType, map.m_mapIndex, isPublicSession);
					}
					else
					{
						ModalPanel service2 = ServiceLocator.GetService<ModalPanel>();
						NetworkBattleController service3 = ServiceLocator.GetService<NetworkBattleController>();
						if (service3 == null)
						{
							service2.PopUp("MP_POPUP_MAPCHANGE_NEED_BATTLE_SCENE");
							return;
						}
						if (ServiceLocator.GetService<GameStateManager>().GameState == GameState.BattleState)
						{
							service2.PopUp("MP_POPUP_MAPCHANGE_IN_BATTLE");
							return;
						}
						service3.RequestChangeMap(mapType, map.m_mapIndex);
						service3.WaitingForOtherPlayerToAcceptMapOpenId = service2.PopUp("MP_POPUP_MAPCHANGE_WAITING_FOR_PLAYER", CancelMapChangeRequest, "BUTTON_CANCEL", -1f, false);
						m_timeService.SetState(1f, 0f);
					}
				}
				if (uiMapSelector != null)
				{
					uiMapSelector.CloseMapSelector();
				}
				if (flag2)
				{
					CampaignHandler.ResetLoadedLevel();
					TABSSceneManager.LoadMap(map);
				}
			});
			if (inGameScene && TABSSceneManager.CurrentLoadedMap == map)
			{
				buttonInstance.GetComponent<MapGrid>().Selected();
				component.Select();
				flag = true;
			}
			m_LevelButtons.Add(component);
			SpawnedButtons.Add(buttonInstance);
		}
		if (!flag)
		{
			SelectFirstButtonInList();
		}
	}

	private void SelectCurrentMap()
	{
		if (inGameScene)
		{
			StartCoroutine(Delay());
		}
		IEnumerator Delay()
		{
			yield return null;
			for (int i = 0; i < Enum.GetNames(typeof(MapAsset.MapType)).Length; i++)
			{
				ChangeMapType(i);
				yield return null;
				if (GoToMap((MapAsset.MapType)i))
				{
					yield break;
				}
			}
			ChangeMapType(0);
		}
		bool GoToMap(MapAsset.MapType mapType)
		{
			MapAsset[] array = unitDatabase.GetMapAssetsByType(mapType, onlyUnlocked: true).ToArray();
			MapAsset currentLoadedMap = TABSSceneManager.CurrentLoadedMap;
			int num = -1;
			for (int i = 0; i < array.Length; i++)
			{
				if (i % 12 == 0)
				{
					num++;
				}
				if (currentLoadedMap == array[i])
				{
					ChangePage(num);
					return true;
				}
			}
			return false;
		}
	}

	private void CancelMapChangeRequest()
	{
		NetworkBattleController service = ServiceLocator.GetService<NetworkBattleController>();
		if (service != null)
		{
			service.CancelMapChangeRequest();
		}
	}

	private void SetPageButtons(int pageCount)
	{
		currentAspectRatio = ScreenHelpers.GetAspectRatio();
		bool flag = currentAspectRatio < buttonSwapAspectRatioThreshold;
		bool flag2 = pageCount > 1;
		if (leftPageButton != null)
		{
			leftPageButton.gameObject.SetActive(flag2 && !flag);
		}
		if (rightPageButton != null)
		{
			rightPageButton.gameObject.SetActive(flag2 && !flag);
		}
		if (smallLeftPageButton != null)
		{
			smallLeftPageButton.gameObject.SetActive(flag2 && flag);
		}
		if (smallRightPageButton != null)
		{
			smallRightPageButton.gameObject.SetActive(flag2 && flag);
		}
	}

	private void PopulateCampaignSelector()
	{
		m_LevelButtons = new List<Button>();
		ShowPermissionsMessage(visible: false);
		for (int num = m_Grid.childCount - 1; num >= 0; num--)
		{
			UnityEngine.Object.Destroy(m_Grid.GetChild(num).gameObject);
		}
		switch (m_TabIndex)
		{
		case 0:
			if (m_LandfallCampaigns == null)
			{
				m_LandfallCampaigns = unitDatabase.GetAllCampaigns().ToList();
			}
			availableCampaigns = new List<TABSCampaignAsset>();
			foreach (TABSCampaignAsset landfallCampaign in m_LandfallCampaigns)
			{
				if (string.IsNullOrWhiteSpace(landfallCampaign.Entity.UnlockKey) || ServiceLocator.GetService<ISaveLoaderService>().HasUnlockedSecret(landfallCampaign.Entity.UnlockKey))
				{
					availableCampaigns.Add(landfallCampaign);
				}
			}
			SpawnCampaignCells(availableCampaigns.ToArray());
			break;
		case 1:
			if (!m_accountPermissions.IsSignedIn)
			{
				ShowPermissionsMessage(visible: true, "POPUP_NOT_SIGNED_IN_TO_VIEW");
			}
			else
			{
				SpawnCampaignCells(m_LoadedCustomCampaigns, onlyModCampaigns: true);
			}
			break;
		case 2:
			if (!m_accountPermissions.IsSignedIn)
			{
				ShowPermissionsMessage(visible: true, "POPUP_NOT_SIGNED_IN_TO_VIEW");
			}
			else
			{
				MakeCustomCampaign();
			}
			break;
		case 3:
			SpawnCampaignCells(m_LoadedLocalCustomCampaigns);
			break;
		}
		selectButton = true;
		SetUpNavigation();
		SelectFirstButtonInList();
	}

	private void PopulateCampaignLevelSelector()
	{
		if (m_SelectedCampaignLevel == null)
		{
			m_SelectedCampaignLevel = m_SelectedCampaignUICell.CurrentCampaign;
		}
		TABSCampaignLevelAsset[] levelsInCampaign = m_SelectedCampaignLevel.LevelsInCampaign;
		totalPages = Mathf.CeilToInt((float)levelsInCampaign.Length / 12f);
		int num = levelsInCampaign.Length - 12 * currentPage;
		if (m_PageCounter != null)
		{
			m_PageCounter.Set(currentPage + 1, totalPages);
			SetPageButtons(totalPages);
		}
		Clear();
		if (m_CorruptMaps == null)
		{
			m_CorruptMaps = new List<TABSCampaignLevelAsset>();
		}
		bool flag = false;
		bool flag2 = ServiceLocator.GetService<GameModeService>().IsCurrentBaseGameModeType<MainMenuGameMode>();
		for (int i = 0; i < Mathf.Min(12, num); i++)
		{
			int num2 = levelsInCampaign.Length - (num - i);
			if (levelsInCampaign[num2] == null)
			{
				m_CorruptMaps.Add(levelsInCampaign[num2]);
				continue;
			}
			bool flag3 = ServiceLocator.GetService<ISaveLoaderService>().HasBeatenLevel(levelsInCampaign[num2].Entity.GUID, m_SelectedCampaignLevel.Entity.GUID);
			bool flag4 = num2 == 0 || ServiceLocator.GetService<ISaveLoaderService>().HasBeatenLevel(levelsInCampaign[num2 - 1].Entity.GUID, m_SelectedCampaignLevel.Entity.GUID);
			bool flag5 = ServiceLocator.GetService<DebugService>() != null && ServiceLocator.GetService<DebugService>().HasUnlockedProgress;
			bool flag6 = flag3 || flag4 || flag5;
			bool newLevel = !flag5 && !flag3 && flag6;
			CampaignSelectedCellUI campaignSelectedCellUI = SpawnCampaignLevelCell(num2, levelsInCampaign[num2], flag6, newLevel);
			m_LevelButtons.Add(campaignSelectedCellUI.GetComponent<Button>());
			SpawnedButtons.Add(campaignSelectedCellUI.gameObject);
			if (CampaignPlayerDataHolder.SelectedCampaign != null && !flag2 && CampaignPlayerDataHolder.GetCurrentLevel() == levelsInCampaign[num2])
			{
				campaignSelectedCellUI.GetComponent<MapGrid>().Selected();
				campaignSelectedCellUI.GetComponent<Button>().Select();
				flag = true;
			}
		}
		if (m_CorruptMaps.Count > 0)
		{
			string empty = string.Empty;
			foreach (TABSCampaignLevelAsset corruptMap in m_CorruptMaps)
			{
				_ = corruptMap;
			}
			ServiceLocator.GetService<ModalPanel>().PopUp("POPUP_INVALIDLEVELS", empty, "\n");
		}
		else
		{
			if (!flag)
			{
				SelectFirstButtonInList();
			}
			_ = m_playerActions.InputType;
		}
	}

	private void SelectCurrentLevel()
	{
		if (inGameScene && !TABSSceneManager.IsInCustomContentPage())
		{
			StartCoroutine(Delay());
		}
		IEnumerator Delay()
		{
			yield return null;
			if (m_SelectedCampaignLevel == null)
			{
				m_SelectedCampaignLevel = m_SelectedCampaignUICell.CurrentCampaign;
			}
			TABSCampaignLevelAsset[] levelsInCampaign = m_SelectedCampaignLevel.LevelsInCampaign;
			TABSCampaignLevelAsset currentLevel = CampaignPlayerDataHolder.GetCurrentLevel();
			int num = -1;
			for (int i = 0; i < levelsInCampaign.Length; i++)
			{
				if (currentLevel == levelsInCampaign[i])
				{
					ChangePage(num);
					break;
				}
				if (i % 12 == 0)
				{
					num++;
				}
			}
		}
	}

	private CampaignSelectedCellUI SpawnCampaignLevelCell(int index, TABSCampaignLevelAsset reference, bool unlocked, bool newLevel)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(m_templateLevelButton, m_Grid, worldPositionStays: false);
		MapGrid component = gameObject.GetComponent<MapGrid>();
		Sprite sprite = null;
		if (!reference.IsModIOLevel)
		{
			sprite = reference.Entity.LevelCellSpriteIcon;
		}
		component.Setup(sprite, reference.Entity.Name, !reference.IsCustomCampaignLevel, reference.CampaignInfo.Description);
		component.CheckLevelWinStreak(reference.Entity.GUID, m_SelectedCampaignLevel.Entity.GUID);
		CampaignSelectedCellUI cellUI = gameObject.FetchComponent<CampaignSelectedCellUI>();
		cellUI.Init(index, reference, delegate
		{
			OnLevelClicked(cellUI);
		}, unlocked, newLevel);
		gameObject.transform.localScale = Vector3.one;
		gameObject.SetActive(value: true);
		if (reference.IsModIOLevel || reference.IsCustomCampaignLevel)
		{
			MapGrid gridCell = component;
			CampaignHandler.GetBattleSprite(reference, delegate(Sprite sprite2)
			{
				if (gridCell != null)
				{
					gridCell.Setup(sprite2, reference.Entity.Name, !reference.IsCustomCampaignLevel, reference.CampaignInfo.Description);
				}
			});
		}
		else if ((bool)sprite)
		{
			gameObject.GetComponent<Image>().sprite = sprite;
		}
		return cellUI;
	}

	private void SpawnCampaignCells(TABSCampaignAsset[] campaigns, bool onlyModCampaigns = false)
	{
		totalPages = Mathf.CeilToInt((float)campaigns.Length / 12f);
		int num = campaigns.Length - 12 * currentPage;
		if (m_PageCounter != null)
		{
			m_PageCounter.Set(currentPage + 1, totalPages);
			SetPageButtons(totalPages);
		}
		for (int i = 0; i < Mathf.Min(12, num); i++)
		{
			int num2 = campaigns.Length - (num - i);
			if (campaigns[num2].IsModCampaign && onlyModCampaigns)
			{
				SpawnCampaignCell(campaigns[num2]);
			}
			else if (!campaigns[num2].IsModCampaign && !onlyModCampaigns)
			{
				SpawnCampaignCell(campaigns[num2]);
			}
		}
	}

	private void SpawnCampaignCell(TABSCampaignAsset campaign)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(m_templateLevelButton, m_Grid, worldPositionStays: false);
		CampaignPlayCellUI cellUI = gameObject.FetchComponent<CampaignPlayCellUI>();
		cellUI.Init(campaign, delegate
		{
			OnCampaignClicked(cellUI);
		});
		MapGrid component = gameObject.GetComponent<MapGrid>();
		Sprite sprite = null;
		if (!campaign.IsModCampaign)
		{
			sprite = campaign.Entity.LevelCellSpriteIcon;
		}
		component.Setup(sprite, campaign.Entity.Name, !campaign.IsCustomCampaign, campaign.CampaignInfo.Description);
		component.CheckCamapaignWinStreak(campaign.Entity.GUID);
		gameObject.transform.localScale = Vector3.one;
		gameObject.SetActive(value: true);
		m_LevelButtons.Add(gameObject.GetComponent<Button>());
		if (!campaign.IsModCampaign && !campaign.IsCustomCampaign)
		{
			return;
		}
		MapGrid gridCell = component;
		CampaignHandler.GetCampaignSprite(campaign, delegate(Sprite sprite2)
		{
			if (gridCell != null)
			{
				gridCell.Setup(sprite2, campaign.Entity.Name, !campaign.IsCustomCampaign, campaign.CampaignInfo.Description);
			}
		});
	}

	private void MakeCustomCampaign()
	{
		if (m_LoadedCustomLevels.Length != 0)
		{
			m_CustomBattlesCampaign = ScriptableObject.CreateInstance<TABSCampaignAsset>();
			m_CustomBattlesCampaign.SetData("LABEL_CUSTOM_BATTLES_CAMPAIGN_TITLE", m_LoadedCustomLevels);
			totalPages = Mathf.CeilToInt((float)m_LoadedCustomLevels.Length / 12f);
			int num = m_LoadedCustomLevels.Length - 12 * currentPage;
			if ((bool)m_PageCounter)
			{
				m_PageCounter.Set(currentPage + 1, totalPages);
				SetPageButtons(totalPages);
			}
			for (int i = 0; i < Mathf.Min(12, num); i++)
			{
				int num2 = m_LoadedCustomLevels.Length - (num - i);
				SpawnLevelCell(num2, m_LoadedCustomLevels[num2], unlocked: true, newLevel: false);
			}
		}
	}

	private void SetUpNavigation()
	{
		UIHelpers.CreateAutomaticNavigation(m_Grid.GetSelectableChildren());
	}

	protected override void Clear()
	{
		base.Clear();
		if (SpawnedButtons != null)
		{
			for (int i = 0; i < SpawnedButtons.Count; i++)
			{
				UnityEngine.Object.Destroy(SpawnedButtons[i]);
			}
			SpawnedButtons.Clear();
		}
		for (int num = m_Grid.childCount - 1; num >= 0; num--)
		{
			UnityEngine.Object.Destroy(m_Grid.GetChild(num).gameObject);
		}
	}

	protected override void Update()
	{
		base.Update();
		if (m_playerActions.m_resetSettings.WasPressed)
		{
			if (m_openModalPanelID.HasValue)
			{
				return;
			}
			OnResetButtonClicked();
		}
		if (m_playerActions.m_back.WasPressed)
		{
			if (m_openModalPanelID.HasValue)
			{
				ClearModalPanelPopUpID();
			}
			else
			{
				OnBackButtonClicked();
			}
		}
	}

	public void ChangeMapType(int map)
	{
		int length = Enum.GetValues(typeof(MapAsset.MapType)).Length;
		if (map < 0)
		{
			map = length - 1;
		}
		else if (map >= length)
		{
			map = 0;
		}
		else if (map >= m_PageTabs.Length)
		{
			map = 0;
		}
		MapAsset.MapType mapType = (MapAsset.MapType)map;
		if (base.mapType != mapType)
		{
			m_PageTabs[map].isOn = true;
			m_TabIndex = map;
			base.mapType = mapType;
			Populate();
		}
	}

	private void OnLevelClicked(CampaignSelectedCellUI cellUI)
	{
		Load(cellUI);
	}

	private void Load(CampaignSelectedCellUI selectedCellUI)
	{
		if (m_didLoadLevel)
		{
			return;
		}
		m_didLoadLevel = true;
		((System.Action)delegate
		{
			TABSCampaignLevelAsset campaignLevel = ContentDatabase.Instance().GetCampaignLevel(selectedCellUI.CampaignLevelReference.Entity.GUID);
			m_SelectedCampaignLevel.LevelsInCampaign[selectedCellUI.CampaignIndex] = campaignLevel;
			CampaignPlayerDataHolder.StartedPlayingNewCampaign(m_SelectedCampaignLevel, selectedCellUI.CampaignIndex);
			Debug.Log("Loading campaign Level: " + selectedCellUI.CampaignLevelReference.Entity.Name);
			_ = campaignLevel.CustomMap;
			CustomMap userMap = ContentDatabase.Instance().GetUserMap(campaignLevel.CustomMap);
			if (userMap != null)
			{
				SpawnLevel.SetCustomMapToLoad(userMap);
			}
			TABSSceneManager.LoadCampaign();
		})();
	}

	private void ShowPermissionsMessage(bool visible, string newMessage = null)
	{
		if (m_permissionsMessage.gameObject.activeSelf != visible)
		{
			m_permissionsMessage.gameObject.SetActive(visible);
		}
		if (!string.IsNullOrEmpty(newMessage))
		{
			m_permissionsMessage.text = newMessage;
		}
	}

	private void UpdateFilter(int tabIndex)
	{
		m_TabIndex = tabIndex;
		Populate();
	}

	private void OnCampaignClicked(CampaignPlayCellUI cellUI)
	{
		m_SelectedCampaignUICell = cellUI;
		LoadCampaign();
	}

	private void LoadCampaign()
	{
		levelType = LevelType.CampaignLevel;
		OpenPage();
	}

	protected override void OnOpen()
	{
		m_didLoadLevel = false;
		base.OnOpen();
		if (stateCodeAnimation != null && allowAnimation)
		{
			stateCodeAnimation.PlayIn();
		}
		SetAnimatorDirection();
		if (m_OpenedFromPlacementUI && m_BackgroundCodeAnimation != null && !m_BackgroundCodeAnimation.IsVisible)
		{
			m_BackgroundCodeAnimation.PlayIn();
		}
		EnableBackButtonContainer(enable: true);
		if (ServiceLocator.GetService<GameModeService>().IsCurrentBaseGameModeType<CampaignGameMode>())
		{
			SelectCurrentLevel();
		}
		else
		{
			SelectCurrentMap();
		}
		OpenMenu?.Invoke();
		switch (PlayerActions.Instance.InputType)
		{
		case InputType.Controller:
			m_ResetGamepad.SetActive(TABSSceneManager.IsInMainMenuScene());
			m_ResetMouseAndKeyBoard.SetActive(value: false);
			break;
		case InputType.Keyboard:
			m_ResetMouseAndKeyBoard.SetActive(TABSSceneManager.IsInMainMenuScene());
			m_ResetGamepad.SetActive(value: false);
			break;
		}
	}

	protected override void OnClose()
	{
		base.OnClose();
		if (stateCodeAnimation != null && allowAnimation)
		{
			stateCodeAnimation.PlayOut();
		}
		SetAnimatorDirection();
		if (m_OpenedFromPlacementUI && m_BackgroundCodeAnimation != null && m_BackgroundCodeAnimation.IsVisible)
		{
			m_BackgroundCodeAnimation.PlayOut();
		}
		CloseMenu?.Invoke();
	}

	public void OpenFromPlacementUI(bool closeBackground)
	{
		m_OpenedFromPlacementUI = closeBackground;
	}

	private void OnWorkshopClicked()
	{
		m_WorkshopCanvas.SetActive(value: true);
	}

	private void SpawnLevelCell(int index, TABSCampaignLevelAsset reference, bool unlocked, bool newLevel)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(m_templateLevelButton, m_Grid, worldPositionStays: false);
		MapGrid mapGrid = gameObject.GetComponent<MapGrid>();
		reference.Entity.GetSpriteIconAsync(delegate(Sprite sprite)
		{
			if (mapGrid != null)
			{
				mapGrid.Setup(sprite, reference.Entity.Name, !reference.IsCustomCampaignLevel, reference.CampaignInfo.Description);
			}
		});
		mapGrid.CheckLevelWinStreak(reference.Entity.GUID, CampaignPlayerDataHolder.GetCurrentCampaignID);
		CampaignSelectedCellUI cellUI = gameObject.FetchComponent<CampaignSelectedCellUI>();
		cellUI.Init(index, reference, delegate
		{
			OnLevelClicked(cellUI);
		}, unlocked, newLevel);
		gameObject.transform.localScale = Vector3.one;
		gameObject.SetActive(value: true);
		m_LevelButtons.Add(gameObject.GetComponent<Button>());
		if (!reference.IsModIOLevel && !reference.IsCustomCampaignLevel)
		{
			return;
		}
		Image cellImage = gameObject.GetComponent<Image>();
		CampaignHandler.GetBattleSprite(reference, delegate(Sprite sprite)
		{
			if (cellImage != null)
			{
				cellImage.sprite = sprite;
			}
		});
	}
}
