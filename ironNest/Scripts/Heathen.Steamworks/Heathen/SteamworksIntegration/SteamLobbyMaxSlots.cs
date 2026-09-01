using TMPro;
using UnityEngine;

namespace Heathen.SteamworksIntegration
{
	[ModularComponent(typeof(SteamLobbyData), "Max Slots", "label")]
	[AddComponentMenu(null)]
	[RequireComponent(typeof(SteamLobbyData))]
	public class SteamLobbyMaxSlots : MonoBehaviour
	{
		public TextMeshProUGUI label;

		private SteamLobbyData _mInspector;

		private void Awake()
		{
		}

		private void OnDestroy()
		{
		}

		private void HandleLobbyDataChange(LobbyData lobby, LobbyMemberData? member)
		{
		}

		private void HandleOnChanged(LobbyData arg0)
		{
		}
	}
}
