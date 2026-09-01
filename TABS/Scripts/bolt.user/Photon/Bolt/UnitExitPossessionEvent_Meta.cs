using System;
using Photon.Bolt.Internal;

namespace Photon.Bolt
{
	internal class UnitExitPossessionEvent_Meta : Event_Meta, IEventFactory, IFactory
	{
		internal static UnitExitPossessionEvent_Meta Instance;

		internal ObjectPool<UnitExitPossessionEvent> _pool;

		TypeId IFactory.TypeId => TypeId;

		UniqueId IFactory.TypeKey => new UniqueId(126, 252, 97, 6, 38, 142, 248, 78, 181, 137, 31, 5, 145, 186, 92, 153);

		Type IFactory.TypeObject => typeof(UnitExitPossessionEvent);

		static UnitExitPossessionEvent_Meta()
		{
			Instance = new UnitExitPossessionEvent_Meta();
			Instance.InitMeta();
		}

		internal override void InitObject(NetworkObj obj, Offsets offsets)
		{
		}

		internal override void InitMeta()
		{
			TypeId = new TypeId(18);
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
			_pool = new ObjectPool<UnitExitPossessionEvent>();
		}

		object IFactory.Create()
		{
			return _pool.Get();
		}

		void IFactory.Return(object objToReturn)
		{
			_pool.Return(objToReturn as UnitExitPossessionEvent);
		}

		void IEventFactory.Dispatch(Event ev, object target)
		{
			if (target is IUnitExitPossessionEventListener unitExitPossessionEventListener)
			{
				unitExitPossessionEventListener.OnEvent((UnitExitPossessionEvent)ev);
			}
		}
	}
}
