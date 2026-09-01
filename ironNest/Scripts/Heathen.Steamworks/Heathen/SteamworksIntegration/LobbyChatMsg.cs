using System;
using Steamworks;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public struct LobbyChatMsg
	{
		public LobbyData lobby;

		public EChatEntryType type;

		public UserData sender;

		public byte[] data;

		public DateTime ReceivedTime;

		public string Message => null;

		public override string ToString()
		{
			return null;
		}

		public T FromJson<T>()
		{
			return default(T);
		}

		public bool TryFromJson<T>(out T result)
		{
			result = default(T);
			return false;
		}
	}
}
