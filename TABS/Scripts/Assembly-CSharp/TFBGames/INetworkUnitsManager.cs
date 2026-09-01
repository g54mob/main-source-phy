using Landfall.TABS;
using UnityEngine;

namespace TFBGames
{
	public interface INetworkUnitsManager : IService
	{
		Unit GetUnit(ulong networkId);

		Unit GetUnitBySmallNetworkId(ushort smallNetworkId);

		Unit GetUnitByInstanceId(int instanceId);

		Unit GetUnitByRemoteInstanceId(int remoteInstanceId);

		Unit GetUnitInPool(int unitId, int modId, Team team, UnitPoolInfo poolInfo, out bool hasError);

		int GetNetworkUnitsCount();

		void DestroyAllUnits();

		void ClientSendFailedToSpawnUnitEvent(int remoteInstanceId);

		void ClientSendFailedToLinkUnitEvent(int remoteInstanceId, Team remoteTeam);

		void ServerSendSpawnUnitFromPoolEvent(Unit unit, UnitSpawnSource spawnSource, Vector3 spawnPosition, ushort copyOfSmallNetworkId);

		void NonOwnerSendUnitIdsEvent(int instanceId, int remoteInstanceId);

		void SendFailedToLinkPooledUnitEvent(int remoteInstanceId, UnitPoolInfo poolInfo);

		void OnEnterPossession(Unit unit);

		void OnExitPossession(Unit unit);

		void OnNetworkUnitInBattleScene(NetworkUnit networkUnit);
	}
}
