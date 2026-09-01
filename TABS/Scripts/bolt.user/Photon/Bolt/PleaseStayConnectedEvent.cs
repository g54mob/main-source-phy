using System;
using Photon.Bolt.Exceptions;
using Photon.Bolt.Internal;

namespace Photon.Bolt
{
	public class PleaseStayConnectedEvent : Event
	{
		public PleaseStayConnectedEvent()
			: base(PleaseStayConnectedEvent_Meta.Instance)
		{
		}

		public override string ToString()
		{
			return $"[PleaseStayConnectedEvent]";
		}

		public static PleaseStayConnectedEvent Create(BoltEntity entity, EntityTargets targets)
		{
			if (!entity)
			{
				throw new ArgumentNullException("entity");
			}
			if (!entity.IsAttached)
			{
				throw new BoltException("You can not raise events on entities which are not attached");
			}
			if (!(Factory.NewEvent(((IFactory)PleaseStayConnectedEvent_Meta.Instance).TypeKey) is PleaseStayConnectedEvent pleaseStayConnectedEvent))
			{
				return null;
			}
			pleaseStayConnectedEvent.Targets = (int)targets;
			pleaseStayConnectedEvent.TargetEntity = entity.Entity;
			pleaseStayConnectedEvent.Reliability = ReliabilityModes.Unreliable;
			return pleaseStayConnectedEvent;
		}

		public static PleaseStayConnectedEvent Create(BoltEntity entity)
		{
			return Create(entity, EntityTargets.Everyone);
		}

		private static PleaseStayConnectedEvent Create(byte targets, BoltConnection connection, ReliabilityModes reliability)
		{
			if (!(Factory.NewEvent(((IFactory)PleaseStayConnectedEvent_Meta.Instance).TypeKey) is PleaseStayConnectedEvent pleaseStayConnectedEvent))
			{
				return null;
			}
			pleaseStayConnectedEvent.Targets = targets;
			pleaseStayConnectedEvent.TargetConnection = connection;
			pleaseStayConnectedEvent.Reliability = reliability;
			return pleaseStayConnectedEvent;
		}

		public static PleaseStayConnectedEvent Create(GlobalTargets targets)
		{
			return Create((byte)targets, null, ReliabilityModes.ReliableOrdered);
		}

		public static PleaseStayConnectedEvent Create(GlobalTargets targets, ReliabilityModes reliability)
		{
			return Create((byte)targets, null, reliability);
		}

		public static PleaseStayConnectedEvent Create(BoltConnection connection)
		{
			return Create(10, connection, ReliabilityModes.ReliableOrdered);
		}

		public static PleaseStayConnectedEvent Create(BoltConnection connection, ReliabilityModes reliability)
		{
			return Create(10, connection, reliability);
		}

		public static PleaseStayConnectedEvent Create()
		{
			return Create(2, null, ReliabilityModes.ReliableOrdered);
		}

		public static PleaseStayConnectedEvent Create(ReliabilityModes reliability)
		{
			return Create(2, null, reliability);
		}

		public static bool Post(BoltEntity entity, EntityTargets targets)
		{
			PleaseStayConnectedEvent pleaseStayConnectedEvent = Create(entity, targets);
			if (pleaseStayConnectedEvent == null)
			{
				return false;
			}
			pleaseStayConnectedEvent.Send();
			return true;
		}

		public static bool Post(BoltEntity entity)
		{
			return Post(entity, EntityTargets.Everyone);
		}

		private static bool Post(byte targets, BoltConnection connection, ReliabilityModes reliability)
		{
			PleaseStayConnectedEvent pleaseStayConnectedEvent = Create(targets, connection, reliability);
			if (pleaseStayConnectedEvent == null)
			{
				return false;
			}
			pleaseStayConnectedEvent.Send();
			return true;
		}

		public static bool Post(GlobalTargets targets)
		{
			return Post((byte)targets, null, ReliabilityModes.ReliableOrdered);
		}

		public static bool Post(GlobalTargets targets, ReliabilityModes reliability)
		{
			return Post((byte)targets, null, reliability);
		}

		public static bool Post(BoltConnection connection)
		{
			return Post(10, connection, ReliabilityModes.ReliableOrdered);
		}

		public static bool Post(BoltConnection connection, ReliabilityModes reliability)
		{
			return Post(10, connection, reliability);
		}

		public static bool Post()
		{
			return Post(2, null, ReliabilityModes.ReliableOrdered);
		}

		public static bool Post(ReliabilityModes reliability)
		{
			return Post(2, null, reliability);
		}
	}
}
