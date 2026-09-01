using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using InternalModding.Triggers;

namespace Modding.Levels
{
	public class EntityBehaviour
	{
		private readonly int _hashCode;

		public Entity Entity { get; private set; }

		public bool HasLogic
		{
			get
			{
				return InternalObject.hasLogic;
			}
		}

		public bool HasRunningLogic
		{
			get
			{
				return InternalObject.hasRunningLogic;
			}
		}

		public ReadOnlyCollection<LogicChain> LogicChains
		{
			get
			{
				return InternalObject.logicData.Select(LogicChain.From).ToList().AsReadOnly();
			}
		}

		public ReadOnlyCollection<LogicChain> RunningLogicChains
		{
			get
			{
				return InternalObject.runningLogic.Select(LogicChain.From).ToList().AsReadOnly();
			}
		}

		public Dictionary<string, float> Variables
		{
			get
			{
				return new Dictionary<string, float>(InternalObject.variables);
			}
		}

		public GenericEntity InternalObject { get; private set; }

		private EntityBehaviour(Entity entity)
		{
			Entity = entity;
			InternalObject = entity.InternalObject.behaviour;
			_hashCode = InternalObject.GetHashCode();
		}

		public void TriggerLogicChains(string triggerId)
		{
			if (!triggerId.Contains("-"))
			{
				TriggerType triggerType = EntityLogic.GetTriggerType(triggerId);
				InternalObject.ProcessEvent(triggerType);
				return;
			}
			string[] array = triggerId.Split('-');
			string modId = array[0];
			int id = int.Parse(array[1]);
			ModdedTrigger triggerById = SingleInstanceFindOnly<TriggerLoader>.Instance.GetTriggerById(modId, id);
			InternalObject.ProcessModdedEvent(triggerById);
		}

		public void TriggerLogicChains(LogicChain chain)
		{
			TriggerLogicChains(chain.TriggerIdentifier);
		}

		public void Activate()
		{
			InternalObject.ActivateEntity();
		}

		public void Deactivate()
		{
			InternalObject.DeactivateEntity();
		}

		public void SetVariable(string name, float value)
		{
			InternalObject.SetVariable(name, EventContainer.VarModifyType.Set, value);
		}

		public override string ToString()
		{
			return "EntityBehaviour (" + Entity.Prefab.Name + ", " + Entity.Id + ")";
		}

		protected bool Equals(EntityBehaviour other)
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
			return Equals((EntityBehaviour)obj);
		}

		public override int GetHashCode()
		{
			return _hashCode;
		}

		internal static EntityBehaviour From(Entity entity)
		{
			if (entity.InternalObject.behaviour == null)
			{
				return null;
			}
			return new EntityBehaviour(entity);
		}

		public static bool operator ==(EntityBehaviour left, EntityBehaviour right)
		{
			return object.Equals(left, right);
		}

		public static bool operator !=(EntityBehaviour left, EntityBehaviour right)
		{
			return !object.Equals(left, right);
		}
	}
}
