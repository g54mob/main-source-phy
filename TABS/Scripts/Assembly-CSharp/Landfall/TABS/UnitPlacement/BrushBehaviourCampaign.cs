using Landfall.TABS.Workshop;
using UnityEngine;

namespace Landfall.TABS.UnitPlacement
{
	public class BrushBehaviourCampaign : BrushBehaviourBase
	{
		public override bool CanRemove(Unit unit)
		{
			if (unit.Team != Team.Red)
			{
				return false;
			}
			if (unit.CampaignUnit)
			{
				return false;
			}
			return true;
		}

		public override void HoveringStaticSceneObjects(GameObject go, Vector3 worldPosition, Vector3 surfaceNormal)
		{
			if (GetTeamAreaAtPosition(worldPosition, out var _) == Team.Blue)
			{
				base.UnitPlacementBrush.Cursor.SetCursorState(PlacementCursorState.DenyPlacement);
			}
			else
			{
				base.HoveringStaticSceneObjects(go, worldPosition, surfaceNormal);
			}
		}

		public override void PrimaryActionOnObject(GameObject go, Vector3 worldPosition)
		{
			if (!(go == null) && GetTeamAreaAtPosition(worldPosition, out var _) != Team.Blue)
			{
				base.PrimaryActionOnObject(go, worldPosition);
			}
		}

		public override void SecondaryActionOnObject(GameObject go, Vector3 worldPosition)
		{
			Unit component = go.GetComponent<Unit>();
			if (GetTeamAreaAtPosition(worldPosition, out var _) != Team.Blue && !component.CampaignUnit)
			{
				base.SecondaryActionOnObject(go, worldPosition);
			}
		}

		public override void PlaceUnit(UnitBlueprint blueprint, Team team, Vector3 worldPosition, Quaternion rotation, bool isCampaignUnit, bool forMartianPlayer = false, int martianInstanceId = 0)
		{
			m_unitPlacementBrush.PlaceUnitInternal(blueprint, team, worldPosition, rotation, addToLayout: true, null, !isCampaignUnit, isCampaignUnit, checkUserPlacementMaxUnits: true);
		}

		public override void PlaceLayoutUnit(TABSCampaignLevelAsset.TABSLayoutUnit unit, Team team, Quaternion unitRotation)
		{
			m_unitPlacementBrush.PlaceLayoutUnitInternal(unit, team, addToLayout: true, unitRotation, !unit.CampaignUnit);
		}

		public override void RemoveUnit(Unit unit, bool forMartian = false)
		{
			if (!unit.CampaignUnit)
			{
				m_unitPlacementBrush.RemoveUnitInternal(unit, unit.Team);
			}
		}
	}
}
