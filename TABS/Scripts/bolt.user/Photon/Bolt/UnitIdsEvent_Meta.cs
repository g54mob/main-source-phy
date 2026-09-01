using System;
using Photon.Bolt.Internal;

namespace Photon.Bolt
{
	internal class UnitIdsEvent_Meta : Event_Meta, IEventFactory, IFactory
	{
		internal static UnitIdsEvent_Meta Instance;

		internal ObjectPool<UnitIdsEvent> _pool;

		TypeId IFactory.TypeId => TypeId;

		UniqueId IFactory.TypeKey => new UniqueId(183, 24, 167, 15, 19, 162, 99, 73, 174, 212, 199, 76, 233, 106, 42, 64);

		Type IFactory.TypeObject => typeof(UnitIdsEvent);

		static UnitIdsEvent_Meta()
		{
			Instance = new UnitIdsEvent_Meta();
			Instance.InitMeta();
		}

		internal override void InitObject(NetworkObj obj, Offsets offsets)
		{
		}

		internal override void InitMeta()
		{
			TypeId = new TypeId(19);
			CountStorage = 2;
			CountObjects = 1;
			CountProperties = 2;
			Properties = new NetworkPropertyInfo[2];
			NetworkProperty_Integer networkProperty_Integer = new NetworkProperty_Integer();
			networkProperty_Integer.PropertyMeta = this;
			networkProperty_Integer.Settings_Property("UnitInstanceId", 1, -1073741824);
			networkProperty_Integer.Settings_Offsets(0, 0);
			networkProperty_Integer.Settings_Integer(PropertyIntCompressionSettings.Create(16, 32768));
			AddProperty(0, 0, networkProperty_Integer, -1);
			NetworkProperty_Integer networkProperty_Integer2 = new NetworkProperty_Integer();
			networkProperty_Integer2.PropertyMeta = this;
			networkProperty_Integer2.Settings_Property("UnitRemoteInstanceId", 1, -1073741824);
			networkProperty_Integer2.Settings_Offsets(1, 1);
			networkProperty_Integer2.Settings_Integer(PropertyIntCompressionSettings.Create(16, 32768));
			AddProperty(1, 0, networkProperty_Integer2, -1);
			base.InitMeta();
			_pool = new ObjectPool<UnitIdsEvent>();
		}

		object IFactory.Create()
		{
			return _pool.Get();
		}

		void IFactory.Return(object objToReturn)
		{
			_pool.Return(objToReturn as UnitIdsEvent);
		}

		void IEventFactory.Dispatch(Event ev, object target)
		{
			if (target is IUnitIdsEventListener unitIdsEventListener)
			{
				unitIdsEventListener.OnEvent((UnitIdsEvent)ev);
			}
		}
	}
}
