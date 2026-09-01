using System;
using Landfall.TABS.Budget;
using Landfall.TABS.Budget.Wallets;
using Landfall.TABS.RuntimeCleanup;
using Landfall.TABS.UnitPlacement;
using Landfall.TABS.Workshop;
using UnityEngine;

namespace Landfall.TABS.GameMode
{
	public class CampaignGameMode : BaseGameMode
	{
		private TABSCampaignLevelAsset m_lastLoadedLevel;

		private Coroutine m_checkGameOverRoutine;

		private bool m_playerWonMatch;

		public override void Start()
		{
			base.Start();
			base.BattleBudget = new BattleBudget();
			base.BattleBudget.InitializeWithWalletType<CampaignWallet>();
			base.Brush = new UnitPlacementBrush(base.GameModeService);
			base.Brush.InitializeBrushWithType<BrushBehaviourCampaign>();
		}

		public override void OnEnterPlacementState()
		{
			if (m_checkGameOverRoutine != null)
			{
				base.GameModeService.StopCoroutine(m_checkGameOverRoutine);
				m_checkGameOverRoutine = null;
			}
			bool flag = false;
			if (base.MatchOver)
			{
				if (m_playerWonMatch && CampaignPlayerDataHolder.AutoLoadNextLevel)
				{
					base.CurtainController.CloseCurtains("", "");
					VerticalCurtainController curtainController = base.CurtainController;
					curtainController.OnAnimationDoneCallback = (VerticalCurtainController.OnAnimationDoneDelegate)Delegate.Combine(curtainController.OnAnimationDoneCallback, new VerticalCurtainController.OnAnimationDoneDelegate(CurtainAnimationDone));
				}
				else
				{
					base.CurtainController.OpenCurtains();
					base.TimeService.SetState(1f, 0.2f);
				}
				flag = m_playerWonMatch;
				base.MatchOver = false;
				m_playerWonMatch = false;
			}
			if (!flag || !CampaignPlayerDataHolder.AutoLoadNextLevel)
			{
				TABSCampaignLevelAsset currentLevel = CampaignPlayerDataHolder.GetCurrentLevel();
				Debug.Log("Level Instance ID: " + currentLevel.GetInstanceID());
				if (m_lastLoadedLevel == null || m_lastLoadedLevel != currentLevel)
				{
					LoadNewLevel(currentLevel);
				}
				base.OnEnterPlacementState();
			}
			ServiceLocator.GetService<RuntimeGarbageCollector>().ForceFlushGC();
		}

		public override void OnEnterBattleState()
		{
			if (base.CameraMovementZoom != null)
			{
				base.CameraMovementZoom.GoToCenter();
			}
			base.OnEnterBattleState();
		}

		protected void LoadNewLevel(TABSCampaignLevelAsset level)
		{
			base.BattleBudget.SetBudget(level.m_budget);
			base.BattleBudget.ResetBudgetAll();
			base.TeamLayouts.ClearTeamLayout(Team.Red, clearCampaignUnits: true);
			base.TeamLayouts.ClearTeamLayout(Team.Blue, clearCampaignUnits: true);
			TABSCampaignLevelAsset.TABSLayoutUnit[] redUnits = level.RedUnits;
			for (int i = 0; i < redUnits.Length; i++)
			{
				redUnits[i].CampaignUnit = true;
			}
			redUnits = level.BlueUnits;
			for (int i = 0; i < redUnits.Length; i++)
			{
				redUnits[i].CampaignUnit = true;
			}
			SceneSettings.SerializedSettings = level.SceneSettings;
			CampaignHandler.ApplySceneData(level, campaign: true);
			m_lastLoadedLevel = level;
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

		public override void OnEnterNewScene()
		{
			base.OnEnterNewScene();
			if (CampaignHandler.LastLoadedLevel != null)
			{
				CampaignHandler.LoadLevel(CampaignHandler.LastLoadedLevel, campaign: true);
			}
		}

		public override void OnWinnerDecided(Team winningTeam, string winSubText)
		{
			base.OnWinnerDecided(winningTeam, winSubText);
			bool num = winningTeam == Team.Red;
			string headerText = (num ? "LABEL_VICTORY_TITLE" : "LABEL_DEFEAT_TITLE");
			if (num)
			{
				m_playerWonMatch = true;
			}
			if (num && CampaignPlayerDataHolder.AutoLoadNextLevel)
			{
				CampaignPlayerDataHolder.CompletedCurrentLevel();
			}
			base.CurtainController.PeekCurtains(headerText, winSubText);
			TABSAnalytics.CampaignBattleEnded(num, CampaignPlayerDataHolder.GetCurrentLevel());
		}

		private void CurtainAnimationDone()
		{
			VerticalCurtainController curtainController = base.CurtainController;
			curtainController.OnAnimationDoneCallback = (VerticalCurtainController.OnAnimationDoneDelegate)Delegate.Remove(curtainController.OnAnimationDoneCallback, new VerticalCurtainController.OnAnimationDoneDelegate(CurtainAnimationDone));
			ServiceLocator.GetService<RuntimeGarbageCollector>().ForceFlushGC();
			base.TimeService.SetState(1f, 0.2f);
			CampaignPlayerDataHolder.LoadNextCampaignLevel();
		}
	}
}
