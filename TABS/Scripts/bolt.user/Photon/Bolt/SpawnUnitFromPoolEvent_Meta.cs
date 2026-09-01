using System;
using Photon.Bolt.Internal;

namespace Photon.Bolt
{
	internal class SpawnUnitFromPoolEvent_Meta : Event_Meta, IEventFactory, IFactory
	{
		internal static SpawnUnitFromPoolEvent_Meta Instance;

		internal ObjectPool<SpawnUnitFromPoolEvent> _pool;

		TypeId IFactory.TypeId => TypeId;

		UniqueId IFactory.TypeKey => new UniqueId(92, 90, 202, 123, 164, 225, 90, 68, 157, 201, 131, 39, 186, 180, 238, 184);

		Type IFactory.TypeObject => typeof(SpawnUnitFromPoolEvent);

		static SpawnUnitFromPoolEvent_Meta()
		{
			Instance = new SpawnUnitFromPoolEvent_Meta();
			Instance.InitMeta();
		}

		internal override void InitObject(NetworkObj obj, Offsets offsets)
		{
		}

		internal override void InitMeta()
		{
			TypeId = new TypeId(34);
			CountStorage = 4;
			CountObjects = 1;
			CountProperties = 4;
			Properties = new NetworkPropertyInfo[4];
			NetworkProperty_Integer networkProperty_Integer = new NetworkProperty_Integer();
			networkProperty_Integer.PropertyMeta = this;
			networkProperty_Integer.Settings_Property("SpawnSource", 1, -1073741824);
			networkProperty_Integer.Settings_Offsets(0, 0);
			networkProperty_Integer.Settings_Integer(PropertyIntCompressionSettings.Create(1, 0));
			AddProperty(0, 0, networkProperty_Integer, -1);
			NetworkProperty_Integer networkProperty_Integer2 = new NetworkProperty_Integer();
			networkProperty_Integer2.PropertyMeta = this;
			networkProperty_Integer2.Settings_Property("UnitSmallNetworkId", 1, -1073741824);
			networkProperty_Integer2.Settings_Offsets(1, 1);
			networkProperty_Integer2.Settings_Integer(PropertyIntCompressionSettings.Create(16, 0));
			AddProperty(1, 0, networkProperty_Integer2, -1);
			NetworkProperty_Vector networkProperty_Vector = new NetworkProperty_Vector();
			networkProperty_Vector.PropertyMeta = this;
			networkProperty_Vector.Settings_Property("UnitSpawnPosition", 1, -1073741824);
			networkProperty_Vector.Settings_Offsets(2, 2);
			networkProperty_Vector.Settings_Vector(PropertyFloatCompressionSettings.Create(), PropertyFloatCompressionSettings.Create(), PropertyFloatCompressionSettings.Create(), strict: false);
			AddProperty(2, 0, networkProperty_Vector, -1);
			NetworkProperty_Integer networkProperty_Integer3 = new NetworkProperty_Integer();
			networkProperty_Integer3.PropertyMeta = this;
			networkProperty_Integer3.Settings_Property("CopyOfSmallNetworkId", 1, -1073741824);
			networkProperty_Integer3.Settings_Offsets(3, 3);
			networkProperty_Integer3.Settings_Integer(PropertyIntCompressionSettings.Create(16, 0));
			AddProperty(3, 0, networkProperty_Integer3, -1);
			base.InitMeta();
			_pool = new ObjectPool<SpawnUnitFromPoolEvent>();
		}

		object IFactory.Create()
		{
			return _pool.Get();
		}

		void IFactory.Return(object objToReturn)
		{
			_pool.Return(objToReturn as SpawnUnitFromPoolEvent);
		}

		void IEventFactory.Dispatch(Event ev, object target)
		{
			if (target is ISpawnUnitFromPoolEventListener spawnUnitFromPoolEventListener)
			{
				spawnUnitFromPoolEventListener.OnEvent((SpawnUnitFromPoolEvent)ev);
			}
		}
	}
}
