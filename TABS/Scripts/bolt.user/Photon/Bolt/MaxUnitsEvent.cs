using Photon.Bolt.Internal;
using UnityEngine;

namespace Photon.Bolt
{
	public class MaxUnitsEvent : Event
	{
		public int MaxUnits
		{
			get
			{
				return Storage.Values[OffsetStorage].Int0;
			}
			set
			{
				value = Mathf.Clamp(value, 0, 127);
				int @int = Storage.Values[OffsetStorage].Int0;
				Storage.Values[OffsetStorage].Int0 = value;
				if (!NetworkValue.Diff(@int, value))
				{
				}
			}
		}

		public bool HasMaxUnits
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

		public MaxUnitsEvent()
			: base(MaxUnitsEvent_Meta.Instance)
		{
		}

		public override string ToString()
		{
			return $"[MaxUnitsEvent MaxUnits={MaxUnits} HasMaxUnits={HasMaxUnits}]";
		}

		private static MaxUnitsEvent Create(byte targets, BoltConnection connection, ReliabilityModes reliability)
		{
			if (!(Factory.NewEvent(((IFactory)MaxUnitsEvent_Meta.Instance).TypeKey) is MaxUnitsEvent maxUnitsEvent))
			{
				return null;
			}
			maxUnitsEvent.Targets = targets;
			maxUnitsEvent.TargetConnection = connection;
			maxUnitsEvent.Reliability = reliability;
			return maxUnitsEvent;
		}

		public static MaxUnitsEvent Create(GlobalTargets targets)
		{
			return Create((byte)targets, null, ReliabilityModes.ReliableOrdered);
		}

		public static MaxUnitsEvent Create(GlobalTargets targets, ReliabilityModes reliability)
		{
			return Create((byte)targets, null, reliability);
		}

		public static MaxUnitsEvent Create(BoltConnection connection)
		{
			return Create(10, connection, ReliabilityModes.ReliableOrdered);
		}

		public static MaxUnitsEvent Create(BoltConnection connection, ReliabilityModes reliability)
		{
			return Create(10, connection, reliability);
		}

		public static MaxUnitsEvent Create()
		{
			return Create(2, null, ReliabilityModes.ReliableOrdered);
		}

		public static MaxUnitsEvent Create(ReliabilityModes reliability)
		{
			return Create(2, null, reliability);
		}

		private static bool Post(byte targets, BoltConnection connection, ReliabilityModes reliability, int MaxUnits, bool HasMaxUnits)
		{
			MaxUnitsEvent maxUnitsEvent = Create(targets, connection, reliability);
			if (maxUnitsEvent == null)
			{
				return false;
			}
			maxUnitsEvent.MaxUnits = MaxUnits;
			maxUnitsEvent.HasMaxUnits = HasMaxUnits;
			maxUnitsEvent.Send();
			return true;
		}

		public static bool Post(GlobalTargets targets, int MaxUnits, bool HasMaxUnits)
		{
			return Post((byte)targets, null, ReliabilityModes.ReliableOrdered, MaxUnits, HasMaxUnits);
		}

		public static bool Post(GlobalTargets targets, ReliabilityModes reliability, int MaxUnits, bool HasMaxUnits)
		{
			return Post((byte)targets, null, reliability, MaxUnits, HasMaxUnits);
		}

		public static bool Post(BoltConnection connection, int MaxUnits, bool HasMaxUnits)
		{
			return Post(10, connection, ReliabilityModes.ReliableOrdered, MaxUnits, HasMaxUnits);
		}

		public static bool Post(BoltConnection connection, ReliabilityModes reliability, int MaxUnits, bool HasMaxUnits)
		{
			return Post(10, connection, reliability, MaxUnits, HasMaxUnits);
		}

		public static bool Post(int MaxUnits, bool HasMaxUnits)
		{
			return Post(2, null, ReliabilityModes.ReliableOrdered, MaxUnits, HasMaxUnits);
		}

		public static bool Post(ReliabilityModes reliability, int MaxUnits, bool HasMaxUnits)
		{
			return Post(2, null, reliability, MaxUnits, HasMaxUnits);
		}
	}
}
