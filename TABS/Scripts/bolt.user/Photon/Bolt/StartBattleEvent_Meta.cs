using System;
using Photon.Bolt.Internal;

namespace Photon.Bolt
{
	internal class StartBattleEvent_Meta : Event_Meta, IEventFactory, IFactory
	{
		internal static StartBattleEvent_Meta Instance;

		internal ObjectPool<StartBattleEvent> _pool;

		TypeId IFactory.TypeId => TypeId;

		UniqueId IFactory.TypeKey => new UniqueId(byte.MaxValue, 250, 175, 155, 63, 183, 151, 69, 163, 47, 157, 205, 245, 69, 250, 98);

		Type IFactory.TypeObject => typeof(StartBattleEvent);

		static StartBattleEvent_Meta()
		{
			Instance = new StartBattleEvent_Meta();
			Instance.InitMeta();
		}

		internal override void InitObject(NetworkObj obj, Offsets offsets)
		{
		}

		internal override void InitMeta()
		{
			TypeId = new TypeId(2);
			CountStorage = 0;
			CountObjects = 1;
			CountProperties = 0;
			Properties = new NetworkPropertyInfo[0];
			base.InitMeta();
			_pool = new ObjectPool<StartBattleEvent>();
		}

		object IFactory.Create()
		{
			return _pool.Get();
		}

		void IFactory.Return(object objToReturn)
		{
			_pool.Return(objToReturn as StartBattleEvent);
		}

		void IEventFactory.Dispatch(Event ev, object target)
		{
			if (target is IStartBattleEventListener startBattleEventListener)
			{
				startBattleEventListener.OnEvent((StartBattleEvent)ev);
			}
		}
	}
}
