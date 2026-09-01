using Steamworks;
using UnityEngine;

public class BrowseSteamGuides : ClickBehaviour
{
	public void Awake()
	{
	}

	public override void OnClicked()
	{
		HandleClickSteam();
	}

	public void OnEnable()
	{
		foreach (Transform item in base.transform)
		{
			item.gameObject.SetActive(SteamManager.Initialized);
		}
	}

	private void HandleClickSteam()
	{
		if (SteamManager.Initialized)
		{
			string pchURL = "http://steamcommunity.com/app/346010/guides/";
			SteamFriends.ActivateGameOverlayToWebPage(pchURL);
		}
	}
}
