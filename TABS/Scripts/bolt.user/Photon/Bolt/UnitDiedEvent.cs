using Photon.Bolt.Exceptions;
using Photon.Bolt.Internal;
using UnityEngine;

namespace Photon.Bolt
{
	public class UnitDiedEvent : Event
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

		public UnitDiedEvent()
			: base(UnitDiedEvent_Meta.Instance)
		{
		}

		public override string ToString()
		{
			return $"[UnitDiedEvent UnitSmallNetworkId={UnitSmallNetworkId}]";
		}

		private static UnitDiedEvent Create(byte targets, BoltConnection connection, ReliabilityModes reliability)
		{
			if (!BoltCore.isServer)
			{
				throw new BoltException("You are not the server, you can not raise this event");
			}
			if (!(Factory.NewEvent(((IFactory)UnitDiedEvent_Meta.Instance).TypeKey) is UnitDiedEvent unitDiedEvent))
			{
				return null;
			}
			unitDiedEvent.Targets = targets;
			unitDiedEvent.TargetConnection = connection;
			unitDiedEvent.Reliability = reliability;
			return unitDiedEvent;
		}

		public static UnitDiedEvent Create(GlobalTargets targets)
		{
			return Create((byte)targets, null, ReliabilityModes.ReliableOrdered);
		}

		public static UnitDiedEvent Create(GlobalTargets targets, ReliabilityModes reliability)
		{
			return Create((byte)targets, null, reliability);
		}

		public static UnitDiedEvent Create(BoltConnection connection)
		{
			return Create(10, connection, ReliabilityModes.ReliableOrdered);
		}

		public static UnitDiedEvent Create(BoltConnection connection, ReliabilityModes reliability)
		{
			return Create(10, connection, reliability);
		}

		public static UnitDiedEvent Create()
		{
			return Create(2, null, ReliabilityModes.ReliableOrdered);
		}

		public static UnitDiedEvent Create(ReliabilityModes reliability)
		{
			return Create(2, null, reliability);
		}

		private static bool Post(byte targets, BoltConnection connection, ReliabilityModes reliability, int UnitSmallNetworkId)
		{
			UnitDiedEvent unitDiedEvent = Create(targets, connection, reliability);
			if (unitDiedEvent == null)
			{
				return false;
			}
			unitDiedEvent.UnitSmallNetworkId = UnitSmallNetworkId;
			unitDiedEvent.Send();
			return true;
		}

		public static bool Post(GlobalTargets targets, int UnitSmallNetworkId)
		{
			return Post((byte)targets, null, ReliabilityModes.ReliableOrdered, UnitSmallNetworkId);
		}

		public static bool Post(GlobalTargets targets, ReliabilityModes reliability, int UnitSmallNetworkId)
		{
			return Post((byte)targets, null, reliability, UnitSmallNetworkId);
		}

		public static bool Post(BoltConnection connection, int UnitSmallNetworkId)
		{
			return Post(10, connection, ReliabilityModes.ReliableOrdered, UnitSmallNetworkId);
		}

		public static bool Post(BoltConnection connection, ReliabilityModes reliability, int UnitSmallNetworkId)
		{
			return Post(10, connection, reliability, UnitSmallNetworkId);
		}

		public static bool Post(int UnitSmallNetworkId)
		{
			return Post(2, null, ReliabilityModes.ReliableOrdered, UnitSmallNetworkId);
		}

		public static bool Post(ReliabilityModes reliability, int UnitSmallNetworkId)
		{
			return Post(2, null, reliability, UnitSmallNetworkId);
		}
	}
}
