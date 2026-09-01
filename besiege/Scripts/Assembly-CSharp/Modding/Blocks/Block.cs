using System;
using System.Collections.Generic;
using System.Linq;
using InternalModding.Blocks;
using UnityEngine;

namespace Modding.Blocks
{
	public class Block
	{
		private Block _simBlock;

		private Block _buildingBlock;

		private readonly int _hashCode;

		public BlockPrefabInfo Prefab { get; private set; }

		public Guid Guid
		{
			get
			{
				return BuildingBlock.InternalObject.Guid;
			}
		}

		public Block SimBlock
		{
			get
			{
				if (_simBlock != null && _simBlock.GameObject != null)
				{
					return _simBlock;
				}
				if (InternalObject.isSimulating)
				{
					return this;
				}
				return _simBlock = From(InternalObject.SimBlock);
			}
		}

		public Block BuildingBlock
		{
			get
			{
				if (_buildingBlock != null && _buildingBlock.GameObject != null)
				{
					return _buildingBlock;
				}
				if (!InternalObject.isSimulating)
				{
					return this;
				}
				return _buildingBlock = From(InternalObject.BuildingBlock);
			}
		}

		public MPTeam Team
		{
			get
			{
				return InternalObject.Team;
			}
		}

		public float Health
		{
			get
			{
				return (!InternalObject.Prefab.hasHealthBar) ? 0f : InternalObject.BlockHealth.health;
			}
		}

		public float MaxHealth
		{
			get
			{
				BlockHealthBar component = Prefab.GameObject.GetComponent<BlockHealthBar>();
				return (!(component != null)) ? 0f : component.health;
			}
		}

		public PlayerMachine Machine
		{
			get
			{
				return PlayerMachine.From(InternalObject.ParentMachine);
			}
		}

		public bool IsArmor
		{
			get
			{
				return InternalObject.Prefab.isArmor;
			}
		}

		public bool InWind
		{
			get
			{
				return InternalObject.InWind;
			}
		}

		public bool IsBurning
		{
			get
			{
				return InternalObject.fireTag != null && InternalObject.fireTag.burning;
			}
		}

		public BlockScript BlockScript
		{
			get
			{
				ModBlockBehaviourHandler modBlockBehaviourHandler = InternalObject as ModBlockBehaviourHandler;
				if ((bool)modBlockBehaviourHandler)
				{
					return modBlockBehaviourHandler.blockScript;
				}
				return null;
			}
		}

		public GameObject GameObject { get; private set; }

		public BlockBehaviour InternalObject { get; private set; }

		private Block(BlockBehaviour behaviour)
		{
			Prefab = BlockPrefabInfo.From(PrefabMaster.BlockPrefabs[behaviour.BlockID]);
			GameObject = behaviour.gameObject;
			InternalObject = behaviour;
			_hashCode = behaviour.GetHashCode();
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
			return string.Concat("Block (", Prefab.Name, ", ", Guid, ")");
		}

		private bool Equals(Block other)
		{
			return InternalObject == other.InternalObject;
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
			return Equals((Block)obj);
		}

		public override int GetHashCode()
		{
			return _hashCode;
		}

		public static Block From(GameObject obj)
		{
			return From(obj.GetComponent<BlockBehaviour>());
		}

		public static Block From(Guid guid)
		{
			IEnumerable<ServerMachine> enumerable = Playerlist.Players.Select((PlayerData p) => p.machine);
			foreach (ServerMachine item in enumerable)
			{
				if (item == null)
				{
					continue;
				}
				foreach (BlockBehaviour buildingBlock in item.BuildingBlocks)
				{
					if (buildingBlock.Guid == guid)
					{
						return From(buildingBlock);
					}
				}
			}
			return null;
		}

		public static Block From(BlockBehaviour behaviour)
		{
			if (behaviour == null)
			{
				return null;
			}
			return new Block(behaviour);
		}

		public static Block From(BlockScript script)
		{
			if (script == null)
			{
				return null;
			}
			return From(script.handler);
		}

		public static bool operator ==(Block left, Block right)
		{
			return object.Equals(left, right);
		}

		public static bool operator !=(Block left, Block right)
		{
			return !object.Equals(left, right);
		}
	}
}
