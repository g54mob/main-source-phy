using Landfall.TABS;
using Photon.Bolt;
using UnityEngine;

namespace TFBGames
{
	public class PossessedNetworkUnitController : GlobalEventListener
	{
		private PlayerCamerasManager m_playerCameras;

		private CameraAbilityPossess m_cameraPossess;

		private INetworkUnitsManager m_unitsManager;

		private Unit m_possessedUnit;

		private PossessedNetworkUnit m_possessedNetworkUnit;

		private void Start()
		{
			m_unitsManager = ServiceLocator.GetService<INetworkUnitsManager>();
			if (BoltNetwork.IsClient)
			{
				InitializeClient();
			}
		}

		private void OnDestroy()
		{
			if (m_playerCameras != null)
			{
				m_playerCameras.FoundPlayerCameras -= OnFoundPlayerCameras;
			}
		}

		public override void OnEvent(UnitEnterPossessionEvent enterEvent)
		{
			base.OnEvent(enterEvent);
			if (!BoltNetwork.IsServer)
			{
				return;
			}
			if (m_possessedNetworkUnit == null)
			{
				m_possessedNetworkUnit = FindPossessedNetworkUnit();
				if (m_possessedNetworkUnit == null)
				{
					return;
				}
			}
			Unit unitByRemoteInstanceId = m_unitsManager.GetUnitByRemoteInstanceId(enterEvent.UnitInstanceId);
			m_unitsManager.OnEnterPossession(unitByRemoteInstanceId);
			m_possessedNetworkUnit.OnEnterPossession(unitByRemoteInstanceId);
		}

		public override void OnEvent(UnitExitPossessionEvent exitEvent)
		{
			base.OnEvent(exitEvent);
			if (!BoltNetwork.IsServer)
			{
				return;
			}
			if (m_possessedNetworkUnit == null)
			{
				m_possessedNetworkUnit = FindPossessedNetworkUnit();
				if (m_possessedNetworkUnit == null)
				{
					return;
				}
			}
			Unit unitByRemoteInstanceId = m_unitsManager.GetUnitByRemoteInstanceId(exitEvent.UnitInstanceId);
			m_unitsManager.OnExitPossession(unitByRemoteInstanceId);
			m_possessedNetworkUnit.OnExitPossession(unitByRemoteInstanceId);
		}

		private void InitializeClient()
		{
			m_playerCameras = ServiceLocator.GetService<PlayerCamerasManager>();
			if (m_playerCameras.CamerasCount > 0)
			{
				OnFoundPlayerCameras();
			}
			else
			{
				m_playerCameras.FoundPlayerCameras += OnFoundPlayerCameras;
			}
			PossessedNetworkUnit possessedNetworkUnit = FindPossessedNetworkUnit();
			if (possessedNetworkUnit != null)
			{
				Object.Destroy(possessedNetworkUnit.gameObject);
			}
			BoltEntity boltEntity = BoltNetwork.Instantiate(BoltPrefabs.PossessedNetworkUnit);
			if (boltEntity == null)
			{
				Debug.LogError("Failed to instantiate PossessedNetworkUnit entity. Make sure you compiled Bolt's assembly.");
			}
			m_possessedNetworkUnit = boltEntity.GetComponent<PossessedNetworkUnit>();
		}

		private void OnFoundPlayerCameras()
		{
			PlayerCamera camera = m_playerCameras.GetCamera(Player.One);
			m_cameraPossess = camera.PossessCamera;
			m_cameraPossess.AddOnPossessEnterAction(OnEnterPossession);
			m_cameraPossess.AddOnPossessExitAction(OnExitPossession);
			if (m_playerCameras.CamerasCount > 1)
			{
				Debug.LogWarning("We currently don't support more than 1 player camera in PossessedNetworkUnitController.");
			}
		}

		private PossessedNetworkUnit FindPossessedNetworkUnit()
		{
			foreach (BoltEntity entity in BoltNetwork.Entities)
			{
				if (entity.StateIs<IPossessedUnitState>())
				{
					return entity.GetComponent<PossessedNetworkUnit>();
				}
			}
			return null;
		}

		private void OnEnterPossession()
		{
			if (m_possessedUnit != null)
			{
				OnExitPossession();
			}
			m_possessedUnit = m_cameraPossess.currentUnit;
			if (!(m_possessedUnit == null))
			{
				Unit possessedUnit = m_possessedUnit;
				m_unitsManager.OnEnterPossession(possessedUnit);
				m_possessedNetworkUnit.OnEnterPossession(possessedUnit);
				SendEnterPossessionEvent(possessedUnit);
			}
		}

		private void OnExitPossession()
		{
			Unit possessedUnit = m_possessedUnit;
			m_possessedUnit = null;
			if (!(possessedUnit == null))
			{
				m_unitsManager.OnExitPossession(possessedUnit);
				m_possessedNetworkUnit.OnExitPossession(possessedUnit);
				SendExitPossessionEvent(possessedUnit);
			}
		}

		private void SendEnterPossessionEvent(Unit unit)
		{
			UnitEnterPossessionEvent unitEnterPossessionEvent = UnitEnterPossessionEvent.Create(GlobalTargets.OnlyServer, ReliabilityModes.ReliableOrdered);
			unitEnterPossessionEvent.UnitInstanceId = unit.InstanceId;
			unitEnterPossessionEvent.Send();
		}

		private void SendExitPossessionEvent(Unit unit)
		{
			UnitExitPossessionEvent unitExitPossessionEvent = UnitExitPossessionEvent.Create(GlobalTargets.OnlyServer, ReliabilityModes.ReliableOrdered);
			unitExitPossessionEvent.UnitInstanceId = unit.InstanceId;
			unitExitPossessionEvent.Send();
		}
	}
}
