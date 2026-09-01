using System;
using Photon.Bolt.Internal;

namespace Photon.Bolt
{
	internal class UnitAttackEvent_Meta : Event_Meta, IEventFactory, IFactory
	{
		internal static UnitAttackEvent_Meta Instance;

		internal ObjectPool<UnitAttackEvent> _pool;

		TypeId IFactory.TypeId => TypeId;

		UniqueId IFactory.TypeKey => new UniqueId(193, 221, 172, 217, 196, 200, 44, 75, 174, 118, 223, 92, 252, 21, 19, 74);

		Type IFactory.TypeObject => typeof(UnitAttackEvent);

		static UnitAttackEvent_Meta()
		{
			Instance = new UnitAttackEvent_Meta();
			Instance.InitMeta();
		}

		internal override void InitObject(NetworkObj obj, Offsets offsets)
		{
		}

		internal override void InitMeta()
		{
			TypeId = new TypeId(7);
			CountStorage = 4;
			CountObjects = 1;
			CountProperties = 4;
			Properties = new NetworkPropertyInfo[4];
			NetworkProperty_Integer networkProperty_Integer = new NetworkProperty_Integer();
			networkProperty_Integer.PropertyMeta = this;
			networkProperty_Integer.Settings_Property("TargetUnitSmallNetworkId", 1, -1073741824);
			networkProperty_Integer.Settings_Offsets(0, 0);
			networkProperty_Integer.Settings_Integer(PropertyIntCompressionSettings.Create(16, 0));
			AddProperty(0, 0, networkProperty_Integer, -1);
			NetworkProperty_Vector networkProperty_Vector = new NetworkProperty_Vector();
			networkProperty_Vector.PropertyMeta = this;
			networkProperty_Vector.Settings_Property("Position", 1, -1073741824);
			networkProperty_Vector.Settings_Offsets(1, 1);
			networkProperty_Vector.Settings_Vector(PropertyFloatCompressionSettings.Create(), PropertyFloatCompressionSettings.Create(), PropertyFloatCompressionSettings.Create(), strict: false);
			AddProperty(1, 0, networkProperty_Vector, -1);
			NetworkProperty_Vector networkProperty_Vector2 = new NetworkProperty_Vector();
			networkProperty_Vector2.PropertyMeta = this;
			networkProperty_Vector2.Settings_Property("ForceDirection", 1, -1073741824);
			networkProperty_Vector2.Settings_Offsets(2, 2);
			networkProperty_Vector2.Settings_Vector(PropertyFloatCompressionSettings.Create(), PropertyFloatCompressionSettings.Create(), PropertyFloatCompressionSettings.Create(), strict: false);
			AddProperty(2, 0, networkProperty_Vector2, -1);
			NetworkProperty_Integer networkProperty_Integer2 = new NetworkProperty_Integer();
			networkProperty_Integer2.PropertyMeta = this;
			networkProperty_Integer2.Settings_Property("ForceWeapon", 1, -1073741824);
			networkProperty_Integer2.Settings_Offsets(3, 3);
			networkProperty_Integer2.Settings_Integer(PropertyIntCompressionSettings.Create());
			AddProperty(3, 0, networkProperty_Integer2, -1);
			base.InitMeta();
			_pool = new ObjectPool<UnitAttackEvent>();
		}

		object IFactory.Create()
		{
			return _pool.Get();
		}

		void IFactory.Return(object objToReturn)
		{
			_pool.Return(objToReturn as UnitAttackEvent);
		}

		void IEventFactory.Dispatch(Event ev, object target)
		{
			if (target is IUnitAttackEventListener unitAttackEventListener)
			{
				unitAttackEventListener.OnEvent((UnitAttackEvent)ev);
			}
		}
	}
}
