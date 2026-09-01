using Landfall.TABS.AI.Systems;
using Landfall.TABS.GameMode;
using Landfall.TABS.UI.UIGroups.Attributes;
using Newtonsoft.Json;
using Unity.Entities;
using UnityEngine;

namespace Landfall.TABS.WinConditions
{
	[WinConditionID("WIN_CON_SURVIVE_TITLE", true)]
	public class TimeLimitWinCondition : WinCondition
	{
		private const float MINIMUM_TIME = 300f;

		[Title("Seconds")]
		[SerializeField]
		[JsonProperty]
		private float m_secondsToSurvive = 300f;

		private float m_secondsLeft;

		private bool m_triggered;

		private TeamSystem m_teamSystem;

		public float SecondsToSurvive
		{
			get
			{
				return m_secondsToSurvive;
			}
			set
			{
				m_secondsToSurvive = value;
			}
		}

		[JsonIgnore]
		public float SecondsLeft => m_secondsLeft;

		public override string GetDescription(out string[] args)
		{
			args = null;
			return "WIN_CON_SURVIVE_DESC";
		}

		public override string GetBattleDescription(bool inverseDescription)
		{
			string singlePhrase = Localizer.GetSinglePhrase("LABEL_SECONDS");
			string singlePhrase2 = Localizer.GetSinglePhrase("LABEL_SURVIVE");
			string singlePhrase3 = Localizer.GetSinglePhrase("LABEL_WIN_WITHIN");
			if (!inverseDescription)
			{
				return $"{GetWhiteColor()}{singlePhrase2} {GetTimerColor()}{(int)m_secondsToSurvive} {GetWhiteColor()}{singlePhrase}";
			}
			return $"{GetWhiteColor()}{singlePhrase3} {GetTimerColor()}{(int)m_secondsToSurvive} {GetWhiteColor()}{singlePhrase}";
		}

		public override string GetDescriptionForListItem(out string[] args)
		{
			args = new string[1] { ((int)m_secondsToSurvive).ToString() };
			return "WIN_CON_SURVIVE_DESC_LIST";
		}

		public override string GetWinDescription()
		{
			string singlePhrase = Localizer.GetSinglePhrase("LABEL_SECONDS");
			string singlePhrase2 = Localizer.GetSinglePhrase("LABEL_SURVIVED");
			if (base.GameMode.GetType() == typeof(CampaignGameMode) && base.OwningTeam == Team.Blue)
			{
				return $"{GetColoredText(base.OwningTeam)}{GetTeamText(base.OwningTeam)} {GetWhiteColor()}{singlePhrase2} {GetTimerColor()}{(int)m_secondsToSurvive} {GetWhiteColor()}{singlePhrase}";
			}
			return $"{GetWhiteColor()}{singlePhrase2} {GetTimerColor()}{(int)m_secondsToSurvive} {GetWhiteColor()}{singlePhrase}";
		}

		private string GetNoUnitsLeftWinDescription()
		{
			string singlePhrase = Localizer.GetSinglePhrase("LABEL_TEAM");
			_ = string.Empty;
			if (base.GameMode.GetType() == typeof(CampaignGameMode))
			{
				Team owningTeam = base.OwningTeam;
				if (owningTeam != Team.Red)
				{
					string singlePhrase2 = Localizer.GetSinglePhrase("LABEL_DEFEATED_BY");
					return GetWhiteColor() + singlePhrase2 + " " + GetColoredText(owningTeam) + GetTeamText(owningTeam) + " " + GetWhiteColor() + singlePhrase;
				}
			}
			string singlePhrase3 = Localizer.GetSinglePhrase("LABEL_DEFEATED");
			Team team = ((base.OwningTeam == Team.Red) ? Team.Blue : Team.Red);
			return GetWhiteColor() + singlePhrase3 + " " + GetColoredText(team) + GetTeamText(team) + " " + GetWhiteColor() + singlePhrase;
		}

		public override void OnUnitDied(Unit unit)
		{
			if (base.OwningTeam == Team.Red)
			{
				if (base.TeamSystem.GetWinConditionTeamUnits(Team.Blue).Count == 0)
				{
					base.GameMode.DecideWinner(Team.Red, GetNoUnitsLeftWinDescription());
				}
			}
			else if (base.TeamSystem.GetWinConditionTeamUnits(Team.Red).Count == 0)
			{
				base.GameMode.DecideWinner(Team.Blue, GetNoUnitsLeftWinDescription());
			}
		}

		public override ReferenceRequest<Unit> GetUnitToKill()
		{
			return null;
		}

		public override void OnUpdate()
		{
			if (!m_triggered)
			{
				m_secondsLeft -= Time.deltaTime;
				if (m_secondsLeft <= 0f)
				{
					m_triggered = true;
					base.GameMode.DecideWinner(base.OwningTeam, GetWinDescription());
				}
			}
		}

		public override void Reset()
		{
			m_secondsLeft = m_secondsToSurvive;
			m_triggered = false;
			m_teamSystem = World.Active.GetExistingManager<TeamSystem>();
		}
	}
}
