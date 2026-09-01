using System;
using Photon.Bolt.Internal;

namespace Photon.Bolt
{
	internal class PlaceUnitEvent_Meta : Event_Meta, IEventFactory, IFactory
	{
		internal static PlaceUnitEvent_Meta Instance;

		internal ObjectPool<PlaceUnitEvent> _pool;

		TypeId IFactory.TypeId => TypeId;

		UniqueId IFactory.TypeKey => new UniqueId(176, 95, 204, 49, 101, 3, 155, 77, 169, 82, 128, 44, 202, 223, 152, 83);

		Type IFactory.TypeObject => typeof(PlaceUnitEvent);

		static PlaceUnitEvent_Meta()
		{
			Instance = new PlaceUnitEvent_Meta();
			Instance.InitMeta();
		}

		internal override void InitObject(NetworkObj obj, Offsets offsets)
		{
		}

		internal override void InitMeta()
		{
			TypeId = new TypeId(3);
			CountStorage = 6;
			CountObjects = 1;
			CountProperties = 6;
			Properties = new NetworkPropertyInfo[6];
			NetworkProperty_Integer networkProperty_Integer = new NetworkProperty_Integer();
			networkProperty_Integer.PropertyMeta = this;
			networkProperty_Integer.Settings_Property("UnitId", 1, -1073741824);
			networkProperty_Integer.Settings_Offsets(0, 0);
			networkProperty_Integer.Settings_Integer(PropertyIntCompressionSettings.Create());
			AddProperty(0, 0, networkProperty_Integer, -1);
			NetworkProperty_Integer networkProperty_Integer2 = new NetworkProperty_Integer();
			networkProperty_Integer2.PropertyMeta = this;
			networkProperty_Integer2.Settings_Property("UnitModId", 1, -1073741824);
			networkProperty_Integer2.Settings_Offsets(1, 1);
			networkProperty_Integer2.Settings_Integer(PropertyIntCompressionSettings.Create());
			AddProperty(1, 0, networkProperty_Integer2, -1);
			NetworkProperty_Vector networkProperty_Vector = new NetworkProperty_Vector();
			networkProperty_Vector.PropertyMeta = this;
			networkProperty_Vector.Settings_Property("Position", 1, -1073741824);
			networkProperty_Vector.Settings_Offsets(2, 2);
			networkProperty_Vector.Settings_Vector(PropertyFloatCompressionSettings.Create(), PropertyFloatCompressionSettings.Create(), PropertyFloatCompressionSettings.Create(), strict: false);
			AddProperty(2, 0, networkProperty_Vector, -1);
			NetworkProperty_Quaternion networkProperty_Quaternion = new NetworkProperty_Quaternion();
			networkProperty_Quaternion.PropertyMeta = this;
			networkProperty_Quaternion.Settings_Property("Rotation", 1, -1073741824);
			networkProperty_Quaternion.Settings_Offsets(3, 3);
			networkProperty_Quaternion.Settings_Quaternion(PropertyFloatCompressionSettings.Create(), strict: false);
			AddProperty(3, 0, networkProperty_Quaternion, -1);
			NetworkProperty_Bool networkProperty_Bool = new NetworkProperty_Bool();
			networkProperty_Bool.PropertyMeta = this;
			networkProperty_Bool.Settings_Property("IsCampaignUnit", 1, -1073741824);
			networkProperty_Bool.Settings_Offsets(4, 4);
			AddProperty(4, 0, networkProperty_Bool, -1);
			NetworkProperty_Integer networkProperty_Integer3 = new NetworkProperty_Integer();
			networkProperty_Integer3.PropertyMeta = this;
			networkProperty_Integer3.Settings_Property("UnitInstanceId", 1, -1073741824);
			networkProperty_Integer3.Settings_Offsets(5, 5);
			networkProperty_Integer3.Settings_Integer(PropertyIntCompressionSettings.Create(16, 32768));
			AddProperty(5, 0, networkProperty_Integer3, -1);
			base.InitMeta();
			_pool = new ObjectPool<PlaceUnitEvent>();
		}

		object IFactory.Create()
		{
			return _pool.Get();
		}

		void IFactory.Return(object objToReturn)
		{
			_pool.Return(objToReturn as PlaceUnitEvent);
		}

		void IEventFactory.Dispatch(Event ev, object target)
		{
			if (target is IPlaceUnitEventListener placeUnitEventListener)
			{
				placeUnitEventListener.OnEvent((PlaceUnitEvent)ev);
			}
		}
	}
}
