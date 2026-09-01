using System;
using Photon.Bolt.Internal;

namespace Photon.Bolt
{
	internal class PlayerQuitEvent_Meta : Event_Meta, IEventFactory, IFactory
	{
		internal static PlayerQuitEvent_Meta Instance;

		internal ObjectPool<PlayerQuitEvent> _pool;

		TypeId IFactory.TypeId => TypeId;

		UniqueId IFactory.TypeKey => new UniqueId(204, 116, 171, 191, 123, 143, 240, 71, 186, 128, 165, 124, 227, 35, 219, 37);

		Type IFactory.TypeObject => typeof(PlayerQuitEvent);

		static PlayerQuitEvent_Meta()
		{
			Instance = new PlayerQuitEvent_Meta();
			Instance.InitMeta();
		}

		internal override void InitObject(NetworkObj obj, Offsets offsets)
		{
		}

		internal override void InitMeta()
		{
			TypeId = new TypeId(8);
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
			_pool = new ObjectPool<PlayerQuitEvent>();
		}

		object IFactory.Create()
		{
			return _pool.Get();
		}

		void IFactory.Return(object objToReturn)
		{
			_pool.Return(objToReturn as PlayerQuitEvent);
		}

		void IEventFactory.Dispatch(Event ev, object target)
		{
			if (target is IPlayerQuitEventListener playerQuitEventListener)
			{
				playerQuitEventListener.OnEvent((PlayerQuitEvent)ev);
			}
		}
	}
}
