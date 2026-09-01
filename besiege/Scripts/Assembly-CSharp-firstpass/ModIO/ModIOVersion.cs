using System;

namespace ModIO
{
	[Serializable]
	public struct ModIOVersion : IComparable<ModIOVersion>
	{
		public static readonly ModIOVersion Current = new ModIOVersion(2, 3, 4);

		public int major;

		public int minor;

		public int patch;

		public ModIOVersion(int majorVersion = 0, int minorVersion = 0, int patchVersion = 0)
		{
			major = majorVersion;
			minor = minorVersion;
			patch = patchVersion;
		}

		public int CompareTo(ModIOVersion other)
		{
			int num = major.CompareTo(other.major);
			if (num == 0)
			{
				num = minor.CompareTo(other.minor);
				if (num == 0)
				{
					num = patch.CompareTo(other.patch);
				}
			}
			return num;
		}

		public override string ToString()
		{
			return ToString("X.Y.Z");
		}

		public string ToString(string format)
		{
			format = format.ToUpper();
			return format.Replace("X", major.ToString()).Replace("Y", minor.ToString()).Replace("Z", patch.ToString());
		}

		public static bool operator >(ModIOVersion a, ModIOVersion b)
		{
			return a.CompareTo(b) == 1;
		}

		public static bool operator <(ModIOVersion a, ModIOVersion b)
		{
			return a.CompareTo(b) == -1;
		}

		public static bool operator >=(ModIOVersion a, ModIOVersion b)
		{
			return a.CompareTo(b) >= 0;
		}

		public static bool operator <=(ModIOVersion a, ModIOVersion b)
		{
			return a.CompareTo(b) <= 0;
		}
	}
}
