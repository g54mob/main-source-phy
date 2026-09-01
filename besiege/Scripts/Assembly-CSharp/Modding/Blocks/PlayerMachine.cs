using System;
using System.Collections.ObjectModel;
using System.Linq;
using InternalModding.Loading;
using Modding.Common;
using UnityEngine;

namespace Modding.Blocks
{
	public class PlayerMachine
	{
		private readonly int _hashCode;

		public string Name
		{
			get
			{
				return InternalObject.Name;
			}
		}

		public Player Player
		{
			get
			{
				return (!IsServerMachine) ? null : Player.From(InternalObjectServer.player);
			}
		}

		public Vector3 Position
		{
			get
			{
				return InternalObject.Position;
			}
		}

		public Quaternion Rotation
		{
			get
			{
				return InternalObject.Rotation;
			}
		}

		public Vector3 MiddlePosition
		{
			get
			{
				return InternalObject.MiddlePosition;
			}
		}

		public Vector3 Size
		{
			get
			{
				return InternalObject.Size;
			}
		}

		public Bounds Bounds
		{
			get
			{
				return InternalObject.GetBounds();
			}
		}

		public float Mass
		{
			get
			{
				return InternalObject.Mass;
			}
		}

		public float Health
		{
			get
			{
				return (!IsServerMachine) ? 0f : InternalObjectServer.Health;
			}
		}

		public Transform BuildingMachine
		{
			get
			{
				return InternalObject.BuildingMachine;
			}
		}

		public Transform SimulationMachine
		{
			get
			{
				return InternalObject.SimulationMachine;
			}
		}

		public int BlockCount
		{
			get
			{
				return InternalObject.BlockCount;
			}
		}

		public int ClusterCount
		{
			get
			{
				return InternalObject.ClusterCount;
			}
		}

		public ReadOnlyCollection<Block> BuildingBlocks
		{
			get
			{
				return InternalObject.BuildingBlocks.Select(Block.From).ToList().AsReadOnly();
			}
		}

		public ReadOnlyCollection<Block> SimulationBlocks
		{
			get
			{
				return InternalObject.SimulationBlocks.Select(Block.From).ToList().AsReadOnly();
			}
		}

		public bool Unbreakable
		{
			get
			{
				return InternalObject.UnbreakableMode;
			}
		}

		public bool InfiniteAmmo
		{
			get
			{
				return InternalObject.InfiniteAmmoMode;
			}
		}

		public bool ExplodingCannonballs
		{
			get
			{
				return InternalObject.ExplodingCannonballs;
			}
		}

		public bool GhostMode
		{
			get
			{
				return InternalObject.ghostMode;
			}
		}

		public bool CurtainMode
		{
			get
			{
				return InternalObject.curtainMode;
			}
		}

		public XDataHolder MachineData
		{
			get
			{
				return InternalObject.MachineData;
			}
		}

		public Machine InternalObject { get; private set; }

		private bool IsServerMachine
		{
			get
			{
				return InternalObject is ServerMachine;
			}
		}

		public ServerMachine InternalObjectServer
		{
			get
			{
				return InternalObject as ServerMachine;
			}
		}

		private PlayerMachine(Machine machine)
		{
			InternalObject = machine;
			_hashCode = machine.GetHashCode();
		}

		public Block AddBlock(int type, Vector3 position, Quaternion rotation, bool flipped = false)
		{
			if (GetLocal() != this)
			{
				return null;
			}
			Vector3 position2 = InternalObject.BuildingMachine.TransformPoint(position);
			Quaternion rotation2 = InternalObject.BuildingMachine.transform.rotation * rotation;
			BlockBehaviour block;
			InternalObject.AddBlockGlobal(position2, rotation2, (BlockType)type, flipped, out block);
			InternalObject.UndoSystem.AddBlock(global::BlockInfo.FromBlockBehaviour(block));
			return Block.From(block);
		}

		public Block AddBlock(BlockType type, Vector3 position, Quaternion rotation, bool flipped = false)
		{
			return AddBlock((int)type, position, rotation, flipped);
		}

		public Block AddBlock(Guid mod, int localId, Vector3 position, Quaternion rotation, bool flipped = false)
		{
			int effectiveBlockId = ModIds.GetEffectiveBlockId(mod, localId);
			if (effectiveBlockId == 0)
			{
				return null;
			}
			return AddBlock(effectiveBlockId, position, rotation, flipped);
		}

		public void RemoveBlock(Block block)
		{
			if (!(block == null) && !(GetLocal() != this))
			{
				global::BlockInfo info = global::BlockInfo.FromBlockBehaviour(block.InternalObject);
				InternalObject.RemoveBlock(block.InternalObject);
				InternalObject.UndoSystem.RemoveBlock(info);
				SingleInstanceFindOnly<AddPiece>.Instance.UpdateMiddleOfObject();
				if (ReferenceMaster.onMachineModified != null)
				{
					ReferenceMaster.onMachineModified(InternalObject);
				}
			}
		}

		public ReadOnlyCollection<Block> GetBlocksOfType(int type)
		{
			return GetBlocksOfType(type, InternalObject.isSimulating);
		}

		public ReadOnlyCollection<Block> GetBlocksOfType(Guid modId, int localId)
		{
			return GetBlocksOfType(modId, localId, InternalObject.isSimulating);
		}

		public ReadOnlyCollection<Block> GetBlocksOfType(Guid modId, int localId, bool simulation)
		{
			return GetBlocksOfType(ModIds.GetEffectiveBlockId(modId, localId), simulation);
		}

		public ReadOnlyCollection<Block> GetBlocksOfType(BlockType type)
		{
			return GetBlocksOfType(type, InternalObject.isSimulating);
		}

		public ReadOnlyCollection<Block> GetBlocksOfType(BlockType type, bool simulation)
		{
			return GetBlocksOfType((int)type, simulation);
		}

		public ReadOnlyCollection<Block> GetBlocksOfType(int type, bool simulation)
		{
			return ((!simulation) ? BuildingBlocks : SimulationBlocks).Where((Block b) => b.Prefab.Type == type).ToList().AsReadOnly();
		}

		public ReadOnlyCollection<T> GetBlockBehavioursOfType<T>(int type) where T : BlockBehaviour
		{
			return GetBlockBehavioursOfType<T>(type, InternalObject.isSimulating);
		}

		public ReadOnlyCollection<T> GetBlockBehavioursOfType<T>(int type, bool simulation) where T : BlockBehaviour
		{
			return (from b in GetBlocksOfType(type, simulation)
				select b.InternalObject).Cast<T>().ToList().AsReadOnly();
		}

		public ReadOnlyCollection<T> GetBlockBehavioursOfType<T>(Guid modId, int localId) where T : BlockBehaviour
		{
			return GetBlockBehavioursOfType<T>(modId, localId, InternalObject.isSimulating);
		}

		public ReadOnlyCollection<T> GetBlockBehavioursOfType<T>(Guid modId, int localId, bool simulation) where T : BlockBehaviour
		{
			return GetBlockBehavioursOfType<T>(ModIds.GetEffectiveBlockId(modId, localId), simulation);
		}

		public override string ToString()
		{
			return "Machine (" + Name + ")";
		}

		protected bool Equals(PlayerMachine other)
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
			return Equals((PlayerMachine)obj);
		}

		public override int GetHashCode()
		{
			return _hashCode;
		}

		public static PlayerMachine From(GameObject go)
		{
			if (go == null)
			{
				return null;
			}
			return From(go.GetComponent<Machine>());
		}

		public static PlayerMachine From(Machine machine)
		{
			if (machine == null)
			{
				return null;
			}
			return new PlayerMachine(machine);
		}

		public static PlayerMachine GetLocal()
		{
			if (!StatMaster.isMP)
			{
				return From(Machine.Active());
			}
			Player localPlayer = Player.GetLocalPlayer();
			if (localPlayer == null)
			{
				return null;
			}
			return localPlayer.Machine;
		}

		public static bool operator ==(PlayerMachine left, PlayerMachine right)
		{
			return object.Equals(left, right);
		}

		public static bool operator !=(PlayerMachine left, PlayerMachine right)
		{
			return !object.Equals(left, right);
		}
	}
}
