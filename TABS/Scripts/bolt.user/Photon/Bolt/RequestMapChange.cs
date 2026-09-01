using Photon.Bolt.Internal;

namespace Photon.Bolt
{
	public class RequestMapChange : Event
	{
		public int MapType
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

		public int MapIndex
		{
			get
			{
				return Storage.Values[OffsetStorage + 1].Int0;
			}
			set
			{
				int @int = Storage.Values[OffsetStorage + 1].Int0;
				Storage.Values[OffsetStorage + 1].Int0 = value;
				if (!NetworkValue.Diff(@int, value))
				{
				}
			}
		}

		public RequestMapChange()
			: base(RequestMapChange_Meta.Instance)
		{
		}

		public override string ToString()
		{
			return $"[RequestMapChange MapType={MapType} MapIndex={MapIndex}]";
		}

		private static RequestMapChange Create(byte targets, BoltConnection connection, ReliabilityModes reliability)
		{
			if (!(Factory.NewEvent(((IFactory)RequestMapChange_Meta.Instance).TypeKey) is RequestMapChange requestMapChange))
			{
				return null;
			}
			requestMapChange.Targets = targets;
			requestMapChange.TargetConnection = connection;
			requestMapChange.Reliability = reliability;
			return requestMapChange;
		}

		public static RequestMapChange Create(GlobalTargets targets)
		{
			return Create((byte)targets, null, ReliabilityModes.ReliableOrdered);
		}

		public static RequestMapChange Create(GlobalTargets targets, ReliabilityModes reliability)
		{
			return Create((byte)targets, null, reliability);
		}

		public static RequestMapChange Create(BoltConnection connection)
		{
			return Create(10, connection, ReliabilityModes.ReliableOrdered);
		}

		public static RequestMapChange Create(BoltConnection connection, ReliabilityModes reliability)
		{
			return Create(10, connection, reliability);
		}

		public static RequestMapChange Create()
		{
			return Create(2, null, ReliabilityModes.ReliableOrdered);
		}

		public static RequestMapChange Create(ReliabilityModes reliability)
		{
			return Create(2, null, reliability);
		}

		private static bool Post(byte targets, BoltConnection connection, ReliabilityModes reliability, int MapType, int MapIndex)
		{
			RequestMapChange requestMapChange = Create(targets, connection, reliability);
			if (requestMapChange == null)
			{
				return false;
			}
			requestMapChange.MapType = MapType;
			requestMapChange.MapIndex = MapIndex;
			requestMapChange.Send();
			return true;
		}

		public static bool Post(GlobalTargets targets, int MapType, int MapIndex)
		{
			return Post((byte)targets, null, ReliabilityModes.ReliableOrdered, MapType, MapIndex);
		}

		public static bool Post(GlobalTargets targets, ReliabilityModes reliability, int MapType, int MapIndex)
		{
			return Post((byte)targets, null, reliability, MapType, MapIndex);
		}

		public static bool Post(BoltConnection connection, int MapType, int MapIndex)
		{
			return Post(10, connection, ReliabilityModes.ReliableOrdered, MapType, MapIndex);
		}

		public static bool Post(BoltConnection connection, ReliabilityModes reliability, int MapType, int MapIndex)
		{
			return Post(10, connection, reliability, MapType, MapIndex);
		}

		public static bool Post(int MapType, int MapIndex)
		{
			return Post(2, null, ReliabilityModes.ReliableOrdered, MapType, MapIndex);
		}

		public static bool Post(ReliabilityModes reliability, int MapType, int MapIndex)
		{
			return Post(2, null, reliability, MapType, MapIndex);
		}
	}
}
