using System;
using Photon.Bolt.Internal;

namespace Photon.Bolt
{
	internal class DebugEvent_Meta : Event_Meta, IEventFactory, IFactory
	{
		internal static DebugEvent_Meta Instance;

		internal ObjectPool<DebugEvent> _pool;

		TypeId IFactory.TypeId => TypeId;

		UniqueId IFactory.TypeKey => new UniqueId(18, 118, 46, 200, 118, 5, 107, 78, 158, 218, 93, 80, 110, 31, 80, 157);

		Type IFactory.TypeObject => typeof(DebugEvent);

		static DebugEvent_Meta()
		{
			Instance = new DebugEvent_Meta();
			Instance.InitMeta();
		}

		internal override void InitObject(NetworkObj obj, Offsets offsets)
		{
		}

		internal override void InitMeta()
		{
			TypeId = new TypeId(30);
			CountStorage = 2;
			CountObjects = 1;
			CountProperties = 2;
			Properties = new NetworkPropertyInfo[2];
			NetworkProperty_Integer networkProperty_Integer = new NetworkProperty_Integer();
			networkProperty_Integer.PropertyMeta = this;
			networkProperty_Integer.Settings_Property("DebugEventType", 1, -1073741824);
			networkProperty_Integer.Settings_Offsets(0, 0);
			networkProperty_Integer.Settings_Integer(PropertyIntCompressionSettings.Create(8, 0));
			AddProperty(0, 0, networkProperty_Integer, -1);
			NetworkProperty_ProtocolToken networkProperty_ProtocolToken = new NetworkProperty_ProtocolToken();
			networkProperty_ProtocolToken.PropertyMeta = this;
			networkProperty_ProtocolToken.Settings_Property("DebugToken", 1, -1073741824);
			networkProperty_ProtocolToken.Settings_Offsets(1, 1);
			AddProperty(1, 0, networkProperty_ProtocolToken, -1);
			base.InitMeta();
			_pool = new ObjectPool<DebugEvent>();
		}

		object IFactory.Create()
		{
			return _pool.Get();
		}

		void IFactory.Return(object objToReturn)
		{
			_pool.Return(objToReturn as DebugEvent);
		}

		void IEventFactory.Dispatch(Event ev, object target)
		{
			if (target is IDebugEventListener debugEventListener)
			{
				debugEventListener.OnEvent((DebugEvent)ev);
			}
		}
	}
}
