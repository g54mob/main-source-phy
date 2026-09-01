using Landfall.TABS.GameMode;

namespace Landfall.TABS.WinConditions
{
	[WinConditionID("WIN_CON_DEFEAT_TITLE", true)]
	public class LastTeamStandingWinCondition : WinCondition
	{
		public override string GetWinDescription()
		{
			_ = string.Empty;
			string singlePhrase = Localizer.GetSinglePhrase("LABEL_TEAM");
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
					base.GameMode.DecideWinner(Team.Red, GetWinDescription());
				}
			}
			else if (base.TeamSystem.GetWinConditionTeamUnits(Team.Red).Count == 0)
			{
				base.GameMode.DecideWinner(Team.Blue, GetWinDescription());
			}
		}

		public override ReferenceRequest<Unit> GetUnitToKill()
		{
			return null;
		}

		public override void OnUpdate()
		{
		}

		public override string GetDescription(out string[] args)
		{
			args = null;
			return "WIN_CON_DEFEAT_DESC";
		}

		public override string GetDescriptionForListItem(out string[] args)
		{
			args = null;
			return "WIN_CON_DEFEAT_DESC_LIST";
		}

		public override void Reset()
		{
		}

		public override string GetBattleDescription(bool invertDescription)
		{
			if (invertDescription)
			{
				return string.Empty;
			}
			Team team = ((base.OwningTeam == Team.Red) ? Team.Blue : Team.Red);
			string singlePhrase = Localizer.GetSinglePhrase("LABEL_DEFEAT");
			string singlePhrase2 = Localizer.GetSinglePhrase("LABEL_TEAM");
			return GetWhiteColor() + singlePhrase + " " + GetColoredText(team) + GetTeamText(team) + " " + GetWhiteColor() + singlePhrase2;
		}
	}
}
