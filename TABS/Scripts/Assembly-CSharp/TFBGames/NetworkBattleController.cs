using System;
using DM;
using Landfall.TABS;
using Landfall.TABS.GameMode;
using Landfall.TABS.GameState;
using Landfall.TABS.Services;
using Landfall.TABS_Input;
using Photon.Bolt;
using Photon.Bolt.Matchmaking;
using Photon.Bolt.Utils;
using UdpKit.Platform.Photon;
using UnityEngine;

namespace TFBGames
{
	public class NetworkBattleController : GlobalEventListener, IService
	{
		public delegate void GotRemoteMaxUnitsEventHandler(int? oldValue, int? newValue);

		public delegate void PhaseChangedEventHandler(NetworkGamePhase oldPhase, NetworkGamePhase newPhase);

		public delegate void PlayerIsReadyChangedEventHandler(Team team, bool oldIsReady, bool newIsReady);

		[HideInInspector]
		public bool blindMode;

		[HideInInspector]
		public int maxBudget;

		private PlayerActions m_playerActions;

		private GameStateManager m_gameStateManager;

		private ContentDatabase m_database;

		private ModalPanel m_modalPanel;

		private BaseGameMode m_gameMode;

		private INetworkService m_networkService;

		private INetworkQuitController m_quitController;

		private GooglyEyes m_googlyEyesManager;

		private bool m_blueIsReady;

		private bool m_redIsReady;

		private bool m_battleStarted;

		private NetworkPhaseCourier m_phaseCourier;

		private SettingsProfileManager m_settingsProfileManager;

		private ProjectMarsGameSettingsAsset m_projectMarsGameSettings;

		private MultiplayerSettingsMenu m_settingsMenu;

		private int m_maxUnitsAllowed;

		private ITimeService m_timeService;

		private bool m_didSetAllowZeroTimeScaleForMultiplayer;

		private bool m_checkPlayersQuitToCloseMapChangePopup;

		private const string popUpRuleDecline = "MP_POPUP_REJECTED_PROPOSAL";

		private const string popUpRuleAccept = "MP_POPUP_ACCEPTED_PROPOSAL";

		public Team ServerTeam => Team.Red;

		public Team ClientTeam => Team.Blue;

		public NetworkGamePhase Phase
		{
			get
			{
				if (!(m_phaseCourier != null))
				{
					return NetworkGamePhase.Initializing;
				}
				return m_phaseCourier.Phase;
			}
		}

		public NetworkGamePhase RemotePhase
		{
			get
			{
				if (!(m_phaseCourier != null))
				{
					return NetworkGamePhase.Initializing;
				}
				return m_phaseCourier.RemotePhase;
			}
		}

		public bool DidGetRemoteMaxUnits { get; private set; }

		public int? RemoteMaxUnits { get; private set; }

		public bool AreBothPlayersInBattleScene { get; private set; }

		public int WaitingForOtherPlayerToAcceptMapOpenId { get; set; }

		public event Action<Team> ClientGotEndBattleMessage;

		public event Action<Team> ClearTeamEvent;

		public event GotRemoteMaxUnitsEventHandler GotRemoteMaxUnits;

		public event PhaseChangedEventHandler PhaseChanged;

		public event PhaseChangedEventHandler RemotePhaseChanged;

		public event PlayerIsReadyChangedEventHandler PlayerIsReadyChanged;

		public event Action BothPlayersEnteredBattleScene;

		private void Awake()
		{
			ServiceLocator.RegisterService(this);
		}

		private void Start()
		{
			m_settingsMenu = UnityEngine.Object.FindObjectOfType<MultiplayerSettingsMenu>();
			m_googlyEyesManager = GooglyEyes.instance;
		}

		private void OnDestroy()
		{
			CloseWaitingForMapChangeReplyPopup();
			ServiceLocator.UnRegisterSerice<NetworkBattleController>();
		}

		public void OnRegister()
		{
			m_gameMode = ServiceLocator.GetService<GameModeService>().CurrentGameMode;
			if (m_gameMode is OnlineMultiplayerGameMode)
			{
				m_playerActions = PlayerActions.Instance;
				m_gameStateManager = ServiceLocator.GetService<GameStateManager>();
				m_database = ContentDatabase.Instance();
				m_modalPanel = ServiceLocator.GetService<ModalPanel>();
				m_networkService = ServiceLocator.GetService<INetworkService>();
				m_quitController = ServiceLocator.GetService<INetworkQuitController>();
				m_settingsProfileManager = ServiceLocator.GetService<SettingsProfileManager>();
				m_projectMarsGameSettings = ServiceLocator.GetService<ProjectMarsGameSettingsAsset>();
				maxBudget = (int)m_projectMarsGameSettings.GameBudget.current;
				blindMode = m_projectMarsGameSettings.ProjectMarsTurnStyle == ProjectMarsGameSettingsAsset.TurnStyle.Blind;
				m_maxUnitsAllowed = (int)m_projectMarsGameSettings.UnitCap.max;
				m_settingsProfileManager.CurrentSettingsProfile.CurrentMartians = (int)m_projectMarsGameSettings.UnitCap.current;
				m_gameStateManager.GameStateChanged += OnGameStateChanged;
				((OnlineMultiplayerGameMode)m_gameMode).MatchEnded += OnMatchEnded;
				m_phaseCourier = base.gameObject.AddComponent<NetworkPhaseCourier>();
				m_phaseCourier.RemotePhaseChanged += OnRemotePhaseChanged;
				m_timeService = ServiceLocator.GetService<ITimeService>();
				if (!m_didSetAllowZeroTimeScaleForMultiplayer)
				{
					m_didSetAllowZeroTimeScaleForMultiplayer = true;
					m_timeService.PreventZeroTimeScaleForMultiplayer();
				}
			}
		}

		public void UnRegister()
		{
			if (m_gameMode is OnlineMultiplayerGameMode)
			{
				if (m_gameStateManager != null)
				{
					m_gameStateManager.GameStateChanged -= OnGameStateChanged;
				}
				if (m_gameMode != null)
				{
					((OnlineMultiplayerGameMode)m_gameMode).MatchEnded -= OnMatchEnded;
				}
				if (m_phaseCourier != null)
				{
					m_phaseCourier.RemotePhaseChanged -= OnRemotePhaseChanged;
					m_phaseCourier.DestroyCourier();
					m_phaseCourier = null;
				}
				if (m_timeService != null && m_didSetAllowZeroTimeScaleForMultiplayer)
				{
					m_didSetAllowZeroTimeScaleForMultiplayer = false;
					m_timeService.AllowZeroTimeScaleForMultiplayer();
				}
			}
		}

		public void OnUpdate()
		{
			NetworkGamePhase phase = Phase;
			if (phase == NetworkGamePhase.EnteringPlacement && (m_gameMode == null || m_gameMode.EnterPlacementStateSequencer == null || !m_gameMode.EnterPlacementStateSequencer.IsRunning))
			{
				SetPhase(NetworkGamePhase.Placement);
			}
			UpdateCloseMapChangePopup();
		}

		public void OnAwake()
		{
		}

		public void OnStart()
		{
		}

		public void OnFixedUpdate()
		{
		}

		public void OnLateUpdate()
		{
		}

		public void ClearAllUnitsForTeam(Team team)
		{
			RemoveAllUnitsEvent removeAllUnitsEvent = RemoveAllUnitsEvent.Create(GlobalTargets.OnlyServer, ReliabilityModes.ReliableOrdered);
			removeAllUnitsEvent.Team = (int)team;
			removeAllUnitsEvent.Send();
		}

		public void PlayerEnterPlacement(Team team)
		{
			SetPhase(NetworkGamePhase.ReadyForPlacement);
		}

		public void PlayerPrematurelyEndedBattle(Team team)
		{
			SetPhase(NetworkGamePhase.PrematurelyEndingBattle);
			OnLocalOrRemotePrematurelyEndedBattle();
		}

		public void PlayerIsReady(Team team)
		{
			SendPlayerReadyEvent(team, isReady: true);
		}

		public void PlayerIsNotReady(Team team)
		{
			SendPlayerReadyEvent(team, isReady: false);
		}

		public void SetRequestBattleEnd()
		{
			SetPhase(NetworkGamePhase.RequestBattleEnd);
		}

		public void CancelRequestBattleEnd()
		{
			if (Phase != NetworkGamePhase.Battle)
			{
				SetPhase(NetworkGamePhase.Battle);
			}
		}

		public void SetPrematureBattleEnd()
		{
			SetPhase(NetworkGamePhase.PrematurelyEndingBattle);
		}

		public void SetReadyForPlacement()
		{
			SetPhase(NetworkGamePhase.ReadyForPlacement);
		}

		public bool IsPlayerReady(Team team)
		{
			switch (team)
			{
			case Team.Blue:
				return m_blueIsReady;
			case Team.Red:
				return m_redIsReady;
			default:
				Debug.LogErrorFormat("Unsupported team: {0}", team);
				return false;
			}
		}

		public void OnServerBattleEnded(Team winningTeam)
		{
			if (BoltNetwork.IsServer)
			{
				EndBattleEvent endBattleEvent = EndBattleEvent.Create(ReliabilityModes.ReliableOrdered);
				endBattleEvent.WinningTeam = (int)winningTeam;
				endBattleEvent.Send();
			}
		}

		public void RequestChangeMap(MapAsset.MapType mapType, int mapIndex)
		{
			m_checkPlayersQuitToCloseMapChangePopup = true;
			RequestMapChange requestMapChange = RequestMapChange.Create(GlobalTargets.Others, ReliabilityModes.ReliableOrdered);
			requestMapChange.MapType = (int)mapType;
			requestMapChange.MapIndex = mapIndex;
			requestMapChange.Send();
		}

		public void CancelMapChangeRequest()
		{
			m_checkPlayersQuitToCloseMapChangePopup = false;
			if (m_networkService != null && m_networkService.IsRunning && m_networkService.IsConnected)
			{
				InitiatorCancelledMapChangeEvent.Create(GlobalTargets.Others, ReliabilityModes.ReliableOrdered).Send();
			}
		}

		public void RequestChangeRule(int maxUnits, int maxBudget, bool blindMode)
		{
			RequestRulesChange requestRulesChange = RequestRulesChange.Create(GlobalTargets.Others, ReliabilityModes.ReliableOrdered);
			if (requestRulesChange != null)
			{
				requestRulesChange.MaxUnits = ((maxUnits <= m_maxUnitsAllowed) ? maxUnits : m_maxUnitsAllowed);
				requestRulesChange.MaxBudget = maxBudget;
				requestRulesChange.BlindMode = blindMode;
				requestRulesChange.Send();
			}
		}

		public void RespondToRuleChange(bool status, int maxUnits, int maxBudget, bool blindMode)
		{
			RespondRuleChange respondRuleChange = RespondRuleChange.Create(GlobalTargets.Everyone, ReliabilityModes.ReliableOrdered);
			if (respondRuleChange != null)
			{
				respondRuleChange.Status = status;
				respondRuleChange.MaxUnits = maxUnits;
				respondRuleChange.MaxBudget = maxBudget;
				respondRuleChange.BlindMode = blindMode;
				respondRuleChange.Send();
			}
		}

		public override void OnEvent(RequestRulesChange requestRulesChangeEvent)
		{
			base.OnEvent(requestRulesChangeEvent);
			if (m_settingsMenu == null)
			{
				m_settingsMenu = UnityEngine.Object.FindObjectOfType<MultiplayerSettingsMenu>();
			}
			int maxUnits = requestRulesChangeEvent.MaxUnits;
			int num = requestRulesChangeEvent.MaxBudget;
			bool flag = requestRulesChangeEvent.BlindMode;
			ProjectMarsGameSettingsAsset projectMarsGameSettingsAsset = new ProjectMarsGameSettingsAsset();
			SliderData unitCap = projectMarsGameSettingsAsset.UnitCap;
			unitCap.current = maxUnits;
			projectMarsGameSettingsAsset.UnitCap = unitCap;
			SliderData gameBudget = projectMarsGameSettingsAsset.GameBudget;
			gameBudget.current = num;
			projectMarsGameSettingsAsset.GameBudget = gameBudget;
			projectMarsGameSettingsAsset.ProjectMarsTurnStyle = (flag ? ProjectMarsGameSettingsAsset.TurnStyle.Blind : ProjectMarsGameSettingsAsset.TurnStyle.Normal);
			m_settingsMenu.ShowProposedMultiplayerSettings(projectMarsGameSettingsAsset);
		}

		public override void OnEvent(RespondRuleChange response)
		{
			base.OnEvent(response);
			if (m_settingsMenu == null)
			{
				m_settingsMenu = UnityEngine.Object.FindObjectOfType<MultiplayerSettingsMenu>();
			}
			if (!response.Status)
			{
				if (!response.FromSelf)
				{
					m_settingsMenu.CloseWaitingForResponsePopup();
					m_modalPanel.PopUp("MP_POPUP_REJECTED_PROPOSAL", null, -1f, false);
				}
				return;
			}
			SliderData unitCap = m_projectMarsGameSettings.UnitCap;
			unitCap.current = ((response.MaxUnits <= m_maxUnitsAllowed) ? response.MaxUnits : m_maxUnitsAllowed);
			m_projectMarsGameSettings.UnitCap = unitCap;
			SliderData gameBudget = m_projectMarsGameSettings.GameBudget;
			gameBudget.current = response.MaxBudget;
			m_projectMarsGameSettings.GameBudget = gameBudget;
			m_projectMarsGameSettings.ProjectMarsTurnStyle = (response.BlindMode ? ProjectMarsGameSettingsAsset.TurnStyle.Blind : ProjectMarsGameSettingsAsset.TurnStyle.Normal);
			blindMode = true;
			m_settingsMenu.CloseWaitingForResponsePopup();
			if (!response.FromSelf)
			{
				m_modalPanel.PopUp("MP_POPUP_ACCEPTED_PROPOSAL", ReloadMapAfterSettingsChange, -1f, false);
			}
			else
			{
				ReloadMapAfterSettingsChange();
			}
		}

		private void ReloadMapAfterSettingsChange()
		{
			TABSSceneManager.ReloadMap();
		}

		public override void OnEvent(PlayerReadyEvent playerReadyEvent)
		{
			base.OnEvent(playerReadyEvent);
			SetTeamIsReady((Team)playerReadyEvent.Team, playerReadyEvent.IsReady);
			ServerStartBattle();
		}

		public override void OnEvent(StartBattleEvent startBattleEvent)
		{
			base.OnEvent(startBattleEvent);
			if (BoltNetwork.IsClient)
			{
				StartBattle();
			}
			if (blindMode && m_googlyEyesManager != null)
			{
				m_googlyEyesManager.SetRunning(running: false);
			}
		}

		public override void OnEvent(StartPlacementEvent startPlacementEvent)
		{
			base.OnEvent(startPlacementEvent);
			SetTeamIsReady(Team.Red, isReady: false);
			SetTeamIsReady(Team.Blue, isReady: false);
			m_battleStarted = false;
			m_gameStateManager.EnterPlacementState();
			if (blindMode && m_googlyEyesManager != null)
			{
				m_googlyEyesManager.SetRunning(running: false);
			}
		}

		public override void OnEvent(EndBattleEvent endBattleEvent)
		{
			if (m_battleStarted)
			{
				base.OnEvent(endBattleEvent);
				if (BoltNetwork.IsClient)
				{
					this.ClientGotEndBattleMessage?.Invoke((Team)endBattleEvent.WinningTeam);
				}
				SetTeamIsReady(Team.Red, isReady: false);
				SetTeamIsReady(Team.Blue, isReady: false);
				m_battleStarted = false;
			}
		}

		public override void OnEvent(RemoveAllUnitsEvent removeAllUnitsEvent)
		{
			base.OnEvent(removeAllUnitsEvent);
			Team team = (Team)removeAllUnitsEvent.Team;
			this.ClearTeamEvent?.Invoke(team);
			ReplyToRemoveAllUnitsEvent replyToRemoveAllUnitsEvent = ReplyToRemoveAllUnitsEvent.Create(GlobalTargets.AllClients, ReliabilityModes.ReliableOrdered);
			replyToRemoveAllUnitsEvent.Team = removeAllUnitsEvent.Team;
			replyToRemoveAllUnitsEvent.Send();
		}

		public override void OnEvent(ReplyToRemoveAllUnitsEvent respondEvent)
		{
			base.OnEvent(respondEvent);
			Team team = (Team)respondEvent.Team;
			ServiceLocator.GetService<GameModeService>().CurrentGameMode.OnClearedTeam(team);
		}

		public override void OnEvent(RequestMapChange request)
		{
			base.OnEvent(request);
			MapAsset.MapType mapType = (MapAsset.MapType)request.MapType;
			int mapIndex = request.MapIndex;
			MapAsset mapAssetByTypeAndMapIndex = m_database.GetMapAssetByTypeAndMapIndex(mapType, mapIndex);
			if (mapAssetByTypeAndMapIndex == null)
			{
				Debug.LogError($"Map is null, MapType: {mapType}, mapIndex: {mapIndex}");
				return;
			}
			m_modalPanel.Choice(string.Empty, "MP_POPUP_MAPCHANGE_REQUEST", delegate
			{
				RespondToMapChange(status: true, mapType, mapIndex);
			}, delegate
			{
				RespondToMapChange(status: false, mapType, mapIndex);
			}, "BUTTON_YES", "BUTTON_NO", false, Localizer.GetSinglePhrase(mapAssetByTypeAndMapIndex.Entity.Name), "\n");
		}

		public override void OnEvent(RespondMapChange response)
		{
			base.OnEvent(response);
			if (!response.Status)
			{
				if (!response.FromSelf)
				{
					m_modalPanel.PopUp("MP_POPUP_MAPCHANGE_DECLINED", null, -1f, false);
				}
				return;
			}
			MapAsset.MapType mapType = (MapAsset.MapType)response.MapType;
			int mapIndex = response.MapIndex;
			if (BoltNetwork.IsServer && BoltMatchmaking.CurrentSession is PhotonSession session && session.GetProtocolToken() is PhotonRoomProperties photonRoomProperties)
			{
				photonRoomProperties.UpdateMap(mapType, mapIndex);
				BoltMatchmaking.UpdateSession(photonRoomProperties);
			}
			m_projectMarsGameSettings.ResetSettings();
			MapAsset mapAssetByTypeAndMapIndex = m_database.GetMapAssetByTypeAndMapIndex(mapType, mapIndex);
			bool invokeExistingDismissActionOfPopupPanel = ServiceLocator.GetService<ModalPanel>().OpenId != WaitingForOtherPlayerToAcceptMapOpenId;
			TABSSceneManager.LoadMap(mapAssetByTypeAndMapIndex, invokeExistingDismissActionOfPopupPanel);
		}

		public override void OnEvent(InitiatorCancelledMapChangeEvent changeCancelEvent)
		{
			base.OnEvent(changeCancelEvent);
			if (!changeCancelEvent.FromSelf)
			{
				m_modalPanel.CloseWaitPopup();
				m_modalPanel.PopUp("MP_POPUP_MAPCHANGE_CANCELLED");
			}
		}

		public override void OnEvent(MaxUnitsEvent maxEvent)
		{
			base.OnEvent(maxEvent);
			int? remoteMaxUnits = RemoteMaxUnits;
			DidGetRemoteMaxUnits = true;
			RemoteMaxUnits = (maxEvent.HasMaxUnits ? new int?(maxEvent.MaxUnits) : ((int?)null));
			this.GotRemoteMaxUnits?.Invoke(remoteMaxUnits, RemoteMaxUnits);
		}

		private void OnGameStateChanged(GameState gameState)
		{
			switch (gameState)
			{
			case GameState.BattleState:
				SetPhase(NetworkGamePhase.Battle);
				break;
			case GameState.PlacementState:
				SetPhase(NetworkGamePhase.EnteringPlacement);
				break;
			default:
				Debug.LogError($"Unsupported game state: {gameState}");
				break;
			case GameState.None:
				break;
			}
		}

		private void OnMatchEnded()
		{
			SetPhase(NetworkGamePhase.BattleEnded);
		}

		private void SetPhase(NetworkGamePhase phase)
		{
			NetworkGamePhase phase2 = Phase;
			if (m_phaseCourier != null)
			{
				m_phaseCourier.SetPhase(phase);
			}
			if (phase2 != Phase)
			{
				this.PhaseChanged?.Invoke(phase2, Phase);
			}
			if (phase == NetworkGamePhase.ReadyForPlacement)
			{
				ServerStartPlacement();
			}
		}

		private void SetTeamIsReady(Team team, bool isReady)
		{
			bool flag;
			switch (team)
			{
			case Team.Blue:
				flag = m_blueIsReady;
				m_blueIsReady = isReady;
				break;
			case Team.Red:
				flag = m_redIsReady;
				m_redIsReady = isReady;
				break;
			default:
				Debug.LogErrorFormat("Unsupported team: {0}", team);
				return;
			}
			if (flag != isReady)
			{
				this.PlayerIsReadyChanged?.Invoke(team, flag, isReady);
			}
		}

		private void OnRemotePhaseChanged(NetworkGamePhase oldPhase, NetworkGamePhase newPhase)
		{
			if (oldPhase == NetworkGamePhase.Initializing)
			{
				OnBothPlayersInBattleScene();
			}
			this.RemotePhaseChanged?.Invoke(oldPhase, newPhase);
			switch (newPhase)
			{
			case NetworkGamePhase.ReadyForPlacement:
				if (Phase == NetworkGamePhase.PrematurelyEndingBattle)
				{
					SetPhase(NetworkGamePhase.ReadyForPlacement);
				}
				else
				{
					ServerStartPlacement();
				}
				break;
			case NetworkGamePhase.PrematurelyEndingBattle:
				OnLocalOrRemotePrematurelyEndedBattle();
				break;
			}
		}

		private void OnBothPlayersInBattleScene()
		{
			if (RemotePhase != NetworkGamePhase.Disconnected)
			{
				AreBothPlayersInBattleScene = true;
				SendMaxUnitsEvent();
				m_gameMode.BattleBudget.SetBudget(maxBudget);
				this.BothPlayersEnteredBattleScene?.Invoke();
			}
		}

		private void SendPlayerReadyEvent(Team team, bool isReady)
		{
			PlayerReadyEvent playerReadyEvent = PlayerReadyEvent.Create(ReliabilityModes.ReliableOrdered);
			playerReadyEvent.Team = (int)team;
			playerReadyEvent.IsReady = isReady;
			playerReadyEvent.Send();
		}

		private void SendMaxUnitsEvent()
		{
			bool flag = m_settingsProfileManager != null && m_settingsProfileManager.CurrentSettingsProfile != null && m_settingsProfileManager.CurrentSettingsProfile.MultiplayerMaxUnits.HasValue;
			int maxUnits = (flag ? m_settingsProfileManager.CurrentSettingsProfile.MultiplayerMaxUnits.Value : 0);
			MaxUnitsEvent maxUnitsEvent = MaxUnitsEvent.Create(GlobalTargets.Others, ReliabilityModes.ReliableOrdered);
			maxUnitsEvent.MaxUnits = maxUnits;
			maxUnitsEvent.HasMaxUnits = flag;
			maxUnitsEvent.Send();
		}

		private void ServerStartBattle()
		{
			if (BoltNetwork.IsServer && !m_battleStarted && m_blueIsReady && m_redIsReady)
			{
				StartBattleEvent.Create(GlobalTargets.Others, ReliabilityModes.ReliableOrdered).Send();
				StartBattle();
			}
		}

		private void ServerStartPlacement()
		{
			if (BoltNetwork.IsServer && Phase == NetworkGamePhase.ReadyForPlacement && RemotePhase == NetworkGamePhase.ReadyForPlacement)
			{
				StartPlacementEvent.Create(GlobalTargets.Everyone, ReliabilityModes.ReliableOrdered).Send();
			}
		}

		private void StartBattle()
		{
			if (!m_battleStarted)
			{
				m_battleStarted = true;
				SetTeamIsReady(Team.Red, isReady: false);
				SetTeamIsReady(Team.Blue, isReady: false);
				m_playerActions.m_enterExitBattle.ClearInputState();
				m_gameStateManager.EnterBattleState();
			}
		}

		private void RespondToMapChange(bool status, MapAsset.MapType mapType, int mapIndex)
		{
			if (m_networkService != null && m_networkService.IsRunning && m_networkService.IsConnected)
			{
				RespondMapChange respondMapChange = RespondMapChange.Create(GlobalTargets.Everyone, ReliabilityModes.ReliableOrdered);
				respondMapChange.Status = status;
				respondMapChange.MapType = (int)mapType;
				respondMapChange.MapIndex = mapIndex;
				respondMapChange.Send();
			}
		}

		private void OnLocalOrRemotePrematurelyEndedBattle()
		{
			if (Phase == NetworkGamePhase.PrematurelyEndingBattle && RemotePhase == NetworkGamePhase.PrematurelyEndingBattle)
			{
				SetPhase(NetworkGamePhase.ReadyForPlacement);
			}
			else if (RemotePhase == NetworkGamePhase.PrematurelyEndingBattle)
			{
				SetPhase(NetworkGamePhase.PrematurelyEndingBattle);
			}
		}

		private void UpdateCloseMapChangePopup()
		{
			if (m_checkPlayersQuitToCloseMapChangePopup && m_quitController != null && (m_quitController.DidQuit || m_quitController.DidOpponentQuit))
			{
				CloseWaitingForMapChangeReplyPopup();
			}
		}

		private void CloseWaitingForMapChangeReplyPopup()
		{
			m_checkPlayersQuitToCloseMapChangePopup = false;
			if (m_modalPanel != null && m_modalPanel.IsPopupOpen && m_modalPanel.OpenId == WaitingForOtherPlayerToAcceptMapOpenId)
			{
				m_modalPanel.CloseWaitPopup();
			}
		}
	}
}
