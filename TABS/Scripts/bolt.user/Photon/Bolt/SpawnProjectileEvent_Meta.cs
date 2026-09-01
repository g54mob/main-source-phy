using System;
using Photon.Bolt.Internal;

namespace Photon.Bolt
{
	internal class SpawnProjectileEvent_Meta : Event_Meta, IEventFactory, IFactory
	{
		internal static SpawnProjectileEvent_Meta Instance;

		internal ObjectPool<SpawnProjectileEvent> _pool;

		TypeId IFactory.TypeId => TypeId;

		UniqueId IFactory.TypeKey => new UniqueId(76, 196, 168, 24, 232, 205, 224, 65, 136, 237, 57, 185, 198, 89, 165, 185);

		Type IFactory.TypeObject => typeof(SpawnProjectileEvent);

		static SpawnProjectileEvent_Meta()
		{
			Instance = new SpawnProjectileEvent_Meta();
			Instance.InitMeta();
		}

		internal override void InitObject(NetworkObj obj, Offsets offsets)
		{
		}

		internal override void InitMeta()
		{
			TypeId = new TypeId(22);
			CountStorage = 1;
			CountObjects = 1;
			CountProperties = 1;
			Properties = new NetworkPropertyInfo[1];
			NetworkProperty_ProtocolToken networkProperty_ProtocolToken = new NetworkProperty_ProtocolToken();
			networkProperty_ProtocolToken.PropertyMeta = this;
			networkProperty_ProtocolToken.Settings_Property("SpawnToken", 1, -1073741824);
			networkProperty_ProtocolToken.Settings_Offsets(0, 0);
			AddProperty(0, 0, networkProperty_ProtocolToken, -1);
			base.InitMeta();
			_pool = new ObjectPool<SpawnProjectileEvent>();
		}

		object IFactory.Create()
		{
			return _pool.Get();
		}

		void IFactory.Return(object objToReturn)
		{
			_pool.Return(objToReturn as SpawnProjectileEvent);
		}

		void IEventFactory.Dispatch(Event ev, object target)
		{
			if (target is ISpawnProjectileEventListener spawnProjectileEventListener)
			{
				spawnProjectileEventListener.OnEvent((SpawnProjectileEvent)ev);
			}
		}
	}
}
