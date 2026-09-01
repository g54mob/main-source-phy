using TMPro;
using UnityEngine;

namespace Heathen.SteamworksIntegration
{
	[ModularComponent(typeof(SteamLobbyData), "Names", "label")]
	[AddComponentMenu(null)]
	[RequireComponent(typeof(SteamLobbyData))]
	public class SteamLobbyName : MonoBehaviour
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
