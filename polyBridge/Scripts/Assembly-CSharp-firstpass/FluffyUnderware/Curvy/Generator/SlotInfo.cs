using System;

namespace FluffyUnderware.Curvy.Generator
{
	public class SlotInfo : Attribute, IComparable
	{
		public enum SlotArrayType
		{
			Unknown = 0,
			Normal = 1,
			Hidden = 2
		}

		public readonly Type[] DataTypes;

		public string Name;

		private string displayName;

		public string Tooltip;

		public bool Array;

		public SlotArrayType ArrayType = SlotArrayType.Normal;

		public string DisplayName
		{
			get
			{
				return displayName ?? Name;
			}
			set
			{
				displayName = value;
			}
		}

		protected SlotInfo(string name, params Type[] type)
		{
			DataTypes = type;
			Name = name;
		}

		protected SlotInfo(params Type[] type)
			: this(null, type)
		{
		}

		public int CompareTo(object obj)
		{
			return string.Compare(((SlotInfo)obj).Name, Name, StringComparison.Ordinal);
		}
	}
}
