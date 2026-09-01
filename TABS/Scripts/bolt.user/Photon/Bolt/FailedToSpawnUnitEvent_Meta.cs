using System;
using Photon.Bolt.Internal;

namespace Photon.Bolt
{
	internal class FailedToSpawnUnitEvent_Meta : Event_Meta, IEventFactory, IFactory
	{
		internal static FailedToSpawnUnitEvent_Meta Instance;

		internal ObjectPool<FailedToSpawnUnitEvent> _pool;

		TypeId IFactory.TypeId => TypeId;

		UniqueId IFactory.TypeKey => new UniqueId(239, 92, 172, 208, 20, 229, 209, 79, 187, 158, 234, 157, 125, 138, 249, 10);

		Type IFactory.TypeObject => typeof(FailedToSpawnUnitEvent);

		static FailedToSpawnUnitEvent_Meta()
		{
			Instance = new FailedToSpawnUnitEvent_Meta();
			Instance.InitMeta();
		}

		internal override void InitObject(NetworkObj obj, Offsets offsets)
		{
		}

		internal override void InitMeta()
		{
			TypeId = new TypeId(14);
			CountStorage = 1;
			CountObjects = 1;
			CountProperties = 1;
			Properties = new NetworkPropertyInfo[1];
			NetworkProperty_Integer networkProperty_Integer = new NetworkProperty_Integer();
			networkProperty_Integer.PropertyMeta = this;
			networkProperty_Integer.Settings_Property("UnitInstanceId", 1, -1073741824);
			networkProperty_Integer.Settings_Offsets(0, 0);
			networkProperty_Integer.Settings_Integer(PropertyIntCompressionSettings.Create(16, 32768));
			AddProperty(0, 0, networkProperty_Integer, -1);
			base.InitMeta();
			_pool = new ObjectPool<FailedToSpawnUnitEvent>();
		}

		object IFactory.Create()
		{
			return _pool.Get();
		}

		void IFactory.Return(object objToReturn)
		{
			_pool.Return(objToReturn as FailedToSpawnUnitEvent);
		}

		void IEventFactory.Dispatch(Event ev, object target)
		{
			if (target is IFailedToSpawnUnitEventListener failedToSpawnUnitEventListener)
			{
				failedToSpawnUnitEventListener.OnEvent((FailedToSpawnUnitEvent)ev);
			}
		}
	}
}
