using Photon.Bolt.Internal;

namespace Photon.Bolt
{
	public class PlayerQuitEvent : Event
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

		public PlayerQuitEvent()
			: base(PlayerQuitEvent_Meta.Instance)
		{
		}

		public override string ToString()
		{
			return $"[PlayerQuitEvent Team={Team}]";
		}

		private static PlayerQuitEvent Create(byte targets, BoltConnection connection, ReliabilityModes reliability)
		{
			if (!(Factory.NewEvent(((IFactory)PlayerQuitEvent_Meta.Instance).TypeKey) is PlayerQuitEvent playerQuitEvent))
			{
				return null;
			}
			playerQuitEvent.Targets = targets;
			playerQuitEvent.TargetConnection = connection;
			playerQuitEvent.Reliability = reliability;
			return playerQuitEvent;
		}

		public static PlayerQuitEvent Create(GlobalTargets targets)
		{
			return Create((byte)targets, null, ReliabilityModes.ReliableOrdered);
		}

		public static PlayerQuitEvent Create(GlobalTargets targets, ReliabilityModes reliability)
		{
			return Create((byte)targets, null, reliability);
		}

		public static PlayerQuitEvent Create(BoltConnection connection)
		{
			return Create(10, connection, ReliabilityModes.ReliableOrdered);
		}

		public static PlayerQuitEvent Create(BoltConnection connection, ReliabilityModes reliability)
		{
			return Create(10, connection, reliability);
		}

		public static PlayerQuitEvent Create()
		{
			return Create(2, null, ReliabilityModes.ReliableOrdered);
		}

		public static PlayerQuitEvent Create(ReliabilityModes reliability)
		{
			return Create(2, null, reliability);
		}

		private static bool Post(byte targets, BoltConnection connection, ReliabilityModes reliability, int Team)
		{
			PlayerQuitEvent playerQuitEvent = Create(targets, connection, reliability);
			if (playerQuitEvent == null)
			{
				return false;
			}
			playerQuitEvent.Team = Team;
			playerQuitEvent.Send();
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
