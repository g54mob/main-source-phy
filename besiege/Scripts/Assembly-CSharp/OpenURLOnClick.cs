using Steamworks;
using UnityEngine;

public class OpenURLOnClick : ClickBehaviour
{
	public string url = string.Empty;

	public override void OnClicked()
	{
		if (url.ToLower().Contains("steam"))
		{
			SteamFriends.ActivateGameOverlayToWebPage(url);
		}
		else
		{
			Application.OpenURL(url);
		}
	}
}
