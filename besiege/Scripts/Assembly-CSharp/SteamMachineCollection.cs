using Localisation;

public class SteamMachineCollection : SteamFileCollection
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
			return WorkshopManager.ItemListing.Subscribed;
		}
	}

	public override string FilterExtension
	{
		get
		{
			return ".bsg";
		}
	}

	public SteamMachineCollection()
	{
		string translation = LocalisationManager.GetTranslation(927);
		ObjectName = string.Format("{0} {1}", ObjectName, translation);
	}
}
