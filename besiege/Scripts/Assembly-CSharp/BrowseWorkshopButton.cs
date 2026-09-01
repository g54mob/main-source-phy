using Localisation;
using Steamworks;
using UnityEngine;

public class BrowseWorkshopButton : ClickBehaviour
{
	protected ReferenceMaster.WorkshopItemType itemType;

	[SerializeField]
	protected TextMesh browseWorkshopText;

	public void Initialize(FileBrowserType fileBrowserType)
	{
		itemType = GetItemTypeForFileBrowser(fileBrowserType);
		UpdateVisual();
	}

	protected virtual void UpdateVisual()
	{
		string text = string.Empty;
		switch (itemType)
		{
		case ReferenceMaster.WorkshopItemType.Machine:
			text = LocalisationManager.GetTranslation(946);
			break;
		case ReferenceMaster.WorkshopItemType.Levels:
			text = LocalisationManager.GetTranslation(2203);
			break;
		case ReferenceMaster.WorkshopItemType.Skins:
			text = LocalisationManager.GetTranslation(965);
			break;
		}
		browseWorkshopText.text = text;
	}

	private ReferenceMaster.WorkshopItemType GetItemTypeForFileBrowser(FileBrowserType fileBrowserType)
	{
		switch (fileBrowserType)
		{
		case FileBrowserType.LocalMachines:
		case FileBrowserType.SteamMachines:
		case FileBrowserType.WeGameMachines:
		case FileBrowserType.ModIOMachines:
			return ReferenceMaster.WorkshopItemType.Machine;
		case FileBrowserType.LocalLevels:
		case FileBrowserType.SteamLevels:
		case FileBrowserType.WeGameLevels:
		case FileBrowserType.ModIOLevels:
			return ReferenceMaster.WorkshopItemType.Levels;
		case FileBrowserType.Skins:
			return ReferenceMaster.WorkshopItemType.Skins;
		default:
			return ReferenceMaster.WorkshopItemType.Machine;
		}
	}

	public override void OnClicked()
	{
		HandleClickSteam();
	}

	protected void HandleClickSteam()
	{
		if (SteamManager.Initialized)
		{
			string pchURL;
			switch (itemType)
			{
			case ReferenceMaster.WorkshopItemType.Skins:
				pchURL = "http://steamcommunity.com/workshop/browse/?appid=346010&requiredtags[]=Skin+Packs";
				break;
			case ReferenceMaster.WorkshopItemType.Levels:
				pchURL = "http://steamcommunity.com/workshop/browse/?appid=346010&requiredtags[]=Levels";
				break;
			default:
				pchURL = "http://steamcommunity.com/app/346010/workshop/";
				break;
			}
			SteamFriends.ActivateGameOverlayToWebPage(pchURL);
		}
	}
}
