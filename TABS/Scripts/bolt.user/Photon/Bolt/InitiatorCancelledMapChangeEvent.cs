using Photon.Bolt.Internal;

namespace Photon.Bolt
{
	public class InitiatorCancelledMapChangeEvent : Event
	{
		public InitiatorCancelledMapChangeEvent()
			: base(InitiatorCancelledMapChangeEvent_Meta.Instance)
		{
		}

		public override string ToString()
		{
			return $"[InitiatorCancelledMapChangeEvent]";
		}

		private static InitiatorCancelledMapChangeEvent Create(byte targets, BoltConnection connection, ReliabilityModes reliability)
		{
			if (!(Factory.NewEvent(((IFactory)InitiatorCancelledMapChangeEvent_Meta.Instance).TypeKey) is InitiatorCancelledMapChangeEvent initiatorCancelledMapChangeEvent))
			{
				return null;
			}
			initiatorCancelledMapChangeEvent.Targets = targets;
			initiatorCancelledMapChangeEvent.TargetConnection = connection;
			initiatorCancelledMapChangeEvent.Reliability = reliability;
			return initiatorCancelledMapChangeEvent;
		}

		public static InitiatorCancelledMapChangeEvent Create(GlobalTargets targets)
		{
			return Create((byte)targets, null, ReliabilityModes.ReliableOrdered);
		}

		public static InitiatorCancelledMapChangeEvent Create(GlobalTargets targets, ReliabilityModes reliability)
		{
			return Create((byte)targets, null, reliability);
		}

		public static InitiatorCancelledMapChangeEvent Create(BoltConnection connection)
		{
			return Create(10, connection, ReliabilityModes.ReliableOrdered);
		}

		public static InitiatorCancelledMapChangeEvent Create(BoltConnection connection, ReliabilityModes reliability)
		{
			return Create(10, connection, reliability);
		}

		public static InitiatorCancelledMapChangeEvent Create()
		{
			return Create(2, null, ReliabilityModes.ReliableOrdered);
		}

		public static InitiatorCancelledMapChangeEvent Create(ReliabilityModes reliability)
		{
			return Create(2, null, reliability);
		}

		private static bool Post(byte targets, BoltConnection connection, ReliabilityModes reliability)
		{
			InitiatorCancelledMapChangeEvent initiatorCancelledMapChangeEvent = Create(targets, connection, reliability);
			if (initiatorCancelledMapChangeEvent == null)
			{
				return false;
			}
			initiatorCancelledMapChangeEvent.Send();
			return true;
		}

		public static bool Post(GlobalTargets targets)
		{
			return Post((byte)targets, null, ReliabilityModes.ReliableOrdered);
		}

		public static bool Post(GlobalTargets targets, ReliabilityModes reliability)
		{
			return Post((byte)targets, null, reliability);
		}

		public static bool Post(BoltConnection connection)
		{
			return Post(10, connection, ReliabilityModes.ReliableOrdered);
		}

		public static bool Post(BoltConnection connection, ReliabilityModes reliability)
		{
			return Post(10, connection, reliability);
		}

		public static bool Post()
		{
			return Post(2, null, ReliabilityModes.ReliableOrdered);
		}

		public static bool Post(ReliabilityModes reliability)
		{
			return Post(2, null, reliability);
		}
	}
}
