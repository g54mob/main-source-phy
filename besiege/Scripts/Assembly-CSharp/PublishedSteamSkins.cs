using Localisation;

public class PublishedSteamSkins : SteamFileCollection
{
	protected override WorkshopManager.ItemTypes CollectionItemType
	{
		get
		{
			return WorkshopManager.ItemTypes.Skins;
		}
	}

	protected override WorkshopManager.ItemListing CollectionListing
	{
		get
		{
			return WorkshopManager.ItemListing.Published;
		}
	}

	public PublishedSteamSkins()
	{
		string translation = LocalisationManager.GetTranslation(955);
		ObjectName = string.Format("{0} {1}", ObjectName, translation);
	}
}
