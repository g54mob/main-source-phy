using System;
using Steamworks;
using UnityEngine.Events;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public class GameConnectedFriendChatMsgEvent : UnityEvent<UserData, string, EChatEntryType>
	{
	}
}
