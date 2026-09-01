using System;
using Photon.Bolt.Exceptions;
using Photon.Bolt.Internal;
using UnityEngine;

namespace Photon.Bolt
{
	public class FailedToLinkPooledUnitEvent : Event
	{
		public int UnitInstanceId
		{
			get
			{
				return Storage.Values[OffsetStorage].Int0;
			}
			set
			{
				value = Mathf.Clamp(value, -32768, 32767);
				int @int = Storage.Values[OffsetStorage].Int0;
				Storage.Values[OffsetStorage].Int0 = value;
				if (!NetworkValue.Diff(@int, value))
				{
				}
			}
		}

		public int PoolIndex
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

		public int PoolId
		{
			get
			{
				return Storage.Values[OffsetStorage + 2].Int0;
			}
			set
			{
				value = Mathf.Clamp(value, -32768, 32767);
				int @int = Storage.Values[OffsetStorage + 2].Int0;
				Storage.Values[OffsetStorage + 2].Int0 = value;
				if (!NetworkValue.Diff(@int, value))
				{
				}
			}
		}

		public FailedToLinkPooledUnitEvent()
			: base(FailedToLinkPooledUnitEvent_Meta.Instance)
		{
		}

		public override string ToString()
		{
			return $"[FailedToLinkPooledUnitEvent UnitInstanceId={UnitInstanceId} PoolIndex={PoolIndex} PoolId={PoolId}]";
		}

		public static FailedToLinkPooledUnitEvent Create(BoltEntity entity, EntityTargets targets)
		{
			if (!entity)
			{
				throw new ArgumentNullException("entity");
			}
			if (!entity.IsAttached)
			{
				throw new BoltException("You can not raise events on entities which are not attached");
			}
			if (!(Factory.NewEvent(((IFactory)FailedToLinkPooledUnitEvent_Meta.Instance).TypeKey) is FailedToLinkPooledUnitEvent failedToLinkPooledUnitEvent))
			{
				return null;
			}
			failedToLinkPooledUnitEvent.Targets = (int)targets;
			failedToLinkPooledUnitEvent.TargetEntity = entity.Entity;
			failedToLinkPooledUnitEvent.Reliability = ReliabilityModes.Unreliable;
			return failedToLinkPooledUnitEvent;
		}

		public static FailedToLinkPooledUnitEvent Create(BoltEntity entity)
		{
			return Create(entity, EntityTargets.Everyone);
		}

		private static FailedToLinkPooledUnitEvent Create(byte targets, BoltConnection connection, ReliabilityModes reliability)
		{
			if (!(Factory.NewEvent(((IFactory)FailedToLinkPooledUnitEvent_Meta.Instance).TypeKey) is FailedToLinkPooledUnitEvent failedToLinkPooledUnitEvent))
			{
				return null;
			}
			failedToLinkPooledUnitEvent.Targets = targets;
			failedToLinkPooledUnitEvent.TargetConnection = connection;
			failedToLinkPooledUnitEvent.Reliability = reliability;
			return failedToLinkPooledUnitEvent;
		}

		public static FailedToLinkPooledUnitEvent Create(GlobalTargets targets)
		{
			return Create((byte)targets, null, ReliabilityModes.ReliableOrdered);
		}

		public static FailedToLinkPooledUnitEvent Create(GlobalTargets targets, ReliabilityModes reliability)
		{
			return Create((byte)targets, null, reliability);
		}

		public static FailedToLinkPooledUnitEvent Create(BoltConnection connection)
		{
			return Create(10, connection, ReliabilityModes.ReliableOrdered);
		}

		public static FailedToLinkPooledUnitEvent Create(BoltConnection connection, ReliabilityModes reliability)
		{
			return Create(10, connection, reliability);
		}

		public static FailedToLinkPooledUnitEvent Create()
		{
			return Create(2, null, ReliabilityModes.ReliableOrdered);
		}

		public static FailedToLinkPooledUnitEvent Create(ReliabilityModes reliability)
		{
			return Create(2, null, reliability);
		}

		public static bool Post(BoltEntity entity, EntityTargets targets, int UnitInstanceId, int PoolIndex, int PoolId)
		{
			FailedToLinkPooledUnitEvent failedToLinkPooledUnitEvent = Create(entity, targets);
			if (failedToLinkPooledUnitEvent == null)
			{
				return false;
			}
			failedToLinkPooledUnitEvent.UnitInstanceId = UnitInstanceId;
			failedToLinkPooledUnitEvent.PoolIndex = PoolIndex;
			failedToLinkPooledUnitEvent.PoolId = PoolId;
			failedToLinkPooledUnitEvent.Send();
			return true;
		}

		public static bool Post(BoltEntity entity, int UnitInstanceId, int PoolIndex, int PoolId)
		{
			return Post(entity, EntityTargets.Everyone, UnitInstanceId, PoolIndex, PoolId);
		}

		private static bool Post(byte targets, BoltConnection connection, ReliabilityModes reliability, int UnitInstanceId, int PoolIndex, int PoolId)
		{
			FailedToLinkPooledUnitEvent failedToLinkPooledUnitEvent = Create(targets, connection, reliability);
			if (failedToLinkPooledUnitEvent == null)
			{
				return false;
			}
			failedToLinkPooledUnitEvent.UnitInstanceId = UnitInstanceId;
			failedToLinkPooledUnitEvent.PoolIndex = PoolIndex;
			failedToLinkPooledUnitEvent.PoolId = PoolId;
			failedToLinkPooledUnitEvent.Send();
			return true;
		}

		public static bool Post(GlobalTargets targets, int UnitInstanceId, int PoolIndex, int PoolId)
		{
			return Post((byte)targets, null, ReliabilityModes.ReliableOrdered, UnitInstanceId, PoolIndex, PoolId);
		}

		public static bool Post(GlobalTargets targets, ReliabilityModes reliability, int UnitInstanceId, int PoolIndex, int PoolId)
		{
			return Post((byte)targets, null, reliability, UnitInstanceId, PoolIndex, PoolId);
		}

		public static bool Post(BoltConnection connection, int UnitInstanceId, int PoolIndex, int PoolId)
		{
			return Post(10, connection, ReliabilityModes.ReliableOrdered, UnitInstanceId, PoolIndex, PoolId);
		}

		public static bool Post(BoltConnection connection, ReliabilityModes reliability, int UnitInstanceId, int PoolIndex, int PoolId)
		{
			return Post(10, connection, reliability, UnitInstanceId, PoolIndex, PoolId);
		}

		public static bool Post(int UnitInstanceId, int PoolIndex, int PoolId)
		{
			return Post(2, null, ReliabilityModes.ReliableOrdered, UnitInstanceId, PoolIndex, PoolId);
		}

		public static bool Post(ReliabilityModes reliability, int UnitInstanceId, int PoolIndex, int PoolId)
		{
			return Post(2, null, reliability, UnitInstanceId, PoolIndex, PoolId);
		}
	}
}
