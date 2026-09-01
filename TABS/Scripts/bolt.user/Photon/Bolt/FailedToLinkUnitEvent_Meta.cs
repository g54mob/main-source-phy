using System;
using Photon.Bolt.Internal;

namespace Photon.Bolt
{
	internal class FailedToLinkUnitEvent_Meta : Event_Meta, IEventFactory, IFactory
	{
		internal static FailedToLinkUnitEvent_Meta Instance;

		internal ObjectPool<FailedToLinkUnitEvent> _pool;

		TypeId IFactory.TypeId => TypeId;

		UniqueId IFactory.TypeKey => new UniqueId(149, 195, 228, 177, 249, 150, 56, 69, 128, 86, 106, 248, 187, 193, 95, 123);

		Type IFactory.TypeObject => typeof(FailedToLinkUnitEvent);

		static FailedToLinkUnitEvent_Meta()
		{
			Instance = new FailedToLinkUnitEvent_Meta();
			Instance.InitMeta();
		}

		internal override void InitObject(NetworkObj obj, Offsets offsets)
		{
		}

		internal override void InitMeta()
		{
			TypeId = new TypeId(25);
			CountStorage = 2;
			CountObjects = 1;
			CountProperties = 2;
			Properties = new NetworkPropertyInfo[2];
			NetworkProperty_Integer networkProperty_Integer = new NetworkProperty_Integer();
			networkProperty_Integer.PropertyMeta = this;
			networkProperty_Integer.Settings_Property("UnitInstanceId", 1, -1073741824);
			networkProperty_Integer.Settings_Offsets(0, 0);
			networkProperty_Integer.Settings_Integer(PropertyIntCompressionSettings.Create(16, 32768));
			AddProperty(0, 0, networkProperty_Integer, -1);
			NetworkProperty_Integer networkProperty_Integer2 = new NetworkProperty_Integer();
			networkProperty_Integer2.PropertyMeta = this;
			networkProperty_Integer2.Settings_Property("Team", 1, -1073741824);
			networkProperty_Integer2.Settings_Offsets(1, 1);
			networkProperty_Integer2.Settings_Integer(PropertyIntCompressionSettings.Create(1, 0));
			AddProperty(1, 0, networkProperty_Integer2, -1);
			base.InitMeta();
			_pool = new ObjectPool<FailedToLinkUnitEvent>();
		}

		object IFactory.Create()
		{
			return _pool.Get();
		}

		void IFactory.Return(object objToReturn)
		{
			_pool.Return(objToReturn as FailedToLinkUnitEvent);
		}

		void IEventFactory.Dispatch(Event ev, object target)
		{
			if (target is IFailedToLinkUnitEventListener failedToLinkUnitEventListener)
			{
				failedToLinkUnitEventListener.OnEvent((FailedToLinkUnitEvent)ev);
			}
		}
	}
}
