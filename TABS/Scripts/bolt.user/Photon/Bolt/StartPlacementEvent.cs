using Photon.Bolt.Exceptions;
using Photon.Bolt.Internal;

namespace Photon.Bolt
{
	public class StartPlacementEvent : Event
	{
		public StartPlacementEvent()
			: base(StartPlacementEvent_Meta.Instance)
		{
		}

		public override string ToString()
		{
			return $"[StartPlacementEvent]";
		}

		private static StartPlacementEvent Create(byte targets, BoltConnection connection, ReliabilityModes reliability)
		{
			if (!BoltCore.isServer)
			{
				throw new BoltException("You are not the server, you can not raise this event");
			}
			if (!(Factory.NewEvent(((IFactory)StartPlacementEvent_Meta.Instance).TypeKey) is StartPlacementEvent startPlacementEvent))
			{
				return null;
			}
			startPlacementEvent.Targets = targets;
			startPlacementEvent.TargetConnection = connection;
			startPlacementEvent.Reliability = reliability;
			return startPlacementEvent;
		}

		public static StartPlacementEvent Create(GlobalTargets targets)
		{
			return Create((byte)targets, null, ReliabilityModes.ReliableOrdered);
		}

		public static StartPlacementEvent Create(GlobalTargets targets, ReliabilityModes reliability)
		{
			return Create((byte)targets, null, reliability);
		}

		public static StartPlacementEvent Create(BoltConnection connection)
		{
			return Create(10, connection, ReliabilityModes.ReliableOrdered);
		}

		public static StartPlacementEvent Create(BoltConnection connection, ReliabilityModes reliability)
		{
			return Create(10, connection, reliability);
		}

		public static StartPlacementEvent Create()
		{
			return Create(2, null, ReliabilityModes.ReliableOrdered);
		}

		public static StartPlacementEvent Create(ReliabilityModes reliability)
		{
			return Create(2, null, reliability);
		}

		private static bool Post(byte targets, BoltConnection connection, ReliabilityModes reliability)
		{
			StartPlacementEvent startPlacementEvent = Create(targets, connection, reliability);
			if (startPlacementEvent == null)
			{
				return false;
			}
			startPlacementEvent.Send();
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
