using Landfall.TABS.UI.UIGroups.Attributes;
using Landfall.TABS.UI.Widgets.Fields;
using Landfall.TABS.Workshop;
using UnityEngine;

namespace Landfall.TABS.UnitPlacement
{
	public class BrushBehaviourReferencePicker : BrushBehaviourBase
	{
		private UIReferenceField m_initiatedFromField;

		private TeamLock m_teamLock = TeamLock.Allow_All;

		private Team m_owningTeam;

		private bool m_finishedPicking;

		public UIReferenceField InitiatedFromField
		{
			get
			{
				return m_initiatedFromField;
			}
			set
			{
				m_initiatedFromField = value;
			}
		}

		public override void Initialize()
		{
		}

		public override bool CanRemove(Unit unit)
		{
			return true;
		}

		public void LockToTeam(Team owningTeam, TeamLock teamLock)
		{
			m_owningTeam = owningTeam;
			m_teamLock = teamLock;
		}

		public override void StartHoverDynamicObjects(GameObject go, Vector3 worldPosition)
		{
			base.StartHoverDynamicObjects(go, worldPosition);
			Unit component = go.GetComponent<Unit>();
			if (!(component != null))
			{
				return;
			}
			if (m_teamLock == TeamLock.Enemy)
			{
				if (m_owningTeam == component.Team)
				{
					component.RemoveHighlight();
				}
			}
			else if (m_teamLock != TeamLock.Friendly && m_owningTeam == component.Team)
			{
				component.RemoveHighlight();
			}
		}

		public override void PrimaryActionOnObject(GameObject go, Vector3 worldPosition)
		{
			if (m_finishedPicking || !(m_unitPlacementBrush.HoveredUnit != null) || !(m_initiatedFromField != null))
			{
				return;
			}
			Unit component = m_unitPlacementBrush.HoveredUnit.GetComponent<Unit>();
			if (m_teamLock == TeamLock.Enemy)
			{
				if (m_owningTeam != component.Team)
				{
					m_initiatedFromField.ReferencePickFinished(component.RuntimeReference);
					m_finishedPicking = true;
				}
			}
			else if (m_teamLock == TeamLock.Friendly)
			{
				if (m_owningTeam == component.Team)
				{
					m_initiatedFromField.ReferencePickFinished(component.RuntimeReference);
					m_finishedPicking = true;
				}
			}
			else
			{
				m_initiatedFromField.ReferencePickFinished(component.RuntimeReference);
				m_finishedPicking = true;
			}
		}

		public override void PlaceUnit(UnitBlueprint blueprint, Team team, Vector3 worldPosition, Quaternion rotation, bool isCampaignUnit, bool forMartianPlayer = false, int martianInstanceId = 0)
		{
		}

		public override void PlaceLayoutUnit(TABSCampaignLevelAsset.TABSLayoutUnit unit, Team team, Quaternion unitRotation)
		{
		}

		public override void RemoveUnit(Unit unit, bool forMartian = false)
		{
		}
	}
}
