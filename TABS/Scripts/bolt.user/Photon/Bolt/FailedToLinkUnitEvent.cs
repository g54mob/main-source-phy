using Photon.Bolt.Exceptions;
using Photon.Bolt.Internal;
using UnityEngine;

namespace Photon.Bolt
{
	public class FailedToLinkUnitEvent : Event
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

		public int Team
		{
			get
			{
				return Storage.Values[OffsetStorage + 1].Int0;
			}
			set
			{
				value = Mathf.Clamp(value, 0, 1);
				int @int = Storage.Values[OffsetStorage + 1].Int0;
				Storage.Values[OffsetStorage + 1].Int0 = value;
				if (!NetworkValue.Diff(@int, value))
				{
				}
			}
		}

		public FailedToLinkUnitEvent()
			: base(FailedToLinkUnitEvent_Meta.Instance)
		{
		}

		public override string ToString()
		{
			return $"[FailedToLinkUnitEvent UnitInstanceId={UnitInstanceId} Team={Team}]";
		}

		private static FailedToLinkUnitEvent Create(byte targets, BoltConnection connection, ReliabilityModes reliability)
		{
			if (!BoltCore.isClient)
			{
				throw new BoltException("You are not a client, you can not raise this event");
			}
			if (!(Factory.NewEvent(((IFactory)FailedToLinkUnitEvent_Meta.Instance).TypeKey) is FailedToLinkUnitEvent failedToLinkUnitEvent))
			{
				return null;
			}
			failedToLinkUnitEvent.Targets = targets;
			failedToLinkUnitEvent.TargetConnection = connection;
			failedToLinkUnitEvent.Reliability = reliability;
			return failedToLinkUnitEvent;
		}

		public static FailedToLinkUnitEvent Create(GlobalTargets targets)
		{
			return Create((byte)targets, null, ReliabilityModes.ReliableOrdered);
		}

		public static FailedToLinkUnitEvent Create(GlobalTargets targets, ReliabilityModes reliability)
		{
			return Create((byte)targets, null, reliability);
		}

		public static FailedToLinkUnitEvent Create(BoltConnection connection)
		{
			return Create(10, connection, ReliabilityModes.ReliableOrdered);
		}

		public static FailedToLinkUnitEvent Create(BoltConnection connection, ReliabilityModes reliability)
		{
			return Create(10, connection, reliability);
		}

		public static FailedToLinkUnitEvent Create()
		{
			return Create(2, null, ReliabilityModes.ReliableOrdered);
		}

		public static FailedToLinkUnitEvent Create(ReliabilityModes reliability)
		{
			return Create(2, null, reliability);
		}

		private static bool Post(byte targets, BoltConnection connection, ReliabilityModes reliability, int UnitInstanceId, int Team)
		{
			FailedToLinkUnitEvent failedToLinkUnitEvent = Create(targets, connection, reliability);
			if (failedToLinkUnitEvent == null)
			{
				return false;
			}
			failedToLinkUnitEvent.UnitInstanceId = UnitInstanceId;
			failedToLinkUnitEvent.Team = Team;
			failedToLinkUnitEvent.Send();
			return true;
		}

		public static bool Post(GlobalTargets targets, int UnitInstanceId, int Team)
		{
			return Post((byte)targets, null, ReliabilityModes.ReliableOrdered, UnitInstanceId, Team);
		}

		public static bool Post(GlobalTargets targets, ReliabilityModes reliability, int UnitInstanceId, int Team)
		{
			return Post((byte)targets, null, reliability, UnitInstanceId, Team);
		}

		public static bool Post(BoltConnection connection, int UnitInstanceId, int Team)
		{
			return Post(10, connection, ReliabilityModes.ReliableOrdered, UnitInstanceId, Team);
		}

		public static bool Post(BoltConnection connection, ReliabilityModes reliability, int UnitInstanceId, int Team)
		{
			return Post(10, connection, reliability, UnitInstanceId, Team);
		}

		public static bool Post(int UnitInstanceId, int Team)
		{
			return Post(2, null, ReliabilityModes.ReliableOrdered, UnitInstanceId, Team);
		}

		public static bool Post(ReliabilityModes reliability, int UnitInstanceId, int Team)
		{
			return Post(2, null, reliability, UnitInstanceId, Team);
		}
	}
}
