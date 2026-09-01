using Landfall.TABS.GameMode;
using Landfall.TABS.UI;
using Landfall.TABS.UI.UIGroups.Attributes;
using Newtonsoft.Json;
using UnityEngine;

namespace Landfall.TABS.WinConditions
{
	[WinConditionID("WIN_CON_DEFEAT_UNIT_TITLE", false)]
	public class MustKillUnitWinCondition : WinCondition
	{
		[Title("To Defeat")]
		[LockReferenceToTeam(TeamLock.Enemy)]
		[SerializeField]
		[JsonProperty]
		private ReferenceRequest<Unit> m_toKill;

		public ReferenceRequest<Unit> UnitToKill
		{
			get
			{
				return m_toKill;
			}
			set
			{
				m_toKill = value;
			}
		}

		public override string GetDescription(out string[] args)
		{
			args = null;
			return "WIN_CON_DEFEAT_UNIT_DESC";
		}

		public override string GetBattleDescription(bool invertDescription)
		{
			Team team = ((base.OwningTeam == Team.Red) ? Team.Blue : Team.Red);
			Unit referenceTarget = ServiceLocator.GetService<RuntimeReferenceService>().GetReferenceTarget<Unit>(m_toKill);
			if (referenceTarget == null)
			{
				return string.Empty;
			}
			UnitBlueprint unitBlueprint = referenceTarget.unitBlueprint;
			string singlePhrase = Localizer.GetSinglePhrase("LABEL_DEFEAT");
			string singlePhrase2 = Localizer.GetSinglePhrase("LABEL_PROTECT");
			if (!invertDescription)
			{
				return GetWhiteColor() + singlePhrase + " " + GetColoredText(team) + Localizer.GetSinglePhrase(unitBlueprint.Name);
			}
			return GetWhiteColor() + singlePhrase2 + " " + GetColoredText(team) + Localizer.GetSinglePhrase(unitBlueprint.Name);
		}

		public override string GetDescriptionForListItem(out string[] args)
		{
			Unit referenceTarget = ServiceLocator.GetService<RuntimeReferenceService>().GetReferenceTarget<Unit>(m_toKill);
			if (referenceTarget == null)
			{
				args = null;
				return string.Empty;
			}
			UnitBlueprint unitBlueprint = referenceTarget.unitBlueprint;
			string text = "$" + unitBlueprint.Name;
			args = new string[1] { text };
			return "WIN_CON_DEFEAT_UNIT_DESC_LIST";
		}

		public override string GetWinDescription()
		{
			Team team = ((base.OwningTeam == Team.Red) ? Team.Blue : Team.Red);
			Unit referenceTarget = ServiceLocator.GetService<RuntimeReferenceService>().GetReferenceTarget<Unit>(m_toKill);
			if (referenceTarget == null)
			{
				return string.Empty;
			}
			UnitBlueprint unitBlueprint = referenceTarget.unitBlueprint;
			if (base.GameMode.GetType() == typeof(CampaignGameMode) && team == Team.Red)
			{
				string singlePhrase = Localizer.GetSinglePhrase("LABEL_WAS_DEFEATED");
				return GetColoredText(team) + Localizer.GetSinglePhrase(unitBlueprint.Name) + " " + GetWhiteColor() + singlePhrase;
			}
			string singlePhrase2 = Localizer.GetSinglePhrase("LABEL_DEFEATED");
			return GetWhiteColor() + singlePhrase2 + " " + GetColoredText(team) + Localizer.GetSinglePhrase(unitBlueprint.Name);
		}

		public override ReferenceRequest<Unit> GetUnitToKill()
		{
			return m_toKill;
		}

		public override void OnUnitDied(Unit unit)
		{
			if (m_toKill == null)
			{
				Debug.LogError("Must Defeat Unit condition missing ToKill reference!");
			}
			else if (unit.RuntimeReference != null && unit.RuntimeReference.Guid == m_toKill.Guid)
			{
				base.GameMode.DecideWinner(base.OwningTeam, GetWinDescription());
			}
		}

		public override void OnRemovedFromgGUI()
		{
			Object.FindObjectOfType<ReferenceRenderer>().RemoveIconedUnit(m_toKill);
			if (m_toKill != null)
			{
				m_toKill.Release();
			}
		}

		public override void OnUpdate()
		{
		}

		public override void Reset()
		{
		}
	}
}
