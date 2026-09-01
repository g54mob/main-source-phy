using Photon.Bolt.Exceptions;
using Photon.Bolt.Internal;

namespace Photon.Bolt
{
	public class EndBattleEvent : Event
	{
		public int WinningTeam
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

		public EndBattleEvent()
			: base(EndBattleEvent_Meta.Instance)
		{
		}

		public override string ToString()
		{
			return $"[EndBattleEvent WinningTeam={WinningTeam}]";
		}

		private static EndBattleEvent Create(byte targets, BoltConnection connection, ReliabilityModes reliability)
		{
			if (!BoltCore.isServer)
			{
				throw new BoltException("You are not the server, you can not raise this event");
			}
			if (!(Factory.NewEvent(((IFactory)EndBattleEvent_Meta.Instance).TypeKey) is EndBattleEvent endBattleEvent))
			{
				return null;
			}
			endBattleEvent.Targets = targets;
			endBattleEvent.TargetConnection = connection;
			endBattleEvent.Reliability = reliability;
			return endBattleEvent;
		}

		public static EndBattleEvent Create(GlobalTargets targets)
		{
			return Create((byte)targets, null, ReliabilityModes.ReliableOrdered);
		}

		public static EndBattleEvent Create(GlobalTargets targets, ReliabilityModes reliability)
		{
			return Create((byte)targets, null, reliability);
		}

		public static EndBattleEvent Create(BoltConnection connection)
		{
			return Create(10, connection, ReliabilityModes.ReliableOrdered);
		}

		public static EndBattleEvent Create(BoltConnection connection, ReliabilityModes reliability)
		{
			return Create(10, connection, reliability);
		}

		public static EndBattleEvent Create()
		{
			return Create(2, null, ReliabilityModes.ReliableOrdered);
		}

		public static EndBattleEvent Create(ReliabilityModes reliability)
		{
			return Create(2, null, reliability);
		}

		private static bool Post(byte targets, BoltConnection connection, ReliabilityModes reliability, int WinningTeam)
		{
			EndBattleEvent endBattleEvent = Create(targets, connection, reliability);
			if (endBattleEvent == null)
			{
				return false;
			}
			endBattleEvent.WinningTeam = WinningTeam;
			endBattleEvent.Send();
			return true;
		}

		public static bool Post(GlobalTargets targets, int WinningTeam)
		{
			return Post((byte)targets, null, ReliabilityModes.ReliableOrdered, WinningTeam);
		}

		public static bool Post(GlobalTargets targets, ReliabilityModes reliability, int WinningTeam)
		{
			return Post((byte)targets, null, reliability, WinningTeam);
		}

		public static bool Post(BoltConnection connection, int WinningTeam)
		{
			return Post(10, connection, ReliabilityModes.ReliableOrdered, WinningTeam);
		}

		public static bool Post(BoltConnection connection, ReliabilityModes reliability, int WinningTeam)
		{
			return Post(10, connection, reliability, WinningTeam);
		}

		public static bool Post(int WinningTeam)
		{
			return Post(2, null, ReliabilityModes.ReliableOrdered, WinningTeam);
		}

		public static bool Post(ReliabilityModes reliability, int WinningTeam)
		{
			return Post(2, null, reliability, WinningTeam);
		}
	}
}
