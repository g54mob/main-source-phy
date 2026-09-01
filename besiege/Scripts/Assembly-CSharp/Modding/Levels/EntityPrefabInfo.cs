using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using InternalModding.Loading;
using Ordered;
using UnityEngine;

namespace Modding.Levels
{
	public class EntityPrefabInfo
	{
		private readonly int _hashCode;

		public string Name
		{
			get
			{
				return InternalObject.name;
			}
		}

		public int Id
		{
			get
			{
				return InternalObject.ID;
			}
		}

		public StatMaster.Category Category
		{
			get
			{
				return InternalObject.category;
			}
		}

		public Texture2D Icon
		{
			get
			{
				return InternalObject.icon;
			}
		}

		public ReadOnlyCollection<string> Keywords { get; private set; }

		public bool Inflammable
		{
			get
			{
				return InternalObject.inflammable;
			}
		}

		public bool Destructable
		{
			get
			{
				return InternalObject.destructable;
			}
		}

		public bool CanDoDamage
		{
			get
			{
				return InternalObject.damager;
			}
		}

		public bool CanScale
		{
			get
			{
				return InternalObject.canScale;
			}
		}

		public bool UniformScale
		{
			get
			{
				return InternalObject.uniformScale;
			}
		}

		public bool CanPick
		{
			get
			{
				return InternalObject.canPick;
			}
		}

		public ReadOnlyCollection<string> AvailableTriggers
		{
			get
			{
				return InternalObject.events.Select((TriggerType t) => t.ToString()).Union(InternalObject.moddedEvents.Select((int id) => ModIds.GetTriggerByEffectiveId(id).GlobalIdentifier)).ToList()
					.AsReadOnly();
			}
		}

		public GameObject GameObject { get; private set; }

		public LevelPrefab InternalObject { get; private set; }

		private EntityPrefabInfo(LevelPrefab prefab)
		{
			Keywords = prefab.keywords.ToList().AsReadOnly();
			GameObject = prefab.gameObject;
			InternalObject = prefab;
			_hashCode = InternalObject.GetHashCode();
		}

		public override string ToString()
		{
			return "EntityPrefab (" + Name + ")";
		}

		protected bool Equals(EntityPrefabInfo other)
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
			return Equals((EntityPrefabInfo)obj);
		}

		public override int GetHashCode()
		{
			return _hashCode;
		}

		public static EntityPrefabInfo FromId(int id)
		{
			foreach (LevelPrefab value in PrefabMaster.LevelPrefabs[10].Values)
			{
				if (value.ID == id)
				{
					return From(value);
				}
			}
			return null;
		}

		public static EntityPrefabInfo FromIdModded(Guid modId, int localId)
		{
			return FromId(ModIds.GetEffectiveEntityId(modId, localId));
		}

		public static EntityPrefabInfo From(LevelPrefab levelPrefab)
		{
			if (levelPrefab == null)
			{
				return null;
			}
			return new EntityPrefabInfo(levelPrefab);
		}

		public static EntityPrefabInfo[] GetAll()
		{
			return (from p in PrefabMaster.LevelPrefabs.SelectMany((KeyValuePair<int, Ordered.Dictionary<int, LevelPrefab>> pair) => pair.Value.Values).Distinct()
				select From(p)).ToArray();
		}

		public static bool operator ==(EntityPrefabInfo left, EntityPrefabInfo right)
		{
			return object.Equals(left, right);
		}

		public static bool operator !=(EntityPrefabInfo left, EntityPrefabInfo right)
		{
			return !object.Equals(left, right);
		}
	}
}
