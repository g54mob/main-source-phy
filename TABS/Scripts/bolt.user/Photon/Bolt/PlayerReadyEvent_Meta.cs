using System;
using Photon.Bolt.Internal;

namespace Photon.Bolt
{
	internal class PlayerReadyEvent_Meta : Event_Meta, IEventFactory, IFactory
	{
		internal static PlayerReadyEvent_Meta Instance;

		internal ObjectPool<PlayerReadyEvent> _pool;

		TypeId IFactory.TypeId => TypeId;

		UniqueId IFactory.TypeKey => new UniqueId(223, 133, 240, 88, 128, 193, 247, 64, 135, 46, 95, 9, 9, 183, 236, 63);

		Type IFactory.TypeObject => typeof(PlayerReadyEvent);

		static PlayerReadyEvent_Meta()
		{
			Instance = new PlayerReadyEvent_Meta();
			Instance.InitMeta();
		}

		internal override void InitObject(NetworkObj obj, Offsets offsets)
		{
		}

		internal override void InitMeta()
		{
			TypeId = new TypeId(1);
			CountStorage = 2;
			CountObjects = 1;
			CountProperties = 2;
			Properties = new NetworkPropertyInfo[2];
			NetworkProperty_Integer networkProperty_Integer = new NetworkProperty_Integer();
			networkProperty_Integer.PropertyMeta = this;
			networkProperty_Integer.Settings_Property("Team", 1, -1073741824);
			networkProperty_Integer.Settings_Offsets(0, 0);
			networkProperty_Integer.Settings_Integer(PropertyIntCompressionSettings.Create());
			AddProperty(0, 0, networkProperty_Integer, -1);
			NetworkProperty_Bool networkProperty_Bool = new NetworkProperty_Bool();
			networkProperty_Bool.PropertyMeta = this;
			networkProperty_Bool.Settings_Property("IsReady", 1, -1073741824);
			networkProperty_Bool.Settings_Offsets(1, 1);
			AddProperty(1, 0, networkProperty_Bool, -1);
			base.InitMeta();
			_pool = new ObjectPool<PlayerReadyEvent>();
		}

		object IFactory.Create()
		{
			return _pool.Get();
		}

		void IFactory.Return(object objToReturn)
		{
			_pool.Return(objToReturn as PlayerReadyEvent);
		}

		void IEventFactory.Dispatch(Event ev, object target)
		{
			if (target is IPlayerReadyEventListener playerReadyEventListener)
			{
				playerReadyEventListener.OnEvent((PlayerReadyEvent)ev);
			}
		}
	}
}
