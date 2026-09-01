using Photon.Bolt.Exceptions;
using Photon.Bolt.Internal;

namespace Photon.Bolt
{
	public class StartBattleEvent : Event
	{
		public StartBattleEvent()
			: base(StartBattleEvent_Meta.Instance)
		{
		}

		public override string ToString()
		{
			return $"[StartBattleEvent]";
		}

		private static StartBattleEvent Create(byte targets, BoltConnection connection, ReliabilityModes reliability)
		{
			if (!BoltCore.isServer)
			{
				throw new BoltException("You are not the server, you can not raise this event");
			}
			if (!(Factory.NewEvent(((IFactory)StartBattleEvent_Meta.Instance).TypeKey) is StartBattleEvent startBattleEvent))
			{
				return null;
			}
			startBattleEvent.Targets = targets;
			startBattleEvent.TargetConnection = connection;
			startBattleEvent.Reliability = reliability;
			return startBattleEvent;
		}

		public static StartBattleEvent Create(GlobalTargets targets)
		{
			return Create((byte)targets, null, ReliabilityModes.ReliableOrdered);
		}

		public static StartBattleEvent Create(GlobalTargets targets, ReliabilityModes reliability)
		{
			return Create((byte)targets, null, reliability);
		}

		public static StartBattleEvent Create(BoltConnection connection)
		{
			return Create(10, connection, ReliabilityModes.ReliableOrdered);
		}

		public static StartBattleEvent Create(BoltConnection connection, ReliabilityModes reliability)
		{
			return Create(10, connection, reliability);
		}

		public static StartBattleEvent Create()
		{
			return Create(2, null, ReliabilityModes.ReliableOrdered);
		}

		public static StartBattleEvent Create(ReliabilityModes reliability)
		{
			return Create(2, null, reliability);
		}

		private static bool Post(byte targets, BoltConnection connection, ReliabilityModes reliability)
		{
			StartBattleEvent startBattleEvent = Create(targets, connection, reliability);
			if (startBattleEvent == null)
			{
				return false;
			}
			startBattleEvent.Send();
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
