using System;
using Photon.Bolt.Internal;

namespace Photon.Bolt
{
	internal class InitiatorCancelledMapChangeEvent_Meta : Event_Meta, IEventFactory, IFactory
	{
		internal static InitiatorCancelledMapChangeEvent_Meta Instance;

		internal ObjectPool<InitiatorCancelledMapChangeEvent> _pool;

		TypeId IFactory.TypeId => TypeId;

		UniqueId IFactory.TypeKey => new UniqueId(79, 150, 60, 226, 103, 118, 28, 64, 130, 220, 73, 45, 162, 45, 212, 219);

		Type IFactory.TypeObject => typeof(InitiatorCancelledMapChangeEvent);

		static InitiatorCancelledMapChangeEvent_Meta()
		{
			Instance = new InitiatorCancelledMapChangeEvent_Meta();
			Instance.InitMeta();
		}

		internal override void InitObject(NetworkObj obj, Offsets offsets)
		{
		}

		internal override void InitMeta()
		{
			TypeId = new TypeId(21);
			CountStorage = 0;
			CountObjects = 1;
			CountProperties = 0;
			Properties = new NetworkPropertyInfo[0];
			base.InitMeta();
			_pool = new ObjectPool<InitiatorCancelledMapChangeEvent>();
		}

		object IFactory.Create()
		{
			return _pool.Get();
		}

		void IFactory.Return(object objToReturn)
		{
			_pool.Return(objToReturn as InitiatorCancelledMapChangeEvent);
		}

		void IEventFactory.Dispatch(Event ev, object target)
		{
			if (target is IInitiatorCancelledMapChangeEventListener initiatorCancelledMapChangeEventListener)
			{
				initiatorCancelledMapChangeEventListener.OnEvent((InitiatorCancelledMapChangeEvent)ev);
			}
		}
	}
}
