using Photon.Bolt.Exceptions;
using Photon.Bolt.Internal;
using UnityEngine;

namespace Photon.Bolt
{
	public class UnitIdsEvent : Event
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

		public int UnitRemoteInstanceId
		{
			get
			{
				return Storage.Values[OffsetStorage + 1].Int0;
			}
			set
			{
				value = Mathf.Clamp(value, -32768, 32767);
				int @int = Storage.Values[OffsetStorage + 1].Int0;
				Storage.Values[OffsetStorage + 1].Int0 = value;
				if (!NetworkValue.Diff(@int, value))
				{
				}
			}
		}

		public UnitIdsEvent()
			: base(UnitIdsEvent_Meta.Instance)
		{
		}

		public override string ToString()
		{
			return $"[UnitIdsEvent UnitInstanceId={UnitInstanceId} UnitRemoteInstanceId={UnitRemoteInstanceId}]";
		}

		private static UnitIdsEvent Create(byte targets, BoltConnection connection, ReliabilityModes reliability)
		{
			if (!BoltCore.isClient)
			{
				throw new BoltException("You are not a client, you can not raise this event");
			}
			if (!(Factory.NewEvent(((IFactory)UnitIdsEvent_Meta.Instance).TypeKey) is UnitIdsEvent unitIdsEvent))
			{
				return null;
			}
			unitIdsEvent.Targets = targets;
			unitIdsEvent.TargetConnection = connection;
			unitIdsEvent.Reliability = reliability;
			return unitIdsEvent;
		}

		public static UnitIdsEvent Create(GlobalTargets targets)
		{
			return Create((byte)targets, null, ReliabilityModes.ReliableOrdered);
		}

		public static UnitIdsEvent Create(GlobalTargets targets, ReliabilityModes reliability)
		{
			return Create((byte)targets, null, reliability);
		}

		public static UnitIdsEvent Create(BoltConnection connection)
		{
			return Create(10, connection, ReliabilityModes.ReliableOrdered);
		}

		public static UnitIdsEvent Create(BoltConnection connection, ReliabilityModes reliability)
		{
			return Create(10, connection, reliability);
		}

		public static UnitIdsEvent Create()
		{
			return Create(2, null, ReliabilityModes.ReliableOrdered);
		}

		public static UnitIdsEvent Create(ReliabilityModes reliability)
		{
			return Create(2, null, reliability);
		}

		private static bool Post(byte targets, BoltConnection connection, ReliabilityModes reliability, int UnitInstanceId, int UnitRemoteInstanceId)
		{
			UnitIdsEvent unitIdsEvent = Create(targets, connection, reliability);
			if (unitIdsEvent == null)
			{
				return false;
			}
			unitIdsEvent.UnitInstanceId = UnitInstanceId;
			unitIdsEvent.UnitRemoteInstanceId = UnitRemoteInstanceId;
			unitIdsEvent.Send();
			return true;
		}

		public static bool Post(GlobalTargets targets, int UnitInstanceId, int UnitRemoteInstanceId)
		{
			return Post((byte)targets, null, ReliabilityModes.ReliableOrdered, UnitInstanceId, UnitRemoteInstanceId);
		}

		public static bool Post(GlobalTargets targets, ReliabilityModes reliability, int UnitInstanceId, int UnitRemoteInstanceId)
		{
			return Post((byte)targets, null, reliability, UnitInstanceId, UnitRemoteInstanceId);
		}

		public static bool Post(BoltConnection connection, int UnitInstanceId, int UnitRemoteInstanceId)
		{
			return Post(10, connection, ReliabilityModes.ReliableOrdered, UnitInstanceId, UnitRemoteInstanceId);
		}

		public static bool Post(BoltConnection connection, ReliabilityModes reliability, int UnitInstanceId, int UnitRemoteInstanceId)
		{
			return Post(10, connection, reliability, UnitInstanceId, UnitRemoteInstanceId);
		}

		public static bool Post(int UnitInstanceId, int UnitRemoteInstanceId)
		{
			return Post(2, null, ReliabilityModes.ReliableOrdered, UnitInstanceId, UnitRemoteInstanceId);
		}

		public static bool Post(ReliabilityModes reliability, int UnitInstanceId, int UnitRemoteInstanceId)
		{
			return Post(2, null, reliability, UnitInstanceId, UnitRemoteInstanceId);
		}
	}
}
