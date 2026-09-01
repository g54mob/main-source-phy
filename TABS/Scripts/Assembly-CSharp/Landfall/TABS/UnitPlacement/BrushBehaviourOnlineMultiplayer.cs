using Landfall.TABS.Workshop;
using TFBGames;
using UnityEngine;

namespace Landfall.TABS.UnitPlacement
{
	public class BrushBehaviourOnlineMultiplayer : BrushBehaviourBase
	{
		private NetworkBattleController m_networkBattle;

		private INetworkUnitsManager m_networkUnits;

		private INetworkService m_networkService;

		public override bool CanPlace(UnitBlueprint unitToSpawn, Team team, ref Vector3 position, Quaternion rotation, bool forRemotePlayer = false)
		{
			if (CanPlayerPlaceUnits(team, forRemotePlayer))
			{
				return base.CanPlace(unitToSpawn, team, ref position, rotation, forRemotePlayer);
			}
			return false;
		}

		public override bool CanRemove(Unit unit)
		{
			return true;
		}

		public override void PlaceUnit(UnitBlueprint blueprint, Team team, Vector3 worldPosition, Quaternion rotation, bool isCampaignUnit, bool forRemotePlayer = false, int martianInstanceId = 0)
		{
			if (CanPlayerPlaceUnits(team, forRemotePlayer))
			{
				m_unitPlacementBrush.PlaceUnitInternal(blueprint, team, worldPosition, rotation, addToLayout: true, null, costsBudget: true, campaignUnit: false, !isCampaignUnit, martianInstanceId);
			}
		}

		public override void RemoveUnit(Unit unit, bool forRemotePlayer = false)
		{
			if (CanPlayerPlaceUnits(unit.Team, forRemotePlayer))
			{
				m_unitPlacementBrush.RemoveUnitInternal(unit, unit.Team);
			}
		}

		public override void PlaceLayoutUnit(TABSCampaignLevelAsset.TABSLayoutUnit unit, Team team, Quaternion unitRotation)
		{
			GetServices();
			if (m_networkService.PlayerTeam == team)
			{
				m_unitPlacementBrush.PlaceLayoutUnitInternal(unit, team, addToLayout: true, unitRotation);
			}
		}

		private void GetServices()
		{
			if (m_networkBattle == null)
			{
				m_networkBattle = ServiceLocator.GetService<NetworkBattleController>();
			}
			if (m_networkUnits == null)
			{
				m_networkUnits = ServiceLocator.GetService<INetworkUnitsManager>();
			}
			if (m_networkService == null)
			{
				m_networkService = ServiceLocator.GetService<INetworkService>();
			}
		}

		private bool CanPlayerPlaceUnits(Team team, bool forRemotePlayer)
		{
			GetServices();
			if (m_networkBattle == null)
			{
				return false;
			}
			if (m_networkBattle.IsPlayerReady(team))
			{
				return false;
			}
			return MultiplayerHelper.IsCorrectTeamToEdit(team, forRemotePlayer, m_networkBattle.ServerTeam, m_networkBattle.ClientTeam);
		}
	}
}
