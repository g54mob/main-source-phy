using System;
using Photon.Bolt.Internal;

namespace Photon.Bolt
{
	internal class UnitEnterPossessionEvent_Meta : Event_Meta, IEventFactory, IFactory
	{
		internal static UnitEnterPossessionEvent_Meta Instance;

		internal ObjectPool<UnitEnterPossessionEvent> _pool;

		TypeId IFactory.TypeId => TypeId;

		UniqueId IFactory.TypeKey => new UniqueId(25, 92, 16, 147, 33, 86, 189, 75, 140, 188, 222, 119, 102, 96, 66, 58);

		Type IFactory.TypeObject => typeof(UnitEnterPossessionEvent);

		static UnitEnterPossessionEvent_Meta()
		{
			Instance = new UnitEnterPossessionEvent_Meta();
			Instance.InitMeta();
		}

		internal override void InitObject(NetworkObj obj, Offsets offsets)
		{
		}

		internal override void InitMeta()
		{
			TypeId = new TypeId(17);
			CountStorage = 1;
			CountObjects = 1;
			CountProperties = 1;
			Properties = new NetworkPropertyInfo[1];
			NetworkProperty_Integer networkProperty_Integer = new NetworkProperty_Integer();
			networkProperty_Integer.PropertyMeta = this;
			networkProperty_Integer.Settings_Property("UnitInstanceId", 1, -1073741824);
			networkProperty_Integer.Settings_Offsets(0, 0);
			networkProperty_Integer.Settings_Integer(PropertyIntCompressionSettings.Create(16, 32768));
			AddProperty(0, 0, networkProperty_Integer, -1);
			base.InitMeta();
			_pool = new ObjectPool<UnitEnterPossessionEvent>();
		}

		object IFactory.Create()
		{
			return _pool.Get();
		}

		void IFactory.Return(object objToReturn)
		{
			_pool.Return(objToReturn as UnitEnterPossessionEvent);
		}

		void IEventFactory.Dispatch(Event ev, object target)
		{
			if (target is IUnitEnterPossessionEventListener unitEnterPossessionEventListener)
			{
				unitEnterPossessionEventListener.OnEvent((UnitEnterPossessionEvent)ev);
			}
		}
	}
}
