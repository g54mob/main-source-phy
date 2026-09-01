using UnityEngine;

namespace Modding.Levels
{
	public class Entity
	{
		private Entity _simEntity;

		private Entity _buildEntity;

		private readonly int _hashCode;

		public EntityPrefabInfo Prefab { get; private set; }

		public EntityBehaviour Behaviour { get; private set; }

		public long Id
		{
			get
			{
				return InternalObject.identifier;
			}
		}

		public string Name
		{
			get
			{
				return InternalObject.LogicName();
			}
			set
			{
				InternalObject.EntityBehaviour.logicName.Value = value;
			}
		}

		public Entity SimEntity
		{
			get
			{
				if (_simEntity != null && _simEntity.InternalObject != null)
				{
					return _simEntity;
				}
				if (InternalObject.isSimulating)
				{
					return this;
				}
				return _simEntity = From(InternalObject.simEntity);
			}
		}

		public Entity BuildEntity
		{
			get
			{
				if (_buildEntity != null && _buildEntity.InternalObject != null)
				{
					return _buildEntity;
				}
				if (!InternalObject.isSimulating)
				{
					return this;
				}
				return _buildEntity = From(InternalObject.buildEntity);
			}
		}

		public bool PhysicsEnabled
		{
			get
			{
				return InternalObject.behaviour.PhysicsEnabled;
			}
		}

		public bool IsSelected
		{
			get
			{
				return InternalObject.IsSelected;
			}
		}

		public bool IsBuildZone
		{
			get
			{
				return InternalObject.isBuildZone;
			}
		}

		public bool IsBurning
		{
			get
			{
				return InternalObject.hasFireController && InternalObject.fireController.onFire;
			}
		}

		public bool IsDestroyed
		{
			get
			{
				return InternalObject.isDestroyed;
			}
		}

		public GameObject GameObject { get; private set; }

		public LevelEntity InternalObject { get; private set; }

		private Entity(LevelEntity entity)
		{
			Prefab = EntityPrefabInfo.From(entity.behaviour.prefab);
			GameObject = entity.gameObject;
			InternalObject = entity;
			_hashCode = entity.GetHashCode();
			Behaviour = EntityBehaviour.From(this);
		}

		public void Select(bool selected)
		{
			InternalObject.Select(selected);
		}

		public void SetOnFire(bool onFire)
		{
			FireTag fireTag = InternalObject.fireTag;
			if (!(fireTag == null))
			{
				if (onFire)
				{
					fireTag.Ignite();
				}
				else
				{
					fireTag.WaterHit();
				}
			}
		}

		public override string ToString()
		{
			return "Entity (" + Prefab.Name + ", " + Id + ")";
		}

		protected bool Equals(Entity other)
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
			return Equals((Entity)obj);
		}

		public override int GetHashCode()
		{
			return _hashCode;
		}

		public static Entity From(GameObject entityObject)
		{
			if (entityObject == null)
			{
				return null;
			}
			LevelEntity component = entityObject.GetComponent<LevelEntity>();
			return From(component);
		}

		public static Entity From(long id)
		{
			if (id == LevelPrefab.INVALID_ID)
			{
				return null;
			}
			LevelEntity entity;
			if (!LevelEditor.Instance.Get(id, out entity))
			{
				return null;
			}
			return From(entity);
		}

		public static Entity From(LevelEntity levelEntity)
		{
			if (levelEntity == null)
			{
				return null;
			}
			if (levelEntity.identifier == LevelPrefab.INVALID_ID)
			{
				return null;
			}
			return new Entity(levelEntity);
		}

		public static bool operator ==(Entity left, Entity right)
		{
			return object.Equals(left, right);
		}

		public static bool operator !=(Entity left, Entity right)
		{
			return !object.Equals(left, right);
		}
	}
}
