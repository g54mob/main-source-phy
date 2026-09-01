using Photon.Bolt.Internal;

namespace Photon.Bolt
{
	public class RemoveAllUnitsEvent : Event
	{
		public int Team
		{
			get
			{
				return Storage.Values[OffsetStorage].Int0;
			}
			set
			{
				int @int = Storage.Values[OffsetStorage].Int0;
				Storage.Values[OffsetStorage].Int0 = value;
				if (!NetworkValue.Diff(@int, value))
				{
				}
			}
		}

		public RemoveAllUnitsEvent()
			: base(RemoveAllUnitsEvent_Meta.Instance)
		{
		}

		public override string ToString()
		{
			return $"[RemoveAllUnitsEvent Team={Team}]";
		}

		private static RemoveAllUnitsEvent Create(byte targets, BoltConnection connection, ReliabilityModes reliability)
		{
			if (!(Factory.NewEvent(((IFactory)RemoveAllUnitsEvent_Meta.Instance).TypeKey) is RemoveAllUnitsEvent removeAllUnitsEvent))
			{
				return null;
			}
			removeAllUnitsEvent.Targets = targets;
			removeAllUnitsEvent.TargetConnection = connection;
			removeAllUnitsEvent.Reliability = reliability;
			return removeAllUnitsEvent;
		}

		public static RemoveAllUnitsEvent Create(GlobalTargets targets)
		{
			return Create((byte)targets, null, ReliabilityModes.ReliableOrdered);
		}

		public static RemoveAllUnitsEvent Create(GlobalTargets targets, ReliabilityModes reliability)
		{
			return Create((byte)targets, null, reliability);
		}

		public static RemoveAllUnitsEvent Create(BoltConnection connection)
		{
			return Create(10, connection, ReliabilityModes.ReliableOrdered);
		}

		public static RemoveAllUnitsEvent Create(BoltConnection connection, ReliabilityModes reliability)
		{
			return Create(10, connection, reliability);
		}

		public static RemoveAllUnitsEvent Create()
		{
			return Create(2, null, ReliabilityModes.ReliableOrdered);
		}

		public static RemoveAllUnitsEvent Create(ReliabilityModes reliability)
		{
			return Create(2, null, reliability);
		}

		private static bool Post(byte targets, BoltConnection connection, ReliabilityModes reliability, int Team)
		{
			RemoveAllUnitsEvent removeAllUnitsEvent = Create(targets, connection, reliability);
			if (removeAllUnitsEvent == null)
			{
				return false;
			}
			removeAllUnitsEvent.Team = Team;
			removeAllUnitsEvent.Send();
			return true;
		}

		public static bool Post(GlobalTargets targets, int Team)
		{
			return Post((byte)targets, null, ReliabilityModes.ReliableOrdered, Team);
		}

		public static bool Post(GlobalTargets targets, ReliabilityModes reliability, int Team)
		{
			return Post((byte)targets, null, reliability, Team);
		}

		public static bool Post(BoltConnection connection, int Team)
		{
			return Post(10, connection, ReliabilityModes.ReliableOrdered, Team);
		}

		public static bool Post(BoltConnection connection, ReliabilityModes reliability, int Team)
		{
			return Post(10, connection, reliability, Team);
		}

		public static bool Post(int Team)
		{
			return Post(2, null, ReliabilityModes.ReliableOrdered, Team);
		}

		public static bool Post(ReliabilityModes reliability, int Team)
		{
			return Post(2, null, reliability, Team);
		}
	}
}
