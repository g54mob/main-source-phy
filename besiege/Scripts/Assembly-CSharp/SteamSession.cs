using System;
using Steamworks;

[Serializable]
public class SteamSession : ConnectionSession
{
	public CSteamID SteamID;

	public SteamSession(CSteamID SteamID)
	{
		this.SteamID = SteamID;
	}

	public override string ToString()
	{
		return string.Format("SteamSession({0}) Name={1}, ConnectionID={2}, Ping={3}, LastReceivedTime={4}", SteamID, Username, ConnectionID, Ping);
	}
}
