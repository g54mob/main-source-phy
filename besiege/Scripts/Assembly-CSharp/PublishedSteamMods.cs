using Localisation;

public class PublishedSteamMods : SteamFileCollection
{
	protected override WorkshopManager.ItemTypes CollectionItemType
	{
		get
		{
			return WorkshopManager.ItemTypes.Mods;
		}
	}

	protected override WorkshopManager.ItemListing CollectionListing
	{
		get
		{
			return WorkshopManager.ItemListing.Published;
		}
	}

	public PublishedSteamMods()
	{
		string translation = LocalisationManager.GetTranslation(504);
		ObjectName = string.Format("{0} {1}", ObjectName, translation);
	}
}
