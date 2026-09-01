using System;
using BitCode.Networking;

namespace TFBGames
{
	public interface IPlatformNetworkManagerService : IService
	{
		IMultiplayerSession ActiveSession { get; }

		bool IsSessionActive { get; }

		bool CanSendInvites { get; }

		void CreateSession(bool isQuickGame, Action<IMultiplayerSession, Exception> callback);

		void JoinSession(string sessionJoinInfo, Action<IMultiplayerSession, Exception> callback);

		void JoinSessionFromInvite(IGameInvitation invite, Action<IMultiplayerSession, Exception> callback);

		void LeaveActiveSession(Action<Exception> callback);

		string GetSessionJoinString(IMultiplayerSession session);

		void SendPlatformPlayerInfo();
	}
}
