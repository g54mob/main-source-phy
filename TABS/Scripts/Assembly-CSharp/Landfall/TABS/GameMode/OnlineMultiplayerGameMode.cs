using System;
using Landfall.TABS.AI.Systems;
using Landfall.TABS.Budget;
using Landfall.TABS.Budget.Wallets;
using Landfall.TABS.GameState;
using Landfall.TABS.RuntimeCleanup;
using Landfall.TABS.Services;
using Landfall.TABS.UnitPlacement;
using Landfall.TABS.Workshop;
using Landfall.TABS_Input;
using Photon.Bolt;
using TFBG;
using TFBGames;
using Unity.Entities;
using UnityEngine;

namespace Landfall.TABS.GameMode
{
	public class OnlineMultiplayerGameMode : BaseGameMode, IDisruptionServiceSubscriber
	{
		private SettingsInstance m_flipColorsSetting;

		private TeamSystem m_teamSystem;

		private NetworkBattleController m_networkBattle;

		private INetworkService m_networkService;

		private ITimeService m_timeService;

		private GameDisruptionService m_gameDisruptionService;

		private CountdownTimerService m_countdownTimerService;

		private GameModeService m_gameModeService;

		private Team m_winners;

		private INetworkQuitController m_quitController;

		private bool m_checkForQuit;

		private PlayerActions m_playerActions;

		private ModalPanel modalPanel;

		private const float endMatchCountdownTimeDuration = 10f;

		private const float endMatchTimeScale = 0.001f;

		private const float endMatchTimeScaleTransitionTime = 0.5f;

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

		protected override bool CanUseSlowMotion => false;

		public float? TimerEndedTime { get; private set; }

		public event Action MatchEnded;

		public override void Start()
		{
			base.BattleBudget = new BattleBudget();
			base.BattleBudget.InitializeWithWalletType<LocalMultiplayerWallet>();
			base.Brush = new UnitPlacementBrush(base.GameModeService);
			base.Brush.InitializeBrushWithType<BrushBehaviourOnlineMultiplayer>();
			m_flipColorsSetting = ServiceLocator.GetService<GlobalSettingsHandler>().GetSettingsInstance("GAMEPLAY_FLIP_COLORS");
			m_teamSystem = World.Active.GetOrCreateManager<TeamSystem>();
			new GameObject("SandboxUIManager").AddComponent<SandboxUIManager>();
			m_networkService = ServiceLocator.GetService<INetworkService>();
			m_timeService = ServiceLocator.GetService<ITimeService>();
			m_gameDisruptionService = ServiceLocator.GetService<GameDisruptionService>();
			m_countdownTimerService = ServiceLocator.GetService<CountdownTimerService>();
			m_gameModeService = ServiceLocator.GetService<GameModeService>();
			modalPanel = ServiceLocator.GetService<ModalPanel>();
			m_playerActions = PlayerActions.Instance;
			m_quitController = ServiceLocator.GetService<INetworkQuitController>();
			base.Start();
		}

		public override void Update()
		{
			base.Update();
			UpdateCheckForQuit();
		}

		public override void OnEnterNewScene()
		{
			base.OnEnterNewScene();
			if (CampaignHandler.LastLoadedLevel != null)
			{
				CampaignHandler.LoadLevel(CampaignHandler.LastLoadedLevel, campaign: true);
				CampaignHandler.ResetLoadedLevel();
			}
			else
			{
				base.TeamLayouts.ClearTeamLayout(Team.Red);
				base.TeamLayouts.ClearTeamLayout(Team.Blue);
			}
			if (NetworkBattle != null)
			{
				NetworkBattle.ClientGotEndBattleMessage -= OnClientGotEndBattleMessage;
				NetworkBattle.RemotePhaseChanged -= OnPhaseChanged;
				NetworkBattle.PhaseChanged -= OnPhaseChanged;
			}
			m_networkBattle = ServiceLocator.GetService<NetworkBattleController>();
			if (NetworkBattle != null)
			{
				NetworkBattle.ClientGotEndBattleMessage += OnClientGotEndBattleMessage;
				NetworkBattle.RemotePhaseChanged += OnPhaseChanged;
				NetworkBattle.PhaseChanged += OnPhaseChanged;
			}
		}

		private void OnPhaseChanged(NetworkGamePhase oldphase, NetworkGamePhase newphase)
		{
			if (newphase == NetworkGamePhase.Disconnected)
			{
				modalPanel.CloseRulePopUps();
			}
		}

		public override void OnEnterPlacementState()
		{
			base.CurtainController.OpenCurtains();
			ResetForPlacement();
			ServiceLocator.GetService<RuntimeGarbageCollector>().ForceFlushGC();
			base.MatchOver = false;
			base.OnEnterPlacementState();
		}

		public override void OnEnterBattleState()
		{
			if (base.CameraMovementZoom != null)
			{
				base.CameraMovementZoom.GoToCenter();
			}
			if (BoltNetwork.IsRunning)
			{
				ShowBlindPlacementUnits();
			}
			base.OnEnterBattleState();
		}

		public override void OnUnitRemoved(Unit unit, Team team)
		{
			base.OnUnitRemoved(unit, team);
			base.PlacementUI.OnUnitCountChanged(team);
		}

		public override void OnUnitSpawned(Unit unit, Team team)
		{
			base.OnUnitSpawned(unit, team);
			base.PlacementUI.OnUnitCountChanged(team);
		}

		protected override void BattleStateUpdate(PlayerActions playerActions)
		{
			UpdateForEnterExitBattle(playerActions);
			UpdateForMatchOver(playerActions);
		}

		protected override void UpdateForEnterExitBattle(PlayerActions playerActions)
		{
			if (playerActions.m_enterExitBattle.WasReleased)
			{
				RequestEndBattle();
			}
		}

		public override void DecideWinner(Team winningTeam, string subText)
		{
			if (BoltNetwork.IsServer && !IsBattleAlreadyOver())
			{
				base.DecideWinner(winningTeam, subText);
			}
		}

		public void RequestEndBattle()
		{
			if (base.MatchOver)
			{
				return;
			}
			if (NetworkBattle.RemotePhase == NetworkGamePhase.RequestBattleEnd)
			{
				NetworkBattle.SetPrematureBattleEnd();
				return;
			}
			switch (NetworkBattle.Phase)
			{
			case NetworkGamePhase.RequestBattleEnd:
				NetworkBattle.CancelRequestBattleEnd();
				break;
			case NetworkGamePhase.Battle:
				NetworkBattle.SetRequestBattleEnd();
				break;
			}
		}

		public override void OnWinnerDecided(Team winningTeam, string winSubText)
		{
			if (BoltNetwork.IsServer && !IsBattleAlreadyOver())
			{
				EndMatch();
				base.OnWinnerDecided(winningTeam, winSubText);
				if (NetworkBattle != null)
				{
					NetworkBattle.OnServerBattleEnded(winningTeam);
				}
				BeginEndingMatch(winningTeam);
			}
		}

		private bool IsBattleAlreadyOver()
		{
			if (!base.MatchOver)
			{
				if (m_networkBattle.Phase != NetworkGamePhase.Battle)
				{
					return m_networkBattle.Phase != NetworkGamePhase.RequestBattleEnd;
				}
				return false;
			}
			return true;
		}

		private void EndMatch()
		{
			base.MatchOver = true;
			this.MatchEnded?.Invoke();
		}

		public void Subscribe()
		{
			TimerEndedTime = null;
			m_countdownTimerService.OnCounterEnded += OnCountDownTimerEnded;
			m_gameDisruptionService.AddWatcher(m_countdownTimerService, this);
		}

		public void Unsubscribe()
		{
			if (!(m_countdownTimerService == null))
			{
				m_countdownTimerService.OnCounterEnded -= OnCountDownTimerEnded;
				ToggleUIInput(inputFunctioning: true);
				SetTimeToPaused(pauseTime: false);
			}
		}

		protected override bool CanPressButtonToEndBattle()
		{
			if (CampaignPlayerDataHolder.CurrentGameModeState != GameModeState.Menu && !m_timeService.IsPaused())
			{
				return base.GameStateManager.GameState == Landfall.TABS.GameState.GameState.BattleState;
			}
			return false;
		}

		protected override void EnterPlacementMode()
		{
			PlayerActions.Instance?.m_enterExitBattle.ClearInputState();
			if (!base.MatchOver && base.GameStateManager.GameState == Landfall.TABS.GameState.GameState.BattleState)
			{
				NetworkBattle.PlayerPrematurelyEndedBattle(m_networkService.PlayerTeam);
			}
			else
			{
				NetworkBattle.PlayerEnterPlacement(m_networkService.PlayerTeam);
			}
		}

		private void ShowBlindPlacementUnits()
		{
			if (TABSSceneManager.IsInMainMenuScene() || !(m_gameModeService.CurrentGameMode.GetType() == typeof(OnlineMultiplayerGameMode)) || !NetworkBattle.blindMode)
			{
				return;
			}
			Team team = ((!BoltNetwork.IsClient) ? Team.Blue : Team.Red);
			foreach (Unit teamUnit in m_teamSystem.GetTeamUnits(team))
			{
				teamUnit.ShowRenderers(enable: true);
			}
		}

		private void BeginEndingMatch(Team winningTeam)
		{
			m_winners = winningTeam;
			Subscribe();
			SetTimeToPaused(pauseTime: true);
			ToggleUIInput(inputFunctioning: false);
			DisplayEndOfMatchUI();
			m_countdownTimerService.BeginCountDown(10f);
		}

		private void OnCountDownTimerEnded()
		{
			TimerEndedTime = Time.realtimeSinceStartup;
			m_countdownTimerService.OnCounterEnded -= OnCountDownTimerEnded;
			m_countdownTimerService.EndTimer();
			m_gameDisruptionService.RemoveWatcher(this);
			if (!(NetworkBattle == null) && NetworkBattle.Phase == NetworkGamePhase.BattleEnded)
			{
				NetworkBattle.SetReadyForPlacement();
			}
		}

		private void ResetForPlacement()
		{
			ToggleUIInput(inputFunctioning: true);
			SetTimeToPaused(pauseTime: false);
		}

		private void DisplayEndOfMatchUI()
		{
			string empty = string.Empty;
			empty = ((m_networkService.PlayerTeam == m_winners) ? "LABEL_VICTORY_TITLE" : "LABEL_DEFEATED");
			base.CurtainController.PeekCurtains(empty, string.Empty);
		}

		private void ToggleUIInput(bool inputFunctioning)
		{
		}

		private void OnClientGotEndBattleMessage(Team winningTeam)
		{
			EndMatch();
			BeginEndingMatch(winningTeam);
		}

		private void SetTimeToPaused(bool pauseTime)
		{
			if (!(CanCrashWhenQuitDuringResultsScreen() && pauseTime) || m_networkService == null || !m_networkService.IsClient)
			{
				BoltRuntimeSettings.instance.overrideTimeScale = !pauseTime;
				float targetTimeScale = (pauseTime ? 0.001f : 1f);
				m_timeService.SetState(targetTimeScale, 0.5f);
				if (CanCrashWhenQuitDuringResultsScreen())
				{
					m_checkForQuit = pauseTime;
				}
			}
		}

		private void UpdateCheckForQuit()
		{
			if (m_checkForQuit && m_quitController != null && (m_quitController.DidQuit || m_quitController.DidOpponentQuit))
			{
				m_checkForQuit = false;
				SetTimeToPaused(pauseTime: false);
			}
		}

		private bool CanCrashWhenQuitDuringResultsScreen()
		{
			return false;
		}
	}
}
