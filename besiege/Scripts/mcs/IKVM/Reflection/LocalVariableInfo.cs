namespace IKVM.Reflection
{
	public class LocalVariableInfo
	{
		private readonly int index;

		private readonly Type type;

		private readonly bool pinned;

		private readonly CustomModifiers customModifiers;

		public bool IsPinned
		{
			get
			{
				return pinned;
			}
		}

		public int LocalIndex
		{
			get
			{
				return index;
			}
		}

		public Type LocalType
		{
			get
			{
				return type;
			}
		}

		internal LocalVariableInfo(int index, Type type, bool pinned)
		{
			this.index = index;
			this.type = type;
			this.pinned = pinned;
		}

		internal LocalVariableInfo(int index, Type type, bool pinned, CustomModifiers customModifiers)
			: this(index, type, pinned)
		{
			this.customModifiers = customModifiers;
		}

		public CustomModifiers __GetCustomModifiers()
		{
			return customModifiers;
		}

		public override string ToString()
		{
			return string.Format(pinned ? "{0} ({1}) (pinned)" : "{0} ({1})", type, index);
		}
	}
}
