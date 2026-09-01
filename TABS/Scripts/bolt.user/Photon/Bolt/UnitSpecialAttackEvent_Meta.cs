using System;
using Photon.Bolt.Internal;

namespace Photon.Bolt
{
	internal class UnitSpecialAttackEvent_Meta : Event_Meta, IEventFactory, IFactory
	{
		internal static UnitSpecialAttackEvent_Meta Instance;

		internal ObjectPool<UnitSpecialAttackEvent> _pool;

		TypeId IFactory.TypeId => TypeId;

		UniqueId IFactory.TypeKey => new UniqueId(176, 104, 204, 152, 78, 225, 181, 67, 134, 213, 184, 133, 41, 231, 220, 175);

		Type IFactory.TypeObject => typeof(UnitSpecialAttackEvent);

		static UnitSpecialAttackEvent_Meta()
		{
			Instance = new UnitSpecialAttackEvent_Meta();
			Instance.InitMeta();
		}

		internal override void InitObject(NetworkObj obj, Offsets offsets)
		{
		}

		internal override void InitMeta()
		{
			TypeId = new TypeId(26);
			CountStorage = 2;
			CountObjects = 1;
			CountProperties = 2;
			Properties = new NetworkPropertyInfo[2];
			NetworkProperty_Integer networkProperty_Integer = new NetworkProperty_Integer();
			networkProperty_Integer.PropertyMeta = this;
			networkProperty_Integer.Settings_Property("AttackType", 1, -1073741824);
			networkProperty_Integer.Settings_Offsets(0, 0);
			networkProperty_Integer.Settings_Integer(PropertyIntCompressionSettings.Create(3, 0));
			AddProperty(0, 0, networkProperty_Integer, -1);
			NetworkProperty_ProtocolToken networkProperty_ProtocolToken = new NetworkProperty_ProtocolToken();
			networkProperty_ProtocolToken.PropertyMeta = this;
			networkProperty_ProtocolToken.Settings_Property("AttackToken", 1, -1073741824);
			networkProperty_ProtocolToken.Settings_Offsets(1, 1);
			AddProperty(1, 0, networkProperty_ProtocolToken, -1);
			base.InitMeta();
			_pool = new ObjectPool<UnitSpecialAttackEvent>();
		}

		object IFactory.Create()
		{
			return _pool.Get();
		}

		void IFactory.Return(object objToReturn)
		{
			_pool.Return(objToReturn as UnitSpecialAttackEvent);
		}

		void IEventFactory.Dispatch(Event ev, object target)
		{
			if (target is IUnitSpecialAttackEventListener unitSpecialAttackEventListener)
			{
				unitSpecialAttackEventListener.OnEvent((UnitSpecialAttackEvent)ev);
			}
		}
	}
}
