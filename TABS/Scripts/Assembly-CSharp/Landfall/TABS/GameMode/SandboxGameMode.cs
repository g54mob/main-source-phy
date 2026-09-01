using System.Collections.Generic;
using Landfall.TABS.AI.Systems;
using Landfall.TABS.Budget;
using Landfall.TABS.Budget.Wallets;
using Landfall.TABS.RuntimeCleanup;
using Landfall.TABS.UnitPlacement;
using Landfall.TABS.Workshop;
using Unity.Entities;
using UnityEngine;

namespace Landfall.TABS.GameMode
{
	public class SandboxGameMode : BaseGameMode
	{
		private SettingsInstance m_flipColorsSetting;

		private TeamSystem m_teamSystem;

		private Coroutine m_checkGameOverRoutine;

		public override void Start()
		{
			base.BattleBudget = new BattleBudget();
			base.BattleBudget.InitializeWithWalletType<SandboxWallet>();
			base.Brush = new UnitPlacementBrush(base.GameModeService);
			base.Brush.InitializeBrushWithType<BrushBehaviourSandbox>();
			m_flipColorsSetting = ServiceLocator.GetService<GlobalSettingsHandler>().GetSettingsInstance("GAMEPLAY_FLIP_COLORS");
			m_teamSystem = World.Active.GetOrCreateManager<TeamSystem>();
			if (Object.FindObjectOfType<SandboxUIManager>() == null)
			{
				new GameObject("SandboxUIManager").AddComponent<SandboxUIManager>();
			}
			base.Start();
		}

		public override void OnEnterNewScene()
		{
			base.OnEnterNewScene();
			if (CampaignHandler.LastLoadedLevel != null)
			{
				CampaignHandler.LoadLevel(CampaignHandler.LastLoadedLevel, campaign: true);
				return;
			}
			base.TeamLayouts.ClearTeamLayout(Team.Red);
			base.TeamLayouts.ClearTeamLayout(Team.Blue);
		}

		public override void OnEnterPlacementState()
		{
			if (m_checkGameOverRoutine != null)
			{
				base.GameModeService.StopCoroutine(m_checkGameOverRoutine);
				m_checkGameOverRoutine = null;
			}
			base.CurtainController.OpenCurtains();
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

		public override void OnClearedTeam(Team team)
		{
			TeamSystem existingManager = World.Active.GetExistingManager<TeamSystem>();
			if (existingManager == null)
			{
				return;
			}
			List<Unit> teamUnits = existingManager.GetTeamUnits(team);
			if (teamUnits != null)
			{
				Unit[] array = teamUnits.ToArray();
				base.TeamLayouts.ClearTeamLayout(team);
				for (int i = 0; i < array.Length; i++)
				{
					base.Brush.RemoveUnitInternal(array[i], array[i].Team);
				}
				base.BattleBudget.ResetBudget(team);
			}
		}
	}
}
