using System;
using Photon.Bolt.Internal;

namespace Photon.Bolt
{
	internal class RespondRuleChange_Meta : Event_Meta, IEventFactory, IFactory
	{
		internal static RespondRuleChange_Meta Instance;

		internal ObjectPool<RespondRuleChange> _pool;

		TypeId IFactory.TypeId => TypeId;

		UniqueId IFactory.TypeKey => new UniqueId(89, 126, 84, 115, 17, 131, 226, 66, 184, 85, 199, 203, 17, 109, 166, 132);

		Type IFactory.TypeObject => typeof(RespondRuleChange);

		static RespondRuleChange_Meta()
		{
			Instance = new RespondRuleChange_Meta();
			Instance.InitMeta();
		}

		internal override void InitObject(NetworkObj obj, Offsets offsets)
		{
		}

		internal override void InitMeta()
		{
			TypeId = new TypeId(32);
			CountStorage = 4;
			CountObjects = 1;
			CountProperties = 4;
			Properties = new NetworkPropertyInfo[4];
			NetworkProperty_Bool networkProperty_Bool = new NetworkProperty_Bool();
			networkProperty_Bool.PropertyMeta = this;
			networkProperty_Bool.Settings_Property("Status", 1, -1073741824);
			networkProperty_Bool.Settings_Offsets(0, 0);
			AddProperty(0, 0, networkProperty_Bool, -1);
			NetworkProperty_Integer networkProperty_Integer = new NetworkProperty_Integer();
			networkProperty_Integer.PropertyMeta = this;
			networkProperty_Integer.Settings_Property("MaxUnits", 1, -1073741824);
			networkProperty_Integer.Settings_Offsets(1, 1);
			networkProperty_Integer.Settings_Integer(PropertyIntCompressionSettings.Create());
			AddProperty(1, 0, networkProperty_Integer, -1);
			NetworkProperty_Integer networkProperty_Integer2 = new NetworkProperty_Integer();
			networkProperty_Integer2.PropertyMeta = this;
			networkProperty_Integer2.Settings_Property("MaxBudget", 1, -1073741824);
			networkProperty_Integer2.Settings_Offsets(2, 2);
			networkProperty_Integer2.Settings_Integer(PropertyIntCompressionSettings.Create());
			AddProperty(2, 0, networkProperty_Integer2, -1);
			NetworkProperty_Bool networkProperty_Bool2 = new NetworkProperty_Bool();
			networkProperty_Bool2.PropertyMeta = this;
			networkProperty_Bool2.Settings_Property("BlindMode", 1, -1073741824);
			networkProperty_Bool2.Settings_Offsets(3, 3);
			AddProperty(3, 0, networkProperty_Bool2, -1);
			base.InitMeta();
			_pool = new ObjectPool<RespondRuleChange>();
		}

		object IFactory.Create()
		{
			return _pool.Get();
		}

		void IFactory.Return(object objToReturn)
		{
			_pool.Return(objToReturn as RespondRuleChange);
		}

		void IEventFactory.Dispatch(Event ev, object target)
		{
			if (target is IRespondRuleChangeListener respondRuleChangeListener)
			{
				respondRuleChangeListener.OnEvent((RespondRuleChange)ev);
			}
		}
	}
}
