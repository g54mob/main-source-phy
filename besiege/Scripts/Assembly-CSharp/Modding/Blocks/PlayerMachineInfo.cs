using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

namespace Modding.Blocks
{
	public class PlayerMachineInfo
	{
		private readonly int _hashCode;

		public string Name
		{
			get
			{
				return InternalObject.Name;
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

		public ReadOnlyCollection<BlockInfo> Blocks
		{
			get
			{
				return InternalObject.Blocks.Select(BlockInfo.From).ToList().AsReadOnly();
			}
		}

		public XDataHolder MachineData
		{
			get
			{
				return InternalObject.MachineData;
			}
		}

		public MachineInfo InternalObject { get; private set; }

		private PlayerMachineInfo(MachineInfo info)
		{
			InternalObject = info;
			_hashCode = info.GetHashCode();
		}

		public override string ToString()
		{
			return "MachineInfo (" + Name + ")";
		}

		protected bool Equals(PlayerMachineInfo other)
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
			return Equals((PlayerMachineInfo)obj);
		}

		public override int GetHashCode()
		{
			return _hashCode;
		}

		public static PlayerMachineInfo From(MachineInfo info)
		{
			return new PlayerMachineInfo(info);
		}

		public static bool operator ==(PlayerMachineInfo left, PlayerMachineInfo right)
		{
			return object.Equals(left, right);
		}

		public static bool operator !=(PlayerMachineInfo left, PlayerMachineInfo right)
		{
			return !object.Equals(left, right);
		}
	}
}
