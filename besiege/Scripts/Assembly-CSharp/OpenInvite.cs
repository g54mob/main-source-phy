using Steamworks;
using UnityEngine;

public class OpenInvite : ClickBehaviour
{
	public void Awake()
	{
		if (!SteamManager.Initialized)
		{
			Object.DestroyImmediate(base.gameObject);
		}
	}

	public override void OnClicked()
	{
		OpenInviteFriendScreen();
	}

	public void OpenInviteFriendScreen()
	{
		ulong lobbyID = BesiegeNetworkManager.Instance.LobbyID;
		SteamFriends.ActivateGameOverlayInviteDialog((CSteamID)lobbyID);
	}
}
