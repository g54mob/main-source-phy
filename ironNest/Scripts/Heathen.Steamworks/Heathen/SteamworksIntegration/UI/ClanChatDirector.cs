using Steamworks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Heathen.SteamworksIntegration.UI
{
	[HelpURL("https://kb.heathen.group/assets/steamworks/unity-engine/ui-components/clan-chat-director")]
	public class ClanChatDirector : MonoBehaviour
	{
		[Header("Events")]
		public GameConnectedChatJoinEvent evtJoin;

		[FormerlySerializedAs("evtReceived")]
		public UnityEvent<ChatRoom, UserData, string, EChatEntryType> onReceived;

		[FormerlySerializedAs("onLeave")]
		public UnityEvent<ChatRoom, UserData, bool, bool> onLeave;

		private ChatRoom? _chatRoom;

		public UserData[] Members => null;

		public bool IsOpenInSteam => false;

		public bool InRoom => false;

		public ChatRoom ChatRoom => default(ChatRoom);

		private void OnEnable()
		{
		}

		private void OnDisable()
		{
		}

		public void Join(ClanData clan)
		{
		}

		public void Leave()
		{
		}

		public void Send(string message)
		{
		}

		public void OpenInSteam()
		{
		}

		private void HandleLeave(ChatRoom room, UserData user, bool wasKicked, bool wasDropped)
		{
		}

		private void HandleJoined(ChatRoom arg0, UserData arg1)
		{
		}

		private void HandleNewMessage(ChatRoom room, UserData user, string message, EChatEntryType type)
		{
		}
	}
}
