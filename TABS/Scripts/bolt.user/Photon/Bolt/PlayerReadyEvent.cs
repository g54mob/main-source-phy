using Photon.Bolt.Internal;

namespace Photon.Bolt
{
	public class PlayerReadyEvent : Event
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

		public bool IsReady
		{
			get
			{
				return Storage.Values[OffsetStorage + 1].Bool;
			}
			set
			{
				bool a = Storage.Values[OffsetStorage + 1].Bool;
				Storage.Values[OffsetStorage + 1].Bool = value;
				if (!NetworkValue.Diff(a, value))
				{
				}
			}
		}

		public PlayerReadyEvent()
			: base(PlayerReadyEvent_Meta.Instance)
		{
		}

		public override string ToString()
		{
			return $"[PlayerReadyEvent Team={Team} IsReady={IsReady}]";
		}

		private static PlayerReadyEvent Create(byte targets, BoltConnection connection, ReliabilityModes reliability)
		{
			if (!(Factory.NewEvent(((IFactory)PlayerReadyEvent_Meta.Instance).TypeKey) is PlayerReadyEvent playerReadyEvent))
			{
				return null;
			}
			playerReadyEvent.Targets = targets;
			playerReadyEvent.TargetConnection = connection;
			playerReadyEvent.Reliability = reliability;
			return playerReadyEvent;
		}

		public static PlayerReadyEvent Create(GlobalTargets targets)
		{
			return Create((byte)targets, null, ReliabilityModes.ReliableOrdered);
		}

		public static PlayerReadyEvent Create(GlobalTargets targets, ReliabilityModes reliability)
		{
			return Create((byte)targets, null, reliability);
		}

		public static PlayerReadyEvent Create(BoltConnection connection)
		{
			return Create(10, connection, ReliabilityModes.ReliableOrdered);
		}

		public static PlayerReadyEvent Create(BoltConnection connection, ReliabilityModes reliability)
		{
			return Create(10, connection, reliability);
		}

		public static PlayerReadyEvent Create()
		{
			return Create(2, null, ReliabilityModes.ReliableOrdered);
		}

		public static PlayerReadyEvent Create(ReliabilityModes reliability)
		{
			return Create(2, null, reliability);
		}

		private static bool Post(byte targets, BoltConnection connection, ReliabilityModes reliability, int Team, bool IsReady)
		{
			PlayerReadyEvent playerReadyEvent = Create(targets, connection, reliability);
			if (playerReadyEvent == null)
			{
				return false;
			}
			playerReadyEvent.Team = Team;
			playerReadyEvent.IsReady = IsReady;
			playerReadyEvent.Send();
			return true;
		}

		public static bool Post(GlobalTargets targets, int Team, bool IsReady)
		{
			return Post((byte)targets, null, ReliabilityModes.ReliableOrdered, Team, IsReady);
		}

		public static bool Post(GlobalTargets targets, ReliabilityModes reliability, int Team, bool IsReady)
		{
			return Post((byte)targets, null, reliability, Team, IsReady);
		}

		public static bool Post(BoltConnection connection, int Team, bool IsReady)
		{
			return Post(10, connection, ReliabilityModes.ReliableOrdered, Team, IsReady);
		}

		public static bool Post(BoltConnection connection, ReliabilityModes reliability, int Team, bool IsReady)
		{
			return Post(10, connection, reliability, Team, IsReady);
		}

		public static bool Post(int Team, bool IsReady)
		{
			return Post(2, null, ReliabilityModes.ReliableOrdered, Team, IsReady);
		}

		public static bool Post(ReliabilityModes reliability, int Team, bool IsReady)
		{
			return Post(2, null, reliability, Team, IsReady);
		}
	}
}
