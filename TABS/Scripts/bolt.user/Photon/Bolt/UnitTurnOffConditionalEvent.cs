using System;
using Photon.Bolt.Exceptions;
using Photon.Bolt.Internal;
using UnityEngine;

namespace Photon.Bolt
{
	public class UnitTurnOffConditionalEvent : Event
	{
		public int ConditionalEventId
		{
			get
			{
				return Storage.Values[OffsetStorage].Int0;
			}
			set
			{
				value = Mathf.Clamp(value, 0, 7);
				int @int = Storage.Values[OffsetStorage].Int0;
				Storage.Values[OffsetStorage].Int0 = value;
				if (!NetworkValue.Diff(@int, value))
				{
				}
			}
		}

		public int InstanceEventId
		{
			get
			{
				return Storage.Values[OffsetStorage + 1].Int0;
			}
			set
			{
				value = Mathf.Clamp(value, 0, 7);
				int @int = Storage.Values[OffsetStorage + 1].Int0;
				Storage.Values[OffsetStorage + 1].Int0 = value;
				if (!NetworkValue.Diff(@int, value))
				{
				}
			}
		}

		public UnitTurnOffConditionalEvent()
			: base(UnitTurnOffConditionalEvent_Meta.Instance)
		{
		}

		public override string ToString()
		{
			return $"[UnitTurnOffConditionalEvent ConditionalEventId={ConditionalEventId} InstanceEventId={InstanceEventId}]";
		}

		public static UnitTurnOffConditionalEvent Create(BoltEntity entity, EntityTargets targets)
		{
			if (!entity)
			{
				throw new ArgumentNullException("entity");
			}
			if (!entity.IsAttached)
			{
				throw new BoltException("You can not raise events on entities which are not attached");
			}
			if (!(Factory.NewEvent(((IFactory)UnitTurnOffConditionalEvent_Meta.Instance).TypeKey) is UnitTurnOffConditionalEvent unitTurnOffConditionalEvent))
			{
				return null;
			}
			unitTurnOffConditionalEvent.Targets = (int)targets;
			unitTurnOffConditionalEvent.TargetEntity = entity.Entity;
			unitTurnOffConditionalEvent.Reliability = ReliabilityModes.Unreliable;
			return unitTurnOffConditionalEvent;
		}

		public static UnitTurnOffConditionalEvent Create(BoltEntity entity)
		{
			return Create(entity, EntityTargets.Everyone);
		}

		public static bool Post(BoltEntity entity, EntityTargets targets, int ConditionalEventId, int InstanceEventId)
		{
			UnitTurnOffConditionalEvent unitTurnOffConditionalEvent = Create(entity, targets);
			if (unitTurnOffConditionalEvent == null)
			{
				return false;
			}
			unitTurnOffConditionalEvent.ConditionalEventId = ConditionalEventId;
			unitTurnOffConditionalEvent.InstanceEventId = InstanceEventId;
			unitTurnOffConditionalEvent.Send();
			return true;
		}

		public static bool Post(BoltEntity entity, int ConditionalEventId, int InstanceEventId)
		{
			return Post(entity, EntityTargets.Everyone, ConditionalEventId, InstanceEventId);
		}
	}
}
