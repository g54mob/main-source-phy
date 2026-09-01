using Photon.Bolt.Internal;

namespace Photon.Bolt
{
	public class SpawnProjectileEvent : Event
	{
		public IProtocolToken SpawnToken
		{
			get
			{
				return Storage.Values[OffsetStorage].ProtocolToken;
			}
			set
			{
				IProtocolToken protocolToken = Storage.Values[OffsetStorage].ProtocolToken;
				protocolToken.Release();
				Storage.Values[OffsetStorage].ProtocolToken = value;
				if (!NetworkValue.Diff(protocolToken, value))
				{
				}
			}
		}

		public SpawnProjectileEvent()
			: base(SpawnProjectileEvent_Meta.Instance)
		{
		}

		public override string ToString()
		{
			return $"[SpawnProjectileEvent SpawnToken={SpawnToken}]";
		}

		protected override void PrepareRelease()
		{
			Storage.Values[OffsetStorage].ProtocolToken.Release();
		}

		private static SpawnProjectileEvent Create(byte targets, BoltConnection connection, ReliabilityModes reliability)
		{
			if (!(Factory.NewEvent(((IFactory)SpawnProjectileEvent_Meta.Instance).TypeKey) is SpawnProjectileEvent spawnProjectileEvent))
			{
				return null;
			}
			spawnProjectileEvent.Targets = targets;
			spawnProjectileEvent.TargetConnection = connection;
			spawnProjectileEvent.Reliability = reliability;
			return spawnProjectileEvent;
		}

		public static SpawnProjectileEvent Create(GlobalTargets targets)
		{
			return Create((byte)targets, null, ReliabilityModes.ReliableOrdered);
		}

		public static SpawnProjectileEvent Create(GlobalTargets targets, ReliabilityModes reliability)
		{
			return Create((byte)targets, null, reliability);
		}

		public static SpawnProjectileEvent Create(BoltConnection connection)
		{
			return Create(10, connection, ReliabilityModes.ReliableOrdered);
		}

		public static SpawnProjectileEvent Create(BoltConnection connection, ReliabilityModes reliability)
		{
			return Create(10, connection, reliability);
		}

		public static SpawnProjectileEvent Create()
		{
			return Create(2, null, ReliabilityModes.ReliableOrdered);
		}

		public static SpawnProjectileEvent Create(ReliabilityModes reliability)
		{
			return Create(2, null, reliability);
		}

		private static bool Post(byte targets, BoltConnection connection, ReliabilityModes reliability, IProtocolToken SpawnToken)
		{
			SpawnProjectileEvent spawnProjectileEvent = Create(targets, connection, reliability);
			if (spawnProjectileEvent == null)
			{
				return false;
			}
			spawnProjectileEvent.SpawnToken = SpawnToken;
			spawnProjectileEvent.Send();
			return true;
		}

		public static bool Post(GlobalTargets targets, IProtocolToken SpawnToken)
		{
			return Post((byte)targets, null, ReliabilityModes.ReliableOrdered, SpawnToken);
		}

		public static bool Post(GlobalTargets targets, ReliabilityModes reliability, IProtocolToken SpawnToken)
		{
			return Post((byte)targets, null, reliability, SpawnToken);
		}

		public static bool Post(BoltConnection connection, IProtocolToken SpawnToken)
		{
			return Post(10, connection, ReliabilityModes.ReliableOrdered, SpawnToken);
		}

		public static bool Post(BoltConnection connection, ReliabilityModes reliability, IProtocolToken SpawnToken)
		{
			return Post(10, connection, reliability, SpawnToken);
		}

		public static bool Post(IProtocolToken SpawnToken)
		{
			return Post(2, null, ReliabilityModes.ReliableOrdered, SpawnToken);
		}

		public static bool Post(ReliabilityModes reliability, IProtocolToken SpawnToken)
		{
			return Post(2, null, reliability, SpawnToken);
		}
	}
}
