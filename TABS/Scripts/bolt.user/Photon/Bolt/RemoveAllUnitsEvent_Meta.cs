using System;
using Photon.Bolt.Internal;

namespace Photon.Bolt
{
	internal class RemoveAllUnitsEvent_Meta : Event_Meta, IEventFactory, IFactory
	{
		internal static RemoveAllUnitsEvent_Meta Instance;

		internal ObjectPool<RemoveAllUnitsEvent> _pool;

		TypeId IFactory.TypeId => TypeId;

		UniqueId IFactory.TypeKey => new UniqueId(92, 248, 188, 48, 83, 175, 192, 72, 171, 73, 186, 212, 243, 190, 184, 8);

		Type IFactory.TypeObject => typeof(RemoveAllUnitsEvent);

		static RemoveAllUnitsEvent_Meta()
		{
			Instance = new RemoveAllUnitsEvent_Meta();
			Instance.InitMeta();
		}

		internal override void InitObject(NetworkObj obj, Offsets offsets)
		{
		}

		internal override void InitMeta()
		{
			TypeId = new TypeId(9);
			CountStorage = 1;
			CountObjects = 1;
			CountProperties = 1;
			Properties = new NetworkPropertyInfo[1];
			NetworkProperty_Integer networkProperty_Integer = new NetworkProperty_Integer();
			networkProperty_Integer.PropertyMeta = this;
			networkProperty_Integer.Settings_Property("Team", 1, -1073741824);
			networkProperty_Integer.Settings_Offsets(0, 0);
			networkProperty_Integer.Settings_Integer(PropertyIntCompressionSettings.Create());
			AddProperty(0, 0, networkProperty_Integer, -1);
			base.InitMeta();
			_pool = new ObjectPool<RemoveAllUnitsEvent>();
		}

		object IFactory.Create()
		{
			return _pool.Get();
		}

		void IFactory.Return(object objToReturn)
		{
			_pool.Return(objToReturn as RemoveAllUnitsEvent);
		}

		void IEventFactory.Dispatch(Event ev, object target)
		{
			if (target is IRemoveAllUnitsEventListener removeAllUnitsEventListener)
			{
				removeAllUnitsEventListener.OnEvent((RemoveAllUnitsEvent)ev);
			}
		}
	}
}
