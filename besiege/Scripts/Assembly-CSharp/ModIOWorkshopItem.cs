using ModIO;

public class ModIOWorkshopItem : WorkshopManager.WorkshopItem
{
	public ModProfile Profile { get; private set; }

	public ModIOWorkshopItem(string title, ModProfile profile)
		: base(title)
	{
		Profile = profile;
	}
}
