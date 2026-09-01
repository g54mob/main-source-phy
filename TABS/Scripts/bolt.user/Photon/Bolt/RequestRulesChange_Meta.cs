using System;
using Photon.Bolt.Internal;

namespace Photon.Bolt
{
	internal class RequestRulesChange_Meta : Event_Meta, IEventFactory, IFactory
	{
		internal static RequestRulesChange_Meta Instance;

		internal ObjectPool<RequestRulesChange> _pool;

		TypeId IFactory.TypeId => TypeId;

		UniqueId IFactory.TypeKey => new UniqueId(110, 106, 99, 161, 88, 122, 232, 71, 186, 119, 180, 132, 42, 32, 117, 43);

		Type IFactory.TypeObject => typeof(RequestRulesChange);

		static RequestRulesChange_Meta()
		{
			Instance = new RequestRulesChange_Meta();
			Instance.InitMeta();
		}

		internal override void InitObject(NetworkObj obj, Offsets offsets)
		{
		}

		internal override void InitMeta()
		{
			TypeId = new TypeId(31);
			CountStorage = 3;
			CountObjects = 1;
			CountProperties = 3;
			Properties = new NetworkPropertyInfo[3];
			NetworkProperty_Integer networkProperty_Integer = new NetworkProperty_Integer();
			networkProperty_Integer.PropertyMeta = this;
			networkProperty_Integer.Settings_Property("MaxUnits", 1, -1073741824);
			networkProperty_Integer.Settings_Offsets(0, 0);
			networkProperty_Integer.Settings_Integer(PropertyIntCompressionSettings.Create());
			AddProperty(0, 0, networkProperty_Integer, -1);
			NetworkProperty_Integer networkProperty_Integer2 = new NetworkProperty_Integer();
			networkProperty_Integer2.PropertyMeta = this;
			networkProperty_Integer2.Settings_Property("MaxBudget", 1, -1073741824);
			networkProperty_Integer2.Settings_Offsets(1, 1);
			networkProperty_Integer2.Settings_Integer(PropertyIntCompressionSettings.Create());
			AddProperty(1, 0, networkProperty_Integer2, -1);
			NetworkProperty_Bool networkProperty_Bool = new NetworkProperty_Bool();
			networkProperty_Bool.PropertyMeta = this;
			networkProperty_Bool.Settings_Property("BlindMode", 1, -1073741824);
			networkProperty_Bool.Settings_Offsets(2, 2);
			AddProperty(2, 0, networkProperty_Bool, -1);
			base.InitMeta();
			_pool = new ObjectPool<RequestRulesChange>();
		}

		object IFactory.Create()
		{
			return _pool.Get();
		}

		void IFactory.Return(object objToReturn)
		{
			_pool.Return(objToReturn as RequestRulesChange);
		}

		void IEventFactory.Dispatch(Event ev, object target)
		{
			if (target is IRequestRulesChangeListener requestRulesChangeListener)
			{
				requestRulesChangeListener.OnEvent((RequestRulesChange)ev);
			}
		}
	}
}
