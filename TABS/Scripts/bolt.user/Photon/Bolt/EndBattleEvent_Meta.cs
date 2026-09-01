using System;
using Photon.Bolt.Internal;

namespace Photon.Bolt
{
	internal class EndBattleEvent_Meta : Event_Meta, IEventFactory, IFactory
	{
		internal static EndBattleEvent_Meta Instance;

		internal ObjectPool<EndBattleEvent> _pool;

		TypeId IFactory.TypeId => TypeId;

		UniqueId IFactory.TypeKey => new UniqueId(4, 43, 40, 248, 183, 179, 220, 74, 146, 134, 0, 247, 197, 92, 179, 114);

		Type IFactory.TypeObject => typeof(EndBattleEvent);

		static EndBattleEvent_Meta()
		{
			Instance = new EndBattleEvent_Meta();
			Instance.InitMeta();
		}

		internal override void InitObject(NetworkObj obj, Offsets offsets)
		{
		}

		internal override void InitMeta()
		{
			TypeId = new TypeId(6);
			CountStorage = 1;
			CountObjects = 1;
			CountProperties = 1;
			Properties = new NetworkPropertyInfo[1];
			NetworkProperty_Integer networkProperty_Integer = new NetworkProperty_Integer();
			networkProperty_Integer.PropertyMeta = this;
			networkProperty_Integer.Settings_Property("WinningTeam", 1, -1073741824);
			networkProperty_Integer.Settings_Offsets(0, 0);
			networkProperty_Integer.Settings_Integer(PropertyIntCompressionSettings.Create());
			AddProperty(0, 0, networkProperty_Integer, -1);
			base.InitMeta();
			_pool = new ObjectPool<EndBattleEvent>();
		}

		object IFactory.Create()
		{
			return _pool.Get();
		}

		void IFactory.Return(object objToReturn)
		{
			_pool.Return(objToReturn as EndBattleEvent);
		}

		void IEventFactory.Dispatch(Event ev, object target)
		{
			if (target is IEndBattleEventListener endBattleEventListener)
			{
				endBattleEventListener.OnEvent((EndBattleEvent)ev);
			}
		}
	}
}
