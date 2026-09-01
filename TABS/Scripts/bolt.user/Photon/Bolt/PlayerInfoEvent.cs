using Photon.Bolt.Exceptions;
using Photon.Bolt.Internal;
using UnityEngine;

namespace Photon.Bolt
{
	public class PlayerInfoEvent : Event
	{
		public string PlayerName
		{
			get
			{
				return Storage.Values[OffsetStorage].String;
			}
			set
			{
				string a = Storage.Values[OffsetStorage].String;
				Storage.Values[OffsetStorage].String = value;
				if (!NetworkValue.Diff(a, value))
				{
				}
			}
		}

		public int MultiplayerPlatform
		{
			get
			{
				return Storage.Values[OffsetStorage + 1].Int0;
			}
			set
			{
				value = Mathf.Clamp(value, 0, 15);
				int @int = Storage.Values[OffsetStorage + 1].Int0;
				Storage.Values[OffsetStorage + 1].Int0 = value;
				if (!NetworkValue.Diff(@int, value))
				{
				}
			}
		}

		public PlayerInfoEvent()
			: base(PlayerInfoEvent_Meta.Instance)
		{
		}

		public override string ToString()
		{
			return $"[PlayerInfoEvent PlayerName={PlayerName} MultiplayerPlatform={MultiplayerPlatform}]";
		}

		private static PlayerInfoEvent Create(byte targets, BoltConnection connection, ReliabilityModes reliability)
		{
			if (!BoltCore.isClient)
			{
				throw new BoltException("You are not a client, you can not raise this event");
			}
			if (!(Factory.NewEvent(((IFactory)PlayerInfoEvent_Meta.Instance).TypeKey) is PlayerInfoEvent playerInfoEvent))
			{
				return null;
			}
			playerInfoEvent.Targets = targets;
			playerInfoEvent.TargetConnection = connection;
			playerInfoEvent.Reliability = reliability;
			return playerInfoEvent;
		}

		public static PlayerInfoEvent Create(GlobalTargets targets)
		{
			return Create((byte)targets, null, ReliabilityModes.ReliableOrdered);
		}

		public static PlayerInfoEvent Create(GlobalTargets targets, ReliabilityModes reliability)
		{
			return Create((byte)targets, null, reliability);
		}

		public static PlayerInfoEvent Create(BoltConnection connection)
		{
			return Create(10, connection, ReliabilityModes.ReliableOrdered);
		}

		public static PlayerInfoEvent Create(BoltConnection connection, ReliabilityModes reliability)
		{
			return Create(10, connection, reliability);
		}

		public static PlayerInfoEvent Create()
		{
			return Create(2, null, ReliabilityModes.ReliableOrdered);
		}

		public static PlayerInfoEvent Create(ReliabilityModes reliability)
		{
			return Create(2, null, reliability);
		}

		private static bool Post(byte targets, BoltConnection connection, ReliabilityModes reliability, string PlayerName, int MultiplayerPlatform)
		{
			PlayerInfoEvent playerInfoEvent = Create(targets, connection, reliability);
			if (playerInfoEvent == null)
			{
				return false;
			}
			playerInfoEvent.PlayerName = PlayerName;
			playerInfoEvent.MultiplayerPlatform = MultiplayerPlatform;
			playerInfoEvent.Send();
			return true;
		}

		public static bool Post(GlobalTargets targets, string PlayerName, int MultiplayerPlatform)
		{
			return Post((byte)targets, null, ReliabilityModes.ReliableOrdered, PlayerName, MultiplayerPlatform);
		}

		public static bool Post(GlobalTargets targets, ReliabilityModes reliability, string PlayerName, int MultiplayerPlatform)
		{
			return Post((byte)targets, null, reliability, PlayerName, MultiplayerPlatform);
		}

		public static bool Post(BoltConnection connection, string PlayerName, int MultiplayerPlatform)
		{
			return Post(10, connection, ReliabilityModes.ReliableOrdered, PlayerName, MultiplayerPlatform);
		}

		public static bool Post(BoltConnection connection, ReliabilityModes reliability, string PlayerName, int MultiplayerPlatform)
		{
			return Post(10, connection, reliability, PlayerName, MultiplayerPlatform);
		}

		public static bool Post(string PlayerName, int MultiplayerPlatform)
		{
			return Post(2, null, ReliabilityModes.ReliableOrdered, PlayerName, MultiplayerPlatform);
		}

		public static bool Post(ReliabilityModes reliability, string PlayerName, int MultiplayerPlatform)
		{
			return Post(2, null, reliability, PlayerName, MultiplayerPlatform);
		}
	}
}
