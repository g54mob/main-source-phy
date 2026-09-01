using Localisation;

public class PublishedSteamLevels : SteamFileCollection
{
	protected override WorkshopManager.ItemTypes CollectionItemType
	{
		get
		{
			return WorkshopManager.ItemTypes.Levels;
		}
	}

	protected override WorkshopManager.ItemListing CollectionListing
	{
		get
		{
			return WorkshopManager.ItemListing.Published;
		}
	}

	public override string FilterExtension
	{
		get
		{
			return ".blv";
		}
	}

	public PublishedSteamLevels()
	{
		string translation = LocalisationManager.GetTranslation(2102);
		ObjectName = string.Format("{0} {1}", ObjectName, translation);
	}
}
