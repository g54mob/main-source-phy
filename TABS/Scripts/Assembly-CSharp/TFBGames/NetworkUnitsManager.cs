using System;
using System.Collections.Generic;
using DM;
using Landfall.TABS;
using Landfall.TABS.GameMode;
using Landfall.TABS.GameState;
using Landfall.TABS.UnitPlacement;
using Landfall.TABS.WinConditions;
using Photon.Bolt;
using UdpKit;
using UnityEngine;

namespace TFBGames
{
	public class NetworkUnitsManager : GlobalEventListener, INetworkUnitsManager, IService
	{
		private const int ServerUnitInstanceIDMin = 1;

		private const int ServerUnitInstanceIDMax = 32767;

		private const int ClientUnitInstanceIDMin = -32768;

		private const int ClientUnitInstanceIDMax = -1;

		private const int ServerUnitNetworkIDMin = 1;

		private const int ServerUnitNetworkIDMax = 65535;

		private List<Unit> m_units = new List<Unit>();

		private GameStateManager m_gameStateManager;

		private BaseGameMode m_gameMode;

		private NetworkBattleController m_networkBattle;

		private UnitsSpawnMonitor m_unitsSpawnMonitor;

		private INetworkService m_networkService;

		private RuntimeReferenceService m_runtimeReference;

		private NetworkIDGenerator m_instanceIDGenerator;

		private NetworkIDGenerator m_networkIDGenerator;

		private bool m_didDisconnect;

		private NetworkBattleController BattleController
		{
			get
			{
				if (m_networkBattle == null)
				{
					m_networkBattle = ServiceLocator.GetService<NetworkBattleController>();
				}
				return m_networkBattle;
			}
		}

		private void Awake()
		{
			ServiceLocator.RegisterService((INetworkUnitsManager)this);
		}

		private void Start()
		{
			m_gameStateManager = ServiceLocator.GetService<GameStateManager>();
			m_unitsSpawnMonitor = ServiceLocator.GetService<UnitsSpawnMonitor>();
			m_networkService = ServiceLocator.GetService<INetworkService>();
			m_runtimeReference = ServiceLocator.GetService<RuntimeReferenceService>();
			m_unitsSpawnMonitor.SpawnedUnit += OnSpawnedUnit;
			m_unitsSpawnMonitor.DestroyedUnit += OnDestroyedUnit;
			m_gameMode = ServiceLocator.GetService<GameModeService>().CurrentGameMode;
			if (m_gameMode != null)
			{
				BaseGameMode gameMode = m_gameMode;
				gameMode.OnUnitRemovedCallback = (BaseGameMode.OnUnitRemovedDelegate)Delegate.Combine(gameMode.OnUnitRemovedCallback, new BaseGameMode.OnUnitRemovedDelegate(OnRemovedUnit));
			}
		}

		private void OnDestroy()
		{
			ServiceLocator.UnRegisterSerice<INetworkUnitsManager>();
			if (m_unitsSpawnMonitor != null)
			{
				m_unitsSpawnMonitor.SpawnedUnit -= OnSpawnedUnit;
				m_unitsSpawnMonitor.DestroyedUnit -= OnDestroyedUnit;
			}
			if (m_gameMode != null)
			{
				BaseGameMode gameMode = m_gameMode;
				gameMode.OnUnitRemovedCallback = (BaseGameMode.OnUnitRemovedDelegate)Delegate.Remove(gameMode.OnUnitRemovedCallback, new BaseGameMode.OnUnitRemovedDelegate(OnRemovedUnit));
			}
		}

		public void OnRegister()
		{
		}

		public void OnAwake()
		{
		}

		public void OnStart()
		{
		}

		public void OnUpdate()
		{
		}

		public void OnFixedUpdate()
		{
		}

		public void OnLateUpdate()
		{
		}

		public void UnRegister()
		{
		}

		public Unit GetUnit(ulong networkId)
		{
			int i = 0;
			for (int count = m_units.Count; i < count; i++)
			{
				Unit unit = m_units[i];
				if (unit != null && unit.NetworkId == networkId)
				{
					return unit;
				}
			}
			return null;
		}

		public Unit GetUnitBySmallNetworkId(ushort smallNetworkId)
		{
			int i = 0;
			for (int count = m_units.Count; i < count; i++)
			{
				Unit unit = m_units[i];
				if (unit != null && unit.SmallNetworkId == smallNetworkId)
				{
					return unit;
				}
			}
			return null;
		}

		public Unit GetUnitByInstanceId(int instanceId)
		{
			int i = 0;
			for (int count = m_units.Count; i < count; i++)
			{
				Unit unit = m_units[i];
				if (unit != null && unit.InstanceId == instanceId)
				{
					return unit;
				}
			}
			return null;
		}

		public Unit GetUnitByRemoteInstanceId(int remoteInstanceId)
		{
			int i = 0;
			for (int count = m_units.Count; i < count; i++)
			{
				Unit unit = m_units[i];
				if (unit != null && unit.RemoteInstanceId == remoteInstanceId)
				{
					return unit;
				}
			}
			return null;
		}

		public Unit GetUnitInPool(int unitId, int modId, Team team, UnitPoolInfo poolInfo, out bool hasError)
		{
			hasError = false;
			UnitBlueprint unitBlueprint = ContentDatabase.Instance().GetUnitBlueprint(new DatabaseID(modId, unitId));
			if (unitBlueprint == null)
			{
				hasError = true;
				Debug.LogError("Could not find Unit's blueprint.");
				return null;
			}
			int i = 0;
			for (int count = m_units.Count; i < count; i++)
			{
				Unit unit = m_units[i];
				if (unit == null)
				{
					Debug.LogError("m_units contains a null Unit. Make sure we remove all destroyed Units from the list.");
				}
				else if (unit.IsInPool && unit.NetworkId == 0L && unit.unitBlueprint == unitBlueprint && unit.Team == team && unit.PoolInfo.Value.PoolIndex == poolInfo.PoolIndex && unit.PoolInfo.Value.PoolId == poolInfo.PoolId)
				{
					return unit;
				}
			}
			return null;
		}

		public int GetNetworkUnitsCount()
		{
			int num = 0;
			foreach (BoltEntity entity in BoltNetwork.Entities)
			{
				if (entity.StateIs<IUnitState>())
				{
					num++;
				}
			}
			return num;
		}

		public void DestroyAllUnits()
		{
			if (m_units == null || m_units.Count <= 0)
			{
				return;
			}
			for (int num = m_units.Count - 1; num >= 0; num--)
			{
				Unit unit = m_units[num];
				if (unit != null && unit.gameObject != null)
				{
					UnityEngine.Object.Destroy(unit.gameObject);
				}
			}
		}

		public void ClientSendFailedToSpawnUnitEvent(int remoteInstanceId)
		{
			FailedToSpawnUnitEvent failedToSpawnUnitEvent = FailedToSpawnUnitEvent.Create(GlobalTargets.OnlyServer, ReliabilityModes.ReliableOrdered);
			failedToSpawnUnitEvent.UnitInstanceId = remoteInstanceId;
			failedToSpawnUnitEvent.Send();
		}

		public void ClientSendFailedToLinkUnitEvent(int remoteInstanceId, Team remoteTeam)
		{
			FailedToLinkUnitEvent failedToLinkUnitEvent = FailedToLinkUnitEvent.Create(GlobalTargets.OnlyServer, ReliabilityModes.ReliableOrdered);
			failedToLinkUnitEvent.UnitInstanceId = remoteInstanceId;
			failedToLinkUnitEvent.Team = (int)remoteTeam;
			failedToLinkUnitEvent.Send();
		}

		public void ServerSendSpawnUnitFromPoolEvent(Unit unit, UnitSpawnSource spawnSource, Vector3 spawnPosition, ushort copyOfSmallNetworkId)
		{
			SpawnUnitFromPoolEvent spawnUnitFromPoolEvent = SpawnUnitFromPoolEvent.Create(GlobalTargets.AllClients, ReliabilityModes.ReliableOrdered);
			spawnUnitFromPoolEvent.SpawnSource = (int)spawnSource;
			spawnUnitFromPoolEvent.UnitSmallNetworkId = unit.SmallNetworkId;
			spawnUnitFromPoolEvent.UnitSpawnPosition = spawnPosition;
			spawnUnitFromPoolEvent.CopyOfSmallNetworkId = copyOfSmallNetworkId;
			spawnUnitFromPoolEvent.Send();
		}

		public void NonOwnerSendUnitIdsEvent(int instanceId, int remoteInstanceId)
		{
			UnitIdsEvent unitIdsEvent = UnitIdsEvent.Create(GlobalTargets.Others, ReliabilityModes.ReliableOrdered);
			unitIdsEvent.UnitInstanceId = remoteInstanceId;
			unitIdsEvent.UnitRemoteInstanceId = instanceId;
			unitIdsEvent.Send();
		}

		public void SendFailedToLinkPooledUnitEvent(int remoteInstanceId, UnitPoolInfo poolInfo)
		{
			FailedToLinkPooledUnitEvent failedToLinkPooledUnitEvent = FailedToLinkPooledUnitEvent.Create(GlobalTargets.Others, ReliabilityModes.ReliableOrdered);
			failedToLinkPooledUnitEvent.UnitInstanceId = remoteInstanceId;
			failedToLinkPooledUnitEvent.PoolIndex = poolInfo.PoolIndex;
			failedToLinkPooledUnitEvent.PoolId = poolInfo.PoolId;
			failedToLinkPooledUnitEvent.Send();
		}

		public void OnEnterPossession(Unit unit)
		{
			NetworkUnit networkUnit = ((unit != null) ? GetNetworkUnit(unit.NetworkId) : null);
			if (networkUnit != null)
			{
				networkUnit.OnEnterPossession();
			}
		}

		public void OnExitPossession(Unit unit)
		{
			NetworkUnit networkUnit = ((unit != null) ? GetNetworkUnit(unit.NetworkId) : null);
			if (networkUnit != null)
			{
				networkUnit.OnExitPossession();
			}
		}

		public void OnNetworkUnitInBattleScene(NetworkUnit networkUnit)
		{
			SubscribeToNetworkUnitEvents(networkUnit, subscribe: true);
		}

		public override void Disconnected(BoltConnection connection)
		{
			if (connection.DisconnectReason == UdpConnectionDisconnectReason.Timeout && connection.ConnectionType == UdpConnectionType.Unknown)
			{
				Debug.Log("Ignoring unknown timeout disconnection message.");
				return;
			}
			base.Disconnected(connection);
			m_didDisconnect = true;
		}

		public override void OnEvent(PlaceUnitEvent placeEvent)
		{
			base.OnEvent(placeEvent);
			if (!BoltNetwork.IsServer)
			{
				return;
			}
			BrushBehaviourBase brushBehaviourBase = ServiceLocator.GetService<GameModeService>().CurrentGameMode.Brush?.BrushBehaviour;
			if (brushBehaviourBase == null || brushBehaviourBase.UnitPlacementBrush == null)
			{
				ServerSendReplyPlaceUnitEvent(placeEvent.UnitInstanceId);
				Debug.LogError("brush is null");
				return;
			}
			UnitBlueprint unitBlueprint = ContentDatabase.Instance().GetUnitBlueprint(new DatabaseID(placeEvent.UnitModId, placeEvent.UnitId));
			if (unitBlueprint == null)
			{
				ServerSendReplyPlaceUnitEvent(placeEvent.UnitInstanceId);
				Debug.LogError("blueprint is null");
				return;
			}
			Team remotePlayerTeam = m_networkService.RemotePlayerTeam;
			Vector3 position = placeEvent.Position;
			int unitInstanceId = placeEvent.UnitInstanceId;
			brushBehaviourBase.PlaceUnit(unitBlueprint, remotePlayerTeam, position, placeEvent.Rotation, placeEvent.IsCampaignUnit, forMartianPlayer: true, unitInstanceId);
		}

		public override void OnEvent(SpawnUnitEvent spawnEvent)
		{
			base.OnEvent(spawnEvent);
			if (!BoltNetwork.IsServer)
			{
				return;
			}
			UnitBlueprint unitBlueprint = ContentDatabase.Instance().GetUnitBlueprint(new DatabaseID(spawnEvent.UnitModId, spawnEvent.UnitId));
			if (unitBlueprint == null)
			{
				ServerSendFailedToSpawnUnitEvent(spawnEvent.UnitInstanceId);
				Debug.LogError("blueprint is null");
				return;
			}
			Team remotePlayerTeam = m_networkService.RemotePlayerTeam;
			int unitInstanceId = spawnEvent.UnitInstanceId;
			unitBlueprint.Spawn(spawnEvent.Position, spawnEvent.Rotation, remotePlayerTeam, out var unitToSpawn, 1f, isCampaignUnit: false, spawnRiders: false, unitInstanceId);
			if (unitToSpawn != null && unitToSpawn.RuntimeReference == null)
			{
				unitToSpawn.RuntimeReference = m_runtimeReference.CreateReference(unitToSpawn);
			}
		}

		public override void OnEvent(RemoveUnitEvent removeEvent)
		{
			base.OnEvent(removeEvent);
			if (!BoltNetwork.IsServer || !GetBrush(out var brush))
			{
				return;
			}
			Unit unit = GetUnitByRemoteInstanceId(removeEvent.UnitInstanceId);
			if (unit == null)
			{
				unit = GetUnitBySmallNetworkId((ushort)removeEvent.UnitSmallNetworkId);
				if (unit == null)
				{
					return;
				}
			}
			brush.RemoveUnit(unit, forMartian: true);
		}

		public override void OnEvent(ReplyPlaceUnitEvent replyEvent)
		{
			base.OnEvent(replyEvent);
			if (BoltNetwork.IsClient)
			{
				RemoveUnitOnError(replyEvent.UnitInstanceId, isForRemotePlayer: false, destroyMount: false);
			}
		}

		public override void OnEvent(FailedToSpawnUnitEvent failedEvent)
		{
			base.OnEvent(failedEvent);
			RemoveUnitOnError(failedEvent.UnitInstanceId, isForRemotePlayer: false, destroyMount: false);
		}

		public override void OnEvent(FailedToLinkUnitEvent failedEvent)
		{
			base.OnEvent(failedEvent);
			bool isForRemotePlayer = failedEvent.Team == (int)m_networkService.RemotePlayerTeam;
			RemoveUnitOnError(failedEvent.UnitInstanceId, isForRemotePlayer, destroyMount: true);
		}

		public override void OnEvent(FailedToLinkPooledUnitEvent failedEvent)
		{
			base.OnEvent(failedEvent);
			Unit unitByInstanceId = GetUnitByInstanceId(failedEvent.UnitInstanceId);
			if (!(unitByInstanceId == null))
			{
				if (unitByInstanceId.PoolInfo.HasValue)
				{
					unitByInstanceId.PoolInfo = new UnitPoolInfo(unitByInstanceId.PoolInfo.Value.PoolIndex, unitByInstanceId.PoolInfo.Value.PoolId, hasNetworkError: true);
				}
				else
				{
					unitByInstanceId.PoolInfo = new UnitPoolInfo(failedEvent.PoolIndex, (short)failedEvent.PoolId, hasNetworkError: true);
				}
			}
		}

		public override void OnEvent(UnitIdsEvent idsEvent)
		{
			base.OnEvent(idsEvent);
			Unit unitByInstanceId = GetUnitByInstanceId(idsEvent.UnitInstanceId);
			if (unitByInstanceId != null)
			{
				unitByInstanceId.RemoteInstanceId = (short)idsEvent.UnitRemoteInstanceId;
			}
		}

		public override void OnEvent(UnitDiedEvent diedEvent)
		{
			base.OnEvent(diedEvent);
			if (!BoltNetwork.IsClient)
			{
				return;
			}
			Unit unitBySmallNetworkId = GetUnitBySmallNetworkId((ushort)diedEvent.UnitSmallNetworkId);
			if (!(unitBySmallNetworkId == null))
			{
				HealthHandler healthHandler = ((unitBySmallNetworkId.data != null) ? unitBySmallNetworkId.data.healthHandler : null);
				if (healthHandler != null)
				{
					healthHandler.Die(wasKilledRemotely: true);
				}
			}
		}

		public override void OnEvent(SpawnUnitFromPoolEvent spawnEvent)
		{
			base.OnEvent(spawnEvent);
			if (!BoltNetwork.IsClient)
			{
				return;
			}
			Unit unitBySmallNetworkId = GetUnitBySmallNetworkId((ushort)spawnEvent.UnitSmallNetworkId);
			if (unitBySmallNetworkId == null)
			{
				return;
			}
			UnitSpawnSource spawnSource = (UnitSpawnSource)spawnEvent.SpawnSource;
			if (spawnSource == UnitSpawnSource.MeleeWeaponCopySelf)
			{
				MeleeWeaponCopySelf componentInChildren = unitBySmallNetworkId.GetComponentInChildren<MeleeWeaponCopySelf>();
				if (componentInChildren == null)
				{
					Debug.LogError("Failed to get MeleeWeaponCopySelf component on Unit. Make sure you used the correct spawn source when spawning a Unit from the pool.");
					return;
				}
				if (componentInChildren.CopySelfLimitedPool != null)
				{
					Unit unitBySmallNetworkId2 = GetUnitBySmallNetworkId((ushort)spawnEvent.CopyOfSmallNetworkId);
					Transform transform = ((unitBySmallNetworkId2 != null) ? unitBySmallNetworkId2.transform : unitBySmallNetworkId.transform);
					componentInChildren.CopySelfLimitedPool.InitializeUnitFromPool(unitBySmallNetworkId, transform.position, transform.rotation);
				}
				else
				{
					Debug.LogError("Add support for new pool class.");
				}
				componentInChildren.OnSpawnedUnit(unitBySmallNetworkId, unitBySmallNetworkId.gameObject, spawnEvent.UnitSpawnPosition, spawnedFromPool: true);
			}
			else
			{
				Debug.LogError($"Unsupported spawn source: {spawnSource}");
			}
		}

		private static bool GetBrush(out BrushBehaviourBase brush)
		{
			BaseGameMode currentGameMode = ServiceLocator.GetService<GameModeService>().CurrentGameMode;
			brush = currentGameMode.Brush?.BrushBehaviour;
			if (brush?.UnitPlacementBrush != null)
			{
				return true;
			}
			Debug.LogError("brush is null.");
			return false;
		}

		private void RemoveUnitOnError(int unitInstanceId, bool isForRemotePlayer, bool destroyMount)
		{
			if (!GetBrush(out var brush))
			{
				return;
			}
			Unit unitByInstanceId = GetUnitByInstanceId(unitInstanceId);
			if (unitByInstanceId == null)
			{
				return;
			}
			if (destroyMount && unitByInstanceId.IsRider)
			{
				Mount component = unitByInstanceId.GetComponent<Mount>();
				Unit unit = ((component != null && component.IsMounted && component.OtherData != null) ? component.OtherData.unit : null);
				if (unit != null)
				{
					brush.RemoveUnit(unit, isForRemotePlayer);
					return;
				}
			}
			brush.RemoveUnit(unitByInstanceId, isForRemotePlayer);
		}

		private void OnSpawnedUnit(Unit unit)
		{
			if (!m_units.Contains(unit))
			{
				m_units.Add(unit);
			}
			HealthHandler healthHandler = ((unit.data != null) ? unit.data.healthHandler : null);
			if (healthHandler != null)
			{
				healthHandler.UnitDied += OnUnitDied;
			}
			unit.IsLocalCopyOfRemoteUnit = GetNetworkUnit(unit.NetworkId) != null;
			if (BoltNetwork.IsServer)
			{
				OnSpawnedUnitOnServer(unit);
			}
			else if (BoltNetwork.IsClient)
			{
				OnSpawnedUnitOnClient(unit);
			}
			if (BoltNetwork.IsRunning && BattleController.blindMode && m_gameStateManager.GameState == GameState.PlacementState)
			{
				ApplyProjectMarsBlindModeSettings(unit, unit.Team);
			}
		}

		private void OnDestroyedUnit(Unit unit)
		{
			int num = m_units.IndexOf(unit);
			if (num >= 0)
			{
				m_units.RemoveAt(num);
			}
			HealthHandler healthHandler = ((unit.data != null) ? unit.data.healthHandler : null);
			if (healthHandler != null)
			{
				healthHandler.UnitDied -= OnUnitDied;
			}
			NetworkUnit networkUnit = GetNetworkUnit(unit.NetworkId);
			if (!(networkUnit == null))
			{
				SubscribeToNetworkUnitEvents(networkUnit, subscribe: false);
				if (!(networkUnit.entity != null) || networkUnit.entity.IsOwner)
				{
					BoltNetwork.Destroy(networkUnit.gameObject);
				}
			}
		}

		private void OnRemovedUnit(Unit unit)
		{
			if (BoltNetwork.IsClient && m_gameStateManager != null && m_gameStateManager.GameState == GameState.PlacementState && !unit.IsLocalCopyOfRemoteUnit)
			{
				ClientSendRemoveUnitEvent(unit);
			}
		}

		private Unit GetUnit(NetworkId networkId)
		{
			return GetUnit(networkId.PackedValue);
		}

		private NetworkUnit GetNetworkUnit(ulong networkId)
		{
			foreach (BoltEntity entity in BoltNetwork.Entities)
			{
				if (entity.StateIs<IUnitState>() && entity.NetworkId.PackedValue == networkId)
				{
					return entity.GetComponent<NetworkUnit>();
				}
			}
			return null;
		}

		private void SubscribeToNetworkUnitEvents(NetworkUnit networkUnit, bool subscribe)
		{
			if (!(networkUnit == null))
			{
				networkUnit.NetworkUnitDetached -= OnNetworkUnitDetached;
				if (subscribe)
				{
					networkUnit.NetworkUnitDetached += OnNetworkUnitDetached;
				}
			}
		}

		private void OnSpawnedUnitOnServer(Unit unit)
		{
			if (!(ServiceLocator.GetService<GameModeService>().CurrentGameMode.GetType() != typeof(OnlineMultiplayerGameMode)))
			{
				if (m_instanceIDGenerator == null)
				{
					m_instanceIDGenerator = new NetworkIDGenerator(1, 32767);
					m_networkIDGenerator = new NetworkIDGenerator(1, 65535);
				}
				unit.InstanceId = (short)m_instanceIDGenerator.GetNextID();
				unit.SmallNetworkId = (ushort)m_networkIDGenerator.GetNextID();
				SpawnNetworkUnit(unit);
			}
		}

		private void OnSpawnedUnitOnClient(Unit unit)
		{
			if (ServiceLocator.GetService<GameModeService>().CurrentGameMode.GetType() != typeof(OnlineMultiplayerGameMode))
			{
				return;
			}
			if (m_instanceIDGenerator == null)
			{
				m_instanceIDGenerator = new NetworkIDGenerator(-32768, -1);
			}
			unit.InstanceId = (short)m_instanceIDGenerator.GetNextID();
			NetworkUnit networkUnit = GetNetworkUnit(unit.NetworkId);
			if (networkUnit != null)
			{
				SubscribeToNetworkUnitEvents(networkUnit, subscribe: true);
			}
			else if (!unit.IsRider && !unit.IsInPool)
			{
				Transform transform = unit.transform;
				if (m_gameStateManager != null && m_gameStateManager.GameState == GameState.BattleState)
				{
					ClientSendSpawnUnitEvent(unit.unitBlueprint.Entity.GUID.m_ID, unit.unitBlueprint.Entity.GUID.m_modID, transform.position, transform.rotation, unit.InstanceId);
				}
				else
				{
					ClientSendPlaceUnitEvent(unit.unitBlueprint.Entity.GUID.m_ID, unit.unitBlueprint.Entity.GUID.m_modID, transform.position, transform.rotation, isCampaignUnit: false, unit.InstanceId);
				}
			}
		}

		private void ServerSendFailedToSpawnUnitEvent(int remoteInstanceId)
		{
			FailedToSpawnUnitEvent failedToSpawnUnitEvent = FailedToSpawnUnitEvent.Create(GlobalTargets.AllClients, ReliabilityModes.ReliableOrdered);
			failedToSpawnUnitEvent.UnitInstanceId = remoteInstanceId;
			failedToSpawnUnitEvent.Send();
		}

		private void ClientSendPlaceUnitEvent(int id, int modId, Vector3 position, Quaternion rotation, bool isCampaignUnit, int instanceId)
		{
			PlaceUnitEvent placeUnitEvent = PlaceUnitEvent.Create(GlobalTargets.OnlyServer, ReliabilityModes.ReliableOrdered);
			placeUnitEvent.UnitId = id;
			placeUnitEvent.UnitModId = modId;
			placeUnitEvent.Position = position;
			placeUnitEvent.Rotation = rotation;
			placeUnitEvent.IsCampaignUnit = isCampaignUnit;
			placeUnitEvent.UnitInstanceId = instanceId;
			placeUnitEvent.Send();
		}

		private void ClientSendSpawnUnitEvent(int id, int modId, Vector3 position, Quaternion rotation, int instanceId)
		{
			SpawnUnitEvent spawnUnitEvent = SpawnUnitEvent.Create(GlobalTargets.OnlyServer, ReliabilityModes.ReliableOrdered);
			spawnUnitEvent.UnitId = id;
			spawnUnitEvent.UnitModId = modId;
			spawnUnitEvent.Position = position;
			spawnUnitEvent.Rotation = rotation;
			spawnUnitEvent.UnitInstanceId = instanceId;
			spawnUnitEvent.Send();
		}

		private void ServerSendReplyPlaceUnitEvent(int instanceId)
		{
			ReplyPlaceUnitEvent replyPlaceUnitEvent = ReplyPlaceUnitEvent.Create(GlobalTargets.AllClients, ReliabilityModes.ReliableOrdered);
			replyPlaceUnitEvent.UnitInstanceId = instanceId;
			replyPlaceUnitEvent.Send();
		}

		private void ClientSendRemoveUnitEvent(Unit unit)
		{
			RemoveUnitEvent removeUnitEvent = RemoveUnitEvent.Create(GlobalTargets.OnlyServer, ReliabilityModes.ReliableOrdered);
			removeUnitEvent.UnitSmallNetworkId = unit.SmallNetworkId;
			removeUnitEvent.UnitInstanceId = unit.InstanceId;
			removeUnitEvent.Send();
		}

		private void SpawnNetworkUnit(Unit unit)
		{
			int iD = unit.unitBlueprint.Entity.GUID.m_ID;
			int modID = unit.unitBlueprint.Entity.GUID.m_modID;
			Team team = unit.Team;
			Mount component = unit.GetComponent<Mount>();
			ushort mountUnitSmallNetworkId = (ushort)((component != null && component.IsMounted && component.OtherData != null && component.OtherData.unit != null) ? component.OtherData.unit.SmallNetworkId : 0);
			Unit unitBySmallNetworkId = GetUnitBySmallNetworkId(unit.CopyOfSmallNetworkId);
			UnitSpawnToken token = new UnitSpawnToken(iD, modID, team, component != null && component.IsMounted, mountUnitSmallNetworkId, (component != null) ? component.SitId : 0, (ushort)((unitBySmallNetworkId != null) ? unitBySmallNetworkId.SmallNetworkId : 0), (unitBySmallNetworkId != null) ? unit.CopyOfUnitSpawnPosition : Vector3.zero, unit.SpawnSource, unit.RemoteInstanceId, unit.IsRiderWithLinkedMount, unit.InstanceId, unit.SmallNetworkId, unit.PoolInfo);
			Transform transform = unit.transform;
			BoltEntity boltEntity = BoltNetwork.Instantiate(BoltPrefabs.NetworkUnit, token, transform.position, transform.rotation);
			if (boltEntity == null)
			{
				Debug.LogError("entity is null");
				return;
			}
			NetworkUnit component2 = boltEntity.GetComponent<NetworkUnit>();
			bool isClient = BoltNetwork.IsClient;
			component2.Initialize(unit, isClient);
			SubscribeToNetworkUnitEvents(component2, subscribe: true);
		}

		private void ApplyProjectMarsBlindModeSettings(Unit spawnedUnit, Team spawnedUnitTeam)
		{
			if (spawnedUnit == null || BattleController == null || m_networkService == null || !BattleController.blindMode || m_networkService.RemotePlayerTeam != spawnedUnitTeam)
			{
				return;
			}
			spawnedUnit.IsSpawnedInBlindPlacement = true;
			ShowProjectile[] componentsInChildren = spawnedUnit.GetComponentsInChildren<ShowProjectile>();
			if (componentsInChildren != null && componentsInChildren.Length != 0)
			{
				ShowProjectile[] array = componentsInChildren;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].IsInBlindGame = true;
				}
			}
			spawnedUnit.GetAllRenderers();
			if (spawnedUnit.SpawnEffects != null)
			{
				spawnedUnit.SpawnEffects.gameObject.SetActive(value: false);
			}
			spawnedUnit.ShowRenderers(enable: false);
		}

		private void OnUnitDied(Unit unit)
		{
			NetworkUnit networkUnit = GetNetworkUnit(unit.NetworkId);
			if (networkUnit != null)
			{
				networkUnit.OnUnitDied();
			}
			if (BoltNetwork.IsServer)
			{
				UnitDiedEvent unitDiedEvent = UnitDiedEvent.Create(GlobalTargets.AllClients, ReliabilityModes.ReliableOrdered);
				unitDiedEvent.UnitSmallNetworkId = unit.SmallNetworkId;
				unitDiedEvent.Send();
			}
		}

		private void OnNetworkUnitDetached(NetworkUnit networkUnit)
		{
			if (networkUnit == null || networkUnit.entity == null || !m_networkService.IsConnected || m_didDisconnect || networkUnit.entity == null || networkUnit.entity.IsOwner)
			{
				return;
			}
			Unit unit = ((networkUnit.Unit != null) ? networkUnit.Unit : GetUnit(networkUnit.entity.NetworkId));
			if (unit == null)
			{
				return;
			}
			if (m_gameStateManager != null && m_gameStateManager.GameState == GameState.BattleState)
			{
				if (!unit.data.Dead)
				{
					unit.data.healthHandler.Die(wasKilledRemotely: true);
				}
				UnityEngine.Object.Destroy(unit.gameObject);
			}
			else
			{
				BrushBehaviourBase brushBehaviourBase = ServiceLocator.GetService<GameModeService>().CurrentGameMode.Brush?.BrushBehaviour;
				if (brushBehaviourBase == null || brushBehaviourBase.UnitPlacementBrush == null)
				{
					Debug.LogError("brush is null");
				}
				else
				{
					brushBehaviourBase.UnitPlacementBrush.RemoveUnitInternal(unit, unit.Team);
				}
			}
		}
	}
}
