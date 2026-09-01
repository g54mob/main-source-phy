using System;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public class ServerPlayerEntry
	{
		public string name;

		public int score;

		public TimeSpan TimePlayed;
	}
}
