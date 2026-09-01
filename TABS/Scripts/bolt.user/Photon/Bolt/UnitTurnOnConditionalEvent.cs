using System;
using Photon.Bolt.Exceptions;
using Photon.Bolt.Internal;
using UnityEngine;

namespace Photon.Bolt
{
	public class UnitTurnOnConditionalEvent : Event
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

		public UnitTurnOnConditionalEvent()
			: base(UnitTurnOnConditionalEvent_Meta.Instance)
		{
		}

		public override string ToString()
		{
			return $"[UnitTurnOnConditionalEvent ConditionalEventId={ConditionalEventId} InstanceEventId={InstanceEventId}]";
		}

		public static UnitTurnOnConditionalEvent Create(BoltEntity entity, EntityTargets targets)
		{
			if (!entity)
			{
				throw new ArgumentNullException("entity");
			}
			if (!entity.IsAttached)
			{
				throw new BoltException("You can not raise events on entities which are not attached");
			}
			if (!(Factory.NewEvent(((IFactory)UnitTurnOnConditionalEvent_Meta.Instance).TypeKey) is UnitTurnOnConditionalEvent unitTurnOnConditionalEvent))
			{
				return null;
			}
			unitTurnOnConditionalEvent.Targets = (int)targets;
			unitTurnOnConditionalEvent.TargetEntity = entity.Entity;
			unitTurnOnConditionalEvent.Reliability = ReliabilityModes.Unreliable;
			return unitTurnOnConditionalEvent;
		}

		public static UnitTurnOnConditionalEvent Create(BoltEntity entity)
		{
			return Create(entity, EntityTargets.Everyone);
		}

		public static bool Post(BoltEntity entity, EntityTargets targets, int ConditionalEventId, int InstanceEventId)
		{
			UnitTurnOnConditionalEvent unitTurnOnConditionalEvent = Create(entity, targets);
			if (unitTurnOnConditionalEvent == null)
			{
				return false;
			}
			unitTurnOnConditionalEvent.ConditionalEventId = ConditionalEventId;
			unitTurnOnConditionalEvent.InstanceEventId = InstanceEventId;
			unitTurnOnConditionalEvent.Send();
			return true;
		}

		public static bool Post(BoltEntity entity, int ConditionalEventId, int InstanceEventId)
		{
			return Post(entity, EntityTargets.Everyone, ConditionalEventId, InstanceEventId);
		}
	}
}
