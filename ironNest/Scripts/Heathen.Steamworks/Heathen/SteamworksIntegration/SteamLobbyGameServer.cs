using Steamworks;
using UnityEngine;

namespace Heathen.SteamworksIntegration
{
	[ModularComponent(typeof(SteamLobbyData), "Game Server", null)]
	[AddComponentMenu(null)]
	[RequireComponent(typeof(SteamLobbyData))]
	public class SteamLobbyGameServer : MonoBehaviour
	{
		private SteamLobbyData _mInspector;

		private void Awake()
		{
		}

		private void OnDestroy()
		{
		}

		private bool EnsureOwner(out LobbyData data)
		{
			data = default(LobbyData);
			return false;
		}

		private void GlobalGameCreated(LobbyData lobby, CSteamID serverId, string ip, ushort port)
		{
		}

		[ContextMenu("Set as Listen Server")]
		public void SetListenServer()
		{
		}

		public void SetDedicatedSteamGameServer(CSteamID serverId)
		{
		}

		public void SetDedicatedGenericServer(string ip, ushort port)
		{
		}

		public void SetGameServer(CSteamID id, string ip, ushort port)
		{
		}

		public bool HasGameServer()
		{
			return false;
		}

		public LobbyGameServer? GetGameServer()
		{
			return null;
		}

		public string GetIdAddress()
		{
			return null;
		}

		public string GetIpAddress()
		{
			return null;
		}

		public ushort GetPort()
		{
			return 0;
		}
	}
}
