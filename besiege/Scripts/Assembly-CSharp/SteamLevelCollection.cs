using Localisation;

public class SteamLevelCollection : SteamFileCollection
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
			return WorkshopManager.ItemListing.Subscribed;
		}
	}

	public override string FilterExtension
	{
		get
		{
			return ".blv";
		}
	}

	public SteamLevelCollection()
	{
		string translation = LocalisationManager.GetTranslation(2102);
		ObjectName = string.Format("{0} {1}", ObjectName, translation);
	}
}
