using Steamworks;
using UnityEngine;

namespace Heathen.SteamworksIntegration
{
	public class InputActionSet : ScriptableObject
	{
		public string setName;

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
