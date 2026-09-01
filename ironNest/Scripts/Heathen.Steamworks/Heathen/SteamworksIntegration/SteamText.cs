using System;
using UnityEngine.Serialization;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public struct SteamText
	{
		[FormerlySerializedAs("default")]
		[FormerlySerializedAs("Default")]
		public string defaultText;

		public SteamText(string value)
		{
			defaultText = null;
		}

		public readonly string Get()
		{
			return null;
		}

		public override string ToString()
		{
			return null;
		}

		public static implicit operator string(SteamText l)
		{
			return null;
		}
	}
}
