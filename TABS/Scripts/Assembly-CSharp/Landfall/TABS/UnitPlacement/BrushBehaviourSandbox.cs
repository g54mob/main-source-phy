using Landfall.TABS.Workshop;
using UnityEngine;

namespace Landfall.TABS.UnitPlacement
{
	public class BrushBehaviourSandbox : BrushBehaviourBase
	{
		public override bool CanRemove(Unit unit)
		{
			return true;
		}

		public override void PlaceUnit(UnitBlueprint blueprint, Team team, Vector3 worldPosition, Quaternion rotation, bool isCampaignUnit, bool forMartianPlayer = false, int martianInstanceId = 0)
		{
			m_unitPlacementBrush.PlaceUnitInternal(blueprint, team, worldPosition, rotation, addToLayout: true, null, costsBudget: true, campaignUnit: false, !isCampaignUnit);
		}

		public override void PlaceLayoutUnit(TABSCampaignLevelAsset.TABSLayoutUnit unit, Team team, Quaternion unitRotation)
		{
			m_unitPlacementBrush.PlaceLayoutUnitInternal(unit, team, addToLayout: true, unitRotation);
		}

		public override void RemoveUnit(Unit unit, bool forMartian = false)
		{
			m_unitPlacementBrush.RemoveUnitInternal(unit, unit.Team);
		}
	}
}
