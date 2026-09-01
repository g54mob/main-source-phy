using TMPro;
using UnityEngine;

namespace Heathen.SteamworksIntegration
{
	[AddComponentMenu(null)]
	[ModularComponent(typeof(SteamLobbyData), "Hex Labels", "label")]
	[RequireComponent(typeof(SteamLobbyData))]
	public class SteamLobbyHexIdLabel : MonoBehaviour
	{
		public TextMeshProUGUI label;

		private SteamLobbyData _inspector;

		private void Awake()
		{
		}

		private void OnDestroy()
		{
		}

		private void HandleOnChanged(LobbyData arg0)
		{
		}
	}
}
