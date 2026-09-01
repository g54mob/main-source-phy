using Photon.Bolt.Internal;

namespace Photon.Bolt
{
	public class RequestRulesChange : Event
	{
		public int MaxUnits
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

		public int MaxBudget
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

		public bool BlindMode
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

		public RequestRulesChange()
			: base(RequestRulesChange_Meta.Instance)
		{
		}

		public override string ToString()
		{
			return $"[RequestRulesChange MaxUnits={MaxUnits} MaxBudget={MaxBudget} BlindMode={BlindMode}]";
		}

		private static RequestRulesChange Create(byte targets, BoltConnection connection, ReliabilityModes reliability)
		{
			if (!(Factory.NewEvent(((IFactory)RequestRulesChange_Meta.Instance).TypeKey) is RequestRulesChange requestRulesChange))
			{
				return null;
			}
			requestRulesChange.Targets = targets;
			requestRulesChange.TargetConnection = connection;
			requestRulesChange.Reliability = reliability;
			return requestRulesChange;
		}

		public static RequestRulesChange Create(GlobalTargets targets)
		{
			return Create((byte)targets, null, ReliabilityModes.ReliableOrdered);
		}

		public static RequestRulesChange Create(GlobalTargets targets, ReliabilityModes reliability)
		{
			return Create((byte)targets, null, reliability);
		}

		public static RequestRulesChange Create(BoltConnection connection)
		{
			return Create(10, connection, ReliabilityModes.ReliableOrdered);
		}

		public static RequestRulesChange Create(BoltConnection connection, ReliabilityModes reliability)
		{
			return Create(10, connection, reliability);
		}

		public static RequestRulesChange Create()
		{
			return Create(2, null, ReliabilityModes.ReliableOrdered);
		}

		public static RequestRulesChange Create(ReliabilityModes reliability)
		{
			return Create(2, null, reliability);
		}

		private static bool Post(byte targets, BoltConnection connection, ReliabilityModes reliability, int MaxUnits, int MaxBudget, bool BlindMode)
		{
			RequestRulesChange requestRulesChange = Create(targets, connection, reliability);
			if (requestRulesChange == null)
			{
				return false;
			}
			requestRulesChange.MaxUnits = MaxUnits;
			requestRulesChange.MaxBudget = MaxBudget;
			requestRulesChange.BlindMode = BlindMode;
			requestRulesChange.Send();
			return true;
		}

		public static bool Post(GlobalTargets targets, int MaxUnits, int MaxBudget, bool BlindMode)
		{
			return Post((byte)targets, null, ReliabilityModes.ReliableOrdered, MaxUnits, MaxBudget, BlindMode);
		}

		public static bool Post(GlobalTargets targets, ReliabilityModes reliability, int MaxUnits, int MaxBudget, bool BlindMode)
		{
			return Post((byte)targets, null, reliability, MaxUnits, MaxBudget, BlindMode);
		}

		public static bool Post(BoltConnection connection, int MaxUnits, int MaxBudget, bool BlindMode)
		{
			return Post(10, connection, ReliabilityModes.ReliableOrdered, MaxUnits, MaxBudget, BlindMode);
		}

		public static bool Post(BoltConnection connection, ReliabilityModes reliability, int MaxUnits, int MaxBudget, bool BlindMode)
		{
			return Post(10, connection, reliability, MaxUnits, MaxBudget, BlindMode);
		}

		public static bool Post(int MaxUnits, int MaxBudget, bool BlindMode)
		{
			return Post(2, null, ReliabilityModes.ReliableOrdered, MaxUnits, MaxBudget, BlindMode);
		}

		public static bool Post(ReliabilityModes reliability, int MaxUnits, int MaxBudget, bool BlindMode)
		{
			return Post(2, null, reliability, MaxUnits, MaxBudget, BlindMode);
		}
	}
}
