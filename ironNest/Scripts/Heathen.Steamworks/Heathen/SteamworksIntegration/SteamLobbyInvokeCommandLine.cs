using UnityEngine;

namespace Heathen.SteamworksIntegration
{
	[ModularComponent(typeof(SteamLobbyData), "Command Line", null)]
	[AddComponentMenu(null)]
	[RequireComponent(typeof(SteamLobbyDataEvents))]
	public class SteamLobbyInvokeCommandLine : MonoBehaviour
	{
		public enum Rule
		{
			Any = 0,
			PartyOnly = 1,
			SessionOnly = 2,
			GeneralOnly = 3,
			NotParty = 4,
			NotSession = 5,
			NotGeneral = 6
		}

		[SettingsField(0, false, "Launch Command")]
		public Rule joinRequestedWhen;

		private SteamLobbyDataEvents _mEvents;

		private LobbyData _pendingLobby;

		private void Start()
		{
		}

		private void HandleLobbyDataUpdate(LobbyData lobby, LobbyMemberData? member)
		{
		}
	}
}
