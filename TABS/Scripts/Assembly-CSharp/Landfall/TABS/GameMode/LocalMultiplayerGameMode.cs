using System;
using System.Collections.Generic;
using Landfall.TABS.AI.Systems;
using Landfall.TABS.Budget;
using Landfall.TABS.Budget.Wallets;
using Landfall.TABS.GameState;
using Landfall.TABS.RuntimeCleanup;
using Landfall.TABS.UnitPlacement;
using Landfall.TABS_Input;
using TFBGames;
using Unity.Entities;
using UnityEngine;

namespace Landfall.TABS.GameMode
{
	public class LocalMultiplayerGameMode : BaseGameMode
	{
		private const float CountdownTime = 3f;

		private const string Player1 = "MP_LABEL_PLAYER_ONE";

		private const string Player2 = "MP_LABEL_PLAYER_TWO";

		private const int MaxPlayers = 2;

		private ILocalMultiplayerUI ui;

		private SettingsInstance m_flipColorsSetting;

		private TeamSystem m_teamSystem;

		private Coroutine m_checkGameOverRoutine;

		private InputService inputService;

		private SplitScreenController splitScreen;

		private SplitScreenHandler splitScreenHandler;

		private int maxBudget;

		private float maxPlacementTime;

		private float placementTimer;

		private bool countdownTimerStarted;

		private bool countdownTimerEnded;

		private bool canPlacementTimerTick;

		private float countdownTimer;

		private TFBGames.Player currentPlayer;

		private TFBGames.Player previousPlayer;

		private int playerOneTurnsRequired;

		private PlayerProfile redPlayer;

		private PlayerProfile bluePlayer;

		private int[] turnsTaken;

		private readonly List<IPossessController> possessControllers = new List<IPossessController>(2);

		public TFBGames.Player CurrentPlayer => currentPlayer;

		public bool IsTimedPlacement => LocalMultiplayerGameRules.IsTimerToPlaceUnitsOn;

		public LocalMultiplayerGameRules LocalMultiplayerGameRules { get; private set; }

		public override void Start()
		{
			CampaignPlayerDataHolder.StartedPlayingLocalMultiplayer();
			LocalMultiplayerGameRules = ServiceLocator.GetService<LocalMultiplayerGameRules>();
			if (LocalMultiplayerGameRules != null)
			{
				LocalMultiplayerGameRules.OnSettingsChanged += ResetMatch;
			}
			m_flipColorsSetting = ServiceLocator.GetService<GlobalSettingsHandler>().GetSettingsInstance("GAMEPLAY_FLIP_COLORS");
			m_teamSystem = World.Active.GetOrCreateManager<TeamSystem>();
			splitScreen = ServiceLocator.GetService<SplitScreenController>();
			inputService = ServiceLocator.GetService<InputService>();
			base.Brush = new UnitPlacementBrush(base.GameModeService);
			base.OnDonePlacingUnitsCallback = (OnDonePlacingAllUnitsDelegate)Delegate.Combine(base.OnDonePlacingUnitsCallback, new OnDonePlacingAllUnitsDelegate(OnPlacementFinished));
			base.Start();
			ResetMatch();
		}

		public override void Destroy()
		{
			if (splitScreenHandler != null)
			{
				splitScreenHandler.Cleanup();
			}
			if (LocalMultiplayerGameRules != null)
			{
				LocalMultiplayerGameRules.OnSettingsChanged -= ResetMatch;
			}
			base.Destroy();
		}

		private void ResetMatch()
		{
			maxPlacementTime = LocalMultiplayerGameRules.TimeToPlaceUnits.current;
			maxBudget = (int)LocalMultiplayerGameRules.MaxUnitPoints.current;
			placementTimer = maxPlacementTime;
			switch (LocalMultiplayerGameRules.TurnStyle)
			{
			case TurnStyle.Normal:
				playerOneTurnsRequired = 1;
				break;
			case TurnStyle.HalfNHalf:
				playerOneTurnsRequired = 2;
				break;
			default:
				throw new ArgumentOutOfRangeException("TurnStyle", LocalMultiplayerGameRules.TurnStyle, "Cannot resolve value of TurnStyle.");
			}
			base.BattleBudget = new BattleBudget();
			base.BattleBudget.InitializeWithWalletType<LocalMultiplayerWallet>();
			base.BattleBudget.SetBudget(maxBudget);
			currentPlayer = TFBGames.Player.One;
			inputService.SetPlayer(currentPlayer);
			base.Brush.InitializeBrushWithType<BrushBehaviourLocalMultiplayer>();
			turnsTaken = new int[2];
			ResetPlacementTimer();
		}

		private void OnPlacementFinished()
		{
			if (LocalMultiplayerGameRules.IsTimerToPlaceUnitsOn)
			{
				canPlacementTimerTick = true;
			}
		}

		public override void OnEnterNewScene()
		{
			base.OnEnterNewScene();
			if (base.PlacementUI != null)
			{
				List<PlayerProfile> profiles = base.PlacementUI.StubProfileConfiguration.Profiles;
				redPlayer = new PlayerProfile("MP_LABEL_PLAYER_ONE", profiles[0].PlayerIcon, string.Empty, Team.Red, null);
				bluePlayer = new PlayerProfile("MP_LABEL_PLAYER_TWO", profiles[1].PlayerIcon, string.Empty, Team.Blue, null);
				if (ui != null)
				{
					ui.SetPlayerProfile(TFBGames.Player.One, redPlayer);
					ui.SetPlayerProfile(TFBGames.Player.Two, bluePlayer);
				}
				base.PlacementUI.SetMartians(redPlayer, bluePlayer);
			}
		}

		public override void Update()
		{
			if (inputService.PlayerInputDevices.Length >= 2)
			{
				base.Update();
			}
		}

		protected override void PlacementStateUpdate(PlayerActions playerActions)
		{
			if (IsPhaseChangeTriggered(playerActions) && CanSwitchPhase(currentPlayer))
			{
				ChangePhase();
			}
		}

		protected override void BattleStateUpdate(PlayerActions playerActions)
		{
			base.BattleStateUpdate(playerActions);
			if (splitScreenHandler != null)
			{
				splitScreenHandler.Update(playerActions);
			}
		}

		public void AddPossessController(IPossessController possessController)
		{
			possessControllers.Add(possessController);
			if (possessControllers.Count == 2)
			{
				splitScreenHandler = new SplitScreenHandler(LocalMultiplayerGameRules.SplitScreenMergeDelay, inputService.PlayerActionsGroup, possessControllers, splitScreen);
			}
		}

		public void SetLocalMultiplayerUI(ILocalMultiplayerUI localMultiplayerUI)
		{
			if (localMultiplayerUI != null)
			{
				ui = localMultiplayerUI;
			}
		}

		private bool CanSwitchPhase(TFBGames.Player player)
		{
			int num;
			switch (player)
			{
			case TFBGames.Player.Any:
				return false;
			default:
				num = 1;
				break;
			case TFBGames.Player.One:
				num = 0;
				break;
			}
			Team team = (Team)num;
			return m_teamSystem.GetTeamUnits(team).Count > 0;
		}

		private bool HasPlacementTimeExpired()
		{
			if (!LocalMultiplayerGameRules.IsTimerToPlaceUnitsOn)
			{
				return false;
			}
			if (canPlacementTimerTick)
			{
				placementTimer -= Time.unscaledDeltaTime;
			}
			if (ui != null)
			{
				ui.UpdateTime(placementTimer);
			}
			return placementTimer < 0f;
		}

		private bool IsPhaseChangeTriggered(PlayerActions playerActions)
		{
			if (IsMenuOpen())
			{
				return false;
			}
			bool flag = playerActions.m_enterExitBattle.WasReleased;
			if (LocalMultiplayerGameRules.IsTimerToPlaceUnitsOn)
			{
				flag |= HasPlacementTimeExpired();
			}
			return flag;
		}

		private int GetRequiredTurns(TFBGames.Player player)
		{
			switch (player)
			{
			case TFBGames.Player.One:
				return playerOneTurnsRequired;
			case TFBGames.Player.Two:
				return 1;
			default:
				throw new ArgumentOutOfRangeException($"{player} Does not have a valid turn requirement");
			}
		}

		private void ShowUnits(Team team, bool show)
		{
			foreach (Unit teamUnit in m_teamSystem.GetTeamUnits(team))
			{
				teamUnit.ShowRenderers(show);
			}
		}

		public void ChangePhase()
		{
			turnsTaken[(int)currentPlayer]++;
			previousPlayer = currentPlayer;
			currentPlayer = ((currentPlayer == TFBGames.Player.One) ? TFBGames.Player.Two : TFBGames.Player.One);
			Team currentTeam = ((currentPlayer != TFBGames.Player.One) ? Team.Blue : Team.Red);
			inputService.SetPlayer(currentPlayer);
			ResetPlacementTimer();
			base.PlacementUI.ShowClearUIOnlyForPlayer(currentPlayer);
			bool flag = GetRequiredTurns(previousPlayer) == turnsTaken[(int)previousPlayer];
			if (ui != null)
			{
				ui.SetCurrentTeam(currentTeam);
				ui.SetPlayerStatus(currentPlayer, LocalMultiplayerPlayerStatus.Building);
				ui.SetPlayerStatus(previousPlayer, (!flag) ? LocalMultiplayerPlayerStatus.Waiting : LocalMultiplayerPlayerStatus.Ready);
			}
			if (CanStartBattle())
			{
				inputService.SetPlayer(TFBGames.Player.Any);
				base.PlacementUI.ShowClearUIOnlyForPlayer(TFBGames.Player.Any);
				ShowUnits(Team.Blue, show: true);
				ShowUnits(Team.Red, show: true);
				base.PlacementUI.StartBattle();
				if (ui != null)
				{
					ui.UpdateGameState(Landfall.TABS.GameState.GameState.BattleState);
				}
			}
		}

		private void ResetPlacementTimer()
		{
			if (LocalMultiplayerGameRules.IsTimerToPlaceUnitsOn)
			{
				placementTimer = maxPlacementTime;
			}
		}

		public bool CanPlaceUnits(Team team)
		{
			if (base.Brush == null || base.Brush.UnitToSpawn == null)
			{
				return false;
			}
			TFBGames.Player player = ((team != Team.Red) ? TFBGames.Player.Two : TFBGames.Player.One);
			int num = base.BattleBudget.GetBudget(team) - (int)base.Brush.UnitToSpawn.GetUnitCost();
			int num2 = turnsTaken[(int)player];
			int requiredTurns = GetRequiredTurns(player);
			int num3 = (int)((float)maxBudget * ((float)(num2 + 1) / (float)requiredTurns));
			return num >= maxBudget - num3;
		}

		public bool CanStartBattle()
		{
			if (turnsTaken[0] >= GetRequiredTurns(TFBGames.Player.One))
			{
				return turnsTaken[1] >= GetRequiredTurns(TFBGames.Player.Two);
			}
			return false;
		}

		public Team CurrentPlayerTeam()
		{
			switch (inputService.CurrentPlayer)
			{
			case TFBGames.Player.One:
				return Team.Red;
			case TFBGames.Player.Two:
				return Team.Blue;
			default:
				return Team.Red;
			}
		}

		protected override void EnterPlacementMode()
		{
			base.EnterPlacementMode();
			PlayerActions.Instance.m_enterExitBattle.ClearInputState();
			base.BattleBudget.SetBudget(maxBudget);
			countdownTimerStarted = false;
			countdownTimerEnded = false;
			canPlacementTimerTick = false;
			currentPlayer = TFBGames.Player.One;
			turnsTaken[0] = 0;
			turnsTaken[1] = 0;
			base.PlacementUI.ShowClearUIOnlyForPlayer(TFBGames.Player.One);
			if (ui != null)
			{
				ui.SetCurrentTeam(Team.Red);
				ui.UpdateGameState(Landfall.TABS.GameState.GameState.PlacementState);
			}
		}

		public override void OnEnterPlacementState()
		{
			inputService.SetPlayer(TFBGames.Player.One);
			if (splitScreenHandler != null)
			{
				splitScreenHandler.EndSplitScreen();
			}
			if (m_checkGameOverRoutine != null)
			{
				base.GameModeService.StopCoroutine(m_checkGameOverRoutine);
				m_checkGameOverRoutine = null;
			}
			if (ui != null)
			{
				ui.SetPlayerStatus(TFBGames.Player.One, LocalMultiplayerPlayerStatus.Building);
				ui.SetPlayerStatus(TFBGames.Player.Two, LocalMultiplayerPlayerStatus.Waiting);
			}
			ServiceLocator.GetService<RuntimeGarbageCollector>().ForceFlushGC();
			base.CurtainController.OpenCurtains();
			base.MatchOver = false;
			base.OnEnterPlacementState();
		}

		public override void OnEnterBattleState()
		{
			if (base.CameraMovementZoom != null)
			{
				base.CameraMovementZoom.GoToCenter();
			}
			if (ui != null)
			{
				ui.SetPlayerStatus(TFBGames.Player.One, LocalMultiplayerPlayerStatus.None);
				ui.SetPlayerStatus(TFBGames.Player.Two, LocalMultiplayerPlayerStatus.None);
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

		public override void OnUnitDied(Unit unit)
		{
			base.OnUnitDied(unit);
			int count = m_teamSystem.GetWinConditionTeamUnits(Team.Red).Count;
			int count2 = m_teamSystem.GetWinConditionTeamUnits(Team.Blue).Count;
			if ((count == 0 || count2 == 0) && splitScreenHandler != null)
			{
				splitScreenHandler.EndSplitScreen();
			}
		}

		public override void OnWinnerDecided(Team winningTeam, string winSubText)
		{
			base.OnWinnerDecided(winningTeam, winSubText);
			string text = "LABEL_RED_VICTORY";
			string text2 = "LABEL_BLUE_VICTORY";
			if (m_flipColorsSetting.currentValue == 1)
			{
				string text3 = text;
				text = text2;
				text2 = text3;
			}
			switch (winningTeam)
			{
			case Team.Blue:
				base.CurtainController.PeekCurtains(text2, winSubText);
				break;
			case Team.Red:
				base.CurtainController.PeekCurtains(text, winSubText);
				break;
			}
		}
	}
}
