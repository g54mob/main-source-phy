public interface IMenuNetCallbacks
{
	void onLobbyMemberDataChanged();

	void onLobbyCodeChanged(string lobbyCode);

	void onMatchCreateFail(string code, string message);

	void onLobbyPacket(Packet packet);

	void onCantEnterLobby();

	void onEnterLobby();

	void onCantEnterDetailsLobby(string code, string message);

	void onGotLobbyInvitation(string lobbyId);

	void onFailedToFindLobby(string message);

	void lobbyReady();

	void onCantEnterSteamLobbyFromCrossplatform();

	void onPlayerSynced(NetPlayerData player);
}
