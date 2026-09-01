using UnityEngine;

namespace Heathen.SteamworksIntegration
{
	[ModularComponent(typeof(SteamLobbyData), "Leave", null)]
	[AddComponentMenu(null)]
	[RequireComponent(typeof(SteamLobbyData))]
	public class SteamLobbyLeave : MonoBehaviour
	{
		private SteamLobbyData _mInspector;

		private void Awake()
		{
		}

		public void Leave()
		{
		}
	}
}
