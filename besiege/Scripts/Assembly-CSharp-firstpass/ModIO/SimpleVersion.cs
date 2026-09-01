using System;

namespace ModIO
{
	[Serializable]
	[Obsolete("Use ModIOVersion instead")]
	public struct SimpleVersion : IComparable<SimpleVersion>
	{
		public int major;

		public int minor;

		public SimpleVersion(int majorVersion = 0, int minorVersion = 0)
		{
			major = majorVersion;
			minor = minorVersion;
		}

		public int CompareTo(SimpleVersion other)
		{
			if (major != other.major)
			{
				return major.CompareTo(other.major);
			}
			return minor.CompareTo(other.minor);
		}

		public static bool operator >(SimpleVersion operand1, SimpleVersion operand2)
		{
			return operand1.CompareTo(operand2) == 1;
		}

		public static bool operator <(SimpleVersion operand1, SimpleVersion operand2)
		{
			return operand1.CompareTo(operand2) == -1;
		}

		public static bool operator >=(SimpleVersion operand1, SimpleVersion operand2)
		{
			return operand1.CompareTo(operand2) >= 0;
		}

		public static bool operator <=(SimpleVersion operand1, SimpleVersion operand2)
		{
			return operand1.CompareTo(operand2) <= 0;
		}
	}
}
