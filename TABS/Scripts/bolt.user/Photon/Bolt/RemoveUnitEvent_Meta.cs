using System;
using Photon.Bolt.Internal;

namespace Photon.Bolt
{
	internal class RemoveUnitEvent_Meta : Event_Meta, IEventFactory, IFactory
	{
		internal static RemoveUnitEvent_Meta Instance;

		internal ObjectPool<RemoveUnitEvent> _pool;

		TypeId IFactory.TypeId => TypeId;

		UniqueId IFactory.TypeKey => new UniqueId(238, 253, 213, 76, 242, 207, 62, 64, 150, 201, 101, 165, 234, 49, 34, 162);

		Type IFactory.TypeObject => typeof(RemoveUnitEvent);

		static RemoveUnitEvent_Meta()
		{
			Instance = new RemoveUnitEvent_Meta();
			Instance.InitMeta();
		}

		internal override void InitObject(NetworkObj obj, Offsets offsets)
		{
		}

		internal override void InitMeta()
		{
			TypeId = new TypeId(4);
			CountStorage = 2;
			CountObjects = 1;
			CountProperties = 2;
			Properties = new NetworkPropertyInfo[2];
			NetworkProperty_Integer networkProperty_Integer = new NetworkProperty_Integer();
			networkProperty_Integer.PropertyMeta = this;
			networkProperty_Integer.Settings_Property("UnitSmallNetworkId", 1, -1073741824);
			networkProperty_Integer.Settings_Offsets(0, 0);
			networkProperty_Integer.Settings_Integer(PropertyIntCompressionSettings.Create(16, 0));
			AddProperty(0, 0, networkProperty_Integer, -1);
			NetworkProperty_Integer networkProperty_Integer2 = new NetworkProperty_Integer();
			networkProperty_Integer2.PropertyMeta = this;
			networkProperty_Integer2.Settings_Property("UnitInstanceId", 1, -1073741824);
			networkProperty_Integer2.Settings_Offsets(1, 1);
			networkProperty_Integer2.Settings_Integer(PropertyIntCompressionSettings.Create(16, 32768));
			AddProperty(1, 0, networkProperty_Integer2, -1);
			base.InitMeta();
			_pool = new ObjectPool<RemoveUnitEvent>();
		}

		object IFactory.Create()
		{
			return _pool.Get();
		}

		void IFactory.Return(object objToReturn)
		{
			_pool.Return(objToReturn as RemoveUnitEvent);
		}

		void IEventFactory.Dispatch(Event ev, object target)
		{
			if (target is IRemoveUnitEventListener removeUnitEventListener)
			{
				removeUnitEventListener.OnEvent((RemoveUnitEvent)ev);
			}
		}
	}
}
