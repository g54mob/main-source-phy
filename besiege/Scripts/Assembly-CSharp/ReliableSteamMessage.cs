using Steamworks;

public class ReliableSteamMessage : ReliableMessage
{
	public CSteamID SteamID { get; set; }

	public ReliableSteamMessage(CSteamID steamIDRemote)
	{
		SteamID = steamIDRemote;
	}

	public ReliableSteamMessage(uint messageID, uint timestamp, uint frame, byte[] buffer, CSteamID steamIDRemote)
		: base(messageID, timestamp, frame, buffer)
	{
		SteamID = steamIDRemote;
	}

	public static ReliableSteamMessage From(CSteamID steamIDRemote, byte[] buffer)
	{
		ReliableSteamMessage reliableSteamMessage = new ReliableSteamMessage(steamIDRemote);
		reliableSteamMessage.Unpack(buffer);
		return reliableSteamMessage;
	}
}
