using System;
using UnityEngine;

namespace Modding.Blocks
{
	public class BlockInfo
	{
		private readonly int _hashCode;

		public int Type
		{
			get
			{
				return (int)InternalObject.ID;
			}
		}

		public Guid Guid
		{
			get
			{
				return InternalObject.Guid;
			}
		}

		public Vector3 Position
		{
			get
			{
				return InternalObject.Position;
			}
			set
			{
				InternalObject.Position = value;
			}
		}

		public Quaternion Rotation
		{
			get
			{
				return InternalObject.Rotation;
			}
			set
			{
				InternalObject.Rotation = value;
			}
		}

		public Vector3 Scale
		{
			get
			{
				return InternalObject.Scale;
			}
			set
			{
				InternalObject.Scale = value;
			}
		}

		public XDataHolder Data
		{
			get
			{
				return InternalObject.BlockData;
			}
		}

		public global::BlockInfo InternalObject { get; private set; }

		private BlockInfo(global::BlockInfo info)
		{
			InternalObject = info;
			_hashCode = info.GetHashCode();
		}

		public override string ToString()
		{
			return string.Concat("BlockInfo (", Guid, ")");
		}

		private bool Equals(BlockInfo other)
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
			return Equals((BlockInfo)obj);
		}

		public override int GetHashCode()
		{
			return _hashCode;
		}

		public static BlockInfo From(Block block)
		{
			return From(global::BlockInfo.FromBlockBehaviour(block.InternalObject));
		}

		public static BlockInfo From(global::BlockInfo info)
		{
			return new BlockInfo(info);
		}

		public static bool operator ==(BlockInfo left, BlockInfo right)
		{
			return object.Equals(left, right);
		}

		public static bool operator !=(BlockInfo left, BlockInfo right)
		{
			return !object.Equals(left, right);
		}
	}
}
