using System;
using UnityEngine;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public struct MetadataTemplate : IEquatable<MetadataTemplate>
	{
		[Tooltip("The key or field name to be used. names will not be duplicated, if you add another field of the same name it will overwrite, not duplicate")]
		public string key;

		[Tooltip("The value of the field to be applied, empty values are ignored")]
		public string value;

		public readonly bool Equals(MetadataTemplate other)
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

		public override readonly string ToString()
		{
			return null;
		}

		public static bool operator ==(MetadataTemplate l, MetadataTemplate r)
		{
			return false;
		}

		public static bool operator !=(MetadataTemplate l, MetadataTemplate r)
		{
			return false;
		}
	}
}
