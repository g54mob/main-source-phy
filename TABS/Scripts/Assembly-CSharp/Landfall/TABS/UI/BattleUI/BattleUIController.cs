using System.Collections.Generic;
using Landfall.TABS.GameMode;
using Landfall.TABS.GameState;
using Landfall.TABS.WinConditions;
using UnityEngine;

namespace Landfall.TABS.UI.BattleUI
{
	public class BattleUIController : GameStateListener
	{
		[SerializeField]
		private WinConditionTimerUI m_winConditionTimerUi;

		private TimeLimitWinCondition m_timeLimitWinCondition;

		private CodeAnimation m_codeAnimation;

		private bool m_MatchOver;

		public WinConditionTimerUI WinConditionTimerUI => m_winConditionTimerUi;

		protected override void Awake()
		{
			base.Awake();
			m_codeAnimation = m_winConditionTimerUi.GetComponent<CodeAnimation>();
		}

		private void OnGameEnded()
		{
			if (!m_MatchOver)
			{
				m_MatchOver = true;
				if (m_codeAnimation.currentState == CodeAnimationInstance.AnimationUse.In)
				{
					m_codeAnimation.PlayOut();
				}
			}
		}

		public override void OnEnterPlacementState()
		{
			if (m_codeAnimation.currentState == CodeAnimationInstance.AnimationUse.In)
			{
				m_codeAnimation.PlayOut();
			}
			m_MatchOver = false;
		}

		public override void OnEnterBattleState()
		{
			GameModeService service = ServiceLocator.GetService<GameModeService>();
			List<TimeLimitWinCondition> list = new List<TimeLimitWinCondition>();
			WinCondition[] winConditionsForTeam = service.CurrentGameMode.WinConditionPropagator.GetWinConditionsForTeam(Team.Red);
			WinCondition[] winConditionsForTeam2 = service.CurrentGameMode.WinConditionPropagator.GetWinConditionsForTeam(Team.Blue);
			foreach (WinCondition winCondition in winConditionsForTeam2)
			{
				if (winCondition.GetType() == typeof(TimeLimitWinCondition))
				{
					list.Add((TimeLimitWinCondition)winCondition);
				}
			}
			winConditionsForTeam2 = winConditionsForTeam;
			foreach (WinCondition winCondition2 in winConditionsForTeam2)
			{
				if (winCondition2.GetType() == typeof(TimeLimitWinCondition))
				{
					list.Add((TimeLimitWinCondition)winCondition2);
				}
			}
			float num = float.MaxValue;
			int num2 = -1;
			for (int j = 0; j < list.Count; j++)
			{
				TimeLimitWinCondition timeLimitWinCondition = list[j];
				if (timeLimitWinCondition.SecondsLeft < num)
				{
					num2 = j;
					num = timeLimitWinCondition.SecondsLeft;
				}
			}
			if (num2 == -1)
			{
				m_timeLimitWinCondition = null;
				return;
			}
			m_timeLimitWinCondition = list[num2];
			SettingsInstance settingsInstance = ServiceLocator.GetService<GlobalSettingsHandler>().GetSettingsInstance("GAMEPLAY_FLIP_COLORS");
			Team team = m_timeLimitWinCondition.OwningTeam;
			switch (team)
			{
			case Team.Red:
				team = ((settingsInstance.currentValue != 0) ? Team.Blue : Team.Red);
				break;
			case Team.Blue:
				team = ((settingsInstance.currentValue == 0) ? Team.Blue : Team.Red);
				break;
			}
			m_winConditionTimerUi.SetTeamText(team);
			m_codeAnimation.PlayIn();
		}

		public override void OnMatchOver()
		{
			base.OnMatchOver();
			if (m_codeAnimation.currentState == CodeAnimationInstance.AnimationUse.In)
			{
				m_codeAnimation.PlayOut();
			}
		}

		private void Update()
		{
			if (m_timeLimitWinCondition != null)
			{
				m_winConditionTimerUi.UpdateTime(m_timeLimitWinCondition.SecondsLeft);
			}
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
		}
	}
}
