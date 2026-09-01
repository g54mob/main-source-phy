using System;
using System.Collections;
using System.Collections.Generic;
using DM;
using Landfall.TABS.AI;
using Landfall.TABS.AI.Systems;
using Landfall.TABS.Budget;
using Landfall.TABS.GameState;
using Landfall.TABS.RuntimeCleanup;
using Landfall.TABS.Services;
using Landfall.TABS.UnitPlacement;
using Landfall.TABS.WinConditions;
using Landfall.TABS.Workshop;
using Landfall.TABS_Input;
using TFBGames;
using UIStateManager;
using Unity.Entities;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Landfall.TABS.GameMode
{
	public abstract class BaseGameMode
	{
		public delegate void OnUnitRemovedDelegate(Unit unit);

		public delegate void OnUnitSpawnedDelegate(Unit unit);

		public delegate void OnDonePlacingAllUnitsDelegate();

		private TeamLayouts m_teamLayouts;

		private TaskSequencer m_enterBattleStateSequencer;

		private TaskSequencer m_enterPlacementStateSequencer;

		private GameStateManager m_gameStateManager;

		private FixedTimeStepService m_fixedTimeStepService;

		private PlacementUI m_placementUI;

		private InterfaceStateManager m_interfaceStateManager;

		private UIMapSelector m_mapSelector;

		private SandboxPlacementCamera m_placementCamera;

		private InputService m_inputService;

		private CameraMovementZoomIn m_cameraMovementZoom;

		private VerticalCurtainController m_curtainController;

		private bool m_redUnitsPlaced;

		private bool m_blueUnitsPlaced;

		private bool InPlacementUIState;

		private WinConditionPropagator m_winConditions;

		private SettingsInstance m_delayGameOverSetting;

		private ITimeService m_timeService;

		private float m_battleTime;

		private int m_loseStreak;

		private bool m_allowFreeCam;

		private bool m_toggledSlowmo;

		private bool m_toggledSuperSlowmo;

		private bool m_toggledPause;

		private bool m_supressMouseHoldRelease;

		private Coroutine m_delayedEndGameRoutine;

		private Team m_cachedCurrentWinner;

		private string m_cachedCurrentSubText;

		public VerticalCurtainController CurtainController
		{
			get
			{
				return m_curtainController;
			}
			set
			{
				m_curtainController = value;
			}
		}

		protected PlacementUI PlacementUI => m_placementUI;

		protected UIMapSelector UIMapSelector => m_mapSelector;

		public Coroutine DelayedEndGameRoutine
		{
			get
			{
				return m_delayedEndGameRoutine;
			}
			set
			{
				m_delayedEndGameRoutine = value;
			}
		}

		public Team CachedCurrentWinner
		{
			get
			{
				return m_cachedCurrentWinner;
			}
			set
			{
				m_cachedCurrentWinner = value;
			}
		}

		public string CachedCurrentSubText
		{
			get
			{
				return m_cachedCurrentSubText;
			}
			set
			{
				m_cachedCurrentSubText = value;
			}
		}

		public BattleBudget BattleBudget { get; protected set; }

		public UnitPlacementBrush Brush { get; protected set; }

		public SandboxPlacementCamera PlacementCamera => m_placementCamera;

		public TaskSequencer EnterBattleStateSequencer => m_enterBattleStateSequencer;

		public TaskSequencer EnterPlacementStateSequencer => m_enterPlacementStateSequencer;

		public TeamLayouts TeamLayouts
		{
			get
			{
				return m_teamLayouts;
			}
			protected set
			{
				m_teamLayouts = value;
			}
		}

		public GameStateManager GameStateManager => m_gameStateManager;

		public GameModeService GameModeService { get; set; }

		public bool MatchOver { get; set; }

		public bool IsInFreeLook { get; protected set; }

		public CameraMovementZoomIn CameraMovementZoom => m_cameraMovementZoom;

		public WinConditionPropagator WinConditionPropagator => m_winConditions;

		public OnUnitRemovedDelegate OnUnitRemovedCallback { get; set; }

		public OnUnitSpawnedDelegate OnUnitSpawnedCallback { get; set; }

		public OnDonePlacingAllUnitsDelegate OnDonePlacingUnitsCallback { get; set; }

		public ITimeService TimeService
		{
			get
			{
				return m_timeService;
			}
			set
			{
				m_timeService = value;
			}
		}

		protected virtual bool CanUseSlowMotion => true;

		public event Action EnteredFreeLook;

		public event Action ExitedFreeLook;

		public virtual void Start()
		{
			TeamLayouts = new TeamLayouts();
			m_enterBattleStateSequencer = new TaskSequencer(GameModeService);
			m_enterBattleStateSequencer.AddTask(new TaskSequence(DisableFreeCam));
			m_enterBattleStateSequencer.AddTask(new TaskSequence(EnterFreeLookRoutine));
			m_enterPlacementStateSequencer = new TaskSequencer(GameModeService);
			m_enterPlacementStateSequencer.AddTask(new TaskSequence(RunEnterPlacementStateSequence));
			m_enterPlacementStateSequencer.AddTask(new TaskSequence(PlaceLayoutUnits));
			m_enterPlacementStateSequencer.AddTask(new TaskSequence(AllowFreeCam));
			m_gameStateManager = ServiceLocator.GetService<GameStateManager>();
			m_fixedTimeStepService = ServiceLocator.GetService<FixedTimeStepService>();
			m_placementUI = UnityEngine.Object.FindObjectOfType<PlacementUI>();
			m_mapSelector = UnityEngine.Object.FindObjectOfType<UIMapSelector>();
			m_placementCamera = UnityEngine.Object.FindObjectOfType<SandboxPlacementCamera>();
			m_cameraMovementZoom = UnityEngine.Object.FindObjectOfType<CameraMovementZoomIn>();
			m_interfaceStateManager = UnityEngine.Object.FindObjectOfType<InterfaceStateManager>();
			m_inputService = ServiceLocator.GetService<InputService>();
			m_winConditions = new WinConditionPropagator(this);
			m_delayGameOverSetting = ServiceLocator.GetService<GlobalSettingsHandler>().GetSettingsInstance("GAMEPLAY_DELAYGAMEOVER");
			m_timeService = ServiceLocator.GetService<ITimeService>();
			m_timeService.Init(GameModeService.AudioSettings.AudioMixer, GameModeService.AudioSettings.LowpassCurve);
			SceneManager.activeSceneChanged += SceneChanged;
		}

		private void SceneChanged(Scene arg0, Scene arg1)
		{
			m_enterPlacementStateSequencer.AbortSqeuence();
		}

		public void FindPlacementCamera()
		{
			m_placementCamera = UnityEngine.Object.FindObjectOfType<SandboxPlacementCamera>();
			if (m_placementCamera == null)
			{
				Debug.LogError("Unable to find SanboxPlacementCamera");
			}
		}

		public IEnumerator EnterFreeLookRoutine()
		{
			m_placementCamera.EnterFreeLook();
			yield return null;
		}

		public IEnumerator ExitFreeLookRoutine()
		{
			m_placementCamera.ExitFreeLook();
			yield return null;
		}

		private IEnumerator AllowFreeCam()
		{
			m_allowFreeCam = true;
			yield return null;
		}

		private IEnumerator DisableFreeCam()
		{
			m_allowFreeCam = false;
			yield return null;
		}

		private IEnumerator RunEnterBattleStateSequence()
		{
			yield return null;
			m_placementCamera.EnterBattleState();
		}

		public virtual void OnEnterNewScene()
		{
			m_placementUI = UnityEngine.Object.FindObjectOfType<PlacementUI>();
			m_mapSelector = UnityEngine.Object.FindObjectOfType<UIMapSelector>();
			m_placementCamera = UnityEngine.Object.FindObjectOfType<SandboxPlacementCamera>();
			m_cameraMovementZoom = UnityEngine.Object.FindObjectOfType<CameraMovementZoomIn>();
			m_interfaceStateManager = UnityEngine.Object.FindObjectOfType<InterfaceStateManager>();
			Brush.ClearBrushUnit();
			ServiceLocator.GetService<GameObjectPooling>().ClearPools();
			TimeService.Unlock();
			TimeService.UnPause();
			TimeService.SetState(1f, 0f);
			m_loseStreak = 0;
		}

		private IEnumerator RunEnterPlacementStateSequence()
		{
			if (m_mapSelector != null)
			{
				m_mapSelector.SetMapButtonsClickable(enabled: false);
			}
			ExitFreeLook();
			SandboxPlacementCamera cam = UnityEngine.Object.FindObjectOfType<SandboxPlacementCamera>();
			while (cam.IsReturning)
			{
				yield return null;
			}
			m_toggledSlowmo = false;
			m_toggledSuperSlowmo = false;
			m_toggledPause = false;
			m_supressMouseHoldRelease = false;
		}

		public void PlacementUIComponentStateActive(bool state)
		{
			InPlacementUIState = state;
		}

		private void EnterFreelook()
		{
			m_placementCamera.EnterFreeLook();
			IsInFreeLook = true;
			this.EnteredFreeLook?.Invoke();
		}

		private void ExitFreeLook()
		{
			m_placementCamera.ExitFreeLook();
			IsInFreeLook = false;
			this.ExitedFreeLook?.Invoke();
		}

		public void StartGame()
		{
			if (!UIScreenInputBlocker.BlockInput && CanStartGame())
			{
				ToggleGroupCallback toggleGroupCallback = UnityEngine.Object.FindObjectOfType<ToggleGroupCallback>();
				if (toggleGroupCallback != null)
				{
					toggleGroupCallback.TeamToggleCallback?.Invoke();
				}
				m_winConditions.OnReset();
				GameStateManager.EnterBattleState();
			}
		}

		private bool CanStartGame()
		{
			if (m_enterPlacementStateSequencer.IsRunning)
			{
				return false;
			}
			if (Brush.IsRunningPlacementRoutine)
			{
				return false;
			}
			if (TeamLayouts.TeamLayoutHasUnits(Team.Red))
			{
				return TeamLayouts.TeamLayoutHasUnits(Team.Blue);
			}
			return false;
		}

		public virtual void Update()
		{
			if (Brush != null)
			{
				Brush.Update();
			}
			PlayerActions instance = PlayerActions.Instance;
			if (instance != null)
			{
				if (GameStateManager.GameState == Landfall.TABS.GameState.GameState.BattleState)
				{
					m_winConditions.OnUpdate();
					BattleStateUpdate(instance);
				}
				else
				{
					PlacementStateUpdate(instance);
				}
			}
		}

		protected virtual void PlacementStateUpdate(PlayerActions playerActions)
		{
			if (!m_allowFreeCam || m_placementCamera == null)
			{
				return;
			}
			bool num = PlayerActions.Instance.InputType == InputType.Controller;
			bool flag = MapSettingsUI.IsOpen || UIMapSelector.IsOpen;
			bool flag2 = num && flag;
			bool blockInput = UIScreenInputBlocker.BlockInput;
			if (IsMenuOpen() || blockInput || m_placementCamera.IsReturning || flag2 || UIScreenInputBlocker.IsAnimatedMenuChangingState)
			{
				return;
			}
			if (playerActions.m_toggleFreeCam.WasPressed && InPlacementUIState && !m_placementUI.BattleRadialMenu.IsOpen && !m_placementCamera.IsReturning)
			{
				if (m_placementCamera.IsInFreeLook())
				{
					ExitFreeLook();
				}
				else
				{
					EnterFreelook();
				}
			}
			if (playerActions.m_enterExitBattle.WasReleased && !IsMenuOpen())
			{
				m_placementUI.StartBattle();
			}
		}

		public void OnEnterPossession()
		{
			if (CanUseSlowMoInCurrentGameMode())
			{
				m_toggledSlowmo = false;
				m_toggledSuperSlowmo = false;
				m_toggledPause = false;
				TimeService.SetState(1f, 0.2f);
				TimeService.Lock();
			}
			ISaveLoaderService service = ServiceLocator.GetService<ISaveLoaderService>();
			if (!(CampaignPlayerDataHolder.SelectedCampaign == null))
			{
				DatabaseID gUID = CampaignPlayerDataHolder.SelectedCampaign.Entity.GUID;
				service.ClearNonPossessedCampaign(gUID);
			}
		}

		public void OnLeavePossession()
		{
			_ = GameModeService.CurrentGameMode;
			if (CanUseSlowMoInCurrentGameMode() && !MatchOver)
			{
				TimeService.Unlock();
			}
		}

		private void TimeButtonChecks(PlayerActions playerActions)
		{
			_ = GameModeService.CurrentGameMode;
			if (TimeService.IsPaused() || m_inputService.CurrentState == InputService.InputState.UI || !CanUseSlowMotion || !CanUseSlowMoInCurrentGameMode())
			{
				return;
			}
			bool wasPressed = playerActions.m_slowMotion.WasPressed;
			bool wasReleased = playerActions.m_slowMotion.WasReleased;
			if (wasPressed)
			{
				m_supressMouseHoldRelease = false;
				TimeService.SetState(0.1f, 0.2f);
				m_toggledSlowmo = false;
				m_toggledSuperSlowmo = false;
				m_toggledPause = false;
			}
			else if (wasReleased)
			{
				if (m_supressMouseHoldRelease)
				{
					m_supressMouseHoldRelease = false;
				}
				else
				{
					m_toggledSlowmo = false;
					m_toggledSuperSlowmo = false;
					m_toggledPause = false;
					TimeService.SetState(1f, 0.2f);
				}
			}
			bool wasPressed2 = playerActions.m_toggleSuperSlow.WasPressed;
			bool wasPressed3 = playerActions.m_toggleTime.WasPressed;
			bool wasPressed4 = playerActions.m_toggleSlowMotion.WasPressed;
			if (!(wasPressed2 || wasPressed3 || wasPressed4))
			{
				return;
			}
			m_supressMouseHoldRelease = false;
			if (wasPressed3)
			{
				m_toggledSlowmo = false;
				m_toggledSuperSlowmo = false;
				m_toggledPause = !m_toggledPause;
				if (m_toggledPause)
				{
					m_supressMouseHoldRelease = true;
					TimeService.SetState(0f, 0.2f);
				}
			}
			else if (wasPressed4)
			{
				m_toggledPause = false;
				m_toggledSuperSlowmo = false;
				m_toggledSlowmo = !m_toggledSlowmo;
				if (m_toggledSlowmo)
				{
					m_supressMouseHoldRelease = true;
					TimeService.SetState(0.1f, 0.2f);
				}
			}
			else if (wasPressed2)
			{
				m_toggledPause = false;
				m_toggledSlowmo = false;
				m_toggledSuperSlowmo = !m_toggledSuperSlowmo;
				if (m_toggledSuperSlowmo)
				{
					m_supressMouseHoldRelease = true;
					TimeService.SetState(0.01f, 0.2f);
				}
			}
			if (!m_toggledPause && !m_toggledSlowmo && !m_toggledSuperSlowmo)
			{
				TimeService.SetState(1f, 0.2f);
			}
		}

		protected virtual void BattleStateUpdate(PlayerActions playerActions)
		{
			UpdateTimeChecks(playerActions);
			UpdateForEnterExitBattle(playerActions);
			UpdateForMatchOver(playerActions);
		}

		protected void UpdateTimeChecks(PlayerActions playerActions)
		{
			TimeButtonChecks(playerActions);
			m_battleTime += Time.deltaTime;
		}

		protected void UpdateForMatchOver(PlayerActions playerActions)
		{
			if (MatchOver && playerActions.m_toggleFreeCam.WasPressed && !TimeService.IsPaused() && CanPressButtonToToggleCurtainsDuringBattle() && !IsMenuOpen())
			{
				if (CurtainController.AreCurtainsClosed)
				{
					CurtainController.OpenCurtains();
				}
				else
				{
					CurtainController.PeekCurtains();
				}
			}
		}

		protected virtual void UpdateForEnterExitBattle(PlayerActions playerActions)
		{
			if (playerActions.m_enterExitBattle.WasReleased && CanPressButtonToEndBattle())
			{
				if (m_delayedEndGameRoutine != null)
				{
					GameModeService.StopCoroutine(m_delayedEndGameRoutine);
					m_delayedEndGameRoutine = null;
					m_timeService.SetState(0f, 0.2f);
					OnWinnerDecided(m_cachedCurrentWinner, m_cachedCurrentSubText);
				}
				else if (!IsMenuOpen())
				{
					RestartMatch();
				}
			}
		}

		protected virtual bool CanPressButtonToToggleCurtainsDuringBattle()
		{
			return true;
		}

		protected virtual bool CanPressButtonToEndBattle()
		{
			return true;
		}

		protected virtual void EnterPlacementMode()
		{
			PlayerActions.Instance?.m_enterExitBattle.ClearInputState();
			if (GameStateManager.GameState == Landfall.TABS.GameState.GameState.BattleState && !MatchOver)
			{
				ClearStreakInCampaign();
			}
			GameStateManager.EnterPlacementState();
		}

		public void ClearStreakInCampaign()
		{
			ISaveLoaderService service = ServiceLocator.GetService<ISaveLoaderService>();
			if (!(CampaignPlayerDataHolder.SelectedCampaign == null) && !(GetType() != typeof(CampaignGameMode)))
			{
				DatabaseID gUID = CampaignPlayerDataHolder.SelectedCampaign.Entity.GUID;
				service.ClearWinStreak(gUID);
			}
		}

		public virtual bool IsMenuOpen()
		{
			if (m_interfaceStateManager == null)
			{
				return true;
			}
			return !m_interfaceStateManager.IsDefaultState;
		}

		public virtual void Destroy()
		{
			GameModeService.StopAllCoroutines();
		}

		public virtual void OnClearedTeam(Team team)
		{
			TeamSystem existingManager = World.Active.GetExistingManager<TeamSystem>();
			if (existingManager == null)
			{
				return;
			}
			List<Unit> teamUnits = existingManager.GetTeamUnits(team);
			if (teamUnits == null)
			{
				return;
			}
			Unit[] array = teamUnits.ToArray();
			TeamLayouts.ClearTeamLayout(team);
			foreach (Unit unit in array)
			{
				if (!unit.CampaignUnit)
				{
					Brush.RemoveUnitInternal(unit, unit.Team);
				}
			}
			BattleBudget.ResetBudget(team);
		}

		private IEnumerator PlaceLayoutUnits()
		{
			ResetMatchVariables();
			BattleBudget.ResetBudgetAll();
			switch (m_fixedTimeStepService.CurrentFixedTimeStep)
			{
			case FixedTimeStepService.FixedTimeStep.SixtyUpdates:
				Time.maximumDeltaTime = 1f / 60f;
				break;
			case FixedTimeStepService.FixedTimeStep.ThirtyUpdates:
				Time.maximumDeltaTime = 1f / 30f;
				break;
			default:
				Debug.LogError("This FixedTimeStep has not been handled. This should not happen.Defaulting to 60 fixed updates per second.");
				Time.maximumDeltaTime = 1f / 60f;
				break;
			}
			Brush?.OnEnterPlacementState();
			TABSCampaignLevelAsset.TABSLayoutUnit[] layout = m_teamLayouts.GetTeamLayout(Team.Red).ToArray();
			TABSCampaignLevelAsset.TABSLayoutUnit[] layout2 = m_teamLayouts.GetTeamLayout(Team.Blue).ToArray();
			m_teamLayouts.ClearTeamLayout(Team.Red, clearCampaignUnits: true);
			m_teamLayouts.ClearTeamLayout(Team.Blue, clearCampaignUnits: true);
			World.Active.GetExistingManager<TeamSystem>().RemoveAll();
			Coroutine coroutine = Brush?.PlaceLayout(layout, Team.Red, delegate
			{
				DonePlacingUnits(Team.Red);
			});
			Coroutine routineB = Brush?.PlaceLayout(layout2, Team.Blue, delegate
			{
				DonePlacingUnits(Team.Blue);
			});
			yield return coroutine;
			yield return routineB;
			yield return null;
		}

		private void DonePlacingUnits(Team team)
		{
			switch (team)
			{
			case Team.Red:
				m_redUnitsPlaced = true;
				break;
			case Team.Blue:
				m_blueUnitsPlaced = true;
				break;
			}
			if (m_redUnitsPlaced && m_blueUnitsPlaced)
			{
				m_redUnitsPlaced = false;
				m_blueUnitsPlaced = false;
				OnDonePlacingUnitsCallback?.Invoke();
				if (m_mapSelector != null)
				{
					m_mapSelector.SetMapButtonsClickable(enabled: true);
				}
			}
		}

		public virtual void OnEnterPlacementState()
		{
			m_enterPlacementStateSequencer.RunSequence();
		}

		public virtual void OnExitPlacementState()
		{
		}

		private void ResetMatchVariables()
		{
			UIScreenInputBlocker.DoBlockInput(open: false);
			TimeService.Unlock();
			TimeService.UnPause();
			TimeService.SetState(1f, 0f);
			MatchOver = false;
		}

		protected void RestartMatch()
		{
			if (GameModeService.CurrentGameMode.GetType() == typeof(SandboxGameMode))
			{
				ServiceLocator.GetService<RuntimeGarbageCollector>().ForceFlushGC();
				TimeService.SetState(1f, 0.2f);
			}
			EnterPlacementMode();
		}

		public virtual void OnEnterBattleState()
		{
			m_winConditions.OnReset();
			m_enterBattleStateSequencer.RunSequence();
			Time.maximumDeltaTime = 0.1f;
			Brush?.OnEnterBattleState();
			foreach (Unit allUnit in World.Active.GetExistingManager<TeamSystem>().GetAllUnits())
			{
				allUnit.UnfreezeBody();
			}
			m_battleTime = 0f;
		}

		public virtual void OnExitBattleState()
		{
			ServiceLocator.GetService<GameObjectPooling>().ClearPools();
		}

		public virtual void OnUnitSpawned(Unit unit, Team team)
		{
			RangeWeapon[] componentsInChildren = unit.GetComponentsInChildren<RangeWeapon>();
			if (componentsInChildren == null)
			{
				return;
			}
			foreach (RangeWeapon rangeWeapon in componentsInChildren)
			{
				GameObject objectToSpawn = rangeWeapon.ObjectToSpawn;
				if (objectToSpawn != null)
				{
					if (objectToSpawn.GetComponent<PooledProjectile>() != null)
					{
						ServiceLocator.GetService<GameObjectPooling>().GetPool(objectToSpawn, rangeWeapon.numberOfObjects * rangeWeapon.NumberOfInitialInstanceToAddToPool);
					}
				}
				else
				{
					Debug.LogError("Weapons should not have a null ObjectToSpawn. Fix this on the projectile prefab!", rangeWeapon.gameObject);
				}
			}
			OnUnitSpawnedCallback?.Invoke(unit);
		}

		public virtual void OnUnitRemoved(Unit unit, Team team)
		{
			OnUnitRemovedCallback?.Invoke(unit);
			RangeWeapon[] componentsInChildren = unit.GetComponentsInChildren<RangeWeapon>();
			if (componentsInChildren == null)
			{
				return;
			}
			foreach (RangeWeapon rangeWeapon in componentsInChildren)
			{
				if (rangeWeapon.ObjectToSpawn != null)
				{
					if (rangeWeapon.ObjectToSpawn.GetComponent<PooledProjectile>() != null)
					{
						ServiceLocator.GetService<GameObjectPooling>().GetPool(rangeWeapon.ObjectToSpawn).ClearAvailable(rangeWeapon.numberOfObjects * rangeWeapon.NumberOfInitialInstanceToAddToPool);
					}
				}
				else
				{
					Debug.LogError("Weapons should not have a null ObjectToSpawn. Fix this on the projectile prefab!", rangeWeapon.gameObject);
				}
			}
		}

		public virtual void OnUnitDied(Unit unit)
		{
			unit.GetComponent<UnitAPI>().SetIsDead();
			World.Active.GetExistingManager<TeamSystem>().TriggerUnitDeathAction(unit);
			m_winConditions.OnUnitDied(unit);
		}

		private IEnumerator RemoveAfterSeconds(GameObject go, float delay)
		{
			yield return new WaitForSeconds(delay);
			UnityEngine.Object.Destroy(go);
		}

		public virtual void OnWinnerDecided(Team winningTeam, string winSubText)
		{
			MatchOver = true;
			if (winningTeam == Team.Red)
			{
				m_loseStreak = 0;
				CheckWinAchievements();
			}
			else
			{
				ClearStreakInCampaign();
				m_loseStreak++;
				CheckLoseAchievements();
			}
		}

		public virtual void OnGUI()
		{
		}

		public virtual void DecideWinner(Team winningTeam, string subText)
		{
			if (m_delayedEndGameRoutine != null)
			{
				GameModeService.StopCoroutine(m_delayedEndGameRoutine);
				m_delayedEndGameRoutine = null;
			}
			m_delayedEndGameRoutine = GameModeService.StartCoroutine(DelayedCheckMatchOver(m_delayGameOverSetting.currentSliderValue, winningTeam, subText));
		}

		private IEnumerator DelayedCheckMatchOver(float delay, Team winningTeam, string winSubtext)
		{
			m_cachedCurrentWinner = winningTeam;
			m_cachedCurrentSubText = winSubtext;
			yield return new WaitForSecondsRealtime(delay);
			TimeService.UnPause();
			TimeService.Unlock();
			m_timeService.SetState(0f, 0.2f);
			if (IsMenuOpen() && m_interfaceStateManager != null)
			{
				EscapeMenuComponent uIComponent = m_interfaceStateManager.GetUIComponent<EscapeMenuComponent>();
				if (uIComponent != null)
				{
					uIComponent.ShowBackgroundBlur(show: false);
				}
				m_interfaceStateManager.OpenUIComponent(m_placementUI);
				m_timeService.SetState(0f, 0f);
			}
			OnWinnerDecided(winningTeam, winSubtext);
			m_delayedEndGameRoutine = null;
			TimeService.Lock();
		}

		public bool CanUseSlowMoInCurrentGameMode()
		{
			return GameModeService.CurrentGameMode.GetType() != typeof(OnlineMultiplayerGameMode);
		}

		private void CheckWinAchievements()
		{
			AchievementService service = ServiceLocator.GetService<AchievementService>();
			if (!(this is CampaignGameMode))
			{
				return;
			}
			int maxBudget = BattleBudget.GetMaxBudget(Team.Red);
			if (BattleBudget.CanAfford(Team.Red, maxBudget / 2) && !BattleBudget.IsInfinite(Team.Red))
			{
				service.UnlockAchievement("WIN_HALF_BUDGET");
			}
			if (PlacedAllUnitsInFaction(874593522))
			{
				service.UnlockAchievement("WIN_ALL_SECRET");
			}
			if (PlacedAllUnitsInFaction(1128865892))
			{
				service.UnlockAchievement("WIN_ALL_FANTASY_EVIL");
			}
			if (PlacedAllUnitsInFaction(1958682983))
			{
				service.UnlockAchievement("WIN_ALL_FANTASY_GOOD");
			}
			if (PlacedAllUnitsInFaction(673578412))
			{
				service.UnlockAchievement("WIN_ALL_LEGACY");
			}
			if (PlacedAllUnitsInFaction(769154847))
			{
				service.UnlockAchievement("WIN_ALL_WILDWEST");
			}
			if (PlacedAllUnitsInFaction(331252088))
			{
				service.UnlockAchievement("WIN_ALL_SPOOKY");
			}
			if (PlacedAllUnitsInFaction(275658898))
			{
				service.UnlockAchievement("WIN_ALL_RENAISSANCE");
			}
			if (PlacedAllUnitsInFaction(1707284660))
			{
				service.UnlockAchievement("WIN_ALL_PIRATE");
			}
			if (PlacedAllUnitsInFaction(442148333))
			{
				service.UnlockAchievement("WIN_ALL_VIKING");
			}
			if (PlacedAllUnitsInFaction(1701001489))
			{
				service.UnlockAchievement("WIN_ALL_ANCIENT");
			}
			if (PlacedAllUnitsInFaction(1616703987))
			{
				service.UnlockAchievement("WIN_ALL_MEDIEVAL");
			}
			if (PlacedAllUnitsInFaction(1314406212))
			{
				service.UnlockAchievement("WIN_ALL_FARMER");
			}
			if (PlacedAllUnitsInFaction(120876263))
			{
				service.UnlockAchievement("WIN_ALL_TRIBAL");
			}
			List<TABSCampaignLevelAsset.TABSLayoutUnit> teamLayout = TeamLayouts.GetTeamLayout(Team.Red);
			List<TABSCampaignLevelAsset.TABSLayoutUnit> teamLayout2 = TeamLayouts.GetTeamLayout(Team.Blue);
			Dictionary<DatabaseID, int> dictionary = new Dictionary<DatabaseID, int>();
			foreach (TABSCampaignLevelAsset.TABSLayoutUnit item in teamLayout)
			{
				DatabaseID gUID = item.m_unitBlueprint.Entity.GUID;
				if (dictionary.ContainsKey(gUID))
				{
					dictionary[gUID]++;
				}
				else
				{
					dictionary.Add(gUID, 1);
				}
			}
			bool flag = true;
			foreach (KeyValuePair<DatabaseID, int> item2 in dictionary)
			{
				int num = 0;
				foreach (TABSCampaignLevelAsset.TABSLayoutUnit item3 in teamLayout2)
				{
					if (item3.m_unitBlueprint.Entity.GUID == item2.Key)
					{
						num++;
					}
				}
				if (item2.Value != num)
				{
					flag = false;
					break;
				}
			}
			if (flag)
			{
				service.UnlockAchievement("WIN_EQUAL_ARMIES");
			}
			int num2 = 0;
			foreach (TABSCampaignLevelAsset.TABSLayoutUnit item4 in teamLayout)
			{
				DatabaseID gUID2 = item4.m_unitBlueprint.Entity.GUID;
				if (gUID2.m_modID == -1 && gUID2.m_ID == 380802557)
				{
					num2++;
				}
			}
			if (num2 >= 10)
			{
				service.UnlockAchievement("WIN_CLERGY");
			}
			TeamSystem orCreateManager = World.Active.GetOrCreateManager<TeamSystem>();
			bool flag2 = false;
			foreach (Unit teamUnit in orCreateManager.GetTeamUnits(Team.Red))
			{
				if (teamUnit.damageDealt > 0f)
				{
					flag2 = true;
					break;
				}
			}
			if (!flag2)
			{
				service.UnlockAchievement("WIN_NO_DAMAGE");
				service.AdvanceAchievementProgress("WIN_NO_DAMAGE_PROGRESSIVE", 1);
			}
			if (teamLayout.Count == 1)
			{
				service.AdvanceAchievementProgress("WIN_SINLGE_UNIT", 1);
			}
			int num3 = 1672334374;
			if (teamLayout.Count == 1 && teamLayout[0].m_unitBlueprint.Entity.GUID.m_ID == num3)
			{
				service.AdvanceAchievementProgress("WIN_SINLGE_REAPER", 1);
			}
			if (!CampaignPlayerDataHolder.PlayingSingleBattles)
			{
				int num4 = 1470355244;
				if (teamLayout.Count == 1 && teamLayout[0].m_unitBlueprint.Entity.GUID.m_ID == num4 && m_battleTime < 10f)
				{
					service.UnlockAchievement("WIN_TANK");
				}
				int num5 = 168714864;
				if (teamLayout.Count == 1 && teamLayout[0].m_unitBlueprint.Entity.GUID.m_ID == num5)
				{
					service.AdvanceAchievementProgress("WIN_SINGLE_MUSKETEER", 1);
				}
			}
			bool PlacedAllUnitsInFaction(int factionId)
			{
				List<TABSCampaignLevelAsset.TABSLayoutUnit> teamLayout3 = TeamLayouts.GetTeamLayout(Team.Red);
				Faction faction = ContentDatabase.Instance().GetFaction(new DatabaseID(-1, factionId));
				if (faction == null)
				{
					Debug.LogError("Faction with id: " + factionId + " could not be found");
					return false;
				}
				bool flag3 = false;
				UnitBlueprint[] units = faction.Units;
				foreach (UnitBlueprint unitBlueprint in units)
				{
					foreach (TABSCampaignLevelAsset.TABSLayoutUnit item5 in teamLayout3)
					{
						if (item5.m_unitBlueprint.Entity.GUID == unitBlueprint.Entity.GUID)
						{
							flag3 = true;
							break;
						}
					}
					if (!flag3)
					{
						return false;
					}
					flag3 = false;
				}
				return true;
			}
		}

		private void CheckLoseAchievements()
		{
			AchievementService service = ServiceLocator.GetService<AchievementService>();
			if (this is CampaignGameMode && m_loseStreak >= 10)
			{
				service.UnlockAchievement("LOSE_STREAK");
			}
		}
	}
}
