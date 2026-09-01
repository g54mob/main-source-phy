using Photon.Bolt.Exceptions;
using Photon.Bolt.Internal;
using UnityEngine;

namespace Photon.Bolt
{
	public class UnitExitPossessionEvent : Event
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

		public UnitExitPossessionEvent()
			: base(UnitExitPossessionEvent_Meta.Instance)
		{
		}

		public override string ToString()
		{
			return $"[UnitExitPossessionEvent UnitInstanceId={UnitInstanceId}]";
		}

		private static UnitExitPossessionEvent Create(byte targets, BoltConnection connection, ReliabilityModes reliability)
		{
			if (!BoltCore.isClient)
			{
				throw new BoltException("You are not a client, you can not raise this event");
			}
			if (!(Factory.NewEvent(((IFactory)UnitExitPossessionEvent_Meta.Instance).TypeKey) is UnitExitPossessionEvent unitExitPossessionEvent))
			{
				return null;
			}
			unitExitPossessionEvent.Targets = targets;
			unitExitPossessionEvent.TargetConnection = connection;
			unitExitPossessionEvent.Reliability = reliability;
			return unitExitPossessionEvent;
		}

		public static UnitExitPossessionEvent Create(GlobalTargets targets)
		{
			return Create((byte)targets, null, ReliabilityModes.ReliableOrdered);
		}

		public static UnitExitPossessionEvent Create(GlobalTargets targets, ReliabilityModes reliability)
		{
			return Create((byte)targets, null, reliability);
		}

		public static UnitExitPossessionEvent Create(BoltConnection connection)
		{
			return Create(10, connection, ReliabilityModes.ReliableOrdered);
		}

		public static UnitExitPossessionEvent Create(BoltConnection connection, ReliabilityModes reliability)
		{
			return Create(10, connection, reliability);
		}

		public static UnitExitPossessionEvent Create()
		{
			return Create(2, null, ReliabilityModes.ReliableOrdered);
		}

		public static UnitExitPossessionEvent Create(ReliabilityModes reliability)
		{
			return Create(2, null, reliability);
		}

		private static bool Post(byte targets, BoltConnection connection, ReliabilityModes reliability, int UnitInstanceId)
		{
			UnitExitPossessionEvent unitExitPossessionEvent = Create(targets, connection, reliability);
			if (unitExitPossessionEvent == null)
			{
				return false;
			}
			unitExitPossessionEvent.UnitInstanceId = UnitInstanceId;
			unitExitPossessionEvent.Send();
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
