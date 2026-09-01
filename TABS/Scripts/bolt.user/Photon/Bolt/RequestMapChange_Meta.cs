using System;
using Photon.Bolt.Internal;

namespace Photon.Bolt
{
	internal class RequestMapChange_Meta : Event_Meta, IEventFactory, IFactory
	{
		internal static RequestMapChange_Meta Instance;

		internal ObjectPool<RequestMapChange> _pool;

		TypeId IFactory.TypeId => TypeId;

		UniqueId IFactory.TypeKey => new UniqueId(155, 212, 163, 241, 148, 112, 155, 74, 188, 70, 208, 19, 198, 135, 183, 131);

		Type IFactory.TypeObject => typeof(RequestMapChange);

		static RequestMapChange_Meta()
		{
			Instance = new RequestMapChange_Meta();
			Instance.InitMeta();
		}

		internal override void InitObject(NetworkObj obj, Offsets offsets)
		{
		}

		internal override void InitMeta()
		{
			TypeId = new TypeId(12);
			CountStorage = 2;
			CountObjects = 1;
			CountProperties = 2;
			Properties = new NetworkPropertyInfo[2];
			NetworkProperty_Integer networkProperty_Integer = new NetworkProperty_Integer();
			networkProperty_Integer.PropertyMeta = this;
			networkProperty_Integer.Settings_Property("MapType", 1, -1073741824);
			networkProperty_Integer.Settings_Offsets(0, 0);
			networkProperty_Integer.Settings_Integer(PropertyIntCompressionSettings.Create());
			AddProperty(0, 0, networkProperty_Integer, -1);
			NetworkProperty_Integer networkProperty_Integer2 = new NetworkProperty_Integer();
			networkProperty_Integer2.PropertyMeta = this;
			networkProperty_Integer2.Settings_Property("MapIndex", 1, -1073741824);
			networkProperty_Integer2.Settings_Offsets(1, 1);
			networkProperty_Integer2.Settings_Integer(PropertyIntCompressionSettings.Create());
			AddProperty(1, 0, networkProperty_Integer2, -1);
			base.InitMeta();
			_pool = new ObjectPool<RequestMapChange>();
		}

		object IFactory.Create()
		{
			return _pool.Get();
		}

		void IFactory.Return(object objToReturn)
		{
			_pool.Return(objToReturn as RequestMapChange);
		}

		void IEventFactory.Dispatch(Event ev, object target)
		{
			if (target is IRequestMapChangeListener requestMapChangeListener)
			{
				requestMapChangeListener.OnEvent((RequestMapChange)ev);
			}
		}
	}
}
