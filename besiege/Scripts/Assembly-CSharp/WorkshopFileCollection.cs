public abstract class WorkshopFileCollection : AbstractObjectCollection
{
	public override bool HideFileField
	{
		get
		{
			return true;
		}
	}

	protected abstract WorkshopManager.ItemTypes CollectionItemType { get; }

	protected abstract WorkshopManager.ItemListing CollectionListing { get; }

	public override VirtualFolder GetRoot()
	{
		if (CollectionListing == WorkshopManager.ItemListing.Subscribed)
		{
			return GetSubscribedItems(CollectionItemType);
		}
		return GetPublishedItems(CollectionItemType);
	}

	public override void Refresh()
	{
		GetRoot();
		InvokeCollectionChanged();
	}

	protected abstract VirtualFolder GetSubscribedItems(WorkshopManager.ItemTypes contentType);

	protected abstract VirtualFolder GetPublishedItems(WorkshopManager.ItemTypes contentType);
}
