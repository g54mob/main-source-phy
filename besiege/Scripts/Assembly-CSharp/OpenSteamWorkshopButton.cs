public class OpenSteamWorkshopButton : OpenWorkshopButton
{
	protected override bool Initialize()
	{
		if (!SteamManager.Initialized)
		{
			return false;
		}
		return true;
	}
}
