using System;
using Photon.Bolt.Internal;

namespace Photon.Bolt
{
	internal class FailedToLinkPooledUnitEvent_Meta : Event_Meta, IEventFactory, IFactory
	{
		internal static FailedToLinkPooledUnitEvent_Meta Instance;

		internal ObjectPool<FailedToLinkPooledUnitEvent> _pool;

		TypeId IFactory.TypeId => TypeId;

		UniqueId IFactory.TypeKey => new UniqueId(65, 133, 14, 138, 247, 135, 34, 74, 173, 113, 115, 102, 5, 41, 2, 128);

		Type IFactory.TypeObject => typeof(FailedToLinkPooledUnitEvent);

		static FailedToLinkPooledUnitEvent_Meta()
		{
			Instance = new FailedToLinkPooledUnitEvent_Meta();
			Instance.InitMeta();
		}

		internal override void InitObject(NetworkObj obj, Offsets offsets)
		{
		}

		internal override void InitMeta()
		{
			TypeId = new TypeId(35);
			CountStorage = 3;
			CountObjects = 1;
			CountProperties = 3;
			Properties = new NetworkPropertyInfo[3];
			NetworkProperty_Integer networkProperty_Integer = new NetworkProperty_Integer();
			networkProperty_Integer.PropertyMeta = this;
			networkProperty_Integer.Settings_Property("UnitInstanceId", 1, -1073741824);
			networkProperty_Integer.Settings_Offsets(0, 0);
			networkProperty_Integer.Settings_Integer(PropertyIntCompressionSettings.Create(16, 32768));
			AddProperty(0, 0, networkProperty_Integer, -1);
			NetworkProperty_Integer networkProperty_Integer2 = new NetworkProperty_Integer();
			networkProperty_Integer2.PropertyMeta = this;
			networkProperty_Integer2.Settings_Property("PoolIndex", 1, -1073741824);
			networkProperty_Integer2.Settings_Offsets(1, 1);
			networkProperty_Integer2.Settings_Integer(PropertyIntCompressionSettings.Create(4, 0));
			AddProperty(1, 0, networkProperty_Integer2, -1);
			NetworkProperty_Integer networkProperty_Integer3 = new NetworkProperty_Integer();
			networkProperty_Integer3.PropertyMeta = this;
			networkProperty_Integer3.Settings_Property("PoolId", 1, -1073741824);
			networkProperty_Integer3.Settings_Offsets(2, 2);
			networkProperty_Integer3.Settings_Integer(PropertyIntCompressionSettings.Create(16, 32768));
			AddProperty(2, 0, networkProperty_Integer3, -1);
			base.InitMeta();
			_pool = new ObjectPool<FailedToLinkPooledUnitEvent>();
		}

		object IFactory.Create()
		{
			return _pool.Get();
		}

		void IFactory.Return(object objToReturn)
		{
			_pool.Return(objToReturn as FailedToLinkPooledUnitEvent);
		}

		void IEventFactory.Dispatch(Event ev, object target)
		{
			if (target is IFailedToLinkPooledUnitEventListener failedToLinkPooledUnitEventListener)
			{
				failedToLinkPooledUnitEventListener.OnEvent((FailedToLinkPooledUnitEvent)ev);
			}
		}
	}
}
