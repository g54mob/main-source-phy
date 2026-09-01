using Photon.Bolt.Internal;
using UnityEngine;

namespace Photon.Bolt
{
	public class DebugEvent : Event
	{
		public int DebugEventType
		{
			get
			{
				return Storage.Values[OffsetStorage].Int0;
			}
			set
			{
				value = Mathf.Clamp(value, 0, 255);
				int @int = Storage.Values[OffsetStorage].Int0;
				Storage.Values[OffsetStorage].Int0 = value;
				if (!NetworkValue.Diff(@int, value))
				{
				}
			}
		}

		public IProtocolToken DebugToken
		{
			get
			{
				return Storage.Values[OffsetStorage + 1].ProtocolToken;
			}
			set
			{
				IProtocolToken protocolToken = Storage.Values[OffsetStorage + 1].ProtocolToken;
				protocolToken.Release();
				Storage.Values[OffsetStorage + 1].ProtocolToken = value;
				if (!NetworkValue.Diff(protocolToken, value))
				{
				}
			}
		}

		public DebugEvent()
			: base(DebugEvent_Meta.Instance)
		{
		}

		public override string ToString()
		{
			return $"[DebugEvent DebugEventType={DebugEventType} DebugToken={DebugToken}]";
		}

		protected override void PrepareRelease()
		{
			Storage.Values[OffsetStorage + 1].ProtocolToken.Release();
		}

		private static DebugEvent Create(byte targets, BoltConnection connection, ReliabilityModes reliability)
		{
			if (!(Factory.NewEvent(((IFactory)DebugEvent_Meta.Instance).TypeKey) is DebugEvent debugEvent))
			{
				return null;
			}
			debugEvent.Targets = targets;
			debugEvent.TargetConnection = connection;
			debugEvent.Reliability = reliability;
			return debugEvent;
		}

		public static DebugEvent Create(GlobalTargets targets)
		{
			return Create((byte)targets, null, ReliabilityModes.ReliableOrdered);
		}

		public static DebugEvent Create(GlobalTargets targets, ReliabilityModes reliability)
		{
			return Create((byte)targets, null, reliability);
		}

		public static DebugEvent Create(BoltConnection connection)
		{
			return Create(10, connection, ReliabilityModes.ReliableOrdered);
		}

		public static DebugEvent Create(BoltConnection connection, ReliabilityModes reliability)
		{
			return Create(10, connection, reliability);
		}

		public static DebugEvent Create()
		{
			return Create(2, null, ReliabilityModes.ReliableOrdered);
		}

		public static DebugEvent Create(ReliabilityModes reliability)
		{
			return Create(2, null, reliability);
		}

		private static bool Post(byte targets, BoltConnection connection, ReliabilityModes reliability, int DebugEventType, IProtocolToken DebugToken)
		{
			DebugEvent debugEvent = Create(targets, connection, reliability);
			if (debugEvent == null)
			{
				return false;
			}
			debugEvent.DebugEventType = DebugEventType;
			debugEvent.DebugToken = DebugToken;
			debugEvent.Send();
			return true;
		}

		public static bool Post(GlobalTargets targets, int DebugEventType, IProtocolToken DebugToken)
		{
			return Post((byte)targets, null, ReliabilityModes.ReliableOrdered, DebugEventType, DebugToken);
		}

		public static bool Post(GlobalTargets targets, ReliabilityModes reliability, int DebugEventType, IProtocolToken DebugToken)
		{
			return Post((byte)targets, null, reliability, DebugEventType, DebugToken);
		}

		public static bool Post(BoltConnection connection, int DebugEventType, IProtocolToken DebugToken)
		{
			return Post(10, connection, ReliabilityModes.ReliableOrdered, DebugEventType, DebugToken);
		}

		public static bool Post(BoltConnection connection, ReliabilityModes reliability, int DebugEventType, IProtocolToken DebugToken)
		{
			return Post(10, connection, reliability, DebugEventType, DebugToken);
		}

		public static bool Post(int DebugEventType, IProtocolToken DebugToken)
		{
			return Post(2, null, ReliabilityModes.ReliableOrdered, DebugEventType, DebugToken);
		}

		public static bool Post(ReliabilityModes reliability, int DebugEventType, IProtocolToken DebugToken)
		{
			return Post(2, null, reliability, DebugEventType, DebugToken);
		}
	}
}
