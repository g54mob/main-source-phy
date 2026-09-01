using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DM;
using GamepadUI.StateManager.Core;
using Landfall.TABC;
using Landfall.TABS.Budget;
using Landfall.TABS.GameMode;
using Landfall.TABS.GameState;
using Landfall.TABS.UI;
using Landfall.TABS.UnitPlacement;
using Landfall.TABS.WinConditions;
using Landfall.TABS.Workshop;
using Landfall.TABS_Input;
using LevelCreator;
using Photon.Bolt;
using TFBGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Landfall.TABS
{
	public class PlacementUI : UIComponent, IPlacementUI
	{
		public delegate void ClearButtonDelegate(Team team);

		private ContentDatabase m_unitDatabase;

		private const string m_DefaultUnitName = "UNIT_TRIBAL_CLUB";

		private const string StartText = "READY";

		private const string StopText = "STOP";

		[Header("Unit Selection")]
		[SerializeField]
		private GameObject m_ComingSoon;

		[SerializeField]
		private Transform units;

		[SerializeField]
		private UILayoutGroup unitLayoutGroup;

		[SerializeField]
		private PlacementFactionBar factionBar;

		[SerializeField]
		private UIPointerOver m_pointerOverUnits;

		[SerializeField]
		private BattleRadialMenu m_RadialMenu;

		[SerializeField]
		private UnitInfoBar m_UnitInfoBar;

		[SerializeField]
		private GameObject m_LegacyUnitSelect;

		private float mouseScrollDelta;

		[Header("Unit Placement")]
		[SerializeField]
		private MoneyUI redMoney;

		[SerializeField]
		private MoneyUI blueMoney;

		[SerializeField]
		private MovingUIElement topButtons;

		[SerializeField]
		private MovingUIElement[] m_GlyphElements;

		[SerializeField]
		private GameObject m_ClearRedObject;

		[SerializeField]
		private GameObject m_ClearBlueObject;

		[SerializeField]
		private GameObject m_MoneyRedObject;

		[SerializeField]
		private GameObject m_MoneyBlueObject;

		[SerializeField]
		private float m_clearUnitsTime = 0.5f;

		[SerializeField]
		private float m_radialInputHoldThreshold = 0.5f;

		[SerializeField]
		private float m_clearUnitsDelay = 0.5f;

		[SerializeField]
		private WinConditionPresenter m_WinConditionPresenterAnimator;

		[SerializeField]
		private SceneDescriptionUI m_sceneDescription;

		[SerializeField]
		private Button m_OpenAdditonalSidePanelComponent;

		[SerializeField]
		private ExpandedFactionUI expandedFactionUI;

		[SerializeField]
		[Tooltip(" The button that opens the Expanded Faction Selector that will open the Expanded Faction UI state")]
		private Button openExpandedFactionUIButton;

		[Header("Project Mars")]
		[SerializeField]
		private ProjectMarsBattleUserUI redUser;

		[SerializeField]
		private ProjectMarsBattleUserUI blueUser;

		[SerializeField]
		private TMP_Text startButtonLabel;

		[SerializeField]
		private PlayerProfileConfiguration stubProfileConfiguration;

		[Header("Brush Adjustments")]
		[SerializeField]
		private EscapeMenuComponent m_escapeMenuComponent;

		[SerializeField]
		private AdditionalSettingsSidePanelComponent m_additionalSettingsSidePanelComponent;

		[Header("Additional UI Logic")]
		[SerializeField]
		private Button showMapSelect;

		[SerializeField]
		private Button showInputViewer;

		[SerializeField]
		private InputViewer inputViewer;

		private LocalMultiplayerGameRules m_localMultiplayerGameRules;

		private float m_invClearUnitsTime;

		private float m_RadialTimer;

		private float m_clearUnitsDelayCounter;

		private InputService m_inputService;

		private CursorController m_cursorController;

		private ClearButtonDelegate m_onClickedClearButton;

		private Faction m_selectedFaction;

		private static int m_lastSelectedFaction;

		private int m_numberOfFactions;

		private FactionButton selectedFactionButton;

		private UnitButton m_selectedButton;

		private Dictionary<FactionButton, Guid> m_FactionButtonsIndex;

		private static DatabaseID m_lastSelectedUnit;

		private int m_lastSelectedUnitIndex;

		private List<DatabaseID> m_unitIDs;

		private PlayerActions m_playerActions;

		private UnitPlacementBrush m_unitPlacementBrush;

		private PlacementCursor m_placementCursor;

		private UIMapSelector m_mapSelector;

		private UIGlyphsManager m_glyphManager;

		private SettingsInstance m_flipColorsSetting;

		private bool hasRegisteredFlipColors;

		private int currentSwapValue;

		private bool shouldResetLayout;

		private bool m_UseGamepadInputUI;

		private bool m_OverrideAnimations;

		private bool m_IsSandbox;

		private bool m_IsEsacpeMenuOpen;

		private bool m_IsSideMenuOpen;

		private EscapeMenuHandler escapeMenu;

		private SettingsProfileManager m_SettingsProfileManager;

		private BaseGameMode m_currentGameMode;

		private SandboxPlacementCamera m_placementCamera;

		private SimpleStateAnimation m_LegacyUnitAnimator;

		private PlacementCursorState m_currentValidCursorState;

		private float m_clearUnitsTimer;

		private GameStateManager m_gameStateManager;

		private MaxUnitsController m_maxUnits;

		private ControllerService controllerService;

		private NetworkBattleController m_networkBattle;

		private INetworkService m_networkService;

		private AccountManager m_accountManager;

		private SocialProfileService m_socialService;

		private NetworkBattleController NetworkBattle
		{
			get
			{
				if (m_networkBattle == null)
				{
					m_networkBattle = ServiceLocator.GetService<NetworkBattleController>();
				}
				return m_networkBattle;
			}
		}

		public static PlacementUI Instance { get; private set; }

		public float FactionXMin { get; private set; }

		public float FactionXMax { get; private set; }

		public bool IsEscapeMenuOpen => m_IsEsacpeMenuOpen;

		public bool IsSideMenuOpen => m_IsSideMenuOpen;

		public bool IsAnyMenuOpen
		{
			get
			{
				if (!m_IsEsacpeMenuOpen)
				{
					return m_IsSideMenuOpen;
				}
				return true;
			}
		}

		public ProjectMarsBattleUserUI RedUserUI => redUser;

		public ProjectMarsBattleUserUI BlueUserUI => blueUser;

		public PlayerProfileConfiguration StubProfileConfiguration => stubProfileConfiguration;

		public BattleRadialMenu BattleRadialMenu
		{
			get
			{
				return m_RadialMenu;
			}
			set
			{
				m_RadialMenu = value;
			}
		}

		protected override void Awake()
		{
			base.Awake();
			Instance = this;
			SubscribeToGameModeEvents();
			SubscribeToMenuStateChangeEvents();
			m_playerActions = PlayerActions.Instance;
			m_unitDatabase = ContentDatabase.Instance();
			m_inputService = ServiceLocator.GetService<InputService>();
			m_glyphManager = GetComponent<UIGlyphsManager>();
			m_gameStateManager = ServiceLocator.GetService<GameStateManager>();
			m_maxUnits = ServiceLocator.GetService<MaxUnitsController>();
			controllerService = ServiceLocator.GetService<ControllerService>();
			if (m_LegacyUnitSelect != null)
			{
				m_LegacyUnitAnimator = m_LegacyUnitSelect.GetComponent<SimpleStateAnimation>();
			}
			m_IsSandbox = CampaignPlayerDataHolder.CurrentGameModeState == GameModeState.Sandbox;
			m_localMultiplayerGameRules = ServiceLocator.GetService<LocalMultiplayerGameRules>();
			m_localMultiplayerGameRules.OnSettingsChanged += ClearAllUnits;
			if (controllerService != null)
			{
				controllerService.RequestEndMatch += EndBattle;
			}
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			m_inputService.InputChanged += OnInputChange;
			SubscribeToColorSwapEvents();
		}

		protected override void Start()
		{
			base.Start();
			SetUnitSelectionMode();
			m_placementCamera = m_currentGameMode.PlacementCamera;
			m_unitIDs = new List<DatabaseID>();
			m_invClearUnitsTime = 1f / m_clearUnitsTime;
			RedrawFactions();
			RedrawFactionUnits(m_unitDatabase.GetAllFactions().ElementAt(m_lastSelectedFaction));
			SelectFaction();
			SettingsInstance settingsInstance = ServiceLocator.GetService<GlobalSettingsHandler>().GetSettingsInstance("GAMEPLAY_RESTRICT_UNITS");
			if (settingsInstance != null)
			{
				settingsInstance.OnValueChanged += RedrawUI;
			}
			m_cursorController = ServiceLocator.GetService<CursorController>();
			BootstrapBudgets();
			AddOnClickedClearButtonListener(m_currentGameMode.OnClearedTeam);
			if (m_playerActions.InputType == InputType.Controller && m_cursorController != null)
			{
				m_cursorController.CenterCursor();
			}
			if (redUser != null)
			{
				redUser.Close();
				redUser.gameObject.SetActive(value: false);
			}
			if (blueUser != null)
			{
				blueUser.Close();
				blueUser.gameObject.SetActive(value: false);
			}
			m_accountManager = ServiceLocator.GetService<AccountManager>();
			m_socialService = ServiceLocator.GetService<SocialProfileService>();
			if (m_currentGameMode is OnlineMultiplayerGameMode)
			{
				m_networkService = ServiceLocator.GetService<INetworkService>();
				m_networkBattle = ServiceLocator.GetService<NetworkBattleController>();
				if (m_networkBattle != null)
				{
					m_networkBattle.ClearTeamEvent += ClearTeamButtonClicked;
					m_networkBattle.GotRemoteMaxUnits += OnGotRemoteMaxUnits;
					m_networkBattle.PlayerIsReadyChanged += OnPlayerIsReadyChanged;
				}
				if (redUser != null)
				{
					redUser.gameObject.SetActive(value: true);
					redUser.Open();
				}
				if (blueUser != null)
				{
					blueUser.gameObject.SetActive(value: true);
					blueUser.Open();
				}
				HideOpposingTeamUI();
				UpdateStartButtonText();
			}
			if (m_currentGameMode is LocalMultiplayerGameMode)
			{
				if (redUser != null)
				{
					redUser.gameObject.SetActive(value: true);
					redUser.Open();
				}
				if (blueUser != null)
				{
					blueUser.gameObject.SetActive(value: true);
					blueUser.Open();
				}
			}
			m_SettingsProfileManager = ServiceLocator.GetService<SettingsProfileManager>();
			SubscribeToColorSwapEvents();
			UpdateTeamUnitCountText(Team.Red);
			UpdateTeamUnitCountText(Team.Blue);
			if (m_UseGamepadInputUI)
			{
				topButtons.Hide();
			}
		}

		private void ClearAllUnits()
		{
			ClearRed();
			ClearBlue();
		}

		private void HideOpposingTeamUI()
		{
			if (NetworkBattle != null)
			{
				switch (BoltNetwork.IsServer ? NetworkBattle.ServerTeam : NetworkBattle.ClientTeam)
				{
				case Team.Red:
					blueMoney.Hide();
					m_ClearBlueObject.SetActive(value: false);
					break;
				case Team.Blue:
					redMoney.Hide();
					m_ClearRedObject.SetActive(value: false);
					break;
				default:
					throw new ArgumentOutOfRangeException();
				}
			}
		}

		public void ShowClearUIOnlyForPlayer(TFBGames.Player player)
		{
			switch (player)
			{
			case TFBGames.Player.One:
				m_ClearBlueObject.SetActive(value: false);
				m_ClearRedObject.SetActive(value: true);
				break;
			case TFBGames.Player.Two:
				m_ClearRedObject.SetActive(value: false);
				m_ClearBlueObject.SetActive(value: true);
				break;
			case TFBGames.Player.Any:
				m_ClearRedObject.SetActive(value: true);
				m_ClearBlueObject.SetActive(value: true);
				break;
			default:
				throw new ArgumentOutOfRangeException($"{player} is not a valid Player.");
			}
		}

		private void AssignInitialUnitToBrush()
		{
			GameModeService service = ServiceLocator.GetService<GameModeService>();
			bool flag = CampaignPlayerDataHolder.CurrentGameModeState == GameModeState.Campaign;
			if (!(service != null))
			{
				return;
			}
			UnitBlueprint unitBlueprint = null;
			if (flag)
			{
				TABSCampaignLevelAsset currentLevel = CampaignPlayerDataHolder.GetCurrentLevel();
				if (currentLevel.AllowedUnits != null && currentLevel.AllowedUnits.Length != 0)
				{
					for (int i = 0; i < currentLevel.AllowedUnits.Length; i++)
					{
						UnitBlueprint unitBlueprint2 = currentLevel.AllowedUnits[i];
						if (!(unitBlueprint2 == null))
						{
							unitBlueprint = unitBlueprint2;
							break;
						}
					}
				}
				if (unitBlueprint == null)
				{
					Debug.LogWarning("Couldn't get an initial allowed unit from the campaign. Falling back to Clubber.");
					unitBlueprint = m_unitDatabase.GetUserUnitBlueprintByExactName("UNIT_TRIBAL_CLUB");
				}
			}
			else
			{
				if (m_lastSelectedUnit.m_ID != 0 || m_lastSelectedUnit.m_modID != 0)
				{
					unitBlueprint = m_unitDatabase.GetUnitBlueprint(m_lastSelectedUnit);
				}
				if (unitBlueprint == null)
				{
					Debug.LogWarning("Couldn't get an initial unit to select. Falling back to Clubber.");
					unitBlueprint = m_unitDatabase.GetLandfallUnitBlueprint("UNIT_TRIBAL_CLUB");
				}
			}
			if (unitBlueprint != null && unitBlueprint.Entity != null && m_unitPlacementBrush != null)
			{
				m_unitPlacementBrush.SetBrushUnit(unitBlueprint.Entity.GUID);
				m_RadialMenu.SetSelectedUnit(unitBlueprint);
			}
		}

		private void CursorStateChange(PlacementCursorState state)
		{
			if (IsPlacementCursorStateValid(state) && m_currentValidCursorState != state)
			{
				m_currentValidCursorState = state;
				ResetUnitClearProgress(showCircle: true);
			}
		}

		private bool IsPlacementCursorStateValid(PlacementCursorState state)
		{
			if (state != PlacementCursorState.AllowBluePlacement)
			{
				return state == PlacementCursorState.AllowRedPlacement;
			}
			return true;
		}

		private void SetUnitSelectionMode()
		{
			int currentValue = ServiceLocator.GetService<GlobalSettingsHandler>().GetSettingsInstance("UI_INPUT_MODE").currentValue;
			m_UseGamepadInputUI = currentValue == 0;
			if (m_UnitInfoBar != null)
			{
				m_UnitInfoBar.gameObject.SetActive(m_UseGamepadInputUI);
			}
			if (m_LegacyUnitSelect != null)
			{
				m_LegacyUnitSelect.gameObject.SetActive(!m_UseGamepadInputUI);
			}
		}

		protected override void Update()
		{
			base.Update();
			ControllerInput();
			if (m_pointerOverUnits.PointerOver)
			{
				mouseScrollDelta -= Input.mouseScrollDelta.y;
				if (mouseScrollDelta > 1f)
				{
					unitLayoutGroup.MoveList();
					mouseScrollDelta -= 1f;
				}
				else if (mouseScrollDelta < -1f)
				{
					unitLayoutGroup.MoveList(increament: false);
					mouseScrollDelta += 1f;
				}
			}
		}

		public void SetOverrideAnimations(bool animate)
		{
			m_OverrideAnimations = animate;
		}

		protected override void OnClose()
		{
			base.OnClose();
			if (!m_OverrideAnimations)
			{
				HideUI();
			}
			else
			{
				topButtons.Hide();
			}
			m_currentGameMode.PlacementUIComponentStateActive(state: false);
			if (m_WinConditionPresenterAnimator != null)
			{
				m_WinConditionPresenterAnimator.HideConditions();
			}
			ShowSceneDescription(show: false);
			m_unitPlacementBrush.BlockPlacement();
			if (m_RadialMenu != null && m_RadialMenu.IsOpen)
			{
				m_RadialMenu.Close();
			}
			m_OverrideAnimations = false;
		}

		protected override void OnOpen()
		{
			base.OnOpen();
			m_OverrideAnimations = false;
			if (!m_unitPlacementBrush.CameraInFreelook)
			{
				ShowUI();
				CheckTeamColorsSwapped();
			}
			if (m_WinConditionPresenterAnimator != null)
			{
				m_WinConditionPresenterAnimator.ShowConditions();
			}
			ShowSceneDescription(show: true);
			m_currentGameMode.PlacementUIComponentStateActive(state: true);
			m_unitPlacementBrush.UnblockPlacement();
			m_unitPlacementBrush.PreventPlacementBuffer();
		}

		private void OnEnterFreeLook()
		{
			HideUI();
			if (m_WinConditionPresenterAnimator != null)
			{
				m_WinConditionPresenterAnimator.HideConditions();
			}
			ShowSceneDescription(show: false);
		}

		private void OnExitFreeLook()
		{
			ShowUI();
			if (m_WinConditionPresenterAnimator != null)
			{
				m_WinConditionPresenterAnimator.ShowConditions();
			}
			ShowSceneDescription(show: true);
		}

		private void SubscribeToGameModeEvents()
		{
			m_currentGameMode = ServiceLocator.GetService<GameModeService>().CurrentGameMode;
			if (m_currentGameMode != null)
			{
				m_currentGameMode.EnteredFreeLook += OnEnterFreeLook;
				m_currentGameMode.ExitedFreeLook += OnExitFreeLook;
			}
			m_unitPlacementBrush = m_currentGameMode?.Brush;
			if (m_unitPlacementBrush != null)
			{
				m_unitPlacementBrush.PlacementCursorChanged += AssignInitialUnitToBrush;
				m_unitPlacementBrush.CursorStateChange += CursorStateChange;
			}
			else
			{
				Debug.LogError("Unit Placement Brush does not exist.");
			}
		}

		private void UnsubscribeFromGameModeEvents()
		{
			if (m_currentGameMode != null)
			{
				m_currentGameMode.EnteredFreeLook -= OnEnterFreeLook;
				m_currentGameMode.ExitedFreeLook -= OnExitFreeLook;
			}
			if (m_unitPlacementBrush != null)
			{
				m_unitPlacementBrush.PlacementCursorChanged -= AssignInitialUnitToBrush;
				m_unitPlacementBrush.CursorStateChange -= CursorStateChange;
			}
		}

		private void SubscribeToColorSwapEvents()
		{
			if (m_flipColorsSetting == null && !hasRegisteredFlipColors)
			{
				m_flipColorsSetting = ServiceLocator.GetService<GlobalSettingsHandler>().GetSettingsInstance("GAMEPLAY_FLIP_COLORS");
				if (m_flipColorsSetting != null)
				{
					hasRegisteredFlipColors = true;
				}
			}
			if (m_flipColorsSetting != null)
			{
				currentSwapValue = m_flipColorsSetting.currentValue;
				m_flipColorsSetting.OnValueChanged += OnColorsChangedPlacementMode;
			}
		}

		private void UnsubscribeFromColorSwapEvents()
		{
			if (m_flipColorsSetting != null && hasRegisteredFlipColors)
			{
				m_flipColorsSetting.OnValueChanged -= OnColorsChangedPlacementMode;
				hasRegisteredFlipColors = false;
			}
		}

		private void SubscribeToMenuStateChangeEvents()
		{
			m_escapeMenuComponent.Enabled += OnEscapeMenuEnabled;
			m_escapeMenuComponent.Disabled += OnEscapeMenuDisabled;
			if (m_additionalSettingsSidePanelComponent != null)
			{
				m_additionalSettingsSidePanelComponent.Enabled += OnSideMenuEnabled;
				m_additionalSettingsSidePanelComponent.Disabled += OnSideMenuDisabled;
			}
			if (openExpandedFactionUIButton != null)
			{
				openExpandedFactionUIButton.onClick.AddListener(OnOpenExpandedFactionUIButton);
			}
		}

		private void UnsubscribeFromMenuStateChangeEvents()
		{
			if (m_escapeMenuComponent != null)
			{
				m_escapeMenuComponent.Enabled -= OnEscapeMenuEnabled;
				m_escapeMenuComponent.Disabled -= OnEscapeMenuDisabled;
			}
			if (m_additionalSettingsSidePanelComponent != null)
			{
				m_additionalSettingsSidePanelComponent.Enabled -= OnSideMenuEnabled;
				m_additionalSettingsSidePanelComponent.Disabled -= OnSideMenuDisabled;
			}
			if (openExpandedFactionUIButton != null)
			{
				openExpandedFactionUIButton.onClick.RemoveListener(OnOpenExpandedFactionUIButton);
			}
		}

		private void OnOpenExpandedFactionUIButton()
		{
			if (!(expandedFactionUI == null) && !expandedFactionUI.BlockExpandedFactionUIOpen())
			{
				stateManager.OpenUIComponent(expandedFactionUI);
			}
		}

		private void CheckTeamColorsSwapped()
		{
			if (shouldResetLayout)
			{
				Debug.Log("Swapping Team Layouts");
				m_gameStateManager.EnterNoneState();
				m_gameStateManager.EnterPlacementState();
				shouldResetLayout = false;
			}
			UpdateTeamUnitCountText(Team.Red);
			UpdateTeamUnitCountText(Team.Blue);
		}

		private void OnColorsChangedPlacementMode(int swapValue)
		{
			if (swapValue != currentSwapValue)
			{
				currentSwapValue = swapValue;
				if (!shouldResetLayout)
				{
					shouldResetLayout = true;
				}
				Debug.LogFormat("Should Reset: {0}", shouldResetLayout);
			}
		}

		public void OnUnitCountChanged(Team team)
		{
			UpdateTeamUnitCountText(team);
		}

		private void UpdateTeamUnitCountText(Team team)
		{
			int teamUnitCount = m_maxUnits.GetTeamUnitCount(team);
			int? maxCount = ((m_maxUnits != null) ? m_maxUnits.GetMaxUnits() : ((int?)null));
			SetCountText(team, teamUnitCount, maxCount);
		}

		private void BootstrapBudgets()
		{
			BattleBudget battleBudget = m_currentGameMode.BattleBudget;
			int budget = battleBudget.GetBudget(Team.Red);
			int budget2 = battleBudget.GetBudget(Team.Blue);
			SetBudgetText(Team.Red, budget);
			SetBudgetText(Team.Blue, budget2);
			m_currentGameMode.BattleBudget.RegisterOnBudgetChangedListener(UpdateTeamBudgetUI);
		}

		private void UpdateTeamBudgetUI(Team team, int newBudget)
		{
			SetBudgetText(team, newBudget);
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			Instance = null;
			if (ServiceLocator.GetService<GameModeService>() != null)
			{
				RemoveOnClickedClearButtonListener(m_currentGameMode.OnClearedTeam);
			}
			SettingsInstance settingsInstance = ServiceLocator.GetService<GlobalSettingsHandler>()?.GetSettingsInstance("GAMEPLAY_RESTRICT_UNITS");
			if (settingsInstance != null)
			{
				settingsInstance.OnValueChanged -= RedrawUI;
			}
			if (NetworkBattle != null)
			{
				NetworkBattle.ClearTeamEvent -= ClearTeamButtonClicked;
				NetworkBattle.GotRemoteMaxUnits -= OnGotRemoteMaxUnits;
				NetworkBattle.PlayerIsReadyChanged -= OnPlayerIsReadyChanged;
			}
			UnsubscribeFromGameModeEvents();
			UnsubscribeFromMenuStateChangeEvents();
			m_localMultiplayerGameRules.OnSettingsChanged -= ClearAllUnits;
			if (controllerService != null)
			{
				controllerService.RequestEndMatch -= EndBattle;
			}
		}

		private void RedrawUI(int value)
		{
			RedrawUI();
		}

		public void RedrawUI(string unlockKey)
		{
			if (!string.IsNullOrWhiteSpace(unlockKey) && m_selectedFaction != null)
			{
				UnitBlueprint userUnitBlueprintByUnlockKey = m_unitDatabase.GetUserUnitBlueprintByUnlockKey(unlockKey);
				if (userUnitBlueprintByUnlockKey != null)
				{
					m_selectedFaction = m_unitDatabase.GetFactionByUnitBlueprint(userUnitBlueprintByUnlockKey.Entity.GUID);
				}
			}
			RedrawUI();
			if (m_RadialMenu != null)
			{
				m_RadialMenu.UpdateAvailableSandboxUnits();
			}
		}

		private void RedrawUI()
		{
			if (m_selectedFaction != null)
			{
				RedrawFactions();
				RedrawFactionUnits(m_selectedFaction);
			}
		}

		public void EnterBattleState()
		{
			ServiceLocator.GetService<MusicHandler>().PlaySongBattle(TABSSceneManager.CurrentLoadedMap);
			HideUI();
			ResetUnitClearProgress(showCircle: false);
			ShowSceneDescription(show: false);
		}

		public void EnterPlacementState()
		{
			if (CampaignPlayerDataHolder.CurrentGameModeState == GameModeState.Campaign)
			{
				m_ClearBlueObject.SetActive(value: false);
				blueMoney.gameObject.SetActive(value: false);
			}
			ShowUI();
			ShowSceneDescription(show: true);
			if (m_playerActions.InputType == InputType.Controller && m_cursorController != null)
			{
				m_cursorController.CenterCursor();
			}
			ServiceLocator.GetService<MusicHandler>().PlaySongPlacement(TABSSceneManager.CurrentLoadedMap);
			m_RadialMenu.ClearLastCachedFaction();
		}

		private bool CanStartBattle(bool checkUnits = true)
		{
			bool num = m_inputService.CurrentState == InputService.InputState.UI;
			bool flag = SideHasUnits(Team.Red) && SideHasUnits(Team.Blue);
			bool flag2 = !num && !UIScreenInputBlocker.BlockInput && !m_currentGameMode.EnterPlacementStateSequencer.IsRunning;
			if (checkUnits)
			{
				return flag && flag2;
			}
			return flag2;
		}

		private bool SideHasUnits(Team team)
		{
			return m_currentGameMode.TeamLayouts.TeamLayoutHasUnits(team);
		}

		public void StartBattle()
		{
			switch (CampaignPlayerDataHolder.CurrentGameModeState)
			{
			case GameModeState.Sandbox:
			case GameModeState.Campaign:
			case GameModeState.LocalMultiplayer:
				if (ServiceLocator.GetService<GameModeService>().CurrentGameMode is LocalMultiplayerGameMode localMultiplayerGameMode && !localMultiplayerGameMode.CanStartBattle())
				{
					if (localMultiplayerGameMode.TeamLayouts.TeamLayoutHasUnits((localMultiplayerGameMode.CurrentPlayer != TFBGames.Player.One) ? Team.Blue : Team.Red))
					{
						localMultiplayerGameMode.ChangePhase();
					}
				}
				else if (CanStartBattle())
				{
					m_playerActions.m_enterExitBattle.ClearInputState();
					m_gameStateManager.EnterBattleState();
					ShowClearUIOnlyForPlayer(TFBGames.Player.Any);
				}
				break;
			case GameModeState.OnlineMultiplayer:
				if (!CanStartBattle(checkUnits: false))
				{
					break;
				}
				Debug.Log("Clicked Start.");
				if (!(NetworkBattle == null))
				{
					Team playerTeam = m_networkService.PlayerTeam;
					if (NetworkBattle.IsPlayerReady(playerTeam))
					{
						NetworkBattle.PlayerIsNotReady(playerTeam);
					}
					else if (SideHasUnits(playerTeam))
					{
						NetworkBattle.PlayerIsReady(playerTeam);
					}
					Debug.Log($"Clicked Start. Is Server: {BoltNetwork.IsServer}. Team {playerTeam.ToString()}");
				}
				break;
			}
		}

		public void EndBattle()
		{
			switch (CampaignPlayerDataHolder.CurrentGameModeState)
			{
			case GameModeState.Campaign:
				if (m_gameStateManager.GameState == Landfall.TABS.GameState.GameState.BattleState && !m_currentGameMode.MatchOver)
				{
					m_currentGameMode.ClearStreakInCampaign();
				}
				m_playerActions.m_enterExitBattle.ClearInputState();
				m_gameStateManager.EnterPlacementState();
				break;
			case GameModeState.Sandbox:
			case GameModeState.LocalMultiplayer:
				m_playerActions.m_enterExitBattle.ClearInputState();
				m_gameStateManager.EnterPlacementState();
				break;
			case GameModeState.OnlineMultiplayer:
				if (m_currentGameMode is OnlineMultiplayerGameMode onlineMultiplayerGameMode)
				{
					onlineMultiplayerGameMode.RequestEndBattle();
				}
				startButtonLabel.text = "READY";
				break;
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		private void ShowUI()
		{
			if (!base.IsActive || m_gameStateManager.GameState == Landfall.TABS.GameState.GameState.BattleState)
			{
				return;
			}
			bool flag = CampaignPlayerDataHolder.CurrentGameModeState == GameModeState.Campaign;
			if (topButtons != null && !m_UseGamepadInputUI)
			{
				topButtons.Show();
			}
			if (redMoney != null)
			{
				redMoney.Show();
			}
			if (blueMoney != null)
			{
				if (flag)
				{
					blueMoney.Hide();
					m_ClearBlueObject.SetActive(value: false);
				}
				else
				{
					blueMoney.Show();
					m_ClearBlueObject.SetActive(value: true);
				}
			}
			if (m_UnitInfoBar != null)
			{
				m_UnitInfoBar.Show();
			}
			if (m_LegacyUnitAnimator != null)
			{
				m_LegacyUnitAnimator.SetState(SimpleStateAnimation.State.State01);
			}
			if (m_currentGameMode is LocalMultiplayerGameMode localMultiplayerGameMode)
			{
				ShowClearUIOnlyForPlayer(localMultiplayerGameMode.CurrentPlayer);
			}
			HideOpposingTeamUI();
			if (redUser != null)
			{
				redUser.Open();
			}
			if (blueUser != null)
			{
				blueUser.Open();
			}
			ShowControllerGlyphs();
		}

		private void HideUI()
		{
			if (topButtons != null)
			{
				topButtons.Hide();
			}
			if (redMoney != null)
			{
				redMoney.Hide();
			}
			if (blueMoney != null)
			{
				blueMoney.Hide();
			}
			if (m_UnitInfoBar != null)
			{
				m_UnitInfoBar.Hide();
			}
			if (m_RadialMenu != null)
			{
				m_RadialMenu.Close();
			}
			if (m_LegacyUnitAnimator != null)
			{
				m_LegacyUnitAnimator.SetState(SimpleStateAnimation.State.State02);
			}
			if (redUser != null)
			{
				redUser.Close();
			}
			if (blueUser != null)
			{
				blueUser.Close();
			}
			HideControllerGlyphs();
		}

		private void ShowSceneDescription(bool show)
		{
			if (!(m_sceneDescription == null))
			{
				if (show)
				{
					m_sceneDescription.Show();
				}
				else
				{
					m_sceneDescription.Hide();
				}
			}
		}

		private void ShowControllerGlyphs()
		{
			if (m_GlyphElements != null && m_GlyphElements.Length != 0)
			{
				MovingUIElement[] glyphElements = m_GlyphElements;
				for (int i = 0; i < glyphElements.Length; i++)
				{
					glyphElements[i].Show();
				}
			}
		}

		private void HideControllerGlyphs()
		{
			if (m_GlyphElements != null && m_GlyphElements.Length != 0)
			{
				MovingUIElement[] glyphElements = m_GlyphElements;
				for (int i = 0; i < glyphElements.Length; i++)
				{
					glyphElements[i].Hide();
				}
			}
		}

		public void ClearRed()
		{
			ClearTeam(Team.Red);
		}

		public void ClearBlue()
		{
			ClearTeam(Team.Blue);
		}

		public void AddOnClickedClearButtonListener(ClearButtonDelegate callback)
		{
			m_onClickedClearButton = (ClearButtonDelegate)Delegate.Combine(m_onClickedClearButton, callback);
		}

		public void RemoveOnClickedClearButtonListener(ClearButtonDelegate callback)
		{
			m_onClickedClearButton = (ClearButtonDelegate)Delegate.Remove(m_onClickedClearButton, callback);
		}

		public bool SelectUnit(DatabaseID id, UnitButton button)
		{
			if ((bool)m_selectedButton)
			{
				if (button == m_selectedButton)
				{
					return false;
				}
				m_selectedButton.Deselect();
				m_selectedButton.OnExit();
			}
			button.overrideSelection = true;
			m_currentGameMode.Brush.SetBrushUnit(id);
			m_selectedButton = button;
			m_lastSelectedUnit = id;
			m_lastSelectedUnitIndex = m_unitIDs.IndexOf(id);
			return true;
		}

		public void SetMinMax(RectTransform[] factionButtons)
		{
			BakeFactionMinMax(factionButtons);
		}

		public void BakeFactionMinMax(RectTransform[] factionButtons)
		{
			int num = -1;
			int num2 = -1;
			float num3 = 9999999f;
			float num4 = -9999999f;
			for (int i = 0; i < factionButtons.Length; i++)
			{
				if (factionButtons[i].position.x > num4)
				{
					num2 = i;
					num4 = factionButtons[i].position.x;
				}
				if (factionButtons[i].position.x < num3)
				{
					num = i;
					num3 = factionButtons[i].position.x;
				}
			}
			FactionXMin = num3 + factionButtons[num].rect.xMin;
			FactionXMax = num4 + factionButtons[num2].rect.xMax;
		}

		public bool SelectFaction()
		{
			StartCoroutine(SelectFactionFirst());
			return true;
		}

		private IEnumerator SelectFactionFirst()
		{
			yield return null;
		}

		private void SetFirstUnit()
		{
			if (m_unitIDs.Count > 0)
			{
				ISaveLoaderService service = ServiceLocator.GetService<ISaveLoaderService>();
				DatabaseID databaseID = m_unitIDs[0];
				UnitBlueprint unitBlueprint = m_unitDatabase.GetUnitBlueprint(databaseID);
				if (!unitBlueprint.IsSecret || service.HasUnlockedSecret(unitBlueprint.Entity.UnlockKey))
				{
					m_lastSelectedUnit = databaseID;
				}
				else
				{
					int i = 1;
					for (int count = m_unitIDs.Count; i < count; i++)
					{
						databaseID = m_unitIDs[i];
						unitBlueprint = m_unitDatabase.GetUnitBlueprint(databaseID);
						if (unitBlueprint.IsSecret && service.HasUnlockedSecret(unitBlueprint.Entity.UnlockKey))
						{
							m_lastSelectedUnit = databaseID;
							break;
						}
					}
				}
			}
			m_lastSelectedUnitIndex = 0;
		}

		private void SelectFirstUnitButton()
		{
			UnitButton unitButton = unitLayoutGroup.GetUnitButton(m_lastSelectedUnit);
			if ((bool)unitButton)
			{
				unitButton.OnEnter();
				unitButton.SelectUnit();
				unitButton.OnExit();
			}
		}

		public bool SelectFaction(DatabaseID id, FactionButton button)
		{
			if ((bool)selectedFactionButton)
			{
				if (button == selectedFactionButton)
				{
					return false;
				}
				selectedFactionButton.Deselect();
			}
			button.overrideSelection = true;
			m_selectedFaction = m_unitDatabase.GetFaction(id);
			RedrawFactionUnits(m_selectedFaction);
			SetFirstUnit();
			selectedFactionButton = button;
			m_lastSelectedFaction = button.index;
			SelectFaction();
			return true;
		}

		public bool WasLastSelectedFactionIndex(DatabaseID index)
		{
			return false;
		}

		private void ControllerInput()
		{
			if (m_inputService.CurrentState == InputService.InputState.Gameplay && base.IsActive)
			{
				PlacementModeControllerInput();
			}
		}

		private void PlacementModeControllerInput()
		{
			HandleUnitClearing();
			if (m_RadialMenu != null && m_UseGamepadInputUI)
			{
				RadialMenuUnitSelection();
			}
			else
			{
				if (m_LegacyUnitAnimator.m_state == SimpleStateAnimation.State.State01)
				{
					UnitSelectionWithController();
				}
				m_unitPlacementBrush.ShouldUpdate(update: true);
				m_cursorController.AllowCursorMovement(allow: true);
			}
			GameModeState currentGameModeState = CampaignPlayerDataHolder.CurrentGameModeState;
			bool flag = (m_IsSandbox || currentGameModeState == GameModeState.LocalMultiplayer || currentGameModeState == GameModeState.OnlineMultiplayer) && !m_unitPlacementBrush.CameraInFreelook && m_gameStateManager.GameState != Landfall.TABS.GameState.GameState.BattleState;
			if (NetworkBattle != null)
			{
				flag &= NetworkBattle.AreBothPlayersInBattleScene;
			}
			if (SpawnLevel.IsCustomLevelTestRun)
			{
				flag = false;
			}
			if (m_playerActions.m_mapSettings.WasPressed && flag && m_OpenAdditonalSidePanelComponent != null)
			{
				m_OverrideAnimations = true;
				m_OpenAdditonalSidePanelComponent.onClick.Invoke();
			}
			if (m_playerActions.m_openFactionSelection.WasPressed && openExpandedFactionUIButton.gameObject.activeSelf)
			{
				OnOpenExpandedFactionUIButton();
			}
			if (m_playerActions.m_mapSelect.WasPressed && m_gameStateManager.GameState == Landfall.TABS.GameState.GameState.PlacementState)
			{
				showMapSelect.onClick.Invoke();
			}
			if (m_playerActions.m_placementShowInput.WasPressed && m_gameStateManager.GameState == Landfall.TABS.GameState.GameState.PlacementState)
			{
				showInputViewer.onClick.Invoke();
			}
			if (m_playerActions.m_centerCursor.WasPressed)
			{
				m_cursorController.CenterCursor();
			}
			if (m_playerActions.m_centerCameraPositionAndZoom.WasPressed && !m_placementCamera.IsInFreeLook())
			{
				m_placementCamera.CenterCameraPositionAndZoom();
			}
		}

		public void RadialMenuUnitSelection()
		{
			m_unitPlacementBrush.ShouldUpdate(!m_RadialMenu.IsOpen);
			m_cursorController.AllowCursorMovement(!m_RadialMenu.IsOpen);
			if (!CheckPlacementCamera() || !base.IsActive || m_placementCamera.IsInFreeLook() || m_gameStateManager.GameState != Landfall.TABS.GameState.GameState.PlacementState)
			{
				return;
			}
			if (m_playerActions.m_openUnitRadialMenu.WasPressed)
			{
				if (m_RadialMenu.IsOpen)
				{
					CloseRadialMenu();
				}
				else
				{
					OpenRadialMenu();
					m_RadialTimer = 0f;
				}
			}
			if (m_playerActions.m_openUnitRadialMenu.IsPressed)
			{
				m_RadialTimer += Time.unscaledDeltaTime;
			}
			if (m_playerActions.m_openUnitRadialMenu.WasReleased && m_RadialTimer > m_radialInputHoldThreshold)
			{
				CloseRadialMenu();
			}
			if (m_playerActions.m_resetRadialMenuEditing.WasPressed)
			{
				m_RadialMenu.ResetEditedFactions();
			}
			if (m_playerActions.m_editRadialMenuFaction.WasPressed)
			{
				m_RadialMenu.EditRadialMenuPressed();
			}
			if (m_RadialMenu.IsOpen)
			{
				if (m_playerActions.m_cycleTabs.WasPressed && m_RadialMenu.IsSelectingUnits)
				{
					int num = Mathf.RoundToInt(m_playerActions.m_cycleTabs.Value);
					m_lastSelectedFaction = (m_lastSelectedFaction + num) % m_numberOfFactions;
					m_lastSelectedFaction = ((m_lastSelectedFaction < 0) ? (m_numberOfFactions - 1) : m_lastSelectedFaction);
				}
				if (m_playerActions.m_back.WasPressed)
				{
					m_RadialMenu.BackPressedFromPlacementUI();
				}
			}
		}

		private bool CheckPlacementCamera()
		{
			if (m_placementCamera == null)
			{
				m_placementCamera = m_currentGameMode.PlacementCamera;
			}
			return m_placementCamera != null;
		}

		private void OpenRadialMenu()
		{
			if (!m_RadialMenu.IsOpen)
			{
				Camera cam = Landfall.TABC.MainCam.instance.cam;
				Vector2 position = new Vector2(0f, 0f);
				if (cam != null)
				{
					position = m_unitPlacementBrush.LastCursorCanvasPosition;
				}
				m_RadialMenu.OpenAtPosition(position, keepOnScreen: true);
			}
		}

		private void CloseRadialMenu()
		{
			if (m_RadialMenu.IsOpen)
			{
				m_RadialMenu.Close();
			}
		}

		private void OnEscapeMenuEnabled()
		{
			m_IsEsacpeMenuOpen = true;
			OnMenuEnabled();
		}

		private void OnSideMenuEnabled()
		{
			m_IsSideMenuOpen = true;
			OnMenuEnabled();
		}

		private void OnMenuEnabled()
		{
			ResetUnitClearProgress(showCircle: false);
		}

		private void OnEscapeMenuDisabled()
		{
			m_IsEsacpeMenuOpen = false;
			OnMenuDisabled();
		}

		private void OnSideMenuDisabled()
		{
			m_IsSideMenuOpen = false;
			OnMenuDisabled();
		}

		private void OnMenuDisabled()
		{
			if (m_unitPlacementBrush != null && m_unitPlacementBrush.Cursor != null && m_playerActions != null && !m_playerActions.m_move.IsPressed)
			{
				m_unitPlacementBrush.Cursor.ShowFillCircle(m_playerActions.m_clearUnits.IsPressed);
			}
		}

		private void ResetUnitClearProgress(bool showCircle)
		{
			m_clearUnitsTimer = 0f;
			if (!(m_unitPlacementBrush.Cursor == null))
			{
				m_unitPlacementBrush.Cursor.SetCircleFillAmount(0f);
				if (m_playerActions.m_clearUnits.IsPressed)
				{
					m_unitPlacementBrush.Cursor.ShowFillCircle(showCircle);
				}
			}
		}

		private void HandleUnitClearing()
		{
			if (m_gameStateManager.GameState != Landfall.TABS.GameState.GameState.PlacementState || m_unitPlacementBrush.CameraInFreelook)
			{
				return;
			}
			if (!m_playerActions.m_move.IsPressed && m_playerActions.m_clearUnits.IsPressed)
			{
				m_clearUnitsDelayCounter += Time.unscaledDeltaTime;
				Team teamAtCursorPosition = m_currentGameMode.Brush.GetTeamAtCursorPosition();
				if (m_clearUnitsDelayCounter >= m_clearUnitsDelay)
				{
					m_unitPlacementBrush.Cursor.ShowFillCircle(show: true);
					if (m_clearUnitsTimer >= m_clearUnitsTime)
					{
						switch (CampaignPlayerDataHolder.CurrentGameModeState)
						{
						case GameModeState.Sandbox:
							switch (teamAtCursorPosition)
							{
							case Team.Blue:
								ClearBlue();
								break;
							case Team.Red:
								ClearRed();
								break;
							}
							break;
						case GameModeState.Campaign:
							ClearRed();
							break;
						case GameModeState.LocalMultiplayer:
						{
							Team team = ((LocalMultiplayerGameMode)m_currentGameMode).CurrentPlayerTeam();
							if (teamAtCursorPosition == Team.Blue && team == Team.Blue)
							{
								ClearBlue();
							}
							else if (teamAtCursorPosition == Team.Red && team == Team.Red)
							{
								ClearRed();
							}
							break;
						}
						case GameModeState.OnlineMultiplayer:
							if (!(NetworkBattle == null) && m_unitPlacementBrush.Cursor.IsFillCircleVisible && MultiplayerHelper.IsCorrectTeamToEdit(teamAtCursorPosition, forRemotePlayer: false, NetworkBattle.ServerTeam, NetworkBattle.ClientTeam))
							{
								switch (teamAtCursorPosition)
								{
								case Team.Blue:
									ClearBlue();
									break;
								case Team.Red:
									ClearRed();
									break;
								}
							}
							break;
						}
						m_unitPlacementBrush.Cursor.ShowFillCircle(show: false);
					}
					else
					{
						m_clearUnitsTimer += Time.unscaledDeltaTime;
						m_unitPlacementBrush.Cursor.SetCircleFillAmount(m_clearUnitsTimer * m_invClearUnitsTime);
					}
				}
			}
			if (!m_playerActions.m_clearUnits.IsPressed)
			{
				m_clearUnitsDelayCounter = 0f;
			}
			if (m_playerActions.m_clearUnits.WasReleased)
			{
				m_unitPlacementBrush.Cursor.ShowFillCircle(show: false);
				ResetUnitClearProgress(showCircle: true);
			}
		}

		public DatabaseID? UnitSelectionWithController()
		{
			UnitButton unitButton = unitLayoutGroup.GetUnitButton(m_lastSelectedUnit);
			if (m_playerActions.m_cycleUnits.WasPressed && !m_playerActions.m_placementZoomActivate.IsPressed && m_gameStateManager.GameState == Landfall.TABS.GameState.GameState.PlacementState)
			{
				if (unitButton != null)
				{
					unitButton.Deselect();
				}
				int num = Math.Sign(m_playerActions.m_cycleUnits.Value);
				m_lastSelectedUnitIndex = (m_lastSelectedUnitIndex + num) % m_unitIDs.Count;
				m_lastSelectedUnitIndex = ((m_lastSelectedUnitIndex < 0) ? (m_unitIDs.Count - 1) : m_lastSelectedUnitIndex);
				m_lastSelectedUnit = m_unitIDs[m_lastSelectedUnitIndex];
				unitButton = unitLayoutGroup.GetUnitButton(m_lastSelectedUnit);
				if (unitButton == null)
				{
					if (num > 0)
					{
						int numMoveSpots = ((m_lastSelectedUnitIndex != 0) ? 1 : m_unitIDs.Count);
						bool increment = m_lastSelectedUnitIndex != 0;
						MoveUnitList(increment, numMoveSpots);
					}
					else
					{
						int num2 = m_unitIDs.Count - 1;
						int numMoveSpots2 = ((m_lastSelectedUnitIndex != num2) ? 1 : m_unitIDs.Count);
						bool increment2 = m_lastSelectedUnitIndex == num2;
						MoveUnitList(increment2, numMoveSpots2);
					}
					unitButton = unitLayoutGroup.GetUnitButton(m_lastSelectedUnit);
				}
				if (unitButton != null)
				{
					StartCoroutine(Delay(unitButton));
				}
			}
			if (unitButton != null)
			{
				return unitButton.UnitID;
			}
			return null;
		}

		private IEnumerator Delay(UnitButton button)
		{
			yield return null;
			if (button.Unlocked)
			{
				button.ShowSelection();
				button.SelectUnit();
			}
		}

		private void MoveUnitList(bool increment, int numMoveSpots)
		{
			for (int i = 0; i < numMoveSpots; i++)
			{
				unitLayoutGroup.MoveList(increment);
			}
		}

		public void RedrawFactions()
		{
			factionBar.Clear();
			factionBar.Spawn();
			if (m_unitDatabase == null)
			{
				return;
			}
			IEnumerable<Faction> allFactions = m_unitDatabase.GetAllFactions();
			if (allFactions == null || allFactions.Count() <= 0)
			{
				return;
			}
			foreach (Faction allFaction in m_unitDatabase.GetAllFactions())
			{
				if (allFaction.m_displayFaction)
				{
					m_numberOfFactions++;
				}
			}
		}

		public void RedrawFactionUnits(Faction faction)
		{
			if (faction == null || faction.Units == null)
			{
				return;
			}
			m_unitIDs.Clear();
			unitLayoutGroup.ClearElements();
			m_ComingSoon.SetActive(value: false);
			UnitBlueprint[] rearrangedUnits = faction.GetRearrangedUnits();
			foreach (UnitBlueprint unitBlueprint in rearrangedUnits)
			{
				if (!(unitBlueprint == null) && (CampaignPlayerDataHolder.CurrentGameModeState != GameModeState.OnlineMultiplayer || !unitBlueprint.ExcludeFromOnlineMultiplayer))
				{
					bool flag = ServiceLocator.GetService<ISaveLoaderService>().HasUnlockedSecret(unitBlueprint.Entity.UnlockKey);
					if (flag || faction.LockSecrets)
					{
						UnitBlueprint unitBlueprint2 = unitBlueprint;
						m_unitIDs.Add(unitBlueprint2.Entity.GUID);
						DrawFactionUnit(unitBlueprint2, flag);
					}
				}
			}
		}

		public void DrawFactionUnit(UnitBlueprint unit, bool secretUnlocked)
		{
			bool flag = true;
			flag = ((CampaignPlayerDataHolder.CurrentGameModeState != GameModeState.Campaign) ? secretUnlocked : (CampaignPlayerDataHolder.GetCurrentLevel().IsAllowed(unit) && secretUnlocked));
			unitLayoutGroup.AddElement(new UnitButton.UnitButtonData(unit, this, flag, !secretUnlocked));
		}

		public void SetBlueBudgetText(int budget)
		{
			blueMoney.SetMoney(budget);
		}

		public void SetRedBudgetText(int budget)
		{
			redMoney.SetMoney(budget);
		}

		public void SetBudgetText(Team team, int budget)
		{
			switch (team)
			{
			case Team.Red:
				SetRedBudgetText(budget);
				break;
			case Team.Blue:
				SetBlueBudgetText(budget);
				break;
			}
		}

		public void SetBlueCountText(int count, int? maxCount)
		{
			blueMoney.SetCount(count, maxCount);
		}

		public void SetRedBCountText(int count, int? maxCount)
		{
			redMoney.SetCount(count, maxCount);
		}

		public void SetCountText(Team team, int count, int? maxCount)
		{
			switch (team)
			{
			case Team.Red:
				SetRedBCountText(count, maxCount);
				break;
			case Team.Blue:
				SetBlueCountText(count, maxCount);
				break;
			}
		}

		public void SetRadialMenuScale(float scaleFactor)
		{
			if (m_RadialMenu != null)
			{
				m_RadialMenu.SetPlacerScaleFactor(scaleFactor);
			}
		}

		private void OnInputChange(InputType inputType)
		{
			if (m_gameStateManager.GameState != Landfall.TABS.GameState.GameState.PlacementState)
			{
				return;
			}
			switch (inputType)
			{
			case InputType.Controller:
				SelectFaction();
				break;
			case InputType.Keyboard:
			case InputType.Any:
				if (!ServiceLocator.GetService<GameModeService>().CurrentGameMode.IsInFreeLook)
				{
					if (base.IsActive && !m_UseGamepadInputUI)
					{
						topButtons.Show();
					}
					m_unitPlacementBrush.Cursor.SetCircleFillAmount(0f);
					m_clearUnitsTimer = 0f;
					if (m_RadialMenu != null && m_RadialMenu.IsOpen)
					{
						m_RadialMenu.Close();
					}
				}
				break;
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		private void ClearTeam(Team team)
		{
			if (UIScreenInputBlocker.BlockInput)
			{
				Debug.Log("Screen Input Is Blocked cause of open menues!");
			}
			else if (CampaignPlayerDataHolder.CurrentGameModeState == GameModeState.OnlineMultiplayer && NetworkBattle != null && m_networkService != null)
			{
				if (team != m_networkService.PlayerTeam || !NetworkBattle.IsPlayerReady(team))
				{
					Team serverTeam = NetworkBattle.ServerTeam;
					Team clientTeam = NetworkBattle.ClientTeam;
					if (MultiplayerHelper.IsCorrectTeamToEdit(team, forRemotePlayer: false, serverTeam, clientTeam))
					{
						NetworkBattle.ClearAllUnitsForTeam(team);
					}
				}
			}
			else
			{
				ClearTeamButtonClicked(team);
			}
		}

		private void ClearTeamButtonClicked(Team team)
		{
			m_onClickedClearButton?.Invoke(team);
		}

		private void OnGotRemoteMaxUnits(int? oldValue, int? newValue)
		{
			UpdateTeamUnitCountText(Team.Red);
			UpdateTeamUnitCountText(Team.Blue);
		}

		public void SetMartians(PlayerProfile redPlayer, PlayerProfile bluePlayer)
		{
			if (redUser != null)
			{
				redUser.SetPlayerProfile(redPlayer);
			}
			if (blueUser != null)
			{
				blueUser.SetPlayerProfile(bluePlayer);
			}
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			m_inputService.InputChanged -= OnInputChange;
			UnsubscribeFromColorSwapEvents();
		}

		public void ToggleExpandFaction()
		{
			expandedFactionUI.ToggleUI();
		}

		private void OnPlayerIsReadyChanged(Team team, bool oldIsReady, bool newIsReady)
		{
			if (team == m_networkService.PlayerTeam)
			{
				UpdateStartButtonText();
			}
		}

		private void UpdateStartButtonText()
		{
			if (m_networkService == null)
			{
				m_networkService = ServiceLocator.GetService<NetworkService>();
			}
			if (m_networkBattle == null)
			{
				m_networkBattle = ServiceLocator.GetService<NetworkBattleController>();
				if (m_networkBattle == null)
				{
					return;
				}
			}
			Team playerTeam = m_networkService.PlayerTeam;
			bool flag = NetworkBattle.IsPlayerReady(playerTeam);
			startButtonLabel.text = (flag ? "STOP" : "READY");
		}
	}
}
