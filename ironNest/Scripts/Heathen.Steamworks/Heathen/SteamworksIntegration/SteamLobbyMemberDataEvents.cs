using Steamworks;
using UnityEngine;
using UnityEngine.Events;

namespace Heathen.SteamworksIntegration
{
	[ModularEvents(typeof(SteamLobbyMemberData))]
	[AddComponentMenu(null)]
	[RequireComponent(typeof(SteamLobbyMemberData))]
	public class SteamLobbyMemberDataEvents : MonoBehaviour
	{
		[EventField]
		public UnityEvent<bool> onIsLobbyOwnerStatus;

		[EventField]
		public UnityEvent<bool> onReadyChanged;

		[EventField]
		public UnityEvent<LobbyData, LobbyMemberData> onMetadataChanged;

		private SteamLobbyMemberData _mSteamLobbyMemberData;

		private bool _mReady;

		private void Awake()
		{
		}

		private void OnDestroy()
		{
		}

		private void GlobalDataUpdate(LobbyData lobby, LobbyMemberData? member)
		{
		}

		private void ChatStateUpdate(LobbyData lobby, UserData user, EChatMemberStateChange state)
		{
		}
	}
}
