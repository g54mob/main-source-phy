using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using InternalModding.Events;

namespace Modding.Levels
{
	public class LogicEvent
	{
		internal static Dictionary<EventContainer.EventType, string> TypeNames;

		private readonly int _hashCode;

		public string Name
		{
			get
			{
				if (IsModdedEvent)
				{
					return ModdedContainer.Event.Name;
				}
				return TypeNames[InternalObject.eventType];
			}
		}

		public string Identifier
		{
			get
			{
				if (IsModdedEvent)
				{
					return ModdedContainer.Event.GlobalIdentifier;
				}
				return InternalObject.eventType.ToString();
			}
		}

		public LogicChain Chain { get; private set; }

		public ReadOnlyCollection<Entity> PickerEntities
		{
			get
			{
				return InternalObject.entityList.Select(Entity.From).ToList().AsReadOnly();
			}
		}

		public MPTeam Team
		{
			get
			{
				if (!IsModdedEvent)
				{
					return InternalObject.team;
				}
				IEnumerable<TeamButton> source = ModdedContainer.Properties.OfType<TeamButton>();
				TeamButton teamButton = source.FirstOrDefault();
				if (teamButton != null)
				{
					return teamButton.Team;
				}
				return MPTeam.None;
			}
		}

		public MPTeam[] Teams
		{
			get
			{
				if (!IsModdedEvent)
				{
					return new MPTeam[1] { InternalObject.team };
				}
				IEnumerable<TeamButton> source = ModdedContainer.Properties.OfType<TeamButton>();
				IEnumerable<MPTeam> source2 = source.Select((TeamButton team) => team.Team);
				return source2.ToArray();
			}
		}

		public IDictionary<string, EventProperty> EventProperties
		{
			get
			{
				if (!IsModdedEvent)
				{
					return new Dictionary<string, EventProperty>();
				}
				return ModdedContainer.Properties.Where((KeyValuePair<string, EventProperty> p) => !(p.Value is EventProperty.Text) && !(p.Value is EventProperty.Icon)).ToDictionary((KeyValuePair<string, EventProperty> p) => p.Key, (KeyValuePair<string, EventProperty> p) => p.Value);
			}
		}

		public EventContainer.EventType EventType
		{
			get
			{
				return InternalObject.eventType;
			}
		}

		public bool IsModdedEvent
		{
			get
			{
				return InternalObject.eventType == EventContainer.EventType.Modded;
			}
		}

		private ModdedEventContainer ModdedContainer
		{
			get
			{
				return InternalObject.eventData as ModdedEventContainer;
			}
		}

		public EntityEvent InternalObject { get; private set; }

		private LogicEvent(EntityEvent evt, LogicChain chain)
		{
			Chain = chain;
			InternalObject = evt;
			_hashCode = evt.GetHashCode();
		}

		public override string ToString()
		{
			return "LogicEvent (" + Name + ")";
		}

		protected bool Equals(LogicEvent other)
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
			return Equals((LogicEvent)obj);
		}

		public override int GetHashCode()
		{
			return _hashCode;
		}

		public static LogicEvent From(EntityEvent evt, LogicChain chain)
		{
			if (evt == null || chain == null)
			{
				return null;
			}
			return new LogicEvent(evt, chain);
		}

		public static bool operator ==(LogicEvent left, LogicEvent right)
		{
			return object.Equals(left, right);
		}

		public static bool operator !=(LogicEvent left, LogicEvent right)
		{
			return !object.Equals(left, right);
		}
	}
}
