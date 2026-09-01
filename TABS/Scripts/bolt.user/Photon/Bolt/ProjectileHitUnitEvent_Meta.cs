using System;
using Photon.Bolt.Internal;

namespace Photon.Bolt
{
	internal class ProjectileHitUnitEvent_Meta : Event_Meta, IEventFactory, IFactory
	{
		internal static ProjectileHitUnitEvent_Meta Instance;

		internal ObjectPool<ProjectileHitUnitEvent> _pool;

		TypeId IFactory.TypeId => TypeId;

		UniqueId IFactory.TypeKey => new UniqueId(5, 136, 117, 165, 42, 208, 39, 78, 184, 208, 118, 181, 17, 48, 108, 7);

		Type IFactory.TypeObject => typeof(ProjectileHitUnitEvent);

		static ProjectileHitUnitEvent_Meta()
		{
			Instance = new ProjectileHitUnitEvent_Meta();
			Instance.InitMeta();
		}

		internal override void InitObject(NetworkObj obj, Offsets offsets)
		{
		}

		internal override void InitMeta()
		{
			TypeId = new TypeId(27);
			CountStorage = 2;
			CountObjects = 1;
			CountProperties = 2;
			Properties = new NetworkPropertyInfo[2];
			NetworkProperty_Integer networkProperty_Integer = new NetworkProperty_Integer();
			networkProperty_Integer.PropertyMeta = this;
			networkProperty_Integer.Settings_Property("ProjectileNetworkId", 1, -1073741824);
			networkProperty_Integer.Settings_Offsets(0, 0);
			networkProperty_Integer.Settings_Integer(PropertyIntCompressionSettings.Create(16, 0));
			AddProperty(0, 0, networkProperty_Integer, -1);
			NetworkProperty_Integer networkProperty_Integer2 = new NetworkProperty_Integer();
			networkProperty_Integer2.PropertyMeta = this;
			networkProperty_Integer2.Settings_Property("UnitSmallNetworkId", 1, -1073741824);
			networkProperty_Integer2.Settings_Offsets(1, 1);
			networkProperty_Integer2.Settings_Integer(PropertyIntCompressionSettings.Create(16, 0));
			AddProperty(1, 0, networkProperty_Integer2, -1);
			base.InitMeta();
			_pool = new ObjectPool<ProjectileHitUnitEvent>();
		}

		object IFactory.Create()
		{
			return _pool.Get();
		}

		void IFactory.Return(object objToReturn)
		{
			_pool.Return(objToReturn as ProjectileHitUnitEvent);
		}

		void IEventFactory.Dispatch(Event ev, object target)
		{
			if (target is IProjectileHitUnitEventListener projectileHitUnitEventListener)
			{
				projectileHitUnitEventListener.OnEvent((ProjectileHitUnitEvent)ev);
			}
		}
	}
}
