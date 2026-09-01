using System;
using Steamworks;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public struct ChatRoom : IEquatable<ChatRoom>
	{
		public ClanData clan;

		public CSteamID id;

		public EChatRoomEnterResponse enterResponse;

		public readonly UserData[] Members => null;

		public readonly bool IsOpenInSteam => false;

		public bool SendMessage(string message)
		{
			return false;
		}

		public bool OpenChatWindowInSteam()
		{
			return false;
		}

		public void Leave()
		{
		}

		public bool Equals(ChatRoom other)
		{
			return false;
		}

		public override bool Equals(object obj)
		{
			return false;
		}

		public override int GetHashCode()
		{
			return 0;
		}

		public static bool operator ==(ChatRoom left, ChatRoom right)
		{
			return false;
		}

		public static bool operator !=(ChatRoom left, ChatRoom right)
		{
			return false;
		}
	}
}
