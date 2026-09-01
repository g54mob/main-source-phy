using Photon.Bolt.Exceptions;
using Photon.Bolt.Internal;
using UnityEngine;

namespace Photon.Bolt
{
	public class RemoveUnitEvent : Event
	{
		public int UnitSmallNetworkId
		{
			get
			{
				return Storage.Values[OffsetStorage].Int0;
			}
			set
			{
				value = Mathf.Clamp(value, 0, 65535);
				int @int = Storage.Values[OffsetStorage].Int0;
				Storage.Values[OffsetStorage].Int0 = value;
				if (!NetworkValue.Diff(@int, value))
				{
				}
			}
		}

		public int UnitInstanceId
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

		public RemoveUnitEvent()
			: base(RemoveUnitEvent_Meta.Instance)
		{
		}

		public override string ToString()
		{
			return $"[RemoveUnitEvent UnitSmallNetworkId={UnitSmallNetworkId} UnitInstanceId={UnitInstanceId}]";
		}

		private static RemoveUnitEvent Create(byte targets, BoltConnection connection, ReliabilityModes reliability)
		{
			if (!BoltCore.isClient)
			{
				throw new BoltException("You are not a client, you can not raise this event");
			}
			if (!(Factory.NewEvent(((IFactory)RemoveUnitEvent_Meta.Instance).TypeKey) is RemoveUnitEvent removeUnitEvent))
			{
				return null;
			}
			removeUnitEvent.Targets = targets;
			removeUnitEvent.TargetConnection = connection;
			removeUnitEvent.Reliability = reliability;
			return removeUnitEvent;
		}

		public static RemoveUnitEvent Create(GlobalTargets targets)
		{
			return Create((byte)targets, null, ReliabilityModes.ReliableOrdered);
		}

		public static RemoveUnitEvent Create(GlobalTargets targets, ReliabilityModes reliability)
		{
			return Create((byte)targets, null, reliability);
		}

		public static RemoveUnitEvent Create(BoltConnection connection)
		{
			return Create(10, connection, ReliabilityModes.ReliableOrdered);
		}

		public static RemoveUnitEvent Create(BoltConnection connection, ReliabilityModes reliability)
		{
			return Create(10, connection, reliability);
		}

		public static RemoveUnitEvent Create()
		{
			return Create(2, null, ReliabilityModes.ReliableOrdered);
		}

		public static RemoveUnitEvent Create(ReliabilityModes reliability)
		{
			return Create(2, null, reliability);
		}

		private static bool Post(byte targets, BoltConnection connection, ReliabilityModes reliability, int UnitSmallNetworkId, int UnitInstanceId)
		{
			RemoveUnitEvent removeUnitEvent = Create(targets, connection, reliability);
			if (removeUnitEvent == null)
			{
				return false;
			}
			removeUnitEvent.UnitSmallNetworkId = UnitSmallNetworkId;
			removeUnitEvent.UnitInstanceId = UnitInstanceId;
			removeUnitEvent.Send();
			return true;
		}

		public static bool Post(GlobalTargets targets, int UnitSmallNetworkId, int UnitInstanceId)
		{
			return Post((byte)targets, null, ReliabilityModes.ReliableOrdered, UnitSmallNetworkId, UnitInstanceId);
		}

		public static bool Post(GlobalTargets targets, ReliabilityModes reliability, int UnitSmallNetworkId, int UnitInstanceId)
		{
			return Post((byte)targets, null, reliability, UnitSmallNetworkId, UnitInstanceId);
		}

		public static bool Post(BoltConnection connection, int UnitSmallNetworkId, int UnitInstanceId)
		{
			return Post(10, connection, ReliabilityModes.ReliableOrdered, UnitSmallNetworkId, UnitInstanceId);
		}

		public static bool Post(BoltConnection connection, ReliabilityModes reliability, int UnitSmallNetworkId, int UnitInstanceId)
		{
			return Post(10, connection, reliability, UnitSmallNetworkId, UnitInstanceId);
		}

		public static bool Post(int UnitSmallNetworkId, int UnitInstanceId)
		{
			return Post(2, null, ReliabilityModes.ReliableOrdered, UnitSmallNetworkId, UnitInstanceId);
		}

		public static bool Post(ReliabilityModes reliability, int UnitSmallNetworkId, int UnitInstanceId)
		{
			return Post(2, null, reliability, UnitSmallNetworkId, UnitInstanceId);
		}
	}
}
