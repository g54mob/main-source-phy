namespace Heathen.SteamworksIntegration
{
	public interface ISteamInputActionData
	{
		InputActionSetData Set { get; set; }

		InputActionSetLayerData Layer { get; set; }

		InputActionData Action { get; set; }
	}
}
