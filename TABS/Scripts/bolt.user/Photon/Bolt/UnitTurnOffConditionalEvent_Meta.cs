using System;
using Photon.Bolt.Internal;

namespace Photon.Bolt
{
	internal class UnitTurnOffConditionalEvent_Meta : Event_Meta, IEventFactory, IFactory
	{
		internal static UnitTurnOffConditionalEvent_Meta Instance;

		internal ObjectPool<UnitTurnOffConditionalEvent> _pool;

		TypeId IFactory.TypeId => TypeId;

		UniqueId IFactory.TypeKey => new UniqueId(214, 12, 157, 77, 215, 28, 235, 78, 161, 174, 155, 180, 64, 182, 105, 0);

		Type IFactory.TypeObject => typeof(UnitTurnOffConditionalEvent);

		static UnitTurnOffConditionalEvent_Meta()
		{
			Instance = new UnitTurnOffConditionalEvent_Meta();
			Instance.InitMeta();
		}

		internal override void InitObject(NetworkObj obj, Offsets offsets)
		{
		}

		internal override void InitMeta()
		{
			TypeId = new TypeId(29);
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
			_pool = new ObjectPool<UnitTurnOffConditionalEvent>();
		}

		object IFactory.Create()
		{
			return _pool.Get();
		}

		void IFactory.Return(object objToReturn)
		{
			_pool.Return(objToReturn as UnitTurnOffConditionalEvent);
		}

		void IEventFactory.Dispatch(Event ev, object target)
		{
			if (target is IUnitTurnOffConditionalEventListener unitTurnOffConditionalEventListener)
			{
				unitTurnOffConditionalEventListener.OnEvent((UnitTurnOffConditionalEvent)ev);
			}
		}
	}
}
