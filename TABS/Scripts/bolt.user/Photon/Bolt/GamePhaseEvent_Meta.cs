using System;
using Photon.Bolt.Internal;

namespace Photon.Bolt
{
	internal class GamePhaseEvent_Meta : Event_Meta, IEventFactory, IFactory
	{
		internal static GamePhaseEvent_Meta Instance;

		internal ObjectPool<GamePhaseEvent> _pool;

		TypeId IFactory.TypeId => TypeId;

		UniqueId IFactory.TypeKey => new UniqueId(241, 124, 140, 0, 109, 26, 60, 75, 135, 15, 101, 88, 183, 76, 123, 172);

		Type IFactory.TypeObject => typeof(GamePhaseEvent);

		static GamePhaseEvent_Meta()
		{
			Instance = new GamePhaseEvent_Meta();
			Instance.InitMeta();
		}

		internal override void InitObject(NetworkObj obj, Offsets offsets)
		{
		}

		internal override void InitMeta()
		{
			TypeId = new TypeId(16);
			CountStorage = 1;
			CountObjects = 1;
			CountProperties = 1;
			Properties = new NetworkPropertyInfo[1];
			NetworkProperty_Integer networkProperty_Integer = new NetworkProperty_Integer();
			networkProperty_Integer.PropertyMeta = this;
			networkProperty_Integer.Settings_Property("Phase", 1, -1073741824);
			networkProperty_Integer.Settings_Offsets(0, 0);
			networkProperty_Integer.Settings_Integer(PropertyIntCompressionSettings.Create(5, 0));
			AddProperty(0, 0, networkProperty_Integer, -1);
			base.InitMeta();
			_pool = new ObjectPool<GamePhaseEvent>();
		}

		object IFactory.Create()
		{
			return _pool.Get();
		}

		void IFactory.Return(object objToReturn)
		{
			_pool.Return(objToReturn as GamePhaseEvent);
		}

		void IEventFactory.Dispatch(Event ev, object target)
		{
			if (target is IGamePhaseEventListener gamePhaseEventListener)
			{
				gamePhaseEventListener.OnEvent((GamePhaseEvent)ev);
			}
		}
	}
}
