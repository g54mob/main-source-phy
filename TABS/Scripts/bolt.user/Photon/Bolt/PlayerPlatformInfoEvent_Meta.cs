using System;
using Photon.Bolt.Internal;

namespace Photon.Bolt
{
	internal class PlayerPlatformInfoEvent_Meta : Event_Meta, IEventFactory, IFactory
	{
		internal static PlayerPlatformInfoEvent_Meta Instance;

		internal ObjectPool<PlayerPlatformInfoEvent> _pool;

		TypeId IFactory.TypeId => TypeId;

		UniqueId IFactory.TypeKey => new UniqueId(45, 154, 53, 27, 117, 27, 149, 69, 153, 222, 18, 195, 195, 120, 38, 13);

		Type IFactory.TypeObject => typeof(PlayerPlatformInfoEvent);

		static PlayerPlatformInfoEvent_Meta()
		{
			Instance = new PlayerPlatformInfoEvent_Meta();
			Instance.InitMeta();
		}

		internal override void InitObject(NetworkObj obj, Offsets offsets)
		{
		}

		internal override void InitMeta()
		{
			TypeId = new TypeId(33);
			CountStorage = 1;
			CountObjects = 1;
			CountProperties = 1;
			Properties = new NetworkPropertyInfo[1];
			NetworkProperty_String networkProperty_String = new NetworkProperty_String();
			networkProperty_String.PropertyMeta = this;
			networkProperty_String.Settings_Property("PlatformInfo", 1, -1073741824);
			networkProperty_String.Settings_Offsets(0, 0);
			networkProperty_String.AddStringSettings(StringEncodings.UTF8);
			AddProperty(0, 0, networkProperty_String, -1);
			base.InitMeta();
			_pool = new ObjectPool<PlayerPlatformInfoEvent>();
		}

		object IFactory.Create()
		{
			return _pool.Get();
		}

		void IFactory.Return(object objToReturn)
		{
			_pool.Return(objToReturn as PlayerPlatformInfoEvent);
		}

		void IEventFactory.Dispatch(Event ev, object target)
		{
			if (target is IPlayerPlatformInfoEventListener playerPlatformInfoEventListener)
			{
				playerPlatformInfoEventListener.OnEvent((PlayerPlatformInfoEvent)ev);
			}
		}
	}
}
