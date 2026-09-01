using System;
using Photon.Bolt.Internal;

namespace Photon.Bolt
{
	internal class ReplyPlaceUnitEvent_Meta : Event_Meta, IEventFactory, IFactory
	{
		internal static ReplyPlaceUnitEvent_Meta Instance;

		internal ObjectPool<ReplyPlaceUnitEvent> _pool;

		TypeId IFactory.TypeId => TypeId;

		UniqueId IFactory.TypeKey => new UniqueId(125, 197, 164, 207, 3, 86, 97, 79, 163, 11, 208, 152, 6, 37, 247, 137);

		Type IFactory.TypeObject => typeof(ReplyPlaceUnitEvent);

		static ReplyPlaceUnitEvent_Meta()
		{
			Instance = new ReplyPlaceUnitEvent_Meta();
			Instance.InitMeta();
		}

		internal override void InitObject(NetworkObj obj, Offsets offsets)
		{
		}

		internal override void InitMeta()
		{
			TypeId = new TypeId(15);
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
			_pool = new ObjectPool<ReplyPlaceUnitEvent>();
		}

		object IFactory.Create()
		{
			return _pool.Get();
		}

		void IFactory.Return(object objToReturn)
		{
			_pool.Return(objToReturn as ReplyPlaceUnitEvent);
		}

		void IEventFactory.Dispatch(Event ev, object target)
		{
			if (target is IReplyPlaceUnitEventListener replyPlaceUnitEventListener)
			{
				replyPlaceUnitEventListener.OnEvent((ReplyPlaceUnitEvent)ev);
			}
		}
	}
}
