using Steamworks;
using UnityEngine;
using UnityEngine.Events;

namespace Heathen.SteamworksIntegration
{
	[ModularEvents(typeof(SteamLobbyData))]
	[AddComponentMenu(null)]
	[RequireComponent(typeof(SteamLobbyData))]
	public class SteamLobbyDataEvents : MonoBehaviour
	{
		[EventField]
		public LobbyDataEvent onLobbyChange;

		[EventField]
		public UnityEvent<bool> onLobbySet;

		[EventField]
		public UnityEvent<bool> onLobbyRemoved;

		[EventField]
		public UnityEvent<string> onLobbyIdChanged;

		[EventField]
		public UnityEvent<bool> onLobbySetIsOwner;

		[EventField]
		public UnityEvent<bool> onLobbySetIsNotOwner;

		[EventField]
		public UnityEvent<bool> onLobbySetIsMember;

		[EventField]
		public UnityEvent<bool> onLobbySetIsNotMember;

		[EventField]
		public UnityEvent<UserData, LobbyData, GameData> onLobbyInvite;

		[EventField]
		public GameLobbyJoinRequestedEvent onLobbyJoinRequest;

		[EventField]
		public LobbyChatMsgEvent onChatMessage;

		[EventField]
		public LobbyDataListEvent onSearchResult;

		[EventField]
		public LobbyDataEvent onEnterSuccess;

		[EventField]
		public LobbyResponseEvent onEnterFailure;

		[EventField]
		public LobbyDataEvent onCreate;

		[EventField]
		public EResultEvent onCreationFailure;

		[EventField]
		public UnityEvent onQuickMatchFailure;

		[EventField]
		public UnityEvent<LobbyData, LobbyMemberData?> onDataUpdate;

		[EventField]
		public UnityEvent onUserLeft;

		[EventField]
		public UnityEvent onAskedToLeave;

		[EventField]
		public GameServerSetEvent onGameCreate;

		[EventField]
		public UserDataEvent onOtherUserJoined;

		[EventField]
		public UserLeaveEvent onOtherUserLeft;

		[EventField]
		public LobbyAuthenticaitonSessionEvent onAuthenticationSessionResult;

		private SteamLobbyData _mInspector;

		private void Awake()
		{
		}

		private void OnDestroy()
		{
		}

		private void HandleOnChange(LobbyData arg0)
		{
		}

		private void HandleChatMessage(LobbyChatMsg message)
		{
		}

		private void HandleAuthRequest(LobbyData lobby, UserData sender, byte[] ticket, byte[] inventory)
		{
		}

		private void HandleChatUpdate(LobbyData lobby, UserData user, EChatMemberStateChange state)
		{
		}

		private void HandleGameServerSet(LobbyData lobby, CSteamID serverId, string ip, ushort port)
		{
		}

		private void HandleLobbyLeave(LobbyData arg0)
		{
		}

		private void HandleAskedToLeave(LobbyData arg0)
		{
		}

		private void HandleLobbyDataUpdate(LobbyData lobby, LobbyMemberData? member)
		{
		}
	}
}
