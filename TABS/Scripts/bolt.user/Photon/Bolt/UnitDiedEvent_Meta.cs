using System;
using Photon.Bolt.Internal;

namespace Photon.Bolt
{
	internal class UnitDiedEvent_Meta : Event_Meta, IEventFactory, IFactory
	{
		internal static UnitDiedEvent_Meta Instance;

		internal ObjectPool<UnitDiedEvent> _pool;

		TypeId IFactory.TypeId => TypeId;

		UniqueId IFactory.TypeKey => new UniqueId(229, 104, 169, 195, 134, 20, 158, 76, 144, 138, 81, 46, 196, 160, 57, 122);

		Type IFactory.TypeObject => typeof(UnitDiedEvent);

		static UnitDiedEvent_Meta()
		{
			Instance = new UnitDiedEvent_Meta();
			Instance.InitMeta();
		}

		internal override void InitObject(NetworkObj obj, Offsets offsets)
		{
		}

		internal override void InitMeta()
		{
			TypeId = new TypeId(5);
			CountStorage = 1;
			CountObjects = 1;
			CountProperties = 1;
			Properties = new NetworkPropertyInfo[1];
			NetworkProperty_Integer networkProperty_Integer = new NetworkProperty_Integer();
			networkProperty_Integer.PropertyMeta = this;
			networkProperty_Integer.Settings_Property("UnitSmallNetworkId", 1, -1073741824);
			networkProperty_Integer.Settings_Offsets(0, 0);
			networkProperty_Integer.Settings_Integer(PropertyIntCompressionSettings.Create(16, 0));
			AddProperty(0, 0, networkProperty_Integer, -1);
			base.InitMeta();
			_pool = new ObjectPool<UnitDiedEvent>();
		}

		object IFactory.Create()
		{
			return _pool.Get();
		}

		void IFactory.Return(object objToReturn)
		{
			_pool.Return(objToReturn as UnitDiedEvent);
		}

		void IEventFactory.Dispatch(Event ev, object target)
		{
			if (target is IUnitDiedEventListener unitDiedEventListener)
			{
				unitDiedEventListener.OnEvent((UnitDiedEvent)ev);
			}
		}
	}
}
