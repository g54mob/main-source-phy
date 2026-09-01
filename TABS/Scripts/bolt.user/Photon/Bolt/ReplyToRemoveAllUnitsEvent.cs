using Photon.Bolt.Internal;

namespace Photon.Bolt
{
	public class ReplyToRemoveAllUnitsEvent : Event
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

		public ReplyToRemoveAllUnitsEvent()
			: base(ReplyToRemoveAllUnitsEvent_Meta.Instance)
		{
		}

		public override string ToString()
		{
			return $"[ReplyToRemoveAllUnitsEvent Team={Team}]";
		}

		private static ReplyToRemoveAllUnitsEvent Create(byte targets, BoltConnection connection, ReliabilityModes reliability)
		{
			if (!(Factory.NewEvent(((IFactory)ReplyToRemoveAllUnitsEvent_Meta.Instance).TypeKey) is ReplyToRemoveAllUnitsEvent replyToRemoveAllUnitsEvent))
			{
				return null;
			}
			replyToRemoveAllUnitsEvent.Targets = targets;
			replyToRemoveAllUnitsEvent.TargetConnection = connection;
			replyToRemoveAllUnitsEvent.Reliability = reliability;
			return replyToRemoveAllUnitsEvent;
		}

		public static ReplyToRemoveAllUnitsEvent Create(GlobalTargets targets)
		{
			return Create((byte)targets, null, ReliabilityModes.ReliableOrdered);
		}

		public static ReplyToRemoveAllUnitsEvent Create(GlobalTargets targets, ReliabilityModes reliability)
		{
			return Create((byte)targets, null, reliability);
		}

		public static ReplyToRemoveAllUnitsEvent Create(BoltConnection connection)
		{
			return Create(10, connection, ReliabilityModes.ReliableOrdered);
		}

		public static ReplyToRemoveAllUnitsEvent Create(BoltConnection connection, ReliabilityModes reliability)
		{
			return Create(10, connection, reliability);
		}

		public static ReplyToRemoveAllUnitsEvent Create()
		{
			return Create(2, null, ReliabilityModes.ReliableOrdered);
		}

		public static ReplyToRemoveAllUnitsEvent Create(ReliabilityModes reliability)
		{
			return Create(2, null, reliability);
		}

		private static bool Post(byte targets, BoltConnection connection, ReliabilityModes reliability, int Team)
		{
			ReplyToRemoveAllUnitsEvent replyToRemoveAllUnitsEvent = Create(targets, connection, reliability);
			if (replyToRemoveAllUnitsEvent == null)
			{
				return false;
			}
			replyToRemoveAllUnitsEvent.Team = Team;
			replyToRemoveAllUnitsEvent.Send();
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
