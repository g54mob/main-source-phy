using Photon.Bolt.Exceptions;
using Photon.Bolt.Internal;
using UnityEngine;

namespace Photon.Bolt
{
	public class ReplyPlaceUnitEvent : Event
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

		public ReplyPlaceUnitEvent()
			: base(ReplyPlaceUnitEvent_Meta.Instance)
		{
		}

		public override string ToString()
		{
			return $"[ReplyPlaceUnitEvent UnitInstanceId={UnitInstanceId}]";
		}

		private static ReplyPlaceUnitEvent Create(byte targets, BoltConnection connection, ReliabilityModes reliability)
		{
			if (!BoltCore.isServer)
			{
				throw new BoltException("You are not the server, you can not raise this event");
			}
			if (!(Factory.NewEvent(((IFactory)ReplyPlaceUnitEvent_Meta.Instance).TypeKey) is ReplyPlaceUnitEvent replyPlaceUnitEvent))
			{
				return null;
			}
			replyPlaceUnitEvent.Targets = targets;
			replyPlaceUnitEvent.TargetConnection = connection;
			replyPlaceUnitEvent.Reliability = reliability;
			return replyPlaceUnitEvent;
		}

		public static ReplyPlaceUnitEvent Create(GlobalTargets targets)
		{
			return Create((byte)targets, null, ReliabilityModes.ReliableOrdered);
		}

		public static ReplyPlaceUnitEvent Create(GlobalTargets targets, ReliabilityModes reliability)
		{
			return Create((byte)targets, null, reliability);
		}

		public static ReplyPlaceUnitEvent Create(BoltConnection connection)
		{
			return Create(10, connection, ReliabilityModes.ReliableOrdered);
		}

		public static ReplyPlaceUnitEvent Create(BoltConnection connection, ReliabilityModes reliability)
		{
			return Create(10, connection, reliability);
		}

		public static ReplyPlaceUnitEvent Create()
		{
			return Create(2, null, ReliabilityModes.ReliableOrdered);
		}

		public static ReplyPlaceUnitEvent Create(ReliabilityModes reliability)
		{
			return Create(2, null, reliability);
		}

		private static bool Post(byte targets, BoltConnection connection, ReliabilityModes reliability, int UnitInstanceId)
		{
			ReplyPlaceUnitEvent replyPlaceUnitEvent = Create(targets, connection, reliability);
			if (replyPlaceUnitEvent == null)
			{
				return false;
			}
			replyPlaceUnitEvent.UnitInstanceId = UnitInstanceId;
			replyPlaceUnitEvent.Send();
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
