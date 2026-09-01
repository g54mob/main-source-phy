using Photon.Bolt.Internal;
using UnityEngine;

namespace Photon.Bolt
{
	public class GamePhaseEvent : Event
	{
		public int Phase
		{
			get
			{
				return Storage.Values[OffsetStorage].Int0;
			}
			set
			{
				value = Mathf.Clamp(value, 0, 20);
				int @int = Storage.Values[OffsetStorage].Int0;
				Storage.Values[OffsetStorage].Int0 = value;
				if (!NetworkValue.Diff(@int, value))
				{
				}
			}
		}

		public GamePhaseEvent()
			: base(GamePhaseEvent_Meta.Instance)
		{
		}

		public override string ToString()
		{
			return $"[GamePhaseEvent Phase={Phase}]";
		}

		private static GamePhaseEvent Create(byte targets, BoltConnection connection, ReliabilityModes reliability)
		{
			if (!(Factory.NewEvent(((IFactory)GamePhaseEvent_Meta.Instance).TypeKey) is GamePhaseEvent gamePhaseEvent))
			{
				return null;
			}
			gamePhaseEvent.Targets = targets;
			gamePhaseEvent.TargetConnection = connection;
			gamePhaseEvent.Reliability = reliability;
			return gamePhaseEvent;
		}

		public static GamePhaseEvent Create(GlobalTargets targets)
		{
			return Create((byte)targets, null, ReliabilityModes.ReliableOrdered);
		}

		public static GamePhaseEvent Create(GlobalTargets targets, ReliabilityModes reliability)
		{
			return Create((byte)targets, null, reliability);
		}

		public static GamePhaseEvent Create(BoltConnection connection)
		{
			return Create(10, connection, ReliabilityModes.ReliableOrdered);
		}

		public static GamePhaseEvent Create(BoltConnection connection, ReliabilityModes reliability)
		{
			return Create(10, connection, reliability);
		}

		public static GamePhaseEvent Create()
		{
			return Create(2, null, ReliabilityModes.ReliableOrdered);
		}

		public static GamePhaseEvent Create(ReliabilityModes reliability)
		{
			return Create(2, null, reliability);
		}

		private static bool Post(byte targets, BoltConnection connection, ReliabilityModes reliability, int Phase)
		{
			GamePhaseEvent gamePhaseEvent = Create(targets, connection, reliability);
			if (gamePhaseEvent == null)
			{
				return false;
			}
			gamePhaseEvent.Phase = Phase;
			gamePhaseEvent.Send();
			return true;
		}

		public static bool Post(GlobalTargets targets, int Phase)
		{
			return Post((byte)targets, null, ReliabilityModes.ReliableOrdered, Phase);
		}

		public static bool Post(GlobalTargets targets, ReliabilityModes reliability, int Phase)
		{
			return Post((byte)targets, null, reliability, Phase);
		}

		public static bool Post(BoltConnection connection, int Phase)
		{
			return Post(10, connection, ReliabilityModes.ReliableOrdered, Phase);
		}

		public static bool Post(BoltConnection connection, ReliabilityModes reliability, int Phase)
		{
			return Post(10, connection, reliability, Phase);
		}

		public static bool Post(int Phase)
		{
			return Post(2, null, ReliabilityModes.ReliableOrdered, Phase);
		}

		public static bool Post(ReliabilityModes reliability, int Phase)
		{
			return Post(2, null, reliability, Phase);
		}
	}
}
