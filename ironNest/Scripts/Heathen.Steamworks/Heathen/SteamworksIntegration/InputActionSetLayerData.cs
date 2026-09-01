using Steamworks;

namespace Heathen.SteamworksIntegration
{
	public struct InputActionSetLayerData
	{
		public string LayerName;

		public InputActionSetData Data { get; private set; }

		public bool IsActive(InputHandle_t controller)
		{
			return false;
		}

		public void Activate(InputHandle_t controller)
		{
		}

		public void Activate()
		{
		}
	}
}
