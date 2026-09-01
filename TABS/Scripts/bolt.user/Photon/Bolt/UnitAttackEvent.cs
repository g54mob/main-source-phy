using System;
using Photon.Bolt.Exceptions;
using Photon.Bolt.Internal;
using UnityEngine;

namespace Photon.Bolt
{
	public class UnitAttackEvent : Event
	{
		public int TargetUnitSmallNetworkId
		{
			get
			{
				return Storage.Values[OffsetStorage].Int0;
			}
			set
			{
				value = Mathf.Clamp(value, 0, 65535);
				int @int = Storage.Values[OffsetStorage].Int0;
				Storage.Values[OffsetStorage].Int0 = value;
				if (!NetworkValue.Diff(@int, value))
				{
				}
			}
		}

		public Vector3 Position
		{
			get
			{
				return Storage.Values[OffsetStorage + 1].Vector3;
			}
			set
			{
				Vector3 vector = Storage.Values[OffsetStorage + 1].Vector3;
				Storage.Values[OffsetStorage + 1].Vector3 = value;
				if (!NetworkValue.Diff(vector, value))
				{
				}
			}
		}

		public Vector3 ForceDirection
		{
			get
			{
				return Storage.Values[OffsetStorage + 2].Vector3;
			}
			set
			{
				Vector3 vector = Storage.Values[OffsetStorage + 2].Vector3;
				Storage.Values[OffsetStorage + 2].Vector3 = value;
				if (!NetworkValue.Diff(vector, value))
				{
				}
			}
		}

		public int ForceWeapon
		{
			get
			{
				return Storage.Values[OffsetStorage + 3].Int0;
			}
			set
			{
				int @int = Storage.Values[OffsetStorage + 3].Int0;
				Storage.Values[OffsetStorage + 3].Int0 = value;
				if (!NetworkValue.Diff(@int, value))
				{
				}
			}
		}

		public UnitAttackEvent()
			: base(UnitAttackEvent_Meta.Instance)
		{
		}

		public override string ToString()
		{
			return $"[UnitAttackEvent TargetUnitSmallNetworkId={TargetUnitSmallNetworkId} Position={Position} ForceDirection={ForceDirection} ForceWeapon={ForceWeapon}]";
		}

		public static UnitAttackEvent Create(BoltEntity entity, EntityTargets targets)
		{
			if (!entity)
			{
				throw new ArgumentNullException("entity");
			}
			if (!entity.IsAttached)
			{
				throw new BoltException("You can not raise events on entities which are not attached");
			}
			if (!(Factory.NewEvent(((IFactory)UnitAttackEvent_Meta.Instance).TypeKey) is UnitAttackEvent unitAttackEvent))
			{
				return null;
			}
			unitAttackEvent.Targets = (int)targets;
			unitAttackEvent.TargetEntity = entity.Entity;
			unitAttackEvent.Reliability = ReliabilityModes.Unreliable;
			return unitAttackEvent;
		}

		public static UnitAttackEvent Create(BoltEntity entity)
		{
			return Create(entity, EntityTargets.Everyone);
		}

		public static bool Post(BoltEntity entity, EntityTargets targets, int TargetUnitSmallNetworkId, Vector3 Position, Vector3 ForceDirection, int ForceWeapon)
		{
			UnitAttackEvent unitAttackEvent = Create(entity, targets);
			if (unitAttackEvent == null)
			{
				return false;
			}
			unitAttackEvent.TargetUnitSmallNetworkId = TargetUnitSmallNetworkId;
			unitAttackEvent.Position = Position;
			unitAttackEvent.ForceDirection = ForceDirection;
			unitAttackEvent.ForceWeapon = ForceWeapon;
			unitAttackEvent.Send();
			return true;
		}

		public static bool Post(BoltEntity entity, int TargetUnitSmallNetworkId, Vector3 Position, Vector3 ForceDirection, int ForceWeapon)
		{
			return Post(entity, EntityTargets.Everyone, TargetUnitSmallNetworkId, Position, ForceDirection, ForceWeapon);
		}
	}
}
