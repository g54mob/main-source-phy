using System;
using Steamworks;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public struct NumericFilter : IEquatable<NumericFilter>, IComparable<NumericFilter>
	{
		public string key;

		public int value;

		public ELobbyComparison comparison;

		public readonly int CompareTo(NumericFilter other)
		{
			return 0;
		}

		public readonly bool Equals(NumericFilter other)
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

		public static bool operator ==(NumericFilter l, NumericFilter r)
		{
			return false;
		}

		public static bool operator !=(NumericFilter l, NumericFilter r)
		{
			return false;
		}
	}
}
