using Localisation;

public class PublishedSteamMachines : SteamFileCollection
{
	protected override WorkshopManager.ItemTypes CollectionItemType
	{
		get
		{
			return WorkshopManager.ItemTypes.Machines;
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
			return ".bsg";
		}
	}

	public PublishedSteamMachines()
	{
		string translation = LocalisationManager.GetTranslation(927);
		ObjectName = string.Format("{0} {1}", ObjectName, translation);
	}
}
