using Photon.Bolt.Internal;

namespace Photon.Bolt
{
	public class RespondRuleChange : Event
	{
		public bool Status
		{
			get
			{
				return Storage.Values[OffsetStorage].Bool;
			}
			set
			{
				bool a = Storage.Values[OffsetStorage].Bool;
				Storage.Values[OffsetStorage].Bool = value;
				if (!NetworkValue.Diff(a, value))
				{
				}
			}
		}

		public int MaxUnits
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

		public int MaxBudget
		{
			get
			{
				return Storage.Values[OffsetStorage + 2].Int0;
			}
			set
			{
				int @int = Storage.Values[OffsetStorage + 2].Int0;
				Storage.Values[OffsetStorage + 2].Int0 = value;
				if (!NetworkValue.Diff(@int, value))
				{
				}
			}
		}

		public bool BlindMode
		{
			get
			{
				return Storage.Values[OffsetStorage + 3].Bool;
			}
			set
			{
				bool a = Storage.Values[OffsetStorage + 3].Bool;
				Storage.Values[OffsetStorage + 3].Bool = value;
				if (!NetworkValue.Diff(a, value))
				{
				}
			}
		}

		public RespondRuleChange()
			: base(RespondRuleChange_Meta.Instance)
		{
		}

		public override string ToString()
		{
			return $"[RespondRuleChange Status={Status} MaxUnits={MaxUnits} MaxBudget={MaxBudget} BlindMode={BlindMode}]";
		}

		private static RespondRuleChange Create(byte targets, BoltConnection connection, ReliabilityModes reliability)
		{
			if (!(Factory.NewEvent(((IFactory)RespondRuleChange_Meta.Instance).TypeKey) is RespondRuleChange respondRuleChange))
			{
				return null;
			}
			respondRuleChange.Targets = targets;
			respondRuleChange.TargetConnection = connection;
			respondRuleChange.Reliability = reliability;
			return respondRuleChange;
		}

		public static RespondRuleChange Create(GlobalTargets targets)
		{
			return Create((byte)targets, null, ReliabilityModes.ReliableOrdered);
		}

		public static RespondRuleChange Create(GlobalTargets targets, ReliabilityModes reliability)
		{
			return Create((byte)targets, null, reliability);
		}

		public static RespondRuleChange Create(BoltConnection connection)
		{
			return Create(10, connection, ReliabilityModes.ReliableOrdered);
		}

		public static RespondRuleChange Create(BoltConnection connection, ReliabilityModes reliability)
		{
			return Create(10, connection, reliability);
		}

		public static RespondRuleChange Create()
		{
			return Create(2, null, ReliabilityModes.ReliableOrdered);
		}

		public static RespondRuleChange Create(ReliabilityModes reliability)
		{
			return Create(2, null, reliability);
		}

		private static bool Post(byte targets, BoltConnection connection, ReliabilityModes reliability, bool Status, int MaxUnits, int MaxBudget, bool BlindMode)
		{
			RespondRuleChange respondRuleChange = Create(targets, connection, reliability);
			if (respondRuleChange == null)
			{
				return false;
			}
			respondRuleChange.Status = Status;
			respondRuleChange.MaxUnits = MaxUnits;
			respondRuleChange.MaxBudget = MaxBudget;
			respondRuleChange.BlindMode = BlindMode;
			respondRuleChange.Send();
			return true;
		}

		public static bool Post(GlobalTargets targets, bool Status, int MaxUnits, int MaxBudget, bool BlindMode)
		{
			return Post((byte)targets, null, ReliabilityModes.ReliableOrdered, Status, MaxUnits, MaxBudget, BlindMode);
		}

		public static bool Post(GlobalTargets targets, ReliabilityModes reliability, bool Status, int MaxUnits, int MaxBudget, bool BlindMode)
		{
			return Post((byte)targets, null, reliability, Status, MaxUnits, MaxBudget, BlindMode);
		}

		public static bool Post(BoltConnection connection, bool Status, int MaxUnits, int MaxBudget, bool BlindMode)
		{
			return Post(10, connection, ReliabilityModes.ReliableOrdered, Status, MaxUnits, MaxBudget, BlindMode);
		}

		public static bool Post(BoltConnection connection, ReliabilityModes reliability, bool Status, int MaxUnits, int MaxBudget, bool BlindMode)
		{
			return Post(10, connection, reliability, Status, MaxUnits, MaxBudget, BlindMode);
		}

		public static bool Post(bool Status, int MaxUnits, int MaxBudget, bool BlindMode)
		{
			return Post(2, null, ReliabilityModes.ReliableOrdered, Status, MaxUnits, MaxBudget, BlindMode);
		}

		public static bool Post(ReliabilityModes reliability, bool Status, int MaxUnits, int MaxBudget, bool BlindMode)
		{
			return Post(2, null, reliability, Status, MaxUnits, MaxBudget, BlindMode);
		}
	}
}
