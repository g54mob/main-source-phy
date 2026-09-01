using Photon.Bolt.Internal;

namespace Photon.Bolt
{
	public class RespondMapChange : Event
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

		public bool Status
		{
			get
			{
				return Storage.Values[OffsetStorage + 2].Bool;
			}
			set
			{
				bool a = Storage.Values[OffsetStorage + 2].Bool;
				Storage.Values[OffsetStorage + 2].Bool = value;
				if (!NetworkValue.Diff(a, value))
				{
				}
			}
		}

		public RespondMapChange()
			: base(RespondMapChange_Meta.Instance)
		{
		}

		public override string ToString()
		{
			return $"[RespondMapChange MapType={MapType} MapIndex={MapIndex} Status={Status}]";
		}

		private static RespondMapChange Create(byte targets, BoltConnection connection, ReliabilityModes reliability)
		{
			if (!(Factory.NewEvent(((IFactory)RespondMapChange_Meta.Instance).TypeKey) is RespondMapChange respondMapChange))
			{
				return null;
			}
			respondMapChange.Targets = targets;
			respondMapChange.TargetConnection = connection;
			respondMapChange.Reliability = reliability;
			return respondMapChange;
		}

		public static RespondMapChange Create(GlobalTargets targets)
		{
			return Create((byte)targets, null, ReliabilityModes.ReliableOrdered);
		}

		public static RespondMapChange Create(GlobalTargets targets, ReliabilityModes reliability)
		{
			return Create((byte)targets, null, reliability);
		}

		public static RespondMapChange Create(BoltConnection connection)
		{
			return Create(10, connection, ReliabilityModes.ReliableOrdered);
		}

		public static RespondMapChange Create(BoltConnection connection, ReliabilityModes reliability)
		{
			return Create(10, connection, reliability);
		}

		public static RespondMapChange Create()
		{
			return Create(2, null, ReliabilityModes.ReliableOrdered);
		}

		public static RespondMapChange Create(ReliabilityModes reliability)
		{
			return Create(2, null, reliability);
		}

		private static bool Post(byte targets, BoltConnection connection, ReliabilityModes reliability, int MapType, int MapIndex, bool Status)
		{
			RespondMapChange respondMapChange = Create(targets, connection, reliability);
			if (respondMapChange == null)
			{
				return false;
			}
			respondMapChange.MapType = MapType;
			respondMapChange.MapIndex = MapIndex;
			respondMapChange.Status = Status;
			respondMapChange.Send();
			return true;
		}

		public static bool Post(GlobalTargets targets, int MapType, int MapIndex, bool Status)
		{
			return Post((byte)targets, null, ReliabilityModes.ReliableOrdered, MapType, MapIndex, Status);
		}

		public static bool Post(GlobalTargets targets, ReliabilityModes reliability, int MapType, int MapIndex, bool Status)
		{
			return Post((byte)targets, null, reliability, MapType, MapIndex, Status);
		}

		public static bool Post(BoltConnection connection, int MapType, int MapIndex, bool Status)
		{
			return Post(10, connection, ReliabilityModes.ReliableOrdered, MapType, MapIndex, Status);
		}

		public static bool Post(BoltConnection connection, ReliabilityModes reliability, int MapType, int MapIndex, bool Status)
		{
			return Post(10, connection, reliability, MapType, MapIndex, Status);
		}

		public static bool Post(int MapType, int MapIndex, bool Status)
		{
			return Post(2, null, ReliabilityModes.ReliableOrdered, MapType, MapIndex, Status);
		}

		public static bool Post(ReliabilityModes reliability, int MapType, int MapIndex, bool Status)
		{
			return Post(2, null, reliability, MapType, MapIndex, Status);
		}
	}
}
