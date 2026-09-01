using Steamworks;
using UnityEngine;

[AddComponentMenu("UI/ShowAdOnce")]
public class ShowAdOnce : ClickBehaviour
{
	[SerializeField]
	private string webLink = string.Empty;

	[SerializeField]
	private GameObject[] objectsToDisable;

	[SerializeField]
	private GameObject[] extraObjectsToEnable;

	private void Start()
	{
		if (TutorialFileManager.GetTutorialState("SwitchAD") != 1)
		{
			for (int i = 0; i < objectsToDisable.Length; i++)
			{
				if (objectsToDisable[i] != null)
				{
					objectsToDisable[i].SetActive(false);
				}
			}
			for (int j = 0; j < extraObjectsToEnable.Length; j++)
			{
				if (extraObjectsToEnable[j] != null)
				{
					extraObjectsToEnable[j].SetActive(true);
				}
			}
			TutorialFileManager.SetTutorialState("SwitchAD", 1);
		}
		else
		{
			base.gameObject.SetActive(false);
		}
	}

	public override void OnClicked()
	{
		if (webLink != string.Empty)
		{
			SteamFriends.ActivateGameOverlayToWebPage(webLink);
		}
	}
}
