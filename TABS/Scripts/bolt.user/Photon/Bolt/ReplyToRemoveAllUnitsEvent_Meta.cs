using System;
using Photon.Bolt.Internal;

namespace Photon.Bolt
{
	internal class ReplyToRemoveAllUnitsEvent_Meta : Event_Meta, IEventFactory, IFactory
	{
		internal static ReplyToRemoveAllUnitsEvent_Meta Instance;

		internal ObjectPool<ReplyToRemoveAllUnitsEvent> _pool;

		TypeId IFactory.TypeId => TypeId;

		UniqueId IFactory.TypeKey => new UniqueId(138, 21, 251, 147, 85, 0, 219, 68, 128, 218, 208, 29, 187, 175, 171, 87);

		Type IFactory.TypeObject => typeof(ReplyToRemoveAllUnitsEvent);

		static ReplyToRemoveAllUnitsEvent_Meta()
		{
			Instance = new ReplyToRemoveAllUnitsEvent_Meta();
			Instance.InitMeta();
		}

		internal override void InitObject(NetworkObj obj, Offsets offsets)
		{
		}

		internal override void InitMeta()
		{
			TypeId = new TypeId(10);
			CountStorage = 1;
			CountObjects = 1;
			CountProperties = 1;
			Properties = new NetworkPropertyInfo[1];
			NetworkProperty_Integer networkProperty_Integer = new NetworkProperty_Integer();
			networkProperty_Integer.PropertyMeta = this;
			networkProperty_Integer.Settings_Property("Team", 1, -1073741824);
			networkProperty_Integer.Settings_Offsets(0, 0);
			networkProperty_Integer.Settings_Integer(PropertyIntCompressionSettings.Create());
			AddProperty(0, 0, networkProperty_Integer, -1);
			base.InitMeta();
			_pool = new ObjectPool<ReplyToRemoveAllUnitsEvent>();
		}

		object IFactory.Create()
		{
			return _pool.Get();
		}

		void IFactory.Return(object objToReturn)
		{
			_pool.Return(objToReturn as ReplyToRemoveAllUnitsEvent);
		}

		void IEventFactory.Dispatch(Event ev, object target)
		{
			if (target is IReplyToRemoveAllUnitsEventListener replyToRemoveAllUnitsEventListener)
			{
				replyToRemoveAllUnitsEventListener.OnEvent((ReplyToRemoveAllUnitsEvent)ev);
			}
		}
	}
}
