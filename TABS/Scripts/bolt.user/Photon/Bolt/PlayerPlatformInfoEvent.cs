using System;
using Photon.Bolt.Exceptions;
using Photon.Bolt.Internal;

namespace Photon.Bolt
{
	public class PlayerPlatformInfoEvent : Event
	{
		public string PlatformInfo
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

		public PlayerPlatformInfoEvent()
			: base(PlayerPlatformInfoEvent_Meta.Instance)
		{
		}

		public override string ToString()
		{
			return $"[PlayerPlatformInfoEvent PlatformInfo={PlatformInfo}]";
		}

		public static PlayerPlatformInfoEvent Create(BoltEntity entity, EntityTargets targets)
		{
			if (!entity)
			{
				throw new ArgumentNullException("entity");
			}
			if (!entity.IsAttached)
			{
				throw new BoltException("You can not raise events on entities which are not attached");
			}
			if (!(Factory.NewEvent(((IFactory)PlayerPlatformInfoEvent_Meta.Instance).TypeKey) is PlayerPlatformInfoEvent playerPlatformInfoEvent))
			{
				return null;
			}
			playerPlatformInfoEvent.Targets = (int)targets;
			playerPlatformInfoEvent.TargetEntity = entity.Entity;
			playerPlatformInfoEvent.Reliability = ReliabilityModes.Unreliable;
			return playerPlatformInfoEvent;
		}

		public static PlayerPlatformInfoEvent Create(BoltEntity entity)
		{
			return Create(entity, EntityTargets.Everyone);
		}

		private static PlayerPlatformInfoEvent Create(byte targets, BoltConnection connection, ReliabilityModes reliability)
		{
			if (!(Factory.NewEvent(((IFactory)PlayerPlatformInfoEvent_Meta.Instance).TypeKey) is PlayerPlatformInfoEvent playerPlatformInfoEvent))
			{
				return null;
			}
			playerPlatformInfoEvent.Targets = targets;
			playerPlatformInfoEvent.TargetConnection = connection;
			playerPlatformInfoEvent.Reliability = reliability;
			return playerPlatformInfoEvent;
		}

		public static PlayerPlatformInfoEvent Create(GlobalTargets targets)
		{
			return Create((byte)targets, null, ReliabilityModes.ReliableOrdered);
		}

		public static PlayerPlatformInfoEvent Create(GlobalTargets targets, ReliabilityModes reliability)
		{
			return Create((byte)targets, null, reliability);
		}

		public static PlayerPlatformInfoEvent Create(BoltConnection connection)
		{
			return Create(10, connection, ReliabilityModes.ReliableOrdered);
		}

		public static PlayerPlatformInfoEvent Create(BoltConnection connection, ReliabilityModes reliability)
		{
			return Create(10, connection, reliability);
		}

		public static PlayerPlatformInfoEvent Create()
		{
			return Create(2, null, ReliabilityModes.ReliableOrdered);
		}

		public static PlayerPlatformInfoEvent Create(ReliabilityModes reliability)
		{
			return Create(2, null, reliability);
		}

		public static bool Post(BoltEntity entity, EntityTargets targets, string PlatformInfo)
		{
			PlayerPlatformInfoEvent playerPlatformInfoEvent = Create(entity, targets);
			if (playerPlatformInfoEvent == null)
			{
				return false;
			}
			playerPlatformInfoEvent.PlatformInfo = PlatformInfo;
			playerPlatformInfoEvent.Send();
			return true;
		}

		public static bool Post(BoltEntity entity, string PlatformInfo)
		{
			return Post(entity, EntityTargets.Everyone, PlatformInfo);
		}

		private static bool Post(byte targets, BoltConnection connection, ReliabilityModes reliability, string PlatformInfo)
		{
			PlayerPlatformInfoEvent playerPlatformInfoEvent = Create(targets, connection, reliability);
			if (playerPlatformInfoEvent == null)
			{
				return false;
			}
			playerPlatformInfoEvent.PlatformInfo = PlatformInfo;
			playerPlatformInfoEvent.Send();
			return true;
		}

		public static bool Post(GlobalTargets targets, string PlatformInfo)
		{
			return Post((byte)targets, null, ReliabilityModes.ReliableOrdered, PlatformInfo);
		}

		public static bool Post(GlobalTargets targets, ReliabilityModes reliability, string PlatformInfo)
		{
			return Post((byte)targets, null, reliability, PlatformInfo);
		}

		public static bool Post(BoltConnection connection, string PlatformInfo)
		{
			return Post(10, connection, ReliabilityModes.ReliableOrdered, PlatformInfo);
		}

		public static bool Post(BoltConnection connection, ReliabilityModes reliability, string PlatformInfo)
		{
			return Post(10, connection, reliability, PlatformInfo);
		}

		public static bool Post(string PlatformInfo)
		{
			return Post(2, null, ReliabilityModes.ReliableOrdered, PlatformInfo);
		}

		public static bool Post(ReliabilityModes reliability, string PlatformInfo)
		{
			return Post(2, null, reliability, PlatformInfo);
		}
	}
}
