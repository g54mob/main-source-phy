using System;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public struct NearFilter : IEquatable<NearFilter>, IComparable<NearFilter>
	{
		public string key;

		public int value;

		public readonly int CompareTo(NearFilter other)
		{
			return 0;
		}

		public readonly bool Equals(NearFilter other)
		{
			return false;
		}

		public override readonly bool Equals(object obj)
		{
			return false;
		}

		public override readonly int GetHashCode()
		{
			return 0;
		}

		public static bool operator ==(NearFilter l, NearFilter r)
		{
			return false;
		}

		public static bool operator !=(NearFilter l, NearFilter r)
		{
			return false;
		}
	}
}
