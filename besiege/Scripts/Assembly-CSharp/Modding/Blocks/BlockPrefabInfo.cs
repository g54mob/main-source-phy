using System;
using System.Linq;
using InternalModding.Loading;
using UnityEngine;

namespace Modding.Blocks
{
	public class BlockPrefabInfo
	{
		private readonly int _hashCode;

		public string Name
		{
			get
			{
				return InternalObject.name;
			}
		}

		public int Type
		{
			get
			{
				return InternalObject.ID;
			}
		}

		public GameObject GameObject
		{
			get
			{
				return InternalObject.gameObject;
			}
		}

		public GameObject GhostObject
		{
			get
			{
				return InternalObject.ghost;
			}
		}

		public BlockPrefab InternalObject { get; private set; }

		private BlockPrefabInfo(BlockPrefab prefab)
		{
			InternalObject = prefab;
			_hashCode = prefab.GetHashCode();
		}

		public override string ToString()
		{
			return "BlockPrefab (" + Name + ")";
		}

		protected bool Equals(BlockPrefabInfo other)
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
			return Equals((BlockPrefabInfo)obj);
		}

		public override int GetHashCode()
		{
			return _hashCode;
		}

		public static BlockPrefabInfo GetOfficial(BlockType type)
		{
			return FromId((int)type);
		}

		public static BlockPrefabInfo FromId(int id)
		{
			if (!PrefabMaster.BlockPrefabs.ContainsKey(id))
			{
				return null;
			}
			return From(PrefabMaster.BlockPrefabs[id]);
		}

		public static BlockPrefabInfo FromIdModded(Guid modId, int localId)
		{
			return FromId(ModIds.GetEffectiveBlockId(modId, localId));
		}

		public static BlockPrefabInfo From(BlockPrefab prefab)
		{
			if (prefab == null)
			{
				return null;
			}
			return new BlockPrefabInfo(prefab);
		}

		public static BlockPrefabInfo[] GetAll()
		{
			return PrefabMaster.BlockPrefabs.Values.Select((BlockPrefab p) => From(p)).ToArray();
		}

		public static bool operator ==(BlockPrefabInfo left, BlockPrefabInfo right)
		{
			return object.Equals(left, right);
		}

		public static bool operator !=(BlockPrefabInfo left, BlockPrefabInfo right)
		{
			return !object.Equals(left, right);
		}
	}
}
