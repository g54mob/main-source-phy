using System.Collections.Generic;
using Landfall.TABS.WinConditions;
using Landfall.TABS.Workshop;
using UnityEngine;

namespace Landfall.TABS.UnitPlacement
{
	public class TeamLayouts
	{
		private Dictionary<Team, List<TABSCampaignLevelAsset.TABSLayoutUnit>> m_layouts = new Dictionary<Team, List<TABSCampaignLevelAsset.TABSLayoutUnit>>();

		public void AddUnitToLayout(Team team, UnitBlueprint blueprint, Unit unit, Vector3 worldPosition, bool isCampaignUnit)
		{
			TABSCampaignLevelAsset.TABSLayoutUnit item = new TABSCampaignLevelAsset.TABSLayoutUnit
			{
				RuntimeReference = unit.RuntimeReference,
				m_position = worldPosition,
				Rotation = unit.transform.rotation.eulerAngles.y,
				m_unitBlueprint = blueprint,
				CampaignUnit = isCampaignUnit
			};
			GetOrCreateTeamContainer(team).Add(item);
		}

		public void SetTeamLayout(Team team, TABSCampaignLevelAsset.TABSLayoutUnit[] layout)
		{
			ClearTeamLayout(team, clearCampaignUnits: true);
			List<TABSCampaignLevelAsset.TABSLayoutUnit> orCreateTeamContainer = GetOrCreateTeamContainer(team);
			foreach (TABSCampaignLevelAsset.TABSLayoutUnit item in layout)
			{
				orCreateTeamContainer.Add(item);
			}
		}

		public void RemoveUnitFromLayout(Team team, RuntimeReference reference)
		{
			if (reference == null)
			{
				Debug.LogError("Something went wrong when trying to remove unit from team: " + team);
				return;
			}
			List<TABSCampaignLevelAsset.TABSLayoutUnit> orCreateTeamContainer = GetOrCreateTeamContainer(team);
			for (int i = 0; i < orCreateTeamContainer.Count; i++)
			{
				TABSCampaignLevelAsset.TABSLayoutUnit tABSLayoutUnit = orCreateTeamContainer[i];
				if (tABSLayoutUnit.RuntimeReference.Guid == reference.Guid)
				{
					orCreateTeamContainer.Remove(tABSLayoutUnit);
					break;
				}
			}
		}

		private List<TABSCampaignLevelAsset.TABSLayoutUnit> GetOrCreateTeamContainer(Team team)
		{
			CreateContainersIfMissing(team);
			return m_layouts[team];
		}

		private void CreateContainersIfMissing(Team team)
		{
			if (!m_layouts.ContainsKey(team))
			{
				List<TABSCampaignLevelAsset.TABSLayoutUnit> value = new List<TABSCampaignLevelAsset.TABSLayoutUnit>();
				m_layouts.Add(team, value);
			}
		}

		public void ClearTeamLayout(Team team, bool clearCampaignUnits = false)
		{
			List<TABSCampaignLevelAsset.TABSLayoutUnit> orCreateTeamContainer = GetOrCreateTeamContainer(team);
			for (int num = orCreateTeamContainer.Count - 1; num >= 0; num--)
			{
				TABSCampaignLevelAsset.TABSLayoutUnit tABSLayoutUnit = orCreateTeamContainer[num];
				if (clearCampaignUnits || !tABSLayoutUnit.CampaignUnit)
				{
					orCreateTeamContainer.RemoveAt(num);
				}
			}
		}

		public bool TeamLayoutHasUnits(Team team)
		{
			return GetOrCreateTeamContainer(team).Count > 0;
		}

		public List<TABSCampaignLevelAsset.TABSLayoutUnit> GetTeamLayout(Team team)
		{
			return GetOrCreateTeamContainer(team);
		}
	}
}
