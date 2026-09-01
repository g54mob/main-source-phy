using System.Collections.Generic;
using Steamworks;
using UnityEngine;
using UnityEngine.Serialization;

namespace Heathen.SteamworksIntegration
{
	[AddComponentMenu("Steamworks/Overlay")]
	[HelpURL("https://kb.heathen.group/steam/features/overlay")]
	[DisallowMultipleComponent]
	public class OverlayManager : MonoBehaviour
	{
		public enum ManagedEvents
		{
			JoinGameRequested = 0,
			LobbyInviteAccepted = 1,
			OverlayActivated = 2,
			ServerChangeRequested = 3
		}

		[FormerlySerializedAs("m_Delegates")]
		[SerializeField]
		private List<ManagedEvents> mDelegates;

		[SerializeField]
		private ENotificationPosition notificationPosition;

		[SerializeField]
		private Vector2Int notificationInset;

		public GameOverlayActivatedEvent evtOverlayActivated;

		public GameLobbyJoinRequestedEvent evtGameLobbyJoinRequested;

		public GameServerChangeRequestedEvent evtGameServerChangeRequested;

		public GameRichPresenceJoinRequestedEvent evtRichPresenceJoinRequested;

		public ENotificationPosition NotificationPosition
		{
			get
			{
				return default(ENotificationPosition);
			}
			set
			{
			}
		}

		public Vector2Int NotificationInset
		{
			get
			{
				return default(Vector2Int);
			}
			set
			{
			}
		}

		public bool IsShowing => false;

		public bool IsEnabled => false;

		private void OnEnable()
		{
		}

		private void EnabledProcess()
		{
		}

		private void OnDisable()
		{
		}

		public void OpenDialogName(string dialog)
		{
		}

		public void OpenDialog(OverlayDialog dialog)
		{
		}

		public void OpenLobbyInvite(LobbyData lobbyId)
		{
		}

		public void OpenLobbyInvite(SteamLobbyData lobby)
		{
		}

		public void OpenConnectStringInvite(string connectionString)
		{
		}

		public void OpenRemotePlayInvite(LobbyData lobbyId)
		{
		}

		public void OpenRemotePlayInvite(SteamLobbyData lobby)
		{
		}

		public void OpenStore(int appId)
		{
		}

		public void OpenStoreAddToCart(int appId)
		{
		}

		public void OpenStoreAddToCartAndShow(int appId)
		{
		}

		public void OpenStore(AppData appID, EOverlayToStoreFlag flag)
		{
		}

		public void OpenUserProfile(SteamUserData user)
		{
		}

		public void OpenUserChat(SteamUserData user)
		{
		}

		public void OpenUserJoinTrade(SteamUserData user)
		{
		}

		public void OpenUserStats(SteamUserData user)
		{
		}

		public void OpenUserAchievements(SteamUserData user)
		{
		}

		public void OpenUserAddFriend(SteamUserData user)
		{
		}

		public void OpenUserRemoveFriend(SteamUserData user)
		{
		}

		public void OpenUserAcceptFriendRequest(SteamUserData user)
		{
		}

		public void OpenUserIgnoreFriendRequest(SteamUserData user)
		{
		}

		public void OpenUser(string dialog, CSteamID steamId)
		{
		}

		public void OpenUser(FriendDialog dialog, CSteamID steamId)
		{
		}

		public void OpenWebPage(string url)
		{
		}
	}
}
