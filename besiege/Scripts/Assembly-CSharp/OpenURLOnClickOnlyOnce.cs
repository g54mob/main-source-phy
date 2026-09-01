using Steamworks;
using UnityEngine;

public class OpenURLOnClickOnlyOnce : ClickBehaviour
{
	public GameObject objectToDisable;

	public string url = string.Empty;

	private void Start()
	{
		if (TutorialFileManager.GetTutorialState(objectToDisable.name) == 0)
		{
			objectToDisable.SetActive(false);
		}
	}

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
		TutorialFileManager.SetTutorialState(objectToDisable.name, 0);
	}
}
