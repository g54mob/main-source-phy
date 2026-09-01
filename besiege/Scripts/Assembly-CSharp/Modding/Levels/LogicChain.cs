using System.Collections.ObjectModel;
using System.Linq;

namespace Modding.Levels
{
	public class LogicChain
	{
		private readonly int _hashCode;

		public Entity Entity { get; private set; }

		public ReadOnlyCollection<LogicEvent> Events
		{
			get
			{
				return InternalObject.events.Select((EntityEvent e) => LogicEvent.From(e, this)).ToList().AsReadOnly();
			}
		}

		public string TriggerName
		{
			get
			{
				if (IsModdedTrigger)
				{
					return InternalObject.moddedTriggerType.Name;
				}
				return ReferenceMaster.TranslateTriggerType(InternalObject.triggerType);
			}
		}

		public string TriggerIdentifier
		{
			get
			{
				if (IsModdedTrigger)
				{
					return InternalObject.moddedTriggerType.GlobalIdentifier;
				}
				return InternalObject.triggerType.ToString();
			}
		}

		public bool IsModdedTrigger
		{
			get
			{
				return InternalObject.triggerType == TriggerType.Modded;
			}
		}

		public EntityLogic InternalObject { get; private set; }

		private LogicChain(Entity entity, EntityLogic logic)
		{
			Entity = entity;
			InternalObject = logic;
			_hashCode = logic.GetHashCode();
		}

		public override string ToString()
		{
			return "LogicChain (" + TriggerName + ")";
		}

		protected bool Equals(LogicChain other)
		{
			return object.Equals(InternalObject, other.InternalObject);
		}

		public override bool Equals(object obj)
		{
			if (object.ReferenceEquals(null, obj))
			{
				return false;
			}
			if (object.ReferenceEquals(this, obj))
			{
				return true;
			}
			if (obj.GetType() != GetType())
			{
				return false;
			}
			return Equals((LogicChain)obj);
		}

		public override int GetHashCode()
		{
			return _hashCode;
		}

		public static LogicChain From(EntityLogic logic)
		{
			return From(Entity.From(logic.entityBehaviour.entity), logic);
		}

		internal static LogicChain From(Entity entity, EntityLogic logic)
		{
			if (entity == null || logic == null)
			{
				return null;
			}
			return new LogicChain(entity, logic);
		}

		public static bool operator ==(LogicChain left, LogicChain right)
		{
			return object.Equals(left, right);
		}

		public static bool operator !=(LogicChain left, LogicChain right)
		{
			return !object.Equals(left, right);
		}
	}
}
