using System;
using System.Collections.Generic;
using Landfall.TABS;
using Landfall.TABS.Workshop;
using Landfall.TABS_Input;
using TFBGames;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuStateHandler : UINavigationGroupManager
{
	[SerializeField]
	private Button m_CampaignButton;

	[SerializeField]
	private Button m_SandboxButton;

	[SerializeField]
	private Button m_UnitCreatorButton;

	[SerializeField]
	private Button m_RoadmapButton;

	[SerializeField]
	private Button m_WorkshopButton;

	[SerializeField]
	private Button m_SettingsButton;

	[SerializeField]
	private Button m_multiplayerButton;

	[SerializeField]
	private Transform m_ButtonsContainer;

	[SerializeField]
	[Tooltip("The grid that will display levels within a Campaign.")]
	private CampaignLoaderUI campaignSelectUI;

	[SerializeField]
	[Tooltip("The grid that will display a list of the Campaigns.")]
	private MainMenuSelectedCampaignUI campaignLevelSelectUI;

	[SerializeField]
	[Tooltip("The grid that will display Sandbox levels.")]
	private UISandboxLevelSelector sandboxLevelSelectUI;

	[SerializeField]
	[Tooltip("The UI Navigation Group attached to the Main Menu UI.")]
	private UINavigationGroup mainUI;

	[SerializeField]
	[Tooltip("The Settings UI.")]
	private SettingsHandler settingsUI;

	[SerializeField]
	[Tooltip("The Roadmap UI")]
	private RoadmapHandler roadmapUI;

	[SerializeField]
	[Tooltip("The Mod Browser UI")]
	private UINavigationGroup modBrowserUI;

	[SerializeField]
	[Tooltip("Campaign message UI")]
	private BattleCreatorHandleCampaignRatingUI campaignRatingUI;

	[SerializeField]
	[Tooltip("Multiplayer Menu")]
	private MultiplayerSettingsMenu multiplayerMenu;

	public MenuState m_CurrentMenuState;

	private Button m_lastSelected;

	private IAccountPermissions m_accountPermissions;

	private IUserProfileUI m_userProfileUI;

	private PlayerActions m_playerActions;

	private CursorVisibilityController m_cursorVisibility;

	public TABSCampaignAsset SelectedCampaign { get; private set; }

	public static MainMenuStateHandler Instance { get; private set; }

	protected override void Awake()
	{
		base.Awake();
		if (Instance != null)
		{
			UnityEngine.Object.Destroy(this);
			return;
		}
		Instance = this;
		InitListeners();
		m_CurrentMenuState = MenuState.Main;
		CampaignPlayerDataHolder.BackToMenu();
	}

	private void Start()
	{
		m_cursorVisibility = ServiceLocator.GetService<CursorVisibilityController>();
		m_cursorVisibility.SetLockStateAndVisibility(CursorLockMode.None, visible: true);
		m_lastSelected = m_SandboxButton;
		m_accountPermissions = ServiceLocator.GetService<IAccountPermissions>();
		m_userProfileUI = ServiceLocator.GetService<IUserProfileUI>();
		m_playerActions = PlayerActions.Instance;
		RebuildNavigation();
	}

	private void RebuildNavigation()
	{
		IList<Selectable> selectableChildren = m_ButtonsContainer.GetSelectableChildren();
		if (selectableChildren != null)
		{
			UIHelpers.CreateExplicitLinearNavigation(selectableChildren, horizontal: false);
		}
	}

	public void AddControllerSwitchEvent()
	{
		InputService service = ServiceLocator.GetService<InputService>();
		if (service != null)
		{
			service.InputChanged += OnInputSourceChanged;
		}
	}

	private void OnInputSourceChanged(InputType type)
	{
		if (m_CurrentMenuState == MenuState.Main)
		{
			switch (type)
			{
			case InputType.Controller:
				m_lastSelected.Select();
				break;
			default:
				throw new ArgumentOutOfRangeException("type", type, null);
			case InputType.Keyboard:
			case InputType.Any:
				break;
			}
		}
	}

	private void InitListeners()
	{
		m_CampaignButton.onClick.AddListener(OnCampaignButtonClicked);
		m_RoadmapButton.onClick.AddListener(OnRoadmapButtonClicked);
		m_WorkshopButton.onClick.AddListener(OnWorkshopButtonClicked);
		m_SettingsButton.onClick.AddListener(OnSettingsButtonClicked);
		m_SandboxButton.onClick.AddListener(OnSandboxButtonClicked);
		m_UnitCreatorButton.onClick.AddListener(OnUnitCreatorButtonClicked);
	}

	private void OnCampaignButtonClicked()
	{
		GoToMenu(MenuState.CampaignSelect);
		if (campaignSelectUI != null)
		{
			campaignSelectUI.OpenPage();
			EnableGroup(campaignSelectUI, disableOthers: true);
		}
	}

	private void OnRoadmapButtonClicked()
	{
		GoToMenu(MenuState.Roadmap);
		if (roadmapUI != null)
		{
			EnableGroup(roadmapUI, disableOthers: true);
		}
	}

	private void OnWorkshopButtonClicked()
	{
		if (!m_accountPermissions.IsSignedIn)
		{
			m_userProfileUI?.Hide();
			ServiceLocator.GetService<ModalPanel>().PopUp("POPUP_NOT_SIGNED_IN_TO_VIEW", delegate
			{
				m_userProfileUI?.Show(m_userProfileUI.CanChangeProfile);
				if (m_playerActions.InputType == InputType.Controller)
				{
					m_WorkshopButton.Select();
				}
			});
			return;
		}
		m_userProfileUI?.Hide();
		m_accountPermissions.CanViewAndDownloadUgcAsync(showPopup: true, "POPUP_NOT_ALLOWED_TO_VIEW_UGC", delegate(bool permitted)
		{
			m_userProfileUI?.Show(m_userProfileUI.CanChangeProfile);
			if (m_playerActions.InputType == InputType.Controller)
			{
				m_WorkshopButton.Select();
			}
			if (permitted)
			{
				GoToMenu(MenuState.Workshop);
				EventSystem.current.SetSelectedGameObject(null);
			}
		});
		if (modBrowserUI != null)
		{
			EnableGroup(modBrowserUI, disableOthers: true);
		}
	}

	private void OnSettingsButtonClicked()
	{
		GoToMenu(MenuState.Settings);
		if (settingsUI != null)
		{
			EnableGroup(settingsUI, disableOthers: true);
		}
	}

	private void OnUnitCreatorButtonClicked()
	{
		if (!m_accountPermissions.IsSignedIn)
		{
			m_userProfileUI?.Hide();
			ServiceLocator.GetService<ModalPanel>().PopUp("POPUP_NOT_SIGNED_IN_TO_CREATE", delegate
			{
				m_userProfileUI?.Show(m_userProfileUI.CanChangeProfile);
				if (m_playerActions.InputType == InputType.Controller)
				{
					m_UnitCreatorButton.Select();
				}
			});
		}
		else
		{
			TABSSceneManager.LoadCustomContentPage();
		}
	}

	private void OnSandboxButtonClicked()
	{
		CampaignPlayerDataHolder.StartedPlayingSandbox();
		GoToSandboxLevelSelection();
	}

	public void GoToSandboxLevelSelection()
	{
		GoToMenu(MenuState.LevelSelection);
		if (sandboxLevelSelectUI != null)
		{
			sandboxLevelSelectUI.OpenPage();
			EnableGroup(sandboxLevelSelectUI, disableOthers: true);
		}
	}

	public void GoToMenu(int menuID)
	{
		GoToMenu((MenuState)menuID);
	}

	public void GoToMenu(MenuState menu)
	{
		m_CurrentMenuState = menu;
		switch (menu)
		{
		case MenuState.CampaignSelect:
			SetLastSelectedButton(m_CampaignButton);
			break;
		case MenuState.Settings:
			SetLastSelectedButton(m_SettingsButton);
			break;
		case MenuState.UnitCreator:
			SetLastSelectedButton(m_UnitCreatorButton);
			break;
		case MenuState.LevelSelection:
			SetLastSelectedButton(m_SandboxButton);
			break;
		case MenuState.Roadmap:
			SetLastSelectedButton(m_RoadmapButton);
			break;
		case MenuState.Workshop:
			SetLastSelectedButton(m_WorkshopButton);
			break;
		case MenuState.Main:
			if (mainUI != null)
			{
				EnableGroup(mainUI, disableOthers: true);
			}
			break;
		case MenuState.Multiplayer:
			SetLastSelectedButton(m_multiplayerButton);
			break;
		default:
			throw new ArgumentOutOfRangeException("menu", menu, null);
		case MenuState.CampaignLevels:
		case MenuState.ThanksScreen:
		case MenuState.ScreenSize:
			break;
		}
	}

	public void CloseCampaignSelect()
	{
		if (campaignSelectUI != null)
		{
			campaignSelectUI.ClosePage();
			DisableGroup(campaignSelectUI);
		}
	}

	public void CloseCampaignLevelSelect()
	{
		if (campaignLevelSelectUI != null)
		{
			campaignLevelSelectUI.ClosePage();
			DisableGroup(campaignLevelSelectUI);
		}
		if (m_CurrentMenuState == MenuState.CampaignSelect)
		{
			EnableGroup(campaignSelectUI, disableOthers: true);
			campaignSelectUI.OpenPage();
		}
	}

	public void CloseSandboxSelect()
	{
		if (sandboxLevelSelectUI != null)
		{
			sandboxLevelSelectUI.ClosePage();
			DisableGroup(sandboxLevelSelectUI);
		}
	}

	public void CloseSettingsUI()
	{
		if (settingsUI != null)
		{
			DisableGroup(settingsUI);
			GoToMenu(MenuState.Main);
		}
	}

	public void CloseRoadmapUI()
	{
		if (roadmapUI != null)
		{
			DisableGroup(roadmapUI);
		}
	}

	public void CloseCampaignRatingUI()
	{
		if (campaignRatingUI != null)
		{
			DisableGroup(campaignRatingUI);
		}
	}

	public void CloseModBrowserUI()
	{
		if (modBrowserUI != null)
		{
			DisableGroup(modBrowserUI);
		}
	}

	public void OnCampaignSelected(TABSCampaignAsset campaign)
	{
		SelectedCampaign = campaign;
		GoToMenu(MenuState.CampaignLevels);
		if (campaignLevelSelectUI != null)
		{
			campaignLevelSelectUI.OpenPage();
			EnableGroup(campaignLevelSelectUI, disableOthers: true);
		}
	}

	public void LastSelectable()
	{
		m_lastSelected.Select();
	}

	private void SetLastSelectedButton(Button lastSelected)
	{
		m_lastSelected = lastSelected;
	}

	private void OnDisable()
	{
		InputService service = ServiceLocator.GetService<InputService>();
		if (service != null)
		{
			service.InputChanged -= OnInputSourceChanged;
		}
	}
}
