using System;
using Photon.Bolt.Exceptions;
using Photon.Bolt.Internal;
using UnityEngine;

namespace Photon.Bolt
{
	public class UnitSpecialAttackEvent : Event
	{
		public int AttackType
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

		public IProtocolToken AttackToken
		{
			get
			{
				return Storage.Values[OffsetStorage + 1].ProtocolToken;
			}
			set
			{
				IProtocolToken protocolToken = Storage.Values[OffsetStorage + 1].ProtocolToken;
				protocolToken.Release();
				Storage.Values[OffsetStorage + 1].ProtocolToken = value;
				if (!NetworkValue.Diff(protocolToken, value))
				{
				}
			}
		}

		public UnitSpecialAttackEvent()
			: base(UnitSpecialAttackEvent_Meta.Instance)
		{
		}

		public override string ToString()
		{
			return $"[UnitSpecialAttackEvent AttackType={AttackType} AttackToken={AttackToken}]";
		}

		protected override void PrepareRelease()
		{
			Storage.Values[OffsetStorage + 1].ProtocolToken.Release();
		}

		public static UnitSpecialAttackEvent Create(BoltEntity entity, EntityTargets targets)
		{
			if (!entity)
			{
				throw new ArgumentNullException("entity");
			}
			if (!entity.IsAttached)
			{
				throw new BoltException("You can not raise events on entities which are not attached");
			}
			if (!(Factory.NewEvent(((IFactory)UnitSpecialAttackEvent_Meta.Instance).TypeKey) is UnitSpecialAttackEvent unitSpecialAttackEvent))
			{
				return null;
			}
			unitSpecialAttackEvent.Targets = (int)targets;
			unitSpecialAttackEvent.TargetEntity = entity.Entity;
			unitSpecialAttackEvent.Reliability = ReliabilityModes.Unreliable;
			return unitSpecialAttackEvent;
		}

		public static UnitSpecialAttackEvent Create(BoltEntity entity)
		{
			return Create(entity, EntityTargets.Everyone);
		}

		public static bool Post(BoltEntity entity, EntityTargets targets, int AttackType, IProtocolToken AttackToken)
		{
			UnitSpecialAttackEvent unitSpecialAttackEvent = Create(entity, targets);
			if (unitSpecialAttackEvent == null)
			{
				return false;
			}
			unitSpecialAttackEvent.AttackType = AttackType;
			unitSpecialAttackEvent.AttackToken = AttackToken;
			unitSpecialAttackEvent.Send();
			return true;
		}

		public static bool Post(BoltEntity entity, int AttackType, IProtocolToken AttackToken)
		{
			return Post(entity, EntityTargets.Everyone, AttackType, AttackToken);
		}
	}
}
