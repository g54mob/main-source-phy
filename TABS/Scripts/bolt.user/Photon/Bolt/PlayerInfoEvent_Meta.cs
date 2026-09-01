using System;
using Photon.Bolt.Internal;

namespace Photon.Bolt
{
	internal class PlayerInfoEvent_Meta : Event_Meta, IEventFactory, IFactory
	{
		internal static PlayerInfoEvent_Meta Instance;

		internal ObjectPool<PlayerInfoEvent> _pool;

		TypeId IFactory.TypeId => TypeId;

		UniqueId IFactory.TypeKey => new UniqueId(30, 244, 162, 128, 46, 36, 227, 66, 157, 202, 169, 51, 252, 242, 225, 108);

		Type IFactory.TypeObject => typeof(PlayerInfoEvent);

		static PlayerInfoEvent_Meta()
		{
			Instance = new PlayerInfoEvent_Meta();
			Instance.InitMeta();
		}

		internal override void InitObject(NetworkObj obj, Offsets offsets)
		{
		}

		internal override void InitMeta()
		{
			TypeId = new TypeId(24);
			CountStorage = 2;
			CountObjects = 1;
			CountProperties = 2;
			Properties = new NetworkPropertyInfo[2];
			NetworkProperty_String networkProperty_String = new NetworkProperty_String();
			networkProperty_String.PropertyMeta = this;
			networkProperty_String.Settings_Property("PlayerName", 1, -1073741824);
			networkProperty_String.Settings_Offsets(0, 0);
			networkProperty_String.AddStringSettings(StringEncodings.ASCII);
			AddProperty(0, 0, networkProperty_String, -1);
			NetworkProperty_Integer networkProperty_Integer = new NetworkProperty_Integer();
			networkProperty_Integer.PropertyMeta = this;
			networkProperty_Integer.Settings_Property("MultiplayerPlatform", 1, -1073741824);
			networkProperty_Integer.Settings_Offsets(1, 1);
			networkProperty_Integer.Settings_Integer(PropertyIntCompressionSettings.Create(4, 0));
			AddProperty(1, 0, networkProperty_Integer, -1);
			base.InitMeta();
			_pool = new ObjectPool<PlayerInfoEvent>();
		}

		object IFactory.Create()
		{
			return _pool.Get();
		}

		void IFactory.Return(object objToReturn)
		{
			_pool.Return(objToReturn as PlayerInfoEvent);
		}

		void IEventFactory.Dispatch(Event ev, object target)
		{
			if (target is IPlayerInfoEventListener playerInfoEventListener)
			{
				playerInfoEventListener.OnEvent((PlayerInfoEvent)ev);
			}
		}
	}
}
