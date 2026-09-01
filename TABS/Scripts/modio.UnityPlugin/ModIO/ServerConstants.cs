namespace ModIO
{
	public static class ServerConstants
	{
		public static string ConvertUserPortalToHeaderValue(UserPortal portal)
		{
			string text = null;
			switch (portal)
			{
			case UserPortal.Apple:
				return "apple";
			case UserPortal.Discord:
				return "discord";
			case UserPortal.EpicGamesStore:
				return "egs";
			case UserPortal.GOG:
				return "gog";
			case UserPortal.Google:
				return "google";
			case UserPortal.itchio:
				return "itchio";
			case UserPortal.Nintendo:
				return "nintendo";
			case UserPortal.Oculus:
				return "oculus";
			case UserPortal.PlayStationNetwork:
				return "psn";
			case UserPortal.Steam:
				return "steam";
			case UserPortal.XboxLive:
				return "xboxlive";
			default:
				return "none";
			}
		}
	}
}
