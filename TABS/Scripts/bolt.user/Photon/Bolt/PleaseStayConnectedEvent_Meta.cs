using System;
using Photon.Bolt.Internal;

namespace Photon.Bolt
{
	internal class PleaseStayConnectedEvent_Meta : Event_Meta, IEventFactory, IFactory
	{
		internal static PleaseStayConnectedEvent_Meta Instance;

		internal ObjectPool<PleaseStayConnectedEvent> _pool;

		TypeId IFactory.TypeId => TypeId;

		UniqueId IFactory.TypeKey => new UniqueId(39, 247, 5, 117, 13, 169, 58, 68, 189, 64, 167, 57, 209, 239, 212, 128);

		Type IFactory.TypeObject => typeof(PleaseStayConnectedEvent);

		static PleaseStayConnectedEvent_Meta()
		{
			Instance = new PleaseStayConnectedEvent_Meta();
			Instance.InitMeta();
		}

		internal override void InitObject(NetworkObj obj, Offsets offsets)
		{
		}

		internal override void InitMeta()
		{
			TypeId = new TypeId(36);
			CountStorage = 0;
			CountObjects = 1;
			CountProperties = 0;
			Properties = new NetworkPropertyInfo[0];
			base.InitMeta();
			_pool = new ObjectPool<PleaseStayConnectedEvent>();
		}

		object IFactory.Create()
		{
			return _pool.Get();
		}

		void IFactory.Return(object objToReturn)
		{
			_pool.Return(objToReturn as PleaseStayConnectedEvent);
		}

		void IEventFactory.Dispatch(Event ev, object target)
		{
			if (target is IPleaseStayConnectedEventListener pleaseStayConnectedEventListener)
			{
				pleaseStayConnectedEventListener.OnEvent((PleaseStayConnectedEvent)ev);
			}
		}
	}
}
