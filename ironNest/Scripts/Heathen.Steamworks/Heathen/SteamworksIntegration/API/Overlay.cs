using Steamworks;
using UnityEngine;

namespace Heathen.SteamworksIntegration.API
{
	public static class Overlay
	{
		public static class Client
		{
			private static bool _isShowing;

			private static ENotificationPosition _notificationPosition;

			private static Vector2Int _notificationInset;

			public static bool IsEnabled => false;

			public static bool IsShowing
			{
				get
				{
					return false;
				}
				internal set
				{
				}
			}

			public static ENotificationPosition NotificationPosition
			{
				get
				{
					return default(ENotificationPosition);
				}
				set
				{
				}
			}

			public static Vector2Int NotificationInset
			{
				get
				{
					return default(Vector2Int);
				}
				set
				{
				}
			}

			[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
			private static void Init()
			{
			}

			public static void Activate(string dialog)
			{
			}

			public static void Activate(OverlayDialog dialog)
			{
			}

			public static void ActivateInviteDialog(LobbyData lobbyId)
			{
			}

			public static void ActivateInviteDialog(string connectionString)
			{
			}

			public static void ActivateRemotePlayInviteDialog(LobbyData lobbyId)
			{
			}

			public static void Activate(AppData appID, EOverlayToStoreFlag flag)
			{
			}

			public static void Activate(string dialog, CSteamID steamId)
			{
			}

			public static void Activate(FriendDialog dialog, CSteamID steamId)
			{
			}

			public static void ActivateWebPage(string url)
			{
			}
		}
	}
}
