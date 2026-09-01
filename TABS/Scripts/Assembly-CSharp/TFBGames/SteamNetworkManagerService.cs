using System;
using BitCode.Networking;
using BitCode.Platform.Steamworks.Networking;
using UnityEngine;

namespace TFBGames
{
	public class SteamNetworkManagerService : PlatformNetworkManagerServiceBase
	{
		public SteamMultiplayerSessionManager SteamNetworkManager
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public SteamMultiplayerSession SteamActiveSession
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public override bool CanSendInvites => true;

		protected override string PlatformString => "Steam";

		public override void CreateSession(bool isQuickGame, Action<IMultiplayerSession, Exception> callback)
		{
			callback?.Invoke(null, null);
		}

		public override void JoinSession(string sessionJoinInfo, Action<IMultiplayerSession, Exception> callback)
		{
			callback?.Invoke(null, null);
		}

		public override void JoinSessionFromInvite(IGameInvitation invite, Action<IMultiplayerSession, Exception> callback)
		{
			if (!(invite is SteamGameInvitation))
			{
				Debug.LogError("Invitation must be a Steam invitation");
			}
			else
			{
				callback?.Invoke(null, null);
			}
		}

		public override void LeaveActiveSession(Action<Exception> callback)
		{
			callback?.Invoke(null);
		}

		public override string GetSessionJoinString(IMultiplayerSession session)
		{
			return "";
		}
	}
}
