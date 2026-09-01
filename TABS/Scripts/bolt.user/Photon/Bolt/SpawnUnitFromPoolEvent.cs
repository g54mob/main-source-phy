using System;
using Photon.Bolt.Exceptions;
using Photon.Bolt.Internal;
using UnityEngine;

namespace Photon.Bolt
{
	public class SpawnUnitFromPoolEvent : Event
	{
		public int SpawnSource
		{
			get
			{
				return Storage.Values[OffsetStorage].Int0;
			}
			set
			{
				value = Mathf.Clamp(value, 0, 1);
				int @int = Storage.Values[OffsetStorage].Int0;
				Storage.Values[OffsetStorage].Int0 = value;
				if (!NetworkValue.Diff(@int, value))
				{
				}
			}
		}

		public int UnitSmallNetworkId
		{
			get
			{
				return Storage.Values[OffsetStorage + 1].Int0;
			}
			set
			{
				value = Mathf.Clamp(value, 0, 65535);
				int @int = Storage.Values[OffsetStorage + 1].Int0;
				Storage.Values[OffsetStorage + 1].Int0 = value;
				if (!NetworkValue.Diff(@int, value))
				{
				}
			}
		}

		public Vector3 UnitSpawnPosition
		{
			get
			{
				return Storage.Values[OffsetStorage + 2].Vector3;
			}
			set
			{
				Vector3 vector = Storage.Values[OffsetStorage + 2].Vector3;
				Storage.Values[OffsetStorage + 2].Vector3 = value;
				if (!NetworkValue.Diff(vector, value))
				{
				}
			}
		}

		public int CopyOfSmallNetworkId
		{
			get
			{
				return Storage.Values[OffsetStorage + 3].Int0;
			}
			set
			{
				value = Mathf.Clamp(value, 0, 65535);
				int @int = Storage.Values[OffsetStorage + 3].Int0;
				Storage.Values[OffsetStorage + 3].Int0 = value;
				if (!NetworkValue.Diff(@int, value))
				{
				}
			}
		}

		public SpawnUnitFromPoolEvent()
			: base(SpawnUnitFromPoolEvent_Meta.Instance)
		{
		}

		public override string ToString()
		{
			return $"[SpawnUnitFromPoolEvent SpawnSource={SpawnSource} UnitSmallNetworkId={UnitSmallNetworkId} UnitSpawnPosition={UnitSpawnPosition} CopyOfSmallNetworkId={CopyOfSmallNetworkId}]";
		}

		public static SpawnUnitFromPoolEvent Create(BoltEntity entity, EntityTargets targets)
		{
			if (!entity)
			{
				throw new ArgumentNullException("entity");
			}
			if (!entity.IsAttached)
			{
				throw new BoltException("You can not raise events on entities which are not attached");
			}
			if (!(Factory.NewEvent(((IFactory)SpawnUnitFromPoolEvent_Meta.Instance).TypeKey) is SpawnUnitFromPoolEvent spawnUnitFromPoolEvent))
			{
				return null;
			}
			spawnUnitFromPoolEvent.Targets = (int)targets;
			spawnUnitFromPoolEvent.TargetEntity = entity.Entity;
			spawnUnitFromPoolEvent.Reliability = ReliabilityModes.Unreliable;
			return spawnUnitFromPoolEvent;
		}

		public static SpawnUnitFromPoolEvent Create(BoltEntity entity)
		{
			return Create(entity, EntityTargets.Everyone);
		}

		private static SpawnUnitFromPoolEvent Create(byte targets, BoltConnection connection, ReliabilityModes reliability)
		{
			if (!BoltCore.isServer)
			{
				throw new BoltException("You are not the server, you can not raise this event");
			}
			if (!(Factory.NewEvent(((IFactory)SpawnUnitFromPoolEvent_Meta.Instance).TypeKey) is SpawnUnitFromPoolEvent spawnUnitFromPoolEvent))
			{
				return null;
			}
			spawnUnitFromPoolEvent.Targets = targets;
			spawnUnitFromPoolEvent.TargetConnection = connection;
			spawnUnitFromPoolEvent.Reliability = reliability;
			return spawnUnitFromPoolEvent;
		}

		public static SpawnUnitFromPoolEvent Create(GlobalTargets targets)
		{
			return Create((byte)targets, null, ReliabilityModes.ReliableOrdered);
		}

		public static SpawnUnitFromPoolEvent Create(GlobalTargets targets, ReliabilityModes reliability)
		{
			return Create((byte)targets, null, reliability);
		}

		public static SpawnUnitFromPoolEvent Create(BoltConnection connection)
		{
			return Create(10, connection, ReliabilityModes.ReliableOrdered);
		}

		public static SpawnUnitFromPoolEvent Create(BoltConnection connection, ReliabilityModes reliability)
		{
			return Create(10, connection, reliability);
		}

		public static SpawnUnitFromPoolEvent Create()
		{
			return Create(2, null, ReliabilityModes.ReliableOrdered);
		}

		public static SpawnUnitFromPoolEvent Create(ReliabilityModes reliability)
		{
			return Create(2, null, reliability);
		}

		public static bool Post(BoltEntity entity, EntityTargets targets, int SpawnSource, int UnitSmallNetworkId, Vector3 UnitSpawnPosition, int CopyOfSmallNetworkId)
		{
			SpawnUnitFromPoolEvent spawnUnitFromPoolEvent = Create(entity, targets);
			if (spawnUnitFromPoolEvent == null)
			{
				return false;
			}
			spawnUnitFromPoolEvent.SpawnSource = SpawnSource;
			spawnUnitFromPoolEvent.UnitSmallNetworkId = UnitSmallNetworkId;
			spawnUnitFromPoolEvent.UnitSpawnPosition = UnitSpawnPosition;
			spawnUnitFromPoolEvent.CopyOfSmallNetworkId = CopyOfSmallNetworkId;
			spawnUnitFromPoolEvent.Send();
			return true;
		}

		public static bool Post(BoltEntity entity, int SpawnSource, int UnitSmallNetworkId, Vector3 UnitSpawnPosition, int CopyOfSmallNetworkId)
		{
			return Post(entity, EntityTargets.Everyone, SpawnSource, UnitSmallNetworkId, UnitSpawnPosition, CopyOfSmallNetworkId);
		}

		private static bool Post(byte targets, BoltConnection connection, ReliabilityModes reliability, int SpawnSource, int UnitSmallNetworkId, Vector3 UnitSpawnPosition, int CopyOfSmallNetworkId)
		{
			SpawnUnitFromPoolEvent spawnUnitFromPoolEvent = Create(targets, connection, reliability);
			if (spawnUnitFromPoolEvent == null)
			{
				return false;
			}
			spawnUnitFromPoolEvent.SpawnSource = SpawnSource;
			spawnUnitFromPoolEvent.UnitSmallNetworkId = UnitSmallNetworkId;
			spawnUnitFromPoolEvent.UnitSpawnPosition = UnitSpawnPosition;
			spawnUnitFromPoolEvent.CopyOfSmallNetworkId = CopyOfSmallNetworkId;
			spawnUnitFromPoolEvent.Send();
			return true;
		}

		public static bool Post(GlobalTargets targets, int SpawnSource, int UnitSmallNetworkId, Vector3 UnitSpawnPosition, int CopyOfSmallNetworkId)
		{
			return Post((byte)targets, null, ReliabilityModes.ReliableOrdered, SpawnSource, UnitSmallNetworkId, UnitSpawnPosition, CopyOfSmallNetworkId);
		}

		public static bool Post(GlobalTargets targets, ReliabilityModes reliability, int SpawnSource, int UnitSmallNetworkId, Vector3 UnitSpawnPosition, int CopyOfSmallNetworkId)
		{
			return Post((byte)targets, null, reliability, SpawnSource, UnitSmallNetworkId, UnitSpawnPosition, CopyOfSmallNetworkId);
		}

		public static bool Post(BoltConnection connection, int SpawnSource, int UnitSmallNetworkId, Vector3 UnitSpawnPosition, int CopyOfSmallNetworkId)
		{
			return Post(10, connection, ReliabilityModes.ReliableOrdered, SpawnSource, UnitSmallNetworkId, UnitSpawnPosition, CopyOfSmallNetworkId);
		}

		public static bool Post(BoltConnection connection, ReliabilityModes reliability, int SpawnSource, int UnitSmallNetworkId, Vector3 UnitSpawnPosition, int CopyOfSmallNetworkId)
		{
			return Post(10, connection, reliability, SpawnSource, UnitSmallNetworkId, UnitSpawnPosition, CopyOfSmallNetworkId);
		}

		public static bool Post(int SpawnSource, int UnitSmallNetworkId, Vector3 UnitSpawnPosition, int CopyOfSmallNetworkId)
		{
			return Post(2, null, ReliabilityModes.ReliableOrdered, SpawnSource, UnitSmallNetworkId, UnitSpawnPosition, CopyOfSmallNetworkId);
		}

		public static bool Post(ReliabilityModes reliability, int SpawnSource, int UnitSmallNetworkId, Vector3 UnitSpawnPosition, int CopyOfSmallNetworkId)
		{
			return Post(2, null, reliability, SpawnSource, UnitSmallNetworkId, UnitSpawnPosition, CopyOfSmallNetworkId);
		}
	}
}
