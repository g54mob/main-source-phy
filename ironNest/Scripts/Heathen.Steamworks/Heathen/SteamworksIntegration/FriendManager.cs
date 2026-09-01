using Steamworks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Heathen.SteamworksIntegration
{
	[HelpURL("https://kb.heathen.group/assets/steamworks")]
	[DisallowMultipleComponent]
	public class FriendManager : MonoBehaviour
	{
		[FormerlySerializedAs("evtGameConnectedChatMsg")]
		public GameConnectedFriendChatMsgEvent onGameConnectedChatMsg;

		[FormerlySerializedAs("evtRichPresenceUpdated")]
		public UnityEvent<UserData, AppData> onRichPresenceUpdated;

		[FormerlySerializedAs("evtPersonaStateChanged")]
		public UnityEvent<UserData, EPersonaChange> onPersonaStateChanged;

		public bool ListenForFriendsMessages
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		private void OnEnable()
		{
		}

		private void OnDisable()
		{
		}

		public UserData[] GetFriends(EFriendFlags flags)
		{
			return null;
		}

		public UserData[] GetCoplayFriends()
		{
			return null;
		}

		public string GetFriendMessage(UserData userId, int index, out EChatEntryType type)
		{
			type = default(EChatEntryType);
			return null;
		}

		public bool SendMessage(UserData friend, string message)
		{
			return false;
		}
	}
}
