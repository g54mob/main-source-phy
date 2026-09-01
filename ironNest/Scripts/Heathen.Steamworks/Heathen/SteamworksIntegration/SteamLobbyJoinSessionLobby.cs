using UnityEngine;

namespace Heathen.SteamworksIntegration
{
	[ModularComponent(typeof(SteamLobbyData), "Join Session", null)]
	[AddComponentMenu(null)]
	[RequireComponent(typeof(SteamLobbyData))]
	[RequireComponent(typeof(SteamLobbyJoin))]
	public class SteamLobbyJoinSessionLobby : MonoBehaviour
	{
		[SettingsField(0, false, "Join")]
		public SteamLobbyData partyLobbyData;

		[SettingsField(0, false, "Join")]
		public bool leaveOnSessionClear;

		private SteamLobbyData _mInspector;

		private SteamLobbyJoin _mJoin;

		private void Awake()
		{
		}

		private void OnDestroy()
		{
		}

		private void HandleChange(LobbyData arg0)
		{
		}

		private void HandleDataUpdate(LobbyData lobby, LobbyMemberData? member)
		{
		}

		private void JoinSessionLobby(LobbyData sessionLobby)
		{
		}
	}
}
