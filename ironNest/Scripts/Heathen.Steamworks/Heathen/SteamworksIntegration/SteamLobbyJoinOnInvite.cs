using UnityEngine;

namespace Heathen.SteamworksIntegration
{
	[ModularComponent(typeof(SteamLobbyData), "Join on Invite", null)]
	[AddComponentMenu(null)]
	[RequireComponent(typeof(SteamLobbyJoin))]
	[RequireComponent(typeof(SteamLobbyData))]
	public class SteamLobbyJoinOnInvite : MonoBehaviour
	{
		public enum JoinOnMode
		{
			WithInitialInvite = 0,
			AfterAcceptInFriendChat = 1
		}

		public enum FilterMode
		{
			None = 0,
			IgnoreIfInParty = 1,
			IgnoreIfInSession = 2,
			IgnoreIfInAny = 3
		}

		public enum PreprocessOptions
		{
			None = 0,
			LeaveAllFirst = 1,
			LeavePartyFirst = 2,
			LeaveSessionFirst = 3
		}

		[SettingsField(0, false, "Join")]
		public JoinOnMode mode;

		[SettingsField(0, false, "Join")]
		public FilterMode filter;

		[SettingsField(0, false, "Join")]
		public PreprocessOptions preprocess;

		private SteamLobbyData _mInspector;

		private SteamLobbyJoin _mJoin;

		private void Awake()
		{
		}

		private void OnDestroy()
		{
		}

		private bool CanProcess()
		{
			return false;
		}

		private void Preprocess()
		{
		}

		private void HandleInviteReceived(UserData user, LobbyData lobby, GameData game)
		{
		}

		private void HandleInviteAccepted(LobbyData arg0, UserData arg1)
		{
		}

		public void OpenOverlay()
		{
		}

		public void InviteUser(UserData user)
		{
		}
	}
}
