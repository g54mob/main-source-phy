using System;

namespace Heathen.SteamworksIntegration
{
	[Obsolete("Please use MetadataTemplate")]
	public struct MetadataTempalate : IEquatable<MetadataTemplate>
	{
		public string Key;

		public string Value;

		public bool Equals(MetadataTemplate other)
		{
			return false;
		}

		public static implicit operator MetadataTempalate(MetadataTemplate other)
		{
			return default(MetadataTempalate);
		}

		public static implicit operator MetadataTemplate(MetadataTempalate other)
		{
			return default(MetadataTemplate);
		}
	}
}
