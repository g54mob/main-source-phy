using System;
using Photon.Bolt.Internal;

namespace Photon.Bolt
{
	internal class UnitTurnOnConditionalEvent_Meta : Event_Meta, IEventFactory, IFactory
	{
		internal static UnitTurnOnConditionalEvent_Meta Instance;

		internal ObjectPool<UnitTurnOnConditionalEvent> _pool;

		TypeId IFactory.TypeId => TypeId;

		UniqueId IFactory.TypeKey => new UniqueId(13, 65, 137, 232, 28, 3, 102, 65, 191, 62, 148, 102, 10, 1, 92, 13);

		Type IFactory.TypeObject => typeof(UnitTurnOnConditionalEvent);

		static UnitTurnOnConditionalEvent_Meta()
		{
			Instance = new UnitTurnOnConditionalEvent_Meta();
			Instance.InitMeta();
		}

		internal override void InitObject(NetworkObj obj, Offsets offsets)
		{
		}

		internal override void InitMeta()
		{
			TypeId = new TypeId(28);
			CountStorage = 2;
			CountObjects = 1;
			CountProperties = 2;
			Properties = new NetworkPropertyInfo[2];
			NetworkProperty_Integer networkProperty_Integer = new NetworkProperty_Integer();
			networkProperty_Integer.PropertyMeta = this;
			networkProperty_Integer.Settings_Property("ConditionalEventId", 1, -1073741824);
			networkProperty_Integer.Settings_Offsets(0, 0);
			networkProperty_Integer.Settings_Integer(PropertyIntCompressionSettings.Create(3, 0));
			AddProperty(0, 0, networkProperty_Integer, -1);
			NetworkProperty_Integer networkProperty_Integer2 = new NetworkProperty_Integer();
			networkProperty_Integer2.PropertyMeta = this;
			networkProperty_Integer2.Settings_Property("InstanceEventId", 1, -1073741824);
			networkProperty_Integer2.Settings_Offsets(1, 1);
			networkProperty_Integer2.Settings_Integer(PropertyIntCompressionSettings.Create(3, 0));
			AddProperty(1, 0, networkProperty_Integer2, -1);
			base.InitMeta();
			_pool = new ObjectPool<UnitTurnOnConditionalEvent>();
		}

		object IFactory.Create()
		{
			return _pool.Get();
		}

		void IFactory.Return(object objToReturn)
		{
			_pool.Return(objToReturn as UnitTurnOnConditionalEvent);
		}

		void IEventFactory.Dispatch(Event ev, object target)
		{
			if (target is IUnitTurnOnConditionalEventListener unitTurnOnConditionalEventListener)
			{
				unitTurnOnConditionalEventListener.OnEvent((UnitTurnOnConditionalEvent)ev);
			}
		}
	}
}
