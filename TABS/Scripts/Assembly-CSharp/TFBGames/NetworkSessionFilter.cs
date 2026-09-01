using System.Collections.Generic;

namespace TFBGames
{
	public class NetworkSessionFilter
	{
		public string GameVersion { get; }

		public bool CanPlayCrossNetworkSession { get; }

		public List<MultiplayerPlatform> AllowedPlatforms { get; }

		public NetworkSessionFilter(string gameVersion, bool canPlayCrossNetworkSession, List<MultiplayerPlatform> allowedPlatforms)
		{
			GameVersion = gameVersion;
			CanPlayCrossNetworkSession = canPlayCrossNetworkSession;
			AllowedPlatforms = allowedPlatforms;
		}
	}
}
