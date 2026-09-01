using System;
using Photon.Bolt.Internal;

namespace Photon.Bolt
{
	internal class MaxUnitsEvent_Meta : Event_Meta, IEventFactory, IFactory
	{
		internal static MaxUnitsEvent_Meta Instance;

		internal ObjectPool<MaxUnitsEvent> _pool;

		TypeId IFactory.TypeId => TypeId;

		UniqueId IFactory.TypeKey => new UniqueId(59, 169, 160, 4, 250, 101, 34, 78, 190, 214, 53, 64, 205, 127, 78, 97);

		Type IFactory.TypeObject => typeof(MaxUnitsEvent);

		static MaxUnitsEvent_Meta()
		{
			Instance = new MaxUnitsEvent_Meta();
			Instance.InitMeta();
		}

		internal override void InitObject(NetworkObj obj, Offsets offsets)
		{
		}

		internal override void InitMeta()
		{
			TypeId = new TypeId(23);
			CountStorage = 2;
			CountObjects = 1;
			CountProperties = 2;
			Properties = new NetworkPropertyInfo[2];
			NetworkProperty_Integer networkProperty_Integer = new NetworkProperty_Integer();
			networkProperty_Integer.PropertyMeta = this;
			networkProperty_Integer.Settings_Property("MaxUnits", 1, -1073741824);
			networkProperty_Integer.Settings_Offsets(0, 0);
			networkProperty_Integer.Settings_Integer(PropertyIntCompressionSettings.Create(7, 0));
			AddProperty(0, 0, networkProperty_Integer, -1);
			NetworkProperty_Bool networkProperty_Bool = new NetworkProperty_Bool();
			networkProperty_Bool.PropertyMeta = this;
			networkProperty_Bool.Settings_Property("HasMaxUnits", 1, -1073741824);
			networkProperty_Bool.Settings_Offsets(1, 1);
			AddProperty(1, 0, networkProperty_Bool, -1);
			base.InitMeta();
			_pool = new ObjectPool<MaxUnitsEvent>();
		}

		object IFactory.Create()
		{
			return _pool.Get();
		}

		void IFactory.Return(object objToReturn)
		{
			_pool.Return(objToReturn as MaxUnitsEvent);
		}

		void IEventFactory.Dispatch(Event ev, object target)
		{
			if (target is IMaxUnitsEventListener maxUnitsEventListener)
			{
				maxUnitsEventListener.OnEvent((MaxUnitsEvent)ev);
			}
		}
	}
}
