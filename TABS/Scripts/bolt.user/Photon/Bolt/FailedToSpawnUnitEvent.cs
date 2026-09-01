using Photon.Bolt.Internal;
using UnityEngine;

namespace Photon.Bolt
{
	public class FailedToSpawnUnitEvent : Event
	{
		public int UnitInstanceId
		{
			get
			{
				return Storage.Values[OffsetStorage].Int0;
			}
			set
			{
				value = Mathf.Clamp(value, -32768, 32767);
				int @int = Storage.Values[OffsetStorage].Int0;
				Storage.Values[OffsetStorage].Int0 = value;
				if (!NetworkValue.Diff(@int, value))
				{
				}
			}
		}

		public FailedToSpawnUnitEvent()
			: base(FailedToSpawnUnitEvent_Meta.Instance)
		{
		}

		public override string ToString()
		{
			return $"[FailedToSpawnUnitEvent UnitInstanceId={UnitInstanceId}]";
		}

		private static FailedToSpawnUnitEvent Create(byte targets, BoltConnection connection, ReliabilityModes reliability)
		{
			if (!(Factory.NewEvent(((IFactory)FailedToSpawnUnitEvent_Meta.Instance).TypeKey) is FailedToSpawnUnitEvent failedToSpawnUnitEvent))
			{
				return null;
			}
			failedToSpawnUnitEvent.Targets = targets;
			failedToSpawnUnitEvent.TargetConnection = connection;
			failedToSpawnUnitEvent.Reliability = reliability;
			return failedToSpawnUnitEvent;
		}

		public static FailedToSpawnUnitEvent Create(GlobalTargets targets)
		{
			return Create((byte)targets, null, ReliabilityModes.ReliableOrdered);
		}

		public static FailedToSpawnUnitEvent Create(GlobalTargets targets, ReliabilityModes reliability)
		{
			return Create((byte)targets, null, reliability);
		}

		public static FailedToSpawnUnitEvent Create(BoltConnection connection)
		{
			return Create(10, connection, ReliabilityModes.ReliableOrdered);
		}

		public static FailedToSpawnUnitEvent Create(BoltConnection connection, ReliabilityModes reliability)
		{
			return Create(10, connection, reliability);
		}

		public static FailedToSpawnUnitEvent Create()
		{
			return Create(2, null, ReliabilityModes.ReliableOrdered);
		}

		public static FailedToSpawnUnitEvent Create(ReliabilityModes reliability)
		{
			return Create(2, null, reliability);
		}

		private static bool Post(byte targets, BoltConnection connection, ReliabilityModes reliability, int UnitInstanceId)
		{
			FailedToSpawnUnitEvent failedToSpawnUnitEvent = Create(targets, connection, reliability);
			if (failedToSpawnUnitEvent == null)
			{
				return false;
			}
			failedToSpawnUnitEvent.UnitInstanceId = UnitInstanceId;
			failedToSpawnUnitEvent.Send();
			return true;
		}

		public static bool Post(GlobalTargets targets, int UnitInstanceId)
		{
			return Post((byte)targets, null, ReliabilityModes.ReliableOrdered, UnitInstanceId);
		}

		public static bool Post(GlobalTargets targets, ReliabilityModes reliability, int UnitInstanceId)
		{
			return Post((byte)targets, null, reliability, UnitInstanceId);
		}

		public static bool Post(BoltConnection connection, int UnitInstanceId)
		{
			return Post(10, connection, ReliabilityModes.ReliableOrdered, UnitInstanceId);
		}

		public static bool Post(BoltConnection connection, ReliabilityModes reliability, int UnitInstanceId)
		{
			return Post(10, connection, reliability, UnitInstanceId);
		}

		public static bool Post(int UnitInstanceId)
		{
			return Post(2, null, ReliabilityModes.ReliableOrdered, UnitInstanceId);
		}

		public static bool Post(ReliabilityModes reliability, int UnitInstanceId)
		{
			return Post(2, null, reliability, UnitInstanceId);
		}
	}
}
