using System;
using Photon.Bolt.Internal;

namespace Photon.Bolt
{
	internal class StartPlacementEvent_Meta : Event_Meta, IEventFactory, IFactory
	{
		internal static StartPlacementEvent_Meta Instance;

		internal ObjectPool<StartPlacementEvent> _pool;

		TypeId IFactory.TypeId => TypeId;

		UniqueId IFactory.TypeKey => new UniqueId(132, 76, 51, 54, 30, 163, 230, 70, 166, 180, 185, 39, 18, 123, 136, 151);

		Type IFactory.TypeObject => typeof(StartPlacementEvent);

		static StartPlacementEvent_Meta()
		{
			Instance = new StartPlacementEvent_Meta();
			Instance.InitMeta();
		}

		internal override void InitObject(NetworkObj obj, Offsets offsets)
		{
		}

		internal override void InitMeta()
		{
			TypeId = new TypeId(13);
			CountStorage = 0;
			CountObjects = 1;
			CountProperties = 0;
			Properties = new NetworkPropertyInfo[0];
			base.InitMeta();
			_pool = new ObjectPool<StartPlacementEvent>();
		}

		object IFactory.Create()
		{
			return _pool.Get();
		}

		void IFactory.Return(object objToReturn)
		{
			_pool.Return(objToReturn as StartPlacementEvent);
		}

		void IEventFactory.Dispatch(Event ev, object target)
		{
			if (target is IStartPlacementEventListener startPlacementEventListener)
			{
				startPlacementEventListener.OnEvent((StartPlacementEvent)ev);
			}
		}
	}
}
