using System;
using System.Runtime.CompilerServices;
using Heathen.SteamworksIntegration;
using Steamworks;
using UnityEngine;

namespace SteamTools
{
	public static class Events
	{
		private static Callback<DlcInstalled_t> _dlcInstalled;

		private static Callback<NewUrlLaunchParameters_t> _newUrlLaunchParameters;

		private static Callback<SteamServerConnectFailure_t> _steamServerConnectFailure;

		private static Callback<SteamServersConnected_t> _steamServersConnected;

		private static Callback<SteamServersDisconnected_t> _steamServersDisconnected;

		private static Callback<GamepadTextInputDismissed_t> _gamepadTextInputDismissed;

		private static Callback<GameConnectedChatLeave_t> _gameConnectedChatLeave;

		private static Callback<GameConnectedClanChatMsg_t> _gameConnectedClanChatMsg;

		private static Callback<GameConnectedChatJoin_t> _gameConnectedChatJoin;

		private static Callback<GameConnectedFriendChatMsg_t> _gameConnectedFriendChatMsg;

		private static Callback<FriendRichPresenceUpdate_t> _friendRichPresenceUpdate;

		private static Callback<PersonaStateChange_t> _personaStateChange;

		private static Callback<SteamInventoryDefinitionUpdate_t> _steamInventoryDefinitionUpdate;

		private static Callback<SteamInventoryResultReady_t> _steamInventoryResultReady;

		private static Callback<MicroTxnAuthorizationResponse_t> _microTxnAuthorizationResponse;

		private static Callback<LobbyEnter_t> _lobbyEnter;

		private static Callback<LobbyDataUpdate_t> _lobbyDataUpdate;

		private static Callback<LobbyChatMsg_t> _lobbyChatMsg;

		private static Callback<LobbyChatUpdate_t> _lobbyChatUpdate;

		private static Callback<LobbyGameCreated_t> _lobbyGameCreated;

		private static Callback<LobbyInvite_t> _lobbyInvite;

		private static Callback<FavoritesListChanged_t> _favoritesListChanged;

		private static Callback<GameLobbyJoinRequested_t> _gameLobbyJoinRequested;

		private static Callback<GameOverlayActivated_t> _gameOverlayActivated;

		private static Callback<GameServerChangeRequested_t> _gameServerChangeRequested;

		private static Callback<GameRichPresenceJoinRequested_t> _gameRichPresenceJoinRequested;

		private static Callback<ReservationNotificationCallback_t> _reservationNotificationCallback;

		private static Callback<ActiveBeaconsUpdated_t> _activeBeaconsUpdated;

		private static Callback<AvailableBeaconLocationsUpdated_t> _availableBeaconLocationsUpdated;

		private static Callback<SteamRemotePlaySessionConnected_t> _remotePlaySessionConnected;

		private static Callback<SteamRemotePlaySessionDisconnected_t> _remotePlaySessionDisconnected;

		private static Callback<RemoteStorageLocalFileChange_t> _remoteStorageLocalFileChange;

		private static Callback<ScreenshotReady_t> _screenshotReady;

		private static Callback<ScreenshotRequested_t> _screenshotRequested;

		private static Callback<UserStatsReceived_t> _userStatsReceived;

		private static Callback<UserStatsUnloaded_t> _userStatsUnloaded;

		private static Callback<UserStatsStored_t> _userStatsStored;

		private static Callback<UserAchievementStored_t> _userAchievementStored;

		private static Callback<AppResumingFromSuspend_t> _appResumeFromSuspend;

		private static Callback<FloatingGamepadTextInputDismissed_t> _floatingGamepadTextInputDismissed;

		public static event Action OnSteamInitialised
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		public static event StringDelegate OnSteamInitialisationError
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		public static event DlcDataDelegate OnDlcInstalled
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		public static event Action OnNewUrlLaunchParameters
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		public static event Action OnSteamServersConnected
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		public static event SteamServerConnectFailureDelegate OnSteamServerConnectFailure
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		public static event EResultDelegate OnSteamServersDisconnected
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		public static event Action OnGamepadTextInputShown
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		public static event SteamTextInputDelegate OnGamepadTextInputDismissed
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		public static event SteamUserLeaveDataDelegate OnGameConnectedChatLeave
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		public static event SteamClanChatMsgDelegate OnGameConnectedClanChatMsg
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		public static event SteamClanChatJoinDelegate OnGameConnectedChatJoin
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		public static event SteamFriendChatMsgDelegate OnGameConnectedFriendChatMsg
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		public static event SteamFriendRichPresenceUpdateDelegate OnFriendRichPresenceUpdate
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		public static event PersonaStateChangeEvent OnPersonaStateChange
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		public static event InputControllerStateDataDelegate OnInputDataChanged
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		public static event SteamInputHandleDelegate OnControllerConnected
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		public static event SteamInputHandleDelegate OnControllerDisconnected
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		public static event Action OnInventoryDefinitionUpdate
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		public static event SteamInventoryResultReadyDelegate OnInventoryResultReady
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		public static event SteamMtxTranAuthDelegate OnMicroTxnAuthorisationResponse
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		public static event SteamLobbyEnterSuccessDelegate OnLobbyEnterSuccess
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		public static event SteamLobbyEnterFailedDelegate OnLobbyEnterFailed
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		public static event SteamLobbyDataUpdateDelegate OnLobbyDataUpdate
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		public static event SteamLobbyChatMsgDelegate OnLobbyChatMsg
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		public static event SteamLobbyAuthDelegate OnLobbyAuthentication
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		public static event SteamLobbyDataDelegate OnAskedToLeaveLobby
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		public static event SteamLobbyChatUpdateDelegate OnLobbyChatUpdate
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		public static event SteamLobbyGameServerDelegate OnLobbyGameServer
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		public static event SteamLobbyDataDelegate OnLobbyLeave
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		public static event SteamLobbyInviteDelegate OnLobbyInvite
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		public static event SteamFavoritesListChangeDelegate OnFavoritesListChanged
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		public static event SteamLobbyJoinRequestDelegate OnLobbyJoinRequested
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		public static event BoolDelegate OnGameOverlayActivated
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		public static event SteamGameServerChangeRequestedDelegate OnGameServerChangeRequested
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		public static event SteamRichPresenceJoinRequestedDelegate OnRichPresenceJoinRequested
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		public static event SteamReservationNotificationDelegate OnReservationNotification
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		public static event Action OnActiveBeaconsUpdated
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		public static event Action OnAvailableBeaconLocationsUpdated
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		public static event SteamRemotePlaySessionIdDelegate OnRemotePlaySessionConnected
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		public static event SteamRemotePlaySessionIdDelegate OnRemotePlaySessionDisconnected
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		public static event Action OnRemoteStorageLocalFileChange
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		public static event SteamScreenshotReadyDelegate OnScreenshotReady
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		public static event Action OnScreenshotRequested
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		public static event SteamStatsReceivedDelegate OnStatsReceived
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		public static event SteamUserStatsUnloadedDelegate OnStatsUnloaded
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		public static event SteamUserAchievementStoredDelegate OnUserAchievementStored
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		public static event SteamStatsStoredDelegate OnStatsStored
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		public static event Action OnAppResumeFromSuspend
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		public static event Action OnKeyboardShown
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		public static event Action OnKeyboardClosed
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void Init()
		{
		}

		private static void DisposeCallbacks()
		{
		}

		internal static void Initialise()
		{
		}

		private static void OnFloatingGamepadTextInputDismissedCallback(FloatingGamepadTextInputDismissed_t param)
		{
		}

		private static void OnAppResumeFromSuspendCallback(AppResumingFromSuspend_t param)
		{
		}

		private static void OnUserAchievementStoredCallback(UserAchievementStored_t param)
		{
		}

		private static void OnUserStatsStoredCallback(UserStatsStored_t param)
		{
		}

		private static void OnUserStatsUnloadedCallback(UserStatsUnloaded_t param)
		{
		}

		private static void OnUserStatsReceivedCallback(UserStatsReceived_t param)
		{
		}

		private static void OnScreenshotRequestedCallback(ScreenshotRequested_t param)
		{
		}

		private static void OnScreenshotReadyCallback(ScreenshotReady_t param)
		{
		}

		private static void OnRemoteStorageLocalFileChangeCallback(RemoteStorageLocalFileChange_t param)
		{
		}

		private static void OnRemotePlaySessionDisconnectedCallback(SteamRemotePlaySessionDisconnected_t param)
		{
		}

		private static void OnRemotePlaySessionConnectedCallback(SteamRemotePlaySessionConnected_t param)
		{
		}

		private static void OnAvailableBeaconLocationsUpdatedCallback(AvailableBeaconLocationsUpdated_t param)
		{
		}

		private static void OnActiveBeaconsUpdatedCallback(ActiveBeaconsUpdated_t param)
		{
		}

		private static void OnReservationNotificationCallback(ReservationNotificationCallback_t param)
		{
		}

		private static void OnGameRichPresenceJoinRequestedCallback(GameRichPresenceJoinRequested_t param)
		{
		}

		private static void OnGameServerChangeRequestedCallback(GameServerChangeRequested_t param)
		{
		}

		private static void OnGameOverlayActivatedCallback(GameOverlayActivated_t param)
		{
		}

		private static void OnGameLobbyJoinRequestedCallback(GameLobbyJoinRequested_t param)
		{
		}

		private static void OnFavoritesListChangedCallback(FavoritesListChanged_t param)
		{
		}

		private static void OnLobbyInviteCallback(LobbyInvite_t param)
		{
		}

		private static void OnLobbyGameCreated(LobbyGameCreated_t param)
		{
		}

		private static void OnLobbyChatUpdateCallback(LobbyChatUpdate_t param)
		{
		}

		private static void OnLobbyChatMsgCallback(LobbyChatMsg_t param)
		{
		}

		private static void OnLobbyDataUpdateCallback(LobbyDataUpdate_t param)
		{
		}

		private static void OnLobbyEnterCallback(LobbyEnter_t param)
		{
		}

		private static void OnMicroTxnAuthorizationResponseCallback(MicroTxnAuthorizationResponse_t param)
		{
		}

		private static void OnSteamInventoryResultReadyCallback(SteamInventoryResultReady_t param)
		{
		}

		private static void OnSteamInventoryDefinitionUpdateCallback(SteamInventoryDefinitionUpdate_t param)
		{
		}

		private static void OnPersonaStateChangeCallback(PersonaStateChange_t param)
		{
		}

		private static void OnFriendRichPresenceUpdateCallback(FriendRichPresenceUpdate_t param)
		{
		}

		private static void OnGameConnectedFriendChatMsgCallback(GameConnectedFriendChatMsg_t param)
		{
		}

		private static void OnGameConnectedChatJoinCallback(GameConnectedChatJoin_t param)
		{
		}

		private static void OnGameConnectedClanChatMsgCallback(GameConnectedClanChatMsg_t param)
		{
		}

		private static void OnGameConnectedChatLeaveCallback(GameConnectedChatLeave_t param)
		{
		}

		private static void OnGamepadTextInputDismissedCallback(GamepadTextInputDismissed_t param)
		{
		}

		private static void OnSteamServerConnectFailureCallback(SteamServerConnectFailure_t param)
		{
		}

		private static void OnSteamServersConnectedCallback(SteamServersConnected_t _)
		{
		}

		internal static void InvokeOnSteamInitialised()
		{
		}

		internal static void InvokeOnSteamInitialisationError(string message)
		{
		}

		internal static void OnDlcInstalledCallback(DlcInstalled_t data)
		{
		}

		internal static void OnNewUrlLaunchParametersCallback(NewUrlLaunchParameters_t _)
		{
		}

		internal static void OnSteamServersDisconnectedCallback(SteamServersDisconnected_t param)
		{
		}

		internal static void InvokeOnGamepadTextInputShown()
		{
		}

		internal static void InvokeOnInputDataChanged(InputControllerStateData data)
		{
		}

		internal static void InvokeOnControllerConnected(InputHandle_t handle)
		{
		}

		internal static void InvokeOnControllerDisconnected(InputHandle_t handle)
		{
		}

		internal static void InvokeOnInventoryResultReady(InventoryResult result)
		{
		}

		internal static void InvokeOnLobbyLeave(LobbyData lobby)
		{
		}

		internal static void InvokeOnKeyboardShown()
		{
		}
	}
}
