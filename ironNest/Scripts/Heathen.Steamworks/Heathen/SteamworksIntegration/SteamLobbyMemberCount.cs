using Steamworks;
using TMPro;
using UnityEngine;

namespace Heathen.SteamworksIntegration
{
	[ModularComponent(typeof(SteamLobbyData), "Member Count", "label")]
	[AddComponentMenu(null)]
	[RequireComponent(typeof(SteamLobbyData))]
	public class SteamLobbyMemberCount : MonoBehaviour
	{
		public TextMeshProUGUI label;

		private SteamLobbyData _mInspector;

		private void Awake()
		{
		}

		private void OnDestroy()
		{
		}

		private void HandleChatUpdate(LobbyData lobby, UserData user, EChatMemberStateChange state)
		{
		}

		private void HandleOnChanged(LobbyData arg0)
		{
		}
	}
}
