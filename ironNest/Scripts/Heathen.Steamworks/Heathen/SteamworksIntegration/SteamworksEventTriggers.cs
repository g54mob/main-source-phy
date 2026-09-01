using System.Collections.Generic;
using Heathen.SteamworksIntegration.API;
using Steamworks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Heathen.SteamworksIntegration
{
	[DisallowMultipleComponent]
	public class SteamworksEventTriggers : MonoBehaviour
	{
		[FormerlySerializedAs("mDelegates")]
		[FormerlySerializedAs("m_Delegates")]
		[SerializeField]
		private List<SteamEventTriggerType> delegates;

		public UnityEvent initSuccess;

		public StringEvent initFailed;

		public DlcInstalledEvent dlcInstalled;

		public UnityEvent newUrlLaunchParameters;

		public App.Client.UnityEventServersDisconnected serversDisconnected;

		public UnityEvent serversConnected;

		public App.Client.UnityEventServersConnectFailure serversConnectFailure;

		public UnityEvent gamepadTextInputShown;

		public UnityEvent<bool, string> gamepadTextInputDismissed;

		public UnityEvent<ChatRoom, UserData, string, EChatEntryType> chatMessageReceived;

		public GameConnectedChatJoinEvent gameConnectedChatJoin;

		public UnityEvent<ChatRoom, UserData, bool, bool> gameConnectedChatLeave;

		public GameConnectedFriendChatMsgEvent gameConnectedFriendChatMsg;

		public UnityEvent<UserData, AppData> friendRichPresenceUpdate;

		public UnityEvent<UserData, EPersonaChange> personaStateChange;

		public ControllerDataEvent inputDataChanged;

		public UnityEvent<InputHandle_t> controllerConnected;

		public UnityEvent<InputHandle_t> controllerDisconnected;

		public SteamInventoryDefinitionUpdateEvent inventoryDefinitionUpdate;

		public SteamInventoryResultReadyEvent inventoryResultReady;

		public UnityEvent<AppData, ulong, bool> microTransactionAuthorizationResponse;

		public FavoritesListChangedEvent serverFavoritesListChanged;

		public LobbyDataEvent lobbyAskedToLeave;

		public LobbyAuthenticationEvent lobbyAuthenticationRequest;

		public LobbyChatMsgEvent lobbyChatMsg;

		public UnityEvent<LobbyData, UserData, EChatMemberStateChange> lobbyChatUpdate;

		public UnityEvent<LobbyData, LobbyMemberData?> lobbyChatDataUpdate;

		public UnityEvent<LobbyData, EChatRoomEnterResponse> lobbyEnterFailed;

		public UnityEvent<LobbyData> lobbyEnterSuccess;

		public UnityEvent<LobbyData, CSteamID, string, ushort> lobbyGameCreated;

		public UnityEvent<UserData, LobbyData, GameData> lobbyInvite;

		public LobbyDataEvent lobbyLeave;

		public GameLobbyJoinRequestedEvent gameLobbyJoinRequested;

		public UnityEvent<bool> gameOverlayActivated;

		public GameRichPresenceJoinRequestedEvent gameRichPresenceJoinRequested;

		public UnityEvent<string, string> gameServerChangeRequested;

		public UnityEvent activeBeaconsUpdated;

		public UnityEvent availableBeaconLocationsUpdated;

		public UnityEvent<UserData, PartyBeaconID_t> reservationNotificationCallback;

		public UnityEvent<RemotePlaySessionID_t> remotePlaySessionConnected;

		public UnityEvent<RemotePlaySessionID_t> remotePlaySessionDisconnected;

		public UnityEvent remoteStorageFileChange;

		public UnityEvent<ScreenshotHandle, EResult> screenshotReady;

		public UnityEvent screenshotRequested;

		public UnityEvent<UserAchievementStoredData> achievementStored;

		public UnityEvent<GameData, EResult, UserData> statsReceived;

		public UnityEvent<GameData, EResult> statsStored;

		public UnityEvent<UserData> statsUnloaded;

		public UnityEvent appResumeFromSuspend;

		public UnityEvent keyboardClosed;

		public UnityEvent keyboardShown;

		private void Awake()
		{
		}

		private void OnDestroy()
		{
		}
	}
}
