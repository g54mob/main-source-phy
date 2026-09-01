using Steamworks;

public class SteamCallbackHandler : IPlatformCallbackHandler
{
	private Callback<GameRichPresenceJoinRequested_t> Callback_GameRichPresenceJoinRequested;

	private Callback<GameLobbyJoinRequested_t> Callback_GameLobbyJoinRequested;

	public bool Initialize()
	{
		if (!SteamManager.Initialized)
		{
			return false;
		}
		CreateCallbacks();
		return true;
	}

	private void CreateCallbacks()
	{
		Callback_GameLobbyJoinRequested = Callback<GameLobbyJoinRequested_t>.Create(OnGameLobbyJoinRequested);
		Callback_GameRichPresenceJoinRequested = Callback<GameRichPresenceJoinRequested_t>.Create(OnGameRichPresenceJoinRequested);
	}

	private void OnGameRichPresenceJoinRequested(GameRichPresenceJoinRequested_t pCallback)
	{
		string connectString = ((pCallback.m_rgchConnect != null) ? pCallback.m_rgchConnect : string.Empty);
		BesiegeEntryPointHelper.JoinGameServer(connectString);
	}

	private void OnGameLobbyJoinRequested(GameLobbyJoinRequested_t pCallback)
	{
		JoinGameLobby(pCallback.m_steamIDLobby.m_SteamID);
	}

	private void JoinGameLobby(ulong lobbyID)
	{
		BesiegeEntryPointHelper.JoinGameLobby(lobbyID);
	}

	public void Dispose()
	{
		if (SteamManager.Initialized)
		{
			Callback_GameRichPresenceJoinRequested.Dispose();
			Callback_GameLobbyJoinRequested.Dispose();
		}
	}
}
